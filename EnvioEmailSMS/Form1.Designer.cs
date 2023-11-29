namespace EnvioEmailSMS
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.btnCargar = new System.Windows.Forms.Button();
            this.Label3 = new System.Windows.Forms.Label();
            this.btnFichero = new System.Windows.Forms.Button();
            this.txtFichero = new System.Windows.Forms.TextBox();
            this.cboCartera = new System.Windows.Forms.ComboBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rBtnEmail = new System.Windows.Forms.RadioButton();
            this.rBtnSMS = new System.Windows.Forms.RadioButton();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCargar
            // 
            this.btnCargar.Location = new System.Drawing.Point(225, 124);
            this.btnCargar.Name = "btnCargar";
            this.btnCargar.Size = new System.Drawing.Size(75, 23);
            this.btnCargar.TabIndex = 21;
            this.btnCargar.Text = "Guardar";
            this.btnCargar.UseVisualStyleBackColor = true;
            this.btnCargar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Location = new System.Drawing.Point(13, 93);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(42, 13);
            this.Label3.TabIndex = 20;
            this.Label3.Text = "Fichero";
            // 
            // btnFichero
            // 
            this.btnFichero.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFichero.Location = new System.Drawing.Point(271, 89);
            this.btnFichero.Name = "btnFichero";
            this.btnFichero.Size = new System.Drawing.Size(29, 23);
            this.btnFichero.TabIndex = 19;
            this.btnFichero.Text = "...";
            this.btnFichero.UseVisualStyleBackColor = true;
            this.btnFichero.Click += new System.EventHandler(this.btnFichero_Click);
            // 
            // txtFichero
            // 
            this.txtFichero.Location = new System.Drawing.Point(83, 90);
            this.txtFichero.Name = "txtFichero";
            this.txtFichero.Size = new System.Drawing.Size(183, 20);
            this.txtFichero.TabIndex = 18;
            // 
            // cboCartera
            // 
            this.cboCartera.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCartera.FormattingEnabled = true;
            this.cboCartera.Location = new System.Drawing.Point(83, 17);
            this.cboCartera.Name = "cboCartera";
            this.cboCartera.Size = new System.Drawing.Size(218, 21);
            this.cboCartera.TabIndex = 17;
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(13, 20);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(41, 13);
            this.Label1.TabIndex = 16;
            this.Label1.Text = "Cartera";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.rBtnEmail);
            this.panel1.Controls.Add(this.rBtnSMS);
            this.panel1.Location = new System.Drawing.Point(83, 47);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(217, 34);
            this.panel1.TabIndex = 0;
            // 
            // rBtnEmail
            // 
            this.rBtnEmail.AutoSize = true;
            this.rBtnEmail.Location = new System.Drawing.Point(117, 9);
            this.rBtnEmail.Name = "rBtnEmail";
            this.rBtnEmail.Size = new System.Drawing.Size(53, 17);
            this.rBtnEmail.TabIndex = 25;
            this.rBtnEmail.Text = "E-mail";
            this.rBtnEmail.UseVisualStyleBackColor = true;
            // 
            // rBtnSMS
            // 
            this.rBtnSMS.AutoCheck = false;
            this.rBtnSMS.AutoSize = true;
            this.rBtnSMS.Location = new System.Drawing.Point(47, 9);
            this.rBtnSMS.Name = "rBtnSMS";
            this.rBtnSMS.Size = new System.Drawing.Size(48, 17);
            this.rBtnSMS.TabIndex = 24;
            this.rBtnSMS.Text = "SMS";
            this.rBtnSMS.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(314, 161);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnCargar);
            this.Controls.Add(this.Label3);
            this.Controls.Add(this.btnFichero);
            this.Controls.Add(this.txtFichero);
            this.Controls.Add(this.cboCartera);
            this.Controls.Add(this.Label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Envio SMS - Email";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Button btnCargar;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.Button btnFichero;
        internal System.Windows.Forms.TextBox txtFichero;
        internal System.Windows.Forms.ComboBox cboCartera;
        internal System.Windows.Forms.Label Label1;
        private System.Windows.Forms.HelpProvider helpProvider1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rBtnEmail;
        private System.Windows.Forms.RadioButton rBtnSMS;
    }
}

