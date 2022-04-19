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

        private static List<Guid> _questionnaireIDList = new List<Guid>();
        protected void Page_Load(object sender, EventArgs e)
        {
            int pageIndex;  //目前頁數
            if (!IsPostBack)
            {
                string keyword = this.Request.QueryString["Caption"];   //使用者輸入的關鍵字
                string startTime = this.Request.QueryString["StartDate"];  //使用者輸入的開始時間
                string endTime = this.Request.QueryString["EndDate"];      //使用者輸入的結束時間
                this.txtSearchText.Text = keyword;
                this.txtStartTime.Text = startTime;
                this.txtEndTime.Text = endTime;
                List<string> keyQS = new List<string>() { "Caption", "StartDate", "EndDate" };
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
            //關鍵字
            if (!string.IsNullOrWhiteSpace(this.txtSearchText.Text))
            {
                caption = "Caption=" + this.txtSearchText.Text.Trim();
                qs = "?";
            }
            //開始時間
            //如果前面有值就加上&符號
            if (!string.IsNullOrWhiteSpace(this.txtStartTime.Text) && string.IsNullOrWhiteSpace(this.txtSearchText.Text))
            {
                startTime = "StartDate=" + this.txtStartTime.Text.Trim();
                qs = "?";
            }
            else if (!string.IsNullOrWhiteSpace(this.txtStartTime.Text) && !string.IsNullOrWhiteSpace(this.txtSearchText.Text))
            {
                startTime = "&" + "StartDate=" + this.txtStartTime.Text.Trim();
                qs = "?";
            }
            //結束時間
            //如果前面有值就加上&符號
            if (!string.IsNullOrWhiteSpace(this.txtEndTime.Text) && string.IsNullOrWhiteSpace(this.txtStartTime.Text))
            {
                endTime = "EndDate=" + this.txtEndTime.Text.Trim();
                qs = "?";
            }
            else if (!string.IsNullOrWhiteSpace(this.txtEndTime.Text) && !string.IsNullOrWhiteSpace(this.txtStartTime.Text))
            {
                endTime = "&" + "EndDate=" + this.txtEndTime.Text.Trim();
                qs = "?";
            }

            this.Response.Redirect("List.aspx" + qs + caption + startTime + endTime);
        }

        protected void btnDelete_Click(object sender, ImageClickEventArgs e)
        {
            //重置選取的ID資料
            _questionnaireIDList.Clear();
            //讀取被選取的checkbox的value
            var questionnaire = Request["checkboxQus"];

            if (questionnaire != null)
            {
                var questionnaireIDs = questionnaire.ToString().Split(',');
                foreach (var item in questionnaireIDs)
                {
                    _questionnaireIDList.Add(Guid.Parse(item));
                }
            }
            //開啟警告畫面
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
                if (_questionnaireIDList.Count != 0)
                {
                    QuestionnaireManager.DeleteQuestionnaireList(_questionnaireIDList);
                }
                this.divDeleteMsg.Visible = false;
                Response.Redirect(Request.RawUrl);
            }
            else if (sender == this.btnDeleteNo)
            {
                this.divDeleteMsg.Visible = false;
            }
        }
    }
}