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
    public class QuestionnaireDetailHandler : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {
        //存有session
        //questionnaireData 問卷頁面資料
        //questionDataList 問題頁面清單資料
        //commonlyQuestionList 常用問題資料清單
        //DBQuestionDataListCount 紀錄DB內問題清單長度
        //userDataList 單一問卷填寫紀錄
        //userDataListCount 單一問卷填寫者數量

        public void ProcessRequest(HttpContext context)
        {
            //問卷
            if (string.Compare("POST", context.Request.HttpMethod, true) == 0 &&
                 string.Compare("Questionnaire", context.Request.QueryString["Page"], true) == 0)
            {
                string jsonText;
                if (context.Session["questionnaireData"] != null)
                {
                    //從session取值
                    var questionnaireData = context.Session["questionnaireData"];
                    jsonText = Newtonsoft.Json.JsonConvert.SerializeObject(questionnaireData);
                }
                else
                {
                    //從DB取值，並輸出
                    Guid questionnaireID = Guid.Parse(context.Request.Form["questionnaireID"]);
                    var questionnaireData = QuestionnaireManager.GetQuestionnaireData(questionnaireID);
                    jsonText = Newtonsoft.Json.JsonConvert.SerializeObject(questionnaireData);
                    //將資料存入session
                    context.Session["questionnaireData"] = questionnaireData;
                }


                context.Response.ContentType = "application/json";
                context.Response.Write(jsonText);
                return;
            }

            //問題
            if (string.Compare("POST", context.Request.HttpMethod, true) == 0 &&
                 string.Compare("Question", context.Request.QueryString["Page"], true) == 0)
            {
                string jsonText;
                if (context.Session["questionDataList"] != null)
                {
                    //從session取值
                    var questionDataList = context.Session["questionDataList"];
                    jsonText = Newtonsoft.Json.JsonConvert.SerializeObject(questionDataList);
                }
                else
                {
                    //從DB取值，並輸出
                    Guid questionnaireID = Guid.Parse(context.Request.Form["questionnaireID"]);
                    var questionDataList = QuestionManager.GetQuestionList(questionnaireID);
                    jsonText = Newtonsoft.Json.JsonConvert.SerializeObject(questionDataList);
                    //將資料存入session
                    context.Session["questionDataList"] = questionDataList;
                    //紀錄DB內問題清單長度
                    context.Session["DBQuestionDataListCount"] = questionDataList.Count;
                }


                context.Response.ContentType = "application/json";
                context.Response.Write(jsonText);
                return;
            }

            //填寫資料
            if (string.Compare("POST", context.Request.HttpMethod, true) == 0 &&
                 string.Compare("UserData", context.Request.QueryString["Page"], true) == 0)
            {
                string jsonText;
                if (context.Session["questionDataList"] != null)
                {
                    //從session取值
                    var userDataList = context.Session["userDataList"];

                    jsonText = Newtonsoft.Json.JsonConvert.SerializeObject(userDataList);
                }
                else
                {
                    //從DB取值，並輸出
                    Guid questionnaireID = Guid.Parse(context.Request.Form["questionnaireID"]);
                    var userDataList = UserAnswerManager.GetUserDataList(questionnaireID,out int userDataListCount);
                    jsonText = Newtonsoft.Json.JsonConvert.SerializeObject(userDataList);
                    //將資料存入session
                    context.Session["userDataList"] = userDataList;
                    //紀錄資料總數
                    context.Session["userDataListCount"] = userDataListCount;
                }


                context.Response.ContentType = "application/json";
                context.Response.Write(jsonText);
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