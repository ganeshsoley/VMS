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
    public partial class frmVehicleProp : Form
    {
        #region Private Variable
         private bool flgNew;
         private bool flgLoading;

         private Vehicle objVehicle;
        private User objUser;
        #endregion

        #region Constructor
        public frmVehicleProp()
        {
            InitializeComponent();
        }
        public frmVehicleProp(Vehicle objVehicle, User currentUser)
        {
            this.objVehicle = objVehicle;
            objUser = currentUser;
            InitializeComponent();
        }
        #endregion

        /// <summary>
        /// Contains Public Properties of Form that are accessible through out the Project.
        /// </summary>
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
        #endregion

        /// <summary>
        /// Contains Delegates and events available on Form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="Action"></param>
        #region Delegate & Event
        public delegate void VehicleUpdateHandler(object sender, VehicleUpdateEventArgs e, DataEventType Action);

        public event VehicleUpdateHandler Entry_DataChanged;
        #endregion

        #region Private Methods
        private void EnableDisableSave()
        {
            btnSave.Enabled = objVehicle.IsValid;
        }

        private void Vehicle_OnValid(object sender, EventArgs e)
        {
            EnableDisableSave();
        }

        private void Vehicle_OnInValid(object sender, EventArgs e)
        {
            EnableDisableSave();
        }

        private void SubscribeToEvents()
        {
            objVehicle.OnValid += new Vehicle.EventHandler(Vehicle_OnValid);
            objVehicle.OnInvalid += new Vehicle.EventHandler(Vehicle_OnInValid);
        }
        #endregion

      
        #region UI Control Logic
        private void frmVehicleProp_Load(object sender, EventArgs e)
        {
            this.Icon = new Icon("Images/DTPL.ico");
            flgLoading = true;
            Vehicle_OnInValid(sender, e);

            if (objVehicle.IsNew)
            {
                this.Text += " [ NEW ]";
            }
            else
            {
                this.Text += " [ " + objVehicle.VehicleNo + " ]";
            }
            txtVehicleNo.Text = objVehicle.VehicleNo;
            txtLicenseNo.Text = objVehicle.VLicencseNo;
            if (objVehicle.PUCExpiry != DateTime.MinValue)
                txtPUCExpiry.Text = objVehicle.PUCExpiry.ToShortDateString();

            if (objVehicle.IsActive==1)
                chkIsActive.Checked = true;
            else
                chkIsActive.Checked = false;

            SubscribeToEvents();
            flgLoading = false;
        }

        private void frmVehicleProp_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void txtVehicleNo_Enter(object sender, EventArgs e)
        {
            GeneralMethods.Selection(txtVehicleNo); 
        }

        private void txtVehicleNo_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtVehicleNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!IsLoading)
                {
                    objVehicle.VehicleNo = Convert.ToString(txtVehicleNo.Text.Trim());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void txtVehicleNo_Leave(object sender, EventArgs e)
        {
            txtVehicleNo.Text = objVehicle.VehicleNo;
        }

        private void txtLicenseNo_Enter(object sender, EventArgs e)
        {
            GeneralMethods.Selection(txtLicenseNo); 
        }

        private void txtLicenseNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void txtLicenseNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!IsLoading)
                {
                    objVehicle.VLicencseNo = Convert.ToString(txtLicenseNo.Text.Trim());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void txtLicenseNo_Leave(object sender, EventArgs e)
        {
            txtLicenseNo.Text = objVehicle.VLicencseNo;
        }

        private void txtPUCExpiry_Enter(object sender, EventArgs e)
        {
            GeneralMethods.Selection(txtPUCExpiry);  
        }

        private void txtPUCExpiry_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtPUCExpiry_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!IsLoading)
                {
                    if (txtPUCExpiry.Text.Trim().Length == 10)
                        objVehicle.PUCExpiry = Convert.ToDateTime(txtPUCExpiry.Text.Trim());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void txtPUCExpiry_Leave(object sender, EventArgs e)
        {
            if (objVehicle.PUCExpiry != DateTime.MinValue)
                txtPUCExpiry.Text = objVehicle.PUCExpiry.ToShortDateString();
        }

        private void chkIsActive_Leave(object sender, EventArgs e)
        {
            if (!IsLoading)
            {
                if (chkIsActive.Checked)
                    objVehicle.IsActive = 1;
                else
                    objVehicle.IsActive = 0;
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                bool flgApplyEdit;
                flgApplyEdit = VehicleManager.Save(objVehicle, objUser);
                if (flgApplyEdit)
                {
                    // instance the event args and pass it value
                    VehicleUpdateEventArgs args = new VehicleUpdateEventArgs(objVehicle.Dbid, objVehicle.VehicleNo, objVehicle.VLicencseNo,objVehicle.PUCExpiry);

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
        #endregion

     
    }
    public class VehicleUpdateEventArgs : System.EventArgs
    {
        private int mDBID;
        private string mVehicleNo;
        private string mVLicenceNo;
        private DateTime mPUCExpiry;

        public VehicleUpdateEventArgs(int sDBID, string sVehicleNo, string sVLicenceNo, DateTime sPUCExpiry)
        {
            this.mDBID = sDBID;
            this.mVehicleNo  = sVehicleNo;
            this.mVLicenceNo = sVLicenceNo;
            this.mPUCExpiry = sPUCExpiry;
        }

        public int DBID
        {
            get
            {
                return mDBID;
            }
        }

        public string VehicleNo
        {
            get
            {
                return mVehicleNo ;
            }
        }

        public string VLicenceNo
        {
            get
            {
                return mVLicenceNo;
            }
        }

        public DateTime PUCExpiry
        {
            get
            {
                return mPUCExpiry;
            }
        }
    }
}
