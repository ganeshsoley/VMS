using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace UI.ReadWriteCSV
{
    public class CSVFileReader: StreamReader
    {
        #region Constructor(s)
        public CSVFileReader(Stream objStream)
        {
        }

        public CSVFileReader(string fileName) : base(fileName)
        {
        }
        #endregion

        public bool ReadRow(CSVRow row)
        {
        }
    }
}
