﻿namespace Mariana.WinApp.Features.DisciplinaModule
{
    partial class DisciplinaControl
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
            this.listDisciplinas = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // listDisciplinas
            // 
            this.listDisciplinas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listDisciplinas.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listDisciplinas.FormattingEnabled = true;
            this.listDisciplinas.ItemHeight = 20;
            this.listDisciplinas.Location = new System.Drawing.Point(0, 0);
            this.listDisciplinas.Margin = new System.Windows.Forms.Padding(2);
            this.listDisciplinas.Name = "listDisciplinas";
            this.listDisciplinas.Size = new System.Drawing.Size(607, 308);
            this.listDisciplinas.Sorted = true;
            this.listDisciplinas.TabIndex = 2;
            // 
            // DisciplinaControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.listDisciplinas);
            this.Name = "DisciplinaControl";
            this.Size = new System.Drawing.Size(607, 308);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox listDisciplinas;
    }
}
