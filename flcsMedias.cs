using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
using System.Linq;

namespace Gulliver
{
    public partial class flcsMedias : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        private GulliverLibrary.QueryHandler gulliverQueryHandler;
        private List<GulliverLibrary.Media> medias;

        public flcsMedias()
        {
            gulliverQueryHandler = new GulliverLibrary.QueryHandler();
            InitializeComponent();
            FillMedias(txtSearchbox.Text.Trim());
            SetText(txtSearchbox, "Search media name here ...");
        }

        private void FillMedias(string searchText)
        {
            packagesDS.Media.Rows.Clear();
            medias = gulliverQueryHandler.GetAllMedias();

            if (searchText.Trim() != string.Empty && searchText != "Search media name here ...")
            medias = medias.Where(m => m.supplier.Trim().ToUpper().Contains(searchText.ToUpper().Trim())).ToList();

            foreach (GulliverLibrary.Media media in medias.OrderBy(m => m.supplier.Trim()))
                packagesDS.Media.AddMediaRow("Delete", media.id, media.supplier.Trim(), media.channelCode.Trim(), media.commission, ((media.pleaseNote != null)? media.pleaseNote.Trim(): string.Empty));            
        }

        private void SetText(TextBox txtSearchbox, string text)
        {
            txtSearchbox.Text = text;
            txtSearchbox.ForeColor = Color.Gray;
        }

        private void txtSearchbox_Enter(object sender, EventArgs e)
        {
            if (txtSearchbox.ForeColor != Color.Black)
            {
                txtSearchbox.Text = string.Empty;
                txtSearchbox.ForeColor = Color.Black;
            }
        }

        private void txtSearchbox_Leave(object sender, EventArgs e)
        {
            if (txtSearchbox.Text.Trim() == string.Empty)
                SetText(txtSearchbox, "Search media name here ...");
        }

        private void dataGridViewPackageBackup_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && dataGridViewMedias.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null && dataGridViewMedias.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "Delete")
            {
                switch (MessageBox.Show("This will delete selected media - continue?", "Delete Media", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    case System.Windows.Forms.DialogResult.Yes:
                        dataGridViewMedias.Rows.Remove((DataGridViewRow)dataGridViewMedias.Rows[e.RowIndex]);
                        gulliverQueryHandler.DeleteMediaById(Convert.ToInt32(dataGridViewMedias.Rows[e.RowIndex].Cells[1].Value));
                        break;

                    case System.Windows.Forms.DialogResult.No:
                        return;
                }
            }
        }

        private void txtSearchbox_TextChanged(object sender, EventArgs e)
        {
            dataGridViewMedias.Rows.Cast<DataGridViewRow>().ToList().ForEach(r => r.Visible = true);
            dataGridViewMedias.CurrentCell = (dataGridViewMedias.Rows.Count != 0) ? dataGridViewMedias.Rows[dataGridViewMedias.Rows.Count - 1].Cells[0] : null;
          
            if (txtSearchbox.Text.Trim() != string.Empty && txtSearchbox.Text != "Search media name here ...")
            {
                 List<DataGridViewRow> rows = dataGridViewMedias.Rows.Cast<DataGridViewRow>().Where(r => r.Cells[2].Value != null && !r.Cells[2].Value.ToString().Trim().ToUpper().Contains(txtSearchbox.Text.Trim().ToUpper())).ToList();

                foreach (DataGridViewRow row in rows)
                {
                    try
                    {
                        row.Visible = false;
                    }
                    catch { }
                }
            }

            //FillMedias(txtSearchbox.Text.Trim());
        }

        private void toolAddMedia_Click(object sender, EventArgs e)
        {
            this.packagesDS.Media.Rows.Add("Delete", 0, string.Empty, string.Empty, 0, string.Empty);
            dataGridViewMedias.Sort(dataGridViewMedias.Columns[1], ListSortDirection.Ascending);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
           List<GulliverLibrary.Media> medias = GetMedias();
           gulliverQueryHandler.SaveMedia(medias);
           this.Close();
        }

        private List<GulliverLibrary.Media> GetMedias()
        {
            List<GulliverLibrary.Media> medias = new List<GulliverLibrary.Media>();

            foreach (PackagesDS.MediaRow row in this.packagesDS.Media.Where(m => m.Name != null))
            {
                GulliverLibrary.Media media = new GulliverLibrary.Media();
                media.id = row.id;
                media.supplier = row.Name.Trim();
                media.channelCode = row.Channel_Codes.Trim();
                media.commission = row.Commission;
                media.pleaseNote = row.Please_Note;
                medias.Add(media);
            }

            return medias;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            switch (KryptonMessageBox.Show("If you have made any changes, this wont save - continue?", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                case System.Windows.Forms.DialogResult.Yes:
                    this.Close();
                    break;

                case System.Windows.Forms.DialogResult.No:
                    return;
            }
        }
        
       
    }
}