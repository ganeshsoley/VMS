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
    public partial class frmDriverList : Form
    {
        #region Private Variable
        private bool flgLoading;
        private bool flgList;
        private bool flgListCancel;

        private int dbid;
        private string name;
        
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
        public string DriverName
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }
     
        #endregion

        #region Constructor
        public frmDriverList()
        {
            InitializeComponent();
            InitializeListView();
        }

        public frmDriverList(UserCompany currentCompany, User currentUser)
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

        #region Private Method(s)
        private void InitializeListView()
        {
            // Create an instance of a ListView column sorter and assign it 
            // to the ListView control.
            lvwColSorter = new ListViewColumnSorter();

            this.ContextMenuStrip = conMnu;
            this.lvwDrivers.FullRowSelect = true;
            this.lvwDrivers.GridLines = true;
            this.lvwDrivers.ListViewItemSorter = lvwColSorter;
            this.lvwDrivers.MultiSelect = false;
            this.lvwDrivers.View = View.Details;
        }
        private void FillList()
        {
            
            DriverList objList = new DriverList();
            objList = DriverManager.GetList("");

            lvwDrivers.Items.Clear();

            if (objList != null)
            {
                foreach (Driver objDriver in objList)
                {
                    ListViewItem objLvwItem = new ListViewItem();
                    objLvwItem.Name = Convert.ToString(objDriver.DBID);
                    objLvwItem.Text = Convert.ToString(objDriver.Name);
                    //objLvwItem.SubItems.Add(Convert.ToString(objDept.Description));

                    lvwDrivers.Items.Add(objLvwItem);
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
        private void frmDriverList_Load(object sender, EventArgs e)
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

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsList && lvwDrivers.SelectedItems != null && lvwDrivers.SelectedItems.Count == 1)
                {
                    dbid = Convert.ToInt32(lvwDrivers.SelectedItems[0].Name);
                    DriverName = lvwDrivers.SelectedItems[0].Text;
                    
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
                    DriverName = string.Empty;
                    
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

        private void lvwDrivers_ColumnClick(object sender, ColumnClickEventArgs e)
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
            this.lvwDrivers.Sort();
        }

        private void lvwDrivers_DoubleClick(object sender, EventArgs e)
        {
            if (lvwDrivers.SelectedItems != null && lvwDrivers.SelectedItems.Count != 0)
            {
                modifyToolStripMenuItem_Click(sender, e);
            }
        }

        private void lvwDrivers_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (lvwDrivers.SelectedItems.Count == 1 && e.KeyChar == (char)Keys.Enter)
            {
                modifyToolStripMenuItem_Click(sender, e);
            }
        }
        #endregion

        #region Context Menu
        private void conMnu_Opening(object sender, CancelEventArgs e)
        {
            if (!IsList)
            {
                if (lvwDrivers.SelectedItems != null && lvwDrivers.SelectedItems.Count != 0)
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
                if (lvwDrivers.SelectedItems != null && lvwDrivers.SelectedItems.Count != 0)
                {
                    if (IsList)
                    {
                        btnOk_Click(sender, e);
                    }
                    else
                    {
                        if (objUIRights.ModifyRight)
                        {
                            Driver objDriver;
                            frmDriverProp objFrmProp;

                            objDriver = DriverManager.GetItem(Convert.ToInt32(lvwDrivers.SelectedItems[0].Name));
                            objFrmProp = new frmDriverProp(objDriver);
                            objFrmProp.MdiParent = this.MdiParent;
                            objFrmProp.Entry_DataChanged += new frmDriverProp.DriverUpdateHandler(Entry_DataChanged);
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
                        Driver objDriver;
                        frmDriverProp objFrmProp;

                        objDriver = new Driver();
                        objFrmProp = new frmDriverProp(objDriver);
                        objFrmProp.IsNew = true;
                        objFrmProp.MdiParent = this.MdiParent;
                        objFrmProp.Entry_DataChanged += new frmDriverProp.DriverUpdateHandler(Entry_DataChanged);
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
                if (lvwDrivers.SelectedItems != null && lvwDrivers.SelectedItems.Count != 0)
                {
                    if (!IsList)
                    {
                        if (objUIRights.DeleteRight)
                        {
                            DialogResult dr = new DialogResult();
                            dr = MessageBox.Show("Do You Really Want to Delete Record ?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                            if (dr == DialogResult.Yes)
                            {
                                Driver objDriver = new Driver();
                                objDriver = DriverManager.GetItem(Convert.ToInt32(lvwDrivers.SelectedItems[0].Name));
                                DriverManager.Delete(objDriver);
                                lvwDrivers.Items.Remove(lvwDrivers.SelectedItems[0]);
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

        private void Entry_DataChanged(object sender, DriverUpdateEventArgs e, DataEventType Action)
        {
            ListViewItem lvItem;
            switch (Action)
            {
                case DataEventType.INSERT_EVENT:

                    lvItem = new ListViewItem();
                    lvItem.Name = Convert.ToString(e.DBID);
                    lvItem.Text = e.Name;
                    lvItem.SubItems.Add(e.LicenceNo);

                    lvwDrivers.Items.Add(lvItem);
                    lvwDrivers.EnsureVisible(lvItem.Index);

                    break;

                case DataEventType.UPDATE_EVENT:
                    lvItem = lvwDrivers.Items[lvwDrivers.SelectedItems[0].Index];
                    lvItem.Text = e.Name;
                    lvItem.SubItems[1].Text = e.LicenceNo;

                    lvwDrivers.EnsureVisible(lvwDrivers.SelectedItems[0].Index);

                    break;
            }
        }
    }
}
