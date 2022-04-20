using Questionnaire202204.Managers;
using Questionnaire202204.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Questionnaire202204.SystemAdmin
{
    public partial class Detail : System.Web.UI.Page
    {
        public Guid questionnaireID;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                string questionnaireIDText = this.Request.QueryString["ID"];
                string pageState = this.Request.QueryString["State"];
                //檢查QS上的ID是否正確
                if (!string.IsNullOrWhiteSpace(questionnaireIDText))
                {
                    //檢查QS傳遞內容是否為GUID
                    if (!Guid.TryParse(questionnaireIDText, out questionnaireID))
                    {
                        //不符條件，送回列表頁
                        Response.Redirect("List.aspx");
                        return;
                    }
                }
                else
                {
                    //不符條件，送回列表頁
                    Response.Redirect("List.aspx");
                    return;
                }

                ////檢查若QS上的State為空值則自動導向問卷畫面
                //if (string.IsNullOrWhiteSpace(pageState))
                //{
                //    this.Response.Redirect($"Detail.aspx?ID={questionnaireID}&State=Questionnaire");
                //    return;
                //}

                //_PageState(pageState);


            }


        }



        /// <summary>
        /// 判斷並導向現在使用者位在哪個功能
        /// </summary>
        /// <param name="pageState">QS裡State的值</param>
        private void _PageState(string pageState)
        {
            switch (pageState)
            {
                case "Questionnaire":
                    _PageQuestionnaire();
                    break;
                case "Question":
                    _PageQuestion();
                    break;
                case "UserData":
                    break;
                case "Statistics":
                    break;
                default:
                    //狀況全不符合，自動導向問卷畫面
                    this.Response.Redirect($"Detail.aspx?ID={questionnaireID}&State=Questionnaire");
                    return;
            }
        }
        /// <summary>
        /// 問卷頁面
        /// </summary>
        private void _PageQuestionnaire()
        {
            var questionnaireData = QuestionnaireManager.GetQuestionnaireData(questionnaireID);
            if (questionnaireData.QuestionnaireID != null)
            {
                //this.txtQuestionnaireTitle.Text = questionnaireData.Title;
                //this.txtQuestionnaireContent.Text = questionnaireData.Briefly;
                //this.txtQuestionnaireStartDate.Text = questionnaireData.StartTimeText;
                //this.txtQuestionnaireEndDate.Text = questionnaireData.EndTimeText;
                //this.checkIsEnable.Checked = questionnaireData.IsEnable;
            }
        }
        /// <summary>
        /// 問題頁面
        /// </summary>
        private void _PageQuestion()
        {
            //var questionData = QuestionManager.GetQuestionList(questionnaireID);
            //if (questionData.Count != 0)
            //{
            //    this.rptQusList.Visible = true;
            //    this.trNoData.Visible = false;

            //    this.rptQusList.DataSource = questionData;
            //    this.rptQusList.DataBind();
            //}
            //else
            //{
            //    this.rptQusList.Visible = false;
            //    this.trNoData.Visible = true;
            //}
        }

        /// <summary>
        /// 取消對問卷的改動並導向清單列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            this.Session.Clear();
            Response.Redirect("List.aspx");
        }
        /// <summary>
        /// 儲存資料並導向清單列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (sender == this.btnQuestionnaireSave)
            {
                QuestionnaireModel model = (QuestionnaireModel)this.Session["questionnaireData"];
                QuestionnaireManager.SaveQuestionnaireData(model);
            }
            else if (sender == this.btnQuestionListSave)
            {

            }
        }
    }
}