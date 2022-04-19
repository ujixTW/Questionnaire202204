using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Questionnaire202204.SystemAdmin
{
    public partial class Detail : System.Web.UI.Page
    {
        private Guid _questionnaireID;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string questionnaireIDText = this.Request.QueryString["ID"];
                string pageState = this.Request.QueryString["State"];
                //檢查QS上的ID是否正確
                if (!string.IsNullOrWhiteSpace(questionnaireIDText))
                {
                    //檢查QS傳遞內容是否為GUID
                    if (!Guid.TryParse(questionnaireIDText, out _questionnaireID))
                    {
                        //不符條件，送回列表頁
                        Response.Redirect("List.aspx");
                        return;
                    }
                }
                else
                {
                    //不符條件，送回列表頁
                    Response.Redirect("List.aspx");
                    return;
                }
                //檢查若QS上的State為空值則自動導向問卷畫面
                if (string.IsNullOrWhiteSpace(pageState))
                {
                    this.Response.Redirect($"Detail.aspx?ID={_questionnaireID}&State=Questionnaire");
                    return;
                }

                _PageState(pageState);


            }


        }
        /// <summary>
        /// 判斷現在使用者位在哪個功能
        /// </summary>
        /// <param name="pageState">QS裡State的值</param>
        private void _PageState(string pageState)
        {
            switch (pageState)
            {
                case "Questionnaire":
                    break;
                case "Question":
                    break;
                case "UserData":
                    break;
                case "Statistics":
                    break;
                default:
                    //狀況全不符合，自動導向問卷畫面
                    this.Response.Redirect($"Detail.aspx?ID={_questionnaireID}&State=Questionnaire");
                    return;
            }
        }


    }
}