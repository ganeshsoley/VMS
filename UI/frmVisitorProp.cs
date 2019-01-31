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
    public partial class frmVisitorProp : Form
    {
        #region Private Variable(s)
        private bool flgNew;
        private bool flgLoading;

        private Visitor objVisitor;
        private User currentUser;
        #endregion

        #region Constructor(s)
        public frmVisitorProp()
        {
            InitializeComponent();
        }

        public frmVisitorProp(Visitor objVisitor, User objUser)
        {
            this.objVisitor = objVisitor;
            this.currentUser = objUser;

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
        public delegate void VisitorUpdateHandler(object sender, VisitorUpdateEventArgs e, DataEventType Action);

        public event VisitorUpdateHandler Entry_DataChanged;
        #endregion

        #region Private Methods
        private void EnableDisableSave()
        {
            btnSave.Enabled = objVisitor.IsValid;
        }

        private void Visitor_OnValid(object sender, EventArgs e)
        {
            EnableDisableSave();
        }

        private void Visitor_OnInValid(object sender, EventArgs e)
        {
            EnableDisableSave();
        }

        private void SubscribeToEvents()
        {
            objVisitor.OnValid += new Visitor.EventHandler(Visitor_OnValid);
            objVisitor.OnInvalid += new Visitor.EventHandler(Visitor_OnInValid);
        }

        private void FillIDProofs()
        {
            cboIDProofType.Items.Clear();
            cboIDProofType.Items.Add("PAN Card");
            cboIDProofType.Items.Add("AADHAR Card");
            cboIDProofType.Items.Add("Voter ID Card");
            cboIDProofType.Items.Add("Driving License");
            cboIDProofType.Items.Add("Passport");
        }

        private void FillCompany()
        {
            string[] CompanyList = VisitorManager.GetCompanys();

            cboCompany.Items.Clear();
            if (CompanyList != null)
            {
                foreach (string str in CompanyList)
                {
                    cboCompany.Items.Add(str);
                }
            }
        }
        #endregion

        private void frmVisitorProp_Load(object sender, EventArgs e)
        {
            this.Icon = new Icon("Images/DTPL.ico");
            flgLoading = true;
            Visitor_OnInValid(sender, e);
            FillCompany();
            FillIDProofs();

            if (objVisitor.IsNew)
            {
                this.Text += " [ NEW ]";
                //objVisitor.RegNo = VisitorManager.GetVRegNo();
                //lblRegNo.Text = Convert.ToString(objVisitor.RegNo);
            }
            else
            {
                this.Text += " [ " + objVisitor.VName + " ]";
                //lblRegNo.Text = Convert.ToString(objVisitor.RegNo);
            }
            txtVName.Text = objVisitor.VName;
            cboCompany.Text = objVisitor.Company;
            txtAddress.Text = objVisitor.Address;
            txtMobileNo.Text = objVisitor.MobileNo;
            cboIDProofType.Text = objVisitor.IDProofType;
            txtIDProofNo.Text = objVisitor.IDProofNo;

            SubscribeToEvents();
            flgLoading = false;
        }

        private void txtVName_Enter(object sender, EventArgs e)
        {
            GeneralMethods.Selection(txtVName);
        }

        private void txtVName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!IsLoading)
                {
                    objVisitor.VName= Convert.ToString(txtVName.Text.Trim());
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void txtVName_Leave(object sender, EventArgs e)
        {
            txtVName.Text = objVisitor.VName;
        }

        private void cboCompany_Enter(object sender, EventArgs e)
        {
            GeneralMethods.Selection(cboCompany);
        }

        private void cboCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!IsLoading)
                {
                    objVisitor.Company = Convert.ToString(cboCompany.Text.Trim());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void cboCompany_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!IsLoading)
                {
                    objVisitor.Company = Convert.ToString(cboCompany.Text.Trim());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void cboCompany_Leave(object sender, EventArgs e)
        {
            cboCompany.Text = objVisitor.Company;
        }

        private void txtAddress_Enter(object sender, EventArgs e)
        {
            GeneralMethods.Selection(txtAddress);
        }

        private void txtAddress_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!IsLoading)
                {
                    objVisitor.Address = Convert.ToString(txtAddress.Text.Trim());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void txtAddress_Leave(object sender, EventArgs e)
        {
            txtAddress.Text = objVisitor.Address;
        }

        private void txtMobileNo_Enter(object sender, EventArgs e)
        {
            GeneralMethods.Selection(txtMobileNo);
        }

        private void txtMobileNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!IsLoading)
                {
                    objVisitor.MobileNo= Convert.ToString(txtMobileNo.Text.Trim());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void txtMobileNo_Leave(object sender, EventArgs e)
        {
            txtMobileNo.Text = Convert.ToString(objVisitor.MobileNo);
        }

        private void cboIDProofType_Enter(object sender, EventArgs e)
        {
            GeneralMethods.Selection(cboIDProofType);
        }

        private void cboIDProofType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!IsLoading)
                {
                    objVisitor.IDProofType = Convert.ToString(cboIDProofType.Text.Trim());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void cboIDProofType_Leave(object sender, EventArgs e)
        {
            cboIDProofType.Text = objVisitor.IDProofType;
        }

        private void txtIDProofNo_Enter(object sender, EventArgs e)
        {
            GeneralMethods.Selection(txtIDProofNo);
        }

        private void txtIDProofNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!IsLoading)
                {
                    objVisitor.IDProofNo= Convert.ToString(txtIDProofNo.Text.Trim());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void txtIDProofNo_Leave(object sender, EventArgs e)
        {
            txtIDProofNo.Text = Convert.ToString(objVisitor.IDProofNo);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                bool flgApplyEdit;
                flgApplyEdit = VisitorManager.Save(objVisitor, currentUser);
                if (flgApplyEdit)
                {
                    // instance the event args and pass it value
                    VisitorUpdateEventArgs args = new VisitorUpdateEventArgs(objVisitor.DBID, Convert.ToString(objVisitor.RegNo), objVisitor.VName, objVisitor.Company, objVisitor.MobileNo);

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

        private void txtVName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsLetter(e.KeyChar) || (e.KeyChar == (char)Keys.Back) || (e.KeyChar == (char)Keys.Delete) || (e.KeyChar == (char)Keys.Space)))
                e.Handled = true;
        }

        private void txtMobileNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar) || (e.KeyChar == (char)Keys.Back) || (e.KeyChar == (char)Keys.Delete)))
                e.Handled = true;
        }

        private void frmVisitorProp_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void cboCompany_KeyPress(object sender, KeyPressEventArgs e)
        {
            GeneralMethods.AutoComplete(cboCompany, e, false);
        }

        private void cboIDProofType_KeyPress(object sender, KeyPressEventArgs e)
        {
            GeneralMethods.AutoComplete(cboIDProofType, e, true);
        }
    }

    public class VisitorUpdateEventArgs : System.EventArgs
    {
        private long mDBID;
        private string mRegNo;
        private string mVName;
        private string mVCompany;
        private string mContactNo;

        public VisitorUpdateEventArgs(long sDBID, string sRegNo, string sVName, string sVCompany, string sContactNo)
        {
            this.mDBID = sDBID;
            this.mRegNo = sRegNo;
            this.mVName = sVName;
            this.mVCompany = sVCompany;
            this.mContactNo = sContactNo;
        }

        public long DBID
        {
            get
            {
                return mDBID;
            }
        }

        public string RegNo
        {
            get
            {
                return mRegNo;
            }
        }

        public string VName
        {
            get
            {
                return mVName;
            }
        }

        public string Company
        {
            get
            {
                return mVCompany;
            }
        }

        public string ContactNo
        {
            get
            {
                return mContactNo;
            }
        }
    }
}
