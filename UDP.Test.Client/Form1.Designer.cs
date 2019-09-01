namespace UDP.Test.Client
{
    partial class Form1
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtMensagem = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtConsole = new System.Windows.Forms.TextBox();
            this.txtIp = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtMensagem
            // 
            this.txtMensagem.Location = new System.Drawing.Point(56, 158);
            this.txtMensagem.Name = "txtMensagem";
            this.txtMensagem.Size = new System.Drawing.Size(695, 22);
            this.txtMensagem.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(56, 198);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(695, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Enviar Mensagem para o servidor UDP";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(53, 138);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Mensagem";
            // 
            // txtConsole
            // 
            this.txtConsole.BackColor = System.Drawing.Color.Black;
            this.txtConsole.ForeColor = System.Drawing.Color.Lime;
            this.txtConsole.Location = new System.Drawing.Point(56, 227);
            this.txtConsole.Multiline = true;
            this.txtConsole.Name = "txtConsole";
            this.txtConsole.Size = new System.Drawing.Size(695, 197);
            this.txtConsole.TabIndex = 3;
            this.txtConsole.Text = "Console";
            // 
            // txtIp
            // 
            this.txtIp.Location = new System.Drawing.Point(9, 56);
            this.txtIp.Name = "txtIp";
            this.txtIp.Size = new System.Drawing.Size(284, 22);
            this.txtIp.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(20, 17);
            this.label2.TabIndex = 5;
            this.label2.Text = "IP";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtPort);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtIp);
            this.groupBox1.Location = new System.Drawing.Point(56, 37);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(695, 100);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Configuracoes Servidor UDP";
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(318, 56);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(147, 22);
            this.txtPort.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(315, 36);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 17);
            this.label3.TabIndex = 7;
            this.label3.Text = "Porta";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txtConsole);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtMensagem);
            this.Name = "Form1";
            this.Text = "Form1";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtMensagem;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtConsole;
        private System.Windows.Forms.TextBox txtIp;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPort;
    }
}

