using Questionnaire202204.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Questionnaire202204.API
{
    /// <summary>
    /// QuestionnaireDetailHandler 的摘要描述
    /// </summary>
    public class QuestionnaireDetailHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            if (string.Compare("POST", context.Request.HttpMethod, true) == 0 &&
                 string.Compare("Questionnaire", context.Request.QueryString["Page"], true) == 0)
            {
                Guid questionnaireID = Guid.Parse(context.Request.Form["questionnaireID"]);
                var questionnaireData = QuestionnaireManager.GetQuestionnaireData(questionnaireID);
                string jsonText = Newtonsoft.Json.JsonConvert.SerializeObject(questionnaireData);

                context.Response.ContentType = "application/json";
                context.Response.Write(jsonText);
                return;
            }
            if (string.Compare("POST", context.Request.HttpMethod, true) == 0 &&
                 string.Compare("Question", context.Request.QueryString["Page"], true) == 0)
            {
                Guid questionnaireID = Guid.Parse(context.Request.Form["questionnaireID"]);
                var questionDataList = QuestionManager.GetQuestionList(questionnaireID);
                string jsonText = Newtonsoft.Json.JsonConvert.SerializeObject(questionDataList);

                context.Response.ContentType = "application/json";
                context.Response.Write(jsonText);
                return;
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}