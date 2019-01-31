using BLL;
using EntityObject;
using EntityObject.Enum;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;
using UI.WebCam;

namespace UI
{
    public partial class frmVisitorGPProp : Form
    {
        #region Private Variable(s)
        private bool flgNew;
        private bool flgLoading;
        //private bool flgCameraON;
        WebCamera objWebCam;

        private VisitorGatePass objVisitorGP;
        private User objUser;
        #endregion

        #region Constructor(s)
        public frmVisitorGPProp()
        {
            InitializeComponent();
        }

        public frmVisitorGPProp(VisitorGatePass objVisitorGP, User objCurUser)
        {
            this.objVisitorGP = objVisitorGP;
            objUser = objCurUser;
            InitializeComponent();
        }
        #endregion

        #region Public Properties
        public bool IsNew
        {
            get
            {
                return flgNew;
            }
            set
            {
                flgNew = value;
            }
        }

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
        #endregion

        #region Delegate & Event
        public delegate void VisitorGPUpdateHandler(object sender, VisitorGPUpdateEventArgs e, DataEventType Action);

        public event VisitorGPUpdateHandler Entry_DataChanged;
        #endregion

        #region Private Methods
        private void EnableDisableSave()
        {
            btnSave.Enabled = objVisitorGP.IsValid;
        }

        private void VisitorGP_OnValid(object sender, EventArgs e)
        {
            EnableDisableSave();
        }

        private void VisitorGP_OnInValid(object sender, EventArgs e)
        {
            EnableDisableSave();
        }

        private void SubscribeToEvents()
        {
            objVisitorGP.OnValid += new VisitorGatePass.EventHandler(VisitorGP_OnValid);
            objVisitorGP.OnInvalid += new VisitorGatePass.EventHandler(VisitorGP_OnInValid);
        }

        private void ChangeCameraMode(bool flgMode)
        {
            btnStartCam.Enabled = flgMode;
            btnCaptureImg.Enabled = !flgMode;
        }

        private void FillPurpose()
        {
            cboPurpose.Items.Add("OFFICIAL");
            cboPurpose.Items.Add("PERSONAL");
        }
        #endregion

        #region SMS Code 
        private string strAPI1;
        private string strSource;
        private string strDestination;
        private string strMessage;
        private string strFullAPI;
        private string strContactNo;
        private string myMsg;

        public bool GetSMSRespons()
        {
            strContactNo = objVisitorGP.EmpMobile;
            myMsg = objVisitorGP.VisitorName + " came to visit you against APMT No: " + objVisitorGP.AppointmentNo;

            strAPI1 = GeneralMethods.GetSMSAPI();   // "http://103.16.101.52:8080/bulksms/bulksms?username=syy-dtpl&password=1234&type=0&dlr=1";

            strSource = GeneralMethods.GetSMSSenderID();        // "&source=DTPLFa";
            strDestination = "&destination=91" + strContactNo;
            strMessage = "&message=" + myMsg;

            strFullAPI = strAPI1 + strDestination + strSource + strMessage;

            string sResponse = GetResponse(strFullAPI);
            return true;
        }

        public static string GetResponse(string sURL)
        {
            
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(sURL);
            request.MaximumAutomaticRedirections = 4;
            request.Credentials = CredentialCache.DefaultCredentials;
            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream receiveStream = response.GetResponseStream();
                StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
                string sResponse = readStream.ReadToEnd();
                response.Close();
                readStream.Close();
                return sResponse;
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region UI Control Logic
        private void frmVisitorGPProp_Load(object sender, EventArgs e)
        {
            this.Icon = new Icon("Images/DTPL.ico");
            flgLoading = true;
            objWebCam = new WebCamera();
            objWebCam.InitializeWebCam(ref picVideo);

            VisitorGP_OnInValid(sender, e);

            FillPurpose();
            if (objVisitorGP.IsNew)
            {
                this.Text += " [ NEW ]";
                ChangeCameraMode(true);
            }
            else
            {
                grbMain.Enabled = false;
                this.Text += " [ " + objVisitorGP.VisitorName + " ]";
                btnAppointmentList.Enabled = false;
                btnVisitorList.Enabled = false;
                btnEmpList.Enabled = false;
                btnStartCam.Enabled = false;
                btnCaptureImg.Enabled = false;
            }
            txtGateDate.Text = objVisitorGP.GateDate.ToShortDateString();
            dtpInTime.Text = objVisitorGP.TimeIn.ToShortTimeString();
            lblAppointmentNo.Text = Convert.ToString(objVisitorGP.AppointmentNo);
            lblVisitorName.Text = objVisitorGP.VisitorName;
            lblNoOfPersons.Text = Convert.ToString(objVisitorGP.NoOfPersons);
            if (objVisitorGP.ExtraPersons > 0)
                txtExPersons.Text = Convert.ToString(objVisitorGP.ExtraPersons);
            cboPurpose.Text = objVisitorGP.Purpose;
            lblCompany.Text = objVisitorGP.CompanyName;
            lblAddress.Text = objVisitorGP.Address;
            lblContactNo.Text = objVisitorGP.ContactNo;
            txtVehicleNo.Text = objVisitorGP.VehicleNo;

            lblToMeet.Text = objVisitorGP.ToMeet;
            lblEmpDept.Text = objVisitorGP.EmpDept;
            lblEmpContact.Text = objVisitorGP.EmpMobile;
            txtDepositedMaterial.Text = objVisitorGP.DepositMaterial;
            txtCarryingMaterial.Text = objVisitorGP.CarryMaterial;

            if (objVisitorGP.IsNew == false & objVisitorGP.VisitorImage != null)
            {
                MemoryStream mem = new MemoryStream(objVisitorGP.VisitorImage);
                picVisitorImg.Image = Image.FromStream(mem);
                picVideo.Visible = false;
                picVisitorImg.Visible = true;
            }
            else
            {
                picVideo.Visible = false;
                picVisitorImg.Visible = true;
            }
            
            SubscribeToEvents();
            flgLoading = false;
            if (objVisitorGP.IsNew)
            {
                txtGateDate.Text = DateTime.Now.ToShortDateString();
                dtpInTime.Value = DateTime.Now;
                picVisitorImg.Image = UI.Properties.Resources.DefaultImage;

                MemoryStream ms = new MemoryStream();
                picVisitorImg.Image.Save(ms, ImageFormat.Jpeg);
                byte[] photo_aray = new byte[ms.Length];
                ms.Position = 0;
                ms.Read(photo_aray, 0, photo_aray.Length);
                objVisitorGP.VisitorImage = photo_aray;
                ms.Dispose();
            }
        }

        private void frmVisitorGPProp_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void txtGateDate_Enter(object sender, EventArgs e)
        {
            GeneralMethods.Selection(txtGateDate);
        }

        private void txtGateDate_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!flgLoading)
                {
                    objVisitorGP.GateDate = Convert.ToDateTime(txtGateDate.Text.Trim());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void txtGateDate_Leave(object sender, EventArgs e)
        {
            txtGateDate.Text = objVisitorGP.GateDate.ToShortDateString();
        }

        private void dtpInTime_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (!flgLoading)
                {
                    objVisitorGP.TimeIn = dtpInTime.Value;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void dtpInTime_Validating(object sender, CancelEventArgs e)
        {
            if (dtpInTime.Value < dtpInTime.MinDate || dtpInTime.Value > dtpInTime.MaxDate)
            {
                MessageBox.Show("Value Entered is Invalid.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void dtpInTime_Leave(object sender, EventArgs e)
        {
            objVisitorGP.TimeIn = dtpInTime.Value;
        }

        private void btnAppointmentList_Click(object sender, EventArgs e)
        {
            frmAppointmentList frmList = new frmAppointmentList();
            frmList.Condition = "APPOINTMENTDATE = '" + objVisitorGP.GateDate.ToString("yyyy-MM-dd") + "'";
            frmList.IsList = true;
            frmList.ShowDialog();

            if (!frmList.IsListCancel)
            {
                Appointment objApp = new Appointment();
                objApp = AppointmentManager.GetItem(frmList.DBID);

                // Fetch Appointment Detail
                objVisitorGP.AppointmentNo = objApp.AppointmentNo;
                objVisitorGP.VisitorID = objApp.VisitorID;
                objVisitorGP.VisitorName = objApp.Name;
                objVisitorGP.CompanyName = objApp.Company;
                objVisitorGP.ContactNo = objApp.ContactNo;

                //Fetch VIsitor Detail
                Employee objEmp = new Employee();
                objEmp = EmployeeManager.GetItem(objApp.EmployeeID);
                objVisitorGP.EmployeeID = objEmp.DBID;
                objVisitorGP.ToMeet = objEmp.Initials;
                objVisitorGP.EmpDept = objEmp.DeptName;
                objVisitorGP.EmpMobile = objEmp.MobileNo;

                // Display Detail on Form
                lblAppointmentNo.Text = objVisitorGP.AppointmentNo;
                lblVisitorName.Text = objApp.Name;
                lblCompany.Text = objApp.Company;
                lblContactNo.Text = objApp.ContactNo;

                lblToMeet.Text = objVisitorGP.ToMeet;
                lblEmpDept.Text = objVisitorGP.EmpDept;
                lblEmpContact.Text = objVisitorGP.EmpMobile;

                // Fetch Here Last Image of visitor
                byte[] vImage = VisitorGatePassManager.GetVisitorImage(objApp.VisitorID);
                if (vImage != null)
                {
                    MemoryStream mem = new MemoryStream(vImage);

                    picVisitorImg.Image = Image.FromStream(mem);
                    objVisitorGP.VisitorImage = vImage;
                    picVideo.Visible = false;
                    picVisitorImg.Visible = true;
                }
                btnVisitorList.Enabled = false;
                btnEmpList.Enabled = false;

                SendKeys.Send("{TAB}");
            }
            frmList.Dispose();
        }

        private void btnVisitorList_Click(object sender, EventArgs e)
        {
            frmVisitorList frmList = new frmVisitorList();
            frmList.IsList = true;
            frmList.ShowDialog();

            if (!frmList.IsListCancel & frmList.DBID >0)
            {
                Visitor objVisitor = new Visitor();
                objVisitor = VisitorManager.GetItem(frmList.DBID);
                objVisitorGP.VisitorID = objVisitor.DBID;
                objVisitorGP.VisitorName = objVisitor.VName;
                objVisitorGP.CompanyName = objVisitor.Company;
                objVisitorGP.ContactNo = objVisitor.MobileNo;

                lblVisitorName.Text = objVisitorGP.VisitorName;
                lblCompany.Text = objVisitorGP.CompanyName;
                lblContactNo.Text = objVisitorGP.ContactNo;

                // Fetch Here Last Image of same visitor
                byte[] vImage = VisitorGatePassManager.GetVisitorImage(objVisitor.DBID);
                if (vImage != null)
                {
                    MemoryStream mem = new MemoryStream(vImage);
                    picVisitorImg.Image = Image.FromStream(mem);
                    objVisitorGP.VisitorImage = vImage;
                    picVideo.Visible = false;
                    picVisitorImg.Visible = true;

                }
                SendKeys.Send("{TAB}");
                //objVisitor.dispose();
            }
            frmList.Dispose();
        }

        private void txtExPersons_Enter(object sender, EventArgs e)
        {
            GeneralMethods.Selection(txtExPersons);
        }

        private void txtExPersons_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!flgLoading)
                {
                    objVisitorGP.ExtraPersons = Convert.ToInt32(txtExPersons.Text.Trim());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void txtExPersons_Leave(object sender, EventArgs e)
        {
            txtExPersons.Text = Convert.ToString(objVisitorGP.ExtraPersons);
        }

        private void cboPurpose_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!flgLoading & cboPurpose.SelectedIndex != -1)
            {
                objVisitorGP.Purpose = cboPurpose.Text.Trim();
            }
        }

        private void cboPurpose_Leave(object sender, EventArgs e)
        {
            cboPurpose.Text = objVisitorGP.Purpose;
        }

        private void txtVehicleNo_Enter(object sender, EventArgs e)
        {
            GeneralMethods.Selection(txtVehicleNo);
        }

        private void txtVehicleNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!flgLoading)
                {
                    objVisitorGP.VehicleNo = txtVehicleNo.Text.Trim();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void txtVehicleNo_Leave(object sender, EventArgs e)
        {
            txtVehicleNo.Text = objVisitorGP.VehicleNo;
        }

        private void btnEmpList_Click(object sender, EventArgs e)
        {
            frmEmployeeList frmList = new frmEmployeeList();
            frmList.IsList = true;
            frmList.ShowDialog();

            if (!frmList.IsListCancel)
            {
                Employee objEmp = new Employee();
                objEmp = EmployeeManager.GetItem(frmList.DBID);

                objVisitorGP.EmployeeID = objEmp.DBID;
                objVisitorGP.ToMeet = objEmp.Initials;
                objVisitorGP.EmpDept = objEmp.DeptName;
                objVisitorGP.EmpMobile = objEmp.MobileNo;

                lblToMeet.Text = objVisitorGP.ToMeet;
                lblEmpDept.Text = objVisitorGP.EmpDept;
                lblEmpContact.Text = objVisitorGP.EmpMobile;

                SendKeys.Send("{TAB}");
            }
            frmList.Dispose();
        }

        private void txtDepositedMaterial_Enter(object sender, EventArgs e)
        {
            GeneralMethods.Selection(txtDepositedMaterial);
        }

        private void txtDepositedMaterial_TextChanged(object sender, EventArgs e)
        {
            try
            {
                objVisitorGP.DepositMaterial = txtDepositedMaterial.Text.Trim();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void txtDepositedMaterial_Leave(object sender, EventArgs e)
        {
            txtDepositedMaterial.Text = objVisitorGP.DepositMaterial;
        }

        private void txtCarryingMaterial_Enter(object sender, EventArgs e)
        {
            GeneralMethods.Selection(txtCarryingMaterial);
        }

        private void txtCarryingMaterial_TextChanged(object sender, EventArgs e)
        {
            try
            {
                objVisitorGP.CarryMaterial = txtCarryingMaterial.Text.Trim();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void txtCarryingMaterial_Leave(object sender, EventArgs e)
        {
            txtCarryingMaterial.Text = objVisitorGP.CarryMaterial;
        }

        private void btnStartCam_Click(object sender, EventArgs e)
        {
            picVisitorImg.Visible = false;
            picVideo.Visible = true;
            ChangeCameraMode(false);
            // Camera Code Starts Here...
            objWebCam.Start();
            SendKeys.Send("{TAB}");

            // Following Code is Temporary When Go Live Comment Following Code
            // and Remove comment from above Code..
            ////OpenFileDialog objOpF = new OpenFileDialog();
            ////objOpF.Filter = "Image (.jpg) | *.jpg";
            ////objOpF.RestoreDirectory = true;
            ////objOpF.InitialDirectory = @"E:\";
            ////objOpF.ShowDialog();

            ////if (objOpF.FileName != string.Empty)
            ////{
            ////    picVisitorImg.Image = Image.FromFile(objOpF.FileName);
            ////    objVisitorGP.ImgFileName = "Image.jpg";
            ////    //Helper.SaveImageCapture(picVisitorImg.Image, objVisitorGP.ImgFileName);
            ////}
        }

        private void btnCaptureImg_Click(object sender, EventArgs e)
        {
            //string strPath;
            //strPath = Path.Combine(Application.StartupPath, @"VisitorImages");

            //SaveFileDialog s = new SaveFileDialog();
            ////s.FileName = "Image";// Default file name
            ////s.DefaultExt = ".Jpg";// Default file extension
            ////s.Filter = "Image (.jpg)|*.jpg"; // Filter files by extension
            //s.RestoreDirectory = true;
            //s.InitialDirectory = strPath;
            //if (!Directory.Exists(strPath))
            //{
            //    Directory.CreateDirectory(strPath);
            //}
            //if (File.Exists(strPath + "\\" + objVisitorGP.ImgFileName))
            //{
            //    File.Delete(strPath+ "\\" + objVisitorGP.ImgFileName);
            //}

            picVisitorImg.Image = picVideo.Image;

            //Helper.SaveImageCapture(picVisitorImg.Image, strPath + "\\" + objVisitorGP.ImgFileName);
            
            //use memorystream
            MemoryStream ms = new MemoryStream();
            picVisitorImg.Image.Save(ms, ImageFormat.Jpeg);
            byte[] photo_aray = new byte[ms.Length];
            ms.Position = 0;
            ms.Read(photo_aray, 0, photo_aray.Length);
            objVisitorGP.VisitorImage = photo_aray;

            //objVisitorGP.ImgFileName = "Image.jpg";
            objWebCam.Stop();
            // Camera Code Ends Here...
            ChangeCameraMode(true);
            SendKeys.Send("{TAB}");
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                bool flgApplyEdit;
                flgApplyEdit = VisitorGatePassManager.Save(objVisitorGP, objUser);
                if (flgApplyEdit)
                {
                    if (objVisitorGP.IsNew)
                    {
                        if (objVisitorGP.AppointmentNo != "")
                        {
                            GetSMSRespons();
                        }
                    }
                    // instance the event args and pass it value
                    VisitorGPUpdateEventArgs args = new VisitorGPUpdateEventArgs(objVisitorGP.DBID, objVisitorGP.GatePassNo, objVisitorGP.GateDate, objVisitorGP.VisitorName, objVisitorGP.ToMeet, objVisitorGP.Purpose, objVisitorGP.TimeIn, objVisitorGP.TimeOut);

                    // raise event wtth  updated 
                    if (Entry_DataChanged != null)
                    {
                        if (this.IsNew)
                        {
                            Entry_DataChanged(this, args, DataEventType.INSERT_EVENT);
                        }
                        else
                        {
                            Entry_DataChanged(this, args, DataEventType.UPDATE_EVENT);
                        }
                    }
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Record Not Saved.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                //this.Dispose();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void cboPurpose_KeyPress(object sender, KeyPressEventArgs e)
        {
            GeneralMethods.AutoComplete(this.cboPurpose, e, true);
        }

        #endregion

    }


    public class VisitorGPUpdateEventArgs : System.EventArgs
    {
        private int mDBID;
        private string mGPNo;
        private DateTime mGPDate;
        private string mVisitorName;
        private string mToMeet;
        private string mPurpose;
        private DateTime mTimeIn;
        private DateTime mTimeOUt;

        public VisitorGPUpdateEventArgs(int sDBID, string sGPNo, DateTime sGPDate, string sVisitorName, string sToMeet, string sPurpose, DateTime sTimeIn, DateTime sTimeOut)
        {
            this.mDBID = sDBID;
            this.mGPNo = sGPNo;
            this.mGPDate = sGPDate;
            this.mVisitorName = sVisitorName;
            this.mToMeet = sToMeet;
            this.mPurpose = sPurpose;
            this.mTimeIn = sTimeIn;
            this.mTimeOUt = sTimeOut;
        }

        public int DBID
        {
            get
            {
                return mDBID;
            }
        }

        public string GPNo
        {
            get
            {
                return mGPNo;
            }
        }

        public DateTime GPDate
        {
            get
            {
                return mGPDate;
            }
        }

        public string VisitorName
        {
            get
            {
                return mVisitorName;
            }
        }

        public string ToMeet
        {
            get
            {
                return mToMeet;
            }
        }

        public string Purpose
        {
            get
            {
                return mPurpose;
            }
        }

        public DateTime TimeIn
        {
            get
            {
                return mTimeIn;
            }
        }

        public DateTime TimeOut
        {
            get
            {
                return mTimeOUt;
            }
        }
    }
}
