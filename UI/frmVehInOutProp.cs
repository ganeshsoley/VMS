using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EntityObject;
using EntityObject.Enum;
using BLL;

namespace UI
{
    public partial class frmVehInOutProp : Form
    {
        #region Private Variable
        private bool flgNew;
        private bool flgLoading;
        private string mEntryType;

        private VehInOut objVehInOut;
        #endregion

        #region Constructor
        public frmVehInOutProp()
        {
            InitializeComponent();
        }
        public frmVehInOutProp(VehInOut objVehInOut)
        {
            this.objVehInOut = objVehInOut;
            InitializeComponent();
        }
        #endregion

        #region Public Properties
        public bool IsNew
        {
            get
            {
                return flgNew;
            }
            set
            {
                flgNew = value;
            }
        }
        public bool IsLoading
        {
            get
            {
                return flgLoading;
            }
            set
            {
                flgLoading = value;
            }
        }

        public string EntryType
        {
            get { return mEntryType; }
            set { mEntryType = value; }
        }
        #endregion

        #region Delegate & Event
        public delegate void VehInOutUpdateHandler(object sender, VehInOutUpdateEventArgs e, DataEventType Action);

        public event VehInOutUpdateHandler Entry_DataChanged;
        #endregion

        #region Private Methods
        private void EnableDisableSave()
        {
            btnSave.Enabled = objVehInOut.IsValid;
        }
        private void VehInOut_OnValid(object sender, EventArgs e)
        {
            EnableDisableSave();
        }
        private void VehInOut_OnInValid(object sender, EventArgs e)
        {
            EnableDisableSave();
        }
        private void SubscribeToEvents()
        {
            objVehInOut.OnValid += new VehInOut.EventHandler(VehInOut_OnValid);
            objVehInOut.OnInvalid += new VehInOut.EventHandler(VehInOut_OnInValid);
        }
        private void FillDriver()
        {
            string[] DriverList = VehInOutManager.GetDrivers();

            cboDriver.Items.Clear();
            if (DriverList != null)
            {
                //cboDriver.Items.Add("All");
                foreach (string str in DriverList)
                {
                    cboDriver .Items.Add(str);
                }
               // cboDriver.Text = "All";
            }
        }
        private void FillVehicle()
        {
            string[] VehicleList = VehInOutManager.GetVehicles();

            cboVehicleNo.Items.Clear();
            if (VehicleList != null)
            {
                foreach (string str in VehicleList)
                {
                    cboVehicleNo.Items.Add(str);
                }
                
            }
        }
        private void FillInVendors()
        {
            string[] InVendorList = VehInOutManager.GetInVendors();

            cboVendorIn.Items.Clear();
            if (InVendorList != null)
            {
                
                foreach (string str in InVendorList)
                {
                    cboVendorIn.Items.Add(str);
                }
                
            }
        }
        private void FillOutVendors()
        {
            string[] OutVendorList = VehInOutManager.GetOutVendors();

            cboVendorOut.Items.Clear();
            if (OutVendorList != null)
            {

                foreach (string str in OutVendorList)
                {
                    cboVendorOut.Items.Add(str);
                }

            }
        }
        private void FillCity()
        {
            string[] CityList = VehInOutManager.GetCities();

            cboCity.Items.Clear();
            if (CityList != null)
            {
                
                foreach (string str in CityList)
                {
                    cboCity.Items.Add(str);
                }
                
            }
        }

        private void FillPlants()
        {
            cboPlant.Items.Add("DTPL 100");
            cboPlant.Items.Add("DTPL 102");
            cboPlant.Items.Add("DTPL 103");
            cboPlant.Items.Add("DTPL 100,102");
            cboPlant.Items.Add("DTPL 102,103");
            cboPlant.Items.Add("DTPL 100,103");
            cboPlant.Items.Add("DTPL 100,102,103");
            cboPlant.Items.Add("DTPL SHENDRA");
        }
        #endregion


        #region UI Control Logic
        private void frmVehInOutProp_Load(object sender, EventArgs e)
        {
            this.Icon = new Icon("Images/DTPL.ico");
            flgLoading = true;
            VehInOut_OnInValid(sender, e);
            FillPlants();
            FillDriver();
            FillVehicle();
            FillInVendors();
            FillOutVendors();
            FillCity();

            if (objVehInOut.IsNew)
            {
                this.Text += " [ NEW ]";
                //objVehInOut.RegNo = VehInOutManager.GetVRegNo();
                //lblRegNo.Text = Convert.ToString(objVisitor.RegNo);
                //dtpDateOut.Text = Convert.ToString(DateTime.Now.ToShortDateString());
                //dtpTimeOut.Text = Convert.ToString(DateTime.Now.ToShortTimeString());
                //dtpDateIn.Text = Convert.ToString(DateTime.Now.ToShortDateString());
                //dtpTimeIn.Text = Convert.ToString(DateTime.Now.ToShortTimeString());
            }
            else
            {
                this.Text += " [ " + objVehInOut.EntryNo + " ]";
                txtEntryNo.Text = Convert.ToString(objVehInOut.EntryNo);
                if (objVehInOut.Type == 4)
                {
                    cboEntryType.Text = "COMPANY";
                }
                else if (objVehInOut.Type == 1)
                {
                    cboEntryType.Text = "IN/OUT OTHER";
                }
                if (objVehInOut.OutDate != DateTime.MinValue)
                {
                    dtpDateOut.Value = objVehInOut.OutDate;
                    txtOutDate.Text = objVehInOut.OutDate.ToShortDateString();
                }
                //else
                //    dtpDateOut.Value = DateTime.Now.Date;

                if (objVehInOut.OutTime != DateTime.MinValue)
                    dtpTimeOut.Value = objVehInOut.OutTime;
                //else
                //    dtpTimeOut.Value = DateTime.Now;

                if (objVehInOut.InDate != DateTime.MinValue)
                {
                    dtpDateIn.Value = objVehInOut.InDate;
                    txtInDate.Text = objVehInOut.InDate.ToShortDateString();
                }
                //else
                //    dtpDateIn.Value = DateTime.Now.Date;

                if (objVehInOut.InTime != DateTime.MinValue)
                    dtpTimeIn.Value = objVehInOut.InTime;
                //else
                //    dtpTimeIn.Value = DateTime.Now;

                //dtpDateOut.Text = objVehInOut.OutDate.ToShortDateString();
                //dtpTimeOut.Text = objVehInOut.OutTime.ToShortTimeString();
                //dtpDateIn.Text = objVehInOut.InDate.ToShortDateString();
                //dtpTimeIn.Text = objVehInOut.InTime.ToShortTimeString();
                txtInReading.Text = Convert.ToString(objVehInOut.InReading);
                txtOutReading.Text = Convert.ToString(objVehInOut.OutReading);
                
            }
            
            cboVehicleNo.Text = objVehInOut.VehNo;

            if (objVehInOut.InOut == 1)
            {
                rbtnIn.Checked = true;
                rbtnOut.Checked = false;
            }
            if (objVehInOut.InOut == 2)
            {
                rbtnIn.Checked = false;
                rbtnOut.Checked = true;
            }
 
            cboDriver.Text = objVehInOut.DriverName;
            cboVendorIn.Text = objVehInOut.VendorIn;
            cboVendorOut.Text = objVehInOut.VendorOut;
            cboCity.Text = objVehInOut.CityName;
            cboPlant.Text = objVehInOut.Plant;
            txtCarryInMaterial.Text = objVehInOut.InCarryMaterial;
            txtCarryOutMaterial.Text = objVehInOut.OutCarryMaterial;
            txtPUC.Text = objVehInOut.PUCNo;
            txtRCBook.Text = objVehInOut.RCBookNo;
            txtDriverLic.Text = objVehInOut.DrLicNo;
            txtPersonWtVeh.Text = Convert.ToString(objVehInOut.PersonsWithVeh);
            txtNoOfInvCopyIn.Text = Convert.ToString(objVehInOut.InInvCnt);
            txtNoOfInvCopyOut.Text = Convert.ToString(objVehInOut.OutInvCnt);

            if (objVehInOut.PUCFlg == "Y")
                chkPUC.Checked = true;

            if (objVehInOut.RCBookFlg == "Y")
                chkRCBook.Checked = true;

            if (objVehInOut.Finance == "Y")
                chkFinance.Checked = true;

            if (objVehInOut.FitnessFlg == "Y")
                chkFinance.Checked = true;

            if (objVehInOut.DrLicFlg == "Y")
                chkDrivingLic.Checked = true;

            SubscribeToEvents();
            flgLoading = false;
            if (objVehInOut.IsNew)
                cboEntryType.Text = this.EntryType;
        }

        private void frmVehInOutProp_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void cboVehicleNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!IsLoading)
                {
                    objVehInOut.VehNo = Convert.ToString(cboVehicleNo.Text.Trim());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private void cboVehicleNo_Enter(object sender, EventArgs e)
        {
            GeneralMethods.Selection(cboVehicleNo);
        }
        private void cboVehicleNo_Leave(object sender, EventArgs e)
        {
           //objVehInOut.VehNo = cboVehicleNo.Text.Trim().ToUpper();
           cboVehicleNo.Text = cboVehicleNo.Text.Trim().ToUpper(); 
        }
        private void cboVehicleNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!IsLoading)
                {
                    objVehInOut.VehNo = Convert.ToString(cboVehicleNo.Text.Trim());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void rbtnIn_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnIn.Checked == true )
            {
                objVehInOut.InOut = 1;
                //dtpDateOut.Enabled = false;
                txtOutDate.Enabled = false;
                dtpTimeOut.Enabled = false;
                cboVendorOut.Enabled = false;
                txtCarryOutMaterial.Enabled = false;
                txtNoOfInvCopyOut.Enabled = false;

                //dtpDateIn.Enabled = true;
                txtInDate.Enabled = true;
                dtpTimeIn.Enabled = true;
                cboVendorIn.Enabled = true;
                txtCarryInMaterial.Enabled = true;
                txtNoOfInvCopyIn.Enabled = true;

                if (objVehInOut.IsNew)
                {
                    //dtpDateIn.Value = DateTime.Now.Date;
                    txtInDate.Text = DateTime.Now.ToShortDateString();
                    dtpTimeIn.Value = DateTime.Now;
                    objVehInOut.OutDate = DateTime.MinValue;
                    objVehInOut.OutTime = DateTime.MinValue;
                }
                else
                {
                    if (objVehInOut.InDate != DateTime.MinValue)
                    {
                        //dtpDateIn.Value = objVehInOut.InDate;
                        txtInDate.Text = objVehInOut.InDate.ToShortDateString();
                    }
                    if (objVehInOut.InTime != DateTime.MinValue)
                        dtpTimeIn.Value = objVehInOut.InTime;
                }
                if (cboEntryType.Text == "COMPANY")
                {
                    txtInReading.Enabled = true;
                    txtOutReading.Enabled = false;
                }
                else
                {
                    txtInReading.Enabled = false;
                    txtOutReading.Enabled = false;
                }
            }
        }
        private void rbtnOut_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnOut.Checked == true)
            {
                objVehInOut.InOut = 2;
                //dtpDateOut.Enabled = true;
                txtOutDate.Enabled = true;
                dtpTimeOut.Enabled = true ;
                cboVendorOut.Enabled = true;
                txtCarryOutMaterial.Enabled = true;
                txtNoOfInvCopyOut.Enabled = true;

                //dtpDateIn.Enabled = false;
                txtInDate.Enabled = false;
                dtpTimeIn.Enabled = false;
                cboVendorIn.Enabled = false;
                txtCarryInMaterial.Enabled = false;
                txtNoOfInvCopyIn.Enabled = false;

                if (objVehInOut.IsNew)
                {
                    objVehInOut.InDate = DateTime.MinValue;
                    objVehInOut.InTime = DateTime.MinValue;
                    //dtpDateOut.Value = DateTime.Now.Date;
                    txtOutDate.Text = DateTime.Now.ToShortDateString();
                    dtpTimeOut.Value = DateTime.Now;
                }
                else
                {
                    if (objVehInOut.OutDate != DateTime.MinValue)
                    {
                        //dtpDateOut.Value = objVehInOut.OutDate;
                        txtOutDate.Text = objVehInOut.OutDate.ToShortDateString();
                    }
                    if (objVehInOut.OutTime != DateTime.MinValue)
                        dtpTimeOut.Value = objVehInOut.OutTime;
                }
                if (cboEntryType.Text == "COMPANY")
                {
                    txtOutReading.Enabled = true;
                }
                else
                {
                    txtOutReading.Enabled = false;
                }
            }
        }

        private void dtpDateOut_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (!IsLoading)
                {
                    objVehInOut.OutDate  = Convert.ToDateTime(dtpDateOut.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private void dtpTimeOut_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (!IsLoading)
                {
                    objVehInOut.OutTime = Convert.ToDateTime(dtpTimeOut.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private void dtpDateIn_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (!IsLoading)
                {
                    objVehInOut.InDate = Convert.ToDateTime(dtpDateIn.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private void dtpTimeIn_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (!IsLoading)
                {
                    objVehInOut.InTime = Convert.ToDateTime(dtpTimeIn.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void cboDriver_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!IsLoading)
                {
                    objVehInOut.DriverName = Convert.ToString(cboDriver.Text.Trim());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private void cboDriver_Enter(object sender, EventArgs e)
        {
            GeneralMethods.Selection(cboDriver);
        }
        private void cboDriver_Leave(object sender, EventArgs e)
        {
            objVehInOut.DriverName = cboDriver.Text;
            cboDriver.Text = cboDriver.Text.Trim().ToUpper();
        }
        private void cboDriver_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!IsLoading)
                {
                    objVehInOut.DriverName = Convert.ToString(cboDriver.Text.Trim());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void cboVendorIn_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!IsLoading)
                {
                    objVehInOut.VendorIn  = Convert.ToString(cboVendorIn.Text.Trim());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private void cboVendorIn_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!IsLoading)
                {
                    objVehInOut.VendorIn = Convert.ToString(cboVendorIn.Text.Trim());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private void cboVendorIn_Enter(object sender, EventArgs e)
        {
            GeneralMethods.Selection(cboVendorIn);
        }
        private void cboVendorIn_Leave(object sender, EventArgs e)
        {
            objVehInOut.VendorIn = cboVendorIn.Text;
            cboVendorIn.Text = cboVendorIn.Text.Trim().ToUpper(); 
        }

        private void cboVendorOut_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!IsLoading)
                {
                    objVehInOut.VendorOut = Convert.ToString(cboVendorOut.Text.Trim());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private void cboVendorOut_Enter(object sender, EventArgs e)
        {
            GeneralMethods.Selection(cboVendorOut); 
        }
        private void cboVendorOut_Leave(object sender, EventArgs e)
        {
            objVehInOut.VendorOut = cboVendorOut.Text;
            cboVendorOut.Text = cboVendorOut.Text.Trim().ToUpper();
        }
        private void cboVendorOut_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!IsLoading)
                {
                    objVehInOut.VendorOut = Convert.ToString(cboVendorOut.Text.Trim());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void cboCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!IsLoading)
                {
                    objVehInOut.CityName = Convert.ToString(cboCity.Text.Trim());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private void cboCity_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!IsLoading)
                {
                    objVehInOut.CityName = Convert.ToString(cboCity.Text.Trim());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private void cboCity_Enter(object sender, EventArgs e)
        {
            GeneralMethods.Selection(cboCity);
        }
        private void cboCity_Leave(object sender, EventArgs e)
        {
            objVehInOut.CityName = cboCity.Text;
            cboCity.Text = cboCity.Text.Trim().ToUpper(); 
        }

        private void cboPlant_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!IsLoading)
                {
                    objVehInOut.Plant = Convert.ToString(cboPlant.Text.Trim());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private void cboPlant_Enter(object sender, EventArgs e)
        {
            GeneralMethods.Selection(cboPlant);
        }
        private void cboPlant_Leave(object sender, EventArgs e)
        {
            objVehInOut.Plant = cboPlant.Text;
            cboPlant.Text = cboPlant.Text.Trim().ToUpper();
        }
        private void cboPlant_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!IsLoading)
                {
                    objVehInOut.Plant = Convert.ToString(cboPlant.Text.Trim());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void txtCarryInMaterial_Enter(object sender, EventArgs e)
        {
            GeneralMethods.Selection(txtCarryInMaterial);
        }
        private void txtCarryInMaterial_KeyPress(object sender, KeyPressEventArgs e)
        {

        }
        private void txtCarryInMaterial_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!IsLoading)
                {
                    objVehInOut.InCarryMaterial  = Convert.ToString(txtCarryInMaterial.Text.Trim());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private void txtCarryInMaterial_Leave(object sender, EventArgs e)
        {
            txtCarryInMaterial.Text = txtCarryInMaterial.Text.Trim().ToUpper();
        }

        private void txtCarryOutMaterial_Enter(object sender, EventArgs e)
        {
            GeneralMethods.Selection(txtCarryOutMaterial); 
        }
        private void txtCarryOutMaterial_Leave(object sender, EventArgs e)
        {
            txtCarryOutMaterial.Text = txtCarryOutMaterial.Text.Trim().ToUpper();
        }
        private void txtCarryOutMaterial_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!IsLoading)
                {
                    objVehInOut.OutCarryMaterial  = Convert.ToString(txtCarryOutMaterial.Text.Trim());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private void txtCarryOutMaterial_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtPUC_Enter(object sender, EventArgs e)
        {
            GeneralMethods.Selection(txtPUC); 
        }
        private void txtPUC_KeyPress(object sender, KeyPressEventArgs e)
        {

        }
        private void txtPUC_Leave(object sender, EventArgs e)
        {
            txtPUC.Text = objVehInOut.PUCNo;
        }
        private void txtPUC_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!IsLoading)
                {
                    objVehInOut.PUCNo = Convert.ToString(txtPUC.Text.Trim());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void txtRCBook_Enter(object sender, EventArgs e)
        {
            GeneralMethods.Selection(txtRCBook); 
        }
        private void txtRCBook_KeyPress(object sender, KeyPressEventArgs e)
        {

        }
        private void txtRCBook_Leave(object sender, EventArgs e)
        {
            objVehInOut.RCBookNo = txtRCBook.Text;
            txtRCBook.Text = txtRCBook.Text.Trim().ToUpper();
        }
        private void txtRCBook_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!IsLoading)
                {
                    objVehInOut.RCBookNo = Convert.ToString(txtRCBook.Text.Trim());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void txtDriverLic_Enter(object sender, EventArgs e)
        {
            GeneralMethods.Selection(txtDriverLic); 
        }
        private void txtDriverLic_KeyPress(object sender, KeyPressEventArgs e)
        {

        }
        private void txtDriverLic_Leave(object sender, EventArgs e)
        {
            objVehInOut.DrLicNo = txtDriverLic.Text;
            txtDriverLic.Text = txtDriverLic.Text.Trim().ToUpper();
        }
        private void txtDriverLic_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!IsLoading)
                {
                    objVehInOut.DrLicNo  = Convert.ToString(txtDriverLic.Text.Trim());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void txtPersonWtVeh_Enter(object sender, EventArgs e)
        {
            GeneralMethods.Selection(txtPersonWtVeh);
        }
        private void txtPersonWtVeh_KeyPress(object sender, KeyPressEventArgs e)
        {

        }
        private void txtPersonWtVeh_Leave(object sender, EventArgs e)
        {
            txtPersonWtVeh.Text = Convert.ToString(objVehInOut.PersonsWithVeh);
        }
        private void txtPersonWtVeh_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!IsLoading)
                {
                    objVehInOut.PersonsWithVeh = Convert.ToInt16(txtPersonWtVeh.Text.Trim());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void txtNoOfInvCopyIn_Enter(object sender, EventArgs e)
        {
            GeneralMethods.Selection(txtNoOfInvCopyIn);  
        }
        private void txtNoOfInvCopyIn_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar) || (e.KeyChar == (char)Keys.Back) || (e.KeyChar == (char)Keys.Delete)))
                e.Handled = true;
        }
        private void txtNoOfInvCopyIn_Leave(object sender, EventArgs e)
        {
            txtNoOfInvCopyIn.Text = Convert.ToString(objVehInOut.InInvCnt);
        }
        private void txtNoOfInvCopyIn_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!IsLoading)
                {
                    objVehInOut.InInvCnt = Convert.ToInt16(txtNoOfInvCopyIn.Text.Trim());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void txtNoOfInvCopyOut_Enter(object sender, EventArgs e)
        {
            GeneralMethods.Selection(txtNoOfInvCopyOut);  
        }
        private void txtNoOfInvCopyOut_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar) || (e.KeyChar == (char)Keys.Back) || (e.KeyChar == (char)Keys.Delete)))
                e.Handled = true;
        }
        private void txtNoOfInvCopyOut_Leave(object sender, EventArgs e)
        {
            txtNoOfInvCopyOut.Text = Convert.ToString(objVehInOut.OutInvCnt);
        }
        private void txtNoOfInvCopyOut_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!IsLoading)
                {
                    objVehInOut.OutInvCnt = Convert.ToInt16(txtNoOfInvCopyOut.Text.Trim());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void chkPUC_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPUC.Checked == true)
            {
                objVehInOut.PUCFlg = "Y";
            }
            else if (chkPUC.Checked == false)
            {
                objVehInOut.PUCFlg = "N";
            }
        }
        private void chkRCBook_CheckedChanged(object sender, EventArgs e)
        {
            if (chkRCBook.Checked == true)
            {
                objVehInOut.RCBookFlg = "Y";
            }
            else if (chkRCBook.Checked == false)
            {
                objVehInOut.RCBookFlg = "N";
            }
        }
        private void chkFinance_CheckedChanged(object sender, EventArgs e)
        {
            if (chkFinance.Checked == true)
            {
                objVehInOut.Finance = "Y";
            }
            else if (chkFinance.Checked == false)
            {
                objVehInOut.Finance = "N";
            }
        }
        private void chkFitness_CheckedChanged(object sender, EventArgs e)
        {
            if (chkFitness.Checked == true)
            {
                objVehInOut.FitnessFlg= "Y";
            }
            else if (chkFitness.Checked == false)
            {
                objVehInOut.FitnessFlg = "N";
            }
        }
        private void chkDrivingLic_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDrivingLic.Checked == true)
            {
                objVehInOut.DrLicFlg = "Y";
            }
            else if (chkDrivingLic.Checked == false)
            {
                objVehInOut.DrLicFlg = "N";
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                bool flgApplyEdit;
                flgApplyEdit = VehInOutManager.Save(objVehInOut);
                if (flgApplyEdit)
                {
                    // instance the event args and pass it value
                    VehInOutUpdateEventArgs args = new VehInOutUpdateEventArgs(objVehInOut.Dbid, objVehInOut.EntryNo, objVehInOut.VehNo, objVehInOut.EntryDate, objVehInOut.DriverName, objVehInOut.InDate, objVehInOut.InTime, objVehInOut.OutDate, objVehInOut.OutTime, objVehInOut.Type, objVehInOut.InOut);

                    // raise event wtth  updated 
                    if (Entry_DataChanged != null)
                    {
                        if (this.IsNew)
                        {
                            Entry_DataChanged(this, args, DataEventType.INSERT_EVENT);
                        }
                        else
                        {
                            Entry_DataChanged(this, args, DataEventType.UPDATE_EVENT);
                        }
                    }

                    this.Close();
                }
                else
                {
                    MessageBox.Show("Record Not Saved.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                //this.Dispose();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
       
        private void cboEntryType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!IsLoading)
                {
                    if (cboEntryType.Text == "COMPANY")
                    { objVehInOut.Type = 4; }
                    else if (cboEntryType.Text == "IN/OUT OTHER")
                    { objVehInOut.Type = 1; }
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void cboEntryType_Enter(object sender, EventArgs e)
        {
            GeneralMethods.Selection(cboEntryType);
        }

        private void cboEntryType_LocationChanged(object sender, EventArgs e)
        {
            try
            {
                if (!IsLoading)
                {
                    if (cboEntryType.Text == "COMPANY")
                    { objVehInOut.Type = 4; }
                    else if (cboEntryType.Text == "IN/OUT OTHER")
                    { objVehInOut.Type = 1; }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void cboEntryType_Leave(object sender, EventArgs e)
        {
            //objVehInOut.VehNo = cboVehicleNo.Text.Trim().ToUpper();
            if (cboEntryType.Text == "COMPANY")
            { objVehInOut.Type = 4; }
            else if (cboEntryType.Text == "IN/OUT OTHER")
            { objVehInOut.Type = 1; }
        }

        private void cboEntryType_TextChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    if (!IsLoading)
            //    {
            //        if (cboEntryType.Text == "COMPANY")
            //        { objVehInOut.Type = 4; }
            //        else if (cboEntryType.Text == "IN/OUT OTHER")
            //        { objVehInOut.Type = 1; }

            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //}
        }

        private void txtInReading_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!IsLoading)
                {
                    objVehInOut.InReading = Convert.ToInt32(txtInReading.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void txtOutReading_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!IsLoading)
                {
                    objVehInOut.OutReading = Convert.ToInt32(txtOutReading.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void txtOutReading_Enter(object sender, EventArgs e)
        {
            GeneralMethods.Selection(txtOutReading);
        }

        private void txtOutReading_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar) || (e.KeyChar == (char)Keys.Back) || (e.KeyChar == (char)Keys.Delete)))
                e.Handled = true;
        }

        private void txtOutReading_Leave(object sender, EventArgs e)
        {
            txtOutReading.Text = Convert.ToString(objVehInOut.OutReading);
        }

        private void txtInReading_Enter(object sender, EventArgs e)
        {
            GeneralMethods.Selection(txtInReading);
        }

        private void txtInReading_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar) || (e.KeyChar == (char)Keys.Back) || (e.KeyChar == (char)Keys.Delete)))
                e.Handled = true;
        }

        private void txtInReading_Leave(object sender, EventArgs e)
        {
            txtInReading.Text = Convert.ToString(objVehInOut.InReading);
        }

        private void cboPlant_KeyPress(object sender, KeyPressEventArgs e)
        {
            GeneralMethods.AutoComplete(cboPlant, e, true);
        }

        private void cboEntryType_KeyPress(object sender, KeyPressEventArgs e)
        {
            GeneralMethods.AutoComplete(cboEntryType, e, true);
        }

        private void txtOutDate_Enter(object sender, EventArgs e)
        {
            txtOutDate.SelectAll();
        }

        private void txtOutDate_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!IsLoading)
                {
                    DateTime dt;
                    if (GeneralMethods.IsDate(txtOutDate.Text, out dt) & txtOutDate.Text.Trim().Length == 10)
                    {
                        objVehInOut.OutDate = Convert.ToDateTime(txtOutDate.Text);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void txtOutDate_Leave(object sender, EventArgs e)
        {
            if (objVehInOut.OutDate != DateTime.MinValue)
            {
                txtOutDate.Text = objVehInOut.OutDate.ToShortDateString();
            }
        }

        private void txtInDate_Enter(object sender, EventArgs e)
        {
            txtInDate.SelectAll();
        }

        private void txtInDate_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!IsLoading)
                {
                    DateTime dt;
                    if (GeneralMethods.IsDate(txtInDate.Text, out dt) & txtInDate.Text.Trim().Length == 10)
                    {
                        objVehInOut.InDate = Convert.ToDateTime(txtInDate.Text);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void txtInDate_Leave(object sender, EventArgs e)
        {
            if (objVehInOut.InDate != DateTime.MinValue)
            {
                txtInDate.Text = objVehInOut.InDate.ToShortDateString();
            }
        }
        #endregion
    }

    public class VehInOutUpdateEventArgs : System.EventArgs
    {
        private int mDBID;
        private int mEntryNo;
        private string mVehicleNo;
        private DateTime mEntryDate;
        private string mDriverName;
        private DateTime mInDate;
        private DateTime mInTime;
        private DateTime mOutDate;
        private DateTime mOutTime;
        private int mEntryType;
        private int mInOUt;

        public VehInOutUpdateEventArgs(int sDBID, int sEntryNo, string sVehicleNo,DateTime sEntryDate, string sDriverName, DateTime sInDate, DateTime sInTime, DateTime sOutdate, DateTime sOutTime, int sEntryType, int sInOut)
        {
            this.mDBID = sDBID;
            this.mEntryNo = sEntryNo;
            this.mVehicleNo = sVehicleNo;
            this.mEntryDate = sEntryDate;
            this.mDriverName = sDriverName;
            this.mInDate = sInDate;
            this.mInTime = sInTime;
            this.mOutDate = sOutdate;
            this.mOutTime = sOutTime;
            this.mEntryType = sEntryType;
            this.mInOUt = sInOut;
        }

        public int DBID
        {
            get
            {
                return mDBID;
            }
        }

        public int EntryNo
        {
            get
            {
                return mEntryNo;
            }
        }

        public string VehicleNo
        {
            get
            {
                return mVehicleNo;
            }
        }

        public DateTime EntryDate
        {
            get
            {
                return mEntryDate;
            }
        }

        public string DriverName
        {
            get
            {
                return mDriverName;
            }
        }
        public DateTime InDate
        {
            get
            {
                return mInDate;
            }
        }
        public DateTime InTime
        {
            get
            {
                return mInTime;
            }
        }
        public DateTime OutDate
        {
            get
            {
                return mOutDate;
            }
        }
        public DateTime OutTime
        {
            get
            {
                return mOutTime;
            }
        }

        public int EntryType
        {
            get
            {
                return mEntryType;
            }
        }

        public int InOUt
        {
            get
            {
                return mInOUt;
            }
        }
    }
}
