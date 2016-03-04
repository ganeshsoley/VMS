using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntityObject
{
    public class Visitor: BrokenRule
    {
        #region Private Variable(s)
        private bool flgNew;
        private bool flgEdited;
        private bool flgDeleted;
        private bool flgLoading;

        private long dbID;
        private long vRegNo;
        private string vName;
        private string company;
        private string address;
        private string contactNo;
        private string idProofType;
        private string idProofNo;
        #endregion

        #region Constructor(s)
        public Visitor()
        {
            flgNew = true;
            flgEdited = false;
            flgDeleted = false;

            dbID = 0;
            vRegNo = 0;
            vName = string.Empty;
            company = string.Empty;
            address = string.Empty;
            contactNo = string.Empty;
            idProofType = string.Empty;
            idProofNo = string.Empty;

            RuleBroken("Name", true);
            RuleBroken("Company", true);
            RuleBroken("ContactNo", true);
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

        public long RegNo
        {
            get
            {
                return vRegNo;
            }
            set
            {
                vRegNo = value;
                flgEdited = true;
            }
        }

        public string VName
        {
            get
            {
                return vName;
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
                RuleBroken("Name", (value.Trim().Length == 0));
                vName = value.Trim().ToUpper();
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
                if (!flgLoading)
                {
                    if (value.Trim().Length > 70)
                    {
                        throw new Exception("Length can not be greater than 70 character(s).");
                    }
                }
                RuleBroken("Company", (value.Trim().Length == 0));
                company = value.Trim().ToUpper();
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
                    if (value.Trim().Length > 70)
                    {
                        throw new Exception("Length can not be greater than 70 character(s).");
                    }
                }
                address = value.Trim().ToUpper();
                flgEdited = true;
            }
        }

        public string MobileNo
        {
            get
            {
                return contactNo;
            }
            set
            {
                if (!flgLoading)
                {
                    if (value.Trim().Length > 20)
                    {
                        throw new Exception("Length can not be greater than 70 character(s).");
                    }
                }
                RuleBroken("ContactNo", (value.Trim().Length == 0));
                contactNo = value.Trim().ToUpper();
                flgEdited = true;
            }
        }

        public string IDProofType
        {
            get
            {
                return idProofType;
            }
            set
            {
                if (!flgLoading)
                {
                    if (value.Trim().Length > 20)
                    {
                        throw new Exception("Length can not be greater than 70 character(s).");
                    }
                }
                idProofType = value.Trim().ToUpper();
                flgEdited = true;
            }
        }

        public string IDProofNo
        {
            get
            {
                return idProofNo;
            }
            set
            {
                if (!flgLoading)
                {
                    if (value.Trim().Length > 20)
                    {
                        throw new Exception("Length can not be greater than 70 character(s).");
                    }
                }
                idProofNo = value.Trim().ToUpper();
                flgEdited = true;
            }
        }
        #endregion
    }
}
