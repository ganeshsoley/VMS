using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntityObject
{
    public class VisitorGatePass: BrokenRule
    {
        #region Private Variable(s)
        private bool flgNew;
        private bool flgEdited;
        private bool flgDeleted;
        private bool flgLoading;
        //private bool flgUpload;

        private int dbid;
        private string gatePassNo;
        private DateTime gateDate;
        private long visitorID;
        private string visitorName;
        private string address;
        private string contactNo;
        private string vehicleNo;
        private string depositMaterial;
        private string carryMaterial;
        private int noOfPerson;
        private string companyName;
        private long empID;
        private string toMeet;
        private string purpose;
        private DateTime timeIn;
        private string empDept;
        private string empMobile; 
        private DateTime timeOut;
        private string imgFileName;
        private string appointmentNo ;
		private int exPersons;
        #endregion

        #region Constructor(s)
        public VisitorGatePass()
        {
            flgNew = true;
            flgEdited = false;
            flgDeleted = false;

            dbid = 0;
            gatePassNo = string.Empty;
            gateDate = DateTime.MinValue;
            visitorID = 0;
            visitorName = string.Empty;
            address = string.Empty;
            contactNo = string.Empty;
            vehicleNo = string.Empty;
            depositMaterial = string.Empty;
            carryMaterial = string.Empty;
            noOfPerson = 1;
            companyName = string.Empty;
            empID = 0;
            toMeet = string.Empty;
            purpose = string.Empty;
            timeIn = DateTime.MinValue;
            timeOut = DateTime.MinValue;
            imgFileName = string.Empty;
            appointmentNo = string.Empty;
            empMobile = string.Empty;
            exPersons = 0;

            RuleBroken("VisitorName", true);
            RuleBroken("Purpose", true);
            RuleBroken("Employee", true);
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

        public string GatePassNo
        {
            get
            {
                return gatePassNo;
            }
            set
            {
                if (!flgLoading)
                {
                    if(value.Trim().Length > 12)
                        throw new Exception("Length can not be greater than 12 character(s).");
                }
                gatePassNo = value;
                flgEdited = true;
            }
        }

        public DateTime GateDate
        {
            get
            {
                return gateDate;
            }
            set
            {
                gateDate = value;
                flgEdited = true;
            }
        }

        public long VisitorID
        {
            get
            {
                return visitorID;
            }
            set
            {
                visitorID = value;
                flgEdited = true;
            }
        }

        public string VisitorName
        {
            get
            {
                return visitorName;
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
                RuleBroken("VisitorName", (value.Trim().Length == 0));
                visitorName = value.Trim().ToUpper();
                flgEdited = true;
            }
        }

        public string Address
        {
            get
            {
                return address;
            }
            set
            {
                if (!flgLoading)
                {
                    if (value.Trim().Length > 200)
                    {
                        throw new Exception("Length can not be greater than 200 character(s).");
                    }
                }
                address = value.Trim().ToUpper();
                flgEdited = true;
            }
        }

        public string ContactNo
        {
            get
            {
                return contactNo;
            }
            set
            {
                if (!flgLoading)
                {
                    if (value.Trim().Length > 22)
                    {
                        throw new Exception("Length can not be greater than 22 character(s).");
                    }
                }
                contactNo = value.Trim().ToUpper();
                flgEdited = true;
            }
        }

        public string VehicleNo
        {
            get
            {
                return vehicleNo;
            }
            set
            {
                if (!flgLoading)
                {
                    if (value.Trim().Length > 25)
                    {
                        throw new Exception("Length can not be greater than 25 character(s).");
                    }
                }
                vehicleNo = value.Trim().ToUpper();
                flgEdited = true;
            }
        }

        public string DepositMaterial
        {
            get
            {
                return depositMaterial;
            }
            set
            {
                if (!flgLoading)
                {
                    if (value.Trim().Length > 100)
                    {
                        throw new Exception("Length can not be greater than 100 character(s).");
                    }
                }
                depositMaterial = value.Trim().ToUpper();
                flgEdited = true;
            }
        }

        public string CarryMaterial
        {
            get
            {
                return carryMaterial;
            }
            set
            {
                if (!flgLoading)
                {
                    if (value.Trim().Length > 100)
                    {
                        throw new Exception("Length can not be greater than 100 character(s).");
                    }
                }
                carryMaterial = value.Trim().ToUpper();
                flgEdited = true;
            }
        }

        public int NoOfPersons
        {
            get
            {
                return noOfPerson;
            }
            set
            {
                noOfPerson = value;
                flgEdited = true;
            }
        }

        public string CompanyName
        {
            get
            {
                return companyName;
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
                companyName = value;
                flgEdited = true;
            }
        }

        public long EmployeeID
        {
            get
            {
                return empID;
            }
            set
            {
                RuleBroken("Employee", (value == 0));
                empID = value;
                flgEdited = true;
            }
        }

        public string ToMeet
        {
            get
            {
                return toMeet;
            }
            set
            {
                if (!flgLoading)
                {
                    if (value.Trim().Length > 100)
                    {
                        throw new Exception("Length can not be greater than 100 character(s).");
                    }
                }
                toMeet = value;
                flgEdited = true;
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
                if (!IsLoading)
                {
                }
                empDept = value.Trim().ToUpper();
                flgEdited = true;
            }
        }

        public string EmpMobile
        {
            get
            {
                return empMobile;
            }
            set
            {
                if (!flgLoading)
                {
                    if (value.Trim().Length > 20)
                    {
                        throw new Exception("Length can not be greater than 20 character(s).");
                    }
                }
                empMobile = value.Trim().ToUpper();
                flgEdited = true;
            }
        }

        public string Purpose
        {
            get
            {
                return purpose;
            }
            set
            {
                if (!flgLoading)
                {
                    if (value.Trim().Length > 20)
                    {
                        throw new Exception("Length can not be greater than 20 character(s).");
                    }
                }
                RuleBroken("Purpose", (value.Trim().Length == 0));
                purpose = value.Trim().ToUpper();
                flgEdited = true;
            }
        }

        public DateTime TimeIn
        {
            get
            {
                return timeIn;
            }
            set
            {
                //RuleBroken("",
                timeIn = value;
                flgEdited = true;
            }
        }

        public DateTime TimeOut
        {
            get
            {
                return timeOut;
            }
            set
            {
                timeOut = value;
                flgEdited = true;
            }
        }

        public string ImgFileName
        {
            get
            {
                return imgFileName;
            }
            set
            {
                if (!flgLoading)
                {
                    if (value.Trim().Length > 25)
                    {
                        throw new Exception("Length can not be greater than 25 character(s).");
                    }
                }
                imgFileName = value.Trim().ToUpper();
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

        public int ExtraPersons
        {
            get
            {
                return exPersons;
            }
            set
            {
                exPersons = value;
                flgEdited = true;
            }
        }

        public byte[] VisitorImage
        {
            get;
            set;
        }
        #endregion
    }
}
