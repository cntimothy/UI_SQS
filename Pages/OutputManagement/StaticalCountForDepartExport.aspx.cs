﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SQS.DataStructure;
using SQS.Controller;
using FineUI;
using System.Data;

namespace SQS.UI.Pages.OutputManagement
{
    public partial class StaticalCountForDepartExport : PageBase
    {
        #region Page Init
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindYearToDropDownList();
                bindGridColumnName();
            }
        }
        #endregion

        #region Event
        /// <summary>
        /// 生成结果事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Button_GetResult_Click(object sender, EventArgs e)
        {
            if (DropDownList_StartYear.SelectedValue == "0")
            {
                Alert.ShowInTop("请选择开始年份！", MessageBoxIcon.Warning);
                return;
            }
            if (DropDownList_StopYear.SelectedValue == "0")
            {
                Alert.ShowInTop("请选择结束年份！", MessageBoxIcon.Warning);
                return;
            }
            string startTime = DropDownList_StartYear.SelectedValue + "-" + DropDownList_StartMonth.SelectedValue;
            string stopTime = DropDownList_StopYear.SelectedValue + "-" + DropDownList_StopMonth.SelectedValue;

            DateTime d1 = Convert.ToDateTime(startTime);
            DateTime d2 = Convert.ToDateTime(stopTime);
            if (d1 > d2)
            {
                Alert.ShowInTop("结束时间必须大于开始时间！", MessageBoxIcon.Warning);
                return;
            }

            DataTable table = new DataTable();
            string exception = "";
            if (OutputManagementCtrl.GetCountResultForDepart(ref table, startTime, stopTime, ref exception))
            {
                Grid1.DataSource = table;
                Grid1.DataBind();
            }
            else
            {
                Alert.ShowInTop(exception);
                table.Clear();
                Grid1.DataSource = table;
                Grid1.DataBind();
            }
        }

        /// <summary>
        /// 导出事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Button_Export_Click(object sender, EventArgs e)
        {
            string exception = "";
            DataTable table = getDataTableFromGrid();
            string fileName = "";
            if (ExportManagementCtrl.ExportStaticalCountForDepart(ref fileName, table, ref exception))
            {
                Response.ClearContent();
                Response.ContentType = "application/excel";
                Response.AddHeader("content-disposition", "attachment;filename=" + Server.UrlEncode(fileName));
                string path = Server.MapPath("..\\..\\downloadfiles\\" + fileName);
                Response.TransmitFile(path);
            }
            else
            {
                Alert.ShowInTop("导出失败！\n原因：" + exception, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Private Method
        /// <summary>
        /// 绑定年份到下拉列表
        /// </summary>
        private void bindYearToDropDownList()
        {
            string exception = "";
            List<string> yearList = new List<string>();
            if (CommonCtrl.GetYearList(ref yearList, ref exception))
            {
                foreach (string year in yearList)
                {
                    DropDownList_StartYear.Items.Add(year, year);
                    DropDownList_StopYear.Items.Add(year, year);
                }
            }
        }

        /// <summary>
        /// 将Grid中的数据转化为table
        /// </summary>
        /// <returns></returns>
        private DataTable getDataTableFromGrid()
        {
            string exception = "";
            List<string> publishGradeNameListForPaper = new List<string>();
            List<string> publishGradeNameListForBook = new List<string>();
            List<string> topicGradeNameList = new List<string>();
            List<string> rewardGradeNameList = new List<string>();
            List<string> rewardClassNameList = new List<string>();
            if(!OutputManagementCtrl.GetPublishGradeNameListForPaper(ref publishGradeNameListForPaper, ref exception))
            {
                Alert.ShowInTop("获取发表级别失败！", MessageBoxIcon.Error);
                return null;
            }
            if(!OutputManagementCtrl.GetPublishGradeNameListForBook(ref publishGradeNameListForBook, ref exception))
            {
                Alert.ShowInTop("获取发行级别失败！", MessageBoxIcon.Error);
                return null;
            }
            if (!OutputManagementCtrl.GetTopicGradeNameList(ref topicGradeNameList, ref exception))
            {
                Alert.ShowInTop("获取课题级别失败！", MessageBoxIcon.Error);
                return null;
            }
            if (!OutputManagementCtrl.GetRewardGradeNameList(ref rewardGradeNameList, ref exception))
            {
                Alert.ShowInTop("获取获奖级别失败！", MessageBoxIcon.Error);
                return null;
            }
            if (!OutputManagementCtrl.GetRewardClassNameList(ref rewardClassNameList, ref exception))
            {
                Alert.ShowInTop("获取获奖等级失败！", MessageBoxIcon.Error);
                return null;
            }

            //构造table
            DataTable table = new DataTable();
            table.Columns.Add("部系");
            foreach (string name in publishGradeNameListForPaper)
            {
                table.Columns.Add("论文" + name);
            }
            foreach (string name in rewardGradeNameList)
            {
                table.Columns.Add("论文" + name);
            }
            foreach (string name in rewardClassNameList)
            {
                table.Columns.Add("论文" + name);
            }

            foreach (string name in publishGradeNameListForBook)
            {
                table.Columns.Add("著作" + name);
            }
            foreach (string name in rewardGradeNameList)
            {
                table.Columns.Add("著作" + name);
            }
            foreach (string name in rewardClassNameList)
            {
                table.Columns.Add("著作" + name);
            }

            foreach (string name in topicGradeNameList)
            {
                table.Columns.Add("课题" + name);
            }
            foreach (string name in rewardGradeNameList)
            {
                table.Columns.Add("课题" + name);
            }
            foreach (string name in rewardClassNameList)
            {
                table.Columns.Add("课题" + name);
            }
            //DataTable table = new DataTable();
            //foreach (GridColumn column in Grid1.GroupColumns[0].Columns)
            //{
            //    table.Columns.Add(column.HeaderText);
            //}
            //foreach (GridColumn column in Grid1.GroupColumns[1].Columns)
            //{
            //    table.Columns.Add(Grid1.GroupColumns[1].HeaderText + column.HeaderText);
            //}
            //foreach (GridColumn column in Grid1.GroupColumns[2].Columns)
            //{
            //    table.Columns.Add(Grid1.GroupColumns[2].HeaderText + column.HeaderText);
            //}
            //foreach (GridColumn column in Grid1.GroupColumns[3].Columns)
            //{
            //    table.Columns.Add(Grid1.GroupColumns[3].HeaderText + column.HeaderText);
            //}
            foreach (GridRow gridRow in Grid1.Rows)
            {
                DataRow dataRow = table.NewRow();
                for (int i = 0; i < gridRow.Values.Length; i++)    
                {
                    dataRow[i] = gridRow.Values[i];
                }
                table.Rows.Add(dataRow);
            }
            return table;
        }

        /// <summary>
        /// 绑定Grid标题栏
        /// </summary>
        private void bindGridColumnName()
        {
            //构造标题
            string exception = "";
            List<string> publishGradeNameListForPaper = new List<string>();
            List<string> publishGradeNameListForBook = new List<string>();
            List<string> topicGradeNameList = new List<string>();
            List<string> rewardGradeNameList = new List<string>();
            List<string> rewardClassNameList = new List<string>();
            if (!OutputManagementCtrl.GetPublishGradeNameListForPaper(ref publishGradeNameListForPaper, ref exception))
            {
                Alert.ShowInTop("获取发表级别失败！", MessageBoxIcon.Error);
                return;
            }
            if (!OutputManagementCtrl.GetPublishGradeNameListForBook(ref publishGradeNameListForBook, ref exception))
            {
                Alert.ShowInTop("获取发行级别失败！", MessageBoxIcon.Error);
                return;
            }
            if (!OutputManagementCtrl.GetTopicGradeNameList(ref topicGradeNameList, ref exception))
            {
                Alert.ShowInTop("获取课题级别失败！", MessageBoxIcon.Error);
                return;
            }
            if (!OutputManagementCtrl.GetRewardGradeNameList(ref rewardGradeNameList, ref exception))
            {
                Alert.ShowInTop("获取获奖级别失败！", MessageBoxIcon.Error);
                return;
            }
            if (!OutputManagementCtrl.GetRewardClassNameList(ref rewardClassNameList, ref exception))
            {
                Alert.ShowInTop("获取获奖等级失败！", MessageBoxIcon.Error);
                return;
            }

            List<string> headersForPaper = new List<string>();
            List<string> headersForBook = new List<string>();
            List<string> headersForTopic = new List<string>();
            headersForPaper.AddRange(publishGradeNameListForPaper);
            headersForPaper.AddRange(rewardGradeNameList);
            headersForPaper.AddRange(rewardClassNameList);
            headersForBook.AddRange(publishGradeNameListForBook);
            headersForBook.AddRange(rewardGradeNameList);
            headersForBook.AddRange(rewardClassNameList);
            headersForTopic.AddRange(topicGradeNameList);
            headersForTopic.AddRange(rewardGradeNameList);
            headersForTopic.AddRange(rewardClassNameList);

            //设置标题
            for (int i = 0; i < Grid1.GroupColumns[1].Columns.Count; i++)
            {
                Grid1.GroupColumns[1].Columns[i].HeaderText = headersForPaper[i];
            }

            for (int i = 0; i < Grid1.GroupColumns[2].Columns.Count; i++)
            {
                Grid1.GroupColumns[2].Columns[i].HeaderText = headersForBook[i];
            }

            for (int i = 0; i < Grid1.GroupColumns[3].Columns.Count; i++)
            {
                Grid1.GroupColumns[3].Columns[i].HeaderText = headersForTopic[i];
            }
        }
        #endregion
    }
}