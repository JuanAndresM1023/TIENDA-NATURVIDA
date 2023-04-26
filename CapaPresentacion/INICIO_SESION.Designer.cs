namespace CapaPresentacion
{
    partial class INICIO_SESION
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtboxusuario = new System.Windows.Forms.TextBox();
            this.txtboxcontraseña = new System.Windows.Forms.TextBox();
            this.btniniciar = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.btnsalir = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(123, 79);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "USUARIO";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(123, 155);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "CONTRASEÑA";
            // 
            // txtboxusuario
            // 
            this.txtboxusuario.Location = new System.Drawing.Point(126, 112);
            this.txtboxusuario.Name = "txtboxusuario";
            this.txtboxusuario.ShortcutsEnabled = false;
            this.txtboxusuario.Size = new System.Drawing.Size(166, 20);
            this.txtboxusuario.TabIndex = 1;
            // 
            // txtboxcontraseña
            // 
            this.txtboxcontraseña.Location = new System.Drawing.Point(126, 190);
            this.txtboxcontraseña.Name = "txtboxcontraseña";
            this.txtboxcontraseña.ShortcutsEnabled = false;
            this.txtboxcontraseña.Size = new System.Drawing.Size(166, 20);
            this.txtboxcontraseña.TabIndex = 2;
            this.txtboxcontraseña.UseSystemPasswordChar = true;
            // 
            // btniniciar
            // 
            this.btniniciar.Location = new System.Drawing.Point(345, 239);
            this.btniniciar.Name = "btniniciar";
            this.btniniciar.Size = new System.Drawing.Size(76, 25);
            this.btniniciar.TabIndex = 4;
            this.btniniciar.Text = "INICIAR";
            this.btniniciar.UseVisualStyleBackColor = true;
            this.btniniciar.Click += new System.EventHandler(this.btniniciar_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(121, 26);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(184, 25);
            this.label4.TabIndex = 8;
            this.label4.Text = "INICIAR SESION";
            // 
            // btnsalir
            // 
            this.btnsalir.Location = new System.Drawing.Point(12, 239);
            this.btnsalir.Name = "btnsalir";
            this.btnsalir.Size = new System.Drawing.Size(75, 23);
            this.btnsalir.TabIndex = 9;
            this.btnsalir.Text = "SALIR";
            this.btnsalir.UseVisualStyleBackColor = true;
            this.btnsalir.Click += new System.EventHandler(this.btnsalir_Click);
            // 
            // INICIO_SESION
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(433, 278);
            this.Controls.Add(this.btnsalir);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btniniciar);
            this.Controls.Add(this.txtboxcontraseña);
            this.Controls.Add(this.txtboxusuario);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "INICIO_SESION";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "INICIO SESION";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtboxusuario;
        private System.Windows.Forms.TextBox txtboxcontraseña;
        private System.Windows.Forms.Button btniniciar;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnsalir;
    }
}