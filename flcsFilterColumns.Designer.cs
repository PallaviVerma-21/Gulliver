namespace GulliverII
{
    partial class flcsFilterColumns
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(flcsFilterColumns));
            this.kryptonManager = new ComponentFactory.Krypton.Toolkit.KryptonManager(this.components);
            this.kryptonPanel = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.kryptonHeaderGroup7 = new ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup();
            this.cbColumns = new System.Windows.Forms.CheckedListBox();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel)).BeginInit();
            this.kryptonPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup7.Panel)).BeginInit();
            this.kryptonHeaderGroup7.Panel.SuspendLayout();
            this.kryptonHeaderGroup7.SuspendLayout();
            this.SuspendLayout();
            // 
            // kryptonPanel
            // 
            this.kryptonPanel.Controls.Add(this.kryptonHeaderGroup7);
            this.kryptonPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel.Name = "kryptonPanel";
            this.kryptonPanel.Size = new System.Drawing.Size(422, 416);
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
            this.kryptonHeaderGroup7.Panel.Controls.Add(this.cbColumns);
            this.kryptonHeaderGroup7.Size = new System.Drawing.Size(422, 416);
            this.kryptonHeaderGroup7.StateCommon.HeaderPrimary.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(218)))), ((int)(((byte)(218)))));
            this.kryptonHeaderGroup7.StateCommon.HeaderPrimary.Back.Color2 = System.Drawing.Color.White;
            this.kryptonHeaderGroup7.StateCommon.HeaderPrimary.Content.ShortText.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(102)))));
            this.kryptonHeaderGroup7.TabIndex = 18;
            this.kryptonHeaderGroup7.ValuesPrimary.Heading = "Filter Columns";
            this.kryptonHeaderGroup7.ValuesPrimary.Image = null;
            // 
            // cbColumns
            // 
            this.cbColumns.CheckOnClick = true;
            this.cbColumns.ColumnWidth = 200;
            this.cbColumns.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbColumns.FormattingEnabled = true;
            this.cbColumns.Location = new System.Drawing.Point(0, 0);
            this.cbColumns.MultiColumn = true;
            this.cbColumns.Name = "cbColumns";
            this.cbColumns.Size = new System.Drawing.Size(420, 386);
            this.cbColumns.TabIndex = 11;
            // 
            // flcsFilterColumns
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(422, 416);
            this.Controls.Add(this.kryptonPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "flcsFilterColumns";
            this.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.ProfessionalSystem;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.flcsFilterColumns_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel)).EndInit();
            this.kryptonPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup7.Panel)).EndInit();
            this.kryptonHeaderGroup7.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup7)).EndInit();
            this.kryptonHeaderGroup7.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonManager kryptonManager;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel;
        private ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup kryptonHeaderGroup7;
        private System.Windows.Forms.CheckedListBox cbColumns;
    }
}

