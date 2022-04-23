using Questionnaire202204.Managers;
using Questionnaire202204.Models;
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
        //Session請參考ReadMe.txt
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
                 string.Compare("UserAnswer", context.Request.QueryString["Page"], true) == 0)
            {
                UserAnswerProcessRequest(context);
            }

            //統計
            if (string.Compare("POST", context.Request.HttpMethod, true) == 0 &&
            string.Compare("Statistics", context.Request.QueryString["Page"], true) == 0)
            {

                return;
            }

        }
        /// <summary>
        /// 填寫資料頁簽
        /// </summary>
        /// <param name="context">提供內建函式的伺服器物件的參考物件</param>
        private void UserAnswerProcessRequest(HttpContext context)
        {
            //詳細作答情形-使用者資料
            if (string.Compare("UserData", context.Request.QueryString["Detail"], true) == 0)
            {
                string jsonText;
                //暫存使用者資料、問題清單、使用者回答的Model
                var UserAnswerPageData = new QuestionAndUserAnswerModel();
                var userID = Guid.Parse(context.Request.Form["userID"]);
                var questionnaireID = Guid.Parse(context.Request.Form["questionnaireID"]);

                //找出是哪筆使用者資料
                var userDataList = (List<UserDataModel>)context.Session["userDataList"];
                for (var i = 0; i < userDataList.Count; i++)
                {
                    if (userID == userDataList[i].UserID)
                    {
                        UserAnswerPageData.userData = userDataList[i];
                        i = userDataList.Count + 10;
                    }
                }
                //存入問題清單至暫存資料
                if (context.Session["questionDataList"] != null)
                {
                    //從session取值
                    UserAnswerPageData.questionList = (List<QuestionModel>)context.Session["questionDataList"];
                }
                else
                {
                    //從DB取值，並輸出
                    UserAnswerPageData.questionList = QuestionManager.GetQuestionList(questionnaireID);
                    //將資料存入session
                    context.Session["questionDataList"] = UserAnswerPageData.questionList;
                    //紀錄DB內問題清單長度
                    context.Session["DBQuestionDataListCount"] = UserAnswerPageData.questionList.Count;
                }
                //填入使用者答案至暫存資料
                UserAnswerPageData.userAnswerList = UserAnswerManager.GetUserAnswer(userID, questionnaireID);


                jsonText = Newtonsoft.Json.JsonConvert.SerializeObject(UserAnswerPageData);

                context.Response.ContentType = "application/json";
                context.Response.Write(jsonText);
                return;
            }

            //作答清單
            if (string.Compare("POST", context.Request.HttpMethod, true) == 0 &&
                 string.Compare("UserAnswer", context.Request.QueryString["Page"], true) == 0)
            {
                string jsonText;
                if (context.Session["userDataList"] != null)
                {
                    //從session取值
                    List<UserDataModel> sessionUserDataList = (List<UserDataModel>)context.Session["userDataList"];
                    //取得現在頁數
                    var nowPage = int.Parse(context.Request.Form["page"]);
                    var pageDataStart = (nowPage != 0) ? (nowPage - 1) * 10 : 0;
                    //取得資料總筆數
                    var totalDataCount = (int)context.Session["userDataListCount"];
                    //計算迴圈i最大值
                    var maxDataCount = ((totalDataCount - pageDataStart) < 9) ? totalDataCount : pageDataStart + 9;
                    List<UserDataModel> userDataList = new List<UserDataModel>();
                    for (var i = pageDataStart; i < maxDataCount; i++)
                    {
                        userDataList.Add(sessionUserDataList[i]);
                    }

                    jsonText = Newtonsoft.Json.JsonConvert.SerializeObject(userDataList);
                }
                else
                {
                    //從DB取值，並輸出
                    Guid questionnaireID = Guid.Parse(context.Request.Form["questionnaireID"]);
                    var userDataList = UserAnswerManager.GetUserDataList(questionnaireID, out int userDataListCount);
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