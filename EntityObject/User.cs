using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntityObject
{
    public class User : BrokenRule
    {
        #region Private Variable
        private bool flgNew;

        private int dbId;
        private string firstName;
        private string loginName;
        private string password;
        private string confirmPwd;
        private string role;
        private int branchCode;
        private int companyCode;
        private string dept;
        private int isActive;
        private int deptHead;
        private int authLogin;      
        private int hrLogin;
        private int gateLogin;
        private int manager;
        private int login;
        private string loginTime;
        #endregion

        #region Constructor(s)
        public User()
        {
            flgNew = true;

            dbId = 0;
            firstName = string.Empty;    
            loginName = string.Empty;         
            password = string.Empty;    
            role = string.Empty;
            branchCode = 0;
            companyCode = 0;
            dept = string.Empty;
            isActive = 1;
            deptHead = 0;
            authLogin = 0;
            hrLogin = 0;
            gateLogin = 0;
            manager = 0;
            login = 0;
            loginTime = string.Empty;

            RuleBroken("FirstName", true);
            RuleBroken("LoginName", true);
            RuleBroken("Password",true);
            RuleBroken("Role", true);
            RuleBroken("Dept", true);
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

        public int DBID
        {
            get
            {
                return dbId;
            }
            set
            {
                dbId = value;
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
                if (value.Trim().Length > 30)
                {
                    throw new Exception("Length must not be greater than 30 characters.");
                }
                RuleBroken("FirstName", (value.Trim() == string.Empty || value.Trim().Length > 30));
                firstName = value.Trim().ToUpper();
            }
        }

        public string LoginName      
        {
            get
            {
                return loginName;
            }
            set
            {
                if(value.Trim().Length > 25)
                {
                    throw new Exception("Length must not be greater than 25 characters.");
                }
                RuleBroken("LoginName", (value.Trim() == string.Empty || value.Trim().Length > 25));
                loginName = value.Trim().ToUpper();
            }
        }

        public string Password       
        {
            get
            {
                return password;
            }
            set
            {
                if (value.Trim().Length > 15)
                {
                    throw new Exception("Length must not be greater than 15 characters.");
                }
                RuleBroken("Password", (value.Trim() == string.Empty || value.Trim().Length > 15));
                password = value.Trim().ToUpper();
            }
        }

        public string ConfirmPassword
        {
            get
            {
                return confirmPwd;
            }
            set
            {
                if (value.Trim().Length > 15)
                {
                    throw new Exception("Length must not be greater than 15 characters.");
                }
                confirmPwd = value.Trim().ToUpper();
            }
        }

        public string Role
        {
            get
            {
                return role;
            }
            set
            {
                if(value.Trim().Length > 15)
                {
                    throw new Exception("Length must not be greater than 15 characters.");
                }
                RuleBroken("Role", value.Trim() == string.Empty || value.Trim().Length > 15);
                role = value.Trim().ToUpper();
            }
        }

        public int BranchCode
        {
            get
            {
                return branchCode;
            }
            set
            {
                branchCode = value;
            }
        }

        public int CompanyCode
        {
            get
            {
                return companyCode;
            }
            set
            {
                companyCode = value;
            }
        }

        public string Dept
        {
            get
            {
                return dept;
            }
            set
            {
                if(value.Trim().Length > 20)
                {
                    throw new Exception("Length must not be greater than 20 characters.");
                }
                RuleBroken("Dept", (value.Trim() == string.Empty || value.Trim().Length > 20));
                dept = value.Trim().ToUpper();
            }
        }

        public int IsActive
        {
            get
            {
                return isActive;
            }
            set
            {
                isActive = value;
            }
        }
        
        public int DeptHead
        {
            get
            {
                return deptHead;
            }
            set
            {
                deptHead = value;
            }
        }
        
        public int AuthLogin
        {
            get
            {
                return authLogin;
            }
            set
            {
                authLogin = value;
            }
        }

        public int HrLogin
        {
            get
            {
                return hrLogin;
            }
            set
            {
                hrLogin = value;
            }
        }

        public int GateLogin
        {
            get
            {
                return gateLogin;
            }
            set
            {
                gateLogin = value;
            }
        }

        public int Manager
        {
            get
            {
                return manager;
            }
            set
            {
                manager = value;
            }
        }

        public int Login
        {
            get
            {
                return login;
            }
            set
            {
                login = value;
            }
        }
        
        public string LoginTime
        {
            get
            {
                return loginTime;
            }
            set
            {
                if(value.Trim().Length > 30)
                {
                    throw new Exception("Length must not be greater than 30 characters.");
                }
                loginTime = value.Trim();
            }
        }
        #endregion
    }
}
