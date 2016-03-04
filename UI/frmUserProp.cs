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
    public partial class frmUserProp : Form
    {
        #region Private Variable(s)
        private bool flgNew;
        private bool flgLoading;

        private User objUser;
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
        #endregion
        
        #region Constructor(s)
        public frmUserProp()
        {
            InitializeComponent();
        }

        public frmUserProp(User objUser)
        {
            this.objUser = objUser;
            InitializeComponent();
        }
        #endregion

        #region Private Method(s)
        private void EnableDisableSave()
        {
            btnSave.Enabled = objUser.IsValid;
        }

        private void User_OnValid(object sender, EventArgs e)
        {
            EnableDisableSave();
        }

        private void User_OnInvalid(object sender, EventArgs e)
        {
            EnableDisableSave();
        }

        private void SubscribeToEvents()
        {
            objUser.OnValid += new User.EventHandler(User_OnValid);
            objUser.OnInvalid += new BrokenRule.EventHandler(User_OnInvalid);
        }

        private void FillRole()
        {
            cboRole.Items.Add("ADMINISTRATOR");
            cboRole.Items.Add("USER");
            cboRole.Items.Add("SYSADMIN");
        }

        private void FillDept()
        {
            cboDept.Items.Add("ADMIN");
            cboDept.Items.Add("ACCOUNTS");
            cboDept.Items.Add("BSR");
            cboDept.Items.Add("DESIGN");
            cboDept.Items.Add("CABLE");
            cboDept.Items.Add("CABLE CUTTING");
            cboDept.Items.Add("EXCISE");
            cboDept.Items.Add("FINAL ASSEMBLY");
            cboDept.Items.Add("HR");
            cboDept.Items.Add("MARKETING");
            cboDept.Items.Add("PURCHASE");
            cboDept.Items.Add("STORE");
            cboDept.Items.Add("MAINTENANCE");
            cboDept.Items.Add("MAINGATE");
            cboDept.Items.Add("PRODUCTION");
            cboDept.Items.Add("IQA");
            cboDept.Items.Add("IT");
            cboDept.Items.Add("SYSTEM");
            cboDept.Items.Add("GUEST");
            cboDept.Items.Add("STAMPING");
        }
        #endregion

        #region Delegates & Events
        public delegate void UserUpdateHandler(object sender, UserUpdateEventArgs e, DataEventType Action);

        public event UserUpdateHandler Entry_DataChanged;
        #endregion

        #region UI Control Logic
        private void frmUserProp_Load(object sender, EventArgs e)
        {
            this.Icon = new Icon("Images/DTPL.ico");
            flgLoading = true;
            User_OnInvalid(sender, e);
            FillRole();
            FillDept();

            txtName.Text = objUser.FirstName;
            txtLoginName.Text = objUser.LoginName;
            txtPassword.Text = objUser.Password;
            txtConfirmPwd.Text = objUser.Password;
            if (objUser.IsNew)
            {
                this.Text += " [ New ]";
                cboRole.SelectedIndex = -1;
                cboDept.SelectedIndex = -1;
            }
            else
            {
                this.Text += " [ " + objUser.LoginName + " ]";
                cboRole.Text = objUser.Role;
                cboDept.Text = objUser.Dept;
            }

            if (objUser.IsActive == 1)
            {
                chkIsActive.Checked = true;
            }
            else
            {
                chkIsActive.Checked = false;
            }

            if (objUser.DeptHead == 1)
            {
                chkDeptHead.Checked = true;
            }
            else
            {
                chkDeptHead.Checked = false;
            }

            SubscribeToEvents();
            flgLoading = false;
        }

        private void txtName_Enter(object sender, EventArgs e)
        {
            GeneralMethods.Selection(txtName);
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            if (!flgLoading)
            {
                objUser.FirstName = txtName.Text.Trim();
                objUser.LoginName = txtName.Text.Trim();

                txtLoginName.Text = txtName.Text;
            }
        }

        private void txtName_Leave(object sender, EventArgs e)
        {
            txtName.Text = objUser.FirstName;
        }

        private void txtPassword_Enter(object sender, EventArgs e)
        {
            GeneralMethods.Selection(txtPassword);
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            if (!flgLoading)
            {
                objUser.Password = txtPassword.Text.Trim();
            }
        }

        private void txtPassword_Leave(object sender, EventArgs e)
        {
            txtPassword.Text = objUser.Password;
        }

        private void txtPassword_Validating(object sender, CancelEventArgs e)
        {

        }

        private void txtConfirmPwd_Enter(object sender, EventArgs e)
        {
            GeneralMethods.Selection(txtConfirmPwd);
        }

        private void txtConfirmPwd_Validating(object sender, CancelEventArgs e)
        {
            e.Cancel = false;
            if (!txtConfirmPwd.Text.ToUpper().Equals(txtPassword.Text))
            {
                e.Cancel = true;
                MessageBox.Show(this, "Password Mismatch. Please Re-Enter Password.", System.Reflection.Assembly.GetExecutingAssembly().GetName().Name,MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
        }

        private void chkIsActive_CheckedChanged(object sender, EventArgs e)
        {
            if (!flgLoading)
            {
                if (chkIsActive.Checked)
                    objUser.IsActive = 1;
                else
                    objUser.IsActive = 0;
            }
        }

        private void chkDeptHead_CheckedChanged(object sender, EventArgs e)
        {
            if (!flgLoading)
            {
                if (chkDeptHead.Checked)
                    objUser.DeptHead = 1;
                else
                    objUser.DeptHead = 0;
            }
        }

        private void cboRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!flgLoading)
            {
                objUser.Role = cboRole.Text;
            }
        }

        private void cboRole_Validating(object sender, CancelEventArgs e)
        {
            if (cboRole.SelectedIndex == -1)
                MessageBox.Show("Select Role Properly.");
        }

        private void cboDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!flgLoading)
            {
                objUser.Dept = cboDept.Text;
            }
        }

        private void cboDept_Validating(object sender, CancelEventArgs e)
        {
            if (cboDept.SelectedIndex == -1)
                MessageBox.Show("Select Role Properly.");
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            bool flgApplyEdit;

            flgApplyEdit = UserManager.Save(objUser);

            if (flgApplyEdit)
            {
                UserUpdateEventArgs args = new UserUpdateEventArgs(objUser.DBID, objUser.LoginName, objUser.Role);

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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
    }

    public class UserUpdateEventArgs : EventArgs
    {
        private int dbid;
        private string loginName;
        private string role;

        public UserUpdateEventArgs(int pDBID, string pLoginName, string pRole)
        {
            dbid = pDBID;
            loginName = pLoginName;
            role = pRole;
        }

        public int DBID
        {
            get
            {
                return dbid;
            }
        }

        public string LoginName
        {
            get
            {
                return loginName;
            }
        }

        public string Role
        {
            get
            {
                return role;
            }
        }
    }

}
