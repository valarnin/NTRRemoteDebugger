namespace NTRDebuggerTool.Forms
{
    partial class GateSharkImportDialog
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
            this.ButtonCancel = new System.Windows.Forms.Button();
            this.ButtonImport = new System.Windows.Forms.Button();
            this.DescriptionTextBox = new System.Windows.Forms.TextBox();
            this.CodeDescriptionLabel = new System.Windows.Forms.Label();
            this.LabelCode = new System.Windows.Forms.Label();
            this.CodeTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // ButtonCancel
            // 
            this.ButtonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.ButtonCancel.Location = new System.Drawing.Point(13, 227);
            this.ButtonCancel.Name = "ButtonCancel";
            this.ButtonCancel.Size = new System.Drawing.Size(75, 23);
            this.ButtonCancel.TabIndex = 0;
            this.ButtonCancel.Text = "Cancel";
            this.ButtonCancel.UseVisualStyleBackColor = true;
            this.ButtonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
            // 
            // ButtonImport
            // 
            this.ButtonImport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonImport.Location = new System.Drawing.Point(197, 227);
            this.ButtonImport.Name = "ButtonImport";
            this.ButtonImport.Size = new System.Drawing.Size(75, 23);
            this.ButtonImport.TabIndex = 1;
            this.ButtonImport.Text = "Import";
            this.ButtonImport.UseVisualStyleBackColor = true;
            this.ButtonImport.Click += new System.EventHandler(this.ButtonImport_Click);
            // 
            // DescriptionTextBox
            // 
            this.DescriptionTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.DescriptionTextBox.Location = new System.Drawing.Point(13, 25);
            this.DescriptionTextBox.Name = "DescriptionTextBox";
            this.DescriptionTextBox.Size = new System.Drawing.Size(259, 20);
            this.DescriptionTextBox.TabIndex = 21;
            // 
            // CodeDescriptionLabel
            // 
            this.CodeDescriptionLabel.AutoSize = true;
            this.CodeDescriptionLabel.Location = new System.Drawing.Point(12, 9);
            this.CodeDescriptionLabel.Name = "CodeDescriptionLabel";
            this.CodeDescriptionLabel.Size = new System.Drawing.Size(88, 13);
            this.CodeDescriptionLabel.TabIndex = 22;
            this.CodeDescriptionLabel.Text = "Code Description";
            // 
            // LabelCode
            // 
            this.LabelCode.AutoSize = true;
            this.LabelCode.Location = new System.Drawing.Point(12, 48);
            this.LabelCode.Name = "LabelCode";
            this.LabelCode.Size = new System.Drawing.Size(32, 13);
            this.LabelCode.TabIndex = 23;
            this.LabelCode.Text = "Code";
            // 
            // CodeTextBox
            // 
            this.CodeTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CodeTextBox.Location = new System.Drawing.Point(13, 64);
            this.CodeTextBox.Multiline = true;
            this.CodeTextBox.Name = "CodeTextBox";
            this.CodeTextBox.Size = new System.Drawing.Size(259, 157);
            this.CodeTextBox.TabIndex = 24;
            // 
            // GateSharkImportDialog
            // 
            this.AcceptButton = this.ButtonImport;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.ButtonCancel;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.ControlBox = false;
            this.Controls.Add(this.CodeTextBox);
            this.Controls.Add(this.LabelCode);
            this.Controls.Add(this.CodeDescriptionLabel);
            this.Controls.Add(this.DescriptionTextBox);
            this.Controls.Add(this.ButtonImport);
            this.Controls.Add(this.ButtonCancel);
            this.MinimumSize = new System.Drawing.Size(300, 300);
            this.Name = "GateSharkImportDialog";
            this.ShowIcon = false;
            this.Text = "GateShark Code Import";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ButtonCancel;
        private System.Windows.Forms.Button ButtonImport;
        internal System.Windows.Forms.TextBox DescriptionTextBox;
        private System.Windows.Forms.Label CodeDescriptionLabel;
        private System.Windows.Forms.Label LabelCode;
        internal System.Windows.Forms.TextBox CodeTextBox;
    }
}