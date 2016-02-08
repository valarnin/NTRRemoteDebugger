namespace NTRDebuggerTool.Forms
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.SearchType = new System.Windows.Forms.ComboBox();
            this.LabelSearchType = new System.Windows.Forms.Label();
            this.LabelSearchValue = new System.Windows.Forms.Label();
            this.SearchValue = new System.Windows.Forms.TextBox();
            this.LabelSearchResults = new System.Windows.Forms.Label();
            this.ResultsGrid = new System.Windows.Forms.DataGridView();
            this.SearchResultsAddressColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SearchResultsValueColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ButtonAddResults = new System.Windows.Forms.Button();
            this.SearchButton = new System.Windows.Forms.Button();
            this.ResetButton = new System.Windows.Forms.Button();
            this.LabelResults = new System.Windows.Forms.Label();
            this.ValuesGrid = new System.Windows.Forms.DataGridView();
            this.ValuesGridLockColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ValuesGridAddressColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ValuesGridValueColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ValuesGridTypeColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.ValuesGridContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ValuesGridAddItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ValuesGridDeleteItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ValuesGridConvertCode = new System.Windows.Forms.ToolStripMenuItem();
            this.LabelIPPort = new System.Windows.Forms.Label();
            this.LabelProcess = new System.Windows.Forms.Label();
            this.LabelMemoryRange = new System.Windows.Forms.Label();
            this.ButtonConnectDisconnect = new System.Windows.Forms.Button();
            this.ButtonOpenProcess = new System.Windows.Forms.Button();
            this.ButtonRefreshMemoryRange = new System.Windows.Forms.Button();
            this.IP = new System.Windows.Forms.TextBox();
            this.Port = new System.Windows.Forms.TextBox();
            this.Processes = new System.Windows.Forms.ComboBox();
            this.MemoryRange = new System.Windows.Forms.ComboBox();
            this.ProgressBarStatusStrip = new System.Windows.Forms.StatusStrip();
            this.ProgressBar = new System.Windows.Forms.ProgressBar();
            this.GUIUpdateTimer = new System.Windows.Forms.Timer(this.components);
            this.LabelCustomRange = new System.Windows.Forms.Label();
            this.MemoryStart = new System.Windows.Forms.TextBox();
            this.MemorySize = new System.Windows.Forms.TextBox();
            this.LabelCurrentOperation = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.ResultsGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ValuesGrid)).BeginInit();
            this.ValuesGridContextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // SearchType
            // 
            this.SearchType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SearchType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SearchType.FormattingEnabled = true;
            this.SearchType.Items.AddRange(new object[] {
            "1 Byte",
            "2 Bytes",
            "4 Bytes",
            "8 Bytes",
            "Float",
            "Double",
            "Raw Bytes"});
            this.SearchType.Location = new System.Drawing.Point(313, 110);
            this.SearchType.Name = "SearchType";
            this.SearchType.Size = new System.Drawing.Size(173, 21);
            this.SearchType.TabIndex = 1;
            // 
            // LabelSearchType
            // 
            this.LabelSearchType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LabelSearchType.AutoSize = true;
            this.LabelSearchType.Location = new System.Drawing.Point(250, 115);
            this.LabelSearchType.Name = "LabelSearchType";
            this.LabelSearchType.Size = new System.Drawing.Size(57, 13);
            this.LabelSearchType.TabIndex = 2;
            this.LabelSearchType.Text = "Data Type";
            this.LabelSearchType.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LabelSearchValue
            // 
            this.LabelSearchValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LabelSearchValue.AutoSize = true;
            this.LabelSearchValue.Location = new System.Drawing.Point(236, 140);
            this.LabelSearchValue.Name = "LabelSearchValue";
            this.LabelSearchValue.Size = new System.Drawing.Size(71, 13);
            this.LabelSearchValue.TabIndex = 3;
            this.LabelSearchValue.Text = "Search Value";
            this.LabelSearchValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SearchValue
            // 
            this.SearchValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SearchValue.Location = new System.Drawing.Point(313, 137);
            this.SearchValue.Name = "SearchValue";
            this.SearchValue.Size = new System.Drawing.Size(173, 20);
            this.SearchValue.TabIndex = 4;
            // 
            // LabelSearchResults
            // 
            this.LabelSearchResults.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LabelSearchResults.Location = new System.Drawing.Point(12, 9);
            this.LabelSearchResults.Name = "LabelSearchResults";
            this.LabelSearchResults.Size = new System.Drawing.Size(95, 27);
            this.LabelSearchResults.TabIndex = 5;
            this.LabelSearchResults.Text = "Search Results";
            this.LabelSearchResults.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ResultsGrid
            // 
            this.ResultsGrid.AllowUserToAddRows = false;
            this.ResultsGrid.AllowUserToDeleteRows = false;
            this.ResultsGrid.AllowUserToResizeRows = false;
            this.ResultsGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ResultsGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ResultsGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SearchResultsAddressColumn,
            this.SearchResultsValueColumn});
            this.ResultsGrid.Location = new System.Drawing.Point(12, 39);
            this.ResultsGrid.Name = "ResultsGrid";
            this.ResultsGrid.ReadOnly = true;
            this.ResultsGrid.RowHeadersVisible = false;
            this.ResultsGrid.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ResultsGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ResultsGrid.ShowCellErrors = false;
            this.ResultsGrid.ShowCellToolTips = false;
            this.ResultsGrid.ShowEditingIcon = false;
            this.ResultsGrid.ShowRowErrors = false;
            this.ResultsGrid.Size = new System.Drawing.Size(218, 270);
            this.ResultsGrid.TabIndex = 7;
            // 
            // SearchResultsAddressColumn
            // 
            this.SearchResultsAddressColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.SearchResultsAddressColumn.FillWeight = 600F;
            this.SearchResultsAddressColumn.HeaderText = "Address";
            this.SearchResultsAddressColumn.Name = "SearchResultsAddressColumn";
            this.SearchResultsAddressColumn.ReadOnly = true;
            // 
            // SearchResultsValueColumn
            // 
            this.SearchResultsValueColumn.HeaderText = "Value";
            this.SearchResultsValueColumn.Name = "SearchResultsValueColumn";
            this.SearchResultsValueColumn.ReadOnly = true;
            this.SearchResultsValueColumn.Width = 50;
            // 
            // ButtonAddResults
            // 
            this.ButtonAddResults.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonAddResults.Location = new System.Drawing.Point(113, 12);
            this.ButtonAddResults.Name = "ButtonAddResults";
            this.ButtonAddResults.Size = new System.Drawing.Size(117, 23);
            this.ButtonAddResults.TabIndex = 8;
            this.ButtonAddResults.Text = "Add Selected Results";
            this.ButtonAddResults.UseVisualStyleBackColor = true;
            this.ButtonAddResults.Click += new System.EventHandler(this.ButtonAddResults_Click);
            // 
            // SearchButton
            // 
            this.SearchButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SearchButton.Location = new System.Drawing.Point(239, 163);
            this.SearchButton.Name = "SearchButton";
            this.SearchButton.Size = new System.Drawing.Size(75, 23);
            this.SearchButton.TabIndex = 9;
            this.SearchButton.Text = "Search";
            this.SearchButton.UseVisualStyleBackColor = true;
            this.SearchButton.Click += new System.EventHandler(this.SearchButton_Click);
            // 
            // ResetButton
            // 
            this.ResetButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ResetButton.Location = new System.Drawing.Point(411, 163);
            this.ResetButton.Name = "ResetButton";
            this.ResetButton.Size = new System.Drawing.Size(75, 23);
            this.ResetButton.TabIndex = 10;
            this.ResetButton.Text = "Reset";
            this.ResetButton.UseVisualStyleBackColor = true;
            this.ResetButton.Click += new System.EventHandler(this.ResetButton_Click);
            // 
            // LabelResults
            // 
            this.LabelResults.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LabelResults.Location = new System.Drawing.Point(320, 163);
            this.LabelResults.Name = "LabelResults";
            this.LabelResults.Size = new System.Drawing.Size(85, 23);
            this.LabelResults.TabIndex = 11;
            this.LabelResults.Text = "Results:";
            this.LabelResults.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ValuesGrid
            // 
            this.ValuesGrid.AllowUserToAddRows = false;
            this.ValuesGrid.AllowUserToResizeRows = false;
            this.ValuesGrid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ValuesGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ValuesGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ValuesGridLockColumn,
            this.ValuesGridAddressColumn,
            this.ValuesGridValueColumn,
            this.ValuesGridTypeColumn});
            this.ValuesGrid.ContextMenuStrip = this.ValuesGridContextMenuStrip;
            this.ValuesGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.ValuesGrid.Location = new System.Drawing.Point(239, 192);
            this.ValuesGrid.MultiSelect = false;
            this.ValuesGrid.Name = "ValuesGrid";
            this.ValuesGrid.RowHeadersVisible = false;
            this.ValuesGrid.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ValuesGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.ValuesGrid.ShowCellErrors = false;
            this.ValuesGrid.ShowCellToolTips = false;
            this.ValuesGrid.ShowEditingIcon = false;
            this.ValuesGrid.ShowRowErrors = false;
            this.ValuesGrid.Size = new System.Drawing.Size(247, 117);
            this.ValuesGrid.TabIndex = 12;
            this.ValuesGrid.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ValuesGrid_CellDoubleClick);
            this.ValuesGrid.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.ValuesGrid_CellLeave);
            this.ValuesGrid.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.ValuesGrid_CellValueChanged);
            // 
            // ValuesGridLockColumn
            // 
            this.ValuesGridLockColumn.HeaderText = "Lock";
            this.ValuesGridLockColumn.Name = "ValuesGridLockColumn";
            this.ValuesGridLockColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.ValuesGridLockColumn.TrueValue = "T";
            this.ValuesGridLockColumn.Width = 35;
            // 
            // ValuesGridAddressColumn
            // 
            this.ValuesGridAddressColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ValuesGridAddressColumn.FillWeight = 600F;
            this.ValuesGridAddressColumn.HeaderText = "Address";
            this.ValuesGridAddressColumn.Name = "ValuesGridAddressColumn";
            // 
            // ValuesGridValueColumn
            // 
            this.ValuesGridValueColumn.HeaderText = "Value";
            this.ValuesGridValueColumn.Name = "ValuesGridValueColumn";
            this.ValuesGridValueColumn.Width = 50;
            // 
            // ValuesGridTypeColumn
            // 
            this.ValuesGridTypeColumn.HeaderText = "Type";
            this.ValuesGridTypeColumn.Items.AddRange(new object[] {
            "1 Byte",
            "2 Bytes",
            "4 Bytes",
            "8 Bytes",
            "Float",
            "Double",
            "Raw Bytes"});
            this.ValuesGridTypeColumn.Name = "ValuesGridTypeColumn";
            this.ValuesGridTypeColumn.Width = 40;
            // 
            // ValuesGridContextMenuStrip
            // 
            this.ValuesGridContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ValuesGridAddItem,
            this.ValuesGridDeleteItem,
            this.ValuesGridConvertCode});
            this.ValuesGridContextMenuStrip.Name = "ValuesGridContextMenuStrip";
            this.ValuesGridContextMenuStrip.Size = new System.Drawing.Size(186, 70);
            this.ValuesGridContextMenuStrip.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.ValuesGridContextMenuStrip_ItemClicked);
            // 
            // ValuesGridAddItem
            // 
            this.ValuesGridAddItem.Name = "ValuesGridAddItem";
            this.ValuesGridAddItem.Size = new System.Drawing.Size(185, 22);
            this.ValuesGridAddItem.Text = "New";
            // 
            // ValuesGridDeleteItem
            // 
            this.ValuesGridDeleteItem.Name = "ValuesGridDeleteItem";
            this.ValuesGridDeleteItem.Size = new System.Drawing.Size(185, 22);
            this.ValuesGridDeleteItem.Text = "Delete";
            // 
            // ValuesGridConvertCode
            // 
            this.ValuesGridConvertCode.Name = "ValuesGridConvertCode";
            this.ValuesGridConvertCode.Size = new System.Drawing.Size(185, 22);
            this.ValuesGridConvertCode.Text = "Convert AR3DS Code";
            // 
            // LabelIPPort
            // 
            this.LabelIPPort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LabelIPPort.Location = new System.Drawing.Point(236, 9);
            this.LabelIPPort.Name = "LabelIPPort";
            this.LabelIPPort.Size = new System.Drawing.Size(48, 23);
            this.LabelIPPort.TabIndex = 13;
            this.LabelIPPort.Text = "IP, Port";
            this.LabelIPPort.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // LabelProcess
            // 
            this.LabelProcess.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LabelProcess.Location = new System.Drawing.Point(236, 32);
            this.LabelProcess.Name = "LabelProcess";
            this.LabelProcess.Size = new System.Drawing.Size(48, 23);
            this.LabelProcess.TabIndex = 15;
            this.LabelProcess.Text = "Process";
            this.LabelProcess.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // LabelMemoryRange
            // 
            this.LabelMemoryRange.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LabelMemoryRange.Location = new System.Drawing.Point(236, 55);
            this.LabelMemoryRange.Name = "LabelMemoryRange";
            this.LabelMemoryRange.Size = new System.Drawing.Size(48, 23);
            this.LabelMemoryRange.TabIndex = 16;
            this.LabelMemoryRange.Text = "Range";
            this.LabelMemoryRange.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ButtonConnectDisconnect
            // 
            this.ButtonConnectDisconnect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonConnectDisconnect.Location = new System.Drawing.Point(413, 9);
            this.ButtonConnectDisconnect.Name = "ButtonConnectDisconnect";
            this.ButtonConnectDisconnect.Size = new System.Drawing.Size(73, 23);
            this.ButtonConnectDisconnect.TabIndex = 17;
            this.ButtonConnectDisconnect.Text = "Connect";
            this.ButtonConnectDisconnect.UseVisualStyleBackColor = true;
            this.ButtonConnectDisconnect.Click += new System.EventHandler(this.ButtonConnectDisconnect_Click);
            // 
            // ButtonOpenProcess
            // 
            this.ButtonOpenProcess.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonOpenProcess.Location = new System.Drawing.Point(418, 32);
            this.ButtonOpenProcess.Name = "ButtonOpenProcess";
            this.ButtonOpenProcess.Size = new System.Drawing.Size(68, 23);
            this.ButtonOpenProcess.TabIndex = 18;
            this.ButtonOpenProcess.Text = "Open";
            this.ButtonOpenProcess.UseVisualStyleBackColor = true;
            this.ButtonOpenProcess.Click += new System.EventHandler(this.ButtonOpenProcess_Click);
            // 
            // ButtonRefreshMemoryRange
            // 
            this.ButtonRefreshMemoryRange.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonRefreshMemoryRange.Location = new System.Drawing.Point(418, 55);
            this.ButtonRefreshMemoryRange.Name = "ButtonRefreshMemoryRange";
            this.ButtonRefreshMemoryRange.Size = new System.Drawing.Size(68, 23);
            this.ButtonRefreshMemoryRange.TabIndex = 19;
            this.ButtonRefreshMemoryRange.Text = "Refresh";
            this.ButtonRefreshMemoryRange.UseVisualStyleBackColor = true;
            this.ButtonRefreshMemoryRange.Click += new System.EventHandler(this.ButtonRefreshMemoryRange_Click);
            // 
            // IP
            // 
            this.IP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.IP.Location = new System.Drawing.Point(290, 11);
            this.IP.Name = "IP";
            this.IP.Size = new System.Drawing.Size(75, 20);
            this.IP.TabIndex = 20;
            // 
            // Port
            // 
            this.Port.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Port.Location = new System.Drawing.Point(366, 11);
            this.Port.Name = "Port";
            this.Port.Size = new System.Drawing.Size(41, 20);
            this.Port.TabIndex = 21;
            this.Port.Text = "8000";
            // 
            // Processes
            // 
            this.Processes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Processes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Processes.DropDownWidth = 200;
            this.Processes.FormattingEnabled = true;
            this.Processes.Location = new System.Drawing.Point(290, 34);
            this.Processes.Name = "Processes";
            this.Processes.Size = new System.Drawing.Size(122, 21);
            this.Processes.TabIndex = 22;
            // 
            // MemoryRange
            // 
            this.MemoryRange.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.MemoryRange.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.MemoryRange.FormattingEnabled = true;
            this.MemoryRange.Location = new System.Drawing.Point(290, 57);
            this.MemoryRange.Name = "MemoryRange";
            this.MemoryRange.Size = new System.Drawing.Size(122, 21);
            this.MemoryRange.TabIndex = 23;
            this.MemoryRange.SelectedIndexChanged += new System.EventHandler(this.MemoryRange_SelectedIndexChanged);
            // 
            // ProgressBarStatusStrip
            // 
            this.ProgressBarStatusStrip.Location = new System.Drawing.Point(0, 312);
            this.ProgressBarStatusStrip.Name = "ProgressBarStatusStrip";
            this.ProgressBarStatusStrip.Size = new System.Drawing.Size(498, 22);
            this.ProgressBarStatusStrip.SizingGrip = false;
            this.ProgressBarStatusStrip.TabIndex = 24;
            // 
            // ProgressBar
            // 
            this.ProgressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ProgressBar.Location = new System.Drawing.Point(12, 315);
            this.ProgressBar.Name = "ProgressBar";
            this.ProgressBar.Size = new System.Drawing.Size(305, 15);
            this.ProgressBar.TabIndex = 25;
            // 
            // GUIUpdateTimer
            // 
            this.GUIUpdateTimer.Enabled = true;
            this.GUIUpdateTimer.Tick += new System.EventHandler(this.GUIUpdateTimer_Tick);
            // 
            // LabelCustomRange
            // 
            this.LabelCustomRange.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LabelCustomRange.Location = new System.Drawing.Point(236, 82);
            this.LabelCustomRange.Name = "LabelCustomRange";
            this.LabelCustomRange.Size = new System.Drawing.Size(56, 23);
            this.LabelCustomRange.TabIndex = 26;
            this.LabelCustomRange.Text = "Start, Size";
            this.LabelCustomRange.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // MemoryStart
            // 
            this.MemoryStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.MemoryStart.Location = new System.Drawing.Point(298, 84);
            this.MemoryStart.Name = "MemoryStart";
            this.MemoryStart.Size = new System.Drawing.Size(90, 20);
            this.MemoryStart.TabIndex = 27;
            // 
            // MemorySize
            // 
            this.MemorySize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.MemorySize.Location = new System.Drawing.Point(394, 84);
            this.MemorySize.Name = "MemorySize";
            this.MemorySize.Size = new System.Drawing.Size(90, 20);
            this.MemorySize.TabIndex = 28;
            // 
            // LabelCurrentOperation
            // 
            this.LabelCurrentOperation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.LabelCurrentOperation.Location = new System.Drawing.Point(323, 315);
            this.LabelCurrentOperation.Name = "LabelCurrentOperation";
            this.LabelCurrentOperation.Size = new System.Drawing.Size(163, 15);
            this.LabelCurrentOperation.TabIndex = 29;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(498, 334);
            this.Controls.Add(this.LabelCurrentOperation);
            this.Controls.Add(this.MemorySize);
            this.Controls.Add(this.MemoryStart);
            this.Controls.Add(this.LabelCustomRange);
            this.Controls.Add(this.ProgressBar);
            this.Controls.Add(this.ProgressBarStatusStrip);
            this.Controls.Add(this.MemoryRange);
            this.Controls.Add(this.Processes);
            this.Controls.Add(this.Port);
            this.Controls.Add(this.IP);
            this.Controls.Add(this.ButtonRefreshMemoryRange);
            this.Controls.Add(this.ButtonOpenProcess);
            this.Controls.Add(this.ButtonConnectDisconnect);
            this.Controls.Add(this.LabelMemoryRange);
            this.Controls.Add(this.LabelProcess);
            this.Controls.Add(this.LabelIPPort);
            this.Controls.Add(this.ValuesGrid);
            this.Controls.Add(this.LabelResults);
            this.Controls.Add(this.ResetButton);
            this.Controls.Add(this.SearchButton);
            this.Controls.Add(this.ButtonAddResults);
            this.Controls.Add(this.ResultsGrid);
            this.Controls.Add(this.LabelSearchResults);
            this.Controls.Add(this.SearchValue);
            this.Controls.Add(this.LabelSearchValue);
            this.Controls.Add(this.LabelSearchType);
            this.Controls.Add(this.SearchType);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(514, 372);
            this.Name = "MainForm";
            this.Text = "NTR Cheat Tool";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ResultsGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ValuesGrid)).EndInit();
            this.ValuesGridContextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox SearchType;
        private System.Windows.Forms.Label LabelSearchType;
        private System.Windows.Forms.Label LabelSearchValue;
        internal System.Windows.Forms.TextBox SearchValue;
        private System.Windows.Forms.Label LabelSearchResults;
        internal System.Windows.Forms.DataGridView ResultsGrid;
        private System.Windows.Forms.Button ButtonAddResults;
        private System.Windows.Forms.Button SearchButton;
        private System.Windows.Forms.Button ResetButton;
        private System.Windows.Forms.Label LabelResults;
        private System.Windows.Forms.DataGridView ValuesGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn SearchResultsAddressColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn SearchResultsValueColumn;
        private System.Windows.Forms.Label LabelIPPort;
        private System.Windows.Forms.Label LabelProcess;
        private System.Windows.Forms.Label LabelMemoryRange;
        internal System.Windows.Forms.Button ButtonConnectDisconnect;
        private System.Windows.Forms.Button ButtonOpenProcess;
        private System.Windows.Forms.Button ButtonRefreshMemoryRange;
        internal System.Windows.Forms.TextBox IP;
        internal System.Windows.Forms.TextBox Port;
        private System.Windows.Forms.ComboBox Processes;
        private System.Windows.Forms.ComboBox MemoryRange;
        private System.Windows.Forms.StatusStrip ProgressBarStatusStrip;
        private System.Windows.Forms.ProgressBar ProgressBar;
        private System.Windows.Forms.Timer GUIUpdateTimer;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ValuesGridLockColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ValuesGridAddressColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ValuesGridValueColumn;
        private System.Windows.Forms.DataGridViewComboBoxColumn ValuesGridTypeColumn;
        private System.Windows.Forms.Label LabelCustomRange;
        internal System.Windows.Forms.TextBox MemoryStart;
        internal System.Windows.Forms.TextBox MemorySize;
        private System.Windows.Forms.Label LabelCurrentOperation;
        private System.Windows.Forms.ContextMenuStrip ValuesGridContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem ValuesGridAddItem;
        private System.Windows.Forms.ToolStripMenuItem ValuesGridDeleteItem;
        private System.Windows.Forms.ToolStripMenuItem ValuesGridConvertCode;
    }
}
