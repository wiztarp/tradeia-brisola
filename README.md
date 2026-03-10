# TradeIA Brisola

Aplicação desktop desenvolvida em **C# (.NET / Windows Forms)** para captura automática de gráficos e análise utilizando **Inteligência Artificial**.

O sistema captura uma área específica da tela onde está o gráfico de velas e envia a imagem para uma IA para análise de padrões de mercado, retornando um possível sinal de **Compra, Venda ou Esperar** com justificativa.

---

# Tecnologias utilizadas

- C#
- .NET
- Windows Forms
- HttpClient
- Newtonsoft.Json
- Integração com API de Inteligência Artificial
- Captura de tela com System.Drawing

---

# Funcionalidades

- Captura automática de área da tela com gráfico
- Envio da imagem para análise por IA
- Retorno com sinal de operação (Compra / Venda / Esperar)
- Exibição da justificativa da análise
- Execução automática em intervalo configurado
- Interface simples para iniciar ou pausar a análise

---

# Como funciona

1. O sistema captura automaticamente a área do gráfico definida no código.
2. A imagem é convertida para **Base64**.
3. A imagem é enviada para uma **API de IA**.
4. A IA analisa o comportamento das velas.
5. O sistema recebe um retorno com:
   - Sinal
   - Probabilidade
   - Justificativa da análise.

---

# Objetivo do projeto

Este projeto foi desenvolvido para estudo de:

- Automação de análise de gráficos
- Integração com APIs de Inteligência Artificial
- Captura de tela em aplicações desktop
- Processamento de imagens e envio para APIs

---

# Estrutura do projeto


---

# Observação

Este projeto tem finalidade **educacional e experimental**, voltado ao estudo de automação e integração com inteligência artificial aplicada à análise de gráficos.

---

# Autor

Kenny Brisola

Software Developer  
C# | .NET | SQL Server | APIs | Automação de Processos