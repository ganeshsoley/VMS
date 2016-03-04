using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class clsDBConnection
    {
        public static string GetConnectionString()
        {
        //    return "Data Source=oraold; User Id=DTPLINV11415;Password=DTPLINV11415;";
            //return "Data Source = ORA; User Id = DTPLINV11415; Password = DTPLINV11415;";
        // SQL Server connectivity
        return "Data Source=.\\sqlexpress;Initial Catalog=trial;Integrated Security=True";
        }
    }
}
