using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using ComponentFactory.Krypton.Toolkit;
using System.Diagnostics;
using System.Collections;

namespace Gulliver
{
    public partial class flcsLibrary : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        PackageGenerator.PackageHandler packageHandler;
      
        public flcsLibrary()
        {
           InitializeComponent();
           packageHandler = new PackageGenerator.PackageHandler();
           DisplayAllPackageOffers(true);                   
        }

        #region methods

        private void DisplayAllPackageOffers(bool first)
        {
            libraryProgressbar.PerformStep();
            List<GulliverLibrary.Deal> deals = packageHandler.GetFilteredPackageOffers(txtSearch.Text.Trim().ToUpper(), cbShowAll.Checked);

            if (first)
            {
              List<GulliverLibrary.Media> medias = deals.Select(p => p.Media).Distinct().ToList();
              FillSuppliers(medias);
            }

            if (cmbSuppliers.SelectedItem != null)
            {
               int supplierId = Convert.ToInt32(((ComboBoxItem)cmbSuppliers.SelectedItem).Value);
               
                if (supplierId != 0)
                    deals = deals.Where(p => p.Media.id == supplierId).ToList();
            }

            this.libraryDS.Library.Clear();
            libraryProgressbar.PerformStep();
           
            foreach (GulliverLibrary.Deal deal in deals.OrderBy(p => p.dateOfPromotion))
                this.libraryDS.Library.AddLibraryRow("Delete", "View", deal.id, deal.Media.supplier.Trim(), deal.name.Trim(), string.Empty , deal.dateOfPromotion.ToString("dd/MM/yyyy"), deal.endDateOfPromotion.ToString("dd/MM/yyyy"), "Copy");

            libraryProgressbar.PerformStep();
        }

        private void FillSuppliers(List<GulliverLibrary.Media> medias)
        {
            cmbSuppliers.Items.Clear();
            ComboBoxItem itemI = new ComboBoxItem();
            itemI.Text = "ALL";
            itemI.Value = "0";
            cmbSuppliers.Items.Add(itemI);

            foreach (GulliverLibrary.Media media in medias.OrderBy(s => s.supplier))
            {
              ComboBoxItem item = new ComboBoxItem();
              item.Text = media.supplier.Trim();
              item.Value = media.id;
              cmbSuppliers.Items.Add(item);
            }
        }

        private void StartProgressBar(int maximum)
        {
            libraryProgressbar.Visible = true;
            Application.DoEvents();
            libraryProgressbar.Maximum = maximum;
            libraryProgressbar.Value = 1;
        }

        private void StopProgressBar()
        {
            libraryProgressbar.Visible = false;
            Application.DoEvents();
        }

        #endregion

        #region events

        private void dataGridViewLibrary_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && dataGridViewLibrary.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null && dataGridViewLibrary.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "Delete")
            {
                switch (MessageBox.Show("This will delete selected offer - continue?", "Delete Offer", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    case System.Windows.Forms.DialogResult.Yes:

                        StartProgressBar(25);
                         if (packageHandler == null)
                            packageHandler = new PackageGenerator.PackageHandler();
                         packageHandler.DeleteDealById(Convert.ToInt32(dataGridViewLibrary.Rows[e.RowIndex].Cells[2].Value), libraryProgressbar);
                         DisplayAllPackageOffers(false);
                       StopProgressBar();
                        break;

                    case System.Windows.Forms.DialogResult.No:
                        return;
                }
            }
            else if (e.RowIndex != -1 && dataGridViewLibrary.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null && dataGridViewLibrary.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "View")
            {
                StartProgressBar(2);
                flcsMain flcsMain = new flcsMain(Convert.ToInt32(dataGridViewLibrary.Rows[e.RowIndex].Cells[2].Value));
                flcsMain.Show();
                StopProgressBar();

            }
            else if (e.RowIndex != -1 && dataGridViewLibrary.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null && dataGridViewLibrary.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "Copy")
            {
                switch (MessageBox.Show("This will copy selected offer - continue?", "Copy Offer", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    case System.Windows.Forms.DialogResult.Yes:

                        StartProgressBar(25);
                        if (packageHandler == null)
                            packageHandler = new PackageGenerator.PackageHandler();
                        packageHandler.CopyDeal(Convert.ToInt32(dataGridViewLibrary.Rows[e.RowIndex].Cells[2].Value), libraryProgressbar);
                        cbShowAll.Checked = true;
                        DisplayAllPackageOffers(false);
                       StopProgressBar();
                        break;

                    case System.Windows.Forms.DialogResult.No:
                        return;
                }
            }
        }
        
        private void cbShowAll_CheckedChanged(object sender, EventArgs e)
        {
           DisplayAllPackageOffers(false);
        }

        private void newOfferToolStripMenuItem_Click(object sender, EventArgs e)
        {
           StartProgressBar(2);
           flcsMain formMain = new flcsMain();
           StopProgressBar();
           formMain.ShowDialog();
        }

        private void healthSafetyDocumentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
              string folderPath = folderBrowserDialog.SelectedPath;
              System.IO.File.Copy(PackageGenerator.Tool.healthSafetyDoc, folderPath + @"\Health & Safety .pdf");
              MessageBox.Show("File has been saved to" + folderBrowserDialog.SelectedPath, "Health & Safety File", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void updateBestDealPageToolStripMenuItem_Click(object sender, EventArgs e)
        {
           PackageGenerator.Tool.RunBestDealPage();
           MessageBox.Show("Started running the script and it'll take few minutes to update fleetway home page!", "Best Deal Holidays", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void updateGermanBestDealPageToolStripMenuItem_Click(object sender, EventArgs e)
        {
           PackageGenerator.Tool.RunBestDealGermanPage();
           MessageBox.Show("Started running the script and it'll take few minutes to update fleetway german home page!", "German Best Deal Holidays", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void updateLHCDealToolStripMenuItem_Click(object sender, EventArgs e)
        {
           PackageGenerator.Tool.RunLuxuryScriptForUpdates("LHC");
           MessageBox.Show("Started updating LHC website, you'll receive the updated email in few minutes!", "LHC/ELB Data Import", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void updateELBDealsToolStripMenuItem_Click(object sender, EventArgs e)
        {
           PackageGenerator.Tool.RunLuxuryScriptForUpdates("ELB");
           MessageBox.Show("Started updating ELB website, you'll receive the updated email in few minutes!", "LHC/ELB Data Import", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void existToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void mediasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            flcsMedias mediaForm = new flcsMedias();
            mediaForm.ShowDialog();
        }

        private void baggagesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            flcsBaggages baggagesForm = new flcsBaggages();
            baggagesForm.ShowDialog();
        }

        private void currencyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            flcsCurrency currencyForm = new flcsCurrency();
            currencyForm.ShowDialog();
        }

        private void fABSettingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            flcsFABSetting fabSettingForm = new flcsFABSetting();
            fabSettingForm.ShowDialog();
        }

        #endregion       

      
    }
}