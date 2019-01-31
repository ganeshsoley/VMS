using System;
using System.Collections.Generic;
using System.ComponentModel;
using CrystalDecisions.CrystalReports.Engine;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UI.Reports;
using CrystalDecisions.Shared;
using BLL;

namespace UI
{
    public partial class frmReportViewer : Form
    {
        public frmReportViewer()
        {
            InitializeComponent();
        }

        private void frmReportViewer_Load(object sender, EventArgs e)
        {
            ////ReportDocument objRpt = new ReportDocument();
            ////ConnectionInfo objConInfo = new ConnectionInfo();
            ////TableLogOnInfo objTableLogOnInfo = new TableLogOnInfo();
            ////Tables objCrTables;

            ////objRpt.Load(AppDomain.CurrentDomain.BaseDirectory + "\\Reports\\rptRetDCDetail.rpt");
            ////objConInfo.ServerName = GeneralBLL.GetSqlServerPCName();
            ////objConInfo.DatabaseName = GeneralBLL.GetDatabaseName();
            ////objConInfo.UserID = GeneralBLL.GetDBUserName();
            ////objConInfo.Password = GeneralBLL.GetDBPwd();
            ////objConInfo.IntegratedSecurity = false;

            ////objCrTables = objRpt.Database.Tables;
            ////foreach (CrystalDecisions.CrystalReports.Engine.Table objCrTable in objCrTables)
            ////{
            ////    objTableLogOnInfo = objCrTable.LogOnInfo;
            ////    objTableLogOnInfo.ConnectionInfo = objConInfo;
            ////    objCrTable.ApplyLogOnInfo(objTableLogOnInfo);
            ////}

            //////objRpt.RecordSelectionFormula = strRecSelect;
            ////crystalReportViewer1.ReportSource = objRpt;
            ////this.WindowState = FormWindowState.Maximized;
            ////crystalReportViewer1.RefreshReport();
            ////Show();

        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {

        }
    }
}
