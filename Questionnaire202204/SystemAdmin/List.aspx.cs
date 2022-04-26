using Questionnaire202204.Helpers;
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
        private const int _pageSize = 5;

        protected void Page_Load(object sender, EventArgs e)
        {
            int pageIndex;  //目前頁數
            if (!IsPostBack)
            {
                int totalRows;

                //判斷當前頁數
                string pageIndexText = this.Request.QueryString["Page"];
                if (string.IsNullOrWhiteSpace(pageIndexText) || !int.TryParse(pageIndexText, out pageIndex))
                    pageIndex = 1;
                string keyword;
                string startTimeText;
                string endTimeText;

                PageSearchHelper.GetQuestionnatreSearchText(Context, out keyword, out startTimeText, out endTimeText);

                List<string> keyQS = new List<string>() { "Caption", "StartDate", "EndDate" };
                List<string> keyQSValue = new List<string>() { keyword, startTimeText, endTimeText };

                //取得考題資料清單
                var questionnaireList = QuestionnaireManager.GetQuestionnaireList(keyword, startTimeText, endTimeText, _pageSize, pageIndex, out totalRows);
                
                this.ucQuestionnaireSearchBar.Keyword = keyword;
                this.ucQuestionnaireSearchBar.StartTimeText = startTimeText;
                this.ucQuestionnaireSearchBar.EndTimeText = endTimeText;

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

        protected void btnAdd_Click(object sender, ImageClickEventArgs e)
        {
            Guid newQuestionnaireID = Guid.NewGuid();
            Response.Redirect("Detail.aspx?ID=" + newQuestionnaireID + "&State=Questionnaire");
        }



    }
}