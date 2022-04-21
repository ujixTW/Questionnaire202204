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
        class dropDownDeta { public string type { get; set; } public string id { get; set; } }

        protected void Page_Load(object sender, EventArgs e)
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

            if (!IsPostBack)
            {
                _InputQuestionEditAreaDeta(pageState);

            }

        }

        private void _InputQuestionEditAreaDeta(string pageState)
        {
            //如果為問題頁面，自動套用資料進新增、詳細編輯問題的區域
            if (string.Compare(pageState, "Question", true) == 0)
            {
                //問題
                QuestionModel question = new QuestionModel();
                //問題清單
                List<QuestionModel> qusList = (List<QuestionModel>)this.Session["questionDataList"];
                var qusListCount = (qusList == null) ? 0 : qusList.Count;
                //找到是哪筆問題
                for (var i = 0; i < qusListCount; i++)
                {
                    if (qusList[i].QuestionID == this.Request.QueryString["QusID"])
                    {
                        question = qusList[i];
                        i = qusList.Count + 10;
                    }
                }
                //套用資料進DropDownList
                List<dropDownDeta> deta = new List<dropDownDeta>();
                dropDownDeta drop = new dropDownDeta() { id = question.QuestionID, type = "自訂問題" };
                deta.Add(drop);
                this.listCommonlyQuestionType.DataSource = deta;
                this.listCommonlyQuestionType.DataTextField = "type";
                this.listCommonlyQuestionType.DataValueField = "id";
                this.listCommonlyQuestionType.DataBind();

                //更新其他表格
                this.txtQuestionContent.Text = question.QuestionContent;
                this.listQuestionType.SelectedIndex = question.QusType - 1;
                this.checkIsRequired.Checked = question.IsRequired;
                this.txtQuestionOption.Text = question.OptionContent;
            }
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
                List<QuestionModel> models = (List<QuestionModel>)this.Session["questionDataList"];

            }
            this.ltlSaveMsg.Visible = true;

        }
        //變更問卷文字方塊內文字
        protected void txtQuestionnaire_TextChanged(object sender, EventArgs e)
        {
            this.ltlSaveMsg.Visible = false;
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
                    if ((model.EndTime == null || date < model.EndTime) && date >= DateTime.Today)
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
        //問卷是否啟用checkbox
        protected void checkIsEnable_CheckedChanged(object sender, EventArgs e)
        {
            this.ltlSaveMsg.Visible = false;
            QuestionnaireModel model = (QuestionnaireModel)this.Session["questionnaireData"];
            model.IsEnable = this.checkIsEnable.Checked;
            this.Session["questionnaireData"] = model;
        }
        //加入問卷問題按鈕
        protected void btnAddQuestion_Click(object sender, EventArgs e)
        {
            //將問題清單從session中取出
            var questionDataList = (List<QuestionModel>)this.Session["questionDataList"];
            var questionID = string.Empty;
            var questionNO = 0;
            //取得問題ID及NO
            if (string.IsNullOrWhiteSpace(this.listCommonlyQuestionType.SelectedValue))
            {
                var random = new Random();
                var randomResult = string.Empty;
                for (var i = 0; i < 6; i++)
                {
                    randomResult += random.Next(0, 10);
                }
                var temp = DateTime.Now.ToString("yyyyMMddhhmmss") + randomResult;
                questionID = temp;
                questionNO = questionDataList.Count + 1;
            }
            else
            {
                questionID = this.listCommonlyQuestionType.SelectedValue;
                //取得NO
                for (var i = 0; i < questionDataList.Count; i++)
                {
                    if (questionDataList[i].QuestionID == questionID)
                    {
                        questionNO = questionDataList[i].NO;
                        i = questionDataList.Count + 10;
                    }
                }
            }
            var questionContent = this.txtQuestionContent.Text;
            var questionType = int.Parse(this.listQuestionType.SelectedValue);
            var isRequired = this.checkIsRequired.Checked;
            var questionOption = this.txtQuestionOption.Text;

            //判斷如果資料為編輯或新增
            if (questionNO > questionDataList.Count)
            {
                //將資料暫存成model
                QuestionModel model = new QuestionModel()
                {
                    QuestionID = questionID,
                    QuestionnaireID = questionnaireID,
                    NO = questionNO,
                    QusType = questionType,
                    QuestionContent = questionContent,
                    OptionContent = questionOption,
                    IsRequired = isRequired
                };
                questionDataList.Add(model);
            }
            else
            {
                questionDataList[questionNO - 1].QusType = questionType;
                questionDataList[questionNO - 1].QuestionContent = questionContent;
                questionDataList[questionNO - 1].OptionContent = questionOption;
                questionDataList[questionNO - 1].IsRequired = isRequired;
            }

            //將資料存回session中
            this.Session["questionDataList"] = questionDataList;
            //重整以更新畫面
            Response.Redirect(Request.Path + $"?ID={questionnaireID}&State=Question");
        }

    }
}