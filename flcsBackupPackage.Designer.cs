namespace Gulliver
{
    partial class flcsBackupPackage
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
            this.btnRestore = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.hotelContractHeader = new ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup();
            this.dataGridViewPackageBackup = new ComponentFactory.Krypton.Toolkit.KryptonDataGridView();
            this.idDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.hiddenNumberDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.flightIdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.hotelKeyDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.departureAirportDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.destinationAirportDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.durationDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.obDepartureTimeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.obArrivalTimeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ibDepartureTimeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ibArrivalTimeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.boardDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.flightPriceDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.airlineDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.obFlightNoDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ibFlightNoDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.roomTypeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.occupancyDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.adultsDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.childrenDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.infantsDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.hotelPriceDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.childHotelPriceDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.caaDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.baggagePriceDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.transfersDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.extrasDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.childExtrasDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.baseMarkupDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.totalMarkupDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.totalChildMarkupDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.carhireCostingDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.commissionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.profitDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nettDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sellAtDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.childNettDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.childSellatDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.searchTypeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statusDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.oldSellAtDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bsPackageBackup = new System.Windows.Forms.BindingSource(this.components);
            this.packagesDS = new Gulliver.PackagesDS();
            this.statusstripHolidays = new System.Windows.Forms.StatusStrip();
            this.fiterStatusLabelH = new System.Windows.Forms.ToolStripStatusLabel();
            this.showAllLabelH = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblTotal = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel)).BeginInit();
            this.kryptonPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.hotelContractHeader)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hotelContractHeader.Panel)).BeginInit();
            this.hotelContractHeader.Panel.SuspendLayout();
            this.hotelContractHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPackageBackup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsPackageBackup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.packagesDS)).BeginInit();
            this.statusstripHolidays.SuspendLayout();
            this.SuspendLayout();
            // 
            // kryptonPanel
            // 
            this.kryptonPanel.Controls.Add(this.btnRestore);
            this.kryptonPanel.Controls.Add(this.hotelContractHeader);
            this.kryptonPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel.Name = "kryptonPanel";
            this.kryptonPanel.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.ProfessionalSystem;
            this.kryptonPanel.Size = new System.Drawing.Size(1354, 675);
            this.kryptonPanel.StateCommon.Color1 = System.Drawing.Color.White;
            this.kryptonPanel.TabIndex = 0;
            // 
            // btnRestore
            // 
            this.btnRestore.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRestore.Location = new System.Drawing.Point(1233, 639);
            this.btnRestore.Name = "btnRestore";
            this.btnRestore.Size = new System.Drawing.Size(109, 29);
            this.btnRestore.StateCommon.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(102)))));
            this.btnRestore.StateCommon.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(102)))));
            this.btnRestore.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.White;
            this.btnRestore.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRestore.TabIndex = 57;
            this.btnRestore.Values.Text = "Restore";
            this.btnRestore.Click += new System.EventHandler(this.btnRestore_Click);
            // 
            // hotelContractHeader
            // 
            this.hotelContractHeader.GroupBackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.PanelAlternate;
            this.hotelContractHeader.HeaderVisibleSecondary = false;
            this.hotelContractHeader.Location = new System.Drawing.Point(0, 0);
            this.hotelContractHeader.Name = "hotelContractHeader";
            this.hotelContractHeader.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.ProfessionalSystem;
            // 
            // hotelContractHeader.Panel
            // 
            this.hotelContractHeader.Panel.Controls.Add(this.dataGridViewPackageBackup);
            this.hotelContractHeader.Panel.Controls.Add(this.statusstripHolidays);
            this.hotelContractHeader.Size = new System.Drawing.Size(1354, 633);
            this.hotelContractHeader.StateCommon.HeaderPrimary.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(218)))), ((int)(((byte)(218)))));
            this.hotelContractHeader.StateCommon.HeaderPrimary.Back.Color2 = System.Drawing.Color.White;
            this.hotelContractHeader.StateCommon.HeaderPrimary.Content.ShortText.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(102)))));
            this.hotelContractHeader.TabIndex = 12;
            this.hotelContractHeader.ValuesPrimary.Heading = "Backup Holidays";
            this.hotelContractHeader.ValuesPrimary.Image = null;
            // 
            // dataGridViewPackageBackup
            // 
            this.dataGridViewPackageBackup.AutoGenerateColumns = false;
            this.dataGridViewPackageBackup.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewPackageBackup.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idDataGridViewTextBoxColumn,
            this.hiddenNumberDataGridViewTextBoxColumn,
            this.flightIdDataGridViewTextBoxColumn,
            this.dateDataGridViewTextBoxColumn,
            this.hotelKeyDataGridViewTextBoxColumn,
            this.departureAirportDataGridViewTextBoxColumn,
            this.destinationAirportDataGridViewTextBoxColumn,
            this.durationDataGridViewTextBoxColumn,
            this.obDepartureTimeDataGridViewTextBoxColumn,
            this.obArrivalTimeDataGridViewTextBoxColumn,
            this.ibDepartureTimeDataGridViewTextBoxColumn,
            this.ibArrivalTimeDataGridViewTextBoxColumn,
            this.boardDataGridViewTextBoxColumn,
            this.flightPriceDataGridViewTextBoxColumn,
            this.airlineDataGridViewTextBoxColumn,
            this.obFlightNoDataGridViewTextBoxColumn,
            this.ibFlightNoDataGridViewTextBoxColumn,
            this.roomTypeDataGridViewTextBoxColumn,
            this.occupancyDataGridViewTextBoxColumn,
            this.adultsDataGridViewTextBoxColumn,
            this.childrenDataGridViewTextBoxColumn,
            this.infantsDataGridViewTextBoxColumn,
            this.hotelPriceDataGridViewTextBoxColumn,
            this.childHotelPriceDataGridViewTextBoxColumn,
            this.caaDataGridViewTextBoxColumn,
            this.baggagePriceDataGridViewTextBoxColumn,
            this.transfersDataGridViewTextBoxColumn,
            this.extrasDataGridViewTextBoxColumn,
            this.childExtrasDataGridViewTextBoxColumn,
            this.baseMarkupDataGridViewTextBoxColumn,
            this.totalMarkupDataGridViewTextBoxColumn,
            this.totalChildMarkupDataGridViewTextBoxColumn,
            this.carhireCostingDataGridViewTextBoxColumn,
            this.commissionDataGridViewTextBoxColumn,
            this.profitDataGridViewTextBoxColumn,
            this.nettDataGridViewTextBoxColumn,
            this.sellAtDataGridViewTextBoxColumn,
            this.childNettDataGridViewTextBoxColumn,
            this.childSellatDataGridViewTextBoxColumn,
            this.searchTypeDataGridViewTextBoxColumn,
            this.statusDataGridViewTextBoxColumn,
            this.oldSellAtDataGridViewTextBoxColumn});
            this.dataGridViewPackageBackup.DataSource = this.bsPackageBackup;
            this.dataGridViewPackageBackup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewPackageBackup.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewPackageBackup.Name = "dataGridViewPackageBackup";
            this.dataGridViewPackageBackup.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.ProfessionalSystem;
            this.dataGridViewPackageBackup.Size = new System.Drawing.Size(1352, 581);
            this.dataGridViewPackageBackup.StateCommon.Background.Color1 = System.Drawing.Color.White;
            this.dataGridViewPackageBackup.StateCommon.BackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.GridBackgroundList;
            this.dataGridViewPackageBackup.StateCommon.HeaderColumn.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(218)))), ((int)(((byte)(218)))));
            this.dataGridViewPackageBackup.StateCommon.HeaderColumn.Back.Color2 = System.Drawing.Color.White;
            this.dataGridViewPackageBackup.StateCommon.HeaderColumn.Content.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(102)))));
            this.dataGridViewPackageBackup.StateCommon.HeaderColumn.Content.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridViewPackageBackup.TabIndex = 8;
            this.dataGridViewPackageBackup.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dataGridViewPackageBackup_DataBindingComplete);
            // 
            // idDataGridViewTextBoxColumn
            // 
            this.idDataGridViewTextBoxColumn.DataPropertyName = "id";
            this.idDataGridViewTextBoxColumn.HeaderText = "id";
            this.idDataGridViewTextBoxColumn.Name = "idDataGridViewTextBoxColumn";
            this.idDataGridViewTextBoxColumn.Visible = false;
            // 
            // hiddenNumberDataGridViewTextBoxColumn
            // 
            this.hiddenNumberDataGridViewTextBoxColumn.DataPropertyName = "hiddenNumber";
            this.hiddenNumberDataGridViewTextBoxColumn.HeaderText = "hiddenNumber";
            this.hiddenNumberDataGridViewTextBoxColumn.Name = "hiddenNumberDataGridViewTextBoxColumn";
            this.hiddenNumberDataGridViewTextBoxColumn.Visible = false;
            // 
            // flightIdDataGridViewTextBoxColumn
            // 
            this.flightIdDataGridViewTextBoxColumn.DataPropertyName = "flightId";
            this.flightIdDataGridViewTextBoxColumn.HeaderText = "flightId";
            this.flightIdDataGridViewTextBoxColumn.Name = "flightIdDataGridViewTextBoxColumn";
            this.flightIdDataGridViewTextBoxColumn.Visible = false;
            // 
            // dateDataGridViewTextBoxColumn
            // 
            this.dateDataGridViewTextBoxColumn.DataPropertyName = "date";
            this.dateDataGridViewTextBoxColumn.HeaderText = "Date";
            this.dateDataGridViewTextBoxColumn.Name = "dateDataGridViewTextBoxColumn";
            this.dateDataGridViewTextBoxColumn.Width = 80;
            // 
            // hotelKeyDataGridViewTextBoxColumn
            // 
            this.hotelKeyDataGridViewTextBoxColumn.DataPropertyName = "hotelKey";
            this.hotelKeyDataGridViewTextBoxColumn.HeaderText = "hotelKey";
            this.hotelKeyDataGridViewTextBoxColumn.Name = "hotelKeyDataGridViewTextBoxColumn";
            this.hotelKeyDataGridViewTextBoxColumn.Visible = false;
            // 
            // departureAirportDataGridViewTextBoxColumn
            // 
            this.departureAirportDataGridViewTextBoxColumn.DataPropertyName = "departureAirport";
            this.departureAirportDataGridViewTextBoxColumn.HeaderText = "Departure";
            this.departureAirportDataGridViewTextBoxColumn.Name = "departureAirportDataGridViewTextBoxColumn";
            this.departureAirportDataGridViewTextBoxColumn.Width = 80;
            // 
            // destinationAirportDataGridViewTextBoxColumn
            // 
            this.destinationAirportDataGridViewTextBoxColumn.DataPropertyName = "destinationAirport";
            this.destinationAirportDataGridViewTextBoxColumn.HeaderText = "Destination";
            this.destinationAirportDataGridViewTextBoxColumn.Name = "destinationAirportDataGridViewTextBoxColumn";
            this.destinationAirportDataGridViewTextBoxColumn.Width = 80;
            // 
            // durationDataGridViewTextBoxColumn
            // 
            this.durationDataGridViewTextBoxColumn.DataPropertyName = "duration";
            this.durationDataGridViewTextBoxColumn.HeaderText = "Duration";
            this.durationDataGridViewTextBoxColumn.Name = "durationDataGridViewTextBoxColumn";
            this.durationDataGridViewTextBoxColumn.Width = 80;
            // 
            // obDepartureTimeDataGridViewTextBoxColumn
            // 
            this.obDepartureTimeDataGridViewTextBoxColumn.DataPropertyName = "obDepartureTime";
            this.obDepartureTimeDataGridViewTextBoxColumn.HeaderText = "OB D. Time";
            this.obDepartureTimeDataGridViewTextBoxColumn.Name = "obDepartureTimeDataGridViewTextBoxColumn";
            this.obDepartureTimeDataGridViewTextBoxColumn.Width = 80;
            // 
            // obArrivalTimeDataGridViewTextBoxColumn
            // 
            this.obArrivalTimeDataGridViewTextBoxColumn.DataPropertyName = "obArrivalTime";
            this.obArrivalTimeDataGridViewTextBoxColumn.HeaderText = "OB A. Time";
            this.obArrivalTimeDataGridViewTextBoxColumn.Name = "obArrivalTimeDataGridViewTextBoxColumn";
            this.obArrivalTimeDataGridViewTextBoxColumn.Width = 80;
            // 
            // ibDepartureTimeDataGridViewTextBoxColumn
            // 
            this.ibDepartureTimeDataGridViewTextBoxColumn.DataPropertyName = "ibDepartureTime";
            this.ibDepartureTimeDataGridViewTextBoxColumn.HeaderText = "IB D. Time";
            this.ibDepartureTimeDataGridViewTextBoxColumn.Name = "ibDepartureTimeDataGridViewTextBoxColumn";
            this.ibDepartureTimeDataGridViewTextBoxColumn.Width = 80;
            // 
            // ibArrivalTimeDataGridViewTextBoxColumn
            // 
            this.ibArrivalTimeDataGridViewTextBoxColumn.DataPropertyName = "ibArrivalTime";
            this.ibArrivalTimeDataGridViewTextBoxColumn.HeaderText = "IB A.Time";
            this.ibArrivalTimeDataGridViewTextBoxColumn.Name = "ibArrivalTimeDataGridViewTextBoxColumn";
            this.ibArrivalTimeDataGridViewTextBoxColumn.Width = 80;
            // 
            // boardDataGridViewTextBoxColumn
            // 
            this.boardDataGridViewTextBoxColumn.DataPropertyName = "board";
            this.boardDataGridViewTextBoxColumn.HeaderText = "Board";
            this.boardDataGridViewTextBoxColumn.Name = "boardDataGridViewTextBoxColumn";
            // 
            // flightPriceDataGridViewTextBoxColumn
            // 
            this.flightPriceDataGridViewTextBoxColumn.DataPropertyName = "flightPrice";
            this.flightPriceDataGridViewTextBoxColumn.HeaderText = "Flight";
            this.flightPriceDataGridViewTextBoxColumn.Name = "flightPriceDataGridViewTextBoxColumn";
            this.flightPriceDataGridViewTextBoxColumn.Width = 80;
            // 
            // airlineDataGridViewTextBoxColumn
            // 
            this.airlineDataGridViewTextBoxColumn.DataPropertyName = "airline";
            this.airlineDataGridViewTextBoxColumn.HeaderText = "Airline";
            this.airlineDataGridViewTextBoxColumn.Name = "airlineDataGridViewTextBoxColumn";
            // 
            // obFlightNoDataGridViewTextBoxColumn
            // 
            this.obFlightNoDataGridViewTextBoxColumn.DataPropertyName = "obFlightNo";
            this.obFlightNoDataGridViewTextBoxColumn.HeaderText = "OB Flight No";
            this.obFlightNoDataGridViewTextBoxColumn.Name = "obFlightNoDataGridViewTextBoxColumn";
            // 
            // ibFlightNoDataGridViewTextBoxColumn
            // 
            this.ibFlightNoDataGridViewTextBoxColumn.DataPropertyName = "ibFlightNo";
            this.ibFlightNoDataGridViewTextBoxColumn.HeaderText = "IB Flight No";
            this.ibFlightNoDataGridViewTextBoxColumn.Name = "ibFlightNoDataGridViewTextBoxColumn";
            // 
            // roomTypeDataGridViewTextBoxColumn
            // 
            this.roomTypeDataGridViewTextBoxColumn.DataPropertyName = "roomType";
            this.roomTypeDataGridViewTextBoxColumn.HeaderText = "RoomType";
            this.roomTypeDataGridViewTextBoxColumn.Name = "roomTypeDataGridViewTextBoxColumn";
            // 
            // occupancyDataGridViewTextBoxColumn
            // 
            this.occupancyDataGridViewTextBoxColumn.DataPropertyName = "occupancy";
            this.occupancyDataGridViewTextBoxColumn.HeaderText = "Occupancy";
            this.occupancyDataGridViewTextBoxColumn.Name = "occupancyDataGridViewTextBoxColumn";
            // 
            // adultsDataGridViewTextBoxColumn
            // 
            this.adultsDataGridViewTextBoxColumn.DataPropertyName = "adults";
            this.adultsDataGridViewTextBoxColumn.HeaderText = "Adults";
            this.adultsDataGridViewTextBoxColumn.Name = "adultsDataGridViewTextBoxColumn";
            // 
            // childrenDataGridViewTextBoxColumn
            // 
            this.childrenDataGridViewTextBoxColumn.DataPropertyName = "children";
            this.childrenDataGridViewTextBoxColumn.HeaderText = "Children";
            this.childrenDataGridViewTextBoxColumn.Name = "childrenDataGridViewTextBoxColumn";
            // 
            // infantsDataGridViewTextBoxColumn
            // 
            this.infantsDataGridViewTextBoxColumn.DataPropertyName = "infants";
            this.infantsDataGridViewTextBoxColumn.HeaderText = "Infants";
            this.infantsDataGridViewTextBoxColumn.Name = "infantsDataGridViewTextBoxColumn";
            // 
            // hotelPriceDataGridViewTextBoxColumn
            // 
            this.hotelPriceDataGridViewTextBoxColumn.DataPropertyName = "hotelPrice";
            this.hotelPriceDataGridViewTextBoxColumn.HeaderText = "Accom Price";
            this.hotelPriceDataGridViewTextBoxColumn.Name = "hotelPriceDataGridViewTextBoxColumn";
            // 
            // childHotelPriceDataGridViewTextBoxColumn
            // 
            this.childHotelPriceDataGridViewTextBoxColumn.DataPropertyName = "childHotelPrice";
            this.childHotelPriceDataGridViewTextBoxColumn.HeaderText = "C.Accom Price";
            this.childHotelPriceDataGridViewTextBoxColumn.Name = "childHotelPriceDataGridViewTextBoxColumn";
            // 
            // caaDataGridViewTextBoxColumn
            // 
            this.caaDataGridViewTextBoxColumn.DataPropertyName = "caa";
            this.caaDataGridViewTextBoxColumn.HeaderText = "CAA";
            this.caaDataGridViewTextBoxColumn.Name = "caaDataGridViewTextBoxColumn";
            // 
            // baggagePriceDataGridViewTextBoxColumn
            // 
            this.baggagePriceDataGridViewTextBoxColumn.DataPropertyName = "baggagePrice";
            this.baggagePriceDataGridViewTextBoxColumn.HeaderText = "Baggage Price";
            this.baggagePriceDataGridViewTextBoxColumn.Name = "baggagePriceDataGridViewTextBoxColumn";
            // 
            // transfersDataGridViewTextBoxColumn
            // 
            this.transfersDataGridViewTextBoxColumn.DataPropertyName = "transfers";
            this.transfersDataGridViewTextBoxColumn.HeaderText = "Transfers";
            this.transfersDataGridViewTextBoxColumn.Name = "transfersDataGridViewTextBoxColumn";
            // 
            // extrasDataGridViewTextBoxColumn
            // 
            this.extrasDataGridViewTextBoxColumn.DataPropertyName = "extras";
            this.extrasDataGridViewTextBoxColumn.HeaderText = "Extras";
            this.extrasDataGridViewTextBoxColumn.Name = "extrasDataGridViewTextBoxColumn";
            // 
            // childExtrasDataGridViewTextBoxColumn
            // 
            this.childExtrasDataGridViewTextBoxColumn.DataPropertyName = "childExtras";
            this.childExtrasDataGridViewTextBoxColumn.HeaderText = "C.Extras";
            this.childExtrasDataGridViewTextBoxColumn.Name = "childExtrasDataGridViewTextBoxColumn";
            // 
            // baseMarkupDataGridViewTextBoxColumn
            // 
            this.baseMarkupDataGridViewTextBoxColumn.DataPropertyName = "baseMarkup";
            this.baseMarkupDataGridViewTextBoxColumn.HeaderText = "B.Markup";
            this.baseMarkupDataGridViewTextBoxColumn.Name = "baseMarkupDataGridViewTextBoxColumn";
            // 
            // totalMarkupDataGridViewTextBoxColumn
            // 
            this.totalMarkupDataGridViewTextBoxColumn.DataPropertyName = "totalMarkup";
            this.totalMarkupDataGridViewTextBoxColumn.HeaderText = "Markup";
            this.totalMarkupDataGridViewTextBoxColumn.Name = "totalMarkupDataGridViewTextBoxColumn";
            // 
            // totalChildMarkupDataGridViewTextBoxColumn
            // 
            this.totalChildMarkupDataGridViewTextBoxColumn.DataPropertyName = "totalChildMarkup";
            this.totalChildMarkupDataGridViewTextBoxColumn.HeaderText = "C.Markup";
            this.totalChildMarkupDataGridViewTextBoxColumn.Name = "totalChildMarkupDataGridViewTextBoxColumn";
            // 
            // carhireCostingDataGridViewTextBoxColumn
            // 
            this.carhireCostingDataGridViewTextBoxColumn.DataPropertyName = "carhireCosting";
            this.carhireCostingDataGridViewTextBoxColumn.HeaderText = "Carhire";
            this.carhireCostingDataGridViewTextBoxColumn.Name = "carhireCostingDataGridViewTextBoxColumn";
            // 
            // commissionDataGridViewTextBoxColumn
            // 
            this.commissionDataGridViewTextBoxColumn.DataPropertyName = "commission";
            this.commissionDataGridViewTextBoxColumn.HeaderText = "Commission";
            this.commissionDataGridViewTextBoxColumn.Name = "commissionDataGridViewTextBoxColumn";
            // 
            // profitDataGridViewTextBoxColumn
            // 
            this.profitDataGridViewTextBoxColumn.DataPropertyName = "profit";
            this.profitDataGridViewTextBoxColumn.HeaderText = "Profit";
            this.profitDataGridViewTextBoxColumn.Name = "profitDataGridViewTextBoxColumn";
            // 
            // nettDataGridViewTextBoxColumn
            // 
            this.nettDataGridViewTextBoxColumn.DataPropertyName = "nett";
            this.nettDataGridViewTextBoxColumn.HeaderText = "Nett";
            this.nettDataGridViewTextBoxColumn.Name = "nettDataGridViewTextBoxColumn";
            // 
            // sellAtDataGridViewTextBoxColumn
            // 
            this.sellAtDataGridViewTextBoxColumn.DataPropertyName = "sellAt";
            this.sellAtDataGridViewTextBoxColumn.HeaderText = "SellAt";
            this.sellAtDataGridViewTextBoxColumn.Name = "sellAtDataGridViewTextBoxColumn";
            // 
            // childNettDataGridViewTextBoxColumn
            // 
            this.childNettDataGridViewTextBoxColumn.DataPropertyName = "childNett";
            this.childNettDataGridViewTextBoxColumn.HeaderText = "C. Nett";
            this.childNettDataGridViewTextBoxColumn.Name = "childNettDataGridViewTextBoxColumn";
            // 
            // childSellatDataGridViewTextBoxColumn
            // 
            this.childSellatDataGridViewTextBoxColumn.DataPropertyName = "childSellat";
            this.childSellatDataGridViewTextBoxColumn.HeaderText = "C.Sellat";
            this.childSellatDataGridViewTextBoxColumn.Name = "childSellatDataGridViewTextBoxColumn";
            // 
            // searchTypeDataGridViewTextBoxColumn
            // 
            this.searchTypeDataGridViewTextBoxColumn.DataPropertyName = "searchType";
            this.searchTypeDataGridViewTextBoxColumn.HeaderText = "SearchType";
            this.searchTypeDataGridViewTextBoxColumn.Name = "searchTypeDataGridViewTextBoxColumn";
            this.searchTypeDataGridViewTextBoxColumn.Visible = false;
            // 
            // statusDataGridViewTextBoxColumn
            // 
            this.statusDataGridViewTextBoxColumn.DataPropertyName = "status";
            this.statusDataGridViewTextBoxColumn.HeaderText = "Status";
            this.statusDataGridViewTextBoxColumn.Name = "statusDataGridViewTextBoxColumn";
            // 
            // oldSellAtDataGridViewTextBoxColumn
            // 
            this.oldSellAtDataGridViewTextBoxColumn.DataPropertyName = "oldSellAt";
            this.oldSellAtDataGridViewTextBoxColumn.HeaderText = "OldSellAt";
            this.oldSellAtDataGridViewTextBoxColumn.Name = "oldSellAtDataGridViewTextBoxColumn";
            // 
            // bsPackageBackup
            // 
            this.bsPackageBackup.DataMember = "PackageBackup";
            this.bsPackageBackup.DataSource = this.packagesDS;
            // 
            // packagesDS
            // 
            this.packagesDS.DataSetName = "PackagesDS";
            this.packagesDS.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // statusstripHolidays
            // 
            this.statusstripHolidays.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(218)))), ((int)(((byte)(218)))));
            this.statusstripHolidays.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.statusstripHolidays.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fiterStatusLabelH,
            this.showAllLabelH,
            this.lblTotal});
            this.statusstripHolidays.Location = new System.Drawing.Point(0, 581);
            this.statusstripHolidays.Name = "statusstripHolidays";
            this.statusstripHolidays.Size = new System.Drawing.Size(1352, 22);
            this.statusstripHolidays.TabIndex = 7;
            // 
            // fiterStatusLabelH
            // 
            this.fiterStatusLabelH.Name = "fiterStatusLabelH";
            this.fiterStatusLabelH.Size = new System.Drawing.Size(0, 17);
            // 
            // showAllLabelH
            // 
            this.showAllLabelH.IsLink = true;
            this.showAllLabelH.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.showAllLabelH.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(102)))));
            this.showAllLabelH.Name = "showAllLabelH";
            this.showAllLabelH.Size = new System.Drawing.Size(53, 17);
            this.showAllLabelH.Text = "&Show All";
            // 
            // lblTotal
            // 
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(0, 17);
            // 
            // flcsBackupPackage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1354, 675);
            this.Controls.Add(this.kryptonPanel);
            this.Name = "flcsBackupPackage";
            this.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.ProfessionalSystem;
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel)).EndInit();
            this.kryptonPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.hotelContractHeader.Panel)).EndInit();
            this.hotelContractHeader.Panel.ResumeLayout(false);
            this.hotelContractHeader.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.hotelContractHeader)).EndInit();
            this.hotelContractHeader.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPackageBackup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsPackageBackup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.packagesDS)).EndInit();
            this.statusstripHolidays.ResumeLayout(false);
            this.statusstripHolidays.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonManager kryptonManager;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel;
        private ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup hotelContractHeader;
        private ComponentFactory.Krypton.Toolkit.KryptonDataGridView dataGridViewPackageBackup;
        private System.Windows.Forms.StatusStrip statusstripHolidays;
        private System.Windows.Forms.ToolStripStatusLabel fiterStatusLabelH;
        private System.Windows.Forms.ToolStripStatusLabel showAllLabelH;
        private System.Windows.Forms.ToolStripStatusLabel lblTotal;
        private System.Windows.Forms.BindingSource bsPackageBackup;
        private PackagesDS packagesDS;
        private System.Windows.Forms.DataGridViewTextBoxColumn deleteDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn monthDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dateOfWeekDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn summaryDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn hiddenNumberDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn flightIdDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn hotelKeyDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn departureAirportDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn destinationAirportDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn durationDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn obDepartureTimeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn obArrivalTimeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ibDepartureTimeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ibArrivalTimeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn boardDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn flightPriceDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn airlineDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn obFlightNoDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ibFlightNoDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn roomTypeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn occupancyDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn adultsDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn childrenDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn infantsDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn hotelPriceDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn childHotelPriceDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn caaDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn baggagePriceDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn transfersDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn extrasDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn childExtrasDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn baseMarkupDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn totalMarkupDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn totalChildMarkupDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn carhireCostingDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn commissionDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn profitDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nettDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sellAtDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn childNettDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn childSellatDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn searchTypeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn statusDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn oldSellAtDataGridViewTextBoxColumn;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnRestore;
    }
}

