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
                string startTime = this.Request.QueryString["Start"];  //使用者輸入的開始時間
                string endTime = this.Request.QueryString["End"];      //使用者輸入的結束時間
                this.txtSearchText.Text = keyword;
                this.txtStartTime.Text = startTime;
                this.txtEndTime.Text = endTime;
                List<string> keyQS = new List<string>() { "Caption", "Start", "End" };
                List<string> keyQSValue = new List<string>() { keyword, startTime, endTime };

                //判斷當前頁數
                string pageIndexText = this.Request.QueryString["Page"];
                if (string.IsNullOrWhiteSpace(pageIndexText) || !int.TryParse(pageIndexText, out pageIndex))
                    pageIndex = 1;
                else
                    pageIndex = Convert.ToInt32(pageIndexText);

                int totalRows;
                //取得考題資料清單
                var questionnaireList = QuestionnaireManager.GetQuestionnaireList(keyword, startTime, endTime, _pageSize, pageIndex, out totalRows);

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
            if (!string.IsNullOrWhiteSpace(this.txtSearchText.Text))
            {
                caption = "Caption=" + this.txtSearchText.Text.Trim();
                qs = "?";
            }
            if (!string.IsNullOrWhiteSpace(this.txtStartTime.Text))
            {
                startTime = this.txtStartTime.Text;
                qs = "?";
            }
            if (!string.IsNullOrWhiteSpace(this.txtEndTime.Text))
            {
                endTime = this.txtEndTime.Text;
                qs = "?";
            }

            this.Response.Redirect("List.aspx" + qs + caption + startTime + endTime);
        }

        protected void btnDelete_Click(object sender, ImageClickEventArgs e)
        {
            this.divDeleteMsg.Visible = true;
        }

        protected void btnAdd_Click(object sender, ImageClickEventArgs e)
        {
            Guid newQuestionnaireID = Guid.NewGuid();
            Response.Redirect("Detail.aspx?ID=" + newQuestionnaireID + "&State=1");
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (sender == this.btnDeleteYes)
            {

            }
            else if (sender == this.btnDeleteNo)
            {
                this.divDeleteMsg.Visible = false;
            }
        }
    }
}