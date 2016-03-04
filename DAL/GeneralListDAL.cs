using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace DAL
{
    public class GeneralListDAL
    {
        public static DataTable GetList(string strQry)
        {
            DataTable dtable = null;
            using (SqlConnection Conn = new SqlConnection (General.GetSQLConnectionString()))
            {
                using (SqlCommand cmd = Conn.CreateCommand())
               {
                   cmd.CommandType = CommandType.Text;
                   cmd.CommandText = strQry;

                   try
                   {
                       if (Conn.State != ConnectionState.Open)
                        {
                            Conn.Open();
                        }
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            dtable = new DataTable();
                            da.Fill(dtable);
                        }
                    }
                    catch
                    {
                        throw;
                    }
                }
            }
            return dtable;
        }
    }
}
