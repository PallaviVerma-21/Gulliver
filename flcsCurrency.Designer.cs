namespace GulliverII
{
    partial class flcsCurrency
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(flcsCurrency));
            this.kryptonManager = new ComponentFactory.Krypton.Toolkit.KryptonManager(this.components);
            this.bsCurrency = new System.Windows.Forms.BindingSource(this.components);
            this.libraryDS = new GulliverII.LibraryDS();
            this.packagesDS = new GulliverII.PackagesDS();
            this.hotelContractHeader = new ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup();
            this.kryptonLabel5 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.txtSearchbox = new System.Windows.Forms.TextBox();
            this.dataGridViewCurrencys = new ComponentFactory.Krypton.Toolkit.KryptonDataGridView();
            this.deleteDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewLinkColumn();
            this.currencyDataGridViewTextBoxColumn = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.exchangeRateDataGridViewTextBoxColumn = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.sellAtRateDataGridViewTextBoxColumn = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.cmsCurrency = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolAddCurrency = new System.Windows.Forms.ToolStripMenuItem();
            this.statusstripHolidays = new System.Windows.Forms.StatusStrip();
            this.fiterStatusLabelH = new System.Windows.Forms.ToolStripStatusLabel();
            this.showAllLabelH = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblTotal = new System.Windows.Forms.ToolStripStatusLabel();
            this.miniToolStrip = new System.Windows.Forms.StatusStrip();
            this.btnSave = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.kryptonPanel = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.btnCancel = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            ((System.ComponentModel.ISupportInitialize)(this.bsCurrency)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.libraryDS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.packagesDS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hotelContractHeader)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hotelContractHeader.Panel)).BeginInit();
            this.hotelContractHeader.Panel.SuspendLayout();
            this.hotelContractHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCurrencys)).BeginInit();
            this.cmsCurrency.SuspendLayout();
            this.statusstripHolidays.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel)).BeginInit();
            this.kryptonPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // bsCurrency
            // 
            this.bsCurrency.DataMember = "Currency";
            this.bsCurrency.DataSource = this.libraryDS;
            // 
            // libraryDS
            // 
            this.libraryDS.DataSetName = "LibraryDS";
            this.libraryDS.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // packagesDS
            // 
            this.packagesDS.DataSetName = "PackagesDS";
            this.packagesDS.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // hotelContractHeader
            // 
            this.hotelContractHeader.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.hotelContractHeader.GroupBackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.PanelAlternate;
            this.hotelContractHeader.HeaderVisibleSecondary = false;
            this.hotelContractHeader.Location = new System.Drawing.Point(0, 0);
            this.hotelContractHeader.Name = "hotelContractHeader";
            this.hotelContractHeader.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.ProfessionalSystem;
            // 
            // hotelContractHeader.Panel
            // 
            this.hotelContractHeader.Panel.Controls.Add(this.kryptonLabel5);
            this.hotelContractHeader.Panel.Controls.Add(this.txtSearchbox);
            this.hotelContractHeader.Panel.Controls.Add(this.dataGridViewCurrencys);
            this.hotelContractHeader.Panel.Controls.Add(this.statusstripHolidays);
            this.hotelContractHeader.Size = new System.Drawing.Size(730, 591);
            this.hotelContractHeader.StateCommon.Back.Color1 = System.Drawing.Color.White;
            this.hotelContractHeader.StateCommon.HeaderPrimary.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(102)))));
            this.hotelContractHeader.StateCommon.HeaderPrimary.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(102)))));
            this.hotelContractHeader.StateCommon.HeaderPrimary.Content.ShortText.Color1 = System.Drawing.Color.White;
            this.hotelContractHeader.TabIndex = 13;
            this.hotelContractHeader.ValuesPrimary.Heading = "Currency";
            this.hotelContractHeader.ValuesPrimary.Image = null;
            // 
            // kryptonLabel5
            // 
            this.kryptonLabel5.Location = new System.Drawing.Point(11, 8);
            this.kryptonLabel5.Name = "kryptonLabel5";
            this.kryptonLabel5.Size = new System.Drawing.Size(124, 20);
            this.kryptonLabel5.TabIndex = 72;
            this.kryptonLabel5.Values.Text = "Search currency here";
            // 
            // txtSearchbox
            // 
            this.txtSearchbox.Location = new System.Drawing.Point(139, 8);
            this.txtSearchbox.Name = "txtSearchbox";
            this.txtSearchbox.Size = new System.Drawing.Size(246, 20);
            this.txtSearchbox.TabIndex = 71;
            this.txtSearchbox.TextChanged += new System.EventHandler(this.txtSearchbox_TextChanged);
            this.txtSearchbox.Enter += new System.EventHandler(this.txtSearchbox_Enter);
            this.txtSearchbox.Leave += new System.EventHandler(this.txtSearchbox_Leave);
            // 
            // dataGridViewCurrencys
            // 
            this.dataGridViewCurrencys.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewCurrencys.AutoGenerateColumns = false;
            this.dataGridViewCurrencys.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewCurrencys.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.deleteDataGridViewTextBoxColumn,
            this.currencyDataGridViewTextBoxColumn,
            this.exchangeRateDataGridViewTextBoxColumn,
            this.sellAtRateDataGridViewTextBoxColumn});
            this.dataGridViewCurrencys.ContextMenuStrip = this.cmsCurrency;
            this.dataGridViewCurrencys.DataSource = this.bsCurrency;
            this.dataGridViewCurrencys.Location = new System.Drawing.Point(-1, 34);
            this.dataGridViewCurrencys.Name = "dataGridViewCurrencys";
            this.dataGridViewCurrencys.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.ProfessionalSystem;
            this.dataGridViewCurrencys.Size = new System.Drawing.Size(729, 502);
            this.dataGridViewCurrencys.StateCommon.Background.Color1 = System.Drawing.Color.White;
            this.dataGridViewCurrencys.StateCommon.BackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.GridBackgroundList;
            this.dataGridViewCurrencys.StateCommon.HeaderColumn.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(218)))), ((int)(((byte)(218)))));
            this.dataGridViewCurrencys.StateCommon.HeaderColumn.Back.Color2 = System.Drawing.Color.White;
            this.dataGridViewCurrencys.StateCommon.HeaderColumn.Content.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(102)))));
            this.dataGridViewCurrencys.StateCommon.HeaderColumn.Content.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridViewCurrencys.TabIndex = 8;
            this.dataGridViewCurrencys.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewPackageBackup_CellContentClick);
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
            // currencyDataGridViewTextBoxColumn
            // 
            this.currencyDataGridViewTextBoxColumn.DataPropertyName = "Currency";
            this.currencyDataGridViewTextBoxColumn.HeaderText = "Currency";
            this.currencyDataGridViewTextBoxColumn.Name = "currencyDataGridViewTextBoxColumn";
            this.currencyDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.currencyDataGridViewTextBoxColumn.Width = 150;
            // 
            // exchangeRateDataGridViewTextBoxColumn
            // 
            this.exchangeRateDataGridViewTextBoxColumn.DataPropertyName = "ExchangeRate";
            this.exchangeRateDataGridViewTextBoxColumn.HeaderText = "ExchangeRate";
            this.exchangeRateDataGridViewTextBoxColumn.Name = "exchangeRateDataGridViewTextBoxColumn";
            this.exchangeRateDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.exchangeRateDataGridViewTextBoxColumn.Width = 150;
            // 
            // sellAtRateDataGridViewTextBoxColumn
            // 
            this.sellAtRateDataGridViewTextBoxColumn.DataPropertyName = "SellAtRate";
            this.sellAtRateDataGridViewTextBoxColumn.HeaderText = "SellAtRate";
            this.sellAtRateDataGridViewTextBoxColumn.Name = "sellAtRateDataGridViewTextBoxColumn";
            this.sellAtRateDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.sellAtRateDataGridViewTextBoxColumn.Width = 150;
            // 
            // cmsCurrency
            // 
            this.cmsCurrency.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cmsCurrency.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolAddCurrency});
            this.cmsCurrency.Name = "cmsMedia";
            this.cmsCurrency.Size = new System.Drawing.Size(148, 26);
            // 
            // toolAddCurrency
            // 
            this.toolAddCurrency.Name = "toolAddCurrency";
            this.toolAddCurrency.Size = new System.Drawing.Size(147, 22);
            this.toolAddCurrency.Text = "&Add Currency";
            this.toolAddCurrency.Click += new System.EventHandler(this.toolAddCurrency_Click);
            // 
            // statusstripHolidays
            // 
            this.statusstripHolidays.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(218)))), ((int)(((byte)(218)))));
            this.statusstripHolidays.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.statusstripHolidays.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fiterStatusLabelH,
            this.showAllLabelH,
            this.lblTotal});
            this.statusstripHolidays.Location = new System.Drawing.Point(0, 539);
            this.statusstripHolidays.Name = "statusstripHolidays";
            this.statusstripHolidays.Size = new System.Drawing.Size(728, 22);
            this.statusstripHolidays.TabIndex = 7;
            // 
            // fiterStatusLabelH
            // 
            this.fiterStatusLabelH.Name = "fiterStatusLabelH";
            this.fiterStatusLabelH.Size = new System.Drawing.Size(0, 17);
            // 
            // showAllLabelH
            // 
            this.showAllLabelH.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(102)))));
            this.showAllLabelH.IsLink = true;
            this.showAllLabelH.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.showAllLabelH.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(102)))));
            this.showAllLabelH.Name = "showAllLabelH";
            this.showAllLabelH.Size = new System.Drawing.Size(53, 17);
            this.showAllLabelH.Text = "&Show All";
            // 
            // lblTotal
            // 
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(0, 17);
            // 
            // miniToolStrip
            // 
            this.miniToolStrip.AutoSize = false;
            this.miniToolStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(102)))));
            this.miniToolStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.miniToolStrip.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.miniToolStrip.Location = new System.Drawing.Point(54, 1);
            this.miniToolStrip.Name = "miniToolStrip";
            this.miniToolStrip.Size = new System.Drawing.Size(1157, 22);
            this.miniToolStrip.TabIndex = 7;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(609, 597);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(106, 25);
            this.btnSave.StateCommon.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(102)))));
            this.btnSave.StateCommon.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(102)))));
            this.btnSave.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.White;
            this.btnSave.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.StatePressed.Back.Color1 = System.Drawing.Color.Gold;
            this.btnSave.StatePressed.Back.Color2 = System.Drawing.Color.Gold;
            this.btnSave.StatePressed.Content.ShortText.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(102)))));
            this.btnSave.TabIndex = 15;
            this.btnSave.Values.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // kryptonPanel
            // 
            this.kryptonPanel.Controls.Add(this.btnCancel);
            this.kryptonPanel.Controls.Add(this.btnSave);
            this.kryptonPanel.Controls.Add(this.hotelContractHeader);
            this.kryptonPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel.Name = "kryptonPanel";
            this.kryptonPanel.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.ProfessionalSystem;
            this.kryptonPanel.Size = new System.Drawing.Size(730, 630);
            this.kryptonPanel.StateCommon.Color1 = System.Drawing.Color.White;
            this.kryptonPanel.TabIndex = 0;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(497, 597);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(106, 25);
            this.btnCancel.StateCommon.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(102)))));
            this.btnCancel.StateCommon.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(102)))));
            this.btnCancel.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.White;
            this.btnCancel.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.StatePressed.Back.Color1 = System.Drawing.Color.Gold;
            this.btnCancel.StatePressed.Back.Color2 = System.Drawing.Color.Gold;
            this.btnCancel.StatePressed.Content.ShortText.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(102)))));
            this.btnCancel.TabIndex = 16;
            this.btnCancel.Values.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // flcsCurrency
            // 
            this.AllowStatusStripMerge = false;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(730, 630);
            this.Controls.Add(this.kryptonPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "flcsCurrency";
            this.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.ProfessionalSystem;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)(this.bsCurrency)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.libraryDS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.packagesDS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hotelContractHeader.Panel)).EndInit();
            this.hotelContractHeader.Panel.ResumeLayout(false);
            this.hotelContractHeader.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.hotelContractHeader)).EndInit();
            this.hotelContractHeader.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCurrencys)).EndInit();
            this.cmsCurrency.ResumeLayout(false);
            this.statusstripHolidays.ResumeLayout(false);
            this.statusstripHolidays.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel)).EndInit();
            this.kryptonPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonManager kryptonManager;
        private System.Windows.Forms.BindingSource bsCurrency;
        private PackagesDS packagesDS;
        private ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup hotelContractHeader;
        private ComponentFactory.Krypton.Toolkit.KryptonDataGridView dataGridViewCurrencys;
        private System.Windows.Forms.StatusStrip statusstripHolidays;
        private System.Windows.Forms.ToolStripStatusLabel fiterStatusLabelH;
        private System.Windows.Forms.ToolStripStatusLabel showAllLabelH;
        private System.Windows.Forms.ToolStripStatusLabel lblTotal;
        private System.Windows.Forms.StatusStrip miniToolStrip;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnSave;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnCancel;
        private System.Windows.Forms.TextBox txtSearchbox;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel5;
        private System.Windows.Forms.ContextMenuStrip cmsCurrency;
        private System.Windows.Forms.ToolStripMenuItem toolAddCurrency;
        private LibraryDS libraryDS;
        private System.Windows.Forms.DataGridViewLinkColumn deleteDataGridViewTextBoxColumn;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn currencyDataGridViewTextBoxColumn;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn exchangeRateDataGridViewTextBoxColumn;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn sellAtRateDataGridViewTextBoxColumn;
    }
}

