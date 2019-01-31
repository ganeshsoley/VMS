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
using UI.Reports;
using CrystalDecisions;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;

namespace UI
{
    public partial class frmVisitorGPList : Form
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
        public frmVisitorGPList()
        {
            InitializeComponent();
            InitializeListView();
        }

        public frmVisitorGPList(UserCompany currentCompany, User currentUser)
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

            //this.lvwGPs.CheckBoxes = true;
            this.lvwGPs.ContextMenuStrip = contextMenu;
            this.lvwGPs.FullRowSelect = true;
            this.lvwGPs.GridLines = true;
            this.lvwGPs.ListViewItemSorter = lvwColSorter;
            this.lvwGPs.MultiSelect = false;
            this.lvwGPs.View = View.Details;
        }

        private void FillList()
        {
            int totVisitors = 0, totInVisitors = 0, totOutVisitors = 0;
            VisitorGatePassList objList = new VisitorGatePassList();
            objList = VisitorGatePassManager.GetList("", dtpDate.Value, flgShowAll);

            lvwGPs.Items.Clear();

            if (objList != null)
            {
                foreach (VisitorGatePass objVisitorGP in objList)
                {
                    ListViewItem objLvwItem = new ListViewItem();
                    objLvwItem.Name = Convert.ToString(objVisitorGP.DBID);
                    objLvwItem.Text = objVisitorGP.GatePassNo;
                    objLvwItem.SubItems.Add(objVisitorGP.VisitorName);
                    objLvwItem.SubItems.Add(objVisitorGP.GateDate.ToShortDateString());
                    objLvwItem.SubItems.Add(objVisitorGP.TimeIn.ToShortTimeString());
                    objLvwItem.SubItems.Add(objVisitorGP.ToMeet);
                    objLvwItem.SubItems.Add(objVisitorGP.Purpose);
                    if (objVisitorGP.TimeOut != DateTime.MinValue)
                        objLvwItem.SubItems.Add(objVisitorGP.TimeOut.ToShortTimeString());
                    else
                        objLvwItem.SubItems.Add("");

                    totVisitors += 1;
                    totInVisitors += 1;
                    if (objVisitorGP.TimeOut != DateTime.MinValue)
                        totOutVisitors += 1;

                    lvwGPs.Items.Add(objLvwItem);
                    if (objVisitorGP.TimeOut != DateTime.MinValue)
                    {
                        objLvwItem.ForeColor = Color.Red;
                    }
                    else
                    {
                        objLvwItem.ForeColor = Color.Blue;
                    }

                }
            }
            lblTotal.Text = Convert.ToString(totVisitors);
            lblTotalIn.Text = Convert.ToString(totInVisitors);
            lblTotalOut.Text = Convert.ToString(totOutVisitors);
        }

        private void SetButtonVisibility()
        {
            btnOk.Visible = IsList;
            btnCancel.Visible = IsList;
            btnNew.Visible = !IsList;
            btnReport.Visible = !IsList;
        }

        private void FillEmployees()
        {
            string[] EmployeeList = VisitorGatePassManager.GetEmployee();

            cboEmployee.Items.Clear();
            if (EmployeeList != null)
            {
                cboEmployee.Items.Add("ALL");
                foreach (string str in EmployeeList)
                {
                    cboEmployee.Items.Add(str);
                }
            }
        }
        #endregion

        #region UI Control Logic
        private void frmVisitorGPList_Load(object sender, EventArgs e)
        {
            this.Icon = new Icon("Images/DTPL.ico");
            flgLoading = true;
            dtpDate.Value = DateTime.Now;

            FillList();
            SetButtonVisibility();
            if (IsList)
            {
                this.CancelButton = btnCancel;
            }
            flgLoading = false;
        }

        private void frmVisitorGPList_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (dbid == 0)
                flgListCancel = true;
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

        private void lvwGPs_ColumnClick(object sender, ColumnClickEventArgs e)
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
            this.lvwGPs.Sort();
        }

        private void lvwGPs_DoubleClick(object sender, EventArgs e)
        {
            if (lvwGPs.SelectedItems != null && lvwGPs.SelectedItems.Count != 0)
            {
                modifyToolStripMenuItem_Click(sender, e);
            }
        }

        private void lvwGPs_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (lvwGPs.SelectedItems.Count == 1 && e.KeyChar == (char)Keys.Enter)
            {
                modifyToolStripMenuItem_Click(sender, e);
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsList && lvwGPs.SelectedItems != null && lvwGPs.SelectedItems.Count == 1)
                {
                    dbid = Convert.ToInt32(lvwGPs.SelectedItems[0].Name);
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

        private void btnReport_Click(object sender, EventArgs e)
        {
            grbReport.Visible = true;
            dtpFromDate.Value = DateTime.Now.Date;
            dtpToDate.Value = DateTime.Now.Date;
            FillEmployees();
            cboEmployee.Text = "ALL";
        }

        private void btnRptView_Click(object sender, EventArgs e)
        {
            frmReportViewer objRpt = new frmReportViewer();
            ReportDocument objRptDoc = new ReportDocument();
            ConnectionInfo objConInfo = new ConnectionInfo();
            TableLogOnInfo objTableLogOnInfo = new TableLogOnInfo();
            Tables objCrTables;

            objRptDoc.Load(AppDomain.CurrentDomain.BaseDirectory + "\\Reports\\rptEmpWiseVistDet.rpt");
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

            if (cboEmployee.Text != "ALL")
            {
                objRptDoc.RecordSelectionFormula = " Date({VISITORGATEPASS.GATEDATE})>= #" + dtpFromDate.Text + "# And Date({VISITORGATEPASS.GATEDATE})<= #" + dtpToDate.Text + "# And ({VISITORGATEPASS.TOMEET}) = '" + cboEmployee.Text + "'";
            }
            else
            {
                objRptDoc.RecordSelectionFormula = " Date({VISITORGATEPASS.GATEDATE})>= #" + dtpFromDate.Text + "# And Date({VISITORGATEPASS.GATEDATE})<= #" + dtpToDate.Text + "# ";
            }
            objRpt.crystalReportViewer1.ReportSource = objRptDoc;
            objRpt.MdiParent = this.MdiParent;
            objRpt.WindowState = FormWindowState.Maximized;
            objRpt.crystalReportViewer1.Refresh();
            objRpt.Show();
        }

        private void btnRptCancel_Click(object sender, EventArgs e)
        {
            grbReport.Visible = false;
            lvwGPs.Focus();
        }
        #endregion

        #region Context Menu
        private void contextMenu_Opening(object sender, CancelEventArgs e)
        {
            if (!IsList)
            {
                if (lvwGPs.SelectedItems != null && lvwGPs.SelectedItems.Count != 0)
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
                if (lvwGPs.SelectedItems != null && lvwGPs.SelectedItems.Count != 0)
                {
                    if (IsList)
                    {
                        btnOk_Click(sender, e);
                    }
                    else
                    {
                        if (objUIRights.ModifyRight)
                        {
                            VisitorGatePass objVisitorGP;
                            frmVisitorGPProp objFrmProp;

                            objVisitorGP = VisitorGatePassManager.GetItem(Convert.ToInt32(lvwGPs.SelectedItems[0].Name));
                            objFrmProp = new frmVisitorGPProp(objVisitorGP, currentUser);
                            objFrmProp.MdiParent = this.MdiParent;
                            objFrmProp.Entry_DataChanged += new frmVisitorGPProp.VisitorGPUpdateHandler(Entry_DataChanged);
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
                        VisitorGatePass objVisitorGP;
                        frmVisitorGPProp objFrmProp;

                        objVisitorGP = new VisitorGatePass();
                        objFrmProp = new frmVisitorGPProp(objVisitorGP, currentUser);
                        objFrmProp.IsNew = true;
                        objFrmProp.MdiParent = this.MdiParent;
                        objFrmProp.Entry_DataChanged += new frmVisitorGPProp.VisitorGPUpdateHandler(Entry_DataChanged);
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
                if (lvwGPs.SelectedItems != null && lvwGPs.SelectedItems.Count != 0)
                {
                    if (!IsList)
                    {
                        if (objUIRights.DeleteRight)
                        {
                            DialogResult dr = new DialogResult();
                            dr = MessageBox.Show("Do You Really Want to Delete Record ?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                            if (dr == DialogResult.Yes)
                            {
                                VisitorGatePass objVisitorGP = new VisitorGatePass();
                                objVisitorGP = VisitorGatePassManager.GetItem(Convert.ToInt32(lvwGPs.SelectedItems[0].Name));
                                VisitorGatePassManager.Delete(objVisitorGP);
                                lvwGPs.Items.Remove(lvwGPs.SelectedItems[0]);
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

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //rptVisitorGatePass rptGatePass = new rptVisitorGatePass();
            //frmReportViewer objRpt = new frmReportViewer();

            //rptGatePass.RecordSelectionFormula = "({VISITORGATEPASS.DBID}) = " + (lvwGPs.SelectedItems[0].Name);

            //objRpt.crystalReportViewer1.ReportSource = rptGatePass;
            //objRpt.crystalReportViewer1.Refresh();
            //objRpt.Show();
            try
            {
                if (objUIRights.PrintRight)
                {
                    // Ganesh Code Starts Here..
                    frmReportViewer objRpt = new frmReportViewer();
                    ReportDocument objRptDoc = new ReportDocument();
                    ConnectionInfo objConInfo = new ConnectionInfo();
                    TableLogOnInfo objTableLogOnInfo = new TableLogOnInfo();
                    Tables objCrTables;

                    objRptDoc.Load(AppDomain.CurrentDomain.BaseDirectory + "\\Reports\\rptVisitorGatePass.rpt");
                    objConInfo.ServerName = GeneralBLL.GetSqlServerPCName();
                    objConInfo.DatabaseName = GeneralBLL.GetDatabaseName();
                    objConInfo.UserID = GeneralBLL.GetDBUserName();
                    objConInfo.Password = GeneralBLL.GetDBPwd();
                    //objConInfo.ServerName = "ITProject";
                    //objConInfo.DatabaseName = "DTPL";
                    //objConInfo.UserID = "VMSUser";
                    //objConInfo.Password = "Dtpl@vms16";
                    objConInfo.IntegratedSecurity = false;

                    objCrTables = objRptDoc.Database.Tables;
                    foreach (Table objCrTable in objCrTables)
                    {
                        objTableLogOnInfo = objCrTable.LogOnInfo;
                        objTableLogOnInfo.ConnectionInfo = objConInfo;
                        objCrTable.ApplyLogOnInfo(objTableLogOnInfo);
                    }
                    objRptDoc.RecordSelectionFormula = "({VISITORGATEPASS.DBID}) = " + (lvwGPs.SelectedItems[0].Name);
                    objRpt.crystalReportViewer1.ReportSource = objRptDoc;
                    objRpt.MdiParent = this.MdiParent;
                    objRpt.WindowState = FormWindowState.Maximized;
                    objRpt.crystalReportViewer1.Refresh();
                    objRpt.Show();
                    // Ganesh Code Ends Here..
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

        private void checkOutVisitorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (objUIRights.AddRight)
                {
                    VisitorGatePass objVisitorGP;
                    objVisitorGP = VisitorGatePassManager.GetItem(Convert.ToInt32(lvwGPs.SelectedItems[0].Name));
                    objVisitorGP.TimeOut = DateTime.Now;

                    bool flgApplyEdit;
                    flgApplyEdit = VisitorGatePassManager.Save(objVisitorGP, currentUser);
                    if (flgApplyEdit)
                    {
                        // instance the event args and pass it value
                        VisitorGPUpdateEventArgs args = new VisitorGPUpdateEventArgs(objVisitorGP.DBID, objVisitorGP.GatePassNo, objVisitorGP.GateDate, objVisitorGP.VisitorName, objVisitorGP.ToMeet, objVisitorGP.Purpose, objVisitorGP.TimeIn, objVisitorGP.TimeOut);

                        // raise event wtth  updated 
                        Entry_DataChanged(this, args, DataEventType.UPDATE_EVENT);
                    }
                    else
                    {
                        MessageBox.Show("Record Not Saved.");
                    }
                    FillList();
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

        private void Entry_DataChanged(object sender, VisitorGPUpdateEventArgs e, DataEventType Action)
        {
            ListViewItem lvItem;
            switch (Action)
            {
                case DataEventType.INSERT_EVENT:

                    lvItem = new ListViewItem();
                    lvItem.Name = Convert.ToString(e.DBID);
                    lvItem.Text = Convert.ToString(e.GPNo);
                    lvItem.SubItems.Add(e.VisitorName);
                    lvItem.SubItems.Add(e.GPDate.ToShortDateString());
                    lvItem.SubItems.Add(e.TimeIn.ToShortTimeString());
                    lvItem.SubItems.Add(e.ToMeet);
                    lvItem.SubItems.Add(e.Purpose);
                    if (e.TimeOut != DateTime.MinValue)
                        lvItem.SubItems.Add(e.TimeOut.ToShortTimeString());
                    else
                        lvItem.SubItems.Add("");

                    lvwGPs.Items.Add(lvItem);
                    lvwGPs.EnsureVisible(lvItem.Index);
                    lvItem.ForeColor = Color.Blue;
                    lblTotal.Text = Convert.ToString(Convert.ToInt16(lblTotal.Text) + 1);
                    lblTotalIn.Text = Convert.ToString(Convert.ToInt16(lblTotalIn.Text) + 1);

                    break;

                case DataEventType.UPDATE_EVENT:
                    lvItem = lvwGPs.Items[lvwGPs.SelectedItems[0].Index];
                    lvItem.Text = Convert.ToString(e.GPNo);
                    lvItem.SubItems[1].Text = e.VisitorName;
                    lvItem.SubItems[2].Text = e.GPDate.ToShortDateString();
                    lvItem.SubItems[3].Text = e.TimeIn.ToShortTimeString();
                    lvItem.SubItems[4].Text = e.ToMeet;
                    lvItem.SubItems[5].Text = e.Purpose;
                    if (e.TimeOut != DateTime.MinValue)
                        lvItem.SubItems[6].Text = e.TimeOut.ToShortTimeString();
                    else
                        lvItem.SubItems[6].Text = "";

                    lvwGPs.EnsureVisible(lvwGPs.SelectedItems[0].Index);

                    break;
            }
        }
    }
}
