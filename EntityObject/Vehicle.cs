using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntityObject
{
   public class Vehicle : BrokenRule
   {
       #region Private Variable
       private bool flgNew;
       private bool flgEdited;
       private bool flgDeleted;
       private bool flgLoading;

       private int dbid;
       private string vehicleNo;
       private string vLicenseNo;
       private DateTime pucExpiry;
       private int isActive;
       #endregion

       #region Constructor
       public Vehicle()
        {
            flgNew = true;
            flgEdited = false;
            flgDeleted = false;

            dbid = 0;
            vehicleNo = string.Empty;
            vLicenseNo = string.Empty;
            isActive = 0;
            
            
            RuleBroken("vehicleNo", true);
            
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
               return dbid; 
           }
           set
           {
               dbid = value; 
           }
       }
       public string VehicleNo
       {
           get
           {
               return vehicleNo;
           }
           set
           {
               if (!flgLoading)
               {
                   if (value.Trim().Length > 15)
                   {
                       throw new Exception("Length can not be greater than 15 character(s).");
                   }
               }
               RuleBroken("vehicleNo", (value.Trim().Length == 0));
               vehicleNo  = value.Trim().ToUpper();
               flgEdited = true;
           }
       }
       public string VLicencseNo
       {
           get
           {
               return vLicenseNo; 
           }
           set
           {
               if (!flgLoading)
               {
                   if (value.Trim().Length > 15)
                   {
                       throw new Exception("Length can not be greater than 15 character(s).");
                   }
               }
               vLicenseNo = value.Trim().ToUpper();
               flgEdited = true;
           }
       }
       public DateTime PUCExpiry
       {
           get
           {
               return pucExpiry; 
           }
           set
           {
               pucExpiry = value;
               flgEdited = true;
           }
       }
       public int IsActive
       {
           get
           {
               return isActive; 
           }
           set
           {
               isActive = value;
               flgEdited = true; 
           }
       }
       #endregion

   }
}
