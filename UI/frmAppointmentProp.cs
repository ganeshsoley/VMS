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
using System.IO;
using System.Net;
using System.Web;

namespace UI
{
    public partial class frmAppointmentProp : Form
    {

        #region Private Variable
        private bool flgLoading;
        private bool flgNew;

        private Appointment objAppoint;
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
            if (objAppoint.IsNew)
            {
                strContactNo = objAppoint.ContactNo;
                myMsg = objAppoint.Name + " your meeting with " + objAppoint.EmpName + " is on " + objAppoint.AppointmentDate.ToShortDateString() + "," + objAppoint.ScheduleTime.ToShortTimeString() + " APMT NO: " + objAppoint.AppointmentNo;

                strAPI1 = GeneralMethods.GetSMSAPI();

                strSource = GeneralMethods.GetSMSSenderID();

                strDestination = "&destination=91" + strContactNo;

                strMessage = "&message=" + myMsg;

                strFullAPI = strAPI1 + strDestination + strSource + strMessage;

                string sResponse = GetResponse(strFullAPI);
                
                return true;
            }
            else
            {
                return false ;
            }
        }

        public static string GetResponse(string sURL)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(sURL);
            request.MaximumAutomaticRedirections = 4;
            request.Credentials = CredentialCache.DefaultCredentials;
            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse ();
                Stream receiveStream = response.GetResponseStream ();
                StreamReader readStream = new StreamReader (receiveStream, Encoding.UTF8);
                string sResponse = readStream.ReadToEnd();
                response.Close ();
                readStream.Close ();
                return sResponse;
            }
            catch
            { 
                throw;
            }
        }
        #endregion

        #region Constructor
        public frmAppointmentProp()
        {
            InitializeComponent();
        }

        public frmAppointmentProp(Appointment objAppoint)
        {
            this.objAppoint = objAppoint;
            InitializeComponent();
        }
        #endregion

        /// <summary>
        /// Contains Public Properties of Form that are accessible through out the Project.
        /// </summary>
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

        /// <summary>
        /// Contains Delegates and events available on Form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="Action"></param>
        #region Delegate & Event
        public delegate void AppointUpdateHandler(object sender, AppointUpdateEventArgs e, DataEventType Action);

        public event AppointUpdateHandler Entry_DataChanged;
        #endregion

        #region Private Method(s)
        private void EnableDisableSave()
        {
            btnSave.Enabled = objAppoint.IsValid;
        }

        private void Appointment_OnValid(object sender, EventArgs e)
        {
            EnableDisableSave();
        }

        private void Appointment_OnInValid(object sender, EventArgs e)
        {
            EnableDisableSave();
        }
        
        private void SubscribeToEvents()
        {
            objAppoint.OnValid += new Appointment.EventHandler(Appointment_OnValid);
            objAppoint.OnInvalid += new Appointment.EventHandler(Appointment_OnInValid);
        }
        #endregion

        #region UI Control Logic
        private void frmAppointmentProp_Load(object sender, EventArgs e)
        {
            this.Icon = new Icon("Images/DTPL.ico");
            flgLoading = true;
            Appointment_OnInValid(sender, e);

            if (objAppoint.IsNew)
            {
                this.Text += " [ NEW ]";
            }
            else
            {
                this.Text += " [ " + objAppoint.Name + " ]";

                lblAppointmentNo.Text = Convert.ToString(objAppoint.AppointmentNo);
                dtpAppointDate.Value = Convert.ToDateTime(objAppoint.AppointmentDate);
                lblName.Text = objAppoint.Name;
                lblCompany.Text = objAppoint.Company;
                lblContactNo.Text = objAppoint.ContactNo;
                dtpSchTime.Value = objAppoint.ScheduleTime;
                lblToMeet.Text = objAppoint.EmpName;
                lblDepartment.Text = objAppoint.EmpDept;
                
                dtpAppointDate.Enabled = false;
                btnVisitorList.Enabled = false;
                btnEmployeeList.Enabled = false;
            }

            SubscribeToEvents();
            flgLoading = false;
            if (objAppoint.IsNew)
            {
                dtpAppointDate.Value = DateTime.Now.Date;
                dtpSchTime.Value = DateTime.Now;
            }
        }

        private void dtpAppointDate_ValueChanged(object sender, EventArgs e)
        {
            objAppoint.AppointmentDate = Convert.ToDateTime(dtpAppointDate.Value);
        }

        private void btnVisitorList_Click(object sender, EventArgs e)
        {
            frmVisitorList frmList = new frmVisitorList();
            frmList.IsList = true;
            frmList.ShowDialog();

            if (!frmList.IsListCancel & frmList.DBID >0)
            {
                objAppoint.VisitorID = frmList.DBID;
                objAppoint.Name = frmList.VisitorName;
                objAppoint.Company = frmList.Company;
                objAppoint.ContactNo = frmList.ContactNo;

                lblName.Text = objAppoint.Name;
                lblCompany.Text = objAppoint.Company;
                lblContactNo.Text = objAppoint.ContactNo;

                SendKeys.Send("TAB");
            }
            frmList.Dispose();
        }

        private void dtpSchTime_ValueChanged(object sender, EventArgs e)
        {
            objAppoint.ScheduleTime = dtpSchTime.Value;
        }

        private void btnEmployeeList_Click(object sender, EventArgs e)
        {
            frmEmployeeList frmList = new frmEmployeeList();
            frmList.IsList = true;
            frmList.ShowDialog();

            if (!frmList.IsListCancel)
            {
                objAppoint.EmployeeID = frmList.DBID;
                objAppoint.EmpName = frmList.Initials;
                objAppoint.EmpDept = frmList.DeptName;

                lblToMeet.Text = objAppoint.EmpName;
                lblDepartment.Text = objAppoint.EmpDept;

                SendKeys.Send("TAB");
            }
            frmList.Dispose();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                bool flgApplyEdit;
                
                flgApplyEdit = AppointmentManager.Save(objAppoint);

                if (flgApplyEdit)
                {
                    if (objAppoint.IsNew)
                    {
                        GetSMSRespons();
                    }
                    // instance the event args and pass it value
                    AppointUpdateEventArgs args = new AppointUpdateEventArgs(objAppoint.DBID, objAppoint.EntryNo, objAppoint.EntryDate, objAppoint.AppointmentNo, objAppoint.Name, objAppoint.AppointmentDate, objAppoint.ScheduleTime);

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
        #endregion
    }

    public class AppointUpdateEventArgs : System.EventArgs
    {
        private long mDBID;
        private long mEntryno;
        private DateTime mEntryDate;
        private string mAppointmentNo;
        private string mName;
        private DateTime mAppointmentDate;
        private DateTime mSchTime;

        public AppointUpdateEventArgs(long sDBID, long sEntryNo, DateTime sEntryDate, string sAppointmentNo, string sName, DateTime sAppointmentDate, DateTime sSchTime)
        {
            this.mDBID = sDBID;
            this.mEntryno = sEntryNo;
            this.mEntryDate = sEntryDate;
            this.mAppointmentNo = sAppointmentNo;
            this.mName = sName;
            this.mAppointmentDate = sAppointmentDate;
            this.mSchTime = sSchTime;
        }

        public long DBID
        {
            get
            {
                return mDBID;
            }
        }

        public long EntryNo
        {
            get
            {
                return mEntryno;
            }
        }

        public DateTime EntryDate
        {
            get
            {
                return mEntryDate;
            }
        }

        public string AppointmentNo
        {
            get
            {
                return mAppointmentNo;
            }
        }

        public string Name
        {
            get
            {
                return mName;
            }
        }

        public DateTime AppointmentDate
        {
            get
            {
                return mAppointmentDate;
            }
        }

        public DateTime SchTime
        {
            get
            {
                return mSchTime;
            }
        }
    }

}
