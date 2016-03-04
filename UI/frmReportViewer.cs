using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UI.Reports;  
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
          //  rptVisitorGatePass rptGatePass = new rptVisitorGatePass();
          ////  rptGatePass.RecordSelectionFormula = "({VISITORGATEPASS.DBID}) = " + (lvwGPs.SelectedItems[0].Name);
          //  rptGatePass.RecordSelectionFormula = "({VISITORGATEPASS.DBID}) = " + (1);
          //  this.crystalReportViewer1.ReportSource = rptGatePass;
          //  this.crystalReportViewer1.Refresh();
        }
        
    }
}
