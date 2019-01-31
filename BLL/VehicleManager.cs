using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using EntityObject;
using DAL; 

namespace BLL
{
   public class VehicleManager
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public static VehicleList GetList(string strWhere, bool blnIsActive)
        {
            return VehicleDAL.GetList(strWhere, blnIsActive);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbid"></param>
        /// <returns></returns>
        public static Vehicle GetItem(int dbid)
        {
            return VehicleDAL.GetItem(dbid);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objVehicle"></param>
        /// <returns></returns>
        public static bool Save(Vehicle objVehicle, User objUser)
        {
            bool flgSave;
            try
            {
                using (TransactionScope objTScope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    if (objVehicle.IsEdited || objVehicle.IsNew)
                    {
                        VehicleDAL.Save(objVehicle, objUser);
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
        /// <param name="objVehicle">Object containing all data values.</param>
        /// <returns>boolean value True if Record is deleted successfully 
        /// otherwise returns False.</returns>
        public static bool Delete(Vehicle objVehicle)
        {
            bool recDel;
            recDel = VehicleDAL.Delete(objVehicle.Dbid);
            return recDel;
        }

        /// <summary>
        /// Checks whether current Record already exists or not.
        /// </summary>
        /// <param name="objVehicle">Object containing all Data Values.</param>
        /// <returns>boolean value True if Record already Exists into Database
        /// otherwise returns False.</returns>
        public static bool IsVehicleExist(Vehicle objVehicle)
        {
            return VehicleDAL.IsVehicleExist(objVehicle);
        }

    }
}
