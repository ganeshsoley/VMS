using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;

namespace BLL
{
    public class GeneralBLL
    {
        public static string GetSQLConnectionString()
        {
            return General.GetSQLConnectionString();
        }

        public static string GetSqlServerPCName()
        {
            return General.GetSqlServerPCName();
        }

        public static int GetSQLPort()
        {
            return General.GetSQLPort();
        }

        public static string GetSqlServerInstanceName()
        {
            return General.GetSqlServerInstanceName();
        }

        public static string GetDatabaseName()
        {
            return General.GetDatabaseName();
        }

        public static string GetDBUserName()
        {
            return General.GetDBUserName();
        }

        public static string GetDBPwd()
        {
            return General.GetDBPwd();
        }

    }
}
