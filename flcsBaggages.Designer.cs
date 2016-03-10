namespace Gulliver
{
    partial class flcsBaggages
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
            this.kryptonManager = new ComponentFactory.Krypton.Toolkit.KryptonManager(this.components);
            this.bsBaggages = new System.Windows.Forms.BindingSource(this.components);
            this.libraryDS = new Gulliver.LibraryDS();
            this.packagesDS = new Gulliver.PackagesDS();
            this.hotelContractHeader = new ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup();
            this.kryptonLabel5 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.txtSearchbox = new System.Windows.Forms.TextBox();
            this.dataGridViewBaggages = new ComponentFactory.Krypton.Toolkit.KryptonDataGridView();
            this.cmsBaggage = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolAddBaggage = new System.Windows.Forms.ToolStripMenuItem();
            this.statusstripHolidays = new System.Windows.Forms.StatusStrip();
            this.fiterStatusLabelH = new System.Windows.Forms.ToolStripStatusLabel();
            this.showAllLabelH = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblTotal = new System.Windows.Forms.ToolStripStatusLabel();
            this.miniToolStrip = new System.Windows.Forms.StatusStrip();
            this.btnSave = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.kryptonPanel = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.btnCancel = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.deleteDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewLinkColumn();
            this.airlineDataGridViewTextBoxColumn = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.priceDataGridViewTextBoxColumn = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.bsBaggages)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.libraryDS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.packagesDS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hotelContractHeader)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hotelContractHeader.Panel)).BeginInit();
            this.hotelContractHeader.Panel.SuspendLayout();
            this.hotelContractHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewBaggages)).BeginInit();
            this.cmsBaggage.SuspendLayout();
            this.statusstripHolidays.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel)).BeginInit();
            this.kryptonPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // bsBaggages
            // 
            this.bsBaggages.DataMember = "Baggage";
            this.bsBaggages.DataSource = this.libraryDS;
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
            this.hotelContractHeader.Panel.Controls.Add(this.dataGridViewBaggages);
            this.hotelContractHeader.Panel.Controls.Add(this.statusstripHolidays);
            this.hotelContractHeader.Size = new System.Drawing.Size(835, 591);
            this.hotelContractHeader.StateCommon.Back.Color1 = System.Drawing.Color.White;
            this.hotelContractHeader.StateCommon.HeaderPrimary.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(102)))));
            this.hotelContractHeader.StateCommon.HeaderPrimary.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(102)))));
            this.hotelContractHeader.StateCommon.HeaderPrimary.Content.ShortText.Color1 = System.Drawing.Color.White;
            this.hotelContractHeader.TabIndex = 13;
            this.hotelContractHeader.ValuesPrimary.Heading = "Baggage";
            this.hotelContractHeader.ValuesPrimary.Image = null;
            // 
            // kryptonLabel5
            // 
            this.kryptonLabel5.Location = new System.Drawing.Point(11, 8);
            this.kryptonLabel5.Name = "kryptonLabel5";
            this.kryptonLabel5.Size = new System.Drawing.Size(111, 20);
            this.kryptonLabel5.TabIndex = 72;
            this.kryptonLabel5.Values.Text = "Search airline here";
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
            // dataGridViewBaggages
            // 
            this.dataGridViewBaggages.AutoGenerateColumns = false;
            this.dataGridViewBaggages.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewBaggages.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.deleteDataGridViewTextBoxColumn,
            this.airlineDataGridViewTextBoxColumn,
            this.priceDataGridViewTextBoxColumn});
            this.dataGridViewBaggages.ContextMenuStrip = this.cmsBaggage;
            this.dataGridViewBaggages.DataSource = this.bsBaggages;
            this.dataGridViewBaggages.Location = new System.Drawing.Point(-1, 34);
            this.dataGridViewBaggages.Name = "dataGridViewBaggages";
            this.dataGridViewBaggages.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.ProfessionalSystem;
            this.dataGridViewBaggages.Size = new System.Drawing.Size(835, 502);
            this.dataGridViewBaggages.StateCommon.Background.Color1 = System.Drawing.Color.White;
            this.dataGridViewBaggages.StateCommon.BackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.GridBackgroundList;
            this.dataGridViewBaggages.StateCommon.HeaderColumn.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(218)))), ((int)(((byte)(218)))));
            this.dataGridViewBaggages.StateCommon.HeaderColumn.Back.Color2 = System.Drawing.Color.White;
            this.dataGridViewBaggages.StateCommon.HeaderColumn.Content.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(102)))));
            this.dataGridViewBaggages.StateCommon.HeaderColumn.Content.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridViewBaggages.TabIndex = 8;
            this.dataGridViewBaggages.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewPackageBackup_CellContentClick);
            // 
            // cmsBaggage
            // 
            this.cmsBaggage.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cmsBaggage.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolAddBaggage});
            this.cmsBaggage.Name = "cmsMedia";
            this.cmsBaggage.Size = new System.Drawing.Size(146, 26);
            // 
            // toolAddBaggage
            // 
            this.toolAddBaggage.Name = "toolAddBaggage";
            this.toolAddBaggage.Size = new System.Drawing.Size(145, 22);
            this.toolAddBaggage.Text = "&Add Baggage";
            this.toolAddBaggage.Click += new System.EventHandler(this.toolAddBaggage_Click);
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
            this.statusstripHolidays.Size = new System.Drawing.Size(833, 22);
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
            this.btnSave.Location = new System.Drawing.Point(717, 597);
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
            this.kryptonPanel.Size = new System.Drawing.Size(835, 630);
            this.kryptonPanel.StateCommon.Color1 = System.Drawing.Color.White;
            this.kryptonPanel.TabIndex = 0;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(596, 597);
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
            // deleteDataGridViewTextBoxColumn
            // 
            this.deleteDataGridViewTextBoxColumn.DataPropertyName = "Delete";
            this.deleteDataGridViewTextBoxColumn.HeaderText = "Delete";
            this.deleteDataGridViewTextBoxColumn.Name = "deleteDataGridViewTextBoxColumn";
            this.deleteDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.deleteDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // airlineDataGridViewTextBoxColumn
            // 
            this.airlineDataGridViewTextBoxColumn.DataPropertyName = "airline";
            this.airlineDataGridViewTextBoxColumn.HeaderText = "Airline";
            this.airlineDataGridViewTextBoxColumn.Name = "airlineDataGridViewTextBoxColumn";
            this.airlineDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.airlineDataGridViewTextBoxColumn.Width = 300;
            // 
            // priceDataGridViewTextBoxColumn
            // 
            this.priceDataGridViewTextBoxColumn.DataPropertyName = "price";
            this.priceDataGridViewTextBoxColumn.HeaderText = "Price";
            this.priceDataGridViewTextBoxColumn.Name = "priceDataGridViewTextBoxColumn";
            this.priceDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // flcsBaggages
            // 
            this.AllowStatusStripMerge = false;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(835, 630);
            this.Controls.Add(this.kryptonPanel);
            this.Name = "flcsBaggages";
            this.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.ProfessionalSystem;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)(this.bsBaggages)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.libraryDS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.packagesDS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hotelContractHeader.Panel)).EndInit();
            this.hotelContractHeader.Panel.ResumeLayout(false);
            this.hotelContractHeader.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.hotelContractHeader)).EndInit();
            this.hotelContractHeader.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewBaggages)).EndInit();
            this.cmsBaggage.ResumeLayout(false);
            this.statusstripHolidays.ResumeLayout(false);
            this.statusstripHolidays.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel)).EndInit();
            this.kryptonPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonManager kryptonManager;
        private System.Windows.Forms.BindingSource bsBaggages;
        private PackagesDS packagesDS;
        private ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup hotelContractHeader;
        private ComponentFactory.Krypton.Toolkit.KryptonDataGridView dataGridViewBaggages;
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
        private System.Windows.Forms.ContextMenuStrip cmsBaggage;
        private System.Windows.Forms.ToolStripMenuItem toolAddBaggage;
        private LibraryDS libraryDS;
        private System.Windows.Forms.DataGridViewLinkColumn deleteDataGridViewTextBoxColumn;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn airlineDataGridViewTextBoxColumn;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn priceDataGridViewTextBoxColumn;
    }
}

