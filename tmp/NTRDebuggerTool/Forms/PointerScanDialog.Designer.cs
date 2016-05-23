namespace NTRDebuggerTool.Forms
{
    partial class PointerScanDialog
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
            this.TextAddress = new System.Windows.Forms.TextBox();
            this.LabelAddress = new System.Windows.Forms.Label();
            this.TextMaxOffset = new System.Windows.Forms.TextBox();
            this.LabelMaxOffset = new System.Windows.Forms.Label();
            this.ResultsDataGrid = new System.Windows.Forms.DataGridView();
            this.AddressColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OffsetColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CancelButton = new System.Windows.Forms.Button();
            this.SearchButton = new System.Windows.Forms.Button();
            this.CheckFullSearch = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.ResultsDataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // TextAddress
            // 
            this.TextAddress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TextAddress.Location = new System.Drawing.Point(63, 9);
            this.TextAddress.Name = "TextAddress";
            this.TextAddress.ReadOnly = true;
            this.TextAddress.Size = new System.Drawing.Size(65, 20);
            this.TextAddress.TabIndex = 28;
            // 
            // LabelAddress
            // 
            this.LabelAddress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LabelAddress.Location = new System.Drawing.Point(12, 9);
            this.LabelAddress.Name = "LabelAddress";
            this.LabelAddress.Size = new System.Drawing.Size(45, 23);
            this.LabelAddress.TabIndex = 29;
            this.LabelAddress.Text = "Address";
            this.LabelAddress.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // TextMaxOffset
            // 
            this.TextMaxOffset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TextMaxOffset.Location = new System.Drawing.Point(142, 9);
            this.TextMaxOffset.Name = "TextMaxOffset";
            this.TextMaxOffset.Size = new System.Drawing.Size(65, 20);
            this.TextMaxOffset.TabIndex = 30;
            // 
            // LabelMaxOffset
            // 
            this.LabelMaxOffset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LabelMaxOffset.Location = new System.Drawing.Point(213, 9);
            this.LabelMaxOffset.Name = "LabelMaxOffset";
            this.LabelMaxOffset.Size = new System.Drawing.Size(59, 23);
            this.LabelMaxOffset.TabIndex = 31;
            this.LabelMaxOffset.Text = "Max Offset";
            this.LabelMaxOffset.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ResultsDataGrid
            // 
            this.ResultsDataGrid.AllowUserToAddRows = false;
            this.ResultsDataGrid.AllowUserToDeleteRows = false;
            this.ResultsDataGrid.AllowUserToResizeRows = false;
            this.ResultsDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ResultsDataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.AddressColumn,
            this.OffsetColumn});
            this.ResultsDataGrid.Location = new System.Drawing.Point(12, 35);
            this.ResultsDataGrid.Name = "ResultsDataGrid";
            this.ResultsDataGrid.ReadOnly = true;
            this.ResultsDataGrid.RowHeadersVisible = false;
            this.ResultsDataGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.ResultsDataGrid.Size = new System.Drawing.Size(260, 146);
            this.ResultsDataGrid.TabIndex = 32;
            this.ResultsDataGrid.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ResultsDataGrid_CellDoubleClick);
            // 
            // AddressColumn
            // 
            this.AddressColumn.HeaderText = "Address";
            this.AddressColumn.Name = "AddressColumn";
            this.AddressColumn.ReadOnly = true;
            // 
            // OffsetColumn
            // 
            this.OffsetColumn.HeaderText = "Offset";
            this.OffsetColumn.Name = "OffsetColumn";
            this.OffsetColumn.ReadOnly = true;
            // 
            // CancelButton
            // 
            this.CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelButton.Location = new System.Drawing.Point(15, 187);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(75, 23);
            this.CancelButton.TabIndex = 33;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // SearchButton
            // 
            this.SearchButton.Location = new System.Drawing.Point(197, 187);
            this.SearchButton.Name = "SearchButton";
            this.SearchButton.Size = new System.Drawing.Size(75, 23);
            this.SearchButton.TabIndex = 34;
            this.SearchButton.Text = "Search";
            this.SearchButton.UseVisualStyleBackColor = true;
            this.SearchButton.Click += new System.EventHandler(this.SearchButton_Click);
            // 
            // CheckFullSearch
            // 
            this.CheckFullSearch.AutoSize = true;
            this.CheckFullSearch.Location = new System.Drawing.Point(97, 192);
            this.CheckFullSearch.Name = "CheckFullSearch";
            this.CheckFullSearch.Size = new System.Drawing.Size(79, 17);
            this.CheckFullSearch.TabIndex = 35;
            this.CheckFullSearch.Text = "Full Search";
            this.CheckFullSearch.UseVisualStyleBackColor = true;
            // 
            // PointerScanDialog
            // 
            this.AcceptButton = this.SearchButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 222);
            this.ControlBox = false;
            this.Controls.Add(this.CheckFullSearch);
            this.Controls.Add(this.SearchButton);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.ResultsDataGrid);
            this.Controls.Add(this.LabelMaxOffset);
            this.Controls.Add(this.TextMaxOffset);
            this.Controls.Add(this.LabelAddress);
            this.Controls.Add(this.TextAddress);
            this.MaximumSize = new System.Drawing.Size(300, 260);
            this.MinimumSize = new System.Drawing.Size(300, 260);
            this.Name = "PointerScanDialog";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Pointer Scan";
            ((System.ComponentModel.ISupportInitialize)(this.ResultsDataGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.TextBox TextAddress;
        private System.Windows.Forms.Label LabelAddress;
        internal System.Windows.Forms.TextBox TextMaxOffset;
        private System.Windows.Forms.Label LabelMaxOffset;
        private System.Windows.Forms.DataGridView ResultsDataGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn AddressColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn OffsetColumn;
        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.Button SearchButton;
        private System.Windows.Forms.CheckBox CheckFullSearch;
    }
}