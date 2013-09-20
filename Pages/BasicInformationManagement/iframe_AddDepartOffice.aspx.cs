using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SQS.Controller;
using FineUI;

namespace SQS.UI.Pages
{
    public partial class iframe_AddDepartOffice : PageBase
    {
        #region Page Init
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindDepartToDropDownList();
                Button_Cancel.OnClientClick = ActiveWindow.GetHideReference();
            }
        }
        #endregion

        #region Event
        /// <summary>
        /// 选择新增类型事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void RadioButton_AddType_CheckedChanged(object sender, EventArgs e)
        {
            if (RadioButton_SelectDepart.Checked == true)
            {
                DropDownList_Depart.Enabled = true;
                TextBox_Depart.Enabled = false;
            }
            else
            {
                DropDownList_Depart.Enabled = false;
                TextBox_Depart.Enabled = true;
            }
        }

        /// <summary>
        /// 新增按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Button_AddNew_Click(object sender, EventArgs e)
        {
            string officeStr = TextArea_Office.Text.Trim();
            if (officeStr == "")
            {
                Alert.ShowInTop("请输入处室名称！", MessageBoxIcon.Warning);
                return;
            }
            List<string> officeList = new List<string>();
            officeStr = officeStr.Replace(";", "；");    //用英文的分号替代中文的分号
            string[] officeArray = officeStr.Split('；');
            foreach (string office in officeArray)      //去除掉空项
            {
                if (office != "")
                {
                    officeList.Add(office);
                }
            }
            
            //选择部系方式
            if (RadioButton_SelectDepart.Checked == true)
            {
                string exception = "";
                string departId = DropDownList_Depart.SelectedValue;
                if(BasicInformationManagementCtrl.AddOfficeToDepart(officeList, departId, ref exception))
                {
                    Alert.ShowInTop("新增成功！", MessageBoxIcon.Information);
                    PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
                }
            }
            //新增部系方式
            else
            { 
                string exception = "";
                string depart = TextBox_Depart.Text.Trim();
                if(depart == "")
                {
                    Alert.ShowInTop("请输入部系名称");
                    return;
                }
                if (BasicInformationManagementCtrl.AddDepartOffice(depart, officeList, ref exception))
                {
                    Alert.ShowInTop("新增成功！", MessageBoxIcon.Information);
                    PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
                }
            }
        }
        #endregion

        #region Private Method
        private void bindDepartToDropDownList()
        {
            string exception = "";
            Dictionary<string, string> idNameDic = new Dictionary<string, string>();
            DropDownList_Depart.Items.Clear();
            if(CommonCtrl.GetDepartIdNameDic(ref idNameDic, ref exception))
            {
                foreach (string id in idNameDic.Keys)
                {
                    DropDownList_Depart.Items.Add(idNameDic[id], id);
                }
            }
        }
        #endregion
    }
}