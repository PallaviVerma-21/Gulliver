namespace Gulliver
{
    partial class flcsZoomOut
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
            this.txtText = new System.Windows.Forms.TextBox();
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
            this.kryptonPanel.Size = new System.Drawing.Size(940, 796);
            this.kryptonPanel.TabIndex = 0;
            // 
            // kryptonHeaderGroup7
            // 
            this.kryptonHeaderGroup7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonHeaderGroup7.GroupBackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.PanelAlternate;
            this.kryptonHeaderGroup7.HeaderVisibleSecondary = false;
            this.kryptonHeaderGroup7.Location = new System.Drawing.Point(0, 0);
            this.kryptonHeaderGroup7.Name = "kryptonHeaderGroup7";
            // 
            // kryptonHeaderGroup7.Panel
            // 
            this.kryptonHeaderGroup7.Panel.Controls.Add(this.txtText);
            this.kryptonHeaderGroup7.Size = new System.Drawing.Size(940, 796);
            this.kryptonHeaderGroup7.TabIndex = 19;
            this.kryptonHeaderGroup7.ValuesPrimary.Heading = "Title";
            this.kryptonHeaderGroup7.ValuesPrimary.Image = null;
            // 
            // txtText
            // 
            this.txtText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtText.Location = new System.Drawing.Point(0, 0);
            this.txtText.Multiline = true;
            this.txtText.Name = "txtText";
            this.txtText.Size = new System.Drawing.Size(938, 764);
            this.txtText.TabIndex = 0;
            // 
            // flcsZoomOut
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(940, 796);
            this.Controls.Add(this.kryptonPanel);
            this.Name = "flcsZoomOut";
            this.Text = "Zoom Out";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.flcsZoomOut_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel)).EndInit();
            this.kryptonPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup7.Panel)).EndInit();
            this.kryptonHeaderGroup7.Panel.ResumeLayout(false);
            this.kryptonHeaderGroup7.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup7)).EndInit();
            this.kryptonHeaderGroup7.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonManager kryptonManager;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel;
        private ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup kryptonHeaderGroup7;
        private System.Windows.Forms.TextBox txtText;
    }
}

