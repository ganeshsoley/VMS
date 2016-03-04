using System;
using System.Drawing;
using System.Windows.Forms;
using BLL;
using EntityObject;

namespace UI
{
    public partial class frmMain : Form
    {
        //frmLogin objLogin = new frmLogin();

        #region Private Variable(s)
        public UserCompany CurrentCompany;
        private Int32 curUserID;
        private string[] curUserRights;
        User CurrentUser;
        #endregion

        public Int32 CurrentUserID
        {
            get
            {
                return curUserID;
            }
            set
            {
                curUserID = value;
            }
        }

        #region Private Method(s)
        private void DisableMenus()
        {
            DisableDefnMenus();
            DisableTransaction();
            DisableUtilities();
            DisableButtons();
        }

        private void DisableDefnMenus()
        {
            //UserCompany CurrentCompany = new UserCompany();

            //if (CurrentCompany.m_UserName == "MAINGATE")
            //{
            //    defineToolStripMenuItem.Enabled = false;
            //    deptMasterToolStripMenuItem.Enabled = false;
            //    employeeMasterToolStripMenuItem.Enabled = false;
            //    visitorGatePassToolStripMenuItem.Enabled = true;
            //    cityMasterToolStripMenuItem.Enabled = false;
            //    driverMasterToolStripMenuItem.Enabled = false;
            //    vehicleMasterToolStripMenuItem.Enabled = false;
            //    userProfilesToolStripMenuItem.Enabled = false;
            //    visitorMasterToolStripMenuItem.Enabled = true;
            //}
            //else if (CurrentCompany.m_UserName == "SYSADMIN")
            //{
            //    deptMasterToolStripMenuItem.Enabled = true;
            //    employeeMasterToolStripMenuItem.Enabled = true;
            //    visitorGatePassToolStripMenuItem.Enabled = true;
            //    cityMasterToolStripMenuItem.Enabled = true;
            //    driverMasterToolStripMenuItem.Enabled = true;
            //    vehicleMasterToolStripMenuItem.Enabled = true;
            //    userProfilesToolStripMenuItem.Enabled = true;
            //    visitorMasterToolStripMenuItem.Enabled = true;
            //}
            //else
            //{
            //    deptMasterToolStripMenuItem.Enabled = false;
            //    employeeMasterToolStripMenuItem.Enabled = false;
            //    visitorGatePassToolStripMenuItem.Enabled = false;
            //    cityMasterToolStripMenuItem.Enabled = false;
            //    driverMasterToolStripMenuItem.Enabled = false;
            //    vehicleMasterToolStripMenuItem.Enabled = false;
            //    userProfilesToolStripMenuItem.Enabled = false;
            //    visitorMasterToolStripMenuItem.Enabled = true;
            //}

            defineToolStripMenuItem.Enabled = false;
            deptMasterToolStripMenuItem.Enabled = false;
            employeeMasterToolStripMenuItem.Enabled = false;
            visitorGatePassToolStripMenuItem.Enabled = false;
            cityMasterToolStripMenuItem.Enabled = false;
            driverMasterToolStripMenuItem.Enabled = false;
            vehicleMasterToolStripMenuItem.Enabled = false;
            userProfilesToolStripMenuItem.Enabled = false;
            visitorMasterToolStripMenuItem.Enabled = false;
        }
        private void DisableTransaction()
        {
            //UserCompany CurrentCompany = new UserCompany();

            //if (CurrentCompany.m_UserName == "MAINGATE")
            //{
            //    vehicleInOutToolStripMenuItem.Enabled = true;
            //    appointmentMasterToolStripMenuItem.Enabled = false;
            //    visitorGatePassToolStripMenuItem.Enabled = true;
            //    returnableDCToolStripMenuItem.Enabled = true;
            //    vehicleInOutCompanyToolStripMenuItem.Enabled = true;
            //}
            //else if (CurrentCompany.m_UserName == "SYSADMIN")
            //{
            //    vehicleInOutToolStripMenuItem.Enabled = true;
            //    appointmentMasterToolStripMenuItem.Enabled = true;
            //    visitorGatePassToolStripMenuItem.Enabled = true;
            //    returnableDCToolStripMenuItem.Enabled = true;
            //    vehicleInOutCompanyToolStripMenuItem.Enabled = true;
            //}
            //else
            //{
            //    vehicleInOutToolStripMenuItem.Enabled = false;
            //    appointmentMasterToolStripMenuItem.Enabled = true;
            //    visitorGatePassToolStripMenuItem.Enabled = false;
            //    returnableDCToolStripMenuItem.Enabled = false;
            //    vehicleInOutCompanyToolStripMenuItem.Enabled = false;
            //}

            transactionsToolStripMenuItem.Enabled = false;
            appointmentMasterToolStripMenuItem.Enabled = false;
            visitorGatePassToolStripMenuItem.Enabled = false;
            returnableDCToolStripMenuItem.Enabled = false;
            vehicleInOutToolStripMenuItem.Enabled = false;
            vehicleInOutCompanyToolStripMenuItem.Enabled = false;
        }

        private void DisableUtilities()
        {
            utilitiesToolStripMenuItem.Enabled = false;
        }

        private void DisableButtons()
        {
            //UserCompany CurrentCompany = new UserCompany();

            //if (CurrentCompany.m_UserName == "MAINGATE")
            //{
            //    btnEmployeeMaster.Enabled = false;
            //    btnVisitorMaster.Enabled = true;
            //    btnVisitorGatePass.Enabled = true;
            //    btnRetDC.Enabled = true;
            //    btnVehicleInOut.Enabled = true;
            //    btnCompanyVehInOut.Enabled = true;
            //    btnAppointment.Enabled = false;
            //}
            //else if (CurrentCompany.m_UserName == "SYSADMIN")
            //{
            //    btnEmployeeMaster.Enabled = true;
            //    btnVisitorMaster.Enabled = true;
            //    btnVisitorGatePass.Enabled = true;
            //    btnRetDC.Enabled = true;
            //    btnVehicleInOut.Enabled = true;
            //    btnCompanyVehInOut.Enabled = true;
            //    btnAppointment.Enabled = true;
            //}
            //else
            //{
            //    btnEmployeeMaster.Enabled = false;
            //    btnVisitorMaster.Enabled = true;
            //    btnVisitorGatePass.Enabled = false;
            //    btnRetDC.Enabled = false;
            //    btnVehicleInOut.Enabled = false;
            //    btnCompanyVehInOut.Enabled = false;
            //    btnAppointment.Enabled = true;
            //}
            btnEmployeeMaster.Enabled = false;
            btnVisitorMaster.Enabled = false;
            btnVisitorGatePass.Enabled = false;
            btnRetDC.Enabled = false;
            btnVehicleInOut.Enabled = false;
            btnCompanyVehInOut.Enabled = false;
            btnAppointment.Enabled = false;
        }

        private void EnableMenus(string[] objList)
        {
            if (objList != null)
            {
                foreach (string str in objList)
                {
                    switch (str)
                    {
                        case "D0001":
                            defineToolStripMenuItem.Enabled = true;
                            break;
                        case "D0011":
                            deptMasterToolStripMenuItem.Enabled = true;
                            break;
                        case "D0012":
                            employeeMasterToolStripMenuItem.Enabled = true;
                            btnEmployeeMaster.Enabled = true;
                            break;
                        case "D0013":
                            visitorMasterToolStripMenuItem.Enabled = true;
                            btnVisitorMaster.Enabled = true;
                            break;
                        case "D0014":
                            cityMasterToolStripMenuItem.Enabled = true;
                            break;
                        case "D0015":
                            driverMasterToolStripMenuItem.Enabled = true;
                            break;
                        case "D0016":
                            vehicleMasterToolStripMenuItem.Enabled = true;
                            break;
                        case "T0001":
                            transactionsToolStripMenuItem.Enabled = true;
                            break;
                        case "T0011":
                            vehicleInOutToolStripMenuItem.Enabled = true;
                            vehicleInOutCompanyToolStripMenuItem.Enabled = true;
                            btnVehicleInOut.Enabled = true;
                            btnCompanyVehInOut.Enabled = true;
                            break;
                        case "T0012":
                            appointmentMasterToolStripMenuItem.Enabled = true;
                            btnAppointment.Enabled = true;
                            break;
                        case "T0013":
                            visitorGatePassToolStripMenuItem.Enabled = true;
                            btnVisitorGatePass.Enabled = true;
                            break;
                        case "T0014":
                            returnableDCToolStripMenuItem.Enabled = true;
                            btnRetDC.Enabled = true;
                            break;
                        case "U0001":
                            utilitiesToolStripMenuItem.Enabled = true;
                            break;
                        case "U0011":
                            userRightsToolStripMenuItem.Enabled = true;
                            break;
                    }
                }
            }
        }
        #endregion

        #region Constructor(s)
        public frmMain()
        {
            InitializeComponent();

         //   DateTime now = DateTime.Now;
         //   string date = now.GetDateTimeFormats('d')[0];
         //   string time = now.GetDateTimeFormats('t')[0];

            //   UserCompany CurrentCompany = new UserCompany();
            //   frmLogin frm = new frmLogin();

            //   string fd = CurrentCompany.m_FromDate.ToShortDateString();
            //   string td = CurrentCompany.m_ToDate.ToShortDateString();

            //   toolStripStatusLabel1.Text = "VMS - Financial Year " + fd + " - " + td;
            ////   toolStripStatusLabel2.Text = "USER : " + frm.txtUserName.Text + "" ;
            //   toolStripStatusLabel2.Text = "USER : " + CurrentCompany.m_UserName;
            //   toolStripStatusLabel5.Text = CurrentCompany.m_DataSource; 
            //   toolStripStatusLabel3.Text = "DATE : " + date;
            //   toolStripStatusLabel4.Text = "TIME : " + time;

            //   DisableMenus();
        }

        public frmMain(Int32 userID, Int32 companyID)
        {
            //Int32 appUserID, currentCompanyID;
            //appUserID = userID;
            //currentCompanyID = companyID;
            CurrentCompany = frmMainManager.LoadCompany(companyID);
            CurrentUser = UserManager.GetItem(userID);
            curUserRights = UserRightsManager.GetUserRights(userID);
            InitializeComponent();
        }
        #endregion

        #region UI Control Logic
        private void frmMain_Load(object sender, EventArgs e)
        {
            //long mUserID;
            //string[] RightsList;
            this.Icon = new Icon("Images/DTPL.ico");

            DateTime now = DateTime.Now;
            string date = now.GetDateTimeFormats('d')[0];
            string time = now.GetDateTimeFormats('t')[0];

            //UserCompany CurrentCompany = new UserCompany();
            //frmLogin frm = new frmLogin();

            string fd = CurrentCompany.m_FromDate.ToShortDateString();
            string td = CurrentCompany.m_ToDate.ToShortDateString();

            toolStripStatusLabel1.Text = "VMS - Financial Year " + fd + " - " + td;
            //   toolStripStatusLabel2.Text = "USER : " + frm.txtUserName.Text + "" ;
            //toolStripStatusLabel2.Text = "USER : " + CurrentCompany.m_UserName;
            //mUserID = CurrentCompany.m_Dbid;
            //toolStripStatusLabel5.Text = CurrentCompany.m_DataSource;
            toolStripStatusLabel3.Text = "DATE : " + date;
            toolStripStatusLabel4.Text = "TIME : " + time;
            toolStripStatusLabel2.Visible = false;
            toolStripStatusLabel5.Visible = false;
            DisableMenus();

            //RightsList = frmMainManager.GetUserRights(mUserID);
            EnableMenus(curUserRights);
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            frmLogin mfrm = new frmLogin();
            mfrm.ShowDialog();
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            //frmLogin frm = new frmLogin();
            //frm.ShowDialog();
            
            Application.Exit();
        }

        private void departmentMasterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //frmDeptList objFrmList = new frmDeptList();
                frmDeptList objFrmList = new frmDeptList(CurrentCompany, CurrentUser);
                objFrmList.MdiParent = this;
                objFrmList.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void employeeMasterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //frmEmployeeList objFrmList = new frmEmployeeList();
                frmEmployeeList objFrmList = new frmEmployeeList(CurrentCompany, CurrentUser);
                objFrmList.MdiParent = this;
                objFrmList.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void appointmentMasterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //frmAppointmentList objFrmList = new frmAppointmentList();
                frmAppointmentList objFrmList = new frmAppointmentList(CurrentCompany, CurrentUser);
                objFrmList.MdiParent = this;
                objFrmList.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void visitorMasterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //frmVisitorList objFrmList = new frmVisitorList();
                frmVisitorList objFrmList = new frmVisitorList(CurrentCompany, CurrentUser);
                objFrmList.MdiParent = this;
                objFrmList.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void cityMasterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //frmCityList objFrmList = new frmCityList();
                frmCityList objFrmList = new frmCityList(CurrentCompany, CurrentUser);
                objFrmList.MdiParent = this;
                objFrmList.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void driverMasterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //frmDriverList objFrmList = new frmDriverList();
                frmDriverList objFrmList = new frmDriverList(CurrentCompany, CurrentUser);
                objFrmList.MdiParent = this;
                objFrmList.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void vehicleMasterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //frmVehicleList objFrmList = new frmVehicleList();
                frmVehicleList objFrmList = new frmVehicleList(CurrentCompany, CurrentUser);
                objFrmList.MdiParent = this;
                objFrmList.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void userProfilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //frmUserList objfrmlist = new frmUserList();
                frmUserList objfrmlist = new frmUserList(CurrentCompany, CurrentUser);
                objfrmlist.MdiParent = this;
                objfrmlist.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void visitorGatePassToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //frmVisitorGPList objfrmlist = new frmVisitorGPList();
                frmVisitorGPList objfrmlist = new frmVisitorGPList(CurrentCompany, CurrentUser);
                objfrmlist.MdiParent = this;
                objfrmlist.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void returnableDCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //frmReturnableDCList objFrmList = new frmReturnableDCList();
                frmReturnableDCList objFrmList = new frmReturnableDCList(CurrentCompany, CurrentUser);
                objFrmList.MdiParent = this;
                objFrmList.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnEmployeeMaster_Click(object sender, EventArgs e)
        {
            employeeMasterToolStripMenuItem_Click(sender,e);
        }

        private void btnVisitorMaster_Click(object sender, EventArgs e)
        {
            visitorMasterToolStripMenuItem_Click(sender,e);
        }

        private void btnVisitorGatePass_Click(object sender, EventArgs e)
        {
            visitorGatePassToolStripMenuItem_Click(sender,e);
        }

        private void btnRetDC_Click(object sender, EventArgs e)
        {
            returnableDCToolStripMenuItem_Click(sender, e);
        }

        private void btnVehicleInOut_Click(object sender, EventArgs e)
        {
            vehicleInOutToolStripMenuItem_Click(sender, e);
        }

        private void vehicleInOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmVehInOutList objFrmList = new frmVehInOutList();
                objFrmList.EntryType = "IN/OUT OTHER";
                objFrmList.MdiParent = this;
                objFrmList.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
}
        private void userRightsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmUserAuthority objfrmlist = new frmUserAuthority();
                objfrmlist.MdiParent = this;
                objfrmlist.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void vehicleInOutCompanyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmVehInOutList objFrmList = new frmVehInOutList();
                objFrmList.EntryType = "COMPANY";
                objFrmList.MdiParent = this;
                objFrmList.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnAppointment_Click(object sender, EventArgs e)
        {
            appointmentMasterToolStripMenuItem_Click(sender, e);
        }

        private void btnCompanyVehInOut_Click(object sender, EventArgs e)
        {
            vehicleInOutCompanyToolStripMenuItem_Click(sender, e);
        }
        #endregion
    }
}
