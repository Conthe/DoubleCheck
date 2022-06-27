
namespace DoubleCheck
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
            this.btn_ComparaArqvs = new System.Windows.Forms.Button();
            this.cmb_EscolheAP = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // btn_ComparaArqvs
            // 
            this.btn_ComparaArqvs.Location = new System.Drawing.Point(532, 105);
            this.btn_ComparaArqvs.Name = "btn_ComparaArqvs";
            this.btn_ComparaArqvs.Size = new System.Drawing.Size(75, 23);
            this.btn_ComparaArqvs.TabIndex = 0;
            this.btn_ComparaArqvs.Text = "Arquivos..";
            this.btn_ComparaArqvs.UseVisualStyleBackColor = true;
            this.btn_ComparaArqvs.Click += new System.EventHandler(this.btn_ComparaArqvs_Click);
            // 
            // cmb_EscolheAP
            // 
            this.cmb_EscolheAP.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_EscolheAP.ForeColor = System.Drawing.SystemColors.InfoText;
            this.cmb_EscolheAP.FormattingEnabled = true;
            this.cmb_EscolheAP.Items.AddRange(new object[] {
            "AP008",
            "AP009",
            "AP010",
            "AP011",
            "AP012"});
            this.cmb_EscolheAP.Location = new System.Drawing.Point(420, 107);
            this.cmb_EscolheAP.Name = "cmb_EscolheAP";
            this.cmb_EscolheAP.Size = new System.Drawing.Size(89, 21);
            this.cmb_EscolheAP.TabIndex = 1;
            this.cmb_EscolheAP.SelectedIndexChanged += new System.EventHandler(this.cmb_EscolheAP_SelectedIndexChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.cmb_EscolheAP);
            this.Controls.Add(this.btn_ComparaArqvs);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_ComparaArqvs;
        private System.Windows.Forms.ComboBox cmb_EscolheAP;
    }
}

