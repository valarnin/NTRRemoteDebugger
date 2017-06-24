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
            this.ComboDataType = new System.Windows.Forms.ComboBox();
            this.SearchValue = new System.Windows.Forms.TextBox();
            this.ResultsGrid = new System.Windows.Forms.DataGridView();
            this.SearchResultsAddressColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SearchResultsValueColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ButtonAddResults = new System.Windows.Forms.Button();
            this.SearchButton = new System.Windows.Forms.Button();
            this.ResetButton = new System.Windows.Forms.Button();
            this.ValuesGrid = new System.Windows.Forms.DataGridView();
            this.ValuesGridLockColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ValuesGridAddressColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ValuesGridTitleColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ValuesGridValueColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ValuesGridTypeColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.ValuesGridContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ValuesGridRefreshItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ValuesGridAddItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ValuesGridDeleteItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ValuesGridConvertCode = new System.Windows.Forms.ToolStripMenuItem();
            this.ValuesGridPointerSearch = new System.Windows.Forms.ToolStripMenuItem();
            this.ValuesGridCopyResolvedAddress = new System.Windows.Forms.ToolStripMenuItem();
            this.ValuesGridShowMemoryView = new System.Windows.Forms.ToolStripMenuItem();
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
            this.ProgressBarMemoryRead = new System.Windows.Forms.ProgressBar();
            this.GUIUpdateTimer = new System.Windows.Forms.Timer(this.components);
            this.LabelCustomRange = new System.Windows.Forms.Label();
            this.MemoryStart = new System.Windows.Forms.TextBox();
            this.MemorySize = new System.Windows.Forms.TextBox();
            this.LabelCurrentOperation = new System.Windows.Forms.Label();
            this.ProgressBarMemoryScan = new System.Windows.Forms.ProgressBar();
            this.LabelLastSearch = new System.Windows.Forms.Label();
            this.LabelDataType = new System.Windows.Forms.Label();
            this.LabelSearchValue = new System.Windows.Forms.Label();
            this.LabelSearchType = new System.Windows.Forms.Label();
            this.ComboSearchType = new System.Windows.Forms.ComboBox();
            this.TextEndAddress = new System.Windows.Forms.TextBox();
            this.LabelEndAddress = new System.Windows.Forms.Label();
            this.SearchValue2 = new System.Windows.Forms.TextBox();
            this.LabelDash = new System.Windows.Forms.Label();
            this.ButtonConfig = new System.Windows.Forms.Button();
            this.LabelButtonState = new System.Windows.Forms.Label();
            this.SaveButton = new System.Windows.Forms.Button();
            this.LoadButton = new System.Windows.Forms.Button();
            this.ImportButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.ResultsGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ValuesGrid)).BeginInit();
            this.ValuesGridContextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // ComboDataType
            // 
            this.ComboDataType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ComboDataType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboDataType.FormattingEnabled = true;
            this.ComboDataType.Location = new System.Drawing.Point(602, 109);
            this.ComboDataType.Name = "ComboDataType";
            this.ComboDataType.Size = new System.Drawing.Size(91, 21);
            this.ComboDataType.TabIndex = 1;
            this.ComboDataType.SelectionChangeCommitted += new System.EventHandler(this.ComboDataType_SelectedValueChanged);
            // 
            // SearchValue
            // 
            this.SearchValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SearchValue.Location = new System.Drawing.Point(486, 134);
            this.SearchValue.Name = "SearchValue";
            this.SearchValue.Size = new System.Drawing.Size(286, 20);
            this.SearchValue.TabIndex = 4;
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
            this.ResultsGrid.Size = new System.Drawing.Size(377, 476);
            this.ResultsGrid.TabIndex = 7;
            // 
            // SearchResultsAddressColumn
            // 
            this.SearchResultsAddressColumn.HeaderText = "Address";
            this.SearchResultsAddressColumn.Name = "SearchResultsAddressColumn";
            this.SearchResultsAddressColumn.ReadOnly = true;
            this.SearchResultsAddressColumn.Width = 62;
            // 
            // SearchResultsValueColumn
            // 
            this.SearchResultsValueColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.SearchResultsValueColumn.HeaderText = "Value on Search";
            this.SearchResultsValueColumn.Name = "SearchResultsValueColumn";
            this.SearchResultsValueColumn.ReadOnly = true;
            // 
            // ButtonAddResults
            // 
            this.ButtonAddResults.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonAddResults.Location = new System.Drawing.Point(272, 10);
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
            this.SearchButton.Location = new System.Drawing.Point(626, 159);
            this.SearchButton.Name = "SearchButton";
            this.SearchButton.Size = new System.Drawing.Size(70, 23);
            this.SearchButton.TabIndex = 9;
            this.SearchButton.Text = "Search";
            this.SearchButton.UseVisualStyleBackColor = true;
            this.SearchButton.Click += new System.EventHandler(this.SearchButton_Click);
            // 
            // ResetButton
            // 
            this.ResetButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ResetButton.Location = new System.Drawing.Point(702, 159);
            this.ResetButton.Name = "ResetButton";
            this.ResetButton.Size = new System.Drawing.Size(70, 23);
            this.ResetButton.TabIndex = 10;
            this.ResetButton.Text = "Reset";
            this.ResetButton.UseVisualStyleBackColor = true;
            this.ResetButton.Click += new System.EventHandler(this.ResetButton_Click);
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
            this.ValuesGridTitleColumn,
            this.ValuesGridValueColumn,
            this.ValuesGridTypeColumn});
            this.ValuesGrid.ContextMenuStrip = this.ValuesGridContextMenuStrip;
            this.ValuesGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.ValuesGrid.Location = new System.Drawing.Point(395, 188);
            this.ValuesGrid.MultiSelect = false;
            this.ValuesGrid.Name = "ValuesGrid";
            this.ValuesGrid.RowHeadersVisible = false;
            this.ValuesGrid.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ValuesGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.ValuesGrid.ShowCellErrors = false;
            this.ValuesGrid.ShowEditingIcon = false;
            this.ValuesGrid.ShowRowErrors = false;
            this.ValuesGrid.Size = new System.Drawing.Size(377, 327);
            this.ValuesGrid.TabIndex = 12;
            this.ValuesGrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ValuesGrid_CellClick);
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
            // ValuesGridValueColumn
            // 
            this.ValuesGridValueColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ValuesGridValueColumn.HeaderText = "Value";
            this.ValuesGridValueColumn.Name = "ValuesGridValueColumn";
            this.ValuesGridValueColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.ValuesGridValueColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ValuesGridTypeColumn
            // 
            this.ValuesGridTypeColumn.HeaderText = "Type";
            this.ValuesGridTypeColumn.Name = "ValuesGridTypeColumn";
            this.ValuesGridTypeColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.ValuesGridTypeColumn.Width = 63;
            // 
            // ValuesGridContextMenuStrip
            // 
            this.ValuesGridContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ValuesGridRefreshItem,
            this.ValuesGridAddItem,
            this.ValuesGridDeleteItem,
            this.ValuesGridConvertCode,
            this.ValuesGridPointerSearch,
            this.ValuesGridCopyResolvedAddress,
            this.ValuesGridShowMemoryView});
            this.ValuesGridContextMenuStrip.Name = "ValuesGridContextMenuStrip";
            this.ValuesGridContextMenuStrip.Size = new System.Drawing.Size(198, 180);
            this.ValuesGridContextMenuStrip.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.ValuesGridContextMenuStrip_ItemClicked);
            // 
            // ValuesGridRefreshItem
            // 
            this.ValuesGridRefreshItem.Name = "ValuesGridRefreshItem";
            this.ValuesGridRefreshItem.Size = new System.Drawing.Size(197, 22);
            this.ValuesGridRefreshItem.Text = "Refresh";
            // 
            // ValuesGridAddItem
            // 
            this.ValuesGridAddItem.Name = "ValuesGridAddItem";
            this.ValuesGridAddItem.Size = new System.Drawing.Size(197, 22);
            this.ValuesGridAddItem.Text = "New";
            // 
            // ValuesGridDeleteItem
            // 
            this.ValuesGridDeleteItem.Name = "ValuesGridDeleteItem";
            this.ValuesGridDeleteItem.Size = new System.Drawing.Size(197, 22);
            this.ValuesGridDeleteItem.Text = "Delete";
            // 
            // ValuesGridConvertCode
            // 
            this.ValuesGridConvertCode.Name = "ValuesGridConvertCode";
            this.ValuesGridConvertCode.Size = new System.Drawing.Size(197, 22);
            this.ValuesGridConvertCode.Text = "Convert AR3DS Code";
            // 
            // ValuesGridPointerSearch
            // 
            this.ValuesGridPointerSearch.Name = "ValuesGridPointerSearch";
            this.ValuesGridPointerSearch.Size = new System.Drawing.Size(197, 22);
            this.ValuesGridPointerSearch.Text = "Pointer Scan";
            // 
            // ValuesGridCopyResolvedAddress
            // 
            this.ValuesGridCopyResolvedAddress.Name = "ValuesGridCopyResolvedAddress";
            this.ValuesGridCopyResolvedAddress.Size = new System.Drawing.Size(197, 22);
            this.ValuesGridCopyResolvedAddress.Text = "Copy Resolved Address";
            // 
            // ValuesGridShowMemoryView
            // 
            this.ValuesGridShowMemoryView.Name = "ValuesGridShowMemoryView";
            this.ValuesGridShowMemoryView.Size = new System.Drawing.Size(197, 22);
            this.ValuesGridShowMemoryView.Text = "Show memory area";
            // 
            // LabelIPPort
            // 
            this.LabelIPPort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LabelIPPort.Location = new System.Drawing.Point(395, 9);
            this.LabelIPPort.Name = "LabelIPPort";
            this.LabelIPPort.Size = new System.Drawing.Size(85, 23);
            this.LabelIPPort.TabIndex = 13;
            this.LabelIPPort.Text = "IP, Port";
            this.LabelIPPort.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // LabelProcess
            // 
            this.LabelProcess.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LabelProcess.Location = new System.Drawing.Point(395, 34);
            this.LabelProcess.Name = "LabelProcess";
            this.LabelProcess.Size = new System.Drawing.Size(85, 23);
            this.LabelProcess.TabIndex = 15;
            this.LabelProcess.Text = "Process";
            this.LabelProcess.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // LabelMemoryRange
            // 
            this.LabelMemoryRange.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LabelMemoryRange.Location = new System.Drawing.Point(395, 59);
            this.LabelMemoryRange.Name = "LabelMemoryRange";
            this.LabelMemoryRange.Size = new System.Drawing.Size(85, 23);
            this.LabelMemoryRange.TabIndex = 16;
            this.LabelMemoryRange.Text = "Memory Range";
            this.LabelMemoryRange.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ButtonConnectDisconnect
            // 
            this.ButtonConnectDisconnect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonConnectDisconnect.Location = new System.Drawing.Point(699, 9);
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
            this.ButtonOpenProcess.Location = new System.Drawing.Point(699, 34);
            this.ButtonOpenProcess.Name = "ButtonOpenProcess";
            this.ButtonOpenProcess.Size = new System.Drawing.Size(73, 23);
            this.ButtonOpenProcess.TabIndex = 18;
            this.ButtonOpenProcess.Text = "Open";
            this.ButtonOpenProcess.UseVisualStyleBackColor = true;
            this.ButtonOpenProcess.Click += new System.EventHandler(this.ButtonOpenProcess_Click);
            // 
            // ButtonRefreshMemoryRange
            // 
            this.ButtonRefreshMemoryRange.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonRefreshMemoryRange.Location = new System.Drawing.Point(699, 59);
            this.ButtonRefreshMemoryRange.Name = "ButtonRefreshMemoryRange";
            this.ButtonRefreshMemoryRange.Size = new System.Drawing.Size(73, 23);
            this.ButtonRefreshMemoryRange.TabIndex = 19;
            this.ButtonRefreshMemoryRange.Text = "Refresh";
            this.ButtonRefreshMemoryRange.UseVisualStyleBackColor = true;
            this.ButtonRefreshMemoryRange.Click += new System.EventHandler(this.ButtonRefreshMemoryRange_Click);
            // 
            // IP
            // 
            this.IP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.IP.Location = new System.Drawing.Point(486, 9);
            this.IP.Name = "IP";
            this.IP.Size = new System.Drawing.Size(151, 20);
            this.IP.TabIndex = 20;
            // 
            // Port
            // 
            this.Port.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Port.Location = new System.Drawing.Point(643, 9);
            this.Port.Name = "Port";
            this.Port.Size = new System.Drawing.Size(50, 20);
            this.Port.TabIndex = 21;
            this.Port.Text = "8000";
            // 
            // Processes
            // 
            this.Processes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Processes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Processes.DropDownWidth = 200;
            this.Processes.FormattingEnabled = true;
            this.Processes.Location = new System.Drawing.Point(486, 34);
            this.Processes.Name = "Processes";
            this.Processes.Size = new System.Drawing.Size(207, 21);
            this.Processes.TabIndex = 22;
            // 
            // MemoryRange
            // 
            this.MemoryRange.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.MemoryRange.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.MemoryRange.FormattingEnabled = true;
            this.MemoryRange.Location = new System.Drawing.Point(486, 59);
            this.MemoryRange.Name = "MemoryRange";
            this.MemoryRange.Size = new System.Drawing.Size(207, 21);
            this.MemoryRange.TabIndex = 23;
            this.MemoryRange.SelectedIndexChanged += new System.EventHandler(this.MemoryRange_SelectedIndexChanged);
            // 
            // ProgressBarStatusStrip
            // 
            this.ProgressBarStatusStrip.AutoSize = false;
            this.ProgressBarStatusStrip.Location = new System.Drawing.Point(0, 518);
            this.ProgressBarStatusStrip.Name = "ProgressBarStatusStrip";
            this.ProgressBarStatusStrip.Size = new System.Drawing.Size(784, 44);
            this.ProgressBarStatusStrip.SizingGrip = false;
            this.ProgressBarStatusStrip.TabIndex = 24;
            // 
            // ProgressBarMemoryRead
            // 
            this.ProgressBarMemoryRead.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ProgressBarMemoryRead.Location = new System.Drawing.Point(10, 525);
            this.ProgressBarMemoryRead.Name = "ProgressBarMemoryRead";
            this.ProgressBarMemoryRead.Size = new System.Drawing.Size(377, 15);
            this.ProgressBarMemoryRead.TabIndex = 25;
            // 
            // GUIUpdateTimer
            // 
            this.GUIUpdateTimer.Enabled = true;
            this.GUIUpdateTimer.Interval = 5;
            this.GUIUpdateTimer.Tick += new System.EventHandler(this.GUIUpdateTimer_Tick);
            // 
            // LabelCustomRange
            // 
            this.LabelCustomRange.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LabelCustomRange.Location = new System.Drawing.Point(395, 84);
            this.LabelCustomRange.Name = "LabelCustomRange";
            this.LabelCustomRange.Size = new System.Drawing.Size(85, 23);
            this.LabelCustomRange.TabIndex = 26;
            this.LabelCustomRange.Text = "Start Addr, Size";
            this.LabelCustomRange.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // MemoryStart
            // 
            this.MemoryStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.MemoryStart.Location = new System.Drawing.Point(486, 84);
            this.MemoryStart.Name = "MemoryStart";
            this.MemoryStart.Size = new System.Drawing.Size(65, 20);
            this.MemoryStart.TabIndex = 27;
            this.MemoryStart.TextChanged += new System.EventHandler(this.Memory_TextChanged);
            // 
            // MemorySize
            // 
            this.MemorySize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.MemorySize.Location = new System.Drawing.Point(557, 84);
            this.MemorySize.Name = "MemorySize";
            this.MemorySize.Size = new System.Drawing.Size(65, 20);
            this.MemorySize.TabIndex = 28;
            this.MemorySize.TextChanged += new System.EventHandler(this.Memory_TextChanged);
            // 
            // LabelCurrentOperation
            // 
            this.LabelCurrentOperation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.LabelCurrentOperation.Location = new System.Drawing.Point(298, 543);
            this.LabelCurrentOperation.Name = "LabelCurrentOperation";
            this.LabelCurrentOperation.Size = new System.Drawing.Size(474, 15);
            this.LabelCurrentOperation.TabIndex = 29;
            // 
            // ProgressBarMemoryScan
            // 
            this.ProgressBarMemoryScan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ProgressBarMemoryScan.Location = new System.Drawing.Point(395, 525);
            this.ProgressBarMemoryScan.Name = "ProgressBarMemoryScan";
            this.ProgressBarMemoryScan.Size = new System.Drawing.Size(377, 15);
            this.ProgressBarMemoryScan.TabIndex = 30;
            // 
            // LabelLastSearch
            // 
            this.LabelLastSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LabelLastSearch.Location = new System.Drawing.Point(12, 9);
            this.LabelLastSearch.Name = "LabelLastSearch";
            this.LabelLastSearch.Size = new System.Drawing.Size(254, 27);
            this.LabelLastSearch.TabIndex = 31;
            this.LabelLastSearch.Text = "Last Search";
            // 
            // LabelDataType
            // 
            this.LabelDataType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LabelDataType.Location = new System.Drawing.Point(699, 109);
            this.LabelDataType.Name = "LabelDataType";
            this.LabelDataType.Size = new System.Drawing.Size(73, 23);
            this.LabelDataType.TabIndex = 32;
            this.LabelDataType.Text = "Data Type";
            this.LabelDataType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // LabelSearchValue
            // 
            this.LabelSearchValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LabelSearchValue.Location = new System.Drawing.Point(395, 134);
            this.LabelSearchValue.Name = "LabelSearchValue";
            this.LabelSearchValue.Size = new System.Drawing.Size(85, 23);
            this.LabelSearchValue.TabIndex = 33;
            this.LabelSearchValue.Text = "Search Value";
            this.LabelSearchValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // LabelSearchType
            // 
            this.LabelSearchType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LabelSearchType.Location = new System.Drawing.Point(395, 109);
            this.LabelSearchType.Name = "LabelSearchType";
            this.LabelSearchType.Size = new System.Drawing.Size(85, 23);
            this.LabelSearchType.TabIndex = 34;
            this.LabelSearchType.Text = "Search Type";
            this.LabelSearchType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ComboSearchType
            // 
            this.ComboSearchType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ComboSearchType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboSearchType.FormattingEnabled = true;
            this.ComboSearchType.Location = new System.Drawing.Point(486, 109);
            this.ComboSearchType.Name = "ComboSearchType";
            this.ComboSearchType.Size = new System.Drawing.Size(110, 21);
            this.ComboSearchType.TabIndex = 35;
            this.ComboSearchType.SelectionChangeCommitted += new System.EventHandler(this.ComboSearchType_SelectedValueChanged);
            // 
            // TextEndAddress
            // 
            this.TextEndAddress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TextEndAddress.Enabled = false;
            this.TextEndAddress.Location = new System.Drawing.Point(628, 84);
            this.TextEndAddress.Name = "TextEndAddress";
            this.TextEndAddress.Size = new System.Drawing.Size(65, 20);
            this.TextEndAddress.TabIndex = 36;
            // 
            // LabelEndAddress
            // 
            this.LabelEndAddress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LabelEndAddress.Location = new System.Drawing.Point(699, 84);
            this.LabelEndAddress.Name = "LabelEndAddress";
            this.LabelEndAddress.Size = new System.Drawing.Size(73, 23);
            this.LabelEndAddress.TabIndex = 37;
            this.LabelEndAddress.Text = "End Address";
            this.LabelEndAddress.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // SearchValue2
            // 
            this.SearchValue2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SearchValue2.Location = new System.Drawing.Point(636, 134);
            this.SearchValue2.Name = "SearchValue2";
            this.SearchValue2.Size = new System.Drawing.Size(136, 20);
            this.SearchValue2.TabIndex = 38;
            this.SearchValue2.Visible = false;
            // 
            // LabelDash
            // 
            this.LabelDash.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LabelDash.Location = new System.Drawing.Point(623, 132);
            this.LabelDash.Name = "LabelDash";
            this.LabelDash.Size = new System.Drawing.Size(11, 23);
            this.LabelDash.TabIndex = 39;
            this.LabelDash.Text = "-";
            this.LabelDash.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.LabelDash.Visible = false;
            // 
            // ButtonConfig
            // 
            this.ButtonConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonConfig.Image = global::NTRDebuggerTool.Properties.Resources.cog2;
            this.ButtonConfig.Location = new System.Drawing.Point(398, 9);
            this.ButtonConfig.Name = "ButtonConfig";
            this.ButtonConfig.Size = new System.Drawing.Size(30, 30);
            this.ButtonConfig.TabIndex = 40;
            this.ButtonConfig.UseVisualStyleBackColor = true;
            this.ButtonConfig.Click += new System.EventHandler(this.ButtonConfig_Click);
            // 
            // LabelButtonState
            // 
            this.LabelButtonState.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.LabelButtonState.Location = new System.Drawing.Point(12, 543);
            this.LabelButtonState.Name = "LabelButtonState";
            this.LabelButtonState.Size = new System.Drawing.Size(280, 15);
            this.LabelButtonState.TabIndex = 41;
            // 
            // SaveButton
            // 
            this.SaveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SaveButton.Location = new System.Drawing.Point(398, 159);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(70, 23);
            this.SaveButton.TabIndex = 42;
            this.SaveButton.Text = "Save";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // LoadButton
            // 
            this.LoadButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LoadButton.Location = new System.Drawing.Point(474, 159);
            this.LoadButton.Name = "LoadButton";
            this.LoadButton.Size = new System.Drawing.Size(70, 23);
            this.LoadButton.TabIndex = 43;
            this.LoadButton.Text = "Load";
            this.LoadButton.UseVisualStyleBackColor = true;
            this.LoadButton.Click += new System.EventHandler(this.LoadButton_Click);
            // 
            // ImportButton
            // 
            this.ImportButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ImportButton.Location = new System.Drawing.Point(550, 159);
            this.ImportButton.Name = "ImportButton";
            this.ImportButton.Size = new System.Drawing.Size(70, 23);
            this.ImportButton.TabIndex = 44;
            this.ImportButton.Text = "Import GS";
            this.ImportButton.UseVisualStyleBackColor = true;
            this.ImportButton.Click += new System.EventHandler(this.ImportButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.ImportButton);
            this.Controls.Add(this.LoadButton);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.LabelButtonState);
            this.Controls.Add(this.ButtonConfig);
            this.Controls.Add(this.LabelEndAddress);
            this.Controls.Add(this.TextEndAddress);
            this.Controls.Add(this.ComboSearchType);
            this.Controls.Add(this.LabelSearchType);
            this.Controls.Add(this.LabelSearchValue);
            this.Controls.Add(this.LabelDataType);
            this.Controls.Add(this.LabelLastSearch);
            this.Controls.Add(this.ProgressBarMemoryScan);
            this.Controls.Add(this.LabelCurrentOperation);
            this.Controls.Add(this.MemorySize);
            this.Controls.Add(this.MemoryStart);
            this.Controls.Add(this.LabelCustomRange);
            this.Controls.Add(this.ProgressBarMemoryRead);
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
            this.Controls.Add(this.ResetButton);
            this.Controls.Add(this.SearchButton);
            this.Controls.Add(this.ButtonAddResults);
            this.Controls.Add(this.ResultsGrid);
            this.Controls.Add(this.SearchValue);
            this.Controls.Add(this.ComboDataType);
            this.Controls.Add(this.LabelDash);
            this.Controls.Add(this.SearchValue2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(800, 99999);
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "MainForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
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

        private System.Windows.Forms.ComboBox ComboDataType;
        internal System.Windows.Forms.TextBox SearchValue;
        internal System.Windows.Forms.DataGridView ResultsGrid;
        private System.Windows.Forms.Button ButtonAddResults;
        private System.Windows.Forms.Button SearchButton;
        private System.Windows.Forms.Button ResetButton;
        private System.Windows.Forms.DataGridView ValuesGrid;
        private System.Windows.Forms.Label LabelIPPort;
        private System.Windows.Forms.Label LabelProcess;
        private System.Windows.Forms.Label LabelMemoryRange;
        internal System.Windows.Forms.Button ButtonConnectDisconnect;
        private System.Windows.Forms.Button ButtonOpenProcess;
        private System.Windows.Forms.Button ButtonRefreshMemoryRange;
        internal System.Windows.Forms.TextBox IP;
        internal System.Windows.Forms.TextBox Port;
        internal System.Windows.Forms.ComboBox Processes;
        private System.Windows.Forms.ComboBox MemoryRange;
        private System.Windows.Forms.StatusStrip ProgressBarStatusStrip;
        private System.Windows.Forms.ProgressBar ProgressBarMemoryRead;
        private System.Windows.Forms.Timer GUIUpdateTimer;
        private System.Windows.Forms.Label LabelCustomRange;
        internal System.Windows.Forms.TextBox MemoryStart;
        internal System.Windows.Forms.TextBox MemorySize;
        private System.Windows.Forms.Label LabelCurrentOperation;
        private System.Windows.Forms.ContextMenuStrip ValuesGridContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem ValuesGridAddItem;
        private System.Windows.Forms.ToolStripMenuItem ValuesGridDeleteItem;
        private System.Windows.Forms.ToolStripMenuItem ValuesGridConvertCode;
        private System.Windows.Forms.ProgressBar ProgressBarMemoryScan;
        private System.Windows.Forms.DataGridViewTextBoxColumn SearchResultsAddressColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn SearchResultsValueColumn;
        private System.Windows.Forms.Label LabelLastSearch;
        private System.Windows.Forms.Label LabelDataType;
        private System.Windows.Forms.Label LabelSearchValue;
        private System.Windows.Forms.Label LabelSearchType;
        private System.Windows.Forms.ComboBox ComboSearchType;
        internal System.Windows.Forms.TextBox TextEndAddress;
        private System.Windows.Forms.Label LabelEndAddress;
        internal System.Windows.Forms.TextBox SearchValue2;
        private System.Windows.Forms.Label LabelDash;
        private System.Windows.Forms.Button ButtonConfig;
        private System.Windows.Forms.Label LabelButtonState;
        private System.Windows.Forms.ToolStripMenuItem ValuesGridRefreshItem;
        private System.Windows.Forms.ToolStripMenuItem ValuesGridPointerSearch;
        private System.Windows.Forms.ToolStripMenuItem ValuesGridCopyResolvedAddress;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.Button LoadButton;
        private System.Windows.Forms.Button ImportButton;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ValuesGridLockColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ValuesGridAddressColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ValuesGridTitleColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ValuesGridValueColumn;
        private System.Windows.Forms.DataGridViewComboBoxColumn ValuesGridTypeColumn;
        private System.Windows.Forms.ToolStripMenuItem ValuesGridShowMemoryView;
    }
}
