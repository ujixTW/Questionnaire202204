using Questionnaire202204.Managers;
using Questionnaire202204.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Questionnaire202204
{
    public partial class Form : System.Web.UI.Page
    {
        public Guid QuestionnaireID;
        public bool NotEnable;
        protected void Page_Load(object sender, EventArgs e)
        {
            //檢查QS的ID格式是否正確
            var questionnaireIDText = this.Request.QueryString["ID"];
            if (!Guid.TryParse(questionnaireIDText, out QuestionnaireID))
            {
                Response.Redirect("List.aspx");
                this.Session.Clear();
                return;
            }

            var questionnaireData = new QuestionnaireModel();
            if (this.Session["questionnaireData"] == null)
            {
                questionnaireData = QuestionnaireManager.GetQuestionnaireData(QuestionnaireID);
                this.Session["questionnaireData"] = questionnaireData;
            }
            else
            {
                questionnaireData = (QuestionnaireModel)this.Session["questionnaireData"];
                if (questionnaireData.QuestionnaireID != QuestionnaireID)
                {
                    questionnaireData = QuestionnaireManager.GetQuestionnaireData(QuestionnaireID);
                    this.Session["questionnaireData"] = questionnaireData;
                }
            }


            if (questionnaireData.StartTime > DateTime.Today || questionnaireData.EndTime < DateTime.Today || questionnaireData.IsEnable == false)
            {
                NotEnable = true;
            }
            else
            {
                NotEnable = false;
            }

        }
    }
}