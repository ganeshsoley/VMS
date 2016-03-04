using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EntityObject;
using BLL;

namespace UI
{
    public class GeneralMethods
    {
        public static bool frmAddRight, frmModifyRight, frmViewRight, frmDeleteRight, repView, repPrintRight;
        public static void Selection(TextBox objTxt)
        {
            objTxt.SelectionStart = 0;
            objTxt.SelectionLength = objTxt.Text.Length;
        }

        public static void Selection(ComboBox objCbo)
        {
            objCbo.SelectionStart = 0;
            objCbo.SelectionLength = objCbo.Text.Length;
        }

        public static bool IsDate(string txtDt, out DateTime dt)
        {
            //DateTime dt;
            bool IsValid = false;
            IsValid = DateTime.TryParseExact(txtDt, new[] 
            { "M/d/yyyy", "M/d/yy", "MM/dd/yy", "MM/dd/yyyy", "dd/MM/yyyy", "dd/MM/yy", "yy/MM/dd", "yyyy/MM/dd",
                "yyyy-MM-dd", "dd-MM-yyyy", "dd-MM-yy", "MM-dd-yyyy", "MM-dd-yy" }, CultureInfo.InvariantCulture, DateTimeStyles.None, out dt);
            return IsValid;
        }

        /// <summary>
        /// Determines if input is a number or not.
        /// </summary>
        /// <param name="e"> </param>
        /// <returns>Boolean value 'True' if input is a number otherwise false.</returns>
        public static bool IsNumber(KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                return true;
            else
                return false;
        }

        public static void AutoComplete(ComboBox cb, System.Windows.Forms.KeyPressEventArgs e, bool blnLimitToList)
        {
            string strFindStr = "";

            if (e.KeyChar == (char)8)
            {
                if (cb.SelectionStart <= 1)
                {
                    cb.Text = "";
                    return;
                }

                if (cb.SelectionLength == 0)
                    strFindStr = cb.Text.Substring(0, cb.Text.Length - 1);
                else
                    strFindStr = cb.Text.Substring(0, cb.SelectionStart - 1);
            }
            else
            {
                if (cb.SelectionLength == 0)
                    strFindStr = cb.Text + e.KeyChar;
                else
                    strFindStr = cb.Text.Substring(0, cb.SelectionStart) + e.KeyChar;
            }

            int intIdx = -1;

            // Search the string in the ComboBox list.

            intIdx = cb.FindString(strFindStr);

            if (intIdx != -1)
            {
                cb.SelectedText = "";
                cb.SelectedIndex = intIdx;
                cb.SelectionStart = strFindStr.Length;
                cb.SelectionLength = cb.Text.Length;
                e.Handled = true;
            }
            else
            {
                e.Handled = blnLimitToList;
            }
        }

        public static string GetSQLConnectionString()
        {
            return GeneralBLL.GetSQLConnectionString();
        }

        public static string GetSMSAPI()
        {
            string strAPI = "http://103.16.101.52:8080/bulksms/bulksms?username=syy-dhoot1&password=1234&type=0&dlr=1";
            return strAPI;
        }

        public static string GetSMSSenderID()
        {
            string strSenderID = "&source=DTPLFa";

            return strSenderID;
        }

        public static void FormAuthenticate(string frmName, UserCompany objCompany, User objUser)
        {
            frmAddRight = false;
            frmModifyRight = false;
            frmViewRight = false;
            frmDeleteRight = false;
            repView = false;
            repPrintRight = false;

            string rMenuKey = string.Empty;

            switch (frmName)
            {
                case "frmDeptList":
                    rMenuKey = "D0011";
                    break;

                case "frmEmployeeList":
                    rMenuKey = "D0012";
                    break;

                case "frmVisitorList":
                    rMenuKey = "D0013";
                    break;

                case "frmCityList":
                    rMenuKey = "D0014";
                    break;

                case "frmDriverList":
                    rMenuKey = "D0015";
                    break;

                case "frmVehicleList":
                    rMenuKey = "D0016";
                    break;

                case "frmUserList":
                    rMenuKey = "D0017";
                    break;

                case "frmVehInOutList":
                    rMenuKey = "T0011";
                    break;

                case "frmAppointmentList":
                    rMenuKey = "T0012";
                    break;

                case "frmVisitorGPList":
                    rMenuKey = "T0013";
                    break;

                case "frmReturnableDCList":
                    rMenuKey = "T0014";
                    break;

                case "frmUserAuthority":
                    rMenuKey = "U0011";
                    break;
            }

            if (rMenuKey != string.Empty)
            {
                string[] strRightList = UserRightsManager.GetUserFormRights(objUser.DBID, rMenuKey);

                if (strRightList != null)
                {
                    foreach (string strRight in strRightList)
                    {
                        if (strRight == rMenuKey + "A")
                            frmAddRight = true;
                        else if (strRight == rMenuKey + "M")
                            frmModifyRight = true;
                        else if (strRight == rMenuKey + "V")
                            frmViewRight = true;
                        else if (strRight == rMenuKey + "D")
                            frmDeleteRight = true;
                        else if (strRight == rMenuKey + "P")
                            repPrintRight = true;
                    }
                }
            }
        }
    }
}
