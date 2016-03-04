using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityObject;
using DAL;

namespace BLL
{
    public class UserManager
    {
        private static  string condition;

        /// <summary>
        /// Write-Only Public Property.
        /// </summary>
        public static string Condition
        {
            set 
            {
                condition = value;
            }

        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbid"></param>
        /// <returns></returns>
        public static User GetItem(int dbid)
        {
            return UserDAL.GetItem(dbid);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isActive"></param>
        /// <returns></returns>
        public static UserList GetList(int isActive)
        {
            string strQry, strWhere;
            strQry = "SELECT DBID, LOGINNAME, ROLE " +
                " FROM USERPROFILE " ;
            strWhere = " WHERE ISACTIVE = " + isActive;

            if(condition !=null && condition== string.Empty)
            {
                strWhere += " AND " + condition;
            }
            strQry += strWhere + " ORDER BY LOGINNAME";

            return UserDAL.GetList(strQry);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objUser"></param>
        /// <returns></returns>
        public static bool Save(User objUser)
        {
            bool recSave;
            recSave = UserDAL.Save(objUser);
            return recSave;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="DBID"></param>
        /// <returns></returns>
        public static bool Delete (User objUser)
        {
            return UserDAL.Delete(objUser.DBID);
        }
    }
}
