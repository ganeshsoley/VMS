using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using EntityObject;
using EntityObject.Enum;
using BLL;

namespace UI
{
    public partial class frmVisitorList : Form
    {
        #region Private Variable(s)
        private bool flgLoading;
        private bool flgList;
        private bool flgListCancel;

        private int dbid;
        private int vRegNo;
        private string vName;
        private string vCompany;
        private string vContactNo;

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
        #endregion

        #region Public Object Properties
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

        public int VRegNo
        {
            get
            {
                return vRegNo;
            }
            set
            {
                vRegNo = value;
            }
        }
        
        public string VisitorName
        {
            get
            {
                return vName;
            }
            set
            {
                vName = value;
            }
        }

        public string Company
        {
            get
            {
                return vCompany;
            }
            set
            {
                vCompany = value;
            }
        }

        public string ContactNo
        {
            get
            {
                return vContactNo;
            }
            set
            {
                vContactNo = value;
            }
        }
        #endregion

        #region Constructor(s)
        public frmVisitorList()
        {
            InitializeComponent();
            InitializeListView();
        }

        public frmVisitorList(UserCompany currentCompany, User currentUser)
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
        /// <summary>
        /// Initializes ListView Properties.
        /// </summary>
        private void InitializeListView()
        {
            // Create an instance of a ListView column sorter and assign it 
            // to the ListView control.
            lvwColSorter = new ListViewColumnSorter();

            this.lvwVisitors.ContextMenuStrip = conMnu;
            this.lvwVisitors.FullRowSelect = true;
            this.lvwVisitors.GridLines = true;
            this.lvwVisitors.ListViewItemSorter = lvwColSorter;
            this.lvwVisitors.MultiSelect = false;
            this.lvwVisitors.View = View.Details;
        }

        /// <summary>
        /// Fills Company Names into ComboBox
        /// </summary>
        private void FillCompany()
        {
            string[] CompanyList = VisitorManager.GetCompanys();

            cboCompany.Items.Clear();
            if (CompanyList != null )
            {
                cboCompany.Items.Add("All");
                foreach (string str in CompanyList)
                {
                    cboCompany.Items.Add(str);
                }
                cboCompany.Text = "All";
            }
        }

        /// <summary>
        /// Fills ContactNos into ComboBox
        /// </summary>
        private void FillContactNo()
        {
            string[] ContactList = VisitorManager.GetContacts();

            cboContactNo.Items.Clear();
            if (ContactList != null )
            {
                cboContactNo.Items.Add("All");
                foreach (string str in ContactList)
                {
                    cboContactNo.Items.Add(str);
                }
                cboContactNo.Text = "All";
            }
        }

        /// <summary>
        /// Fills Data into ListView based on Search Criteria provided.
        /// </summary>
        /// <param name="strCompany">Name of Company Visitor Belongs.</param>
        /// <param name="strContact">Visitor Contact No.</param>
        private void FillList(string strCompany, string strContact)
        {
            VisitorList objList = new VisitorList();
            if (strCompany == "All")
                strCompany = "";
            if (strContact == "All")
                strContact = "";
            objList = VisitorManager.GetList(strCompany, strContact);

            lvwVisitors.Items.Clear();

            if (objList != null)
            {
                foreach (Visitor objVisitor in objList)
                {
                    ListViewItem objLvwItem = new ListViewItem();
                    objLvwItem.Name = Convert.ToString(objVisitor.DBID);
                    objLvwItem.Text = objVisitor.VName;
                    objLvwItem.SubItems.Add(objVisitor.Company);
                    objLvwItem.SubItems.Add(objVisitor.MobileNo);
                    objLvwItem.SubItems.Add(Convert.ToString(objVisitor.RegNo));

                    lvwVisitors.Items.Add(objLvwItem);
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
        private void frmVisitorList_Load(object sender, EventArgs e)
        {
            this.Icon = new Icon("Images/DTPL.ico");
            flgLoading = true;
            
            FillCompany();
            FillContactNo();
            FillList(cboCompany.Text, cboContactNo.Text);
            SetButtonVisibility();
            
            if (IsList)
            {
                this.CancelButton = btnCancel;
            }
            flgLoading = false;
        }

        private void frmVisitorList_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (IsList)
            {
                if (dbid == 0)
                    IsListCancel = true;
                else
                    IsListCancel = false;
            }
        }

        private void cboCompany_Enter(object sender, EventArgs e)
        {
            cboCompany.SelectAll();
        }

        private void cboCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillList(cboCompany.Text, cboContactNo.Text);
        }

        private void cboCompany_KeyPress(object sender, KeyPressEventArgs e)
        {
            GeneralMethods.AutoComplete(this.cboCompany, e, true);
        }

        private void cboContactNo_Enter(object sender, EventArgs e)
        {
            cboContactNo.SelectAll();
        }

        private void cboContactNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillList(cboCompany.Text, cboContactNo.Text);
        }

        private void cboContactNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            GeneralMethods.AutoComplete(cboContactNo, e, true);
        }

        private void lvwVisitors_ColumnClick(object sender, ColumnClickEventArgs e)
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
            this.lvwVisitors.Sort();
        }

        private void lvwVisitors_DoubleClick(object sender, EventArgs e)
        {
            if (lvwVisitors.SelectedItems != null && lvwVisitors.SelectedItems.Count != 0)
            {
                modifyToolStripMenuItem_Click(sender, e);
            }
        }

        private void lvwVisitors_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (lvwVisitors.SelectedItems.Count == 1 && e.KeyChar == (char)Keys.Enter)
            {
                modifyToolStripMenuItem_Click(sender, e);
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsList && lvwVisitors.SelectedItems != null && lvwVisitors.SelectedItems.Count == 1)
                {
                    dbid = Convert.ToInt32(lvwVisitors.SelectedItems[0].Name);
                    vName = lvwVisitors.SelectedItems[0].Text;
                    vCompany = lvwVisitors.SelectedItems[0].SubItems[1].Text;
                    vContactNo = lvwVisitors.SelectedItems[0].SubItems[2].Text;
                    vRegNo = Convert.ToInt32(lvwVisitors.SelectedItems[0].SubItems[3].Text);

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
                    vRegNo = 0;
                    vName = string.Empty;
                    vCompany = string.Empty;
                    vContactNo = string.Empty;

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
            newToolStripMenuItem_Click(sender, e);
        }
        #endregion

        #region Context Menu
        private void conMnu_Opening(object sender, CancelEventArgs e)
        {
            if (!IsList)
            {
                if (lvwVisitors.SelectedItems != null && lvwVisitors.SelectedItems.Count != 0)
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
                if (lvwVisitors.SelectedItems != null && lvwVisitors.SelectedItems.Count != 0)
                {
                    if (IsList)
                    {
                        btnOk_Click(sender, e);
                    }
                    else
                    {
                        if (objUIRights.ModifyRight)
                        {
                            Visitor objVisitor;
                            frmVisitorProp objFrmProp;

                            objVisitor = VisitorManager.GetItem(Convert.ToInt32(lvwVisitors.SelectedItems[0].Name));
                            objFrmProp = new frmVisitorProp(objVisitor);
                            objFrmProp.MdiParent = this.MdiParent;
                            objFrmProp.Entry_DataChanged += new frmVisitorProp.VisitorUpdateHandler(Entry_DataChanged);
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
                        Visitor objVisitor;
                        frmVisitorProp objFrmProp;

                        objVisitor = new Visitor();
                        objFrmProp = new frmVisitorProp(objVisitor);
                        objFrmProp.IsNew = true;
                        objFrmProp.MdiParent = this.MdiParent;
                        objFrmProp.Entry_DataChanged += new frmVisitorProp.VisitorUpdateHandler(Entry_DataChanged);
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
                if (lvwVisitors.SelectedItems != null && lvwVisitors.SelectedItems.Count != 0)
                {
                    if (!IsList)
                    {
                        if (objUIRights.DeleteRight)
                        {
                            DialogResult dr = new DialogResult();
                            dr = MessageBox.Show("Do You Really Want to Delete Record ?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                            if (dr == DialogResult.Yes)
                            {
                                Visitor objVisitor = new Visitor();
                                objVisitor = VisitorManager.GetItem(Convert.ToInt32(lvwVisitors.SelectedItems[0].Name));
                                VisitorManager.Delete(objVisitor);
                                lvwVisitors.Items.Remove(lvwVisitors.SelectedItems[0]);
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

        private void Entry_DataChanged(object sender, VisitorUpdateEventArgs e, DataEventType Action)
        {
            ListViewItem lvItem;
            switch (Action)
            {
                case DataEventType.INSERT_EVENT:

                    lvItem = new ListViewItem();
                    lvItem.Name = Convert.ToString(e.DBID);
                    lvItem.Text = e.VName;
                    lvItem.SubItems.Add(e.Company);
                    lvItem.SubItems.Add(e.ContactNo);
                    //lvItem.SubItems.Add(e.RegNo);

                    lvwVisitors.Items.Add(lvItem);
                    lvwVisitors.EnsureVisible(lvItem.Index);
                    break;

                case DataEventType.UPDATE_EVENT:
                    lvItem = lvwVisitors.Items[lvwVisitors.SelectedItems[0].Index];
                    lvItem.Text = e.VName;
                    lvItem.SubItems[1].Text = e.Company;
                    lvItem.SubItems[2].Text = e.ContactNo;
                    //lvItem.SubItems[3].Text = e.RegNo;

                    lvwVisitors.EnsureVisible(lvwVisitors.SelectedItems[0].Index);
                    break;
            }
            /// Below function are called to reload Company and ContactNo List.
            FillCompany();
            FillContactNo();
        }
    }
}
