using Questionnaire202204.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Questionnaire202204
{
    public partial class ConfirmPage : System.Web.UI.Page
    {
        public Guid QuestionnaireID;
        protected void Page_Load(object sender, EventArgs e)
        {
            // 檢查QS的ID格式是否正確
            var questionnaireIDText = this.Request.QueryString["ID"];
            var questionnaireData = (QuestionnaireModel)this.Session["questionnaireData"];

            if (!Guid.TryParse(questionnaireIDText, out QuestionnaireID) || questionnaireData == null || QuestionnaireID != questionnaireData.QuestionnaireID)
            {
                Response.Redirect("List.aspx");
                this.Session.Clear();
                return;
            }



        }

    }
}