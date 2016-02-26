namespace Gulliver
{
    partial class flcsOptionalExtra
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
            this.kryptonHeaderGroup4 = new ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.dataGridViewExtras = new ComponentFactory.Krypton.Toolkit.KryptonDataGridView();
            this.bsOptionalcosting = new System.Windows.Forms.BindingSource(this.components);
            this.gulliverDS = new Gulliver.GulliverDS();
            this.idDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.deleteDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewLinkColumn();
            this.descriptionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.includedDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.costDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel)).BeginInit();
            this.kryptonPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup4.Panel)).BeginInit();
            this.kryptonHeaderGroup4.Panel.SuspendLayout();
            this.kryptonHeaderGroup4.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewExtras)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsOptionalcosting)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gulliverDS)).BeginInit();
            this.SuspendLayout();
            // 
            // kryptonPanel
            // 
            this.kryptonPanel.Controls.Add(this.kryptonHeaderGroup4);
            this.kryptonPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel.Name = "kryptonPanel";
            this.kryptonPanel.Size = new System.Drawing.Size(599, 517);
            this.kryptonPanel.TabIndex = 0;
            // 
            // kryptonHeaderGroup4
            // 
            this.kryptonHeaderGroup4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonHeaderGroup4.GroupBackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.PanelAlternate;
            this.kryptonHeaderGroup4.HeaderVisibleSecondary = false;
            this.kryptonHeaderGroup4.Location = new System.Drawing.Point(0, 0);
            this.kryptonHeaderGroup4.Name = "kryptonHeaderGroup4";
            // 
            // kryptonHeaderGroup4.Panel
            // 
            this.kryptonHeaderGroup4.Panel.Controls.Add(this.statusStrip1);
            this.kryptonHeaderGroup4.Panel.Controls.Add(this.dataGridViewExtras);
            this.kryptonHeaderGroup4.Size = new System.Drawing.Size(599, 517);
            this.kryptonHeaderGroup4.TabIndex = 15;
            this.kryptonHeaderGroup4.ValuesPrimary.Heading = "Optional Costing";
            this.kryptonHeaderGroup4.ValuesPrimary.Image = null;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2});
            this.statusStrip1.Location = new System.Drawing.Point(0, 463);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.ManagerRenderMode;
            this.statusStrip1.Size = new System.Drawing.Size(597, 22);
            this.statusStrip1.TabIndex = 7;
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.IsLink = true;
            this.toolStripStatusLabel2.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(53, 17);
            this.toolStripStatusLabel2.Text = "&Show All";
            // 
            // dataGridViewExtras
            // 
            this.dataGridViewExtras.AutoGenerateColumns = false;
            this.dataGridViewExtras.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewExtras.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idDataGridViewTextBoxColumn,
            this.deleteDataGridViewTextBoxColumn,
            this.descriptionDataGridViewTextBoxColumn,
            this.includedDataGridViewCheckBoxColumn,
            this.costDataGridViewTextBoxColumn});
            this.dataGridViewExtras.DataSource = this.bsOptionalcosting;
            this.dataGridViewExtras.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewExtras.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewExtras.Name = "dataGridViewExtras";
            this.dataGridViewExtras.Size = new System.Drawing.Size(597, 485);
            this.dataGridViewExtras.TabIndex = 0;
            this.dataGridViewExtras.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewExtras_CellContentClick);
            this.dataGridViewExtras.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewExtras_RowEnter);
            this.dataGridViewExtras.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.dataGridViewExtras_UserDeletingRow);
            // 
            // bsOptionalcosting
            // 
            this.bsOptionalcosting.DataMember = "OptionalExtra";
            this.bsOptionalcosting.DataSource = this.gulliverDS;
            // 
            // gulliverDS
            // 
            this.gulliverDS.DataSetName = "GulliverDS";
            this.gulliverDS.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
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
            // descriptionDataGridViewTextBoxColumn
            // 
            this.descriptionDataGridViewTextBoxColumn.DataPropertyName = "Description";
            this.descriptionDataGridViewTextBoxColumn.HeaderText = "Description";
            this.descriptionDataGridViewTextBoxColumn.Name = "descriptionDataGridViewTextBoxColumn";
            this.descriptionDataGridViewTextBoxColumn.Width = 300;
            // 
            // includedDataGridViewCheckBoxColumn
            // 
            this.includedDataGridViewCheckBoxColumn.DataPropertyName = "Included";
            this.includedDataGridViewCheckBoxColumn.HeaderText = "Included";
            this.includedDataGridViewCheckBoxColumn.Name = "includedDataGridViewCheckBoxColumn";
            this.includedDataGridViewCheckBoxColumn.Width = 60;
            // 
            // costDataGridViewTextBoxColumn
            // 
            this.costDataGridViewTextBoxColumn.DataPropertyName = "Cost";
            this.costDataGridViewTextBoxColumn.HeaderText = "Cost";
            this.costDataGridViewTextBoxColumn.Name = "costDataGridViewTextBoxColumn";
            // 
            // flcsOptionalExtra
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(599, 517);
            this.Controls.Add(this.kryptonPanel);
            this.Name = "flcsOptionalExtra";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Optional Extra";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.flcsOptionalExtra_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel)).EndInit();
            this.kryptonPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup4.Panel)).EndInit();
            this.kryptonHeaderGroup4.Panel.ResumeLayout(false);
            this.kryptonHeaderGroup4.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup4)).EndInit();
            this.kryptonHeaderGroup4.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewExtras)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsOptionalcosting)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gulliverDS)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonManager kryptonManager;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel;
        private ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup kryptonHeaderGroup4;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private ComponentFactory.Krypton.Toolkit.KryptonDataGridView dataGridViewExtras;
        private System.Windows.Forms.BindingSource bsOptionalcosting;
        private System.Windows.Forms.DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewLinkColumn deleteDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn descriptionDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn includedDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn costDataGridViewTextBoxColumn;
        private GulliverDS gulliverDS;
    }
}

