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
    public partial class frmCityList : Form
    {
        #region Private Variable
        private bool flgLoading;
        private bool flgList;
        private bool flgListCancel;

        private int dbid;
        private string mcity;
        
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
        public string mLCity
        {
            get
            {
                return mcity;
            }
            set
            {
                mcity = value;
            }
        }

        
        #endregion

        #region Constructor(s)
        public frmCityList()
        {
            InitializeComponent();
            InitializeListView();
        }

        public frmCityList(UserCompany currentCompany, User currentUser)
        {
            this.objUserCompany = currentCompany;
            this.objCurUser = currentUser;

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

            this.ContextMenuStrip = conMnu;
            this.lvwCities.FullRowSelect = true;
            this.lvwCities.GridLines = true;
            this.lvwCities.ListViewItemSorter = lvwColSorter;
            this.lvwCities.MultiSelect = false;
            this.lvwCities.View = View.Details;
        }

        private void FillList()
        {
            CityList objList = new CityList(); 
            objList = CityManager.GetList("");

            lvwCities.Items.Clear();

            if (objList != null)
            {
                foreach (City objCity in objList)
                {
                    ListViewItem objLvwItem = new ListViewItem();
                    objLvwItem.Name = Convert.ToString(objCity.DBID);
                    objLvwItem.Text = Convert.ToString(objCity.mCity);
                    //objLvwItem.SubItems.Add(Convert.ToString(objDept.Description));

                    lvwCities.Items.Add(objLvwItem);
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
        private void frmCityList_Load(object sender, EventArgs e)
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

        private void frmCityList_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (dbid == 0)
                flgListCancel = true;
        }

        private void lvwCities_ColumnClick(object sender, ColumnClickEventArgs e)
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
            this.lvwCities.Sort();
        }
        
        private void lvwCities_DoubleClick(object sender, EventArgs e)
        {
            if (lvwCities.SelectedItems != null && lvwCities.SelectedItems.Count != 0)
            {
                modifyToolStripMenuItem_Click(sender, e);
            }
        }
        
        private void lvwCities_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (lvwCities.SelectedItems.Count == 1 && e.KeyChar == (char)Keys.Enter)
            {
                modifyToolStripMenuItem_Click(sender, e);
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsList && lvwCities.SelectedItems != null && lvwCities.SelectedItems.Count == 1)
                {
                    dbid = Convert.ToInt32(lvwCities.SelectedItems[0].Name);
                    mLCity = lvwCities.SelectedItems[0].Text;

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
                    mLCity = string.Empty;

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
        #endregion

        #region Context Menu
        private void conMnu_Opening(object sender, CancelEventArgs e)
        {
            if (!IsList)
            {
                if (lvwCities.SelectedItems != null && lvwCities.SelectedItems.Count != 0)
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
                if (lvwCities.SelectedItems != null && lvwCities.SelectedItems.Count != 0)
                {
                    if (IsList)
                    {
                        btnOk_Click(sender, e);
                    }
                    else
                    {
                        if (objUIRights.ModifyRight)
                        {
                            City objCity;
                            frmCityProp objFrmProp;

                            objCity = CityManager.GetItem(Convert.ToInt32(lvwCities.SelectedItems[0].Name));
                            objFrmProp = new frmCityProp(objCity);
                            objFrmProp.MdiParent = this.MdiParent;
                            objFrmProp.Entry_DataChanged += new frmCityProp.CityUpdateHandler(Entry_DataChanged);
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
                        City objCity;
                        frmCityProp objFrmProp;

                        objCity = new City();
                        objFrmProp = new frmCityProp(objCity);
                        objFrmProp.IsNew = true;
                        objFrmProp.MdiParent = this.MdiParent;
                        objFrmProp.Entry_DataChanged += new frmCityProp.CityUpdateHandler(Entry_DataChanged);
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
                if (lvwCities.SelectedItems != null && lvwCities.SelectedItems.Count != 0)
                {
                    if (!IsList)
                    {
                        if (objUIRights.DeleteRight)
                        {
                            DialogResult dr = new DialogResult();
                            dr = MessageBox.Show("Do You Really Want to Delete Record ?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                            if (dr == DialogResult.Yes)
                            {
                                City objCity = new City();
                                objCity = CityManager.GetItem(Convert.ToInt32(lvwCities.SelectedItems[0].Name));
                                CityManager.Delete(objCity);
                                lvwCities.Items.Remove(lvwCities.SelectedItems[0]);
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

        private void Entry_DataChanged(object sender, CityUpdateEventArgs e, DataEventType Action)
        {
            ListViewItem lvItem;
            switch (Action)
            {
                case DataEventType.INSERT_EVENT:

                    lvItem = new ListViewItem();
                    lvItem.Name = Convert.ToString(e.DBID);
                    lvItem.Text = e.mDCity;

                    lvwCities.Items.Add(lvItem);
                    lvwCities.EnsureVisible(lvItem.Index);

                    break;

                case DataEventType.UPDATE_EVENT:
                    lvItem = lvwCities.Items[lvwCities.SelectedItems[0].Index];
                    lvItem.Text = e.mDCity;

                    lvwCities.EnsureVisible(lvwCities.SelectedItems[0].Index);

                    break;
            }
        }
    }
}
