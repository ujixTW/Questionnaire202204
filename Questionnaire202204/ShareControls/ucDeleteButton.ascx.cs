using Questionnaire202204.Managers;
using Questionnaire202204.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Questionnaire202204.ShareControls
{
    public partial class ucDeleteButton : System.Web.UI.UserControl
    {
        private static List<Guid> _guidIDList = new List<Guid>();
        private static List<string> _stringIDList = new List<string>();
        private static string[] _UrlArray = new string[] { "List.aspx", "Detail.aspx", "CommonlyQuestionList.aspx" };
        //當前頁數，對應_UrlArray位置的值
        private static int _page = -1;

        /// <summary>
        /// 判斷現在為哪個頁面
        /// </summary>
        /// <param name="page">現在的頁面</param>
        private void _PageCheck(out int page)
        {
            var url = System.IO.Path.GetFileName(this.Request.PhysicalPath);
            for (var i = 0; i < _UrlArray.Length; i++)
            {
                if (url.Equals(_UrlArray[i]))
                {
                    page = i;
                    return;
                }
            }
            page = -1;
        }

        /// <summary>
        /// 開啟刪除確認視窗，存入待刪除資料
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDelete_Click(object sender, ImageClickEventArgs e)
        {
            _PageCheck(out _page);

            switch (_page)
            {
                case 0:
                    _SaveTempGuidIDList();
                    break;
                case 1:
                    _SaveTempStringIDList();
                    break;
                case 2:
                    _SaveTempGuidIDList();
                    break;
            }
            //開啟警告畫面
            this.divDeleteMsg.Visible = true;
        }

        /// <summary>
        /// 後台問卷清單
        /// </summary>
        private void _SaveTempGuidIDList()
        {
            //重置選取的ID資料
            _guidIDList.Clear();
            //讀取被選取的checkbox的value
            var guidListValue = Request["checkboxQus"];

            if (guidListValue != null)
            {
                var guidIDs = guidListValue.ToString().Split(',');
                foreach (var item in guidIDs)
                {
                    _guidIDList.Add(Guid.Parse(item));
                }
            }
        }
        /// <summary>
        /// 後台選項清單
        /// </summary>
        private void _SaveTempStringIDList()
        {
            //重置選取的ID資料
            _stringIDList.Clear();
            //讀取被選取的checkbox的value
            var question = Request["checkboxQus"];

            if (question != null)
            {
                var questionIDs = question.ToString().Split(',');
                foreach (var item in questionIDs)
                {
                    _stringIDList.Add(item);
                }
            }
        }

        /// <summary>
        /// 刪除資料或取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (sender == this.btnDeleteYes)
            {
                switch (_page)
                {
                    case 0:
                        _DeleteAdminQuestionnaireList();
                        break;
                    case 1:
                        _DeleteAdminQuestionList();
                        break;
                    case 2:
                        _DeleteAdminCommonlyQuestionList();
                        break;
                }
                this.divDeleteMsg.Visible = false;
                Response.Redirect(Request.RawUrl);
            }
            else if (sender == this.btnDeleteNo)
            {
                this.divDeleteMsg.Visible = false;
            }
        }
        /// <summary>
        /// 刪除多筆問卷資料
        /// </summary>
        private void _DeleteAdminQuestionnaireList()
        {
            if (_guidIDList.Count != 0)
            {
                QuestionnaireManager.DeleteQuestionnaireList(_guidIDList);
            }
        }

        /// <summary>
        /// 刪除多筆問題資料
        /// </summary>
        private void _DeleteAdminQuestionList()
        {
            var questionnaireID = this.Request.QueryString["ID"];
            if (_stringIDList.Count != 0)
            {
                List<QuestionModel> modelList = (List<QuestionModel>)this.Session["questionDataList"];
                for (var i = 0; i < _stringIDList.Count; i++)
                {
                    for (var j = 0; j < modelList.Count; j++)
                    {
                        if (_stringIDList[i] == modelList[j].QuestionID)
                        {
                            modelList.RemoveAt(j);
                            j -= 1;
                            break;
                        }
                    }
                }
                for (var i = 0; i < modelList.Count; i++)
                {
                    modelList[i].NO = i + 1;
                }
                this.Session["questionDataList"] = modelList;

            }
            //重新整理以更新畫面
            Response.Redirect(Request.Path + $"?ID={questionnaireID}&State=Question");
        }

        /// <summary>
        /// 刪除多筆常用問題資料
        /// </summary>
        private void _DeleteAdminCommonlyQuestionList()
        {
            if (_guidIDList.Count != 0)
            {
                CommonlyQuestionManager.DeleteCommonlyQuestion(_guidIDList);
            }
        }

    }
}