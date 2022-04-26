using Questionnaire202204.Helpers;
using Questionnaire202204.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Questionnaire202204
{
    public partial class List : System.Web.UI.Page
    {
        /// <summary>
        /// 該頁資料最大筆數
        /// </summary>
        private const int _pageSize = 4;
        
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                int totalRows;
                int pageIndex;
                string keyword;
                string startTimeText;
                string endTimeText;

                //判斷當前頁數
                string pageIndexText = this.Request.QueryString["Page"];
                if (string.IsNullOrWhiteSpace(pageIndexText) || !int.TryParse(pageIndexText, out pageIndex))
                    pageIndex = 1;


                PageSearchHelper.GetQuestionnatreSearchText(Context, out keyword, out startTimeText, out endTimeText);

                List<string> keyQS = new List<string>() { "Caption", "StartDate", "EndDate" };
                List<string> keyQSValue = new List<string>() { keyword, startTimeText, endTimeText };

                //取得考題資料清單
                var questionnaireList = QuestionnaireManager.GetFormQuestionnaireList(keyword, startTimeText, endTimeText, _pageSize, pageIndex, out totalRows);
                this.Session["questionnaireDataList"] = questionnaireList;

                this.ucQuestionnaireSearchBar.Keyword = keyword;
                this.ucQuestionnaireSearchBar.StartTimeText = startTimeText;
                this.ucQuestionnaireSearchBar.EndTimeText = endTimeText;

                this.ucPageChange.TotalRows = totalRows;
                this.ucPageChange.PageIndex = pageIndex;
                this.ucPageChange.PageSize = _pageSize;
                this.ucPageChange.Bind(keyQS, keyQSValue);

            }

        }

    }
}