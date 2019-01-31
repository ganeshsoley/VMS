using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using EntityObject;
using EntityObject.Enum;
using BLL;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using System.Data;
using UI.Reports;


namespace UI
{
    public partial class frmReturnableDCList : Form
    {
        #region Private Variable(s)
        private bool flgLoading;
        private bool flgList;
        private bool flgListCancel;
        private bool flgShowAll;

        private int dbid;
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
        #endregion

        #region Constructor(s)
        public frmReturnableDCList()
        {
            InitializeComponent();
            InitializeListView();
        }

        public frmReturnableDCList(UserCompany currentCompany, User currentUser)
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

            this.lvwDCList.ContextMenuStrip = conMenu;
            this.lvwDCList.FullRowSelect = true;
            this.lvwDCList.GridLines = true;
            this.lvwDCList.ListViewItemSorter = lvwColSorter;
            this.lvwDCList.MultiSelect = false;
            this.lvwDCList.View = View.Details;
        }

        private void FillCboType()
        {
            cboEntryType.Items.Clear();
            cboEntryType.Items.Add("INWARD");
            cboEntryType.Items.Add("OUTWARD");
            cboEntryType.Text = "INWARD";
        }

        private void FillList()
        {
            ReturnableDCList objList = new ReturnableDCList();
            objList = ReturnableDCManager.GetList(cboEntryType.Text, dtpDate.Value, flgShowAll);

            lvwDCList.Items.Clear();

            if (objList != null)
            {
                foreach (ReturnableDC objDC in objList)
                {
                    ListViewItem objLvwItem = new ListViewItem();
                    objLvwItem.Name = Convert.ToString(objDC.DBID);
                    objLvwItem.Text = Convert.ToString(objDC.EntryNo);
                    objLvwItem.SubItems.Add(objDC.EntryDate.ToShortDateString());
                    objLvwItem.SubItems.Add(objDC.PartyName);
                    objLvwItem.SubItems.Add(objDC.EntryType);
                    objLvwItem.SubItems.Add(objDC.DCNo);
                    objLvwItem.SubItems.Add(objDC.DCDate.ToShortDateString());
                    if (objDC.EntryType == "INWARD" & objDC.VOutDate != DateTime.MinValue)
                        objLvwItem.ForeColor = Color.Red;
                    //else
                    //    objLvwItem.ForeColor = Color.Blue;

                    if (objDC.EntryType == "OUTWARD" & objDC.VInDate != DateTime.MinValue)
                        objLvwItem.ForeColor = Color.Red;
                    //else
                    //    objLvwItem.ForeColor = Color.Blue;

                    lvwDCList.Items.Add(objLvwItem);
                }
            }
        }

        private void SetButtonVisibility()
        {
            btnOk.Visible = IsList;
            btnCancel.Visible = IsList;
            btnNew.Visible = !IsList;
            //btnReport.Visible = !IsList;
        }

        private void FillParties()
        {
            string[] PartiesList = ReturnableDCManager.GetPartys();

            cboParty.Items.Clear();
            if (PartiesList != null)
            {
                cboParty.Items.Add("ALL");
                foreach (string str in PartiesList)
                {
                    cboParty.Items.Add(str);
                }
            }
        }
        #endregion

        #region UI Control Logic
        private void frmReturnableDCList_Load(object sender, EventArgs e)
        {
            this.Icon = new Icon("Images/DTPL.ico");
            flgLoading = true;
            FillCboType();
            FillList();
            SetButtonVisibility();
            
            if (IsList)
            {
                this.CancelButton = btnCancel;
            }
            flgLoading = false;
        }

        private void frmReturnableDCList_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (dbid == 0)
                flgListCancel = true;
        }

        private void cboEntryType_SelectedIndexChanged(object sender, EventArgs e)
        {
            flgShowAll = false;
            FillList();
        }

        private void dtpDate_ValueChanged(object sender, EventArgs e)
        {
            flgShowAll = false;
            FillList();
        }

        private void btnShowAll_Click(object sender, EventArgs e)
        {
            
        }

        private void lvwDCList_ColumnClick(object sender, ColumnClickEventArgs e)
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
            this.lvwDCList.Sort();
        }

        private void lvwDCList_DoubleClick(object sender, EventArgs e)
        {
            if (lvwDCList.SelectedItems != null && lvwDCList.SelectedItems.Count != 0)
            {
                modifyToolStripMenuItem_Click(sender, e);
            }
        }

        private void lvwDCList_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (lvwDCList.SelectedItems.Count == 1 && e.KeyChar == (char)Keys.Enter)
            {
                modifyToolStripMenuItem_Click(sender, e);
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsList && lvwDCList.SelectedItems != null && lvwDCList.SelectedItems.Count == 1)
                {
                    dbid = Convert.ToInt32(lvwDCList.SelectedItems[0].Name);
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
        private void conMenu_Opening(object sender, CancelEventArgs e)
        {
            if (!IsList)
            {
                if (lvwDCList.SelectedItems != null && lvwDCList.SelectedItems.Count != 0)
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
                if (lvwDCList.SelectedItems != null && lvwDCList.SelectedItems.Count != 0)
                {
                    if (IsList)
                    {
                        btnOk_Click(sender, e);
                    }
                    else
                    {
                        if (objUIRights.ModifyRight)
                        {
                            ReturnableDC objDC;
                            frmReturnableDCProp objFrmProp;

                            objDC = ReturnableDCManager.GetItem(Convert.ToInt32(lvwDCList.SelectedItems[0].Name), true);
                            objFrmProp = new frmReturnableDCProp(objDC, currentUser);
                            objFrmProp.MdiParent = this.MdiParent;
                            objFrmProp.Entry_DataChanged += new frmReturnableDCProp.DCUpdateHandler(Entry_DataChanged);
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
                        ReturnableDC objDC;
                        frmReturnableDCProp objFrmProp;

                        objDC = new ReturnableDC();
                        objFrmProp = new frmReturnableDCProp(objDC, currentUser);
                        objFrmProp.IsNew = true;
                        objFrmProp.MdiParent = this.MdiParent;
                        objFrmProp.Entry_DataChanged += new frmReturnableDCProp.DCUpdateHandler(Entry_DataChanged);
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
                if (lvwDCList.SelectedItems != null && lvwDCList.SelectedItems.Count != 0)
                {
                    if (!IsList)
                    {
                        if (objUIRights.DeleteRight)
                        {
                            DialogResult dr = new DialogResult();
                            dr = MessageBox.Show("Do You Really Want to Delete Record ?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                            if (dr == DialogResult.Yes)
                            {
                                ReturnableDC objDC = new ReturnableDC();
                                objDC = ReturnableDCManager.GetItem(Convert.ToInt32(lvwDCList.SelectedItems[0].Name));
                                ReturnableDCManager.Delete(objDC);
                                lvwDCList.Items.Remove(lvwDCList.SelectedItems[0]);
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

        private void Entry_DataChanged(object sender, DCUpdateEventArgs e, DataEventType Action)
        {
            ListViewItem lvItem;
            switch (Action)
            {
                case DataEventType.INSERT_EVENT:

                    lvItem = new ListViewItem();
                    lvItem.Name = Convert.ToString(e.DBID);
                    lvItem.Text = Convert.ToString(e.EntryNo);
                    lvItem.SubItems.Add(e.EntryDate.ToShortDateString());
                    lvItem.SubItems.Add(e.PartyName);
                    lvItem.SubItems.Add(e.EntryType);
                    lvItem.SubItems.Add(e.DCNo);
                    lvItem.SubItems.Add(e.DCDate.ToShortDateString());

                    lvwDCList.Items.Add(lvItem);
                    lvwDCList.EnsureVisible(lvItem.Index);

                    if (e.EntryType == "INWARD" & e.OutDate != DateTime.MinValue)
                        lvItem.ForeColor = Color.Red;

                    if (e.EntryType == "OUTWARD" & e.InDate != DateTime.MinValue)
                        lvItem.ForeColor = Color.Red;

                    break;

                case DataEventType.UPDATE_EVENT:
                    lvItem = lvwDCList.Items[lvwDCList.SelectedItems[0].Index];
                    lvItem.Text = Convert.ToString(e.EntryNo);
                    lvItem.SubItems[1].Text = e.EntryDate.ToShortDateString();
                    lvItem.SubItems[2].Text = e.PartyName;
                    lvItem.SubItems[3].Text = e.EntryType;
                    lvItem.SubItems[4].Text = e.DCNo;
                    lvItem.SubItems[5].Text = e.DCDate.ToShortDateString();

                    lvwDCList.EnsureVisible(lvwDCList.SelectedItems[0].Index);

                    if (e.EntryType == "INWARD" & e.OutDate != DateTime.MinValue)
                        lvItem.ForeColor = Color.Red;

                    if (e.EntryType == "OUTWARD" & e.InDate != DateTime.MinValue)
                        lvItem.ForeColor = Color.Red;

                    break;
            }
        }

        private void cboEntryType_KeyPress(object sender, KeyPressEventArgs e)
        {
            GeneralMethods.AutoComplete(this.cboEntryType, e, true);
        }

        private void chkShowAll_CheckedChanged(object sender, EventArgs e)
        {
            flgShowAll = chkShowAll.Checked;
            dtpDate.Enabled = !chkShowAll.Checked;
            FillList();
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            grbReport.Visible = true;
            dtpFromDate.Value = DateTime.Now.Date;
            dtpToDate.Value = DateTime.Now.Date;
            FillParties();
            cboParty.Text = "ALL";
        }

        private void btnRptView_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    //string strRecSelect = null;

            //    ReportDocument objRptDoc = new ReportDocument();
            //    ConnectionInfo objConInfo = new ConnectionInfo();
            //    TableLogOnInfo objTableLogOnInfo = new TableLogOnInfo();
            //    Tables objCrTables;

            //    objRptDoc.Load(AppDomain.CurrentDomain.BaseDirectory + "\\Reports\\rptRetDCDetail.rpt");
            //    objConInfo.ServerName = GeneralBLL.GetSqlServerPCName();
            //    objConInfo.DatabaseName = GeneralBLL.GetDatabaseName();
            //    objConInfo.UserID = GeneralBLL.GetDBUserName();
            //    objConInfo.Password = GeneralBLL.GetDBPwd();
            //    objConInfo.IntegratedSecurity = false;

            //    objCrTables = objRptDoc.Database.Tables;
            //    foreach (Table objCrTable in objCrTables)
            //    {
            //        objTableLogOnInfo = objCrTable.LogOnInfo;
            //        objTableLogOnInfo.ConnectionInfo = objConInfo;
            //        objCrTable.ApplyLogOnInfo(objTableLogOnInfo);
            //    }

            //    DateTime mFromDate = Convert.ToDateTime(dtpFromDate.Text);
            //    DateTime mToDate = Convert.ToDateTime(dtpToDate.Text);

            //    //strRecSelect = " Date({RETURNABLEDC.ENTRYDATE})>= #" + mFromDate.ToString("yyyy-MM-dd") + "# And Date({RETURNABLEDC.ENTRYDATE})<= #" + mToDate.ToString("yyyy-MM-dd") + "#";
            //    //strRecSelect = " Date({RETURNABLEDC.ENTRYDATE})>= @mFromDate And Date({RETURNABLEDC.ENTRYDATE})<= @mToDate ";
            //    //if (cboParty.Text.Trim() != "ALL")
            //    //{
            //    //    //objRptDoc.RecordSelectionFormula = " Date({RETURNABLEDC.ENTRYDATE})>= #" + dtpFromDate.Text + "# And Date({RETURNABLEDC.ENTRYDATE})<= #" + dtpToDate.Text + "# And ({RETURNABLEDC.PARTYNAME}) = '" + cboParty.Text + "'";
            //    //    strRecSelect += " And ({RETURNABLEDC.PARTYNAME}) = '" + cboParty.Text + "'";
            //    //}

            //    //objRptDoc.RecordSelectionFormula = strRecSelect;


            //    // Start
            //    objRptDoc.ParameterFields.Add("mFromDate");
            //    objRptDoc.ParameterFields.Add("mToDate");
            //    objRptDoc.SetParameterValue("mFromDate", mFromDate.ToShortDateString());
            //    objRptDoc.SetParameterValue("mToDate", mToDate.ToShortDateString());
            //    if (cboParty.Text.Trim() != "ALL")
            //    {
            //        objRptDoc.ParameterFields.Add("mParty");
            //        objRptDoc.SetParameterValue("mParty", cboParty.Text);
            //    }

            //    // End

            //    frmReportViewer objFrmRpt = new frmReportViewer();
            //    objFrmRpt.crystalReportViewer1.ReportSource = objRptDoc;
            //    objFrmRpt.MdiParent = this.MdiParent;
            //    objFrmRpt.WindowState = FormWindowState.Maximized;
            //    objFrmRpt.crystalReportViewer1.Refresh();
            //    objFrmRpt.Show();

            //    //objRpt.crystalReportViewer1.ReportSource = objRptDoc;
            //    //objRpt.MdiParent = this.MdiParent;
            //    //objRpt.WindowState = FormWindowState.Maximized;
            //    //objRpt.crystalReportViewer1.Refresh();
            //    //objRpt.Show();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.InnerException.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //}

            try
            {
                DateTime mFromDate, mToDate;
                ConnectionInfo objConInfo = new ConnectionInfo();
                TableLogOnInfo objTableLogOnInfo = new TableLogOnInfo();
                Tables objCrTables;
                rptRetDCReport objRptDoc = new rptRetDCReport();
                frmReportViewer objFrmRpt = new frmReportViewer();

                mFromDate = dtpFromDate.Value.Date;
                mToDate = dtpToDate.Value.Date;

                objRptDoc.SetParameterValue("@EntryType", cboEntryType.Text);
                objRptDoc.SetParameterValue("@FromDate", mFromDate);
                objRptDoc.SetParameterValue("@ToDate", mToDate);

                objConInfo.ServerName = GeneralBLL.GetSqlServerPCName();
                objConInfo.DatabaseName = GeneralBLL.GetDatabaseName();
                objConInfo.UserID = GeneralBLL.GetDBUserName();
                objConInfo.Password = GeneralBLL.GetDBPwd();
                objConInfo.IntegratedSecurity = false;

                objCrTables = objRptDoc.Database.Tables;
                foreach (Table objCrTable in objCrTables)
                {
                    objTableLogOnInfo = objCrTable.LogOnInfo;
                    objTableLogOnInfo.ConnectionInfo = objConInfo;
                    objCrTable.ApplyLogOnInfo(objTableLogOnInfo);
                }

                objFrmRpt.crystalReportViewer1.ReportSource = null;
                objFrmRpt.crystalReportViewer1.ReportSource = objRptDoc;
                objFrmRpt.MdiParent = this.MdiParent;
                objFrmRpt.WindowState = FormWindowState.Maximized;
                objFrmRpt.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnRptCancel_Click(object sender, EventArgs e)
        {
            grbReport.Visible = false;
            lvwDCList.Focus();
        }
    }
}
