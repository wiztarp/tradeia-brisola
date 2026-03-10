using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;
using System;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Reflection.Emit;

namespace TradeIA_Brisola
{
    public partial class frmSinais : Form
    {
        private Timer timer;
        private readonly HttpClient client;

        public frmSinais()
        {
            InitializeComponent();

            timer = new Timer();
            timer.Interval = 10000; // 10 segundos
            timer.Tick += Timer_Tick;

            client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "SUA_CHAVE_AQUI");
        }

        private async void Timer_Tick(object sender, EventArgs e)
        {
            try
            {
                using (var imagem = CapturarGrafico())
                {
                    string resultado = await EnviarImagemParaGPTAsync(imagem);
                    lbResultado.Text = resultado;
                }
            }
            catch (Exception ex)
            {
                lbResultado.Text = "Erro: " + ex.Message;
            }
        }

        private async Task<string> EnviarImagemParaGPTAsync(Image imagem)
        {
            using (var ms = new System.IO.MemoryStream())
            {
                imagem.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                var imagemBase64 = Convert.ToBase64String(ms.ToArray());

                var payload = new
                {
                    model = "gpt-4o",
                    messages = new object[]
                    {
                        new { role = "user", content = new object[]
                            {
                                new { type = "text", text = @"
Você é uma Inteligência Artificial especialista em análise de gráficos de velas de 1 minuto para operações de trade binário.

Seu objetivo é observar o comportamento da última(s) vela(s) e identificar se há uma oportunidade clara de COMPRA, VENDA ou se o momento é de ESPERA. Você deve considerar pavios, corpo da vela, sequência anterior, reversões, rompimentos e médias móveis visuais.

---

### 🧠 Conhecimentos aprendidos:

#### 🔁 Padrão de resposta (obrigatório):
- A primeira linha deve conter apenas:
  - ""Compra [porcentagem]%""
  - ""Venda [porcentagem]%""
  - ou ""Esperar [porcentagem]%""
- A segunda linha deve começar com: ""Justificativa: ""

Exemplo de resposta:
```
Compra 82%
Justificativa: Candle de força com pavio inferior, rompendo média móvel após sequência de correção. Indica retomada de alta com confirmação.
```

---

### 📊 Regras de operação:

#### ✅ Sinais válidos:
- **Compra**: Pavios inferiores + candle de força com corpo grande e rompendo média móvel de forma clara
- **Venda**: Pavios superiores + topos arredondados + candle de força rompendo fundo anterior
- **Esperar**: Velas pequenas, laterais, com pavios curtos ou sinais mistos; região de indecisão

#### ⚠️ Importante:
- Só dar **sinal de entrada** (compra ou venda) se houver **confirmação visual forte** e a chance for **maior que 75%**
- Em caso de dúvida ou padrão fraco, sempre marcar como ESPERAR
- Nunca diga ""não posso analisar"" — sua análise é visual e com base na imagem do gráfico

---

### 🧪 Aprendizado com WIN e LOSS:
- Se uma entrada for WIN, considere o padrão como positivo
- Se for LOSS, use para refinar: talvez o candle era fraco ou havia armadilha

---

### 📂 Ativos e comportamentos:

#### UKOIL
- Costuma formar topos arredondados com pavio superior
- Muitas vezes faz falso pullback na média antes de reversão
- Entradas com candles pequenos são arriscadas

#### Facebook OTC
- Apresenta reversões após candles com pavio no topo e corpo fraco
- Tende a lateralizar antes de grandes movimentos
- Pullbacks suaves indicam boa retomada de tendência

#### JPY/USD OTC
- Costuma criar armadilhas no topo com pavios longos
- Falsas reversões antes da real ruptura
- Confirmar reversão com sequência forte é essencial

---

Use sempre as regras acima para avaliar a imagem que será enviada. Retorne apenas a linha com o sinal e porcentagem, seguida da justificativa. Sem emojis, sem explicações genéricas." },
                                new { type = "image_url", image_url = new { url = "data:image/png;base64," + imagemBase64 } }
                            }
                        }
                    },
                    max_tokens = 500
                };

                var json = JsonConvert.SerializeObject(payload);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync("https://api.openai.com/v1/chat/completions", content);
                var result = await response.Content.ReadAsStringAsync();

                dynamic parsed = JsonConvert.DeserializeObject<dynamic>(result);
                string resposta = parsed?.choices?[0]?.message?.content ?? "Erro na resposta da IA";

                // Quebra a resposta em linhas para extrair as partes
                string[] linhas = resposta.Split('\n', StringSplitOptions.RemoveEmptyEntries);

                string sinal = "Esperar";
                int porcentagem = 60;
                string justificativa = "";

                // Tenta encontrar a linha com o sinal e a porcentagem
                foreach (var linha in linhas)
                {
                    if (linha.Contains("compra", StringComparison.OrdinalIgnoreCase)) { sinal = "Compra"; }
                    if (linha.Contains("venda", StringComparison.OrdinalIgnoreCase)) { sinal = "Venda"; }
                    if (linha.Contains("esperar", StringComparison.OrdinalIgnoreCase)) { sinal = "Esperar"; }

                    // Extrai a porcentagem se houver
                    var match = System.Text.RegularExpressions.Regex.Match(linha, @"\d{2,3}%");
                    if (match.Success)
                    {
                        int.TryParse(match.Value.Replace("%", ""), out porcentagem);
                    }

                    // Pega a justificativa (primeira linha que começa com Justificativa)
                    if (linha.Trim().ToLower().StartsWith("justificativa"))
                    {
                        justificativa = linha.Trim();
                    }
                }

                return $"{sinal} {porcentagem}%\n{justificativa}";
            }
        }

        private Bitmap CapturarGrafico()
        {
            Rectangle area = new Rectangle(850, 286, 1920, 1080); // Ajuste conforme seu gráfico
            Bitmap bmp = new Bitmap(area.Width, area.Height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.CopyFromScreen(area.Location, Point.Empty, area.Size);
            }
            pbAnalise.Image = bmp;
            return bmp;
        }


        private void btnIniciar_Click(object sender, EventArgs e)
        {
            timer.Start();
            lbResultado.Text = "⏳ Iniciando análise...";
        }

        private void frmSinais_Load(object sender, EventArgs e)
        {
        }

        private void btnParar_Click(object sender, EventArgs e)
        {
            timer.Stop();
            lbResultado.Text = "🛑 Análise pausada";
        }



    }

}

