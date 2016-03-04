using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntityObject
{
    public class ReturnableDCItem: BrokenRule
    {
        #region Private Variable(s)
        private bool flgNew;
        private bool flgEdited;
        private bool flgDeleted;
        private bool flgLoading;
        private bool flgModified;

        private long dbID;
        private long entryNo;
        private DateTime entryDate;
        private string entryType;
        private string dcNo;
        private DateTime dcDate;
        private string itemCode;
        private string itemDescr;
        private decimal qty;
        private decimal qty2;
        private string unitName;
        private string remark;
        private long masterDBID;
        #endregion

        #region Constructor(s)
        public ReturnableDCItem()
        {
            flgNew = true;
            flgEdited = false;
            flgDeleted = false;
            flgModified = false;

            dbID = 0;
            entryNo = 0;
            entryDate = DateTime.MinValue;
            entryType = string.Empty;
            dcNo = string.Empty;
            dcDate = DateTime.MinValue;
            itemCode = string.Empty;
            itemDescr = string.Empty;
            qty = 0;
            qty2 = 0;
            unitName = string.Empty;
            remark = string.Empty;
            masterDBID = 0;

            RuleBroken("ItemCode", true);
            RuleBroken("Qty", true);
            //RuleBroken("Unit", true);
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

        public bool IsModified
        {
            get
            {
                return flgModified;
            }
            set
            {
                flgModified = value;
            }
        }
        #endregion

        #region Public Object Properties
        public long DBID
        {
            get
            {
                return dbID;
            }
            set
            {
                dbID = value;
                flgEdited = true;
            }
        }

        public long EntryNo
        {
            get
            {
                return entryNo;
            }
            set
            {
                if (!IsLoading)
                {
                }
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
                if (!IsLoading)
                {
                }
                entryDate = value;
                flgEdited = true;
            }
        }

        public string EntryType
        {
            get
            {
                return entryType;
            }
            set
            {
                if (!IsLoading)
                {
                    if (value.Trim().Length > 10)
                    {
                        throw new Exception("Length can not be greater than 10 character(s).");
                    }
                }
                entryType = value.Trim().ToUpper();
                flgEdited = true;
            }
        }

        public string DCNo
        {
            get
            {
                return dcNo;
            }
            set
            {
                if (!IsLoading)
                {
                    if (value.Trim().Length > 30)
                    {
                        throw new Exception("Length can not be greater than 30 character(s).");
                    }
                }
                dcNo = value.Trim().ToUpper();
                flgEdited = true;
            }
        }

        public DateTime DCDate
        {
            get
            {
                return dcDate;
            }
            set
            {
                if (!IsLoading)
                {
                }
                RuleBroken("DCDate", true);
                dcDate = value;
                flgEdited = true;
            }
        }

        public string ItemCode
        {
            get
            {
                return itemCode;
            }
            set
            {
                if (!IsLoading)
                {
                    if (value.Trim().Length > 30)
                    {
                        throw new Exception("Length can not be greater than 30 character(s).");
                    }
                }
                RuleBroken("ItemCode", (value.Trim().Length == 0));
                itemCode = value.Trim().ToUpper();
                flgEdited = true;
            }
        }

        public string ItemDescr
        {
            get
            {
                return itemDescr;
            }
            set
            {
                if (!IsLoading)
                {
                    if (value.Trim().Length > 50)
                    {
                        throw new Exception("Length can not be greater than 50 character(s).");
                    }
                }
                itemDescr = value.Trim().ToUpper();
                flgEdited = true;
            }
        }

        public decimal Qty
        {
            get
            {
                return qty;
            }
            set
            {
                if (!IsLoading)
                {

                }
                RuleBroken("Qty", (value == 0));
                qty = value;
                flgEdited = true;
            }
        }

        public decimal Qty2
        {
            get
            {
                return qty2;
            }
            set
            {
                if (!IsLoading)
                {

                }
                qty2 = value;
                flgEdited = true;
            }
        }

        public string UnitName
        {
            get
            {
                return unitName;
            }
            set
            {
                if (!IsLoading)
                {
                    if (value.Trim().Length > 20)
                    {
                        throw new Exception("Length can not be greater than 20 character(s).");
                    }
                }
                //RuleBroken("Unit", (value.Trim().Length == 0));
                unitName = value.Trim().ToUpper();
                flgEdited = true;
            }
        }

        public string Remark
        {
            get
            {
                return remark;
            }
            set
            {
                if (!IsLoading)
                {
                    if (value.Trim().Length > 50)
                    {
                        throw new Exception("Length can not be greater than 50 character(s).");
                    }
                }
                remark = value.Trim().ToUpper();
                flgEdited = true;
            }
        }

        public long MasterDBID
        {
            get
            {
                return masterDBID;
            }
            set
            {
                masterDBID = value;
                flgEdited = true;
            }
        }
        #endregion
    }
}
