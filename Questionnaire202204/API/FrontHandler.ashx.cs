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
            //前台列表頁
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
            //前台內頁
            if (string.Compare("POST", context.Request.HttpMethod, true) == 0 &&
                string.Compare("Form", context.Request.QueryString["Page"], true) == 0)
            {
                Guid questionnaireID = Guid.Empty;
                if (context.Request.Form["questionnaireID"] != null)
                    questionnaireID = Guid.Parse(context.Request.Form["questionnaireID"]);

                FormPageProcessRequest(context, questionnaireID);
            }

        }
        /// <summary>
        /// 前台內頁API
        /// </summary>
        /// <param name="context"></param>
        private void FormPageProcessRequest(HttpContext context, Guid questionnaireID)
        {
            //初次進入頁面
            if (string.Compare("Start", context.Request.QueryString["Action"], true) == 0)
            {
                if (context.Session["userData"] != null)
                {
                    //如果有作答紀錄則為返回問卷內頁

                    string jsonText;
                    var questionList = (List<QuestionModel>)context.Session["questionDataList"];
                    var userAnsList = (List<UserAnswerModel>)context.Session["userAnswerList"];
                    var userData = (UserDataModel)context.Session["userData"];

                    QuestionAndUserAnswerModel model = new QuestionAndUserAnswerModel();
                    model.questionList = questionList;
                    model.userAnswerList = userAnsList;
                    model.userData = userData;

                    jsonText = Newtonsoft.Json.JsonConvert.SerializeObject(model);

                    context.Response.ContentType = "application/json";
                    context.Response.Write(jsonText);
                    return;
                }
                else
                {
                    //如果無作答紀錄則為初次進入問卷內頁

                    string jsonText;
                    var questionList = QuestionManager.GetQuestionList(questionnaireID);
                    context.Session["questionDataList"] = questionList;

                    QuestionAndUserAnswerModel model = new QuestionAndUserAnswerModel();
                    model.questionList = questionList;

                    jsonText = Newtonsoft.Json.JsonConvert.SerializeObject(model);

                    context.Response.ContentType = "application/json";
                    context.Response.Write(jsonText);
                    return;
                }

            }
            if (string.Compare("Send", context.Request.QueryString["Action"], true) == 0)
            {
                var formList = context.Request.Form;
                //formModel資料長度，扣除前5筆(如userData所見)，後面各3筆資料為一個model，故除以3
                var formDataCount = (formList.Count - 5) / 3;
                var userAnsList = new List<UserAnswerModel>();
                var userData = new UserDataModel()
                {
                    QuestionnaireID = questionnaireID,
                    Name = formList["Name"],
                    Mobile = formList["Mobile"],
                    Email = formList["Email"],
                    Age = int.Parse(formList["Age"])
                };
                for (var i = 0; i < formDataCount; i++)
                {
                    UserAnswerModel model = new UserAnswerModel()
                    {
                        QuestionnaireID = questionnaireID,
                        QuestionID = formList[$"userInputAnswer[{i}][QuestionID]"],
                        OptionNO = int.Parse(formList[$"userInputAnswer[{i}][OptionNO]"]),
                        Answer = formList[$"userInputAnswer[{i}][Answer]"]
                    };
                    userAnsList.Add(model);
                }
                context.Session["userData"] = userData;
                context.Session["userAnswerList"] = userAnsList;

                return;

            }

            if (string.Compare("Cancel", context.Request.QueryString["Action"], true) == 0)
            {
                context.Session.Clear();
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