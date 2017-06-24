namespace NTRDebuggerTool.Forms
{
    partial class MemoryViewer
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
            this.btnReloadAll = new System.Windows.Forms.Button();
            this.hexEditBox = new Be.Windows.Forms.HexBox();
            this.btnReloadSelection = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.pBar = new System.Windows.Forms.ToolStripProgressBar();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtSelTitle = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.comboSelType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblSelAddr = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnAddSelected = new System.Windows.Forms.Button();
            this.wantedAddresses = new System.Windows.Forms.DataGridView();
            this.ValuesGridAddressColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ValuesGridTitleColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ValuesGridTypeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnRemove = new System.Windows.Forms.Button();
            this.statusStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.wantedAddresses)).BeginInit();
            this.SuspendLayout();
            // 
            // btnReloadAll
            // 
            this.btnReloadAll.Location = new System.Drawing.Point(26, 13);
            this.btnReloadAll.Name = "btnReloadAll";
            this.btnReloadAll.Size = new System.Drawing.Size(75, 23);
            this.btnReloadAll.TabIndex = 0;
            this.btnReloadAll.Text = "Reload all";
            this.btnReloadAll.UseVisualStyleBackColor = true;
            this.btnReloadAll.Click += new System.EventHandler(this.btnReloadAll_Click);
            // 
            // hexEditBox
            // 
            this.hexEditBox.ColumnInfoVisible = true;
            this.hexEditBox.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.hexEditBox.GroupSize = 1;
            this.hexEditBox.LineInfoVisible = true;
            this.hexEditBox.Location = new System.Drawing.Point(26, 42);
            this.hexEditBox.Name = "hexEditBox";
            this.hexEditBox.ReadOnly = true;
            this.hexEditBox.ShadowSelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(60)))), ((int)(((byte)(188)))), ((int)(((byte)(255)))));
            this.hexEditBox.Size = new System.Drawing.Size(774, 414);
            this.hexEditBox.StringViewVisible = true;
            this.hexEditBox.TabIndex = 1;
            this.hexEditBox.UseFixedBytesPerLine = true;
            this.hexEditBox.VScrollBarVisible = true;
            this.hexEditBox.SelectionStartChanged += new System.EventHandler(this.hexEditBox_SelectionStartChanged);
            // 
            // btnReloadSelection
            // 
            this.btnReloadSelection.Location = new System.Drawing.Point(118, 13);
            this.btnReloadSelection.Name = "btnReloadSelection";
            this.btnReloadSelection.Size = new System.Drawing.Size(117, 23);
            this.btnReloadSelection.TabIndex = 2;
            this.btnReloadSelection.Text = "Reload selection";
            this.btnReloadSelection.UseVisualStyleBackColor = true;
            this.btnReloadSelection.Click += new System.EventHandler(this.btnReloadSelection_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus,
            this.pBar});
            this.statusStrip1.Location = new System.Drawing.Point(0, 446);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1151, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblStatus
            // 
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(22, 17);
            this.lblStatus.Text = "---";
            // 
            // pBar
            // 
            this.pBar.AutoSize = false;
            this.pBar.Name = "pBar";
            this.pBar.Size = new System.Drawing.Size(500, 16);
            this.pBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtSelTitle);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.comboSelType);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.lblSelAddr);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnAddSelected);
            this.groupBox1.Location = new System.Drawing.Point(806, 42);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(333, 168);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Take over";
            // 
            // txtSelTitle
            // 
            this.txtSelTitle.Location = new System.Drawing.Point(121, 80);
            this.txtSelTitle.Name = "txtSelTitle";
            this.txtSelTitle.Size = new System.Drawing.Size(122, 20);
            this.txtSelTitle.TabIndex = 12;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(73, 83);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Title:";
            // 
            // comboSelType
            // 
            this.comboSelType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboSelType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboSelType.FormattingEnabled = true;
            this.comboSelType.Location = new System.Drawing.Point(121, 45);
            this.comboSelType.Name = "comboSelType";
            this.comboSelType.Size = new System.Drawing.Size(122, 21);
            this.comboSelType.TabIndex = 10;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(73, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Type:";
            // 
            // lblSelAddr
            // 
            this.lblSelAddr.AutoSize = true;
            this.lblSelAddr.Location = new System.Drawing.Point(121, 20);
            this.lblSelAddr.Name = "lblSelAddr";
            this.lblSelAddr.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblSelAddr.Size = new System.Drawing.Size(31, 13);
            this.lblSelAddr.TabIndex = 7;
            this.lblSelAddr.Text = "none";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Selected address:";
            // 
            // btnAddSelected
            // 
            this.btnAddSelected.Location = new System.Drawing.Point(18, 115);
            this.btnAddSelected.Name = "btnAddSelected";
            this.btnAddSelected.Size = new System.Drawing.Size(225, 23);
            this.btnAddSelected.TabIndex = 5;
            this.btnAddSelected.Text = "Add/edit selected address to list";
            this.btnAddSelected.UseVisualStyleBackColor = true;
            this.btnAddSelected.Click += new System.EventHandler(this.btnAddSelected_Click);
            // 
            // wantedAddresses
            // 
            this.wantedAddresses.AllowUserToAddRows = false;
            this.wantedAddresses.AllowUserToResizeRows = false;
            this.wantedAddresses.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.wantedAddresses.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.wantedAddresses.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ValuesGridAddressColumn,
            this.ValuesGridTitleColumn,
            this.ValuesGridTypeColumn});
            this.wantedAddresses.Cursor = System.Windows.Forms.Cursors.Default;
            this.wantedAddresses.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.wantedAddresses.Location = new System.Drawing.Point(806, 216);
            this.wantedAddresses.Name = "wantedAddresses";
            this.wantedAddresses.ReadOnly = true;
            this.wantedAddresses.RowHeadersVisible = false;
            this.wantedAddresses.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.wantedAddresses.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.wantedAddresses.ShowCellErrors = false;
            this.wantedAddresses.ShowEditingIcon = false;
            this.wantedAddresses.ShowRowErrors = false;
            this.wantedAddresses.Size = new System.Drawing.Size(333, 195);
            this.wantedAddresses.TabIndex = 14;
            // 
            // ValuesGridAddressColumn
            // 
            this.ValuesGridAddressColumn.HeaderText = "Address";
            this.ValuesGridAddressColumn.Name = "ValuesGridAddressColumn";
            this.ValuesGridAddressColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.ValuesGridAddressColumn.Width = 62;
            // 
            // ValuesGridTitleColumn
            // 
            this.ValuesGridTitleColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ValuesGridTitleColumn.HeaderText = "Name";
            this.ValuesGridTitleColumn.Name = "ValuesGridTitleColumn";
            this.ValuesGridTitleColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.ValuesGridTitleColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ValuesGridTypeColumn
            // 
            this.ValuesGridTypeColumn.HeaderText = "Type";
            this.ValuesGridTypeColumn.Name = "ValuesGridTypeColumn";
            this.ValuesGridTypeColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.ValuesGridTypeColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ValuesGridTypeColumn.Width = 63;
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(914, 417);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(225, 23);
            this.btnRemove.TabIndex = 15;
            this.btnRemove.Text = "Remove selected address";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // MemoryViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1151, 468);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.wantedAddresses);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.btnReloadSelection);
            this.Controls.Add(this.hexEditBox);
            this.Controls.Add(this.btnReloadAll);
            this.Name = "MemoryViewer";
            this.Text = "MemoryViewer";
            this.Load += new System.EventHandler(this.MemoryViewer_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.wantedAddresses)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnReloadAll;
        private Be.Windows.Forms.HexBox hexEditBox;
        private System.Windows.Forms.Button btnReloadSelection;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private System.Windows.Forms.ToolStripProgressBar pBar;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblSelAddr;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnAddSelected;
        private System.Windows.Forms.ComboBox comboSelType;
        private System.Windows.Forms.TextBox txtSelTitle;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView wantedAddresses;
        private System.Windows.Forms.DataGridViewTextBoxColumn ValuesGridAddressColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ValuesGridTitleColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ValuesGridTypeColumn;
        private System.Windows.Forms.Button btnRemove;
    }
}