using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Data;
using System.Linq;
using System.Text;
using DAL;

namespace BLL
{
    public class GeneralListManager
    {
        private static DataTable dTable;
        
        public static ListViewItem[] GetList(string strQry)
        {
            ListViewItem[] objList = null;
            //dTable = GeneralListDAL.GetList(strQry);
            if (dTable != null)
            {
                int TotalRecords = dTable.Rows.Count;
                int ColCount = GetHeaderCount();
                objList = new ListViewItem[TotalRecords];

                for (int RowCount = 0; RowCount < TotalRecords; RowCount++ )
                {
                    ListViewItem objLvItem = new ListViewItem();
                    
                    objLvItem.Name = Convert.ToString(dTable.Rows[RowCount].ItemArray[0]);
                    objLvItem.Text = Convert.ToString(dTable.Rows[RowCount].ItemArray[1]);
                    int d = 2;
                    foreach (var item in dTable.Rows[RowCount].ItemArray)
                    {
                        if (d == ColCount)
                        {
                            break;
                        }
                        if (Convert.ToString(dTable.Rows[RowCount][d]) != string.Empty)
                            objLvItem.SubItems.Add(Convert.ToString(dTable.Rows[RowCount][d]));
                        else
                            objLvItem.SubItems.Add("");
                        d++;
                        
                    }

                    objList[RowCount] = objLvItem;
                }
            }
            return objList;
        }

        public static string[] GetHeaderList(string strQry)
        {
            string[] objHeaderList = null;
            dTable = GeneralListDAL.GetList(strQry);
            
            if (dTable != null)
            {
                int HeaderCount = GetHeaderCount();
                objHeaderList = new string[HeaderCount];
                for (int rCount = 0; rCount < HeaderCount; rCount++)
                {
                    string colName = dTable.Columns[rCount].ColumnName;
                    objHeaderList[rCount] = colName;
                }
            }
            return objHeaderList;
        }

        public static int GetHeaderCount()
        {
            return dTable.Columns.Count;
        }
    }
}
