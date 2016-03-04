using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntityObject
{
   public class Driver : BrokenRule
   {
       #region Private Variable
       private bool flgNew;
       private bool flgEdited;
       private bool flgDeleted;
       private bool flgLoading;

       private int dbid;
       private string name;
       private string licenseNo;
       private bool isActive;
       #endregion

       #region Constructor
       public Driver()
        {
            flgNew = true;
            flgEdited = false;
            flgDeleted = false;

            dbid = 0;
            name = string.Empty;
            licenseNo = string.Empty;
            isActive = true;
            
            RuleBroken("name", true);
            //RuleBroken("Description", true);
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

       public int DBID
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
       
       public string Name
       {
           get
           {
               return name;
           }
           set
           {
               if (!flgLoading)
               {
                   if (value.Trim().Length > 50)
                   {
                       throw new Exception("Length can not be greater than 50 character(s).");
                   }
               }
               RuleBroken("name", (value.Trim().Length == 0));
               name = value.Trim().ToUpper();
               flgEdited = true;
           }
       }
       
       public string LicenceNo
       {
           get
           {
               return licenseNo; 
           }
           set
           {
               if (!flgLoading)
               {
                   if (value.Trim().Length > 30)
                   {
                       throw new Exception("Length can not be greater than 30 character(s).");
                   }
               }
               licenseNo = value;
               flgEdited = true;
           }
       }
       
       public bool IsActive
       {
           get
           {
               return isActive; 
           }
           set
           {
               isActive  = value;
               flgEdited = true;
           }
       }
       
       #endregion
   }
}
