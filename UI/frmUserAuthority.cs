using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EntityObject;
using BLL;

namespace UI
{
    public partial class frmUserAuthority : Form
    {
        #region Private Variable(s)
        private bool flgLoading;
        private bool updatingTreeView;
        private UserRightsList objRightsList;

        private UserCompany objUserCompany;
        private User objCurUser;
        private UIRights objUIRights;
        #endregion

        #region Public Properties
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

        #region Private Method(s)
        private void FillUserList()
        {
            UserList objUserList = UserManager.GetList(1);
            BindingSource objBindingSrc = new BindingSource();
            objBindingSrc.DataSource = objUserList;

            if (objUserList != null && objUserList.Count > 0)
            {
                cboUser.DataSource = objBindingSrc.DataSource;
                cboUser.DisplayMember = "LOGINNAME";
                cboUser.ValueMember = "DBID";
                cboUser.Text = "Select User";
            }
        }

        private void DefinitionMenu()
        {
            tvwRights.Nodes["Rights"].Nodes.Add("D0001", "Definition");
            tvwRights.Nodes["Rights"].Nodes["D0001"].Nodes.Add("D0011", "Department Master");
            tvwRights.Nodes["Rights"].Nodes["D0001"].Nodes["D0011"].Nodes.Add("D0011A", "Add");
            tvwRights.Nodes["Rights"].Nodes["D0001"].Nodes["D0011"].Nodes.Add("D0011M", "Modify");
            tvwRights.Nodes["Rights"].Nodes["D0001"].Nodes["D0011"].Nodes.Add("D0011V", "View");
            tvwRights.Nodes["Rights"].Nodes["D0001"].Nodes["D0011"].Nodes.Add("D0011D", "Delete");

            tvwRights.Nodes["Rights"].Nodes["D0001"].Nodes.Add("D0012", "Employee Master");
            tvwRights.Nodes["Rights"].Nodes["D0001"].Nodes["D0012"].Nodes.Add("D0012A", "Add");
            tvwRights.Nodes["Rights"].Nodes["D0001"].Nodes["D0012"].Nodes.Add("D0012M", "Modify");
            tvwRights.Nodes["Rights"].Nodes["D0001"].Nodes["D0012"].Nodes.Add("D0012V", "View");
            tvwRights.Nodes["Rights"].Nodes["D0001"].Nodes["D0012"].Nodes.Add("D0012D", "Delete");

            tvwRights.Nodes["Rights"].Nodes["D0001"].Nodes.Add("D0013", "Visitor Master");
            tvwRights.Nodes["Rights"].Nodes["D0001"].Nodes["D0013"].Nodes.Add("D0013A", "Add");
            tvwRights.Nodes["Rights"].Nodes["D0001"].Nodes["D0013"].Nodes.Add("D0013M", "Modify");
            tvwRights.Nodes["Rights"].Nodes["D0001"].Nodes["D0013"].Nodes.Add("D0013V", "View");
            tvwRights.Nodes["Rights"].Nodes["D0001"].Nodes["D0013"].Nodes.Add("D0013D", "Delete");

            tvwRights.Nodes["Rights"].Nodes["D0001"].Nodes.Add("D0014", "City Master");
            tvwRights.Nodes["Rights"].Nodes["D0001"].Nodes["D0014"].Nodes.Add("D0014A", "Add");
            tvwRights.Nodes["Rights"].Nodes["D0001"].Nodes["D0014"].Nodes.Add("D0014M", "Modify");
            tvwRights.Nodes["Rights"].Nodes["D0001"].Nodes["D0014"].Nodes.Add("D0014V", "View");
            tvwRights.Nodes["Rights"].Nodes["D0001"].Nodes["D0014"].Nodes.Add("D0014D", "Delete");

            tvwRights.Nodes["Rights"].Nodes["D0001"].Nodes.Add("D0015", "Driver Master");
            tvwRights.Nodes["Rights"].Nodes["D0001"].Nodes["D0015"].Nodes.Add("D0015A", "Add");
            tvwRights.Nodes["Rights"].Nodes["D0001"].Nodes["D0015"].Nodes.Add("D0015M", "Modify");
            tvwRights.Nodes["Rights"].Nodes["D0001"].Nodes["D0015"].Nodes.Add("D0015V", "View");
            tvwRights.Nodes["Rights"].Nodes["D0001"].Nodes["D0015"].Nodes.Add("D0015D", "Delete");

            tvwRights.Nodes["Rights"].Nodes["D0001"].Nodes.Add("D0016", "Vehicle Master");
            tvwRights.Nodes["Rights"].Nodes["D0001"].Nodes["D0016"].Nodes.Add("D0016A", "Add");
            tvwRights.Nodes["Rights"].Nodes["D0001"].Nodes["D0016"].Nodes.Add("D0016M", "Modify");
            tvwRights.Nodes["Rights"].Nodes["D0001"].Nodes["D0016"].Nodes.Add("D0016V", "View");
            tvwRights.Nodes["Rights"].Nodes["D0001"].Nodes["D0016"].Nodes.Add("D0016D", "Delete");

            tvwRights.Nodes["Rights"].Nodes["D0001"].Nodes.Add("D0017", "User Profile");
            tvwRights.Nodes["Rights"].Nodes["D0001"].Nodes["D0017"].Nodes.Add("D0017A", "Add");
            tvwRights.Nodes["Rights"].Nodes["D0001"].Nodes["D0017"].Nodes.Add("D0017M", "Modify");
            tvwRights.Nodes["Rights"].Nodes["D0001"].Nodes["D0017"].Nodes.Add("D0017V", "View");
            tvwRights.Nodes["Rights"].Nodes["D0001"].Nodes["D0017"].Nodes.Add("D0017D", "Delete");
        }

        private void TransactionMenu()
        {
            tvwRights.Nodes["Rights"].Nodes.Add("T0001", "Transactions");
            tvwRights.Nodes["Rights"].Nodes["T0001"].Nodes.Add("T0012", "Appointment Booking");
            tvwRights.Nodes["Rights"].Nodes["T0001"].Nodes["T0012"].Nodes.Add("T0012A", "Add");
            tvwRights.Nodes["Rights"].Nodes["T0001"].Nodes["T0012"].Nodes.Add("T0012M", "Modify");
            tvwRights.Nodes["Rights"].Nodes["T0001"].Nodes["T0012"].Nodes.Add("T0012V", "View");
            tvwRights.Nodes["Rights"].Nodes["T0001"].Nodes["T0012"].Nodes.Add("T0012D", "Delete");

            tvwRights.Nodes["Rights"].Nodes["T0001"].Nodes.Add("T0013", "Visitor GatePass");
            tvwRights.Nodes["Rights"].Nodes["T0001"].Nodes["T0013"].Nodes.Add("T0013A", "Add");
            tvwRights.Nodes["Rights"].Nodes["T0001"].Nodes["T0013"].Nodes.Add("T0013M", "Modify");
            tvwRights.Nodes["Rights"].Nodes["T0001"].Nodes["T0013"].Nodes.Add("T0013V", "View");
            tvwRights.Nodes["Rights"].Nodes["T0001"].Nodes["T0013"].Nodes.Add("T0013D", "Delete");
            tvwRights.Nodes["Rights"].Nodes["T0001"].Nodes["T0013"].Nodes.Add("T0013P", "Print");

            tvwRights.Nodes["Rights"].Nodes["T0001"].Nodes.Add("T0014", "Returnable DC");
            tvwRights.Nodes["Rights"].Nodes["T0001"].Nodes["T0014"].Nodes.Add("T0014A", "Add");
            tvwRights.Nodes["Rights"].Nodes["T0001"].Nodes["T0014"].Nodes.Add("T0014M", "Modify");
            tvwRights.Nodes["Rights"].Nodes["T0001"].Nodes["T0014"].Nodes.Add("T0014V", "View");
            tvwRights.Nodes["Rights"].Nodes["T0001"].Nodes["T0014"].Nodes.Add("T0014D", "Delete");

            tvwRights.Nodes["Rights"].Nodes["T0001"].Nodes.Add("T0011", "Vehicle In Out Detail");
            tvwRights.Nodes["Rights"].Nodes["T0001"].Nodes["T0011"].Nodes.Add("T0011A", "Add");
            tvwRights.Nodes["Rights"].Nodes["T0001"].Nodes["T0011"].Nodes.Add("T0011M", "Modify");
            tvwRights.Nodes["Rights"].Nodes["T0001"].Nodes["T0011"].Nodes.Add("T0011V", "View");
            tvwRights.Nodes["Rights"].Nodes["T0001"].Nodes["T0011"].Nodes.Add("T0011D", "Delete");
        }

        private void UtilitiesMenu()
        {
            tvwRights.Nodes["Rights"].Nodes.Add("U0001", "Utilities");
            tvwRights.Nodes["Rights"].Nodes["U0001"].Nodes.Add("U0011", "User Rights");
        }

        private void ClearNodes()
        {
            foreach (TreeNode tNode in tvwRights.Nodes)
            {
                tNode.Checked = false;
            }
        }

        private void SelectParents(TreeNode node, Boolean isChecked)
        {
            var parent = node.Parent;

            if (parent == null)
                return;

            if (!isChecked && HasCheckedNode(parent))
                return;

            parent.Checked = isChecked;
            SelectParents(parent, isChecked);
        }

        private void SelectChilds(TreeNode node, Boolean isChecked)
        {
            // object cNode represents Child Node of Current Node
            foreach (TreeNode cNode in node.Nodes)
            {
                cNode.Checked = isChecked;
                if (cNode.Checked && (cNode.Text == "Add" | cNode.Text == "Modify" | cNode.Text == "Delete" | cNode.Text == "Print"))
                    cNode.Checked = !(isChecked);
                SelectChilds(cNode, isChecked);
            }
        }

        private bool HasCheckedNode(TreeNode node)
        {
            return node.Nodes.Cast<TreeNode>().Any(n => n.Checked);
        }

        private UserRightsList ReadNodesRecursive(TreeNode oParentNode)
        {
            //UserRightsList objRightsList = new UserRightsList();
            if (oParentNode.Checked)
            {
                UserRights objRight = new UserRights();
                objRight.UserID = Convert.ToInt32(cboUser.SelectedValue);
                objRight.MenuID = oParentNode.Name;
                objRight.FormName = oParentNode.Text;
                objRightsList.Add(objRight);
                //MessageBox.Show(objRight.UserID + " - " + objRight.MenuID + " - " + objRight.FormName);
            }

            // Start recursion on all subnodes.
            foreach (TreeNode oSubNode in oParentNode.Nodes)
            {
                ReadNodesRecursive(oSubNode);
            }
            return objRightsList;
        }
        #endregion

        #region Constructor(s)
        public frmUserAuthority()
        {
            InitializeComponent();
        }

        public frmUserAuthority(UserCompany currentCompany, User currentUser)
        {
            this.objUserCompany = currentCompany;
            this.objCurUser = currentUser;

            objUIRights = new UIRights();

            InitializeComponent();

            GeneralMethods.FormAuthenticate(this.Name, objUserCompany, objCurUser);
            objUIRights.AddRight = GeneralMethods.frmAddRight;
            objUIRights.ModifyRight = GeneralMethods.frmModifyRight;
            objUIRights.ViewRight = GeneralMethods.frmViewRight;
            objUIRights.DeleteRight = GeneralMethods.frmDeleteRight;
            objUIRights.PrintRight = GeneralMethods.repPrintRight;
        }
        #endregion

        #region UI Control Logic
        private void frmUserAuthority_Load(object sender, EventArgs e)
        {
            flgLoading = true;
            objRightsList = new UserRightsList();

            FillUserList();

            // Create Nodes in TreeView (Start)
            tvwRights.Nodes.Add("Rights", "Rights");
            DefinitionMenu();
            TransactionMenu();
            UtilitiesMenu();
            // Create Nodes in TreeView (End)
            //if (objUIRights.ViewRight)
            //    grbUserList.Enabled = true;
            //else
            //    grbUserList.Enabled = false;

            //if (objUIRights.AddRight)
            //    grbSave.Enabled = true;
            //else
            //    grbSave.Enabled = false;
                
            flgLoading = false;
        }

        private void cboUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] objList = null;
            if (!flgLoading)
            {
                ClearNodes();
                objList = UserRightsManager.GetUserRights(Convert.ToInt32(cboUser.SelectedValue));

                if (objList != null)
                {
                    foreach (string str in objList)
                    {
                        TreeNode[] tNode = tvwRights.Nodes.Find(str, true);
                        if (tNode != null)
                            tNode[0].Checked = true;
                    }
                    tvwRights.Nodes[0].Expand();
                }
            }
        }

        private void cboUser_KeyPress(object sender, KeyPressEventArgs e)
        {
            GeneralMethods.AutoComplete(this.cboUser, e, true);
        }

        private void cboUser_Enter(object sender, EventArgs e)
        {
            cboUser.SelectAll();
        }

        private void tvwRights_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void tvwRights_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void tvwRights_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            
        }

        private void tvwRights_AfterCheck(object sender, TreeViewEventArgs e)
        {
            try
            {
                if (updatingTreeView)
                    return;

                updatingTreeView = true;
                SelectParents(e.Node, e.Node.Checked);
                SelectChilds(e.Node, e.Node.Checked);

                updatingTreeView = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                // Read Values from TreeViews (Start)
                TreeNode oMainNode = tvwRights.Nodes[0];
                UserRightsList objList;
                objList = ReadNodesRecursive(oMainNode);
                if (objList[0].MenuID == "RIGHTS")
                    objList.RemoveAt(0);
                // Read Values from TreeViews (End)

                bool flgApplyEdit;
                flgApplyEdit = UserRightsManager.Save(objList);
                if (flgApplyEdit)
                {
                    //DialogResult result = MessageBox.Show("User Rights Saved Successfully. Do You want to Close this Window? Click 'Yes' to Close Window, otherwise Click 'No'", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    //if (result == DialogResult.Yes)
                    //{
                    //    this.Close();
                    //}
                    MessageBox.Show("User Rights Saved Successfully.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("User Rights Not Saved.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
    }
}
