using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntityObject
{
    public class UserRights
    {
        #region Private Variable(s)
        private bool flgNew;
        private bool flgEdited;
        private bool flgDeleted;
        private bool flgLoading;

        private long userID;
        private string userName;
        private string menuID;
        private string formName;
        #endregion

        #region Constructor(s)
        public UserRights()
        {
            flgNew = true;
            flgEdited = false;
            flgDeleted = false;
            flgLoading = false;

            userID = 0;
            userName = string.Empty;
            menuID = string.Empty;
            formName = string.Empty;
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

        public long UserID
        {
            get
            {
                return userID;
            }
            set
            {
                userID = value;
            }
        }

        public string UserName
        {
            get
            {
                return userName;
            }
            set
            {
                userName = value;
            }
        }

        public string MenuID
        {
            get
            {
                return menuID;
            }
            set
            {
                menuID = value.Trim().ToUpper();
            }
        }

        public string FormName
        {
            get
            {
                return formName;
            }
            set
            {
                formName = value.Trim();
            }
        }
        #endregion

    }
}
