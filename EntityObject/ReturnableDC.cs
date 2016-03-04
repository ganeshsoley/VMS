using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntityObject
{
    public class ReturnableDC: BrokenRule
    {
        #region Private Variable(s)
        private bool flgNew;
        private bool flgEdited;
        private bool flgDeleted;
        private bool flgLoading;
        private bool flgUpload;

        private long dbID;
        private long entryNo;
        private DateTime entryDate;
        private string entryType;
        private string partyName;
        private string vehicleNo;
        private string dcNo;
        private DateTime dcDate;
        private string plantName;
        private DateTime vInDate;
        private DateTime vInTime;
        private DateTime vOutDate;
        private DateTime vOutTime;
        #endregion

        #region Constructor(s)
        public ReturnableDC()
        {
            flgNew = true;
            flgEdited = false;
            flgDeleted = false;

            dbID = 0;
            entryNo = 0;
            entryDate = DateTime.Now;
            entryType = "INWARD";
            partyName = string.Empty;
            vehicleNo = string.Empty;
            dcNo = string.Empty;
            dcDate = DateTime.MinValue;
            plantName = string.Empty;
            vInDate = DateTime.MinValue;
            vInTime = DateTime.MinValue;
            vOutDate = DateTime.MinValue;
            vOutTime = DateTime.MinValue;

            //RuleBroken("EntryNo", true);
            RuleBroken("EntryDate", true);
            //RuleBroken("EntryType", true);
            RuleBroken("PartyName", true);
            RuleBroken("DCNo", true);
            RuleBroken("DCDate", true);
            RuleBroken("PlantName", true);

            DCItems = new ReturnableDCItemList();
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

        public bool IsUpload
        {
            get
            {
                return flgUpload;
            }
            set
            {
                flgUpload = value;
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
                //RuleBroken("EntryNo", (value == 0));
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
                RuleBroken("EntryDate", (value == DateTime.MinValue));
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
                //RuleBroken("EntryType", (value.Trim() == string.Empty));
                entryType = value.Trim().ToUpper();
                flgEdited = true;
            }
        }

        public string PartyName
        {
            get
            {
                return partyName;
            }
            set
            {
                if (!IsLoading)
                {
                    if (value.Trim().Length > 75)
                    {
                        throw new Exception("Length can not be greater than 75 character(s).");
                    }
                }
                RuleBroken("PartyName", (value.Trim() == string.Empty));
                partyName = value.Trim().ToUpper();
                flgEdited = true;
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
                if (!IsLoading)
                {
                    if (value.Trim().Length > 15)
                    {
                        throw new Exception("Length can not be greater than 15 character(s).");
                    }
                }
                //RuleBroken("",);
                vehicleNo = value.Trim().ToUpper();
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
                RuleBroken("DCNo", (value == string.Empty));
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
                RuleBroken("DCDate", (value == DateTime.MinValue));
                dcDate = value;
                flgEdited = true;
            }
        }

        public string PlantName
        {
            get
            {
                return plantName;
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
                RuleBroken("PlantName", (value == string.Empty));
                plantName = value.Trim().ToUpper();
                flgEdited = true;
            }
        }

        public DateTime VInDate
        {
            get
            {
                return vInDate;
            }
            set
            {
                if (!IsLoading)
                {
                }
                vInDate = value;
                flgEdited = true;
            }
        }

        public DateTime VInTime
        {
            get
            {
                return vInTime;
            }
            set
            {
                if (!IsLoading)
                {
                }
                vInTime = value;
                flgEdited = true;
            }
        }

        public DateTime VOutDate
        {
            get
            {
                return vOutDate;
            }
            set
            {
                if (!IsLoading)
                {
                }
                vOutDate = value;
                flgEdited = true;
            }
        }

        public DateTime VOutTime
        {
            get
            {
                return vOutTime;
            }
            set
            {
                if (!IsLoading)
                {
                }
                vOutTime = value;
                flgEdited = true;
            }
        }

        /// <summary>
        /// Gets or sets a collection of <see cref="ProdScrapItem" /> instances for the ProductionScrap Entry.
        /// </summary>
        public ReturnableDCItemList DCItems
        {
            get;
            set;
        }
        #endregion
    }
}
