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
using UI.Reports;
using CrystalDecisions;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;

namespace UI
{
    public partial class frmVehInOutList : Form
    {

        #region Private Variable
        private bool flgLoading;
        private bool flgList;
        private bool flgListCancel;
        private bool flgShowAll;

        private int dbid;
        private string mEntryType;

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

        public string EntryType
        {
            get
            {
                return mEntryType;
            }
            set
            {
                mEntryType = value.ToUpper();
            }
        }
        #endregion

        #region Constructor
        public frmVehInOutList()
        {
            InitializeComponent();
            InitializeListView();
        }

        public frmVehInOutList(UserCompany currentCompany, User currentUser)
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

            this.lvwVehInOut.ContextMenuStrip = conMenu;
            this.lvwVehInOut.FullRowSelect = true;
            this.lvwVehInOut.GridLines = true;
            this.lvwVehInOut.ListViewItemSorter = lvwColSorter;
            this.lvwVehInOut.MultiSelect = false;
            this.lvwVehInOut.View = View.Details;
        }

        private void FillList()
        {
            int intEntryType = 0;
            if (EntryType == "IN/OUT OTHER")
                intEntryType = 1;
            else
                if (EntryType == "COMPANY")
                intEntryType = 4;

            VehInOutList objList = new VehInOutList();
            objList = VehInOutManager.GetList(dtpDate.Value, flgShowAll, intEntryType);

            lvwVehInOut.Items.Clear();

            if (objList != null)
            {
                foreach (VehInOut objVehInOut in objList)
                {
                    ListViewItem objLvwItem = new ListViewItem();
                    objLvwItem.Name = Convert.ToString(objVehInOut.Dbid);
                    objLvwItem.Text = Convert.ToString(objVehInOut.EntryNo);
                    if (objVehInOut.Type == 1)
                        objLvwItem.SubItems.Add("IN/OUT OTHER");
                    else if (objVehInOut.Type == 4)
                        objLvwItem.SubItems.Add("COMPANY");
                    objLvwItem.SubItems.Add(Convert.ToString(objVehInOut.VehNo));
                    if (objVehInOut.InOut == 1)
                        objLvwItem.SubItems.Add("IN");
                    else if (objVehInOut.InOut == 2)
                        objLvwItem.SubItems.Add("OUT");

                    objLvwItem.SubItems.Add(objVehInOut.DriverName);
                    if (objVehInOut.InDate != DateTime.MinValue)
                        objLvwItem.SubItems.Add(objVehInOut.InDate.ToShortDateString());
                    else
                        objLvwItem.SubItems.Add("");
                    
                    if (objVehInOut.InTime != DateTime.MinValue)
                        objLvwItem.SubItems.Add(objVehInOut.InTime.ToShortTimeString());
                    else
                        objLvwItem.SubItems.Add("");
                    
                    if (objVehInOut.OutDate != DateTime.MinValue)
                        objLvwItem.SubItems.Add(objVehInOut.OutDate.ToShortDateString());
                    else
                        objLvwItem.SubItems.Add("");

                    if (objVehInOut.OutTime != DateTime.MinValue)
                        objLvwItem.SubItems.Add(objVehInOut.OutTime.ToShortTimeString());
                    else
                        objLvwItem.SubItems.Add("");

                    lvwVehInOut.Items.Add(objLvwItem);
                    if (objVehInOut.InOut == 1)
                        objLvwItem.ForeColor = Color.Blue;
                    else if (objVehInOut.InOut == 2)
                        objLvwItem.ForeColor = Color.Red;
                }
            }
        }

        private void SetButtonVisibility()
        {
            btnOk.Visible = IsList;
            btnCancel.Visible = IsList;
            btnNew.Visible = !IsList;
        }
        private void FillTypes()
        {
            cboType.Items.Add("ALL");
            cboType.Items.Add("COMPANY");
            cboType.Items.Add("IN/OUT OTHER");
        }
        #endregion

        #region UI Control Logic
        private void frmVehInOutList_Load(object sender, EventArgs e)
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

        private void dtpDate_ValueChanged(object sender, EventArgs e)
        {
            flgShowAll = false;
            FillList();
        }

        private void chkShowAll_CheckedChanged(object sender, EventArgs e)
        {
            flgShowAll = chkShowAll.Checked;
            dtpDate.Enabled = !chkShowAll.Checked;

            FillList();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsList && lvwVehInOut.SelectedItems != null && lvwVehInOut.SelectedItems.Count == 1)
                {
                    dbid = Convert.ToInt32(lvwVehInOut.SelectedItems[0].Name);
                    //Name = lvwAppoints.SelectedItems[0].Text;
                    //descr = lvwAppoints.SelectedItems[0].SubItems[1].Text;

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
                   // Name = string.Empty;
                    //descr = string.Empty;

                    flgListCancel = true;
                }
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        
        private void lvwVehInOut_DoubleClick(object sender, EventArgs e)
        {
            if (lvwVehInOut.SelectedItems != null && lvwVehInOut.SelectedItems.Count != 0)
            {
                modifyToolStripMenuItem_Click(sender, e);
            }
        }
        
        private void lvwVehInOut_ColumnClick(object sender, ColumnClickEventArgs e)
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
            this.lvwVehInOut.Sort();
        }
        
        private void lvwVehInOut_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (lvwVehInOut.SelectedItems.Count == 1 && e.KeyChar == (char)13)
            {
                modifyToolStripMenuItem_Click(sender, e);
            }
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            grbReport.Visible = true;
            dtpFromDate.Value = DateTime.Now.Date;
            dtpToDate.Value = DateTime.Now.Date;
            FillTypes();
            cboType.Text = "ALL";
        }

        private void btnRptView_Click(object sender, EventArgs e)
        {
            try
            {
                frmReportViewer objRpt = new frmReportViewer();
                ReportDocument objRptDoc = new ReportDocument();
                ConnectionInfo objConInfo = new ConnectionInfo();
                TableLogOnInfo objTableLogOnInfo = new TableLogOnInfo();
                Tables objCrTables;

                objRptDoc.Load(AppDomain.CurrentDomain.BaseDirectory + "\\Reports\\rptVehInOutDet.rpt");
                objConInfo.ServerName = GeneralBLL.GetSqlServerPCName();
                objConInfo.DatabaseName = GeneralBLL.GetDatabaseName();
                objConInfo.UserID = GeneralBLL.GetDBUserName();
                objConInfo.Password = GeneralBLL.GetDBPwd();
                objConInfo.IntegratedSecurity = false;

                objCrTables = objRptDoc.Database.Tables;
                foreach (CrystalDecisions.CrystalReports.Engine.Table objCrTable in objCrTables)
                {
                    objTableLogOnInfo = objCrTable.LogOnInfo;
                    objTableLogOnInfo.ConnectionInfo = objConInfo;
                    objCrTable.ApplyLogOnInfo(objTableLogOnInfo);
                }

                DateTime mFromDate = Convert.ToDateTime(dtpFromDate.Text);
                DateTime mToDate = Convert.ToDateTime(dtpToDate.Text);
                int tempValue;
                string strSelect;
                strSelect = " Date({VEHINOUTDETAIL.ENTRYDATE})>= #" + dtpFromDate.Text + "# And Date({VEHINOUTDETAIL.ENTRYDATE})<= #" + dtpToDate.Text + "# ";
                //if (cboType.Text == "ALL")
                //{
                //    //objRptDoc.RecordSelectionFormula = " Date({VEHINOUTDETAIL.ENTRYDATE})>= #" + dtpFromDate.Text + "# And Date({VEHINOUTDETAIL.ENTRYDATE})<= #" + dtpToDate.Text + "# ";
                //    strSelect = " Date({VEHINOUTDETAIL.ENTRYDATE})>= #" + dtpFromDate.Text + "# And Date({VEHINOUTDETAIL.ENTRYDATE})<= #" + dtpToDate.Text + "# ";
                //}
                if (cboType.Text == "COMPANY")
                {
                    tempValue = 1;
                    //objRptDoc.RecordSelectionFormula = " Date({VEHINOUTDETAIL.ENTRYDATE})>= #" + dtpFromDate.Text +
                    //    "# And Date({VEHINOUTDETAIL.ENTRYDATE})<= #" + dtpToDate.Text +
                    //    "# And ({VEHINOUTDETAIL.TYPE}) = " + tempValue;
                    strSelect += " And ({VEHINOUTDETAIL.TYPE}) = " + tempValue;
                }
                else if (cboType.Text == "IN/OUT OTHER")
                {
                    tempValue = 4;
                    //objRptDoc.RecordSelectionFormula = " Date({VEHINOUTDETAIL.ENTRYDATE})>= #" + dtpFromDate.Text +
                    //    "# And Date({VEHINOUTDETAIL.ENTRYDATE})<= #" + dtpToDate.Text +
                    //    "# And ({VEHINOUTDETAIL.TYPE}) = " + tempValue;
                    strSelect += " And ({VEHINOUTDETAIL.TYPE}) = " + tempValue;
                }

                objRptDoc.RecordSelectionFormula = strSelect;
                objRpt.crystalReportViewer1.ReportSource = objRptDoc;
                objRpt.MdiParent = this.MdiParent;
                objRpt.WindowState = FormWindowState.Maximized;
                objRpt.crystalReportViewer1.Refresh();
                objRpt.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnRptCancel_Click(object sender, EventArgs e)
        {
            grbReport.Visible = false;
            lvwVehInOut.Focus();
        }
        #endregion

        #region Context Menu
        private void conMenu_Opening(object sender, CancelEventArgs e)
        {
            if (!IsList)
            {
                if (lvwVehInOut.SelectedItems != null && lvwVehInOut.SelectedItems.Count != 0)
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
                if (lvwVehInOut.SelectedItems != null && lvwVehInOut.SelectedItems.Count != 0)
                {
                    if (IsList)
                    {
                        btnOk_Click(sender, e);
                    }
                    else
                    {
                        if (objUIRights.ModifyRight)
                        {
                            VehInOut objVehInOut;
                            frmVehInOutProp objFrmProp;

                            objVehInOut = VehInOutManager.GetItem(Convert.ToInt32(lvwVehInOut.SelectedItems[0].Name));
                            objFrmProp = new frmVehInOutProp(objVehInOut);
                            objFrmProp.MdiParent = this.MdiParent;
                            objFrmProp.Entry_DataChanged += new frmVehInOutProp.VehInOutUpdateHandler(Entry_DataChanged);
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
                        VehInOut objVehInOut;
                        frmVehInOutProp objFrmProp;

                        objVehInOut = new VehInOut();
                        objFrmProp = new frmVehInOutProp(objVehInOut);
                        objFrmProp.IsNew = true;
                        objFrmProp.EntryType = this.EntryType;
                        objFrmProp.MdiParent = this.MdiParent;
                        objFrmProp.Entry_DataChanged += new frmVehInOutProp.VehInOutUpdateHandler(Entry_DataChanged);
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
                if (lvwVehInOut.SelectedItems != null && lvwVehInOut.SelectedItems.Count != 0)
                {
                    if (!IsList)
                    {
                        if (objUIRights.DeleteRight)
                        {
                            DialogResult dr = new DialogResult();
                            dr = MessageBox.Show("Do You Really Want to Delete Record ?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                            if (dr == DialogResult.Yes)
                            {

                                VehInOut objVehInOut = new VehInOut();
                                objVehInOut = VehInOutManager.GetItem(Convert.ToInt32(lvwVehInOut.SelectedItems[0].Name));
                                VehInOutManager.Delete(objVehInOut);
                                lvwVehInOut.Items.Remove(lvwVehInOut.SelectedItems[0]);
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

        private void Entry_DataChanged(object sender, VehInOutUpdateEventArgs e, DataEventType Action)
        {
            ListViewItem lvItem;
            switch (Action)
            {
                case DataEventType.INSERT_EVENT:

                    lvItem = new ListViewItem();
                    lvItem.Name = Convert.ToString(e.DBID);
                    lvItem.Text = Convert.ToString(e.EntryNo);
                    if (e.EntryType == 1)
                        lvItem.SubItems.Add("IN/OUT OTHER");
                    else if (e.EntryType == 4)
                        lvItem.SubItems.Add("COMPANY");

                    lvItem.SubItems.Add(e.VehicleNo);
                    if (e.InOUt == 1)
                        lvItem.SubItems.Add("IN");
                    else if (e.InOUt == 2)
                        lvItem.SubItems.Add("OUT");

                    lvItem.SubItems.Add(e.DriverName);
                    if (e.InDate != DateTime.MinValue)
                        lvItem.SubItems.Add(e.InDate.ToShortDateString());
                    else
                        lvItem.SubItems.Add("");
                    if (e.InTime != DateTime.MinValue)
                        lvItem.SubItems.Add(e.InTime.ToShortTimeString());
                    else
                        lvItem.SubItems.Add("");
                    if (e.OutDate != DateTime.MinValue)
                        lvItem.SubItems.Add(e.OutDate.ToShortDateString());
                    else
                        lvItem.SubItems.Add("");
                    if (e.OutTime != DateTime.MinValue)
                        lvItem.SubItems.Add(e.OutTime.ToShortTimeString());
                    else
                        lvItem.SubItems.Add("");

                    lvwVehInOut.Items.Add(lvItem);
                    if (e.InOUt == 1)
                        lvItem.ForeColor = Color.Blue;
                    else if (e.InOUt == 2)
                        lvItem.ForeColor = Color.Red;
                    lvwVehInOut.EnsureVisible(lvItem.Index);
                    break;

                case DataEventType.UPDATE_EVENT:
                    lvItem = lvwVehInOut.Items[lvwVehInOut.SelectedItems[0].Index];
                    lvItem.Text = Convert.ToString(e.EntryNo);
                    if (e.EntryType == 1)
                        lvItem.SubItems[1].Text = "IN/OUT OTHER";
                    else if (e.EntryType == 4)
                        lvItem.SubItems[1].Text = "COMPANY";
                    lvItem.SubItems[2].Text = e.VehicleNo;
                    if (e.InOUt == 1)
                        lvItem.SubItems[3].Text = "IN";
                    else if (e.InOUt == 2)
                        lvItem.SubItems[3].Text = "OUT";

                    lvItem.SubItems[4].Text = e.DriverName;
                    if (e.InDate != DateTime.MinValue)
                        lvItem.SubItems[5].Text = e.InDate.ToShortDateString();
                    if (e.InTime != DateTime.MinValue)
                        lvItem.SubItems[6].Text = e.InTime.ToShortTimeString();
                    if (e.OutDate != DateTime.MinValue)
                        lvItem.SubItems[7].Text = e.OutDate.ToShortDateString();
                    if (e.OutTime != DateTime.MinValue)
                        lvItem.SubItems[8].Text = e.OutTime.ToShortTimeString();

                    lvwVehInOut.EnsureVisible(lvwVehInOut.SelectedItems[0].Index);
                    if (e.InOUt == 1)
                        lvItem.ForeColor = Color.Blue;
                    else if (e.InOUt == 2)
                        lvItem.ForeColor = Color.Red;
                    break;
            }
        }
    }
}
