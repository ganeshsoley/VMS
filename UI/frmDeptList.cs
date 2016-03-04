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
    public partial class frmDeptList : Form
    {
        #region Private Variable(s)
        private bool flgLoading;
        private bool flgList;
        private bool flgListCancel;

        private int dbid;
        private string deptName;
        private string descr;

        private ListViewColumnSorter lvwColSorter;
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

        public string Description
        {
            get
            {
                return descr;
            }
            set
            {
                descr = value;
            }
        }
        #endregion

        #region Constructor(s)
        public frmDeptList()
        {
            InitializeComponent();
            InitializeListView();
        }

        public frmDeptList(UserCompany objCompany, User objUser)
        {
            this.objCurUser = objUser;
            this.objUserCompany = objCompany;

            objUIRights = new UIRights();

            InitializeComponent();
            InitializeListView();
            GeneralMethods.FormAuthenticate(this.Name, objUserCompany, objCurUser);

            objUIRights.AddRight = GeneralMethods.frmAddRight;
            objUIRights.ModifyRight = GeneralMethods.frmModifyRight;
            objUIRights.ViewRight = GeneralMethods.frmViewRight;
            objUIRights.DeleteRight = GeneralMethods.frmDeleteRight;
            objUIRights.PrintRight = GeneralMethods.repPrintRight;
        }
        #endregion

        #region Private Method(s)
        private void InitializeListView()
        {
            // Create an instance of a ListView column sorter and assign it 
            // to the ListView control.
            lvwColSorter = new ListViewColumnSorter();

            this.lvwDepts.ContextMenuStrip = conMenu;
            this.lvwDepts.FullRowSelect = true;
            this.lvwDepts.GridLines = true;
            this.lvwDepts.ListViewItemSorter = lvwColSorter;
            this.lvwDepts.MultiSelect = false;
            this.lvwDepts.View = View.Details;
        }

        private void FillList()
        {
            DepartmentList objList = new DepartmentList();
            objList = DeptManager.GetList("");

            lvwDepts.Items.Clear();

            if (objList != null)
            {
                foreach (Department objDept in objList)
                {
                    ListViewItem objLvwItem = new ListViewItem();
                    objLvwItem.Name = Convert.ToString(objDept.DBID);
                    objLvwItem.Text = Convert.ToString(objDept.DeptName);
                    objLvwItem.SubItems.Add(Convert.ToString(objDept.Description));

                    lvwDepts.Items.Add(objLvwItem);
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

        #region UI Control Logic
        private void frmDeptList_Load(object sender, EventArgs e)
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

        private void frmDeptList_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (dbid == 0)
                flgListCancel = true;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsList && lvwDepts.SelectedItems != null && lvwDepts.SelectedItems.Count == 1)
                {
                    dbid = Convert.ToInt32(lvwDepts.SelectedItems[0].Name);
                    deptName = lvwDepts.SelectedItems[0].Text;
                    descr = lvwDepts.SelectedItems[0].SubItems[1].Text;

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
                    deptName = string.Empty;
                    descr = string.Empty;

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

        private void lvwDepts_ColumnClick(object sender, ColumnClickEventArgs e)
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
            this.lvwDepts.Sort();
        }

        private void lvwDepts_DoubleClick(object sender, EventArgs e)
        {
            if (lvwDepts.SelectedItems != null && lvwDepts.SelectedItems.Count != 0)
            {
                modifyToolStripMenuItem_Click(sender, e);
            }
        }

        private void lvwDepts_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (lvwDepts.SelectedItems.Count == 1 && e.KeyChar == (char) Keys.Enter)
            {
                modifyToolStripMenuItem_Click(sender, e);
            }
        }
        #endregion

        #region Context Menu
        private void contextMenu_Opening(object sender, CancelEventArgs e)
        {
            if (!IsList)
            {
                if (lvwDepts.SelectedItems != null && lvwDepts.SelectedItems.Count != 0)
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
                if (lvwDepts.SelectedItems != null && lvwDepts.SelectedItems.Count != 0)
                {
                    if (IsList)
                    {
                        btnOk_Click(sender, e);
                    }
                    else
                    {
                        if (objUIRights.ModifyRight)
                        {
                            Department objDept;
                            frmDeptProp objFrmProp;

                            objDept = DeptManager.GetItem(Convert.ToInt32(lvwDepts.SelectedItems[0].Name));
                            objFrmProp = new frmDeptProp(objDept);
                            objFrmProp.MdiParent = this.MdiParent;
                            objFrmProp.Entry_DataChanged += new frmDeptProp.DeptUpdateHandler(Entry_DataChanged);
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
                        Department objDept;
                        frmDeptProp objFrmProp;

                        objDept = new Department();
                        objFrmProp = new frmDeptProp(objDept);
                        objFrmProp.IsNew = true;
                        objFrmProp.MdiParent = this.MdiParent;
                        objFrmProp.Entry_DataChanged += new frmDeptProp.DeptUpdateHandler(Entry_DataChanged);
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
                if (lvwDepts.SelectedItems != null && lvwDepts.SelectedItems.Count != 0)
                {
                    if (!IsList)
                    {
                        if (objUIRights.DeleteRight)
                        {
                            DialogResult dr = new DialogResult();
                            dr = MessageBox.Show("Do You Really Want to Delete Record ?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                            if (dr == DialogResult.Yes)
                            {
                                Department objDept = new Department();
                                objDept = DeptManager.GetItem(Convert.ToInt32(lvwDepts.SelectedItems[0].Name));
                                DeptManager.Delete(objDept);
                                lvwDepts.Items.Remove(lvwDepts.SelectedItems[0]);
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

        private void Entry_DataChanged(object sender, DeptUpdateEventArgs e, DataEventType Action)
        {
            ListViewItem lvItem;
            switch (Action)
            {
                case DataEventType.INSERT_EVENT:

                    lvItem = new ListViewItem();
                    lvItem.Name = Convert.ToString(e.DBID);
                    lvItem.Text = e.Dept;
                    lvItem.SubItems.Add(e.Descr);
                    
                    lvwDepts.Items.Add(lvItem);
                    lvwDepts.EnsureVisible(lvItem.Index);

                    break;

                case DataEventType.UPDATE_EVENT:
                    lvItem = lvwDepts.Items[lvwDepts.SelectedItems[0].Index];
                    lvItem.Text = e.Dept;
                    lvItem.SubItems[1].Text = e.Descr;
                    
                    lvwDepts.EnsureVisible(lvwDepts.SelectedItems[0].Index);

                    break;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //UI.Reports.VisitorGatePass custReport = new UI.Reports.VisitorGatePass();
            //crystalReportViewer1.ReportSource = custReport;
        }
    }
}
