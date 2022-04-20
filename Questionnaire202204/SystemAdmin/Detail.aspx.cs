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
        /// 儲存資料
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
            this.ltlSaveMsg.Visible = true;
        }
        //變更問卷文字方塊內文字
        protected void txtQuestionnaire_TextChanged(object sender, EventArgs e)
        {
            //取出session
            QuestionnaireModel model = (QuestionnaireModel)this.Session["questionnaireData"];
            if (model == null)
            {
                return;
            }
            //改動特定資料
            if (sender == this.txtQuestionnaireTitle)
            {
                if (string.IsNullOrWhiteSpace(this.txtQuestionnaireTitle.Text))
                {
                    this.ltlQuestionnaireTitleMsg.Visible = true;
                }
                else
                {
                    model.Title = this.txtQuestionnaireTitle.Text;
                    this.ltlQuestionnaireTitleMsg.Visible = false;
                }
            }
            else if (sender == this.txtQuestionnaireContent)
            {
                model.Briefly = this.txtQuestionnaireContent.Text;
            }
            else if (sender == this.txtQuestionnaireStartDate)
            {
                //如果格式不正確就顯示警告視窗
                //不正確格式:日期未包含年月日、日期未分割、輸入非數字及符號、日期未在規定範圍內
                if (DateTime.TryParse(this.txtQuestionnaireStartDate.Text, out DateTime date))
                {
                    if ((model.EndTime==null||date<model.EndTime)&& date >= DateTime.Today)
                    {
                        model.StartTime = date;
                        this.ltlQuestionnaireStartDateMsg.Visible = false;
                    }
                    else
                    {
                        this.ltlQuestionnaireStartDateMsg.Visible = true;
                    }
                }
                else
                {
                    this.ltlQuestionnaireStartDateMsg.Visible = true;
                }
            }
            else if (sender == this.txtQuestionnaireEndDate)
            {
                //如果格式不正確就顯示警告視窗
                //不正確格式:日期未包含年月日、日期未分割、輸入非數字及符號、日期未在規定範圍內
                if (DateTime.TryParse(this.txtQuestionnaireEndDate.Text, out DateTime date))
                {
                    //檢查時間是否小於起始時間
                    if (date > model.StartTime)
                    {
                        model.EndTime = date;
                        this.ltlQuestionnaireEndDateMsg.Visible = false;
                    }
                    else
                    {
                        this.ltlQuestionnaireEndDateMsg.Visible = true;
                    }

                }
                else if (string.IsNullOrWhiteSpace(this.txtQuestionnaireEndDate.Text) || (string.Compare(this.txtQuestionnaireEndDate.Text, "-") == 0))
                {
                    model.EndTime = null;
                    this.ltlQuestionnaireEndDateMsg.Visible = false;
                }
                else
                {
                    this.ltlQuestionnaireEndDateMsg.Visible = true;
                }
            }
            //重新放回session
            this.Session["questionnaireData"] = model;

        }

        protected void checkIsEnable_CheckedChanged(object sender, EventArgs e)
        {
            QuestionnaireModel model = (QuestionnaireModel)this.Session["questionnaireData"];
            model.IsEnable = this.checkIsEnable.Checked;
            this.Session["questionnaireData"] = model;
        }

        
    }
}