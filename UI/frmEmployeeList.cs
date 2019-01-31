using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EntityObject;
using EntityObject.Enum;
using BLL;

namespace UI
{
    public partial class frmEmployeeList : Form
    {
        #region Private Variable(s)
        private bool flgLoading;
        private bool flgList;
        private bool flgListCancel;

        private int dbid;
        private string initials;
        private string empCode;
        private string deptName;
        private string empPlant;

        private ListViewColumnSorter lvwColSorter;
        private UserCompany currentCompany;
        private User currentUser;
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
            set
            {
                dbid = value;
            }
        }

        public string Initials
        {
            get
            {
                return initials;
            }
            set
            {
                initials = value;
            }
        }

        public string EmpCode
        {
            get
            {
                return empCode;
            }
            set
            {
                empCode = value;
            }
        }

        public string DeptName
        {
            get
            {
                return deptName;
            }
            set
            {
                deptName = value;
            }
        }

        public string EmpPlant
        {
            get
            {
                return empPlant;
            }
            set
            {
                empPlant = value;
            }
        }
        #endregion

        public frmEmployeeList()
        {
            InitializeComponent();
            InitializeListView();
        }

        public frmEmployeeList(UserCompany currentCompany, User currentUser)
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

        #region Private Method(s)
        private void InitializeListView()
        {
            // Create an instance of a ListView column sorter and assign it 
            // to the ListView control.
            lvwColSorter = new ListViewColumnSorter();

            this.lvwEmployees.ContextMenuStrip = conMnu;
            this.lvwEmployees.FullRowSelect = true;
            this.lvwEmployees.GridLines = true;
            this.lvwEmployees.ListViewItemSorter = lvwColSorter;
            this.lvwEmployees.MultiSelect = false;
            this.lvwEmployees.View = View.Details;
        }

        private void FillList()
        {
            EmployeeList objList = new EmployeeList();
            objList = EmployeeManager.GetList("");

            lvwEmployees.Items.Clear();

            if (objList != null)
            {
                foreach (Employee objEmp in objList)
                {
                    ListViewItem objLvwItem = new ListViewItem();
                    objLvwItem.Name = Convert.ToString(objEmp.DBID);
                    objLvwItem.Text = Convert.ToString(objEmp.Initials);
                    objLvwItem.SubItems.Add(objEmp.EmpCode);
                    objLvwItem.SubItems.Add(objEmp.DeptName);
                    objLvwItem.SubItems.Add(objEmp.EmpPlant);

                    lvwEmployees.Items.Add(objLvwItem);
                }
            }
        }

        private void SetButtonVisibility()
        {
            btnOk.Visible = IsList;
            btnCancel.Visible = IsList;
            btnNew.Visible = !IsList;
        }
        #endregion

        private void frmEmployeeList_Load(object sender, EventArgs e)
        {
            this.Icon = new Icon("Images/DTPL.ico");
            flgLoading = true;
            FillList();
            SetButtonVisibility();
            if (IsList)
            {
                this.CancelButton = btnCancel;
            }
            flgLoading = false;
        }

        private void lvwEmployees_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            // Determine if clicked column is already the column that is being sorted.
            if (e.Column == lvwColSorter.SortColumn)
            {
                // Reverse the current sort direction for this column.
                if (lvwColSorter.Order == SortOrder.Ascending)
                {
                    lvwColSorter.Order = SortOrder.Descending;
                }
                else
                {
                    lvwColSorter.Order = SortOrder.Ascending;
                }
            }
            else
            {
                // Set the column number that is to be sorted; default to ascending.
                lvwColSorter.SortColumn = e.Column;
                lvwColSorter.Order = SortOrder.Ascending;
            }

            // Perform the sort with these new sort options.
            this.lvwEmployees.Sort();
        }

        private void lvwEmployees_DoubleClick(object sender, EventArgs e)
        {
            if (lvwEmployees.SelectedItems != null && lvwEmployees.SelectedItems.Count != 0)
            {
                modifyToolStripMenuItem_Click(sender, e);
            }
        }

        private void lvwEmployees_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (lvwEmployees.SelectedItems.Count == 1 && e.KeyChar == (char)13)
            {
                modifyToolStripMenuItem_Click(sender, e);
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsList && lvwEmployees.SelectedItems != null && lvwEmployees.SelectedItems.Count == 1)
                {
                    dbid = Convert.ToInt32(lvwEmployees.SelectedItems[0].Name);
                    initials = lvwEmployees.SelectedItems[0].Text;
                    empCode = lvwEmployees.SelectedItems[0].SubItems[1].Text;
                    deptName = lvwEmployees.SelectedItems[0].SubItems[2].Text;
                    empPlant = lvwEmployees.SelectedItems[0].SubItems[3].Text;

                    flgListCancel = false;
                }
                else
                {
                    btnCancel_Click(sender, e);
                }
                this.Close();
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
                if (IsList)
                {
                    dbid = 0;
                    initials = string.Empty;
                    empCode = string.Empty;
                    deptName = string.Empty;
                    empPlant = string.Empty;

                    flgListCancel = true;
                }
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            if (!IsList)
            {
                newToolStripMenuItem_Click(sender, e);
            }
        }

        #region Context Menu
        private void conMnu_Opening(object sender, CancelEventArgs e)
        {
            if (!IsList)
            {
                if (lvwEmployees.SelectedItems != null && lvwEmployees.SelectedItems.Count != 0)
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
                if (lvwEmployees.SelectedItems != null && lvwEmployees.SelectedItems.Count != 0)
                {
                    if (IsList)
                    {
                        btnOk_Click(sender, e);
                    }
                    else
                    {
                        if (objUIRights.ModifyRight)
                        {
                            Employee objEmp;
                            frmEmpProp objFrmProp;

                            objEmp = EmployeeManager.GetItem(Convert.ToInt32(lvwEmployees.SelectedItems[0].Name));
                            objFrmProp = new frmEmpProp(objEmp, currentUser);
                            objFrmProp.MdiParent = this.MdiParent;
                            objFrmProp.Entry_DataChanged += new frmEmpProp.EmpUpdateHandler(Entry_DataChanged);
                            objFrmProp.Show();
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
                        Employee objEmp;
                        frmEmpProp objFrmProp;

                        objEmp = new Employee();
                        objFrmProp = new frmEmpProp(objEmp, currentUser);
                        objFrmProp.IsNew = true;
                        objFrmProp.MdiParent = this.MdiParent;
                        objFrmProp.Entry_DataChanged += new frmEmpProp.EmpUpdateHandler(Entry_DataChanged);
                        objFrmProp.Show();
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
                if (lvwEmployees.SelectedItems != null && lvwEmployees.SelectedItems.Count != 0)
                {
                    if (!IsList)
                    {
                        if (objUIRights.DeleteRight)
                        {
                            DialogResult dr = new DialogResult();
                            dr = MessageBox.Show("Do You Really Want to Delete Record ?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                            if (dr == DialogResult.Yes)
                            {
                                Employee objEmp = new Employee();
                                objEmp = EmployeeManager.GetItem(Convert.ToInt32(lvwEmployees.SelectedItems[0].Name));
                                EmployeeManager.Delete(objEmp);
                                lvwEmployees.Items.Remove(lvwEmployees.SelectedItems[0]);
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

        private void Entry_DataChanged(object sender, EmpUpdateEventArgs e, DataEventType Action)
        {
            ListViewItem lvItem;
            switch (Action)
            {
                case DataEventType.INSERT_EVENT:

                    lvItem = new ListViewItem();
                    lvItem.Name = Convert.ToString(e.DBID);
                    lvItem.Text = e.Initials;
                    lvItem.SubItems.Add(e.EmpCode);
                    lvItem.SubItems.Add(e.DeptName);
                    lvItem.SubItems.Add(e.EmpPlant);

                    lvwEmployees.Items.Add(lvItem);
                    lvwEmployees.EnsureVisible(lvItem.Index);

                    break;

                case DataEventType.UPDATE_EVENT:
                    lvItem = lvwEmployees.Items[lvwEmployees.SelectedItems[0].Index];
                    lvItem.Text = e.Initials;
                    lvItem.SubItems[1].Text = e.EmpCode;
                    lvItem.SubItems[2].Text = e.DeptName;
                    lvItem.SubItems[3].Text = e.EmpPlant;

                    lvwEmployees.EnsureVisible(lvwEmployees.SelectedItems[0].Index);

                    break;
            }
        }

        private void frmEmployeeList_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (dbid == 0)
                flgListCancel = true;
        }
    }
}
