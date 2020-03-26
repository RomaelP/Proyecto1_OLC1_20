namespace WindowsFormsApplication1
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.aRCHIVOToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aBRIRToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gUARDARToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gUARDARToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.sALIRToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.txtEntrada = new System.Windows.Forms.RichTextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.button1 = new System.Windows.Forms.Button();
            this.rtbConsola = new System.Windows.Forms.RichTextBox();
            this.gENERARToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aRCHIVODETOKENSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aRCHIVODEERRORESToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aRCHIVOToolStripMenuItem,
            this.gENERARToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(795, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // aRCHIVOToolStripMenuItem
            // 
            this.aRCHIVOToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aBRIRToolStripMenuItem,
            this.gUARDARToolStripMenuItem,
            this.gUARDARToolStripMenuItem1,
            this.sALIRToolStripMenuItem});
            this.aRCHIVOToolStripMenuItem.Name = "aRCHIVOToolStripMenuItem";
            this.aRCHIVOToolStripMenuItem.Size = new System.Drawing.Size(70, 20);
            this.aRCHIVOToolStripMenuItem.Text = "ARCHIVO";
            // 
            // aBRIRToolStripMenuItem
            // 
            this.aBRIRToolStripMenuItem.Name = "aBRIRToolStripMenuItem";
            this.aBRIRToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.aBRIRToolStripMenuItem.Text = "ABRIR";
            this.aBRIRToolStripMenuItem.Click += new System.EventHandler(this.aBRIRToolStripMenuItem_Click);
            // 
            // gUARDARToolStripMenuItem
            // 
            this.gUARDARToolStripMenuItem.Name = "gUARDARToolStripMenuItem";
            this.gUARDARToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.gUARDARToolStripMenuItem.Text = "NUEVO";
            this.gUARDARToolStripMenuItem.Click += new System.EventHandler(this.gUARDARToolStripMenuItem_Click);
            // 
            // gUARDARToolStripMenuItem1
            // 
            this.gUARDARToolStripMenuItem1.Name = "gUARDARToolStripMenuItem1";
            this.gUARDARToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.gUARDARToolStripMenuItem1.Text = "GUARDAR";
            this.gUARDARToolStripMenuItem1.Click += new System.EventHandler(this.gUARDARToolStripMenuItem1_Click);
            // 
            // sALIRToolStripMenuItem
            // 
            this.sALIRToolStripMenuItem.Name = "sALIRToolStripMenuItem";
            this.sALIRToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.sALIRToolStripMenuItem.Text = "SALIR";
            this.sALIRToolStripMenuItem.Click += new System.EventHandler(this.sALIRToolStripMenuItem_Click);
            // 
            // txtEntrada
            // 
            this.txtEntrada.Location = new System.Drawing.Point(13, 28);
            this.txtEntrada.Name = "txtEntrada";
            this.txtEntrada.Size = new System.Drawing.Size(443, 314);
            this.txtEntrada.TabIndex = 1;
            this.txtEntrada.Text = "";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(13, 348);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(443, 35);
            this.button1.TabIndex = 2;
            this.button1.Text = "ANALIZAR";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // rtbConsola
            // 
            this.rtbConsola.Location = new System.Drawing.Point(12, 389);
            this.rtbConsola.Name = "rtbConsola";
            this.rtbConsola.Size = new System.Drawing.Size(771, 119);
            this.rtbConsola.TabIndex = 4;
            this.rtbConsola.Text = "";
            // 
            // gENERARToolStripMenuItem
            // 
            this.gENERARToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aRCHIVODETOKENSToolStripMenuItem,
            this.aRCHIVODEERRORESToolStripMenuItem});
            this.gENERARToolStripMenuItem.Name = "gENERARToolStripMenuItem";
            this.gENERARToolStripMenuItem.Size = new System.Drawing.Size(70, 20);
            this.gENERARToolStripMenuItem.Text = "GENERAR";
            // 
            // aRCHIVODETOKENSToolStripMenuItem
            // 
            this.aRCHIVODETOKENSToolStripMenuItem.Name = "aRCHIVODETOKENSToolStripMenuItem";
            this.aRCHIVODETOKENSToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.aRCHIVODETOKENSToolStripMenuItem.Text = "ARCHIVO DE TOKENS";
            this.aRCHIVODETOKENSToolStripMenuItem.Click += new System.EventHandler(this.aRCHIVODETOKENSToolStripMenuItem_Click);
            // 
            // aRCHIVODEERRORESToolStripMenuItem
            // 
            this.aRCHIVODEERRORESToolStripMenuItem.Name = "aRCHIVODEERRORESToolStripMenuItem";
            this.aRCHIVODEERRORESToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.aRCHIVODEERRORESToolStripMenuItem.Text = "ARCHIVO DE ERRORES";
            this.aRCHIVODEERRORESToolStripMenuItem.Click += new System.EventHandler(this.aRCHIVODEERRORESToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(795, 520);
            this.Controls.Add(this.rtbConsola);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtEntrada);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Proyecto 1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem aRCHIVOToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aBRIRToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gUARDARToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gUARDARToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem sALIRToolStripMenuItem;        
        private System.Windows.Forms.RichTextBox txtEntrada;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RichTextBox rtbConsola;
        private System.Windows.Forms.ToolStripMenuItem gENERARToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aRCHIVODETOKENSToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aRCHIVODEERRORESToolStripMenuItem;
    }
}

