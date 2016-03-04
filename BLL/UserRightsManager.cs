using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using EntityObject;
using DAL;
using System.Data;

namespace BLL
{
    public class UserRightsManager
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

        public static string[] GetUserFormRights(long UserID, string strMenu)
        {
            string[] objList = null;
            dTable = UserRightsDAL.GetUserFormRights(UserID, strMenu);

            if (dTable.Rows.Count > 0)
            {
                objList = dTable.AsEnumerable().Select(row => row.Field<string>("MENUID")).ToArray();
            }
            return objList;
        }
        public static bool Save(UserRightsList objList)
        {
            bool flgSave;
            try
            {
                using (TransactionScope objTScope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    UserRightsDAL.Delete(objList[0].UserID);
                    foreach (UserRights objRight in objList)
                    {
                        UserRightsDAL.Save(objRight);
                    }
                    flgSave = true;
                    objTScope.Complete();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return flgSave;
        }
    }
}
