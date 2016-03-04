using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntityObject
{
    public class Department : BrokenRule
    {
        #region Private Variable(s)
        private bool flgNew;
        private bool flgEdited;
        private bool flgDeleted;
        private bool flgLoading;
        private bool flgUpload;

        private int dbid;
        private string deptName;
        private string description;
        private bool isActive;
        #endregion

        #region Constructor(s)
        public Department()
        {
            flgNew = true;
            flgEdited = false;
            flgDeleted = false;

            dbid = 0;
            deptName = string.Empty;
            description = string.Empty;
            isActive = true;

            RuleBroken("DeptName", true);
            RuleBroken("Description", true);
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
                RuleBroken("DeptName", (value.Trim().Length == 0));
                deptName = value.Trim().ToUpper();
                flgEdited = true;
            }
        }

        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                if (!flgLoading)
                {
                    if (value.Trim().Length > 30)
                    {
                        throw new Exception("Length Can not be greater than 30 Character(s).");
                    }
                }
                RuleBroken("Description", (value.Trim().Length == 0));
                description = value.Trim().ToUpper();
                flgEdited = true;
            }
        }

        public bool IsActive
        {
            get
            {
                return isActive;
            }
            set
            {
                if (!flgLoading)
                {
                }
                isActive = value;
                flgEdited = true;
            }
        }
        #endregion
    }
}
