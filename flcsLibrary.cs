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
using System.Threading;
using DataGridViewAutoFilter;

namespace GulliverII
{
    public partial class flcsLibrary : ComponentFactory.Krypton.Toolkit.KryptonForm, IDisposable
    {
        PackageGenerator.PackageHandler packageHandler;
      
        public flcsLibrary()
        {
          InitializeComponent();
          packageHandler = new PackageGenerator.PackageHandler(false);
          DisplayAllPackageOffers(true);                   
        }

        #region methods

        private void DisplayAllPackageOffers(bool first)
        {
            libraryProgressbar.PerformStep();
            packageHandler = new PackageGenerator.PackageHandler(true);
            List<GulliverLibrary.Deal> deals = packageHandler.GetFilteredPackageOffers(txtSearch.Text.Trim().ToUpper(), cbShowAll.Checked);

            if (cbOnlyBestDeals.Checked)
                deals = deals.Where(d => d.DealInformation != null && d.DealInformation.goLiveOnBestDealPage).ToList();

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

            foreach (GulliverLibrary.Deal deal in deals.OrderByDescending(p => p.dateOfPromotion))
                this.libraryDS.Library.AddLibraryRow("Delete", "View", "Update", deal.id, deal.Media.supplier.Trim(), deal.name.Trim(), string.Empty, deal.dateOfPromotion.ToString("dd/MM/yyyy"), deal.endDateOfPromotion.ToString("dd/MM/yyyy"), "Copy");

            libraryProgressbar.PerformStep();
            packageHandler.Dispose();
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
                            packageHandler = new PackageGenerator.PackageHandler(false);

                         packageHandler.DeleteDealById(Convert.ToInt32(dataGridViewLibrary.Rows[e.RowIndex].Cells[2].Value), libraryProgressbar);
                         packageHandler.Dispose();
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
                //ProgressBar();
                flcsMain flcsMain = new flcsMain(Convert.ToInt32(dataGridViewLibrary.Rows[e.RowIndex].Cells[2].Value));
                flcsMain.ShowDialog();
                flcsMain.Dispose();
                DisplayAllPackageOffers(false);
                StopProgressBar();

            }
            else if (e.RowIndex != -1 && dataGridViewLibrary.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null && dataGridViewLibrary.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "Update")
            {
                StartProgressBar(2);
                using( flcsDealPage flcsDealPage = new flcsDealPage(Convert.ToInt32(dataGridViewLibrary.Rows[e.RowIndex].Cells[2].Value))) flcsDealPage.ShowDialog();
                StopProgressBar();

            }
            else if (e.RowIndex != -1 && dataGridViewLibrary.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null && dataGridViewLibrary.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "Copy")
            {
                switch (MessageBox.Show("This will copy selected offer - continue?", "Copy Offer", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    case System.Windows.Forms.DialogResult.Yes:

                        StartProgressBar(25);
                        if (packageHandler == null)
                            packageHandler = new PackageGenerator.PackageHandler(false);
                        packageHandler.CopyDeal(Convert.ToInt32(dataGridViewLibrary.Rows[e.RowIndex].Cells[2].Value), libraryProgressbar);
                        packageHandler.Dispose();
                        cbShowAll.Checked = true;
                        DisplayAllPackageOffers(false);
                       StopProgressBar();
                        break;

                    case System.Windows.Forms.DialogResult.No:
                        return;
                }
            }
          
        }

        private void ProgressBar()
        {
            try
            {
                int max = 100;
                var prog = Microsoft.WindowsAPICodePack.Taskbar.TaskbarManager.Instance;
                prog.SetProgressState(Microsoft.WindowsAPICodePack.Taskbar.TaskbarProgressBarState.Normal);
                for (int i = 0; i < max; i++)
                {
                    prog.SetProgressValue(i, max);
                    Thread.Sleep(100);
                }
                prog.SetProgressState(Microsoft.WindowsAPICodePack.Taskbar.TaskbarProgressBarState.NoProgress);
            }
            catch (Exception ex){ }
        }
        
        private void cbShowAll_CheckedChanged(object sender, EventArgs e)
        {
           DisplayAllPackageOffers(true);           
        }

        private void newOfferToolStripMenuItem_Click(object sender, EventArgs e)
        {
           StartProgressBar(2);
           using (flcsMain formMain = new flcsMain())
           {
               StopProgressBar();
               formMain.ShowDialog();
           }
           DisplayAllPackageOffers(false);
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

        private void mediasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using(flcsMedias mediaForm = new flcsMedias()) mediaForm.ShowDialog();
        }

        private void baggagesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using(flcsBaggages baggagesForm = new flcsBaggages())baggagesForm.ShowDialog();
        }

        private void currencyToolStripMenuItem_Click(object sender, EventArgs e)
        {
           using(flcsCurrency currencyForm = new flcsCurrency()) currencyForm.ShowDialog();
        }

        private void fABSettingToolStripMenuItem_Click(object sender, EventArgs e)
        {
           using(flcsFABSetting fabSettingForm = new flcsFABSetting()) fabSettingForm.ShowDialog();
        }

        private void showAllLabel_Click(object sender, EventArgs e)
        {
           DataGridViewAutoFilterTextBoxColumn.RemoveFilter(dataGridViewLibrary);
        }

        private void dataGridViewLibrary_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            String filterStatus = DataGridViewAutoFilterColumnHeaderCell
           .GetFilterStatus(dataGridViewLibrary);
            if (String.IsNullOrEmpty(filterStatus))
            {
                showAllLabel.Visible = false;
                fiterStatusLabel.Visible = false;
            }
            else
            {
                showAllLabel.Visible = true;
                fiterStatusLabel.Visible = true;
                fiterStatusLabel.Text = filterStatus;
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
          DisplayAllPackageOffers(false);
        }

        private void cmbSuppliers_SelectedIndexChanged(object sender, EventArgs e)
        {
          DisplayAllPackageOffers(false);
        }

        #endregion    

        public void Dispose()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }

        ~flcsLibrary()
        {
          Dispose();
        }

        private void dailyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            libraryProgressbar.Visible = true;
            Application.DoEvents();
            libraryProgressbar.Maximum = 10;
            libraryProgressbar.Value = 1;
            libraryProgressbar.Step = 1;
            libraryProgressbar.Value++;
            Application.DoEvents();
            int count = 0;
           
            while (count <= 3)
            {
               System.Threading.Thread.Sleep(500);
               count++;
               libraryProgressbar.Value++;
            }

            DailyMailReportHandler.Generator generator = new DailyMailReportHandler.Generator();
            generator.ExportAllDailyMailDepartures();
            libraryProgressbar.Value++;
            libraryProgressbar.Value++;
            Application.DoEvents();
            libraryProgressbar.Value = libraryProgressbar.Maximum;
            libraryProgressbar.Step = libraryProgressbar.Maximum;
            System.Threading.Thread.Sleep(500);
            libraryProgressbar.Visible = false;
            Application.DoEvents();
            KryptonMessageBox.Show("File is successfully uploaded to the server!", "Daily Mail Departures", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void cbOnlyBestDeals_CheckedChanged(object sender, EventArgs e)
        {
          DisplayAllPackageOffers(false);
        }

        private void thirdpartyHotelManagerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            flcsThirdpartyHotelRequests hotelRequestHandler = new flcsThirdpartyHotelRequests();
            hotelRequestHandler.Show();
        }

        private void deleteDealToolStripMenuItem_Click(object sender, EventArgs e)
        {
            switch (MessageBox.Show("This will delete selected offer - continue?", "Delete Offer", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                case System.Windows.Forms.DialogResult.Yes:

                    StartProgressBar(25);
                    if (packageHandler == null)
                        packageHandler = new PackageGenerator.PackageHandler(false);

                    packageHandler.DeleteDealById(Convert.ToInt32(dataGridViewLibrary.SelectedRows[0].Cells[2].Value), libraryProgressbar);
                    packageHandler.Dispose();
                    DisplayAllPackageOffers(false);
                    StopProgressBar();
                    break;

                case System.Windows.Forms.DialogResult.No:
                    return;
            }
        }

        private void dataGridViewLibrary_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            if (e.StateChanged == DataGridViewElementStates.Selected)
            {
                if (dataGridViewLibrary.SelectedCells.Count > 0 && dataGridViewLibrary.SelectedRows.Count == 1)
                {
                    dataGridViewLibrary.ContextMenuStrip = cms;
                }
                else
                    dataGridViewLibrary.ContextMenuStrip = null;
            }
            else
                dataGridViewLibrary.ContextMenuStrip = null;
        }     

        
    }
}