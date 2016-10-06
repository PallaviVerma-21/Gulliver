namespace GulliverII
{
    partial class flcsOtherChannels
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(flcsOtherChannels));
            this.kryptonManager = new ComponentFactory.Krypton.Toolkit.KryptonManager(this.components);
            this.kryptonPanel = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.headerUpdateDays = new ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup();
            this.buttonSpecHeaderGroup1 = new ComponentFactory.Krypton.Toolkit.ButtonSpecHeaderGroup();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnSave = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.cbChannels = new System.Windows.Forms.CheckedListBox();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel)).BeginInit();
            this.kryptonPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.headerUpdateDays)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.headerUpdateDays.Panel)).BeginInit();
            this.headerUpdateDays.Panel.SuspendLayout();
            this.headerUpdateDays.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // kryptonPanel
            // 
            this.kryptonPanel.Controls.Add(this.headerUpdateDays);
            this.kryptonPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel.Name = "kryptonPanel";
            this.kryptonPanel.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.ProfessionalSystem;
            this.kryptonPanel.Size = new System.Drawing.Size(492, 318);
            this.kryptonPanel.TabIndex = 0;
            // 
            // headerUpdateDays
            // 
            this.headerUpdateDays.AutoSize = true;
            this.headerUpdateDays.ButtonSpecs.AddRange(new ComponentFactory.Krypton.Toolkit.ButtonSpecHeaderGroup[] {
            this.buttonSpecHeaderGroup1});
            this.headerUpdateDays.Dock = System.Windows.Forms.DockStyle.Fill;
            this.headerUpdateDays.GroupBackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.PanelAlternate;
            this.headerUpdateDays.HeaderVisibleSecondary = false;
            this.headerUpdateDays.Location = new System.Drawing.Point(0, 0);
            this.headerUpdateDays.Name = "headerUpdateDays";
            this.headerUpdateDays.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.ProfessionalSystem;
            // 
            // headerUpdateDays.Panel
            // 
            this.headerUpdateDays.Panel.Controls.Add(this.tableLayoutPanel1);
            this.headerUpdateDays.Size = new System.Drawing.Size(492, 318);
            this.headerUpdateDays.StateCommon.HeaderPrimary.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(218)))), ((int)(((byte)(218)))));
            this.headerUpdateDays.StateCommon.HeaderPrimary.Back.Color2 = System.Drawing.Color.White;
            this.headerUpdateDays.StateCommon.HeaderPrimary.Content.ShortText.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(102)))));
            this.headerUpdateDays.TabIndex = 2;
            this.headerUpdateDays.ValuesPrimary.Heading = "Other Channels";
            this.headerUpdateDays.ValuesPrimary.Image = null;
            // 
            // buttonSpecHeaderGroup1
            // 
            this.buttonSpecHeaderGroup1.Type = ComponentFactory.Krypton.Toolkit.PaletteButtonSpecStyle.ArrowUp;
            this.buttonSpecHeaderGroup1.UniqueName = "9072EDD1BE8D4DB54D81A632648D793B";
            this.buttonSpecHeaderGroup1.Visible = false;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.cbChannels, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 85F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(490, 288);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btnSave);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 247);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(484, 38);
            this.flowLayoutPanel1.TabIndex = 24;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(365, 3);
            this.btnSave.Margin = new System.Windows.Forms.Padding(365, 3, 3, 3);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(111, 25);
            this.btnSave.StateCommon.Back.Color1 = System.Drawing.Color.Moccasin;
            this.btnSave.StateCommon.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnSave.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.White;
            this.btnSave.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.StatePressed.Back.Color1 = System.Drawing.Color.Gold;
            this.btnSave.StatePressed.Back.Color2 = System.Drawing.Color.Gold;
            this.btnSave.StatePressed.Content.ShortText.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(102)))));
            this.btnSave.TabIndex = 22;
            this.btnSave.Values.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // cbChannels
            // 
            this.cbChannels.CheckOnClick = true;
            this.cbChannels.ColumnWidth = 155;
            this.cbChannels.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbChannels.FormattingEnabled = true;
            this.cbChannels.Location = new System.Drawing.Point(3, 3);
            this.cbChannels.MultiColumn = true;
            this.cbChannels.Name = "cbChannels";
            this.cbChannels.Size = new System.Drawing.Size(484, 238);
            this.cbChannels.TabIndex = 25;
            // 
            // flcsOtherChannels
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(492, 318);
            this.Controls.Add(this.kryptonPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "flcsOtherChannels";
            this.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.ProfessionalSystem;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel)).EndInit();
            this.kryptonPanel.ResumeLayout(false);
            this.kryptonPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.headerUpdateDays.Panel)).EndInit();
            this.headerUpdateDays.Panel.ResumeLayout(false);
            this.headerUpdateDays.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.headerUpdateDays)).EndInit();
            this.headerUpdateDays.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonManager kryptonManager;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel;
        private ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup headerUpdateDays;
        private ComponentFactory.Krypton.Toolkit.ButtonSpecHeaderGroup buttonSpecHeaderGroup1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnSave;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.CheckedListBox cbChannels;
    }
}

