using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
using System.Collections;

namespace GulliverII
{
    public partial class flcsFilterSearch : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        public List<int> selectedDurations;
        public List<string> selectedDepartureAirports;
        public DateTime startDate;
        public DateTime endDate;
        public bool returnToMainwindow = false;
        public bool pressok = false;

        public flcsFilterSearch(List<string> durations, List<string> depUKAirportsList, List<string> depGermanAirportsList, List<string> depUSAAirportsList, List<string> departureAirports, List<string> depCanadianAirportsList, DateTime startDate, DateTime endDate)
        {
            InitializeComponent();
            FillDurations(durations);
            FillDepAirports(depUKAirportsList, depGermanAirportsList, depUSAAirportsList,depCanadianAirportsList, departureAirports);
            dtpStartDate.Value = startDate;
            dtpEndDate.Value = endDate;
        }

        private void FillDurations(List<string> durations)
        {
            for (int i = 0; i < cbDurations.Items.Count; i++)
            {
                if (durations.Contains(cbDurations.Items[i].ToString()))
                    cbDurations.SetItemChecked(i, true);
            }
        }

        private void FillDepAirports(List<string> depUKAirportsList, List<string> depGermanAirportsList, List<string> depUSAAirportsList, List<string> depCanadianAirportsList, List<string> departureSelectedAirports)
        {
            List<string> specialAirports = new List<string>() { "BHX", "BRS", "EDI", "EMA", "GLA", "LGW", "LPL", "LTN", "MAN", "NCL", "SEN", "STN" };

            foreach (string code in depUKAirportsList)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Text = code.Trim();
                item.Value = code.Trim();
                
                if(departureSelectedAirports.Contains(code.Trim()))
                cbDepartureAirports.Items.Add(item,true);
                else
                cbDepartureAirports.Items.Add(item, false);
            }

            foreach (string code in depGermanAirportsList)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Text = code.Trim();
                item.Value = code.Trim();

                if (departureSelectedAirports.Contains(code.Trim()))
                    cbGermanAirports.Items.Add(item, true);
                else
                    cbGermanAirports.Items.Add(item, false);
            }


            foreach (string code in depUSAAirportsList)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Text = code.Trim();
                item.Value = code.Trim();
                if (departureSelectedAirports.Contains(code.Trim()))
                    cbUSAirports.Items.Add(item, true);
                else
                    cbUSAirports.Items.Add(item, false);
            }


            foreach (string code in depCanadianAirportsList)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Text = code.Trim();
                item.Value = code.Trim();
                if (departureSelectedAirports.Contains(code.Trim()))
                    cbCanadianAirports.Items.Add(item, true);
                else
                    cbCanadianAirports.Items.Add(item, false);
            }
        }

        private void Search()
        {
            selectedDurations = new List<int>();
            foreach (string duration in cbDurations.CheckedItems)
                selectedDurations.Add(Convert.ToInt32(duration));

            selectedDepartureAirports = new List<string>();
            foreach (ComboBoxItem departure in cbDepartureAirports.CheckedItems)
                selectedDepartureAirports.Add(departure.Value.ToString());
            foreach (ComboBoxItem departure in cbGermanAirports.CheckedItems)
                selectedDepartureAirports.Add(departure.Value.ToString());
            foreach (ComboBoxItem departure in cbUSAirports.CheckedItems)
                selectedDepartureAirports.Add(departure.Value.ToString());
            foreach (ComboBoxItem departure in cbCanadianAirports.CheckedItems)
                selectedDepartureAirports.Add(departure.Value.ToString());

            startDate = dtpStartDate.Value;
            endDate = dtpEndDate.Value;

            pressok = true;
        }

        private void flcsFilterSearch_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!pressok)
                returnToMainwindow = true;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
           Search();
           this.Close();
        }

        private void cbAllCanadians_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i <= cbCanadianAirports.Items.Count - 1; i++)
                cbCanadianAirports.SetItemCheckState(i, (cbAllCanadians.Checked ? CheckState.Checked : CheckState.Unchecked));
        }

        private void cbAllUS_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i <= cbUSAirports.Items.Count - 1; i++)
                cbUSAirports.SetItemCheckState(i, (cbAllUS.Checked ? CheckState.Checked : CheckState.Unchecked));
        }

        private void cbAllGerman_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i <= cbGermanAirports.Items.Count - 1; i++)
                cbGermanAirports.SetItemCheckState(i, (cbAllGerman.Checked ? CheckState.Checked : CheckState.Unchecked));
        }

        private void cbAllAirports_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i <= cbDepartureAirports.Items.Count - 1; i++)
                cbDepartureAirports.SetItemCheckState(i, (cbAllAirports.Checked ? CheckState.Checked : CheckState.Unchecked));
        }        
       
    }
}