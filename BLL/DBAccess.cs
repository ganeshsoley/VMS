using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb; 
using EntityObject;
using DAL;

namespace BLL
{
   public class DBAccess
    {
       /// <summary>
       /// 
       /// </summary>
       /// <returns></returns>
       public static string mUserConnection()
       {
           return General.GetSQLConnectionString(); 
       }

       /// <summary>
       /// 
       /// </summary>
       /// <returns></returns>
       public static string mMasterConnection()
       {
           return General.GetSQLConnectionString(); 
       }

       /// <summary>
       /// 
       /// </summary>
       /// <param name="mID"></param>
       /// <param name="mUserName"></param>
       /// <returns></returns>
       public static string LoadCompany(long mID,string mUserName)
       {
            return General.LoadCompany(mID, mUserName);
       }
    }
}
