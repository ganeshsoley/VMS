using System;
using System.Drawing;
using System.Windows.Forms;
using BLL;
using EntityObject;
using EntityObject.Enum;

namespace UI
{
    public partial class frmEmpProp : Form
    {
        #region Private Variable(s)
        private bool flgNew;
        private bool flgLoading;

        private Employee objEmp;
        private User objUser;
        #endregion

        #region Constructor
        public frmEmpProp()
        {
            InitializeComponent();
        }

        public frmEmpProp(Employee objEmp, User curUser)
        {
            this.objEmp = objEmp;
            objUser = curUser;
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
        public delegate void EmpUpdateHandler(object sender, EmpUpdateEventArgs e, DataEventType Action);

        public event EmpUpdateHandler Entry_DataChanged;
        #endregion

        #region Private Methods
        private void EnableDisableSave()
        {
            btnSave.Enabled = objEmp.IsValid;
        }

        private void Emp_OnValid(object sender, EventArgs e)
        {
            EnableDisableSave();
        }

        private void Emp_OnInValid(object sender, EventArgs e)
        {
            EnableDisableSave();
        }

        private void SubscribeToEvents()
        {
            objEmp.OnValid += new Employee.EventHandler(Emp_OnValid);
            objEmp.OnInvalid += new Employee.EventHandler(Emp_OnInValid);
        }
        #endregion

        private void frmEmpProp_Load(object sender, EventArgs e)
        {
            this.Icon = new Icon("Images/DTPL.ico");
            flgLoading = true;
            Emp_OnInValid(sender, e);

            FillPlantList();

            if (objEmp.IsNew)
            {
                this.Text += " [ NEW ]";
            }
            else
            {
                this.Text += " [ " + objEmp.Initials + " ]";
                txtEmpCode.Enabled = false;
            }
            txtEmpCode.Text = objEmp.EmpCode;
            txtFName.Text = objEmp.FirstName;
            txtMidName.Text = objEmp.MiddleName;
            txtLName.Text = objEmp.LastName;
            lblInitials.Text = objEmp.Initials;
            lblDept.Text = objEmp.DeptName;
            txtEMail.Text = objEmp.EMailID;
            txtMobileNo.Text = objEmp.MobileNo;
            cboPlant.Text = objEmp.EmpPlant;

            if (objEmp.InActive == 1)
                chkInActive.Checked = true;
            else
                chkInActive.Checked = false;

            SubscribeToEvents();
            flgLoading = false;
        }

        private void frmEmpProp_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void txtEmpCode_Enter(object sender, EventArgs e)
        {
            GeneralMethods.Selection(txtEmpCode);
        }

        private void txtEmpCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsLetter(e.KeyChar) || Char.IsNumber(e.KeyChar) || (e.KeyChar == (char)Keys.Back) || (e.KeyChar == (char)Keys.Delete)))
                e.Handled = true;
        }

        private void txtEmpCode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!IsLoading)
                {
                    objEmp.EmpCode = txtEmpCode.Text.Trim();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void txtEmpCode_Leave(object sender, EventArgs e)
        {
            txtEmpCode.Text = objEmp.EmpCode;
        }

        private void txtFName_Enter(object sender, EventArgs e)
        {
            GeneralMethods.Selection(txtFName);
        }

        private void txtFName_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyValue >= 48 & e.KeyValue <= 57)
            //    e.SuppressKeyPress = true;
            //else
            //    e.SuppressKeyPress = false;
        }

        private void txtFName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsLetter(e.KeyChar) || (e.KeyChar == (char)Keys.Back) || (e.KeyChar == (char)Keys.Delete) || (e.KeyChar == (char)Keys.Space)))
                e.Handled = true;
        }

        private void txtFName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!IsLoading)
                {
                    objEmp.FirstName = txtFName.Text.Trim();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void txtFName_Leave(object sender, EventArgs e)
        {
            txtFName.Text = objEmp.FirstName;
            lblInitials.Text = objEmp.Initials;
        }

        private void txtMidName_Enter(object sender, EventArgs e)
        {
            GeneralMethods.Selection(txtMidName);
        }

        private void txtMidName_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyValue >= 48 & e.KeyValue <= 57)
            //    e.SuppressKeyPress = true;
            //else
            //    e.SuppressKeyPress = false;
        }

        private void txtMidName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsLetter(e.KeyChar) || (e.KeyChar == (char)Keys.Back) || (e.KeyChar == (char)Keys.Delete) || (e.KeyChar == (char)Keys.Space)))
                e.Handled = true;
        }

        private void txtMidName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!IsLoading)
                {
                    objEmp.MiddleName = txtMidName.Text.Trim();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void txtMidName_Leave(object sender, EventArgs e)
        {
            txtMidName.Text = objEmp.MiddleName;
            lblInitials.Text = objEmp.Initials;
        }

        private void txtLName_Enter(object sender, EventArgs e)
        {
            GeneralMethods.Selection(txtLName);
        }

        private void txtLName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsLetter(e.KeyChar) || (e.KeyChar == (char)Keys.Back) || (e.KeyChar == (char)Keys.Delete) || (e.KeyChar == (char)Keys.Space)))
                e.Handled = true;
        }

        private void txtLName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!IsLoading)
                {
                    objEmp.LastName = txtLName.Text.Trim();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void txtLName_Leave(object sender, EventArgs e)
        {
            txtLName.Text = objEmp.LastName;
            lblInitials.Text = objEmp.Initials;
        }

        private void btnDeptList_Click(object sender, EventArgs e)
        {
            frmDeptList frmList = new frmDeptList();
            frmList.IsList = true;
            frmList.ShowDialog();

            if (!frmList.IsListCancel)
            {
                objEmp.DeptID = frmList.DBID;
                objEmp.DeptName = frmList.DeptName;

                lblDept.Text = objEmp.DeptName;
                SendKeys.Send("{TAB}");
            }
            frmList.Dispose();
        }

        private void txtEMail_Enter(object sender, EventArgs e)
        {
            GeneralMethods.Selection(txtEMail);
        }

        private void txtEMail_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!IsLoading)
                {
                    objEmp.EMailID = txtEMail.Text.Trim();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void txtEMail_Leave(object sender, EventArgs e)
        {
            txtEMail.Text = objEmp.EMailID;
        }

        private void txtMobileNo_Enter(object sender, EventArgs e)
        {
            GeneralMethods.Selection(txtMobileNo);
        }

        private void txtMobileNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar) || (e.KeyChar == (char)Keys.Back) || (e.KeyChar == (char)Keys.Delete)))
                e.Handled = true;
        }

        private void txtMobileNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!IsLoading)
                {
                    objEmp.MobileNo = txtMobileNo.Text.Trim();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void txtMobileNo_Leave(object sender, EventArgs e)
        {
            txtMobileNo.Text = objEmp.MobileNo;
        }

        private void cboPlant_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!IsLoading)
                {
                    objEmp.EmpPlant = cboPlant.Text;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void chkInActive_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (!IsLoading)
                {
                    if (chkInActive.Checked)
                        objEmp.InActive = 1;
                    else
                        objEmp.InActive = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                bool flgApplyEdit;
                flgApplyEdit = EmployeeManager.Save(objEmp, objUser);
                if (flgApplyEdit)
                {
                    // instance the event args and pass it value
                    EmpUpdateEventArgs args = new EmpUpdateEventArgs(objEmp.DBID, objEmp.Initials, objEmp.EmpCode, objEmp.DeptName, objEmp.EmpPlant);

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
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void FillPlantList()
        {
            cboPlant.Items.Add("DTPL 100");
            cboPlant.Items.Add("DTPL 102");
            cboPlant.Items.Add("DTPL 103");
        }
    }

    public class EmpUpdateEventArgs : System.EventArgs
    {
        private long mDBID;
        private string mInitials;
        private string mEmpCode;
        private string mDeptName;
        private string mEmpPlant;

        public EmpUpdateEventArgs(long sDBID, string sInitials, string sEmpCode, string sDeptName, string sEmpPlant)
        {
            this.mDBID = sDBID;
            this.mInitials = sInitials;
            this.mEmpCode = sEmpCode;
            this.mDeptName = sDeptName;
            this.mEmpPlant = sEmpPlant;
        }

        public long DBID
        {
            get
            {
                return mDBID;
            }
        }

        public string Initials
        {
            get
            {
                return mInitials;
            }
        }

        public string DeptName
        {
            get
            {
                return mDeptName;
            }
        }

        public string EmpCode
        {
            get
            {
                return mEmpCode;
            }
        }

        public string EmpPlant
        {
            get
            {
                return mEmpPlant;
            }
        }
    }
}
