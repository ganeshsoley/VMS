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
    public partial class frmVehicleList : Form
    {
        #region Private Variable
        private bool flgLoading;
        private bool flgList;
        private bool flgListCancel;

        private int dbid;
        private string vehicleNo;
        
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

        public string VehicleNo
        {
            get
            {
                return vehicleNo;
            }
            set
            {
                vehicleNo = value;
            }
        }

        
        #endregion

        #region Constructor
        public frmVehicleList()
        {
            InitializeComponent();
            InitializeListView();
        }

        public frmVehicleList(UserCompany currentCompany, User currentUser)
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
            this.lvwVehicles.FullRowSelect = true;
            this.lvwVehicles.GridLines = true;
            this.lvwVehicles.ListViewItemSorter = lvwColSorter;
            this.lvwVehicles.MultiSelect = false;
            this.lvwVehicles.View = View.Details;
        }

        private void FillList()
        {
            VehicleList objList = new VehicleList();  

            objList = VehicleManager.GetList("", chkIsActive.Checked);

            lvwVehicles.Items.Clear();

            if (objList != null)
            {
                foreach (Vehicle objVehicle in objList)
                {
                    ListViewItem objLvwItem = new ListViewItem();
                    objLvwItem.Name = Convert.ToString(objVehicle.Dbid);
                    objLvwItem.Text = Convert.ToString(objVehicle.VehicleNo);
                    if (objVehicle.PUCExpiry != DateTime.MinValue)
                        objLvwItem.SubItems.Add(objVehicle.PUCExpiry.ToShortDateString());
                    else
                        objLvwItem.SubItems.Add("");

                    lvwVehicles.Items.Add(objLvwItem);
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
        private void frmVehicleList_Load(object sender, EventArgs e)
        {
            this.Icon = new Icon("Images/DTPL.ico");
            flgLoading = true;
            chkIsActive.Checked = true;
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
                if (IsList && lvwVehicles.SelectedItems != null && lvwVehicles.SelectedItems.Count == 1)
                {
                    dbid = Convert.ToInt32(lvwVehicles.SelectedItems[0].Name);
                    vehicleNo = lvwVehicles.SelectedItems[0].Text;
                    

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
                    vehicleNo = string.Empty;

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

        private void lvwVehicles_ColumnClick(object sender, ColumnClickEventArgs e)
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
            this.lvwVehicles.Sort();
        }

        private void lvwVehicles_DoubleClick(object sender, EventArgs e)
        {
            if (lvwVehicles.SelectedItems != null && lvwVehicles.SelectedItems.Count != 0)
            {
                modifyToolStripMenuItem_Click(sender, e);
            }
        }

        private void lvwVehicles_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (lvwVehicles.SelectedItems.Count == 1 && e.KeyChar == (char)Keys.Enter)
            {
                modifyToolStripMenuItem_Click(sender, e);
            }
        }

        private void chkIsActive_CheckedChanged(object sender, EventArgs e)
        {
            FillList();
        }
        #endregion

        #region Context Menu
        private void conMnu_Opening(object sender, CancelEventArgs e)
        {
            if (!IsList)
            {
                if (lvwVehicles.SelectedItems != null && lvwVehicles.SelectedItems.Count != 0)
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
                if (lvwVehicles.SelectedItems != null && lvwVehicles.SelectedItems.Count != 0)
                {
                    if (IsList)
                    {
                        btnOk_Click(sender, e);
                    }
                    else
                    {
                        if (objUIRights.ModifyRight)
                        {
                            Vehicle objVehicle;
                            frmVehicleProp objFrmProp;

                            objVehicle = VehicleManager.GetItem(Convert.ToInt32(lvwVehicles.SelectedItems[0].Name));
                            objFrmProp = new frmVehicleProp(objVehicle, currentUser);
                            objFrmProp.MdiParent = this.MdiParent;
                            objFrmProp.Entry_DataChanged += new frmVehicleProp.VehicleUpdateHandler(Entry_DataChanged);
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
                        Vehicle objVehicle;
                        frmVehicleProp objFrmProp;

                        objVehicle = new Vehicle();
                        objFrmProp = new frmVehicleProp(objVehicle, currentUser);
                        objFrmProp.IsNew = true;
                        objFrmProp.MdiParent = this.MdiParent;
                        objFrmProp.Entry_DataChanged += new frmVehicleProp.VehicleUpdateHandler(Entry_DataChanged);
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
                if (lvwVehicles.SelectedItems != null && lvwVehicles.SelectedItems.Count != 0)
                {
                    if (!IsList)
                    {
                        if (objUIRights.DeleteRight)
                        {
                            DialogResult dr = new DialogResult();
                            dr = MessageBox.Show("Do You Really Want to Delete Record ?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                            if (dr == DialogResult.Yes)
                            {
                                Vehicle objVehicle = new Vehicle();
                                objVehicle = VehicleManager.GetItem(Convert.ToInt32(lvwVehicles.SelectedItems[0].Name));
                                VehicleManager.Delete(objVehicle);
                                lvwVehicles.Items.Remove(lvwVehicles.SelectedItems[0]);
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

        private void Entry_DataChanged(object sender, VehicleUpdateEventArgs e, DataEventType Action)
        {
            ListViewItem lvItem;
            switch (Action)
            {
                case DataEventType.INSERT_EVENT:

                    lvItem = new ListViewItem();
                    lvItem.Name = Convert.ToString(e.DBID);
                    lvItem.Text = e.VehicleNo;
                    lvItem.SubItems.Add(e.PUCExpiry.ToShortDateString());

                    lvwVehicles.Items.Add(lvItem);
                    lvwVehicles.EnsureVisible(lvItem.Index);

                    break;

                case DataEventType.UPDATE_EVENT:
                    lvItem = lvwVehicles.Items[lvwVehicles.SelectedItems[0].Index];
                    lvItem.Text = e.VehicleNo;
                    lvItem.SubItems[1].Text = e.PUCExpiry.ToShortDateString();

                    lvwVehicles.EnsureVisible(lvwVehicles.SelectedItems[0].Index);

                    break;
            }
        }
    }

}
