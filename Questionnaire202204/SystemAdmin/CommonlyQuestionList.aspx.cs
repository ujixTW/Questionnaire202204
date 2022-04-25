using Questionnaire202204.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Questionnaire202204.SystemAdmin
{
    public partial class CommonlyQuestionList : System.Web.UI.Page
    {
        private const int _pageSize = 10;
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

                //取得考題資料清單
                var questionList = CommonlyQuestionManager.GetEditCommonlyQuestionList(_pageSize, pageIndex, out totalRows);

                this.ucPageChange.PageSize = _pageSize;
                this.ucPageChange.PageIndex = pageIndex;
                this.ucPageChange.TotalRows = totalRows;
                this.ucPageChange.Bind();

                if (questionList.Count == 0)
                {
                    this.rptQusList.Visible = false;
                    this.trNoData.Visible = true;
                }
                else
                {
                    this.rptQusList.Visible = true;
                    this.trNoData.Visible = false;

                    this.rptQusList.DataSource = questionList;
                    this.rptQusList.DataBind();

                }
            }
        }

        protected void btnAdd_Click(object sender, ImageClickEventArgs e)
        {
            var id = Guid.NewGuid();
            this.Response.Redirect("CommonlyQuestionDetail.aspx?ID=" + id);
        }
    }
}