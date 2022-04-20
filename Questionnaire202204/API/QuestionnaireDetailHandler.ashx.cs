using Questionnaire202204.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Questionnaire202204.API
{
    /// <summary>
    /// 提供資料給SystemAdmin.Detail.ashx的AJAX使用
    /// </summary>
    public class QuestionnaireDetailHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            //問卷
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
            //問題
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
            //填寫資料
            if (string.Compare("POST", context.Request.HttpMethod, true) == 0 &&
                 string.Compare("UserData", context.Request.QueryString["Page"], true) == 0)
            {
                
                return;
            }
            //統計
            if (string.Compare("POST", context.Request.HttpMethod, true) == 0 &&
                string.Compare("Statistics", context.Request.QueryString["Page"], true) == 0)
            {
               
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