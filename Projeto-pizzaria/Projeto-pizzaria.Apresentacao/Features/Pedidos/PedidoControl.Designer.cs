namespace Projeto_pizzaria.Apresentacao.Features.Pedidos
{
    partial class PedidoControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.listPedidos = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // listPedidos
            // 
            this.listPedidos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listPedidos.FormattingEnabled = true;
            this.listPedidos.Location = new System.Drawing.Point(0, 0);
            this.listPedidos.Name = "listPedidos";
            this.listPedidos.Size = new System.Drawing.Size(150, 150);
            this.listPedidos.TabIndex = 0;
            // 
            // PedidoControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.listPedidos);
            this.Name = "PedidoControl";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox listPedidos;
    }
}
