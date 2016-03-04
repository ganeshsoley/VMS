using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BLL;
using EntityObject;

namespace UI
{
    public partial class frmGeneralList : Form
    {
        private ListViewItem[] objList;
        private string[] objHeaderList;
        #region Private Variable(s)
        bool isListActivated;
        bool isListCancelled;
        bool isMultiSelect;
        string listSource;
        bool isFromIssRetForm;
        bool isFromPurchReqForm;
        bool isFromIssueReqForm;
        bool mSplFlg;
        bool SplIssReq;
        bool isFromGinAssemblyForm;
        string callingFormName;
        //int mCnt;

        int dbid;
        string strReturn;

        ListViewColumnSorter lvwColSorter;
        #endregion

        #region Public Properties
        public string CallingForm
        {
            get
            {
                return callingFormName;
            }
            set
            {
                callingFormName = value;
            }
        }

        public bool FromGinAssemblyForm
        {
            get
            {
                return isFromGinAssemblyForm;
            }
            set
            {
                isFromGinAssemblyForm = value;
            }
        }

        public bool FromIssRetForm
        {
            get
            {
                return isFromIssRetForm;
            }
            set
            {
                isFromIssRetForm = value;
            }
        }

        public bool FromIssueReqForm
        {
            get
            {
                return isFromIssueReqForm;
            }
            set
            {
                isFromIssueReqForm = value;
            }
        }

        public bool FromPurReqForm
        {
            get
            {
                return isFromPurchReqForm;
            }
            set
            {
                isFromPurchReqForm = value;
            }
        }

        public bool ListActivated
        {
            get
            {
                return isListActivated;
            }
            set
            {
                isListActivated = value;
            }
        }
        
        public bool ListCancelled
        {
            get
            {
                return isListCancelled;
            }
            set
            {
                isListCancelled = value;
            }
        }

        public string ListCaption
        {
            set
            {
                this.Text = value;
            }
        }

        public string ListSource
        {
            get
            {
                return listSource;
            }
            set
            {
                listSource = value;
            }
        }

        public bool MultiSelect
        {
            get
            {
                return isMultiSelect;
            }
            set
            {
                isMultiSelect = value;
            }
        }
        
        public bool SplFlag
        {
            get
            {
                return mSplFlg;
            }
            set
            {
                mSplFlg = value;
            }
        }

        public bool SplIssueReq
        {
            get
            {
                return SplIssReq;
            }
            set
            {
                SplIssReq = value;
            }
        }
        #endregion

        #region Constructor(s)
        public frmGeneralList(string strQry)
        {
            InitializeComponent();
            GetColHeaderList(strQry);
            GetList(strQry);
        
            InitializeListView();
        }

        #endregion

        #region Private Method(s)
        private void GetColHeaderList(string strQry)
        {
            try
            {
                objHeaderList = null;
                objHeaderList = GeneralListManager.GetHeaderList(strQry);
            }
            catch (ApplicationException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void GetList(string strQry)
        {
            try
            {
                objList = null;
                objList = GeneralListManager.GetList(strQry);
            }
            catch (ApplicationException ex)
            {
                throw new Exception(ex.Message);
            }

        }

        private void FillList()
        {
            try
            {
                lvwGeneral.Items.Clear();
                if (objList != null)
                {
                    lvwGeneral.Items.AddRange(objList);
                }
                lvwGeneral.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

            }
            catch (ApplicationException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void SetVisibility()
        {
            if (isListActivated)
            {
                btnOk.Visible = true;
                btnCancel.Visible = true;
            }
            else
            {
                btnOk.Visible = false;
                btnCancel.Visible = false;
            }
            if (isFromIssRetForm)
                btnSetQty.Enabled = false;
        }

        private void InitializeListView()
        {
            lvwGeneral.FullRowSelect = true;
            lvwGeneral.GridLines = true;
            lvwGeneral.MultiSelect = MultiSelect;
            lvwGeneral.View = View.Details;

            for (int cnt = 1; cnt < objHeaderList.Length; cnt++)
            {
                lvwGeneral.Columns.Add(objHeaderList[cnt]);
            }

            lvwColSorter = new ListViewColumnSorter();
            this.lvwGeneral.ListViewItemSorter = lvwColSorter;
        }
        #endregion

        #region UI Control Logic
        private void frmGeneralList_Load(object sender, EventArgs e)
        {
            this.Icon = new Icon("Images/DTPL.ico");
            FillList();
            SetVisibility();
        }

        private void lvwGeneral_Click(object sender, EventArgs e)
        {
            txtQty.Text = "";
            btnSetQty.Enabled = true;
        }

        private void lvwGeneral_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            // Determine if clicked column is already the column that is being sorted.
            if (e.Column == lvwColSorter.SortColumn)
            {
                // Reverse the current sort direction for this column.
                if (lvwColSorter.Order == SortOrder.Ascending)
                {
                    lvwColSorter.Order = SortOrder.Descending;
                }
                else
                {
                    lvwColSorter.Order = SortOrder.Ascending;
                }
            }
            else
            {
                // Set the column number that is to be sorted; default to ascending.
                lvwColSorter.SortColumn = e.Column;
                lvwColSorter.Order = SortOrder.Ascending;
            }

            // Perform the sort with these new sort options.
            this.lvwGeneral.Sort();
        }

        private void lvwGeneral_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    if (ListActivated)
                    {
                        btnOk_Click(sender, e);
                    }
                    break;
            }
        }

        private void lvwGeneral_DoubleClick(object sender, EventArgs e)
        {
            if (lvwGeneral.SelectedItems[0] != null)
            {
                if (lvwGeneral.CheckBoxes == false)
                {
                    btnOk_Click(sender, e);
                }
                else
                {
                    txtQty.Text = lvwGeneral.SelectedItems[0].SubItems[3].Text;
                    GeneralMethods.Selection(txtQty);
                    btnSetQty.Enabled = true;
                }
            }
        }

        private void lvwGeneral_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (lvwGeneral.SelectedItems != null && lvwGeneral.SelectedItems.Count > 0)
            {
                ListViewItem item = lvwGeneral.SelectedItems[0];
                int i;

                switch (this.Tag.ToString())
                {
                    case "Inv":
                        if (item.Checked)
                        {
                            lblTotalQty.Text = Convert.ToString(Convert.ToDecimal(lblTotalQty.Text) + Convert.ToDecimal(item.SubItems[2].Text));
                            label3.Text = Convert.ToString(Convert.ToDecimal(label3.Text) + Convert.ToDecimal(item.SubItems[5].Text));
                        }
                        else
                        {
                            lblTotalQty.Text = Convert.ToString(Convert.ToDecimal(lblTotalQty.Text) - Convert.ToDecimal(item.SubItems[2].Text));
                            label3.Text = Convert.ToString(Convert.ToDecimal(label3.Text) + Convert.ToDecimal(item.SubItems[5].Text));
                        }
                        break;
                    case "Gin":
                        if (item.Checked)
                        {
                            for (i = 0; i < lvwGeneral.Items.Count; i++)
                            {
                                if (lvwGeneral.Items[i].Checked)
                                {
                                    if (item.Text != lvwGeneral.Items[i].Text)
                                    {
                                        item.Checked = false;
                                        MessageBox.Show("Can Not Select More Than One Item.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                        break;
                                    }
                                }
                            }
                        }
                        break;
                    case "ECWIss":
                        if (item.Checked)
                        {
                            label3.Text = Convert.ToString(Convert.ToDecimal(label3.Text) + Convert.ToDecimal(item.SubItems[4].Text));
                            for (i = 1; i <= lvwGeneral.SelectedItems.Count; i++)
                            {
                                if (lvwGeneral.SelectedItems[i].Checked)
                                {
                                    if (item.Text != lvwGeneral.SelectedItems[i].Text)
                                    {
                                        label3.Text = Convert.ToString(Convert.ToDecimal(label3.Text) - Convert.ToDecimal(item.SubItems[4].Text));
                                        item.Checked = false;
                                        MessageBox.Show("Can Not Select More Than One Item.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                        break;
                                    }
                                }
                            }
                        }
                        else
                        {
                            label3.Text = Convert.ToString(Convert.ToDecimal(label3.Text) - Convert.ToDecimal(item.SubItems[4]));
                        }
                        break;
                }
            }
            else
            {
                MessageBox.Show("First Select Item and then Click on Checkbox.");
                return;
            }
        }

        private void btnSetQty_Click(object sender, EventArgs e)
        {
            if (lvwGeneral.SelectedItems[0] != null)
            {
                if (txtQty.Text.Trim().Length != 0 || txtQty.Text != null)
                {
                    if ((!FromIssRetForm) && (!FromPurReqForm) && (!SplIssReq) && (!FromGinAssemblyForm))
                    {
                        if (Convert.ToDecimal(txtQty.Text) <= Convert.ToDecimal(lvwGeneral.SelectedItems[0].SubItems[3]) )
                        {
                            lvwGeneral.SelectedItems[0].SubItems[3].Text = txtQty.Text;
                            txtQty.Text = "";
                            btnSetQty.Enabled = false;
                        }
                    }
                    else if(FromGinAssemblyForm)
                    {
                        if (Convert.ToDecimal(txtQty.Text) <= Convert.ToDecimal(lvwGeneral.SelectedItems[0].SubItems[2]))
                        {
                            lvwGeneral.SelectedItems[0].SubItems[2].Text = txtQty.Text;
                            txtQty.Text = "";
                            btnSetQty.Enabled = false;
                        }
                        else
                        {
                            if (Convert.ToDecimal(lvwGeneral.SelectedItems[0].SubItems[9]) >0)
                            {
                                lvwGeneral.SelectedItems[0].SubItems[2].Text = txtQty.Text;
                                txtQty.Text = "";
                                btnSetQty.Enabled = false;
                            }
                            else
                            {
                                MessageBox.Show("Receipt Qty can not be greater than Balance Qty.");
                            }
                        }
                    }
                    else if(FromIssRetForm)
                    {
                        if(Convert.ToDecimal(txtQty.Text) <= Convert.ToDecimal(lvwGeneral.SelectedItems[0].SubItems[2]))
                        {
                            lvwGeneral.SelectedItems[0].SubItems[2].Text = txtQty.Text;
                            txtQty.Text = "";
                            btnSetQty.Enabled = false;
                        }
                        else
                        {
                            MessageBox.Show("Receipt Qty can not be greater than Balance Qty.");
                        }
                    }
                    else if(FromPurReqForm)
                    {

                    }
                    else if(SplIssReq)
                    {
                        if(Convert.ToDecimal(txtQty.Text) <= Convert.ToDecimal(lvwGeneral.SelectedItems[0].SubItems[3]))
                        {
                            lvwGeneral.SelectedItems[0].SubItems[3].Text = txtQty.Text;
                            txtQty.Text = "";
                            btnSetQty.Enabled = false;
                        }
                        else
                        {
                            MessageBox.Show("Receipt Qty can not be greater than Balance Qty.");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please Enter Quantity !");
                }
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            int intCounter;
            ListViewItem mItem;
            string mItemCode = string.Empty;
            strReturn = string.Empty;

            try
            {
                if (lvwGeneral.SelectedItems != null && lvwGeneral.SelectedItems.Count >0)
                {
                    if (MultiSelect)
                    {
                        for (intCounter = 1; intCounter == lvwGeneral.Items.Count; intCounter++)
                        {
                            mItem = lvwGeneral.Items[intCounter];
                            if (mItem.Selected)
                                strReturn = strReturn + "'" + mItem.Text + "', ";
                        }
                        strReturn = strReturn.Substring(0, (strReturn.Length - 1));
                    }
                    else
                    {
                        dbid = Convert.ToInt32(lvwGeneral.SelectedItems[0].Name);
                        strReturn = Convert.ToString(lvwGeneral.SelectedItems[0].Text);
                    }
                    isListCancelled = false;
                    FromIssRetForm = false;
                    SplFlag = false;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            isListCancelled = true;
            FromIssRetForm = false;
            SplFlag = false;
            this.Close();
        }
        #endregion
    }
}
