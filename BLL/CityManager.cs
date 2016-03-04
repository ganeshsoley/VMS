using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using EntityObject;
using DAL;
namespace BLL
{
   public class CityManager
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public static CityList GetList(string strWhere)
        {
            return CityDAL.GetList(strWhere);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbid"></param>
        /// <returns></returns>
        public static City GetItem(int dbid)
        {
            return CityDAL.GetItem(dbid);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objPOType"></param>
        /// <returns></returns>
        public static bool Save(City objCity)
        {
            bool flgSave;
            try
            {
                using (TransactionScope objTScope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    if (objCity.IsEdited || objCity.IsNew)
                    {
                        CityDAL.Save(objCity);
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

        /// <summary>
        /// Deletes Record from Database.
        /// </summary>
        /// <param name="objCity">Object containing all data values.</param>
        /// <returns>boolean value True if Record is deleted successfully 
        /// otherwise returns False.</returns>
        public static bool Delete(City objCity)
        {
            bool recDel;
            recDel = CityDAL.Delete(objCity.DBID);
            return recDel;
        }

        /// <summary>
        /// Checks whether current Record already exists or not.
        /// </summary>
        /// <param name="objDept">Object containing all Data Values.</param>
        /// <returns>boolean value True if Record already Exists into Database
        /// otherwise returns False.</returns>
        public static bool IsCityExist(City objCity)
        {
            return CityDAL.IsCityExist(objCity);
        }

    }
}
