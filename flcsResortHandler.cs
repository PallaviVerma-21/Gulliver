using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;

namespace GulliverII
{
    public partial class flcsResortHandler : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        public flcsResortHandler()
        {
            InitializeComponent();

            this.tabControl1.DrawMode = TabDrawMode.OwnerDrawFixed;
            this.tabControl1.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.tabControl1_DrawItem);           
        }

        private void tabControl1_DrawItem(object sender, DrawItemEventArgs e)
        {
            Font tabFont;
            Brush backBrush = new SolidBrush(System.Drawing.ColorTranslator.FromHtml("#FFFFFF")); //Set background color
            Brush foreBrush = new SolidBrush(System.Drawing.ColorTranslator.FromHtml("#004080"));//Set foreground color

            if (e.Index == this.tabControl1.SelectedIndex)
            {
                tabFont = new Font("Calibri", 14, FontStyle.Bold, GraphicsUnit.Pixel);
                backBrush = new SolidBrush(System.Drawing.ColorTranslator.FromHtml("#004080"));
                foreBrush = new SolidBrush(System.Drawing.ColorTranslator.FromHtml("#FFFFFF"));
            }
            else
                tabFont = new Font("Calibri", 12, FontStyle.Regular, GraphicsUnit.Pixel);

            string tabName = this.tabControl1.TabPages[e.Index].Text;
            this.tabControl1.TabPages[e.Index].Width = 200;
            this.tabControl1.TabPages[e.Index].Height = 100;
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            e.Graphics.FillRectangle(backBrush, e.Bounds);
            Rectangle r = e.Bounds;
            r = new Rectangle(r.X, r.Y + 3, r.Width, r.Height);
            e.Graphics.DrawString(tabName, tabFont, foreBrush, r, sf);
            //Dispose objects
            sf.Dispose();

            if (e.Index == this.tabControl1.SelectedIndex)
            {
                tabFont.Dispose();
                backBrush.Dispose();
            }
            else
            {
                backBrush.Dispose();
                foreBrush.Dispose();
            }
        }
            
        private void btnCheckTravelSmart_Click(object sender, EventArgs e)
        {

        }
        
        private void btnAddTravelSmart_Click(object sender, EventArgs e)
        {

        }       
    }
}