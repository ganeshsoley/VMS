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
    public partial class frmDriverProp : Form
    {
        #region Private Variable
        private bool flgNew;
        private bool flgLoading;

        private Driver objDriver;
        private User currentUser;
        #endregion

        #region Constructor
        public frmDriverProp()
        {
            InitializeComponent();
        }
        public frmDriverProp(Driver objDriver, User objUser)
        {
            this.objDriver = objDriver;
            currentUser = objUser;
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
        public delegate void DriverUpdateHandler(object sender, DriverUpdateEventArgs e, DataEventType Action);

        public event DriverUpdateHandler Entry_DataChanged;
        #endregion

        #region Private Methods
        private void EnableDisableSave()
        {
            btnSave.Enabled = objDriver.IsValid;
        }

        private void Driver_OnValid(object sender, EventArgs e)
        {
            EnableDisableSave();
        }

        private void Driver_OnInValid(object sender, EventArgs e)
        {
            EnableDisableSave();
        }

        private void SubscribeToEvents()
        {
            objDriver.OnValid += new Driver.EventHandler(Driver_OnValid);
            objDriver.OnInvalid += new Driver.EventHandler(Driver_OnInValid);
        }
        #endregion

        

        #region UI Control Logic
        private void frmDriverProp_Load(object sender, EventArgs e)
        {
            this.Icon = new Icon("Images/DTPL.ico");
            flgLoading = true;
            Driver_OnInValid(sender, e);

            if (objDriver.IsNew)
            {
                this.Text += " [ NEW ]";
            }
            else
            {
                this.Text += " [ " + objDriver.Name + " ]";
            }
            txtName.Text = objDriver.Name;
            txtLicenseNo.Text = objDriver.LicenceNo;
            if (objDriver.IsActive == true)
                chkIsActive.Checked = true;
            else
                chkIsActive.Checked = false;

            SubscribeToEvents();
            flgLoading = false;
        }

        private void frmDriverProp_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void txtName_Enter(object sender, EventArgs e)
        {
            GeneralMethods.Selection(txtName); 
        }

        private void txtName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsLetter(e.KeyChar) || (e.KeyChar == (char)Keys.Back) || (e.KeyChar == (char)Keys.Delete) || (e.KeyChar == (char)Keys.Space)))
                e.Handled = true;
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!IsLoading)
                {
                    objDriver.Name = Convert.ToString(txtName.Text.Trim());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void txtName_Leave(object sender, EventArgs e)
        {
            txtName.Text = objDriver.Name;
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
                    objDriver.LicenceNo = Convert.ToString(txtLicenseNo.Text.Trim());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void txtLicenseNo_Leave(object sender, EventArgs e)
        {
            txtLicenseNo.Text = objDriver.LicenceNo; 
        }

        private void chkIsActive_Leave(object sender, EventArgs e)
        {
            if (!IsLoading)
            {
                if (chkIsActive.Checked)
                    objDriver.IsActive = true;
                else
                    objDriver.IsActive = false;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                bool flgApplyEdit;
                flgApplyEdit = DriverManager.Save(objDriver, currentUser);
                if (flgApplyEdit)
                {
                    // instance the event args and pass it value
                    DriverUpdateEventArgs args = new DriverUpdateEventArgs(objDriver.DBID, objDriver.Name, objDriver.LicenceNo);

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

    public class DriverUpdateEventArgs : System.EventArgs
    {
        private int mDBID;
        private string mName;
        private string mLicenceNo;

        public DriverUpdateEventArgs(int sDBID, string sName, string sLicenceNo)
        {
            this.mDBID = sDBID;
            this.mName = sName;
            this.mLicenceNo = sLicenceNo;
        }

        public int DBID
        {
            get
            {
                return mDBID;
            }
        }

        public string Name
        {
            get
            {
                return mName;
            }
        }

        public string LicenceNo
        {
            get
            {
                return mLicenceNo;
            }
        }
    }
}
