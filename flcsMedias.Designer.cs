namespace Gulliver
{
    partial class flcsMedias
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
            this.bsMedias = new System.Windows.Forms.BindingSource(this.components);
            this.packagesDS = new Gulliver.PackagesDS();
            this.hotelContractHeader = new ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup();
            this.miniToolStrip = new System.Windows.Forms.StatusStrip();
            this.fiterStatusLabelH = new System.Windows.Forms.ToolStripStatusLabel();
            this.showAllLabelH = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblTotal = new System.Windows.Forms.ToolStripStatusLabel();
            this.dataGridViewPackageBackup = new ComponentFactory.Krypton.Toolkit.KryptonDataGridView();
            this.pleaseNoteDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.commissionDataGridViewTextBoxColumn = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.channelCodesDataGridViewTextBoxColumn = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.nameDataGridViewTextBoxColumn = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.idDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.deleteDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewLinkColumn();
            this.btnSave = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.kryptonPanel = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.statusstripHolidays = new System.Windows.Forms.StatusStrip();
            this.btnCancel = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            ((System.ComponentModel.ISupportInitialize)(this.bsMedias)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.packagesDS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hotelContractHeader)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hotelContractHeader.Panel)).BeginInit();
            this.hotelContractHeader.Panel.SuspendLayout();
            this.hotelContractHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPackageBackup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel)).BeginInit();
            this.kryptonPanel.SuspendLayout();
            this.statusstripHolidays.SuspendLayout();
            this.SuspendLayout();
            // 
            // bsMedias
            // 
            this.bsMedias.DataMember = "Media";
            this.bsMedias.DataSource = this.packagesDS;
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
            this.hotelContractHeader.Panel.Controls.Add(this.dataGridViewPackageBackup);
            this.hotelContractHeader.Panel.Controls.Add(this.statusstripHolidays);
            this.hotelContractHeader.Size = new System.Drawing.Size(1159, 591);
            this.hotelContractHeader.StateCommon.HeaderPrimary.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(218)))), ((int)(((byte)(218)))));
            this.hotelContractHeader.StateCommon.HeaderPrimary.Back.Color2 = System.Drawing.Color.White;
            this.hotelContractHeader.StateCommon.HeaderPrimary.Content.ShortText.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(102)))));
            this.hotelContractHeader.TabIndex = 13;
            this.hotelContractHeader.ValuesPrimary.Heading = "Medias";
            this.hotelContractHeader.ValuesPrimary.Image = null;
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
            // dataGridViewPackageBackup
            // 
            this.dataGridViewPackageBackup.AutoGenerateColumns = false;
            this.dataGridViewPackageBackup.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewPackageBackup.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.deleteDataGridViewTextBoxColumn,
            this.idDataGridViewTextBoxColumn,
            this.nameDataGridViewTextBoxColumn,
            this.channelCodesDataGridViewTextBoxColumn,
            this.commissionDataGridViewTextBoxColumn,
            this.pleaseNoteDataGridViewTextBoxColumn});
            this.dataGridViewPackageBackup.DataSource = this.bsMedias;
            this.dataGridViewPackageBackup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewPackageBackup.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewPackageBackup.Name = "dataGridViewPackageBackup";
            this.dataGridViewPackageBackup.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.ProfessionalSystem;
            this.dataGridViewPackageBackup.Size = new System.Drawing.Size(1157, 539);
            this.dataGridViewPackageBackup.StateCommon.Background.Color1 = System.Drawing.Color.White;
            this.dataGridViewPackageBackup.StateCommon.BackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.GridBackgroundList;
            this.dataGridViewPackageBackup.StateCommon.HeaderColumn.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(218)))), ((int)(((byte)(218)))));
            this.dataGridViewPackageBackup.StateCommon.HeaderColumn.Back.Color2 = System.Drawing.Color.White;
            this.dataGridViewPackageBackup.StateCommon.HeaderColumn.Content.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(102)))));
            this.dataGridViewPackageBackup.StateCommon.HeaderColumn.Content.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridViewPackageBackup.TabIndex = 8;
            // 
            // pleaseNoteDataGridViewTextBoxColumn
            // 
            this.pleaseNoteDataGridViewTextBoxColumn.DataPropertyName = "Please Note";
            this.pleaseNoteDataGridViewTextBoxColumn.HeaderText = "Please Note";
            this.pleaseNoteDataGridViewTextBoxColumn.Name = "pleaseNoteDataGridViewTextBoxColumn";
            this.pleaseNoteDataGridViewTextBoxColumn.Width = 450;
            // 
            // commissionDataGridViewTextBoxColumn
            // 
            this.commissionDataGridViewTextBoxColumn.DataPropertyName = "Commission";
            this.commissionDataGridViewTextBoxColumn.HeaderText = "Commission";
            this.commissionDataGridViewTextBoxColumn.Name = "commissionDataGridViewTextBoxColumn";
            this.commissionDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // channelCodesDataGridViewTextBoxColumn
            // 
            this.channelCodesDataGridViewTextBoxColumn.DataPropertyName = "Channel Codes";
            this.channelCodesDataGridViewTextBoxColumn.HeaderText = "Channel Codes";
            this.channelCodesDataGridViewTextBoxColumn.Name = "channelCodesDataGridViewTextBoxColumn";
            this.channelCodesDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.channelCodesDataGridViewTextBoxColumn.Width = 200;
            // 
            // nameDataGridViewTextBoxColumn
            // 
            this.nameDataGridViewTextBoxColumn.DataPropertyName = "Name";
            this.nameDataGridViewTextBoxColumn.HeaderText = "Name";
            this.nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
            this.nameDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.nameDataGridViewTextBoxColumn.Width = 250;
            // 
            // idDataGridViewTextBoxColumn
            // 
            this.idDataGridViewTextBoxColumn.DataPropertyName = "id";
            this.idDataGridViewTextBoxColumn.HeaderText = "id";
            this.idDataGridViewTextBoxColumn.Name = "idDataGridViewTextBoxColumn";
            this.idDataGridViewTextBoxColumn.Visible = false;
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
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(1041, 597);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(106, 25);
            this.btnSave.StateCommon.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(102)))));
            this.btnSave.StateCommon.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(102)))));
            this.btnSave.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.White;
            this.btnSave.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.TabIndex = 15;
            this.btnSave.Values.Text = "Save";
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
            this.kryptonPanel.Size = new System.Drawing.Size(1159, 634);
            this.kryptonPanel.TabIndex = 0;
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
            this.statusstripHolidays.Size = new System.Drawing.Size(1157, 22);
            this.statusstripHolidays.TabIndex = 7;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(929, 597);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(106, 25);
            this.btnCancel.StateCommon.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(102)))));
            this.btnCancel.StateCommon.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(102)))));
            this.btnCancel.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.White;
            this.btnCancel.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.TabIndex = 16;
            this.btnCancel.Values.Text = "Cancel";
            // 
            // flcsMedias
            // 
            this.AllowStatusStripMerge = false;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1159, 634);
            this.Controls.Add(this.kryptonPanel);
            this.Name = "flcsMedias";
            this.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.ProfessionalSystem;
            ((System.ComponentModel.ISupportInitialize)(this.bsMedias)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.packagesDS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hotelContractHeader.Panel)).EndInit();
            this.hotelContractHeader.Panel.ResumeLayout(false);
            this.hotelContractHeader.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.hotelContractHeader)).EndInit();
            this.hotelContractHeader.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPackageBackup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel)).EndInit();
            this.kryptonPanel.ResumeLayout(false);
            this.statusstripHolidays.ResumeLayout(false);
            this.statusstripHolidays.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonManager kryptonManager;
        private System.Windows.Forms.BindingSource bsMedias;
        private PackagesDS packagesDS;
        private ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup hotelContractHeader;
        private ComponentFactory.Krypton.Toolkit.KryptonDataGridView dataGridViewPackageBackup;
        private System.Windows.Forms.DataGridViewLinkColumn deleteDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn nameDataGridViewTextBoxColumn;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn channelCodesDataGridViewTextBoxColumn;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn commissionDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn pleaseNoteDataGridViewTextBoxColumn;
        private System.Windows.Forms.StatusStrip statusstripHolidays;
        private System.Windows.Forms.ToolStripStatusLabel fiterStatusLabelH;
        private System.Windows.Forms.ToolStripStatusLabel showAllLabelH;
        private System.Windows.Forms.ToolStripStatusLabel lblTotal;
        private System.Windows.Forms.StatusStrip miniToolStrip;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnSave;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnCancel;
    }
}

