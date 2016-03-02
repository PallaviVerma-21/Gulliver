namespace Gulliver
{
    partial class flcsSetLeading
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
            this.kryptonPanel = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.kryptonHeaderGroup7 = new ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup();
            this.statusStrip5 = new System.Windows.Forms.StatusStrip();
            this.filterLabelLeadings = new System.Windows.Forms.ToolStripStatusLabel();
            this.showAllLeadings = new System.Windows.Forms.ToolStripStatusLabel();
            this.dataGridViewLeadings = new ComponentFactory.Krypton.Toolkit.KryptonDataGridView();
            this.idDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.departureAirportDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.durationDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.occupancyDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.leadingPriceDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lockTheLeadingDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.bsSetLeadings = new System.Windows.Forms.BindingSource(this.components);
            this.packagesDS1 = new Gulliver.PackagesDS();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel)).BeginInit();
            this.kryptonPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup7.Panel)).BeginInit();
            this.kryptonHeaderGroup7.Panel.SuspendLayout();
            this.kryptonHeaderGroup7.SuspendLayout();
            this.statusStrip5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewLeadings)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsSetLeadings)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.packagesDS1)).BeginInit();
            this.SuspendLayout();
            // 
            // kryptonPanel
            // 
            this.kryptonPanel.Controls.Add(this.kryptonHeaderGroup7);
            this.kryptonPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel.Name = "kryptonPanel";
            this.kryptonPanel.Size = new System.Drawing.Size(602, 486);
            this.kryptonPanel.TabIndex = 0;
            // 
            // kryptonHeaderGroup7
            // 
            this.kryptonHeaderGroup7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonHeaderGroup7.GroupBackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.PanelAlternate;
            this.kryptonHeaderGroup7.HeaderVisibleSecondary = false;
            this.kryptonHeaderGroup7.Location = new System.Drawing.Point(0, 0);
            this.kryptonHeaderGroup7.Name = "kryptonHeaderGroup7";
            this.kryptonHeaderGroup7.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.ProfessionalSystem;
            // 
            // kryptonHeaderGroup7.Panel
            // 
            this.kryptonHeaderGroup7.Panel.Controls.Add(this.statusStrip5);
            this.kryptonHeaderGroup7.Panel.Controls.Add(this.dataGridViewLeadings);
            this.kryptonHeaderGroup7.Size = new System.Drawing.Size(602, 486);
            this.kryptonHeaderGroup7.StateCommon.HeaderPrimary.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(218)))), ((int)(((byte)(218)))));
            this.kryptonHeaderGroup7.StateCommon.HeaderPrimary.Back.Color2 = System.Drawing.Color.White;
            this.kryptonHeaderGroup7.StateCommon.HeaderPrimary.Content.ShortText.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(102)))));
            this.kryptonHeaderGroup7.TabIndex = 17;
            this.kryptonHeaderGroup7.ValuesPrimary.Heading = "Set Leadings";
            this.kryptonHeaderGroup7.ValuesPrimary.Image = null;
            // 
            // statusStrip5
            // 
            this.statusStrip5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(218)))), ((int)(((byte)(218)))));
            this.statusStrip5.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.statusStrip5.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.filterLabelLeadings,
            this.showAllLeadings});
            this.statusStrip5.Location = new System.Drawing.Point(0, 434);
            this.statusStrip5.Name = "statusStrip5";
            this.statusStrip5.Size = new System.Drawing.Size(600, 22);
            this.statusStrip5.TabIndex = 7;
            // 
            // filterLabelLeadings
            // 
            this.filterLabelLeadings.Name = "filterLabelLeadings";
            this.filterLabelLeadings.Size = new System.Drawing.Size(0, 17);
            // 
            // showAllLeadings
            // 
            this.showAllLeadings.IsLink = true;
            this.showAllLeadings.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.showAllLeadings.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(102)))));
            this.showAllLeadings.Name = "showAllLeadings";
            this.showAllLeadings.Size = new System.Drawing.Size(53, 17);
            this.showAllLeadings.Text = "&Show All";
            this.showAllLeadings.Click += new System.EventHandler(this.showAllLeadings_Click);
            // 
            // dataGridViewLeadings
            // 
            this.dataGridViewLeadings.AutoGenerateColumns = false;
            this.dataGridViewLeadings.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewLeadings.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idDataGridViewTextBoxColumn,
            this.departureAirportDataGridViewTextBoxColumn,
            this.durationDataGridViewTextBoxColumn,
            this.occupancyDataGridViewTextBoxColumn,
            this.leadingPriceDataGridViewTextBoxColumn,
            this.lockTheLeadingDataGridViewCheckBoxColumn});
            this.dataGridViewLeadings.DataSource = this.bsSetLeadings;
            this.dataGridViewLeadings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewLeadings.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewLeadings.Name = "dataGridViewLeadings";
            this.dataGridViewLeadings.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.ProfessionalSystem;
            this.dataGridViewLeadings.Size = new System.Drawing.Size(600, 456);
            this.dataGridViewLeadings.StateCommon.Background.Color1 = System.Drawing.Color.White;
            this.dataGridViewLeadings.StateCommon.BackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.GridBackgroundList;
            this.dataGridViewLeadings.StateCommon.HeaderColumn.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(218)))), ((int)(((byte)(218)))));
            this.dataGridViewLeadings.StateCommon.HeaderColumn.Back.Color2 = System.Drawing.Color.White;
            this.dataGridViewLeadings.StateCommon.HeaderColumn.Content.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(102)))));
            this.dataGridViewLeadings.TabIndex = 0;
            // 
            // idDataGridViewTextBoxColumn
            // 
            this.idDataGridViewTextBoxColumn.DataPropertyName = "id";
            this.idDataGridViewTextBoxColumn.HeaderText = "id";
            this.idDataGridViewTextBoxColumn.Name = "idDataGridViewTextBoxColumn";
            this.idDataGridViewTextBoxColumn.Visible = false;
            // 
            // departureAirportDataGridViewTextBoxColumn
            // 
            this.departureAirportDataGridViewTextBoxColumn.DataPropertyName = "DepartureAirport";
            this.departureAirportDataGridViewTextBoxColumn.HeaderText = "Dep Airport";
            this.departureAirportDataGridViewTextBoxColumn.Name = "departureAirportDataGridViewTextBoxColumn";
            // 
            // durationDataGridViewTextBoxColumn
            // 
            this.durationDataGridViewTextBoxColumn.DataPropertyName = "Duration";
            this.durationDataGridViewTextBoxColumn.HeaderText = "Duration";
            this.durationDataGridViewTextBoxColumn.Name = "durationDataGridViewTextBoxColumn";
            // 
            // occupancyDataGridViewTextBoxColumn
            // 
            this.occupancyDataGridViewTextBoxColumn.DataPropertyName = "Occupancy";
            this.occupancyDataGridViewTextBoxColumn.HeaderText = "Occupancy";
            this.occupancyDataGridViewTextBoxColumn.Name = "occupancyDataGridViewTextBoxColumn";
            // 
            // leadingPriceDataGridViewTextBoxColumn
            // 
            this.leadingPriceDataGridViewTextBoxColumn.DataPropertyName = "Leading Price";
            this.leadingPriceDataGridViewTextBoxColumn.HeaderText = "Leading Price";
            this.leadingPriceDataGridViewTextBoxColumn.Name = "leadingPriceDataGridViewTextBoxColumn";
            this.leadingPriceDataGridViewTextBoxColumn.Width = 120;
            // 
            // lockTheLeadingDataGridViewCheckBoxColumn
            // 
            this.lockTheLeadingDataGridViewCheckBoxColumn.DataPropertyName = "LockTheLeading";
            this.lockTheLeadingDataGridViewCheckBoxColumn.HeaderText = "Lock The Leading";
            this.lockTheLeadingDataGridViewCheckBoxColumn.Name = "lockTheLeadingDataGridViewCheckBoxColumn";
            this.lockTheLeadingDataGridViewCheckBoxColumn.Width = 120;
            // 
            // bsSetLeadings
            // 
            this.bsSetLeadings.DataMember = "LeadingPrices";
            this.bsSetLeadings.DataSource = this.packagesDS1;
            // 
            // packagesDS1
            // 
            this.packagesDS1.DataSetName = "PackagesDS";
            this.packagesDS1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // flcsSetLeading
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(602, 486);
            this.Controls.Add(this.kryptonPanel);
            this.Name = "flcsSetLeading";
            this.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.ProfessionalSystem;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.flcsSetLeading_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel)).EndInit();
            this.kryptonPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup7.Panel)).EndInit();
            this.kryptonHeaderGroup7.Panel.ResumeLayout(false);
            this.kryptonHeaderGroup7.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup7)).EndInit();
            this.kryptonHeaderGroup7.ResumeLayout(false);
            this.statusStrip5.ResumeLayout(false);
            this.statusStrip5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewLeadings)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsSetLeadings)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.packagesDS1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonManager kryptonManager;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel;
        private ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup kryptonHeaderGroup7;
        private System.Windows.Forms.StatusStrip statusStrip5;
        private System.Windows.Forms.ToolStripStatusLabel filterLabelLeadings;
        private System.Windows.Forms.ToolStripStatusLabel showAllLeadings;
        private ComponentFactory.Krypton.Toolkit.KryptonDataGridView dataGridViewLeadings;
        private System.Windows.Forms.BindingSource bsSetLeadings;
        private System.Windows.Forms.DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn departureAirportDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn durationDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn occupancyDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn leadingPriceDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn lockTheLeadingDataGridViewCheckBoxColumn;
        private PackagesDS packagesDS1;
    }
}

