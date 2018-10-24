namespace Projeto_pizzaria.Apresentacao.Features.ItensPedido
{
	partial class ItensPedidoControl
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose( bool disposing )
		{
			if (disposing && ( components != null ))
			{
				components.Dispose();
			}
			base.Dispose( disposing );
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.listItensPedido = new System.Windows.Forms.ListBox();
			this.SuspendLayout();
			// 
			// listItensPedido
			// 
			this.listItensPedido.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listItensPedido.FormattingEnabled = true;
			this.listItensPedido.Location = new System.Drawing.Point(0, 0);
			this.listItensPedido.Name = "listItensPedido";
			this.listItensPedido.Size = new System.Drawing.Size(150, 150);
			this.listItensPedido.TabIndex = 0;
			// 
			// ItensPedidoControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.listItensPedido);
			this.Name = "ItensPedidoControl";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ListBox listItensPedido;
	}
}
