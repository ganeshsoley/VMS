using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntityObject
{
    public class Appointment : BrokenRule
    {
        #region Private Variable(s)
        private bool flgNew;
        private bool flgEdited;
        private bool flgDeleted;
        private bool flgLoading;

        private long dbid;
        private long entryNo;
        private DateTime entryDate;
        private string appointmentNo;
        private DateTime appointmentDate;
        private DateTime scheduleTime;

        private long vID;
        private string name;
        private string company;
        private string vContactNo;
        private long employeeID;
        private string empName;
        private string empDept;
        private bool apmtClose;
        #endregion

        #region Constructor(s)
        public Appointment()
        {
            flgNew = true;
            flgEdited = false;
            flgDeleted = false;

            dbid = 0;
            entryNo = 0;
            entryDate = DateTime.MinValue;
            appointmentNo = string.Empty;
            appointmentDate = DateTime.MinValue;
            scheduleTime = DateTime.MinValue;
            vID = 0;
            name = string.Empty;
            company = string.Empty;
            vContactNo = string.Empty;
            employeeID = 0;
            empName = string.Empty;
            empDept = string.Empty;
            apmtClose = false;

            RuleBroken("AppointmentDate", true);
            RuleBroken("ScheduleTime", true);
            RuleBroken("VisitorName", true);
            RuleBroken("EmployeeID", true);
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
        public bool IsDeleted
        {
            get
            {
                return flgDeleted;
            }
            set
            {
                flgDeleted = value;
            }
        }
        public bool IsEdited
        {
            get
            {
                return flgEdited;
            }
            set
            {
                flgEdited = value;
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

        #region Public Object Properties
        public long DBID
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
        public long EntryNo
        {
            get
            {
                return entryNo;
            }
            set
            {
                entryNo = value;
            }
        }
        public DateTime EntryDate
        {
            get
            {
                return entryDate;
            }
            set
            {
                entryDate = value;
                flgEdited = true;
            }
        }
        public string AppointmentNo
        {
            get
            {
                return appointmentNo;
            }
            set
            {
                appointmentNo = value;
                flgEdited = true;
            }
        }
        public DateTime AppointmentDate
        {
            get
            {
                return appointmentDate;
            }
            set
            {
                if (!flgLoading)
                {

                }
                RuleBroken("AppointmentDate", (value == DateTime.MinValue));
                appointmentDate = value;
                flgEdited = true;
            }
        }
        public DateTime ScheduleTime
        {
            get
            {
                return scheduleTime;
            }
            set
            {
                RuleBroken("ScheduleTime", (value == DateTime.MinValue));
                scheduleTime = value;
                flgEdited = true;
            }
        }

        public long VisitorID
        {
            get
            {
                return vID;
            }
            set
            {
                RuleBroken("VisitorName", (value == 0));
                vID = value;
                flgEdited = true;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                if (!flgLoading)
                {
                    if (value.Trim().Length > 70)
                    {
                        throw new Exception("Length can not be greater than 70 character(s).");
                    }
                }
                name  = value.Trim().ToUpper();
                flgEdited = true;
            }
        }

        public string Company
        {
            get
            {
                return company; 
            }
            set
            {
                company = value.Trim().ToUpper();
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

        public long EmployeeID
        {
            get
            {
                return employeeID; 
            }
            set
            {
                RuleBroken("EmployeeID", (value == 0));
                employeeID = value;
                flgEdited = true;
            }
        }

        public string EmpName
        {
            get
            {
                return empName;
            }
            set
            {
                empName = value;
            }
        }

        public string EmpDept
        {
            get
            {
                return empDept;
            }
            set
            {
                empDept = value;
            }
        }

        public bool ApmtClose
        {
            get
            {
                return apmtClose;
            }
            set
            {
                apmtClose = value;
                flgEdited = true;
            }
        }
        #endregion
    }
}
