namespace Gulliver
{
    partial class flcsLibrary
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(flcsLibrary));
            this.kryptonManager = new ComponentFactory.Krypton.Toolkit.KryptonManager(this.components);
            this.kryptonPanel = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.statusstrip = new System.Windows.Forms.StatusStrip();
            this.fiterStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.showAllLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.libraryProgressbar = new System.Windows.Forms.ToolStripProgressBar();
            this.kryptonPanel1 = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.dataGridViewLibrary = new ComponentFactory.Krypton.Toolkit.KryptonDataGridView();
            this.deleteDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewLinkColumn();
            this.viewDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewLinkColumn();
            this.idDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.supplierDataGridViewTextBoxColumn = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.offerNameDataGridViewTextBoxColumn = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.dealDirectoryNameDataGridViewTextBoxColumn = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.onSalesDataGridViewTextBoxColumn = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.expiredDateDataGridViewTextBoxColumn = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.copyDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewLinkColumn();
            this.bsLibrary = new System.Windows.Forms.BindingSource(this.components);
            this.libraryDS = new Gulliver.LibraryDS();
            this.kryptonHeader1 = new ComponentFactory.Krypton.Toolkit.KryptonHeader();
            this.kryptonLabel2 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.headerI = new ComponentFactory.Krypton.Toolkit.KryptonHeader();
            this.buttonSpecAny1 = new ComponentFactory.Krypton.Toolkit.ButtonSpecAny();
            this.cmbSuppliers = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.kryptonLabel1 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.cbShowAll = new ComponentFactory.Krypton.Toolkit.KryptonCheckBox();
            this.txtSearch = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newOfferToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.healthSafetyDocumentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fleetwayWebUpdatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updateBestDealPageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updateGermanBestDealPageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updateLHCDealToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updateELBDealsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataGridViewAutoFilterTextBoxColumn1 = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.dataGridViewAutoFilterTextBoxColumn2 = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.dataGridViewAutoFilterTextBoxColumn3 = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.dataGridViewAutoFilterTextBoxColumn4 = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.dataGridViewAutoFilterTextBoxColumn5 = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.dataGridViewLinkColumn1 = new System.Windows.Forms.DataGridViewLinkColumn();
            this.dataGridViewLinkColumn2 = new System.Windows.Forms.DataGridViewLinkColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewAutoFilterTextBoxColumn6 = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.dataGridViewAutoFilterTextBoxColumn7 = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.dataGridViewAutoFilterTextBoxColumn8 = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.dataGridViewAutoFilterTextBoxColumn9 = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.dataGridViewAutoFilterTextBoxColumn10 = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.dataGridViewLinkColumn3 = new System.Windows.Forms.DataGridViewLinkColumn();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel)).BeginInit();
            this.kryptonPanel.SuspendLayout();
            this.statusstrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewLibrary)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsLibrary)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.libraryDS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSuppliers)).BeginInit();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // kryptonPanel
            // 
            this.kryptonPanel.Controls.Add(this.statusstrip);
            this.kryptonPanel.Controls.Add(this.kryptonPanel1);
            this.kryptonPanel.Controls.Add(this.menuStrip);
            this.kryptonPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel.Name = "kryptonPanel";
            this.kryptonPanel.Size = new System.Drawing.Size(1254, 710);
            this.kryptonPanel.TabIndex = 0;
            // 
            // statusstrip
            // 
            this.statusstrip.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.statusstrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fiterStatusLabel,
            this.showAllLabel,
            this.libraryProgressbar});
            this.statusstrip.Location = new System.Drawing.Point(0, 688);
            this.statusstrip.Name = "statusstrip";
            this.statusstrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.ManagerRenderMode;
            this.statusstrip.Size = new System.Drawing.Size(1254, 22);
            this.statusstrip.TabIndex = 23;
            // 
            // fiterStatusLabel
            // 
            this.fiterStatusLabel.Name = "fiterStatusLabel";
            this.fiterStatusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // showAllLabel
            // 
            this.showAllLabel.IsLink = true;
            this.showAllLabel.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.showAllLabel.Name = "showAllLabel";
            this.showAllLabel.Size = new System.Drawing.Size(53, 17);
            this.showAllLabel.Text = "&Show All";
            // 
            // libraryProgressbar
            // 
            this.libraryProgressbar.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.libraryProgressbar.Name = "libraryProgressbar";
            this.libraryProgressbar.Size = new System.Drawing.Size(100, 16);
            this.libraryProgressbar.Step = 1;
            this.libraryProgressbar.Visible = false;
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.dataGridViewLibrary);
            this.kryptonPanel1.Controls.Add(this.kryptonHeader1);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel2);
            this.kryptonPanel1.Controls.Add(this.headerI);
            this.kryptonPanel1.Controls.Add(this.cmbSuppliers);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel1);
            this.kryptonPanel1.Controls.Add(this.cbShowAll);
            this.kryptonPanel1.Controls.Add(this.txtSearch);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 24);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.PanelBackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.PanelAlternate;
            this.kryptonPanel1.Size = new System.Drawing.Size(1254, 686);
            this.kryptonPanel1.TabIndex = 22;
            // 
            // dataGridViewLibrary
            // 
            this.dataGridViewLibrary.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewLibrary.AutoGenerateColumns = false;
            this.dataGridViewLibrary.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewLibrary.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.deleteDataGridViewTextBoxColumn,
            this.viewDataGridViewTextBoxColumn,
            this.idDataGridViewTextBoxColumn,
            this.supplierDataGridViewTextBoxColumn,
            this.offerNameDataGridViewTextBoxColumn,
            this.dealDirectoryNameDataGridViewTextBoxColumn,
            this.onSalesDataGridViewTextBoxColumn,
            this.expiredDateDataGridViewTextBoxColumn,
            this.copyDataGridViewTextBoxColumn});
            this.dataGridViewLibrary.DataSource = this.bsLibrary;
            this.dataGridViewLibrary.Location = new System.Drawing.Point(3, 146);
            this.dataGridViewLibrary.Name = "dataGridViewLibrary";
            this.dataGridViewLibrary.Size = new System.Drawing.Size(1248, 515);
            this.dataGridViewLibrary.TabIndex = 25;
            this.dataGridViewLibrary.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewLibrary_CellContentClick);
            // 
            // deleteDataGridViewTextBoxColumn
            // 
            this.deleteDataGridViewTextBoxColumn.DataPropertyName = "Delete";
            this.deleteDataGridViewTextBoxColumn.HeaderText = "Delete";
            this.deleteDataGridViewTextBoxColumn.Name = "deleteDataGridViewTextBoxColumn";
            this.deleteDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.deleteDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.deleteDataGridViewTextBoxColumn.Width = 80;
            // 
            // viewDataGridViewTextBoxColumn
            // 
            this.viewDataGridViewTextBoxColumn.DataPropertyName = "View";
            this.viewDataGridViewTextBoxColumn.HeaderText = "View";
            this.viewDataGridViewTextBoxColumn.Name = "viewDataGridViewTextBoxColumn";
            this.viewDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.viewDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.viewDataGridViewTextBoxColumn.Width = 80;
            // 
            // idDataGridViewTextBoxColumn
            // 
            this.idDataGridViewTextBoxColumn.DataPropertyName = "id";
            this.idDataGridViewTextBoxColumn.HeaderText = "id";
            this.idDataGridViewTextBoxColumn.Name = "idDataGridViewTextBoxColumn";
            this.idDataGridViewTextBoxColumn.Visible = false;
            // 
            // supplierDataGridViewTextBoxColumn
            // 
            this.supplierDataGridViewTextBoxColumn.DataPropertyName = "Supplier";
            this.supplierDataGridViewTextBoxColumn.HeaderText = "Media";
            this.supplierDataGridViewTextBoxColumn.Name = "supplierDataGridViewTextBoxColumn";
            this.supplierDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.supplierDataGridViewTextBoxColumn.Width = 200;
            // 
            // offerNameDataGridViewTextBoxColumn
            // 
            this.offerNameDataGridViewTextBoxColumn.DataPropertyName = "Offer Name";
            this.offerNameDataGridViewTextBoxColumn.HeaderText = "Deal Name";
            this.offerNameDataGridViewTextBoxColumn.Name = "offerNameDataGridViewTextBoxColumn";
            this.offerNameDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.offerNameDataGridViewTextBoxColumn.Width = 450;
            // 
            // dealDirectoryNameDataGridViewTextBoxColumn
            // 
            this.dealDirectoryNameDataGridViewTextBoxColumn.DataPropertyName = "Deal Directory Name";
            this.dealDirectoryNameDataGridViewTextBoxColumn.HeaderText = "Deal Directory Name";
            this.dealDirectoryNameDataGridViewTextBoxColumn.Name = "dealDirectoryNameDataGridViewTextBoxColumn";
            this.dealDirectoryNameDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dealDirectoryNameDataGridViewTextBoxColumn.Visible = false;
            this.dealDirectoryNameDataGridViewTextBoxColumn.Width = 250;
            // 
            // onSalesDataGridViewTextBoxColumn
            // 
            this.onSalesDataGridViewTextBoxColumn.DataPropertyName = "On Sales";
            this.onSalesDataGridViewTextBoxColumn.HeaderText = "On Sales";
            this.onSalesDataGridViewTextBoxColumn.Name = "onSalesDataGridViewTextBoxColumn";
            this.onSalesDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.onSalesDataGridViewTextBoxColumn.Width = 150;
            // 
            // expiredDateDataGridViewTextBoxColumn
            // 
            this.expiredDateDataGridViewTextBoxColumn.DataPropertyName = "Expired Date";
            this.expiredDateDataGridViewTextBoxColumn.HeaderText = "Expired Date";
            this.expiredDateDataGridViewTextBoxColumn.Name = "expiredDateDataGridViewTextBoxColumn";
            this.expiredDateDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // copyDataGridViewTextBoxColumn
            // 
            this.copyDataGridViewTextBoxColumn.DataPropertyName = "Copy";
            this.copyDataGridViewTextBoxColumn.HeaderText = "Copy";
            this.copyDataGridViewTextBoxColumn.Name = "copyDataGridViewTextBoxColumn";
            this.copyDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.copyDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.copyDataGridViewTextBoxColumn.Width = 80;
            // 
            // bsLibrary
            // 
            this.bsLibrary.DataMember = "Library";
            this.bsLibrary.DataSource = this.libraryDS;
            // 
            // libraryDS
            // 
            this.libraryDS.DataSetName = "LibraryDS";
            this.libraryDS.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // kryptonHeader1
            // 
            this.kryptonHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.kryptonHeader1.Location = new System.Drawing.Point(0, 60);
            this.kryptonHeader1.Name = "kryptonHeader1";
            this.kryptonHeader1.Size = new System.Drawing.Size(1254, 31);
            this.kryptonHeader1.TabIndex = 24;
            this.kryptonHeader1.Values.Description = "";
            this.kryptonHeader1.Values.Heading = "Find ";
            this.kryptonHeader1.Values.Image = ((System.Drawing.Image)(resources.GetObject("kryptonHeader1.Values.Image")));
            // 
            // kryptonLabel2
            // 
            this.kryptonLabel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.kryptonLabel2.Location = new System.Drawing.Point(0, 31);
            this.kryptonLabel2.Name = "kryptonLabel2";
            this.kryptonLabel2.Size = new System.Drawing.Size(1254, 29);
            this.kryptonLabel2.StateCommon.Padding = new System.Windows.Forms.Padding(-1, 10, -1, -1);
            this.kryptonLabel2.TabIndex = 23;
            this.kryptonLabel2.Values.Text = "Here is the list of secret escape  offers. Select offer to delete or view ";
            // 
            // headerI
            // 
            this.headerI.ButtonSpecs.AddRange(new ComponentFactory.Krypton.Toolkit.ButtonSpecAny[] {
            this.buttonSpecAny1});
            this.headerI.Dock = System.Windows.Forms.DockStyle.Top;
            this.headerI.Location = new System.Drawing.Point(0, 0);
            this.headerI.Name = "headerI";
            this.headerI.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2010Blue;
            this.headerI.Size = new System.Drawing.Size(1254, 31);
            this.headerI.TabIndex = 21;
            this.headerI.Values.Description = "";
            this.headerI.Values.Heading = "Library";
            this.headerI.Values.Image = null;
            // 
            // buttonSpecAny1
            // 
            this.buttonSpecAny1.UniqueName = "CAA5709467C5470791A9895607BB675A";
            // 
            // cmbSuppliers
            // 
            this.cmbSuppliers.DropDownWidth = 257;
            this.cmbSuppliers.Location = new System.Drawing.Point(87, 104);
            this.cmbSuppliers.Name = "cmbSuppliers";
            this.cmbSuppliers.Size = new System.Drawing.Size(294, 21);
            this.cmbSuppliers.TabIndex = 4;
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.kryptonLabel1.Location = new System.Drawing.Point(23, 105);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(47, 20);
            this.kryptonLabel1.TabIndex = 3;
            this.kryptonLabel1.Values.Text = "Media:";
            // 
            // cbShowAll
            // 
            this.cbShowAll.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbShowAll.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.NormalControl;
            this.cbShowAll.Location = new System.Drawing.Point(746, 105);
            this.cbShowAll.Name = "cbShowAll";
            this.cbShowAll.Size = new System.Drawing.Size(199, 20);
            this.cbShowAll.TabIndex = 2;
            this.cbShowAll.Text = "Show All (include expired offers)";
            this.cbShowAll.Values.Text = "Show All (include expired offers)";
            this.cbShowAll.CheckedChanged += new System.EventHandler(this.cbShowAll_CheckedChanged);
            // 
            // txtSearch
            // 
            this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearch.Location = new System.Drawing.Point(400, 105);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(340, 20);
            this.txtSearch.TabIndex = 1;
            // 
            // menuStrip
            // 
            this.menuStrip.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.optionsToolStripMenuItem,
            this.fleetwayWebUpdatesToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(1254, 24);
            this.menuStrip.TabIndex = 24;
            this.menuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newOfferToolStripMenuItem,
            this.toolStripSeparator1});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(41, 20);
            this.fileToolStripMenuItem.Text = "&Add";
            // 
            // newOfferToolStripMenuItem
            // 
            this.newOfferToolStripMenuItem.Name = "newOfferToolStripMenuItem";
            this.newOfferToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.newOfferToolStripMenuItem.Text = "&New Offer";
            this.newOfferToolStripMenuItem.Click += new System.EventHandler(this.newOfferToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.healthSafetyDocumentToolStripMenuItem});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(74, 20);
            this.optionsToolStripMenuItem.Text = "&Templates";
            // 
            // healthSafetyDocumentToolStripMenuItem
            // 
            this.healthSafetyDocumentToolStripMenuItem.Name = "healthSafetyDocumentToolStripMenuItem";
            this.healthSafetyDocumentToolStripMenuItem.Size = new System.Drawing.Size(216, 22);
            this.healthSafetyDocumentToolStripMenuItem.Text = "Health && Safety Document";
            this.healthSafetyDocumentToolStripMenuItem.Click += new System.EventHandler(this.healthSafetyDocumentToolStripMenuItem_Click);
            // 
            // fleetwayWebUpdatesToolStripMenuItem
            // 
            this.fleetwayWebUpdatesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.updateBestDealPageToolStripMenuItem,
            this.updateGermanBestDealPageToolStripMenuItem,
            this.updateLHCDealToolStripMenuItem,
            this.updateELBDealsToolStripMenuItem});
            this.fleetwayWebUpdatesToolStripMenuItem.Name = "fleetwayWebUpdatesToolStripMenuItem";
            this.fleetwayWebUpdatesToolStripMenuItem.Size = new System.Drawing.Size(138, 20);
            this.fleetwayWebUpdatesToolStripMenuItem.Text = "&Fleetway Web Updates";
            // 
            // updateBestDealPageToolStripMenuItem
            // 
            this.updateBestDealPageToolStripMenuItem.Name = "updateBestDealPageToolStripMenuItem";
            this.updateBestDealPageToolStripMenuItem.Size = new System.Drawing.Size(237, 22);
            this.updateBestDealPageToolStripMenuItem.Text = "&Update Best Deal Page";
            this.updateBestDealPageToolStripMenuItem.Click += new System.EventHandler(this.updateBestDealPageToolStripMenuItem_Click);
            // 
            // updateGermanBestDealPageToolStripMenuItem
            // 
            this.updateGermanBestDealPageToolStripMenuItem.Name = "updateGermanBestDealPageToolStripMenuItem";
            this.updateGermanBestDealPageToolStripMenuItem.Size = new System.Drawing.Size(237, 22);
            this.updateGermanBestDealPageToolStripMenuItem.Text = "&Update German Best Deal Page";
            this.updateGermanBestDealPageToolStripMenuItem.Click += new System.EventHandler(this.updateGermanBestDealPageToolStripMenuItem_Click);
            // 
            // updateLHCDealToolStripMenuItem
            // 
            this.updateLHCDealToolStripMenuItem.Name = "updateLHCDealToolStripMenuItem";
            this.updateLHCDealToolStripMenuItem.Size = new System.Drawing.Size(237, 22);
            this.updateLHCDealToolStripMenuItem.Text = "&Update LHC Deals";
            this.updateLHCDealToolStripMenuItem.Click += new System.EventHandler(this.updateLHCDealToolStripMenuItem_Click);
            // 
            // updateELBDealsToolStripMenuItem
            // 
            this.updateELBDealsToolStripMenuItem.Name = "updateELBDealsToolStripMenuItem";
            this.updateELBDealsToolStripMenuItem.Size = new System.Drawing.Size(237, 22);
            this.updateELBDealsToolStripMenuItem.Text = "&Update ELB Deals";
            this.updateELBDealsToolStripMenuItem.Click += new System.EventHandler(this.updateELBDealsToolStripMenuItem_Click);
            // 
            // dataGridViewAutoFilterTextBoxColumn1
            // 
            this.dataGridViewAutoFilterTextBoxColumn1.DataPropertyName = "Supplier";
            this.dataGridViewAutoFilterTextBoxColumn1.HeaderText = "Supplier";
            this.dataGridViewAutoFilterTextBoxColumn1.Name = "dataGridViewAutoFilterTextBoxColumn1";
            this.dataGridViewAutoFilterTextBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewAutoFilterTextBoxColumn1.Width = 200;
            // 
            // dataGridViewAutoFilterTextBoxColumn2
            // 
            this.dataGridViewAutoFilterTextBoxColumn2.DataPropertyName = "Offer Name";
            this.dataGridViewAutoFilterTextBoxColumn2.HeaderText = "Offer Name";
            this.dataGridViewAutoFilterTextBoxColumn2.Name = "dataGridViewAutoFilterTextBoxColumn2";
            this.dataGridViewAutoFilterTextBoxColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewAutoFilterTextBoxColumn2.Width = 250;
            // 
            // dataGridViewAutoFilterTextBoxColumn3
            // 
            this.dataGridViewAutoFilterTextBoxColumn3.DataPropertyName = "Deal Directory Name";
            this.dataGridViewAutoFilterTextBoxColumn3.HeaderText = "Deal Directory Name";
            this.dataGridViewAutoFilterTextBoxColumn3.Name = "dataGridViewAutoFilterTextBoxColumn3";
            this.dataGridViewAutoFilterTextBoxColumn3.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewAutoFilterTextBoxColumn3.Width = 250;
            // 
            // dataGridViewAutoFilterTextBoxColumn4
            // 
            this.dataGridViewAutoFilterTextBoxColumn4.DataPropertyName = "On Sales";
            this.dataGridViewAutoFilterTextBoxColumn4.HeaderText = "On Sales";
            this.dataGridViewAutoFilterTextBoxColumn4.Name = "dataGridViewAutoFilterTextBoxColumn4";
            this.dataGridViewAutoFilterTextBoxColumn4.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewAutoFilterTextBoxColumn4.Width = 150;
            // 
            // dataGridViewAutoFilterTextBoxColumn5
            // 
            this.dataGridViewAutoFilterTextBoxColumn5.DataPropertyName = "Expired Date";
            this.dataGridViewAutoFilterTextBoxColumn5.HeaderText = "Expired Date";
            this.dataGridViewAutoFilterTextBoxColumn5.Name = "dataGridViewAutoFilterTextBoxColumn5";
            this.dataGridViewAutoFilterTextBoxColumn5.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // dataGridViewLinkColumn1
            // 
            this.dataGridViewLinkColumn1.DataPropertyName = "Delete";
            this.dataGridViewLinkColumn1.HeaderText = "Delete";
            this.dataGridViewLinkColumn1.Name = "dataGridViewLinkColumn1";
            this.dataGridViewLinkColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewLinkColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dataGridViewLinkColumn1.Width = 80;
            // 
            // dataGridViewLinkColumn2
            // 
            this.dataGridViewLinkColumn2.DataPropertyName = "View";
            this.dataGridViewLinkColumn2.HeaderText = "View";
            this.dataGridViewLinkColumn2.Name = "dataGridViewLinkColumn2";
            this.dataGridViewLinkColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewLinkColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dataGridViewLinkColumn2.Width = 80;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "id";
            this.dataGridViewTextBoxColumn1.HeaderText = "id";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Visible = false;
            // 
            // dataGridViewAutoFilterTextBoxColumn6
            // 
            this.dataGridViewAutoFilterTextBoxColumn6.DataPropertyName = "Supplier";
            this.dataGridViewAutoFilterTextBoxColumn6.HeaderText = "Supplier";
            this.dataGridViewAutoFilterTextBoxColumn6.Name = "dataGridViewAutoFilterTextBoxColumn6";
            this.dataGridViewAutoFilterTextBoxColumn6.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewAutoFilterTextBoxColumn6.Width = 200;
            // 
            // dataGridViewAutoFilterTextBoxColumn7
            // 
            this.dataGridViewAutoFilterTextBoxColumn7.DataPropertyName = "Offer Name";
            this.dataGridViewAutoFilterTextBoxColumn7.HeaderText = "Offer Name";
            this.dataGridViewAutoFilterTextBoxColumn7.Name = "dataGridViewAutoFilterTextBoxColumn7";
            this.dataGridViewAutoFilterTextBoxColumn7.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewAutoFilterTextBoxColumn7.Width = 250;
            // 
            // dataGridViewAutoFilterTextBoxColumn8
            // 
            this.dataGridViewAutoFilterTextBoxColumn8.DataPropertyName = "Deal Directory Name";
            this.dataGridViewAutoFilterTextBoxColumn8.HeaderText = "Deal Directory Name";
            this.dataGridViewAutoFilterTextBoxColumn8.Name = "dataGridViewAutoFilterTextBoxColumn8";
            this.dataGridViewAutoFilterTextBoxColumn8.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewAutoFilterTextBoxColumn8.Width = 250;
            // 
            // dataGridViewAutoFilterTextBoxColumn9
            // 
            this.dataGridViewAutoFilterTextBoxColumn9.DataPropertyName = "On Sales";
            this.dataGridViewAutoFilterTextBoxColumn9.HeaderText = "On Sales";
            this.dataGridViewAutoFilterTextBoxColumn9.Name = "dataGridViewAutoFilterTextBoxColumn9";
            this.dataGridViewAutoFilterTextBoxColumn9.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewAutoFilterTextBoxColumn9.Width = 150;
            // 
            // dataGridViewAutoFilterTextBoxColumn10
            // 
            this.dataGridViewAutoFilterTextBoxColumn10.DataPropertyName = "Expired Date";
            this.dataGridViewAutoFilterTextBoxColumn10.HeaderText = "Expired Date";
            this.dataGridViewAutoFilterTextBoxColumn10.Name = "dataGridViewAutoFilterTextBoxColumn10";
            this.dataGridViewAutoFilterTextBoxColumn10.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // dataGridViewLinkColumn3
            // 
            this.dataGridViewLinkColumn3.DataPropertyName = "Copy";
            this.dataGridViewLinkColumn3.HeaderText = "Copy";
            this.dataGridViewLinkColumn3.Name = "dataGridViewLinkColumn3";
            this.dataGridViewLinkColumn3.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewLinkColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dataGridViewLinkColumn3.Width = 80;
            // 
            // flcsLibrary
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1254, 710);
            this.Controls.Add(this.kryptonPanel);
            this.Name = "flcsLibrary";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gulliver II Library";
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel)).EndInit();
            this.kryptonPanel.ResumeLayout(false);
            this.kryptonPanel.PerformLayout();
            this.statusstrip.ResumeLayout(false);
            this.statusstrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.kryptonPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewLibrary)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsLibrary)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.libraryDS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSuppliers)).EndInit();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonManager kryptonManager;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel;
        private ComponentFactory.Krypton.Toolkit.KryptonHeader headerI;
        private ComponentFactory.Krypton.Toolkit.ButtonSpecAny buttonSpecAny1;
        private System.Windows.Forms.StatusStrip statusstrip;
        private System.Windows.Forms.ToolStripStatusLabel fiterStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel showAllLabel;
        private System.Windows.Forms.ToolStripProgressBar libraryProgressbar;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox cmbSuppliers;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel1;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckBox cbShowAll;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtSearch;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newOfferToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem healthSafetyDocumentToolStripMenuItem;
        private ComponentFactory.Krypton.Toolkit.KryptonDataGridView dataGridViewLibrary;
        private ComponentFactory.Krypton.Toolkit.KryptonHeader kryptonHeader1;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel2;
        private System.Windows.Forms.BindingSource bsLibrary;
        private LibraryDS libraryDS;
        private System.Windows.Forms.ToolStripMenuItem fleetwayWebUpdatesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem updateBestDealPageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem updateGermanBestDealPageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem updateLHCDealToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem updateELBDealsToolStripMenuItem;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn dataGridViewAutoFilterTextBoxColumn1;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn dataGridViewAutoFilterTextBoxColumn2;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn dataGridViewAutoFilterTextBoxColumn3;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn dataGridViewAutoFilterTextBoxColumn4;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn dataGridViewAutoFilterTextBoxColumn5;
        private System.Windows.Forms.DataGridViewLinkColumn dataGridViewLinkColumn1;
        private System.Windows.Forms.DataGridViewLinkColumn dataGridViewLinkColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn dataGridViewAutoFilterTextBoxColumn6;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn dataGridViewAutoFilterTextBoxColumn7;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn dataGridViewAutoFilterTextBoxColumn8;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn dataGridViewAutoFilterTextBoxColumn9;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn dataGridViewAutoFilterTextBoxColumn10;
        private System.Windows.Forms.DataGridViewLinkColumn dataGridViewLinkColumn3;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.DataGridViewLinkColumn deleteDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewLinkColumn viewDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn supplierDataGridViewTextBoxColumn;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn offerNameDataGridViewTextBoxColumn;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn dealDirectoryNameDataGridViewTextBoxColumn;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn onSalesDataGridViewTextBoxColumn;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn expiredDateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewLinkColumn copyDataGridViewTextBoxColumn;
    }
}

