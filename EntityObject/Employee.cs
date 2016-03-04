using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntityObject
{
    public class Employee : BrokenRule
    {
        #region Private Variable(s)
        private bool flgNew;
        private bool flgEdited;
        private bool flgDeleted;
        private bool flgLoading;
        private bool flgUpload;

        private long dbID;
        private string empCode;
        private string firstName;
        private string middleName;    
        private string lastName;
        private string initials;
        private int deptID;
        private string deptName;
        private DateTime joinDate;
        private DateTime birthDate;
        private DateTime leftDate;
        private string mobile;
        private string eMail;
        private int inActive;
        private string empPlant;
        #endregion

        #region Constructor(s)
        public Employee()
        {
            flgNew = true;
            flgEdited = false;
            flgDeleted = false;

            dbID = 0;
            empCode = string.Empty;
            firstName = string.Empty;
            middleName = string.Empty;
            lastName = string.Empty;
            initials = string.Empty;
            deptID = 0;
            deptName = string.Empty;
            joinDate = DateTime.MinValue;
            birthDate = DateTime.MinValue;
            leftDate = DateTime.MinValue;
            mobile = string.Empty;
            eMail = string.Empty;
            inActive = 0;
            empPlant = string.Empty;

            RuleBroken("EmpCode", true);
            RuleBroken("FirstName", true);
            //RuleBroken("LastName", true);
            RuleBroken("DeptID", true);
            RuleBroken("Mobile", true);
            RuleBroken("EmpPlant", true);
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

        public bool IsUpload
        {
            get
            {
                return flgUpload;
            }
            set
            {
                flgUpload = value;
            }
        }

        public long DBID
        {
            get
            {
                return dbID;
            }
            set
            {
                dbID = value;
            }
        }

        public string EmpCode
        {
            get
            {
                return empCode;
            }
            set
            {
                if (!flgLoading)
                {
                    if (value.Trim().Length > 10)
                    {
                        throw new Exception("Length can not be greater than 10 character(s).");
                    }
                }
                RuleBroken("EmpCode", (value.Trim().Length == 0));
                empCode = value.Trim().ToUpper();
                flgEdited = true;
            }
        }

        public string FirstName
        {
            get
            {
                return firstName;
            }
            set
            {
                if (!flgLoading)
                {
                    if (value.Trim().Length > 50)
                    {
                        throw new Exception("Length can not be greater than 50 character(s).");
                    }
                }
                RuleBroken("FirstName", (value.Trim().Length == 0));
                firstName = value.Trim().ToUpper();
                if (MiddleName != "" )
                    Initials = value.Trim().ToUpper() + " " + MiddleName.Substring(0, 1) + " " + LastName;
                else
                    Initials = value.Trim().ToUpper() + " " + LastName;
                flgEdited = true;
            }
        }

        public string MiddleName
        {
            get
            {
                return middleName;
            }
            set
            {
                if (!flgLoading)
                {
                    if (value.Trim().Length > 50)
                    {
                        throw new Exception("Length can not be greater than 50 character(s).");
                    }
                }
                middleName = value.Trim().ToUpper();
                if (value.Trim().Length > 0)
                    Initials = FirstName + " " + value.Substring(0, 1).ToUpper() + " " + LastName;
                else
                    Initials = FirstName + " " + LastName;
                flgEdited = true;
            }
        }

        public string LastName
        {
            get
            {
                return lastName;
            }
            set
            {
                if (!flgLoading)
                {
                    if (value.Trim().Length > 50)
                    {
                        throw new Exception("Length can not be greater than 50 character(s).");
                    }
                }
                //RuleBroken("LastName", (value.Trim().Length == 0));
                lastName = value.Trim().ToUpper();
                if (MiddleName != "")
                    Initials = FirstName + " " + MiddleName.Substring(0, 1) + " " + value.Trim().ToUpper();
                else
                    Initials = FirstName + " " + value.Trim().ToUpper();
                flgEdited = true;
            }
        }

        public string Initials
        {
            get
            {
                return initials;
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
                initials = value.Trim().ToUpper();
                flgEdited = true;
            }
        }

        public int DeptID
        {
            get
            {
                return deptID;
            }
            set
            {
                RuleBroken("DeptID", (value == 0));
                deptID = value;
                flgEdited = true;
            }
        }

        public string DeptName
        {
            get
            {
                return deptName;
            }
            set
            {
                if (!flgLoading)
                {
                    if (value.Trim().Length > 30)
                    {
                        throw new Exception("Length can not be greater than 30 character(s).");
                    }
                }
                deptName = value.Trim().ToUpper();
            }
        }

        public DateTime JoinDate
        {
            get
            {
                return joinDate;
            }
            set
            {
                if (!flgLoading)
                {
                }
                joinDate = value;
                flgEdited = true;
            }
        }

        public DateTime BirthDate
        {
            get
            {
                return birthDate;
            }
            set
            {
                if (!flgLoading)
                {

                }
                birthDate = value;
                flgEdited = true;
            }
        }

        public DateTime LeftDate
        {
            get
            {
                return leftDate;
            }
            set
            {
                if (!flgLoading)
                {
                }
                leftDate = value;
                flgEdited = true;
            }
        }

        public string MobileNo
        {
            get
            {
                return mobile;
            }
            set
            {
                if (!flgLoading)
                {
                    if (value.Trim().Length > 10)
                    {
                        throw new Exception("Length can not be greater than 10 character(s).");
                    }
                }
                RuleBroken("Mobile", (value.Trim().Length == 0));
                mobile = value;
                flgEdited = true;
            }
        }

        public string EMailID
        {
            get
            {
                return eMail;
            }
            set
            {
                if (!flgLoading)
                {
                    if (value.Trim().Length > 50)
                    {
                        throw new Exception("Length can not be greater than 50 character(s).");
                    }
                }
                eMail = value.Trim().ToUpper();
                flgEdited = true;
            }
        }

        public int InActive
        {
            get
            {
                return inActive;
            }
            set
            {
                inActive = value;
                flgEdited = true;
            }
        }

        public string EmpPlant
        {
            get
            {
                return empPlant;
            }
            set
            {
                if (!flgLoading)
                {
                    if (value.Trim().Length > 15)
                    {
                        throw new Exception("Length can not be greater than 15 character(s).");
                    }
                }
                RuleBroken("EmpPlant", (value.Trim().Length == 0));
                empPlant = value.Trim().ToUpper();
                flgEdited = true;
            }
        }
        #endregion
    }
}
