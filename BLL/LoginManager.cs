using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityObject;
using DAL;

namespace BLL
{
    public class LoginManager
    {
        public static List<KeyValuePair<long, string>> GetCompFinYear()
        {
            string rec;
            DataTable dTable = LoginDAL.GetCompFinYear();

            List<KeyValuePair<long, string>> FinYrData = new List<KeyValuePair<long, string>>();

            foreach (DataRow dRow in dTable.Rows)
            {
                UInt32 recID = Convert.ToUInt32(dRow["DBID"].ToString());
                rec = dRow["Plant"].ToString() + ": " + dRow["FromDt"].ToString() + " - " + dRow["ToDt"].ToString();
                FinYrData.Add(new KeyValuePair<long, string>(recID, rec));
            }
            return FinYrData;
        }

        public static int CheckLogin(string strUserName, string strPwd)
        {
            int userID;
            userID = LoginDAL.CheckLogin(strUserName, strPwd);

            return userID;
        }
    }
}
