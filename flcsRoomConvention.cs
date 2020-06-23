using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
using System.Configuration;
using System.Collections;
using System.Linq;

namespace GulliverII
{
    public partial class flcsRoomConvention : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        private string id;
        private int dealId;
        private PackageGenerator.PackageHandler packageHandler;
        private GulliverLibrary.HotelContract hotelContract;
        private GulliverLibrary.QueryHandler queryHandler;

        public flcsRoomConvention(string id, int dealId)
        {
            this.id = id;
            this.dealId = dealId;
            packageHandler = new PackageGenerator.PackageHandler(false, ConfigurationManager.AppSettings["enviroment"].ToString());
            hotelContract = packageHandler.GetHotelContractByRecno(Convert.ToInt32(id), dealId);
            queryHandler = new GulliverLibrary.QueryHandler();
            InitializeComponent();
            
            //FillRoomTypes();
            //FillRoomGrades();
            //FillRoomViews();
        }

        //public void FillRoomTypes()
        //{
        //    Hashtable roomTypes = queryHandler.GetRoomConventionByFieldName("type");
        //    ArrayList roomtypeList = new ArrayList();
        //    roomtypeList.AddRange(roomTypes.Keys);
        //    string roomCode = (hotelContract.tsRoomCode != null) ? hotelContract.tsRoomCode : string.Empty;
        //    string roomtype = string.Empty;

        //    if (roomCode != string.Empty)
        //    {
        //        string[] subStrings = roomCode.Split(':');
        //        roomtype = (subStrings.Count() > 1) ? subStrings.First() : string.Empty;
        //    }

        //    ComboBoxItem itemD = new ComboBoxItem();
        //    itemD.Text = "Please select room type";
        //    itemD.Value = string.Empty;
        //    cmbRoomTypes.Items.Add(itemD);

        //    foreach (string key in roomtypeList)
        //    {
        //       ComboBoxItem item = new ComboBoxItem();
        //       item.Text = roomTypes[key].ToString();
        //       item.Value = key;
        //       cmbRoomTypes.Items.Add(item);

        //       if (roomtype == key)
        //           cmbRoomTypes.SelectedItem = item;
        //    }
          
        //}

        //public void FillRoomGrades()
        //{
        //    Hashtable roomGrades = queryHandler.GetRoomConventionByFieldName("grade");
        //    ArrayList roomgradesList = new ArrayList();
        //    roomgradesList.AddRange(roomGrades.Keys);

        //    ComboBoxItem itemD = new ComboBoxItem();
        //    itemD.Text = "Please select room grade";
        //    itemD.Value = string.Empty;
        //    cmbRoomGrades.Items.Add(itemD);

        //    string roomCode = (hotelContract.tsRoomCode != null) ? hotelContract.tsRoomCode : string.Empty;
        //    string roomgrade = string.Empty;

        //    if (roomCode != string.Empty)
        //    {
        //        string[] subStrings = roomCode.Split(':');
        //        roomgrade = (subStrings.Count() >= 2) ? subStrings.First() : string.Empty;
        //    }

        //    foreach (string key in roomgradesList)
        //    {
        //        ComboBoxItem item = new ComboBoxItem();
        //        item.Text = roomGrades[key].ToString();
        //        item.Value = key;
        //        cmbRoomGrades.Items.Add(item);

        //        if (roomgrade == key)
        //            cmbRoomGrades.SelectedItem = item;
        //    }
        //}

        //public void FillRoomViews()
        //{
        //    Hashtable roomViews = queryHandler.GetRoomConventionByFieldName("view");
        //    ArrayList roomViewsList = new ArrayList();
        //    roomViewsList.AddRange(roomViews.Keys);

        //    ComboBoxItem itemD = new ComboBoxItem();
        //    itemD.Text = "Please select room view";
        //    itemD.Value = string.Empty;
        //    cmbRoomViews.Items.Add(itemD);

        //    string roomCode = (hotelContract.tsRoomCode != null) ? hotelContract.tsRoomCode : string.Empty;
        //    string roomView = string.Empty;

        //    if (roomCode != string.Empty)
        //    {
        //        string[] subStrings = roomCode.Split(':');
        //        roomView = (subStrings.Count() > 2) ? subStrings.Last() : string.Empty;
        //    }

        //    foreach (string key in roomViewsList)
        //    {
        //        ComboBoxItem item = new ComboBoxItem();
        //        item.Text = roomViews[key].ToString();
        //        item.Value = key;
        //        cmbRoomViews.Items.Add(item);

        //        if (roomView == key)
        //            cmbRoomViews.SelectedItem = item;
        //    }
        //}

        //private void btnSave_Click(object sender, EventArgs e)
        //{
        //    string roomCode = string.Empty;

        //    if (((ComboBoxItem)cmbRoomTypes.SelectedItem).Value != "" && ((ComboBoxItem)cmbRoomGrades.SelectedItem).Value != "")
        //    {
        //        roomCode = ((ComboBoxItem)cmbRoomTypes.SelectedItem).Value + ":";
        //        roomCode += ((ComboBoxItem)cmbRoomGrades.SelectedItem).Value + ":";
        //        roomCode += ((ComboBoxItem)cmbRoomViews.SelectedItem).Value + ":";
        //        hotelContract.tsRoomCode = roomCode;
        //        queryHandler.SaveRoomConvention(hotelContract);
        //    }
        //    else
        //    {
        //        KryptonMessageBox.Show("Please at least select room type and room grade!");
        //    }
        //}
    }
}