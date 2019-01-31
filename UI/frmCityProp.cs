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
    public partial class frmCityProp : Form
    {
        #region Private Variable
        private bool flgNew;
        private bool flgLoading;

        private City objCity;
        private User objCurrentUser;
        #endregion

        #region Constructor
        public frmCityProp()
        {
            InitializeComponent();
        }
        public frmCityProp(City objCity, User objUser)
        {
            this.objCity = objCity;
            this.objCurrentUser = objUser;
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
        public delegate void CityUpdateHandler(object sender, CityUpdateEventArgs e, DataEventType Action);

        public event CityUpdateHandler Entry_DataChanged;
        #endregion

        #region Private Methods
        private void EnableDisableSave()
        {
            btnSave.Enabled = objCity.IsValid;
        }

        private void City_OnValid(object sender, EventArgs e)
        {
            EnableDisableSave();
        }

        private void City_OnInValid(object sender, EventArgs e)
        {
            EnableDisableSave();
        }

        private void SubscribeToEvents()
        {
            objCity.OnValid += new City.EventHandler(City_OnValid);
            objCity.OnInvalid += new City.EventHandler(City_OnInValid);
        }
        #endregion

        #region UI Control Logic
        private void frmCityProp_Load(object sender, EventArgs e)
        {
            this.Icon = new Icon("Images/DTPL.ico");
            flgLoading = true;
            City_OnInValid(sender, e);

            if (objCity.IsNew)
            {
                this.Text += " [ NEW ]";
            }
            else
            {
                this.Text += " [ " + objCity.mCity + " ]";
            }
            txtCity.Text = objCity.mCity;
        
            SubscribeToEvents();
            flgLoading = false;
            txtCity.Focus();
        }
        
        private void frmCityProp_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void txtCity_Enter(object sender, EventArgs e)
        {
            GeneralMethods.Selection(txtCity);  
        }

        private void txtCity_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtCity_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!IsLoading)
                {
                    objCity.mCity  = Convert.ToString(txtCity.Text.Trim());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void txtCity_Leave(object sender, EventArgs e)
        {
            txtCity.Text = objCity.mCity;
        }
        
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                bool flgApplyEdit;
                flgApplyEdit = CityManager.Save(objCity, objCurrentUser);
                if (flgApplyEdit)
                {
                    // instance the event args and pass it value
                    CityUpdateEventArgs args = new CityUpdateEventArgs(objCity.DBID, objCity.mCity);

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

    public class CityUpdateEventArgs : System.EventArgs
    {
        private int mDBID;
        private string mdCity;
        
        public CityUpdateEventArgs(int sDBID, string sCity)
        {
            this.mDBID = sDBID;
            this.mdCity = sCity ;
        }

        public int DBID
        {
            get
            {
                return mDBID;
            }
        }

        public string mDCity
        {
            get
            {
                return mdCity;
            }
        }
               
    }
}
