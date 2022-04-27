using Questionnaire202204.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Questionnaire202204
{
    public partial class Stastic : System.Web.UI.Page
    {
        public Guid QuestionnaireID;
        protected void Page_Load(object sender, EventArgs e)
        {
            // 檢查QS的ID格式是否正確
            var questionnaireIDText = this.Request.QueryString["ID"];
            if (!Guid.TryParse(questionnaireIDText, out QuestionnaireID))
            {
                Response.Redirect("List.aspx");
                this.Session.Clear();
                return;
            }


            if (this.Session["questionnaireData"] == null)
                this.Session["questionnaireData"] = QuestionnaireManager.GetQuestionnaireData(QuestionnaireID);

        }
    }
}