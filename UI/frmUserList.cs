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
    public partial class frmUserList : Form
    {
        #region Private Variable(s)
        private bool flgLoading;
        private bool flgList;
        private bool flgListCancel;
        
        private int dbid;
        private string loginName;
        private string role;
        private int ActiveStatus;

        private UserCompany currentCompany;
        private User currentUser;
        private UIRights objUIRights;
        #endregion

        #region Public Properties
        public bool IsList
        {
            get
            {
                return flgList;
            }
            set
            {
                flgList = value;
            }
        }

        public bool IsListCancel
        {
            get
            {
                return flgListCancel;
            }
            set
            {
                flgListCancel = value;
            }
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
        #endregion

        #region Constructor(s)
        public frmUserList()
        {
            InitializeComponent();
            InitializeListView();
        }

        public frmUserList(UserCompany currentCompany, User currentUser)
        {
            this.currentCompany = currentCompany;
            this.currentUser = currentUser;

            objUIRights = new UIRights();

            InitializeComponent();
            InitializeListView();

            GeneralMethods.FormAuthenticate(this.Name, currentCompany, currentUser);
            objUIRights.AddRight = GeneralMethods.frmAddRight;
            objUIRights.ModifyRight = GeneralMethods.frmModifyRight;
            objUIRights.ViewRight = GeneralMethods.frmViewRight;
            objUIRights.DeleteRight = GeneralMethods.frmDeleteRight;
            objUIRights.PrintRight = GeneralMethods.repPrintRight;
        }
        #endregion

        #region Context Menu
        private void contextMenu_Opening(object sender, CancelEventArgs e)
        {
            if (!IsList)
            {
                if (lvwUsers.SelectedItems != null && lvwUsers.SelectedItems.Count != 0)
                {
                    modifyToolStripMenuItem.Visible = true;
                    newToolStripMenuItem.Enabled = false;
                    toolStripSeparator1.Visible = true;
                    deleteToolStripMenuItem.Visible = true;
                }
                else
                {
                    modifyToolStripMenuItem.Visible = false;
                    newToolStripMenuItem.Enabled = true;
                    toolStripSeparator1.Visible = false;
                    deleteToolStripMenuItem.Visible = false;
                }
            }
        }

        private void modifyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (lvwUsers.SelectedItems != null && lvwUsers.SelectedItems.Count == 1)
                {
                    if (IsList)
                    {
                        btnOk_Click(sender, e);
                    }
                    else
                    {
                        if (objUIRights.ModifyRight)
                        {
                            User objUser;
                            frmUserProp objFrmUser;

                            objUser = UserManager.GetItem(Convert.ToInt32(lvwUsers.SelectedItems[0].Name));
                            if (objUser != null)
                            {
                                objFrmUser = new frmUserProp(objUser, currentUser);
                                objFrmUser.IsNew = false;
                                objFrmUser.MdiParent = this.MdiParent;
                                objFrmUser.Entry_DataChanged += new frmUserProp.UserUpdateHandler(Entry_DataChanged);
                                objFrmUser.Show();
                            }
                            else
                            {
                                MessageBox.Show(this, "Record Can't be Displayed due to Null Object.", System.Reflection.Assembly.GetExecutingAssembly().GetName().Name, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        else
                        {
                            throw new Exception("Not Authorised.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsList)
                {
                    if (objUIRights.AddRight)
                    {
                        User objUser = new User();
                        frmUserProp objFrmUser = new frmUserProp(objUser, currentUser);

                        objFrmUser.IsNew = true;
                        objFrmUser.MdiParent = this.MdiParent;
                        objFrmUser.Entry_DataChanged += new frmUserProp.UserUpdateHandler(Entry_DataChanged);
                        objFrmUser.Show();
                    }
                    else
                    {
                        throw new Exception("Not Authorised.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (lvwUsers.SelectedItems != null && lvwUsers.SelectedItems.Count == 1)
                {
                    if (!IsList)
                    {
                        if (objUIRights.DeleteRight)
                        {
                            DialogResult dr = new DialogResult();
                            dr = MessageBox.Show("Do You Want to Delete Record?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                            if (dr == DialogResult.Yes)
                            {
                                User objUser = new User();
                                objUser = UserManager.GetItem(Convert.ToInt32(lvwUsers.SelectedItems[0].Name));
                                UserManager.Delete(objUser);
                                lvwUsers.Items.Remove(lvwUsers.SelectedItems[0]);
                            }
                        }
                        else
                        {
                            throw new Exception("Not Authorised.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        #endregion

        #region Private Methods
        private void InitializeListView()
        {
            lvwUsers.ContextMenuStrip = contextMenu;
            lvwUsers.View = View.Details;
            lvwUsers.GridLines = true;
            lvwUsers.FullRowSelect = true;
            lvwUsers.MultiSelect = false;
        }

        private void FillList()
        {
            UserList objList = null;
            objList = UserManager.GetList(ActiveStatus);

            lvwUsers.Items.Clear();
            foreach (User objUser in objList)
            {
                ListViewItem lvItem = new ListViewItem();
                lvItem.Name = Convert.ToString(objUser.DBID);
                lvItem.Text = objUser.LoginName;
                lvItem.SubItems.Add(objUser.Role);

                lvwUsers.Items.Add(lvItem);
            }
        }

        private void SetButtonVisibility()
        {
            btnOk.Visible = IsList;
            btnCancel.Visible = IsList;
            btnNew.Visible = !IsList;
        }

        private void Entry_DataChanged(object sender, UserUpdateEventArgs e, DataEventType Action)
        {
            ListViewItem lvItem = null;

            switch (Action)
            {
                case DataEventType.INSERT_EVENT:
                    lvItem = new ListViewItem();
                    lvItem.Name = Convert.ToString(e.DBID);
                    lvItem.Text = e.LoginName;
                    lvItem.SubItems.Add(e.Role);

                    lvwUsers.Items.Add(lvItem);
                    lvwUsers.EnsureVisible(lvItem.Index);
                    break;

                case DataEventType.UPDATE_EVENT:
                    lvItem = lvwUsers.Items[lvwUsers.SelectedItems[0].Index];
                    lvItem.Text = e.LoginName;
                    lvItem.SubItems[1].Text = e.Role;

                    lvwUsers.EnsureVisible(lvItem.Index);
                    break;
            }
        }
        #endregion

        #region UI Control Logic
        private void frmUserList_Load(object sender, EventArgs e)
        {
            this.Icon = new Icon("Images/DTPL.ico");
            flgLoading = true;
            chkActive.Checked = true;
            FillList();
            SetButtonVisibility();
            flgLoading = false;
        }

        private void lvwUsers_DoubleClick(object sender, EventArgs e)
        {
            if(lvwUsers.SelectedItems != null && lvwUsers.SelectedItems.Count == 1)
            {
                modifyToolStripMenuItem_Click(sender, e);
            }
        }

        private void lvwUsers_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ( e.KeyChar == (char)13)
            {
                lvwUsers_DoubleClick (sender, e);
            }
        }

        private void chkActive_CheckedChanged(object sender, EventArgs e)
        {
            if (chkActive.Checked == true)
            {
                ActiveStatus = 1;
            }
            else
            {
                ActiveStatus = 0;
            }

            if (!flgLoading)
            {
                FillList();
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (IsList && lvwUsers.SelectedItems != null && lvwUsers.SelectedItems.Count == 1)
            {
                dbid = Convert.ToInt32(lvwUsers.SelectedItems[0].Name);
                loginName = lvwUsers.SelectedItems[0].Text;
                role = lvwUsers.SelectedItems[0].SubItems[1].Text;
                flgListCancel = false;
            }
            else
            {
                btnCancel_Click(sender, e);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (IsList)
            {
                dbid = 0;
                loginName = string.Empty;
                role = string.Empty;
                flgListCancel = true;
            }
            this.Close();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            newToolStripMenuItem_Click(sender, e);
        }
        #endregion
    }
}