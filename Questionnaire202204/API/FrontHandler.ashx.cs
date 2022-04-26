using Questionnaire202204.Managers;
using Questionnaire202204.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Questionnaire202204.API
{
    /// <summary>
    /// FrontHandler 的摘要描述
    /// </summary>
    public class FrontHandler : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            if (string.Compare("POST", context.Request.HttpMethod, true) == 0 &&
                string.Compare("List", context.Request.QueryString["Page"], true) == 0)
            {
                string jsonText;
                var questionnaireList = (List<QuestionnaireModel>)context.Session["questionnaireDataList"];

                jsonText = Newtonsoft.Json.JsonConvert.SerializeObject(questionnaireList);

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