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
    public partial class frmAppointmentList : Form
    {
        #region Private Variable
        private bool flgLoading;
        private bool flgList;
        private bool flgListCancel;

        private int dbid;
        private int mEntryNo;
        private string mEntryDate;
        private string mName;

        private string strCondition;

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

        public int EntryNo
        {
            get
            {
                return mEntryNo;
            }
            set
            {
                mEntryNo = value;
            }
        }

        public string EntryDate
        {
            get
            {
                return mEntryDate;
            }
            set
            {
                mEntryDate = value;
            }
        }
        
        public string AppName
        {
            get
            {
                return mName;
            }
            set
            {
                mName = value;
            }
        }
        
        public string Condition
        {
            get
            {
                return strCondition;
            }
            set
            {
                strCondition = value;
            }
        }
        #endregion

        #region Constructor(s)
        public frmAppointmentList()
        {
            InitializeComponent();
            InitializeListView();
        }

        public frmAppointmentList(UserCompany currentCompany, User currentUser)
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

            this.lvwAppoints.ContextMenuStrip = conMenu;
            this.lvwAppoints.FullRowSelect = true;
            this.lvwAppoints.GridLines = true;
            this.lvwAppoints.ListViewItemSorter = lvwColSorter;
            this.lvwAppoints.MultiSelect = false;
            this.lvwAppoints.View = View.Details;
        }

        /// <summary>
        /// Fills AppointmentNos into ComboBox
        /// </summary>
        private void FillAppointmentNo()
        {
            string[] AppointmentList = AppointmentManager.GetAppointments();

            cboAppointmentNo.Items.Clear();
            if (AppointmentList != null)
            {
                cboAppointmentNo.Items.Add("All");
                foreach (string str in AppointmentList)
                {
                    cboAppointmentNo.Items.Add(str);
                }
                cboAppointmentNo.Text = "All";
            }
        }

        /// <summary>
        /// Fills Names into ComboBox
        /// </summary>
        private void FillNames()
        {
            string[] NameList = AppointmentManager.GetNames();

            cboName.Items.Clear();
            if (NameList != null)
            {
                cboName.Items.Add("All");
                foreach (string str in NameList)
                {
                    cboName.Items.Add(str);
                }
                cboName.Text = "All";
            }
        }

        private void FillList(string strAppointmentNo, string strName)
        {
            AppointmentList objList = new AppointmentList();
            if (strAppointmentNo == "All")
                strAppointmentNo = "";
            if (strName == "All")
                strName = "";
            objList = AppointmentManager.GetList(Condition, strAppointmentNo, strName, chkClosed.Checked);

            lvwAppoints.Items.Clear();

            if (objList != null)
            {
                foreach (Appointment objAppoint in objList)
                {
                    ListViewItem objLvwItem = new ListViewItem();
                    objLvwItem.Name = Convert.ToString(objAppoint.DBID);
                    objLvwItem.Text = Convert.ToString(objAppoint.EntryNo);
                    objLvwItem.SubItems.Add(objAppoint.EntryDate.ToShortDateString());
                    objLvwItem.SubItems.Add(Convert.ToString(objAppoint.AppointmentNo));
                    objLvwItem.SubItems.Add(objAppoint.Name);
                    objLvwItem.SubItems.Add(objAppoint.AppointmentDate.ToShortDateString());
                    objLvwItem.SubItems.Add(objAppoint.ScheduleTime.ToShortTimeString());

                    lvwAppoints.Items.Add(objLvwItem);
                }
            }
        }

        private void SetButtonVisibility()
        {
            btnOk.Visible = IsList;
            btnCancel.Visible = IsList;
            btnNew.Visible = !IsList;
            chkClosed.Visible = !IsList;
        }

        private void Entry_DataChanged(object sender, AppointUpdateEventArgs e, DataEventType Action)
        {
            ListViewItem lvItem;
            switch (Action)
            {
                case DataEventType.INSERT_EVENT:

                    lvItem = new ListViewItem();
                    lvItem.Name = Convert.ToString(e.DBID);
                    lvItem.Text = Convert.ToString(e.EntryNo);
                    lvItem.SubItems.Add(e.EntryDate.ToShortDateString());
                    lvItem.SubItems.Add(Convert.ToString(e.AppointmentNo));
                    lvItem.SubItems.Add(e.Name);
                    lvItem.SubItems.Add(e.AppointmentDate.ToShortDateString());
                    lvItem.SubItems.Add(e.SchTime.ToShortTimeString());

                    lvwAppoints.Items.Add(lvItem);
                    lvwAppoints.EnsureVisible(lvItem.Index);

                    break;

                case DataEventType.UPDATE_EVENT:
                    lvItem = lvwAppoints.Items[lvwAppoints.SelectedItems[0].Index];
                    lvItem.Text = Convert.ToString(e.EntryNo);
                    lvItem.SubItems[1].Text = e.EntryDate.ToShortDateString();
                    lvItem.SubItems[2].Text = Convert.ToString(e.AppointmentNo);
                    lvItem.SubItems[3].Text = e.Name;
                    lvItem.SubItems[4].Text = e.AppointmentDate.ToShortDateString();
                    lvItem.SubItems[5].Text = e.SchTime.ToShortTimeString();

                    lvwAppoints.Items[lvItem.Index].Selected = true;
                    lvwAppoints.EnsureVisible(lvwAppoints.SelectedItems[0].Index);

                    break;
            }
        }
        #endregion

        #region UI Control Logic
        private void frmAppointmentList_Load(object sender, EventArgs e)
        {
            this.Icon = new Icon("Images/DTPL.ico");
            flgLoading = true;

            FillAppointmentNo();
            FillNames();
            FillList(cboAppointmentNo.Text, cboName.Text);
            SetButtonVisibility();
            
            if (IsList)
            {
                this.CancelButton = btnCancel;
            }
            flgLoading = false;
        }

        private void frmAppointmentList_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (dbid == 0)
                flgListCancel = true;
        }

        private void cboAppointmentNo_Enter(object sender, EventArgs e)
        {
            cboAppointmentNo.SelectAll();
        }

        private void cboAppointmentNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            GeneralMethods.AutoComplete(cboAppointmentNo, e, true);
        }

        private void cboAppointmentNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillList(cboAppointmentNo.Text, cboName.Text);
        }

        private void cboName_Enter(object sender, EventArgs e)
        {
            cboName.SelectAll();
        }

        private void cboName_KeyPress(object sender, KeyPressEventArgs e)
        {
            GeneralMethods.AutoComplete(cboName, e, true);
        }

        private void cboName_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillList(cboAppointmentNo.Text, cboName.Text);
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsList && lvwAppoints.SelectedItems != null && lvwAppoints.SelectedItems.Count == 1)
                {
                    dbid = Convert.ToInt32(lvwAppoints.SelectedItems[0].Name);
                    mEntryNo = Convert.ToInt32(lvwAppoints.SelectedItems[0].Text);
                    mEntryDate = lvwAppoints.SelectedItems[0].SubItems[1].Text;
                    mName = lvwAppoints.SelectedItems[0].SubItems[2].Text;

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
        
        private void btnNew_Click(object sender, EventArgs e)
        {
            if (!IsList)
            {
                newToolStripMenuItem_Click(sender, e);
            }
        }
        
        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsList)
                {
                    dbid = 0;
                    mEntryNo = 0;
                    mEntryDate = string.Empty;
                    mName = string.Empty;

                    flgListCancel = true;
                }
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        
        private void lvwAppoints_DoubleClick(object sender, EventArgs e)
        {
            if (lvwAppoints.SelectedItems != null && lvwAppoints.SelectedItems.Count != 0)
            {
                modifyToolStripMenuItem_Click(sender, e);
            }
        }
        
        private void lvwAppoints_ColumnClick(object sender, ColumnClickEventArgs e)
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
            this.lvwAppoints.Sort();
        }
        
        private void lvwAppoints_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (lvwAppoints.SelectedItems.Count == 1 && e.KeyChar == (char)13)
            {
                modifyToolStripMenuItem_Click(sender, e);
            }
        }
        #endregion

        #region Context Menu
        private void conMenu_Opening(object sender, CancelEventArgs e)
        {
            if (!IsList)
            {
                if (lvwAppoints.SelectedItems != null && lvwAppoints.SelectedItems.Count != 0)
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
                if (lvwAppoints.SelectedItems != null && lvwAppoints.SelectedItems.Count != 0)
                {
                    if (IsList)
                    {
                        btnOk_Click(sender, e);
                    }
                    else
                    {
                        if (objUIRights.ModifyRight)
                        {
                            Appointment objAppoint;
                            frmAppointmentProp objFrmProp;

                            objAppoint = AppointmentManager.GetItem(Convert.ToInt32(lvwAppoints.SelectedItems[0].Name));
                            objFrmProp = new frmAppointmentProp(objAppoint);
                            objFrmProp.MdiParent = this.MdiParent;
                            objFrmProp.Entry_DataChanged += new frmAppointmentProp.AppointUpdateHandler(Entry_DataChanged);
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
                        Appointment objAppoint;
                        frmAppointmentProp objFrmProp;

                        objAppoint = new Appointment();
                        objFrmProp = new frmAppointmentProp(objAppoint);
                        objFrmProp.IsNew = true;
                        objFrmProp.MdiParent = this.MdiParent;
                        objFrmProp.Entry_DataChanged += new frmAppointmentProp.AppointUpdateHandler(Entry_DataChanged);
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
                if (lvwAppoints.SelectedItems != null && lvwAppoints.SelectedItems.Count != 0)
                {
                    if (!IsList)
                    {
                        if (objUIRights.DeleteRight)
                        {
                            DialogResult dr = new DialogResult();
                            dr = MessageBox.Show("Do You Really Want to Delete Record ?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                            if (dr == DialogResult.Yes)
                            {
                                Appointment objAppoint = new Appointment();
                                objAppoint = AppointmentManager.GetItem(Convert.ToInt32(lvwAppoints.SelectedItems[0].Name));
                                AppointmentManager.Delete(objAppoint);
                                lvwAppoints.Items.Remove(lvwAppoints.SelectedItems[0]);
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

        private void cancelAppointmentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (objUIRights.ModifyRight)
                {
                    Appointment objApmt;
                    objApmt = AppointmentManager.GetItem(Convert.ToInt32(lvwAppoints.SelectedItems[0].Name));
                    objApmt.ApmtClose = true;

                    bool flgApplyEdit;
                    flgApplyEdit = AppointmentManager.Save(objApmt);
                    if (flgApplyEdit)
                    {
                        // instance the event args and pass it value
                        AppointUpdateEventArgs args = new AppointUpdateEventArgs(objApmt.DBID, objApmt.EntryNo, objApmt.EntryDate, objApmt.AppointmentNo, objApmt.Name, objApmt.AppointmentDate, objApmt.ScheduleTime);

                        // raise event wtth  updated 
                        Entry_DataChanged(this, args, DataEventType.UPDATE_EVENT);
                    }
                    else
                    {
                        MessageBox.Show("Record Not Saved.");
                    }
                    FillList(cboAppointmentNo.Text, cboName.Text);
                }
                else
                {
                    throw new Exception("Not Authorised.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        #endregion

        private void chkClosed_CheckedChanged(object sender, EventArgs e)
        {
            FillList(cboAppointmentNo.Text, cboName.Text);
        }
    }
}
