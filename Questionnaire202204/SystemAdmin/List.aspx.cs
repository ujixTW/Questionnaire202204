using Questionnaire202204.Managers;
using Questionnaire202204.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Questionnaire202204.SystemAdmin
{
    public partial class List : System.Web.UI.Page
    {
        private const int _pageSize = 10;

        protected void Page_Load(object sender, EventArgs e)
        {
            int pageIndex;  //目前頁數
            if (!IsPostBack)
            {
                string keyword = this.Request.QueryString["Caption"];   //使用者輸入的關鍵字
                string startTimeText = this.Request.QueryString["StartDate"];  //使用者輸入的開始時間
                string endTimeText = this.Request.QueryString["EndDate"];      //使用者輸入的結束時間

                //確認關鍵字QS格式是否正確
                keyword = (string.IsNullOrEmpty(keyword)) ? string.Empty : keyword;
                //確認起始時間QS格式是否正確
                if (DateTime.TryParse(startTimeText, out DateTime startTime))
                {
                    startTimeText = startTime.ToString("yyyy-MM-dd");
                }
                else
                {
                    startTimeText = string.Empty;
                }
                //確認結束時間QS格式是否正確
                if (DateTime.TryParse(endTimeText, out DateTime endTime))
                {
                    endTimeText = endTime.ToString("yyyy-MM-dd");
                }
                else
                {
                    endTimeText = string.Empty;
                }

                this.txtSearchText.Text = keyword;
                this.txtStartTime.Text = startTimeText;
                this.txtEndTime.Text = endTimeText;
                List<string> keyQS = new List<string>() { "Caption", "StartDate", "EndDate" };
                List<string> keyQSValue = new List<string>() { keyword, startTimeText, endTimeText };

                //判斷當前頁數
                string pageIndexText = this.Request.QueryString["Page"];
                if (string.IsNullOrWhiteSpace(pageIndexText) || !int.TryParse(pageIndexText, out pageIndex))
                    pageIndex = 1;


                int totalRows;
                //取得考題資料清單
                var questionnaireList = QuestionnaireManager.GetQuestionnaireList(keyword, startTimeText, endTimeText, _pageSize, pageIndex, out totalRows);

                this.ucPageChange.TotalRows = totalRows;
                this.ucPageChange.PageIndex = pageIndex;
                this.ucPageChange.PageSize = _pageSize;
                this.ucPageChange.Bind(keyQS, keyQSValue);



                if (questionnaireList.Count == 0)
                {
                    this.rptQusList.Visible = false;
                    this.trNoData.Visible = true;
                }
                else
                {
                    this.rptQusList.Visible = true;
                    this.trNoData.Visible = false;

                    this.rptQusList.DataSource = questionnaireList;
                    this.rptQusList.DataBind();

                }
            }


        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string caption = string.Empty;
            string startTime = string.Empty;
            string endTime = string.Empty;
            string qs = string.Empty;
            //關鍵字
            if (!string.IsNullOrWhiteSpace(this.txtSearchText.Text))
            {
                caption = "Caption=" + Server.UrlEncode(this.txtSearchText.Text.TrimStart().TrimEnd());
            }
            //開始時間
            //如果前面有值就加上&符號
            if (DateTime.TryParse(this.txtStartTime.Text, out DateTime tempStartTime))
            {
                if (string.IsNullOrWhiteSpace(this.txtSearchText.Text))
                {
                    startTime = "StartDate=" + tempStartTime.ToString("d");
                }
                else
                {
                    startTime = "&" + "StartDate=" + tempStartTime.ToString("d");
                }
            }
            else
                this.ltlSearchErrorMsg.Visible = true;
            //結束時間
            //如果前面有值就加上&符號
            if (DateTime.TryParse(this.txtEndTime.Text, out DateTime tempEndTime))
            {
                if (string.IsNullOrWhiteSpace(this.txtSearchText.Text))
                {
                    endTime = "EndDate=" + tempEndTime.ToString("d");
                }
                else
                {
                    endTime = "&" + "EndDate=" + tempEndTime.ToString("d");
                }
            }
            else
                this.ltlSearchErrorMsg.Visible = true;

            qs = (!string.IsNullOrEmpty(caption) || !string.IsNullOrEmpty(startTime) || !string.IsNullOrEmpty(endTime)) ? "?" : "";

            this.Response.Redirect("List.aspx" + qs + caption + startTime + endTime);
        }



        protected void btnAdd_Click(object sender, ImageClickEventArgs e)
        {
            Guid newQuestionnaireID = Guid.NewGuid();
            Response.Redirect("Detail.aspx?ID=" + newQuestionnaireID + "&State=Questionnaire");
        }



    }
}