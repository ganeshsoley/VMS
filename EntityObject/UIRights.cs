using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityObject
{
    public class UIRights
    {
        #region Private Variable(s)
        private bool add;
        private bool modify;
        private bool view;
        private bool delete;
        private bool print;
        #endregion

        #region Public Properties
        public bool AddRight
        {
            get
            {
                return add;
            }
            set
            {
                add = value;
            }
        }

        public bool ModifyRight
        {
            get
            {
                return modify;
            }
            set
            {
                modify = value;
            }
        }

        public bool ViewRight
        {
            get
            {
                return view;
            }
            set
            {
                view = value;
            }
        }

        public bool DeleteRight
        {
            get
            {
                return delete;
            }
            set
            {
                delete = value;
            }
        }

        public bool PrintRight
        {
            get
            {
                return print;
            }
            set
            {
                print = value;
            }
        }
        #endregion
    }
}
