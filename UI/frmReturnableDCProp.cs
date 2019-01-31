using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EntityObject;
using EntityObject.Enum;
using BLL;

namespace UI
{
    public partial class frmReturnableDCProp : Form
    {
        #region Private Variable(s)
        private bool flgNew;
        private bool flgLoading;

        private ReturnableDC objDC;
        private ReturnableDCItem objItemSelected;
        private ListViewColumnSorter lvwColSorter;
        private User currentUser;
        #endregion

        /// <summary>
        /// Contains Public Properties of Form that are accessible through out the Project.
        /// </summary>
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
        #endregion

        #region Constructor(s)
        public frmReturnableDCProp()
        {
            InitializeComponent();
        }

        public frmReturnableDCProp(ReturnableDC objDC, User currentUser)
        {
            this.objDC = objDC;
            this.currentUser = currentUser;
            InitializeComponent();
            InitializeListView();
        }
        #endregion

        #region Private Methods
        private void EnableDisableSave()
        {
            btnSave.Enabled = objDC.IsValid;
        }

        private void ReturnableDC_OnValid(object sender, EventArgs e)
        {
            EnableDisableSave();
        }

        private void ReturnableDC_OnInValid(object sender, EventArgs e)
        {
            EnableDisableSave();
        }

        private void SubscribeToEvents()
        {
            objDC.OnValid += new ReturnableDC.EventHandler(ReturnableDC_OnValid);
            objDC.OnInvalid += new ReturnableDC.EventHandler(ReturnableDC_OnInValid);
        }

        private void EnableDisableItemOK()
        {
            //throw new NotImplementedException();
            btnItemOkNew.Enabled = objItemSelected.IsValid;
            btnItemOk.Enabled = objItemSelected.IsValid;
        }

        private void DCItem_OnValid(object sender, EventArgs e)
        {
            EnableDisableItemOK();
        }

        private void DCItem_OnInvalid(object sender, EventArgs e)
        {
            EnableDisableItemOK();
        }

        private void SubscribeToDCItemEvents()
        {
            objItemSelected.OnValid += new ReturnableDCItem.EventHandler(DCItem_OnValid);
            objItemSelected.OnInvalid += new ReturnableDCItem.EventHandler(DCItem_OnInvalid);
        }

        private void InitializeListView()
        {
            lvwColSorter = new ListViewColumnSorter();
            lvwItems.ContextMenuStrip = conMenu;
            lvwItems.FullRowSelect = true;
            lvwItems.GridLines = true;
            lvwItems.ListViewItemSorter = lvwColSorter;
            lvwItems.MultiSelect = false;
            lvwItems.View = View.Details;
        }

        private void LoadItems()
        {
            ReturnableDCItemList objList = objDC.DCItems;

            lvwItems.Items.Clear();
            if (objList != null)
            {// && objList.Count > 0
                foreach (ReturnableDCItem objItem in objList)
                {
                    ListViewItem lvItem = new ListViewItem();
                    lvItem.Name = Convert.ToString(objItem.DBID);
                    lvItem.Text = objItem.ItemCode;
                    lvItem.SubItems.Add(objItem.ItemDescr);
                    lvItem.SubItems.Add(Convert.ToString(objItem.Qty));
                    lvItem.SubItems.Add(objItem.UnitName);

                    lvwItems.Items.Add(lvItem);
                }
            }
        }

        private void LoadItem()
        {
            flgLoading = true;
            grbList.Visible = false;
            grbProp.Visible = true;

            cboItemCode.Text = objItemSelected.ItemCode;
            txtDescr.Text = objItemSelected.ItemDescr;
            txtQty.Text = Convert.ToString(objItemSelected.Qty);
            cboUnit.Text = objItemSelected.UnitName;
            txtRemark.Text = objItemSelected.Remark;

            SubscribeToDCItemEvents();
            flgLoading = false;
        }

        private void AddItem()
        {
            if (objItemSelected.IsNew == true & objItemSelected.IsModified == false)
            {
                if (objDC.DCItems != null)
                {
                    objDC.DCItems.Add(objItemSelected);
                }
                else
                {
                    objDC.DCItems = new ReturnableDCItemList();
                    objDC.DCItems.Add(objItemSelected);
                }
            }
            else
            {
                objDC.DCItems[lvwItems.SelectedIndices[0]] = objItemSelected;
            }
        }

        private void FillcboDCType()
        {
            cboDCType.Items.Clear();
            cboDCType.Items.Add("INWARD");
            cboDCType.Items.Add("OUTWARD");
        }

        private void FillcboParty()
        {
            string[] PartyList = ReturnableDCManager.GetPartys();

            cboParty.Items.Clear();
            if (PartyList != null)
            {
                foreach (string str in PartyList)
                {
                    cboParty.Items.Add(str);
                }
            }
        }

        private void FillcboPlants()
        {
            cboPlant.Items.Clear();
            cboPlant.Items.Add("DTPL 100");
            cboPlant.Items.Add("DTPL 102");
            cboPlant.Items.Add("DTPL 103");
        }

        private void FillcboItems()
        {
            string[] ItemList = ReturnableDCItemManager.GetItems();

            cboItemCode.Items.Clear();
            if (ItemList != null)
            {
                foreach (string str in ItemList)
                {
                    cboItemCode.Items.Add(str);
                }
            }
        }

        private void FillcboUnits()
        {
            string[] objList = ReturnableDCItemManager.GetUnits();
            if (objList != null)
            {
                foreach(string str in objList)
                {
                    cboUnit.Items.Add(str);
                }
            }
        }

        private void FillcboVehicles()
        {
            string[] objList = ReturnableDCManager.GetVehicles();
            if (objList != null)
            {
                foreach (string str in objList)
                {
                    cboVehicle.Items.Add(str);
                }
            }
        }
        #endregion

        /// <summary>
        /// Contains Delegates and events available on Form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="Action"></param>
        #region Delegate and Event
        public delegate void DCUpdateHandler(object sender, DCUpdateEventArgs e, DataEventType Action);

        public event DCUpdateHandler Entry_DataChanged;
        #endregion

        #region UI Control Logic
        private void frmReturnableDCProp_Load(object sender, EventArgs e)
        {
            this.Icon = new Icon("Images/DTPL.ico");
            flgLoading = true;
            ReturnableDC_OnInValid(sender, e);

            FillcboDCType();
            FillcboParty();
            FillcboVehicles();
            FillcboPlants();

            if (objDC.IsNew)
            {
                this.Text += " [ NEW ]";
            }
            else
            {
                this.Text += " [ " + objDC.EntryNo + " ]";
                lblEntryNo.Text = objDC.EntryNo.ToString();
                txtEntryDate.Enabled = false;
                cboDCType.Enabled = false;
                cboParty.Enabled = false;
                cboVehicle.Enabled = false;
                cboPlant.Enabled = false;
                txtDCNo.Enabled = false;
                txtDCDate.Enabled = false;

                if (objDC.EntryType == "INWARD")
                {
                    txtInDate.Enabled = false;
                    dtpInTime.Enabled = false;

                    txtOutDate.Enabled = true;
                    dtpOutTime.Enabled = true;
                }
                else if (objDC.EntryType == "OUTWARD")
                {
                    txtInDate.Enabled = true;
                    dtpInTime.Enabled = true;

                    txtOutDate.Enabled = false;
                    dtpOutTime.Enabled = false;
                }
            }
            
            cboParty.Text = objDC.PartyName;
            cboVehicle.Text = objDC.VehicleNo;
            cboPlant.Text = objDC.PlantName;
            txtDCNo.Text = objDC.DCNo;
            if (objDC.DCDate != DateTime.MinValue)
                txtDCDate.Text = objDC.DCDate.ToShortDateString();
            if (objDC.VInDate != DateTime.MinValue)
                txtInDate.Text = objDC.VInDate.ToShortDateString();
            if (objDC.VOutDate != DateTime.MinValue)
                txtOutDate.Text = objDC.VOutDate.ToShortDateString();
            if (!objDC.IsNew)
            {
                if (objDC.VInTime != DateTime.MinValue)
                    dtpInTime.Value = objDC.VInTime;
                if (objDC.VOutTime != DateTime.MinValue)
                    dtpOutTime.Value = objDC.VOutTime;
            }

            grbList.Visible = true;
            grbProp.Visible = false;

            LoadItems();
            SubscribeToEvents();
            flgLoading = false;
            if (objDC.IsNew)
            {
                //objDC.EntryNo = ReturnableDCManager.GetEntryNo();
                objDC.EntryDate = DateTime.Now;

                if (objDC.EntryType == "INWARD")
                {
                    txtInDate.Text = txtEntryDate.Text;
                    txtInDate.Enabled = false;
                    dtpInTime.Value = DateTime.Now;
                    dtpInTime.Enabled = false;

                    txtOutDate.Text = "";
                    txtOutDate.Enabled = true;
                    dtpOutTime.Value = dtpOutTime.MinDate;
                    dtpOutTime.Enabled = true;
                }
                else if (objDC.EntryType == "OUTWARD")
                {
                    txtInDate.Text = "";
                    txtInDate.Enabled = true;
                    dtpInTime.Value = dtpInTime.MinDate;
                    dtpInTime.Enabled = true;

                    txtOutDate.Text = txtEntryDate.Text;
                    txtOutDate.Enabled = false;
                    dtpOutTime.Value = DateTime.Now;
                    dtpOutTime.Enabled = false;
                }
            }
            //lblEntryNo.Text = Convert.ToString(objDC.EntryNo);
            txtEntryDate.Text = objDC.EntryDate.ToShortDateString();
            cboDCType.Text = objDC.EntryType;
        }

        private void frmReturnableDCProp_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void txtEntryDate_Enter(object sender, EventArgs e)
        {
            GeneralMethods.Selection(txtEntryDate);
        }

        private void txtEntryDate_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!IsLoading)
                {
                    if (txtEntryDate.Text.Trim().Length == 10)
                        objDC.EntryDate = Convert.ToDateTime(txtEntryDate.Text.Trim());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void txtEntryDate_Leave(object sender, EventArgs e)
        {
            txtEntryDate.Text = objDC.EntryDate.ToShortDateString();
            
        }

        private void cboDCType_Enter(object sender, EventArgs e)
        {
            GeneralMethods.Selection(cboDCType);
        }

        private void cboDCType_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void cboDCType_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!IsLoading)
                {
                    objDC.EntryType = Convert.ToString(cboDCType.Text.Trim());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void cboDCType_Click(object sender, EventArgs e)
        {
            
        }

        private void cboDCType_Leave(object sender, EventArgs e)
        {
            cboDCType.Text = objDC.EntryType;
            if (cboDCType.Text == "INWARD")
            {
                txtInDate.Text = txtEntryDate.Text;
                txtInDate.Enabled = false;

                txtOutDate.Text = "";
                txtOutDate.Enabled = true;
            }
            else if (cboDCType.Text == "OUTWARD")
            {
                txtOutDate.Text = txtEntryDate.Text;
                txtOutDate.Enabled = false;

                txtInDate.Text = "";
                txtInDate.Enabled = true;
            }
        }

        private void cboParty_Enter(object sender, EventArgs e)
        {
            GeneralMethods.Selection(cboParty);
        }

        private void cboParty_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!IsLoading)
                {
                    objDC.PartyName = Convert.ToString(cboParty.Text.Trim());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void cboParty_TextChanged(object sender, EventArgs e)
        {
            cboParty_SelectedIndexChanged(sender, e);
        }

        private void cboParty_Leave(object sender, EventArgs e)
        {
            cboParty.Text = objDC.PartyName;
        }

        private void cboVehicle_Enter(object sender, EventArgs e)
        {
            GeneralMethods.Selection(cboVehicle);
        }

        private void cboVehicle_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!IsLoading)
                {
                    objDC.VehicleNo= Convert.ToString(cboVehicle.Text.Trim());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void cboVehicle_TextChanged(object sender, EventArgs e)
        {
            cboVehicle_SelectedIndexChanged(sender, e);
        }

        private void cboVehicle_Leave(object sender, EventArgs e)
        {
            cboVehicle.Text = objDC.VehicleNo;
        }

        private void cboPlant_Enter(object sender, EventArgs e)
        {
            GeneralMethods.Selection(cboPlant);
        }

        private void cboPlant_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!IsLoading)
                {
                    objDC.PlantName = Convert.ToString(cboPlant.Text.Trim());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void cboPlant_TextChanged(object sender, EventArgs e)
        {
            cboPlant_SelectedIndexChanged(sender, e);
        }

        private void txtDCNo_Enter(object sender, EventArgs e)
        {
            GeneralMethods.Selection(txtDCNo);
        }

        private void txtDCNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!IsLoading)
                {
                    objDC.DCNo= Convert.ToString(txtDCNo.Text.Trim());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void txtDCNo_Leave(object sender, EventArgs e)
        {
            txtDCNo.Text = objDC.DCNo;
        }

        private void txtDCDate_Enter(object sender, EventArgs e)
        {
            GeneralMethods.Selection(txtDCDate);
        }

        private void txtDCDate_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!IsLoading)
                {
                    if (txtDCDate.Text.Trim().Length == 10)
                        objDC.DCDate = Convert.ToDateTime(txtDCDate.Text.Trim());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void txtDCDate_Leave(object sender, EventArgs e)
        {
            if (objDC.DCDate != DateTime.MinValue)
                txtDCDate.Text = objDC.DCDate.ToShortDateString();
        }

        private void txtInDate_Enter(object sender, EventArgs e)
        {
            GeneralMethods.Selection(txtInDate);
        }

        private void txtInDate_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!IsLoading)
                {
                    if (txtInDate.Text.Trim().Length == 10)
                        objDC.VInDate = Convert.ToDateTime(txtInDate.Text.Trim());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void txtInDate_Leave(object sender, EventArgs e)
        {
            if (objDC.VInDate != DateTime.MinValue)
                txtInDate.Text = objDC.VInDate.ToShortDateString();
        }

        private void dtpInTime_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (!IsLoading)
                {
                    objDC.VInTime = Convert.ToDateTime(dtpInTime.Value);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void dtpInTime_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!IsLoading)
                {
                    objDC.VInTime = Convert.ToDateTime(dtpInTime.Value);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void txtOutDate_Enter(object sender, EventArgs e)
        {
            GeneralMethods.Selection(txtOutDate);
        }

        private void txtOutDate_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!IsLoading)
                {
                    if (txtOutDate.Text.Trim().Length == 10)
                        objDC.VOutDate = Convert.ToDateTime(txtOutDate.Text.Trim());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void txtOutDate_Leave(object sender, EventArgs e)
        {
            if (objDC.VOutDate != DateTime.MinValue)
                txtOutDate.Text = objDC.VOutDate.ToShortDateString();
        }

        private void dtpOutTime_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (!IsLoading)
                {
                    objDC.VOutTime = Convert.ToDateTime(dtpOutTime.Value);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void dtpOutTime_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!IsLoading)
                {
                    objDC.VOutTime = Convert.ToDateTime(dtpOutTime.Value);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void lvwItems_ColumnClick(object sender, ColumnClickEventArgs e)
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
            this.lvwItems.Sort();
        }

        private void lvwItems_DoubleClick(object sender, EventArgs e)
        {
            //if (lvwItems.SelectedItems != null && lvwItems.SelectedItems.Count != 0)
            //{
            //    modifyToolStripMenuItem_Click(sender, e);
            //}
        }

        private void lvwItems_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (lvwItems.SelectedItems.Count == 1 && e.KeyChar == (char)Keys.Enter)
            {
                modifyToolStripMenuItem_Click(sender, e);
            }
        }

        private void btnItemNew_Click(object sender, EventArgs e)
        {
            newToolStripMenuItem_Click(sender, e);
        }

        private void cboItemCode_Enter(object sender, EventArgs e)
        {
            GeneralMethods.Selection(cboItemCode);
        }

        private void cboItemCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!IsLoading)
                {
                    objItemSelected.ItemCode = cboItemCode.Text.Trim();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void cboItemCode_TextChanged(object sender, EventArgs e)
        {
            cboItemCode_SelectedIndexChanged(sender, e);
        }

        private void cboItemCode_Leave(object sender, EventArgs e)
        {
            cboItemCode.Text = objItemSelected.ItemCode;
        }

        private void txtDescr_Enter(object sender, EventArgs e)
        {
            GeneralMethods.Selection(txtDescr);
        }

        private void txtDescr_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!IsLoading)
                {
                    objItemSelected.ItemDescr = txtDescr.Text.Trim();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void txtDescr_Leave(object sender, EventArgs e)
        {
            txtDescr.Text = objItemSelected.ItemDescr;
        }

        private void txtQty_Enter(object sender, EventArgs e)
        {
            GeneralMethods.Selection(txtQty);
        }

        private void txtQty_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!IsLoading)
                {
                    objItemSelected.Qty = Convert.ToDecimal(txtQty.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void txtQty_Leave(object sender, EventArgs e)
        {
            txtQty.Text = Convert.ToString(objItemSelected.Qty);
        }

        private void cboUnit_Enter(object sender, EventArgs e)
        {
            GeneralMethods.Selection(cboUnit);
        }

        private void cboUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            //cboUnit_TextChanged(sender, e);
        }

        private void cboUnit_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!IsLoading)
                {
                    objItemSelected.UnitName = cboUnit.Text.Trim();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void cboUnit_Leave(object sender, EventArgs e)
        {
            cboUnit.Text = objItemSelected.UnitName;
        }

        private void txtRemark_Enter(object sender, EventArgs e)
        {
            GeneralMethods.Selection(txtRemark);
        }

        private void txtRemark_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!IsLoading)
                {
                    objItemSelected.Remark = txtRemark.Text.Trim();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void txtRemark_Leave(object sender, EventArgs e)
        {
            txtRemark.Text = objItemSelected.Remark;
        }

        private void btnItemOkNew_Click(object sender, EventArgs e)
        {
            // Add Current Item To Collection
            AddItem();
            // Add an event to set Requisition's Edited flag to true...
            objDC.IsEdited = true;
            // dispose current item
            // how to dispose this item
            // initialize New Item
            newToolStripMenuItem_Click(sender, e);
        }

        private void btnItemOk_Click(object sender, EventArgs e)
        {
            // Add Valid item to Collection
            AddItem();
            // Add an event to set Requisition's Edited flag to true...
            objDC.IsEdited = true;

            grbProp.Visible = false;
            grbList.Visible = true;
            // Load Items into List
            LoadItems();
            lvwItems.Focus();
        }

        private void btnItemCancel_Click(object sender, EventArgs e)
        {
            // Dispose current detail object

            grbProp.Visible = false;
            grbList.Visible = true;
            
            // Load Items into List.
            LoadItems();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            bool flgApplyEdit;
            try
            {
                flgApplyEdit = ReturnableDCManager.Save(objDC, currentUser);

                if (flgApplyEdit == true)
                {
                    // instance the event args and pass it value
                    DCUpdateEventArgs args = new DCUpdateEventArgs(objDC.DBID, objDC.EntryNo, objDC.EntryDate, objDC.PartyName, objDC.EntryType, objDC.DCNo, objDC.DCDate, objDC.VOutDate, objDC.VInDate);

                    // raise event wtth  updated 
                    //EmployeeUpdated(this, args);
                    if (Entry_DataChanged != null)
                    {
                        if (this.IsNew)
                        {
                            Entry_DataChanged(this, args, DataEventType.INSERT_EVENT);
                        }
                        else
                        {
                            Entry_DataChanged(this, args, DataEventType.UPDATE_EVENT);
                        }
                    }

                    this.Close();
                }
                else
                {
                    MessageBox.Show("Record not Saved.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (ApplicationException ex)
            {
                MessageBox.Show(ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
                this.Dispose();
            }
            catch (ApplicationException ex)
            {
                MessageBox.Show(ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        #endregion

        #region Context Menu
        private void conMenu_Opening(object sender, CancelEventArgs e)
        {
            if (lvwItems.SelectedItems != null && lvwItems.SelectedItems.Count != 0)
            {
                modifyToolStripMenuItem.Visible = true;
                modifyToolStripMenuItem.Enabled = false;
                newToolStripMenuItem.Enabled = false;
                toolStripSeparator1.Visible = true;
                deleteToolStripMenuItem.Visible = true;
            }
            else
            {
                modifyToolStripMenuItem.Visible = false;
                newToolStripMenuItem.Enabled = true;
                toolStripSeparator1.Visible = false;
                deleteToolStripMenuItem.Visible = false;
            }
        }

        private void modifyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lvwItems.SelectedItems != null && lvwItems.SelectedItems.Count == 1)
            {
                objItemSelected = objDC.DCItems[lvwItems.SelectedItems[0].Index];
                LoadItem();
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            objItemSelected = new ReturnableDCItem();
            DCItem_OnInvalid(sender, e);
            FillcboItems();
            FillcboUnits();
            LoadItem();
            cboItemCode.Focus();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lvwItems.SelectedItems != null & lvwItems.SelectedItems.Count != 0)
            {
                DialogResult dr = new DialogResult();
                dr = MessageBox.Show("Do You Really want to Delete Record?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (dr == DialogResult.Yes)
                {
                    objDC.DCItems[Convert.ToInt32(lvwItems.SelectedItems[0].Index)].IsDeleted = true;
                    lvwItems.SelectedItems[0].Remove();
                }
            }
        }
        #endregion

        private void txtDCDate_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (Convert.ToDateTime(txtDCDate.Text) > Convert.ToDateTime(txtEntryDate.Text))
                {
                    e.Cancel = true;
                    MessageBox.Show("DCDate can not be greater than EntryDate.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (ApplicationException ex)
            {
                MessageBox.Show(ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void txtQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (GeneralMethods.IsNumber(e))
                e.Handled = true;
        }

        private void txtInDate_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (objDC.EntryType == "OUTWARD" & txtInDate.Text.Trim() != "")
                {
                    if (Convert.ToDateTime(txtInDate.Text) < objDC.EntryDate)
                    {
                        e.Cancel = true;
                        MessageBox.Show("InDate can not be Less than EntryDate.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
            catch (ApplicationException ex)
            {
                MessageBox.Show(ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void txtOutDate_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (objDC.EntryType == "INWARD" & txtOutDate.Text.Trim() != "")
                {
                    if (Convert.ToDateTime(txtOutDate.Text) < objDC.EntryDate)
                    {
                        e.Cancel = true;
                        MessageBox.Show("InDate can not be Less than EntryDate.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
            catch (ApplicationException ex)
            {
                MessageBox.Show(ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void cboDCType_KeyPress(object sender, KeyPressEventArgs e)
        {
            GeneralMethods.AutoComplete(this.cboDCType, e, true);
        }

        private void cboParty_KeyPress(object sender, KeyPressEventArgs e)
        {
            GeneralMethods.AutoComplete(this.cboParty, e, false);
        }

        private void cboPlant_KeyPress(object sender, KeyPressEventArgs e)
        {
            GeneralMethods.AutoComplete(this.cboPlant, e, true);
        }

        private void txtEntryDate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsLetter(e.KeyChar))
                e.Handled = true;
        }

        private void txtDCDate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsLetter(e.KeyChar))
                e.Handled = true;
        }

        private void txtInDate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsLetter(e.KeyChar))
                e.Handled = true;
        }

        private void txtOutDate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsLetter(e.KeyChar))
                e.Handled = true;
        }

        private void lvwItems_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }

    public class DCUpdateEventArgs : System.EventArgs
    {
        private long mDBID;
        private long mEntryNo;
        private DateTime mEntryDate;
        private string mPartyName;
        private string mEntryType;
        private string mDCNo;
        private DateTime mDCDate;
        private DateTime mOutDate;
        private DateTime mInDate;

        public DCUpdateEventArgs(long sDBID, long sEntryNo, DateTime sEntryDate, string sPartyName, string sEntryType, string sDCNo, DateTime sDCDate, DateTime sOutDate, DateTime sInDate)
        {
            this.mDBID = sDBID;
            this.mEntryNo = sEntryNo;
            this.mEntryDate = sEntryDate;
            this.mPartyName = sPartyName;
            this.mEntryType = sEntryType;
            this.mDCNo = sDCNo;
            this.mDCDate= sDCDate;
            this.mOutDate = sOutDate;
            this.mInDate = sInDate;
        }

        public long DBID
        {
            get
            {
                return mDBID;
            }
        }

        public long EntryNo
        {
            get
            {
                return mEntryNo;
            }
        }

        public DateTime EntryDate
        {
            get
            {
                return mEntryDate;
            }
        }

        public string PartyName
        {
            get
            {
                return mPartyName;
            }
        }
        
        public string EntryType
        {
            get
            {
                return mEntryType;
            }
        }

        public string DCNo
        {
            get
            {
                return mDCNo;
            }
        }

        public DateTime DCDate
        {
            get
            {
                return mDCDate;
            }
        }

        public DateTime OutDate
        {
            get
            {
                return mOutDate;
            }
        }

        public DateTime InDate
        {
            get
            {
                return mInDate;
            }
        }
    }
}
