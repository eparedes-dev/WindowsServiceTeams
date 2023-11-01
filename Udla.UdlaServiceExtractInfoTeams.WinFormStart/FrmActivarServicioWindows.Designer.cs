namespace Udla.UdlaServiceExtractInfoTeams.WinFormStart
{
    partial class FrmActivarServicioWindows
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
            this.label1 = new System.Windows.Forms.Label();
            this.btnIniciarLog = new System.Windows.Forms.Button();
            this.btnArchivoConfiguracionVarios = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(46, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(262, 28);
            this.label1.TabIndex = 0;
            this.label1.Text = "SDS Teams Sincronization Service";
            // 
            // btnIniciarLog
            // 
            this.btnIniciarLog.Location = new System.Drawing.Point(94, 51);
            this.btnIniciarLog.Name = "btnIniciarLog";
            this.btnIniciarLog.Size = new System.Drawing.Size(167, 43);
            this.btnIniciarLog.TabIndex = 1;
            this.btnIniciarLog.Text = "Iniciar Servicio Windows";
            this.btnIniciarLog.UseVisualStyleBackColor = true;
            this.btnIniciarLog.Click += new System.EventHandler(this.btnIniciarLog_Click);
            // 
            // btnArchivoConfiguracionVarios
            // 
            this.btnArchivoConfiguracionVarios.Location = new System.Drawing.Point(94, 115);
            this.btnArchivoConfiguracionVarios.Name = "btnArchivoConfiguracionVarios";
            this.btnArchivoConfiguracionVarios.Size = new System.Drawing.Size(167, 46);
            this.btnArchivoConfiguracionVarios.TabIndex = 2;
            this.btnArchivoConfiguracionVarios.Text = "Archivo de Configuración Multiple";
            this.btnArchivoConfiguracionVarios.UseVisualStyleBackColor = true;
            // 
            // FrmActivarServicioWindows
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(351, 196);
            this.Controls.Add(this.btnArchivoConfiguracionVarios);
            this.Controls.Add(this.btnIniciarLog);
            this.Controls.Add(this.label1);
            this.Name = "FrmActivarServicioWindows";
            this.Text = "Agendas Mentor";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnIniciarLog;
        private System.Windows.Forms.Button btnArchivoConfiguracionVarios;
    }
}

