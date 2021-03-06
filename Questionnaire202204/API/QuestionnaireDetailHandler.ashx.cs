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
            
            //問題
            if (string.Compare("POST", context.Request.HttpMethod, true) == 0 &&
                 string.Compare("Question", context.Request.QueryString["Page"], true) == 0)
            {
                QuestionDataListToSession(context);

                string jsonText;

                //從session取值
                var questionDataList = context.Session["questionDataList"];
                jsonText = Newtonsoft.Json.JsonConvert.SerializeObject(questionDataList);


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
                QuestionDataListToSession(context);

                string jsonText;
                var questionnaireID = Guid.Parse(context.Request.Form["questionnaireID"]);

                var userAnswerStatistics = UserAnswerManager.GetAnswerStatisticsList(questionnaireID,out bool isAnswered);
                var answerStatisticsPageData = new QuestionAndAnswerStatisticsModel()
                {
                    IsAnswered=isAnswered,
                    questionList = (List<QuestionModel>)context.Session["questionDataList"],
                    answerStatisticsList = userAnswerStatistics
                };
                jsonText = Newtonsoft.Json.JsonConvert.SerializeObject(answerStatisticsPageData);

                context.Response.ContentType = "application/json";
                context.Response.Write(jsonText);
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
                QuestionDataListToSession(context);

                string jsonText;
                //暫存使用者資料、問題清單、使用者回答的Model
                var userAnswerPageData = new QuestionAndUserAnswerModel();
                var userID = Guid.Parse(context.Request.Form["userID"]);
                var questionnaireID = Guid.Parse(context.Request.Form["questionnaireID"]);

                //找出是哪筆使用者資料
                var userDataList = (List<UserDataModel>)context.Session["userDataList"];
                for (var i = 0; i < userDataList.Count; i++)
                {
                    if (userID == userDataList[i].UserID)
                    {
                        userAnswerPageData.userData = userDataList[i];
                        i = userDataList.Count + 10;
                    }
                }
                userAnswerPageData.questionList = (List<QuestionModel>)context.Session["questionDataList"];
                //填入使用者答案至暫存資料
                userAnswerPageData.userAnswerList = UserAnswerManager.GetUserAnswer(userID, questionnaireID);


                jsonText = Newtonsoft.Json.JsonConvert.SerializeObject(userAnswerPageData);

                context.Response.ContentType = "application/json";
                context.Response.Write(jsonText);
                return;
            }

            //作答清單
            if (string.Compare("POST", context.Request.HttpMethod, true) == 0 &&
                 string.Compare("UserAnswer", context.Request.QueryString["Page"], true) == 0)
            {
                string jsonText;
                if (context.Session["userDataList"] == null)
                {
                    //從DB取值，並輸出
                    Guid questionnaireID = Guid.Parse(context.Request.Form["questionnaireID"]);
                    var userSessionDataList = UserAnswerManager.GetUserDataList(questionnaireID, out int userDataListCount);
                    //將資料存入session
                    context.Session["userDataList"] = userSessionDataList;
                    //紀錄資料總數
                    context.Session["userDataListCount"] = userDataListCount;
                }

                //從session取值
                List<UserDataModel> sessionUserDataList = (List<UserDataModel>)context.Session["userDataList"];
                //取得現在頁數
                var nowPage = int.Parse(context.Request.Form["page"]);
                var pageSize = int.Parse(context.Request.Form["pageSize"]);
                var pageDataStart = (nowPage != 0) ? (nowPage - 1) * pageSize : 0;
                //取得資料總筆數
                var totalDataCount = (int)context.Session["userDataListCount"];
                //計算迴圈i最大值
                var maxDataCount = ((totalDataCount - pageDataStart) < pageSize - 1) ? totalDataCount : pageDataStart + pageSize - 1;
                List<UserDataModel> userDataList = new List<UserDataModel>();
                for (var i = pageDataStart; i < maxDataCount; i++)
                {
                    userDataList.Add(sessionUserDataList[i]);
                }

                jsonText = Newtonsoft.Json.JsonConvert.SerializeObject(userDataList);


                context.Response.ContentType = "application/json";
                context.Response.Write(jsonText);
                return;
            }

        }

        /// <summary>
        /// 若session中沒有question資料，將資料取出並存入session
        /// </summary>
        /// <param name="context">提供內建函式的伺服器物件的參考物件</param>
        private void QuestionDataListToSession(HttpContext context)
        {
            //存入問題清單至暫存資料，供問題、填寫資料、統計頁簽使用
            if (context.Session["questionDataList"] == null)
            {
                var questionnaireID = Guid.Parse(context.Request.Form["questionnaireID"]);
                //從DB取值，並輸出
                var questionList = QuestionManager.GetQuestionList(questionnaireID);
                //將資料存入session
                context.Session["questionDataList"] = questionList;
                //紀錄DB內問題清單長度
                context.Session["DBQuestionDataListCount"] = questionList.Count;
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