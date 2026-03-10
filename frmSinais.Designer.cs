namespace TradeIA_Brisola
{
    partial class frmSinais
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            pbAnalise = new PictureBox();
            lbResultado = new Label();
            btnIniciar = new Button();
            btnParar = new Button();
            ((System.ComponentModel.ISupportInitialize)pbAnalise).BeginInit();
            SuspendLayout();
            // 
            // pbAnalise
            // 
            pbAnalise.Location = new Point(12, 31);
            pbAnalise.Name = "pbAnalise";
            pbAnalise.Size = new Size(729, 691);
            pbAnalise.TabIndex = 0;
            pbAnalise.TabStop = false;
            // 
            // lbResultado
            // 
            lbResultado.Location = new Point(12, 725);
            lbResultado.Name = "lbResultado";
            lbResultado.Size = new Size(729, 124);
            lbResultado.TabIndex = 1;
            lbResultado.Text = "Resultado";
            // 
            // btnIniciar
            // 
            btnIniciar.Location = new Point(12, 1);
            btnIniciar.Name = "btnIniciar";
            btnIniciar.Size = new Size(94, 29);
            btnIniciar.TabIndex = 2;
            btnIniciar.Text = "Iniciar";
            btnIniciar.UseVisualStyleBackColor = true;
            btnIniciar.Click += btnIniciar_Click;
            // 
            // btnParar
            // 
            btnParar.Location = new Point(112, 1);
            btnParar.Name = "btnParar";
            btnParar.Size = new Size(94, 29);
            btnParar.TabIndex = 3;
            btnParar.Text = "Parar";
            btnParar.UseVisualStyleBackColor = true;
            btnParar.Click += btnParar_Click;
            // 
            // frmSinais
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(753, 858);
            Controls.Add(btnParar);
            Controls.Add(btnIniciar);
            Controls.Add(lbResultado);
            Controls.Add(pbAnalise);
            Name = "frmSinais";
            Text = "TradeIA - Brisola";
            Load += frmSinais_Load;
            ((System.ComponentModel.ISupportInitialize)pbAnalise).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox pbAnalise;
        private Label lbResultado;
        private Button btnIniciar;
        private Button btnParar;
    }
}
