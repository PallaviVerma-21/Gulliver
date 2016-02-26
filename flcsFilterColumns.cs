using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;

namespace Gulliver
{
    public partial class flcsFilterColumns : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        public List<string> visibleColumns;
        
        public flcsFilterColumns(List<string> columns, List<string> visibleColumns)
        {
            InitializeComponent();
            this.visibleColumns = visibleColumns;
            FillColumns(columns);
        }

        public void FillColumns(List<string> columns)
        {
            columns.Remove("id");
            columns.Remove("Delete");
            columns.Remove("flightId");
            columns.Remove("hiddenNumber");

            foreach (string column in columns)
            {
                if (visibleColumns.Contains(column))
                    cbColumns.Items.Add(column, true);
                else
                    cbColumns.Items.Add(column, false);
            }
        }

        private void flcsFilterColumns_FormClosing(object sender, FormClosingEventArgs e)
        {
            visibleColumns = new List<string>();

            foreach (string item in cbColumns.CheckedItems)
                visibleColumns.Add(item.Trim());
        }

        
    }
}