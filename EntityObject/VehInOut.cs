using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntityObject
{
   public class VehInOut : BrokenRule 
   {
       #region Private Variable(s)
       private bool flgNew;
       private bool flgEdited;
       private bool flgDeleted;
       private bool flgLoading;

       private int dbID;
       private int entryNo;
       private DateTime entryDate;
       private int type;
       private int inout;
       private DateTime indate;
       private DateTime intime;
       private DateTime outdate;
       private DateTime outtime;
       private string vehno;
       private string drivername;
       private string cityname;
       private string vendorin;
       private string vendorout;
       private string plant;
       private int ininvcnt;
       private int outinvcnt;
       private string incarrymaterial;
       private string outcarrymaterial;
       private string pucflg;
       private string pucno;
       private string rcbookflg;
       private string rcbookno;
       private string finance;
       private string fitnessflg;
       private string fitnessno;
       private string drlicflg;
       private string drlicno;
       private int personswithveh;
        private int inreading;
        private int outreading;
        #endregion

        #region Constructor
        public VehInOut()
        {
            flgNew = true;
            flgEdited = false;
            flgDeleted = false;

            dbID = 0;
            entryNo = 0;
            entryDate = DateTime.MinValue;
            type = 0;
            inout = 0;
            indate = DateTime.MinValue;
            intime = DateTime.MinValue;
            outdate = DateTime.MinValue;
            outtime = DateTime.MinValue;
            vehno = string.Empty;
            drivername = string.Empty;
            cityname = string.Empty;
            vendorin = string.Empty;
            vendorout = string.Empty;
            plant = string.Empty;
            ininvcnt = 0;
            outinvcnt = 0;
            incarrymaterial = string.Empty;
            outcarrymaterial = string.Empty;
            pucflg = string.Empty;
            pucno = string.Empty;
            rcbookflg = string.Empty;
            rcbookno = string.Empty;
            finance = string.Empty;
            fitnessflg = string.Empty;
            fitnessno = string.Empty;
            drlicflg = string.Empty;
            drlicno = string.Empty;
            personswithveh = 0;
            inreading = 0;
            outreading = 0;

            //InReading = 0;
            //OutReading = 0;
            //type = 0;
            //ininvcnt = 0;
            //OutInvCnt = 0;
            //InOut = 0;

            //RuleBroken("Name", true);
            //RuleBroken("Company", true);
            //RuleBroken("ContactNo", true);
        }
       #endregion

       #region Public Properties
       public bool IsNew
       {
           get
           {
               return flgNew;
           }
           set
           {
               flgNew = value;
           }
       }
       
       public bool IsDeleted
       {
           get
           {
               return flgDeleted;
           }
           set
           {
               flgDeleted = value;
           }
       }
       
       public bool IsEdited
       {
           get
           {
               return flgEdited;
           }
           set
           {
               flgEdited = value;
           }
       }
       
       public bool IsLoading
       {
           get
           {
               return flgLoading;
           }
           set
           {
               flgLoading = value;
           }
       }

       public int Dbid
       {
           get
           {
               return dbID; 
           }
           set
           {
               dbID = value; 
           }
       }
       
       public int EntryNo
       {
           get
           {
               return entryNo; 
           }
           set
           {
               entryNo = value;
               flgEdited = true;
           }

       }
       
       public DateTime EntryDate
       {
           get
           {
               return entryDate; 
           }
           set
           {
               entryDate = value;
               flgEdited = true;
           }
       }
       
       public int Type
       {
           get
           {
               return type; 
           }
           set
           {
               type = value;
               flgEdited = true;
           }
       }
       
       public int InOut
       {
           get
           {
               return inout; 
           }
           set
           {
               inout = value;
               flgEdited = true;
           }
       }
       
       public DateTime InDate
       {
           get
           {
               return indate; 
           }
           set
           {
               indate = value; 
               flgEdited = true;
           }
       }
       
       public DateTime InTime
       {
           get
           {
               return intime; 
           }
           set
           {
               intime = value;
               flgEdited = true;
           }
       }
       
       public DateTime OutDate
       {
           get
           {
               return outdate;
           }
           set
           {
               outdate = value;
               flgEdited = true;
           }
       }
       
       public DateTime OutTime
       {
           get
           {
               return outtime; 
           }
           set
           {
               outtime = value;
               flgEdited = true;
           }
       }
       
       public string VehNo
       {
           get
           {
               return vehno; 
           }
           set
           {
               vehno = value.Trim().ToUpper();
               flgEdited = true;
           }
       }
       
       public string DriverName
       {
           get
           {
               return drivername; 
           }
           set
           {
               drivername = value.Trim().ToUpper();
               flgEdited = true;
           }
       }
       
       public string CityName
       {
           get
           {
               return cityname; 
           }
           set
           {
               cityname = value.Trim().ToUpper();
               flgEdited = true;
           }
       }
       
       public string VendorIn
       {
           get
           {
               return vendorin; 
           }
           set
           {
               vendorin = value.Trim().ToUpper();
               flgEdited = true;
           }
       }
       
       public string VendorOut
       {
           get
           {
               return vendorout; 
           }
           set
           {
               vendorout = value.Trim().ToUpper();
               flgEdited = true;
           } 
       }
       
       public string Plant
       {
           get
           {
               return plant; 
           }
           set
           {
               plant = value.Trim().ToUpper();
               flgEdited = true;
           }
       }
       
       public int InInvCnt
       {
           get
           {
               return ininvcnt; 
           }
           set
           {
               ininvcnt = value;
               flgEdited = true;
           }
       }
       
       public int OutInvCnt
       {
           get
           {
               return outinvcnt; 
           }
           set
           {
               outinvcnt = value;
               flgEdited = true;
           }
       }
       
       public string InCarryMaterial
       {
           get
           {
               return incarrymaterial; 
           }
           set
           {
               incarrymaterial = value.Trim().ToUpper();
               flgEdited = true;
           }
       }
       
       public string OutCarryMaterial
       {
           get
           {
               return outcarrymaterial; 
           }
           set
           {
               outcarrymaterial = value.Trim().ToUpper();
               flgEdited = true;
           }
       }
       
       public string PUCFlg
       {
           get
           {
               return pucflg; 
           }
           set
           {
               pucflg = value;
               flgEdited = true;
           }
       }
       
       public string PUCNo
       {
           get
           {
               return pucno; 
           }
           set
           {
               pucno = value.Trim().ToUpper();
               flgEdited = true;
           }
       }
       
       public string RCBookFlg
       {
           get
           {
               return rcbookflg; 
           }
           set
           {
               rcbookflg = value;
               flgEdited = true;
           }
       }
       
       public string RCBookNo
       {
           get
           {
               return rcbookno; 
           }
           set
           {
               rcbookno = value.Trim().ToUpper();
               flgEdited = true;
           }
       }
       
       public string Finance
       {
           get
           {
               return finance; 
           }
           set
           {
               finance = value;
               flgEdited = true;
           }
       }
       
       public string FitnessFlg
       {
           get
           {
               return fitnessflg;
           }
           set
           {
               fitnessflg = value;
               flgEdited = true;
           }
       }
       
       public string FitnessNo
       {
           get
           {
               return fitnessno; 
           }
           set
           {
               fitnessno = value.Trim().ToUpper();
               flgEdited = true;
           }
       }
       
       public string DrLicFlg
       {
           get
           {
               return drlicflg; 
           }
           set
           {
               drlicflg = value;
               flgEdited = true;
           }
       }
       
       public string DrLicNo
       {
           get
           {
               return drlicno; 
           }
           set
           {
               drlicno = value.Trim().ToUpper();
               flgEdited = true;
           }
       }
       
       public int PersonsWithVeh
       {
           get
           {
               return  personswithveh; 
           }
           set
           {
               personswithveh = value;
               flgEdited = true;
           }
       }

        public int InReading
        {
            get
            {
                return inreading;
            }
            set
            {
                inreading = value;
                flgEdited = true;
            }
        }

        public int OutReading
        {
            get
            {
                return outreading;
            }
            set
            {
                outreading = value;
                flgEdited = true;
            }
        }
        #endregion
    }
}
