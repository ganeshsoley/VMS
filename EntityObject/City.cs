using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntityObject
{
   public class City : BrokenRule 
   {
       #region Private Variable
       private bool flgNew;
       private bool flgEdited;
       private bool flgDeleted;
       private bool flgLoading;

       private int dbid;
       private string city;
       #endregion

       #region Constructor
       public City()
        {
            flgNew = true;
            flgEdited = false;
            flgDeleted = false;

            dbid = 0;
            city = string.Empty;
            
            RuleBroken("city", true);
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
       public string mCity
       {
           get
           {
               return city;
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
               RuleBroken("city", (value.Trim().Length == 0));
               city = value.Trim().ToUpper();
               flgEdited = true;
           }
       }
       #endregion
   }
}
