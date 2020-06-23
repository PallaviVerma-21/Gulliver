namespace GulliverII
{
    partial class flcsTransferExcursionInfo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(flcsTransferExcursionInfo));
            this.kryptonManager = new ComponentFactory.Krypton.Toolkit.KryptonManager(this.components);
            this.kryptonPanel = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.headerUpdateDays = new ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup();
            this.buttonSpecHeaderGroup1 = new ComponentFactory.Krypton.Toolkit.ButtonSpecHeaderGroup();
            this.kryptonLabel5 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel4 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.flowLayoutPanel4 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnSave = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.ddlExtraTypes = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.kryptonLabel1 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel3 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel2 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.txtDescription = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.txtTitle = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel6 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel7 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.txtTitle1 = new System.Windows.Forms.TextBox();
            this.txtDescription1 = new System.Windows.Forms.TextBox();
            this.txtObJourneyTime = new System.Windows.Forms.TextBox();
            this.txtObJourneyDistance = new System.Windows.Forms.TextBox();
            this.txtMaximumCapacity = new System.Windows.Forms.TextBox();
            this.txtEstimatesStops = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel)).BeginInit();
            this.kryptonPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.headerUpdateDays)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.headerUpdateDays.Panel)).BeginInit();
            this.headerUpdateDays.Panel.SuspendLayout();
            this.headerUpdateDays.SuspendLayout();
            this.flowLayoutPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ddlExtraTypes)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // kryptonPanel
            // 
            this.kryptonPanel.Controls.Add(this.headerUpdateDays);
            this.kryptonPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel.Name = "kryptonPanel";
            this.kryptonPanel.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.ProfessionalSystem;
            this.kryptonPanel.Size = new System.Drawing.Size(588, 353);
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
            this.headerUpdateDays.Size = new System.Drawing.Size(588, 353);
            this.headerUpdateDays.StateCommon.HeaderPrimary.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(218)))), ((int)(((byte)(218)))));
            this.headerUpdateDays.StateCommon.HeaderPrimary.Back.Color2 = System.Drawing.Color.White;
            this.headerUpdateDays.StateCommon.HeaderPrimary.Content.ShortText.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(102)))));
            this.headerUpdateDays.TabIndex = 3;
            this.headerUpdateDays.ValuesPrimary.Heading = "Transfer & Excursion Description";
            this.headerUpdateDays.ValuesPrimary.Image = null;
            // 
            // buttonSpecHeaderGroup1
            // 
            this.buttonSpecHeaderGroup1.Type = ComponentFactory.Krypton.Toolkit.PaletteButtonSpecStyle.ArrowUp;
            this.buttonSpecHeaderGroup1.UniqueName = "9072EDD1BE8D4DB54D81A632648D793B";
            this.buttonSpecHeaderGroup1.Visible = false;
            // 
            // kryptonLabel5
            // 
            this.kryptonLabel5.Location = new System.Drawing.Point(3, 199);
            this.kryptonLabel5.Name = "kryptonLabel5";
            this.kryptonLabel5.Size = new System.Drawing.Size(169, 20);
            this.kryptonLabel5.TabIndex = 45;
            this.kryptonLabel5.Values.Text = "Outbound Journey Distance :";
            // 
            // kryptonLabel4
            // 
            this.kryptonLabel4.Location = new System.Drawing.Point(3, 168);
            this.kryptonLabel4.Name = "kryptonLabel4";
            this.kryptonLabel4.Size = new System.Drawing.Size(149, 20);
            this.kryptonLabel4.TabIndex = 44;
            this.kryptonLabel4.Values.Text = "Outbound Journey Time :";
            // 
            // flowLayoutPanel4
            // 
            this.flowLayoutPanel4.Controls.Add(this.btnSave);
            this.flowLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel4.Location = new System.Drawing.Point(185, 286);
            this.flowLayoutPanel4.Name = "flowLayoutPanel4";
            this.flowLayoutPanel4.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.flowLayoutPanel4.Size = new System.Drawing.Size(398, 34);
            this.flowLayoutPanel4.TabIndex = 43;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(275, 3);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(120, 28);
            this.btnSave.StateCommon.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(102)))));
            this.btnSave.StateCommon.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(102)))));
            this.btnSave.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.White;
            this.btnSave.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.StatePressed.Back.Color1 = System.Drawing.Color.Gold;
            this.btnSave.StatePressed.Back.Color2 = System.Drawing.Color.Gold;
            this.btnSave.StatePressed.Content.ShortText.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(102)))));
            this.btnSave.TabIndex = 22;
            this.btnSave.Values.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // ddlExtraTypes
            // 
            this.ddlExtraTypes.DropDownWidth = 121;
            this.ddlExtraTypes.Items.AddRange(new object[] {
            "Transfer",
            "Excursion"});
            this.ddlExtraTypes.Location = new System.Drawing.Point(185, 3);
            this.ddlExtraTypes.Name = "ddlExtraTypes";
            this.ddlExtraTypes.Size = new System.Drawing.Size(182, 21);
            this.ddlExtraTypes.TabIndex = 25;
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.Location = new System.Drawing.Point(3, 62);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(79, 20);
            this.kryptonLabel1.TabIndex = 27;
            this.kryptonLabel1.Values.Text = "Description :";
            // 
            // kryptonLabel3
            // 
            this.kryptonLabel3.Location = new System.Drawing.Point(3, 33);
            this.kryptonLabel3.Name = "kryptonLabel3";
            this.kryptonLabel3.Size = new System.Drawing.Size(40, 20);
            this.kryptonLabel3.TabIndex = 24;
            this.kryptonLabel3.Values.Text = "Title :";
            // 
            // kryptonLabel2
            // 
            this.kryptonLabel2.Location = new System.Drawing.Point(3, 3);
            this.kryptonLabel2.Name = "kryptonLabel2";
            this.kryptonLabel2.Size = new System.Drawing.Size(43, 20);
            this.kryptonLabel2.TabIndex = 23;
            this.kryptonLabel2.Values.Text = "Type :";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 31.22867F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 68.77133F));
            this.tableLayoutPanel1.Controls.Add(this.kryptonLabel2, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.kryptonLabel3, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.kryptonLabel1, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.ddlExtraTypes, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel4, 1, 11);
            this.tableLayoutPanel1.Controls.Add(this.kryptonLabel4, 0, 7);
            this.tableLayoutPanel1.Controls.Add(this.kryptonLabel5, 0, 8);
            this.tableLayoutPanel1.Controls.Add(this.kryptonLabel6, 0, 9);
            this.tableLayoutPanel1.Controls.Add(this.kryptonLabel7, 0, 10);
            this.tableLayoutPanel1.Controls.Add(this.txtTitle1, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.txtDescription1, 1, 6);
            this.tableLayoutPanel1.Controls.Add(this.txtObJourneyTime, 1, 7);
            this.tableLayoutPanel1.Controls.Add(this.txtObJourneyDistance, 1, 8);
            this.tableLayoutPanel1.Controls.Add(this.txtMaximumCapacity, 1, 9);
            this.tableLayoutPanel1.Controls.Add(this.txtEstimatesStops, 1, 10);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 12;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 106F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 31F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 12F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(586, 323);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // txtDescription
            // 
            this.txtDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDescription.Location = new System.Drawing.Point(92, 62);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(491, 100);
            this.txtDescription.TabIndex = 29;
            // 
            // txtTitle
            // 
            this.txtTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtTitle.Location = new System.Drawing.Point(92, 33);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(491, 17);
            this.txtTitle.TabIndex = 28;
            // 
            // kryptonLabel6
            // 
            this.kryptonLabel6.Location = new System.Drawing.Point(3, 229);
            this.kryptonLabel6.Name = "kryptonLabel6";
            this.kryptonLabel6.Size = new System.Drawing.Size(119, 20);
            this.kryptonLabel6.TabIndex = 46;
            this.kryptonLabel6.Values.Text = "Maximum capacity :";
            this.kryptonLabel6.Paint += new System.Windows.Forms.PaintEventHandler(this.kryptonLabel6_Paint);
            // 
            // kryptonLabel7
            // 
            this.kryptonLabel7.Location = new System.Drawing.Point(3, 258);
            this.kryptonLabel7.Name = "kryptonLabel7";
            this.kryptonLabel7.Size = new System.Drawing.Size(103, 20);
            this.kryptonLabel7.TabIndex = 47;
            this.kryptonLabel7.Values.Text = "Estimated stops :";
            // 
            // txtTitle1
            // 
            this.txtTitle1.Location = new System.Drawing.Point(185, 33);
            this.txtTitle1.Name = "txtTitle1";
            this.txtTitle1.Size = new System.Drawing.Size(397, 20);
            this.txtTitle1.TabIndex = 48;
            // 
            // txtDescription1
            // 
            this.txtDescription1.Location = new System.Drawing.Point(185, 62);
            this.txtDescription1.Multiline = true;
            this.txtDescription1.Name = "txtDescription1";
            this.txtDescription1.Size = new System.Drawing.Size(397, 100);
            this.txtDescription1.TabIndex = 49;
            // 
            // txtObJourneyTime
            // 
            this.txtObJourneyTime.Location = new System.Drawing.Point(185, 168);
            this.txtObJourneyTime.Name = "txtObJourneyTime";
            this.txtObJourneyTime.Size = new System.Drawing.Size(397, 20);
            this.txtObJourneyTime.TabIndex = 50;
            // 
            // txtObJourneyDistance
            // 
            this.txtObJourneyDistance.Location = new System.Drawing.Point(185, 199);
            this.txtObJourneyDistance.Name = "txtObJourneyDistance";
            this.txtObJourneyDistance.Size = new System.Drawing.Size(397, 20);
            this.txtObJourneyDistance.TabIndex = 51;
            // 
            // txtMaximumCapacity
            // 
            this.txtMaximumCapacity.Location = new System.Drawing.Point(185, 229);
            this.txtMaximumCapacity.Name = "txtMaximumCapacity";
            this.txtMaximumCapacity.Size = new System.Drawing.Size(397, 20);
            this.txtMaximumCapacity.TabIndex = 52;
            // 
            // txtEstimatesStops
            // 
            this.txtEstimatesStops.Location = new System.Drawing.Point(185, 258);
            this.txtEstimatesStops.Name = "txtEstimatesStops";
            this.txtEstimatesStops.Size = new System.Drawing.Size(397, 20);
            this.txtEstimatesStops.TabIndex = 53;
            // 
            // flcsTransferExcursionInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(588, 353);
            this.Controls.Add(this.kryptonPanel);
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "flcsTransferExcursionInfo";
            this.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.ProfessionalSystem;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel)).EndInit();
            this.kryptonPanel.ResumeLayout(false);
            this.kryptonPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.headerUpdateDays.Panel)).EndInit();
            this.headerUpdateDays.Panel.ResumeLayout(false);
            this.headerUpdateDays.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.headerUpdateDays)).EndInit();
            this.headerUpdateDays.ResumeLayout(false);
            this.flowLayoutPanel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ddlExtraTypes)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonManager kryptonManager;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel;
        private ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup headerUpdateDays;
        private ComponentFactory.Krypton.Toolkit.ButtonSpecHeaderGroup buttonSpecHeaderGroup1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel2;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel3;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel1;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox ddlExtraTypes;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel4;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnSave;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel4;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel5;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel6;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel7;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtDescription;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtTitle;
        private System.Windows.Forms.TextBox txtTitle1;
        private System.Windows.Forms.TextBox txtDescription1;
        private System.Windows.Forms.TextBox txtObJourneyTime;
        private System.Windows.Forms.TextBox txtObJourneyDistance;
        private System.Windows.Forms.TextBox txtMaximumCapacity;
        private System.Windows.Forms.TextBox txtEstimatesStops;
    }
}

