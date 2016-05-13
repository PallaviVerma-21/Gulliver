using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
using System.Linq;

namespace GulliverII
{
    public partial class flcsAutoUpdateSetting : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        private PackageGenerator.PackageHandler packageHandler;
        private GulliverLibrary.Deal deal;

        public flcsAutoUpdateSetting(int dealId)
        {
           packageHandler = new PackageGenerator.PackageHandler(false);
           deal = packageHandler.GetDealById(dealId);
           InitializeComponent();
           FillAutoUpdateSettings(dealId);
        }

        private void FillAutoUpdateSettings(int dealId)
        {
            List<GulliverLibrary.AutoUpdateSetting> autoUpdateSettings = packageHandler.GetAutoUpdateSettings(dealId);

            foreach (GulliverLibrary.AutoUpdateSetting autoUpdateSetting in autoUpdateSettings)
            {
                if (autoUpdateSetting.weekday.Trim().ToUpper() == "MONDAY")
                {
                    if (autoUpdateSetting.time.ToUpper().Contains("AM"))
                        cbMonday.SetItemChecked(0, true);
                    if (autoUpdateSetting.time.ToUpper().Contains("PM"))
                        cbMonday.SetItemChecked(1, true);
                }
                else if (autoUpdateSetting.weekday.Trim().ToUpper() == "TUESDAY")
                {
                    if (autoUpdateSetting.time.ToUpper().Contains("AM"))
                        cbTuesday.SetItemChecked(0, true);
                    if (autoUpdateSetting.time.ToUpper().Contains("PM"))
                        cbTuesday.SetItemChecked(1, true);
                }
                else if (autoUpdateSetting.weekday.Trim().ToUpper() == "WEDNESDAY")
                {
                    if (autoUpdateSetting.time.ToUpper().Contains("AM"))
                        cbWednesday.SetItemChecked(0, true);
                    if (autoUpdateSetting.time.ToUpper().Contains("PM"))
                        cbWednesday.SetItemChecked(1, true);
                }
                else if (autoUpdateSetting.weekday.Trim().ToUpper() == "THURSDAY") 
                {
                    if (autoUpdateSetting.time.ToUpper().Contains("AM"))
                        cbThursday.SetItemChecked(0, true);
                    if (autoUpdateSetting.time.ToUpper().Contains("PM"))
                        cbThursday.SetItemChecked(1, true);
                }
                else if (autoUpdateSetting.weekday.Trim().ToUpper() == "FRIDAY")
                {
                    if (autoUpdateSetting.time.ToUpper().Contains("AM"))
                        cbFriday.SetItemChecked(0, true);
                    if (autoUpdateSetting.time.ToUpper().Contains("PM"))
                        cbFriday.SetItemChecked(1, true);
                }
                else if (autoUpdateSetting.weekday.Trim().ToUpper() == "SATURDAY") 
                {
                    if (autoUpdateSetting.time.ToUpper().Contains("AM"))
                        cbSaturday.SetItemChecked(0, true);
                    if (autoUpdateSetting.time.ToUpper().Contains("PM"))
                        cbSaturday.SetItemChecked(1, true);
                }
                else if (autoUpdateSetting.weekday.Trim().ToUpper() == "SUNDAY")
                {
                    if (autoUpdateSetting.time.ToUpper().Contains("AM"))
                        cbSunday.SetItemChecked(0, true);
                    if (autoUpdateSetting.time.ToUpper().Contains("PM"))
                        cbSunday.SetItemChecked(1, true);
                }
            }
        }

        private void SaveAutoUpdateSettings()
        {
            try
            {
                List<GulliverLibrary.AutoUpdateSetting> gulliverUpdateSettings = new List<GulliverLibrary.AutoUpdateSetting>();

                if (cbMonday.CheckedItems != null && cbMonday.CheckedItems.Count > 0)
                {
                    using (GulliverLibrary.AutoUpdateSetting autoUpdateSetting = new GulliverLibrary.AutoUpdateSetting())
                    {
                        autoUpdateSetting.weekday = "MONDAY";
                        autoUpdateSetting.time = string.Join("#", cbMonday.CheckedItems.Cast<string>().ToArray());
                        autoUpdateSetting.Deal = deal;
                        gulliverUpdateSettings.Add(autoUpdateSetting);
                    }
                }

                if (cbTuesday.CheckedItems != null && cbTuesday.CheckedItems.Count > 0)
                {
                    using (GulliverLibrary.AutoUpdateSetting autoUpdateSetting = new GulliverLibrary.AutoUpdateSetting())
                    {
                        autoUpdateSetting.weekday = "TUESDAY";
                        autoUpdateSetting.time = string.Join("#", cbTuesday.CheckedItems.Cast<string>().ToArray());
                        autoUpdateSetting.Deal = deal;
                        gulliverUpdateSettings.Add(autoUpdateSetting);
                    }
                }

                if (cbWednesday.CheckedItems != null && cbWednesday.CheckedItems.Count > 0)
                {
                    using (GulliverLibrary.AutoUpdateSetting autoUpdateSetting = new GulliverLibrary.AutoUpdateSetting())
                    {
                        autoUpdateSetting.weekday = "WEDNESDAY";
                        autoUpdateSetting.time = string.Join("#", cbWednesday.CheckedItems.Cast<string>().ToArray());
                        autoUpdateSetting.Deal = deal;
                        gulliverUpdateSettings.Add(autoUpdateSetting);
                    }
                }

                if (cbThursday.CheckedItems != null && cbThursday.CheckedItems.Count > 0)
                {
                    using (GulliverLibrary.AutoUpdateSetting autoUpdateSetting = new GulliverLibrary.AutoUpdateSetting())
                    {
                        autoUpdateSetting.weekday = "THURSDAY";
                        autoUpdateSetting.time = string.Join("#", cbThursday.CheckedItems.Cast<string>().ToArray());
                        autoUpdateSetting.Deal = deal;
                        gulliverUpdateSettings.Add(autoUpdateSetting);
                    }
                }

                if (cbFriday.CheckedItems != null && cbFriday.CheckedItems.Count > 0)
                {
                    using (GulliverLibrary.AutoUpdateSetting autoUpdateSetting = new GulliverLibrary.AutoUpdateSetting())
                    {
                        autoUpdateSetting.weekday = "FRIDAY";
                        autoUpdateSetting.time = string.Join("#", cbFriday.CheckedItems.Cast<string>().ToArray());
                        autoUpdateSetting.Deal = deal;
                        gulliverUpdateSettings.Add(autoUpdateSetting);
                    }
                }

                if (cbSaturday.CheckedItems != null && cbSaturday.CheckedItems.Count > 0)
                {
                    using (GulliverLibrary.AutoUpdateSetting autoUpdateSetting = new GulliverLibrary.AutoUpdateSetting())
                    {
                        autoUpdateSetting.weekday = "SATURDAY";
                        autoUpdateSetting.time = string.Join("#", cbSaturday.CheckedItems.Cast<string>().ToArray());
                        autoUpdateSetting.Deal = deal;
                        gulliverUpdateSettings.Add(autoUpdateSetting);
                    }
                }

                if (cbSunday.CheckedItems != null && cbSunday.CheckedItems.Count > 0)
                {
                    using (GulliverLibrary.AutoUpdateSetting autoUpdateSetting = new GulliverLibrary.AutoUpdateSetting())
                    {
                        autoUpdateSetting.weekday = "SUNDAY";
                        autoUpdateSetting.time = string.Join("#", cbSunday.CheckedItems.Cast<string>().ToArray());
                        autoUpdateSetting.Deal = deal;
                        gulliverUpdateSettings.Add(autoUpdateSetting);
                    }
                }

                packageHandler.SaveAutoUpdateSettings(gulliverUpdateSettings, deal.id);
                this.Close();
            }
            catch { }
        }
        
        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveAutoUpdateSettings();
        }
    }
}