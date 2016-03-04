using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using EntityObject;

namespace BLL
{
    public class frmMainManager
    {
        private static DataTable dTable;
        public static string[] GetUserRights(long UserID)
        {
            string[] objList = null;
            dTable = UserRightsDAL.GetUserRights(UserID);

            if (dTable.Rows.Count > 0)
            {
                objList = dTable.AsEnumerable().Select(row => row.Field<string>("MENUID")).ToArray();
            }
            return objList;
        }

        public static UserCompany LoadCompany(Int32 CurrentCompID)
        {
            UserCompany objCompany;
            objCompany = General.LoadCompany(CurrentCompID);
            return objCompany;
        }

    }
}
