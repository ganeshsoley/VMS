using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BLL;

namespace UI
{
    public partial class frmLogin : Form
    {
        #region Private Variable(s)
        private int userId;
        private string userName;
        private int compDBID;
        #endregion

        #region Public Properties
        public int UserID
        {
            get
            {
                return userId;
            }
            set
            {
                userId = value;
            }
        }

        public string UserName
        {
            get
            {
                return userName;
            }
            set
            {
                userName = value;
            }
        }

        public int SelectedComID
        {
            get
            {
                return compDBID;
            }

            set
            {
                compDBID = value;
            }
        }
        #endregion

        #region Constructor(s)
        public frmLogin()
        {
            InitializeComponent();
            //string YR;
            //string fd, td, x;
            //DataTable dt = new DataTable();
            ////  string conn;
            //string connectionString = DBAccess.mMasterConnection();

            //using (SqlConnection con = new SqlConnection(connectionString))
            //{
            //    string query = "Select cf.dbid, Plant, YEAR(fromdate) fromdate1, " +
            //        " YEAR(todate) Todate1 " +
            //        " FROM CompFinYr cf, SysCompany c " +
            //        " WHERE cf.compDBID = c.CompanyID " +
            //        " order by cf.dbid desc ";

            //    SqlCommand cmd = new SqlCommand(query, con);
            //    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            //    {
            //        da.Fill(dt);
            //    }
            //}

            //List<KeyValuePair<long, string>> data = new List<KeyValuePair<long, string>>();
            //foreach (DataRow dr in dt.Rows)
            //{

            //    fd = dr["fromdate1"].ToString();
            //    td = dr["todate1"].ToString();
            //    YR = fd + "-" + td;
            //    x = dr["plant"].ToString() + ": " + YR;
            //    long abc = Convert.ToUInt32(dr["dbid"].ToString());
            //    data.Add(new KeyValuePair<long, string>(abc, x));

            //    cboCompany.DataSource = new BindingSource(data, null);
            //    cboCompany.DisplayMember = "Value";
            //    cboCompany.ValueMember = "Key";
            //    //    cboCompany.Items.Add(x).ToString();
            //    //    cboCompany.DisplayMember  = x.ToString();
            //    //   cboCompany.ValueMember = dr["dbid"].ToString();
            //}
        }
        #endregion

        private void FillCompFinYear()
        {
            List<KeyValuePair<long, string>> CompFinYearData = LoginManager.GetCompFinYear();

            cboCompany.DataSource = new BindingSource(CompFinYearData, null);
            cboCompany.DisplayMember = "Value";
            cboCompany.ValueMember = "Key";
            cboCompany.SelectedIndex = -1;
            cboCompany.Text = "Select Fin. Year";
        }

        private void Reset()
        {
            txtUserName.Text = "";
            txtPassword.Text = "";
            cboCompany.SelectedIndex = -1;
            cboCompany.Text = "Select Fin. Year";
        }

        private void frmLogin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            this.Icon = new Icon("Images/DTPL.ico");
            try
            {
                FillCompFinYear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "VMS", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void txtUserName_Enter(object sender, EventArgs e)
        {
            txtUserName.SelectAll();
        }

        private void txtUserName_TextChanged(object sender, EventArgs e)
        {
            if (txtUserName.Text.Trim().Length == 0)
            {
                btnLogIn.Enabled = false;
            }
            else
            {
                btnLogIn.Enabled = true;
            }
        }

        private void btnLogIn_Click(object sender, EventArgs e)
        {
            //KeyValuePair<long, string> selectedPair = (KeyValuePair<long, string>)cboCompany.SelectedItem;
            //string sbl = Convert.ToString(selectedPair.Key);

            //DBAccess.LoadCompany(Convert.ToInt32(sbl), txtUserName.Text);

            //string connectionString = DBAccess.mUserConnection(); 
            //string a;
            //if (txtUserName.Text == "" || cboCompany.Text.ToString() == "")
            //{
            //    MessageBox.Show("Username, Password and Company Must be Entered..");
            //}
            //else
            //{
            //    SqlConnection conn = new SqlConnection(connectionString);

            //    conn.Open();
            //    SqlCommand cmd = new SqlCommand();
            //    cmd.Connection = conn;
            //    cmd.CommandText = " SELECT LoginName FROM UserProfile " +
            //        " WHERE IsActive = 1 " +
            //        " AND LoginName = '" + txtUserName.Text.Trim().ToUpper() + "'";// AND PASSWORD = '" + txtPassword.Text.Trim().ToUpper() + "'";
            //    cmd.CommandType = CommandType.Text;
            //    SqlDataReader dr = cmd.ExecuteReader();

            //    dr.Read();
            //    try
            //    {
            //        a = dr["LoginName"].ToString();

            //        if (txtUserName.Text == a)
            //        {
            //            //string sText = cboCompany.SelectedItem.ToString();
            //            // string sValue = cboCompany.SelectedValue.ToString(); 
            //            string temp = cboCompany.DisplayMember.ToString();
            //            string strId = this.cboCompany.Items[cboCompany.SelectedIndex].ToString();
            //            //int sValue = Convert.ToInt32(this.cboCompany.SelectedValue);
            //        }
            //    }
            //    catch
            //    {
            //        MessageBox.Show("Invalid UserName or Password.", "VMS", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        Application.Exit();
            //    }
            //    conn.Dispose();
            //}

            ////LoadCompany (cboCompany.ItemData(cboCompany.ListIndex));
            //// Authorise(txtUserName, txtPassword); 
            //this.Dispose();

            try
            {
                int TMPuserID;
                if (cboCompany.SelectedIndex < 0)
                {
                    MessageBox.Show("Select Financial Year", "VMS", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtUserName.Focus();

                    return;
                }

                TMPuserID = LoginManager.CheckLogin(txtUserName.Text.Trim().ToUpper(), txtPassword.Text.Trim());
                if (TMPuserID > 0)
                {
                    UserID = TMPuserID;
                    UserName = txtUserName.Text.Trim().ToUpper();
                    SelectedComID = Convert.ToInt32(cboCompany.SelectedValue);

                    return;
                }
                else
                {
                    MessageBox.Show("Invalid UserName or Password. Please Try Again.", "VMS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtUserName.Focus();

                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "VMS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLogOff_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(-1);
            System.Windows.Forms.Application.Exit();
        }
    }
}
