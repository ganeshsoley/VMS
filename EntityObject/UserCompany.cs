using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;

namespace EntityObject
{
   public class UserCompany
    {
        static long Dbid;
        static long mCmpdbid;
        static long mMainDBID;
        static string mCompany;
        static string mCompanyAddress;
        static string mAddress1;
        static string mAddress2;
        static string mCity;
        static string mPhone1;
        static string mPhone2;
        static DateTime mFROMDATE;
        static DateTime mTODATE;
        static string mPlantID;
        static string mECCNo;
        static string mRange;
        static string mOFFICE_PHONE;
        static string mOFFICE_FAX;
        static string mCSTNO;
        static string mBSTNO;
        static string x;
        static string y;
        static string _UserName;
        static string _DataSource;

        //static bool mTransactionLock;
        //static bool gflgDISPATCHLOCK;
        public string m_UserName
        {
            get
            {
                return _UserName;
            }
            set
            {
                _UserName = value;
            }
        }
        public string m_DataSource
        {
            get
            {
                return _DataSource;
            }
            set
            {
                _DataSource = value;
            }
        }
        public long m_Dbid
        {
            get
            {
                return Dbid;
            }
            set
            {
                Dbid = value;
            }
        }
        public long m_Copdbid
        {
            get
            {
                return mCmpdbid;
            }
            set
            {
                mCmpdbid = value;
            }
        }
        public long m_MainDbid
        {
            get
            {
                return mMainDBID;
            }
            set
            {
                mMainDBID = value;
            }
        }
        public string m_Company
        {
            get
            {
                return mCompany;
            }
            set
            {
                mCompany = value;
            }
        }
        public string m_CompanyAddress
        {
            get
            {
                return mCompanyAddress;
            }
            set
            {
                mCompanyAddress = value;
            }
        }
        public string m_Address1
        {
            get
            {
                return mAddress1;
            }
            set
            {
                mAddress1 = value;
            }
        }
        public string m_Address2
        {
            get
            {
                return mAddress2;
            }
            set
            {
                mAddress2 = value;
            }
        }
        public string m_City
        {
            get
            {
                return mCity;
            }
            set
            {
                mCity = value;
            }
        }
        public string m_Phone1
        {
            get
            {
                return mPhone1;
            }
            set
            {
                mPhone1 = value;
            }
        }
        public string m_Phone2
        {
            get
            {
                return mPhone2;
            }
            set
            {
                mPhone2 = value;
            }
        }
        public DateTime m_FromDate
        {
            get
            {
                return mFROMDATE;
            }
            set
            {
                mFROMDATE = value;
            }
        }
        public DateTime m_ToDate
        {
            get
            {
                return mTODATE;
            }
            set
            {
                mTODATE = value;
            }
        }
        public string m_PlantID
        {
            get
            {
                return mPlantID;
            }
            set
            {
                mPlantID = value;
            }
        }
        public string m_ECCNO
        {
            get
            {
                return mECCNo;
            }
            set
            {
                mECCNo = value;
            }
        }
        public string m_Range
        {
            get
            {
                return mRange;
            }
            set
            {
                mRange = value;
            }
        }
        public string m_Office_Phone
        {
            get
            {
                return mOFFICE_PHONE;
            }
            set
            {
                mOFFICE_PHONE = value;
            }
        }
        public string m_Office_Fax
        {
            get
            {
                return mOFFICE_FAX;
            }
            set
            {
                mOFFICE_FAX = value;
            }
        }
        public string m_CSTNO
        {
            get
            {
                return mCSTNO;
            }
            set
            {
                mCSTNO = value;
            }
        }
        public string M_BSTNO
        {
            get
            {
                return mBSTNO;
            }
            set
            {
                mBSTNO = value;
            }
        }
        public string mx
        {
            get
            {
                return x;
            }
            set
            {
                x = value;
            }
        }
        public string my
        {
            get
            {
                return y;
            }
            set
            {
                y = value;
            }
        }

    }
}
