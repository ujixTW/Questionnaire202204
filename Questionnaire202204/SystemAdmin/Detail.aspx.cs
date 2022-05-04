using Questionnaire202204.Helpers;
using Questionnaire202204.Managers;
using Questionnaire202204.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Questionnaire202204.SystemAdmin
{
    //Session請參考ReadMe.txt

    public partial class Detail : System.Web.UI.Page
    {
        public Guid QuestionnaireID;
        public int PageSize { get { return 4; } }
        public int PageIndex;

        protected void Page_Load(object sender, EventArgs e)
        {
            string pageState = this.Request.QueryString["State"];
            string questionnaireIDText = this.Request.QueryString["ID"];
            //檢查QS上的ID格式是否正確
            if (!Guid.TryParse(questionnaireIDText, out QuestionnaireID))
            {
                //不符條件，送回列表頁
                Response.Redirect("List.aspx");
                return;
            }

            if (this.Session["questionnaireData"] != null)
            {
                QuestionnaireModel model = (QuestionnaireModel)this.Session["questionnaireData"];
                if (model.QuestionnaireID != QuestionnaireID)
                {
                    this.Session.Clear();
                }
            }
            else if (this.Session["questionDataList"] != null)
            {
                List<QuestionModel> modelList = (List<QuestionModel>)this.Session["questionDataList"];
                if (modelList[0].QuestionnaireID != QuestionnaireID)
                {
                    this.Session.Clear();
                }
            }


            if (!IsPostBack)
            {


                //依頁簽不同套用不同資料
                switch (pageState)
                {
                    case "Questionnaire":
                        SetQuestionnairePageData();
                        break;
                    case "Question":
                        //如果為問題頁面，自動套用資料進新增、詳細編輯問題的區域
                        _InputQuestionEditAreaDeta();
                        break;
                    case "UserAnswer":
                        //如果為填寫資料頁面，套用資料進切換頁面的UC中
                        _ChangeUserAnswerPage(pageState);
                        break;
                }

            }

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
                QuestionnaireModel questionnaireModel = (QuestionnaireModel)this.Session["questionnaireData"];
                if (questionnaireModel.StartTime > questionnaireModel.EndTime)
                {
                    this.ltlSaveFailMsg.Visible = true;
                    return;
                }
                QuestionnaireManager.SaveQuestionnaireData(questionnaireModel);
                this.Session["questionnaireData"] = QuestionnaireManager.GetQuestionnaireData(QuestionnaireID);
            }
            else if (sender == this.btnQuestionListSave)
            {
                if (_CheckIsAnswered())
                {
                    this.ltlIsAnsweredMsg.Visible = true;
                    return;
                }
                //如果是新增問卷模式且未先儲存問卷頁簽資料
                if (this.Session["questionnaireData"] != null)
                {
                    QuestionnaireModel questionnaireModel = (QuestionnaireModel)this.Session["questionnaireData"];
                    if (questionnaireModel.NO == null)
                    {
                        QuestionnaireManager.SaveQuestionnaireData(questionnaireModel);
                        this.Session["questionnaireData"] = QuestionnaireManager.GetQuestionnaireData(QuestionnaireID);
                    }
                }

                List<QuestionModel> models = (List<QuestionModel>)this.Session["questionDataList"];
                QuestionManager.UpDateQuestionList(models, (int)this.Session["DBQuestionDataListCount"]);
                this.Session["DBQuestionDataListCount"] = models.Count;
            }
            this.ltlSaveMsg.Visible = true;

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
        /// 檢查是否有人做過問卷
        /// </summary>

        //判斷是否有人做過問卷，有為 true，沒有為false
        private bool _CheckIsAnswered()
        {
            //判斷是否有人填問卷了，如果有就阻止問卷內容變更
            if (this.Session["IsAnswered"] is null)
            {
                this.Session["IsAnswered"] = UserAnswerManager.GetIsAnswered(QuestionnaireID);
            }
            return ((bool)this.Session["IsAnswered"]) ? true : false;

        }

        #region 問卷頁簽

        private void SetQuestionnairePageData()
        {
            if (this.Session["questionnaireData"] == null)
            {
                this.Session["questionnaireData"] = QuestionnaireManager.GetQuestionnaireData(QuestionnaireID);
            }
            QuestionnaireModel model = (QuestionnaireModel)this.Session["questionnaireData"];
            this.txtQuestionnaireTitle.Text = model.Title;
            this.txtQuestionnaireContent.Text = model.Briefly;
            this.txtQuestionnaireStartDate.Text = model.StartTimeText;
            this.txtQuestionnaireEndDate.Text = model.EndTimeText;
            this.checkIsEnable.Checked = model.IsEnable;
        }

        //變更問卷文字方塊內文字
        protected void txtQuestionnaire_TextChanged(object sender, EventArgs e)
        {
            this.ltlSaveFailMsg.Visible = false;
            this.ltlSaveMsg.Visible = false;

            //取出session
            QuestionnaireModel model = (QuestionnaireModel)this.Session["questionnaireData"];
            if (model == null)
            {
                return;
            }
            //判斷是否有人填問卷了，如果有就阻止問卷內容變更
            if (sender != this.txtQuestionnaireEndDate && _CheckIsAnswered())
            {
                this.ltlIsAnsweredMsg.Visible = true;
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
                //起始時間如果與舊有資料相同就不做判斷
                if (string.Compare(model.StartTimeText, this.txtQuestionnaireStartDate.Text, true) == 0)
                {
                    return;
                }
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
                    //如果日期有變化就不顯示已有人填寫問卷的警告文字
                    if (model.EndTime != date)
                        this.ltlIsAnsweredMsg.Visible = false;

                    //檢查時間是否小於起始時間
                    if (date > model.StartTime && date > DateTime.Today)
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
            this.ltlSaveFailMsg.Visible = false;
            this.ltlSaveMsg.Visible = false;
            QuestionnaireModel model = (QuestionnaireModel)this.Session["questionnaireData"];
            if (this.checkIsEnable.Checked != model.IsEnable)
            {
                model.IsEnable = this.checkIsEnable.Checked;
                this.ltlIsAnsweredMsg.Visible = false;
            }
            this.Session["questionnaireData"] = model;
        }

        #endregion

        #region 問題頁簽

        /// <summary>
        /// 問題頁面，自動套用資料進新增、詳細編輯問題的區域
        /// </summary>
        private void _InputQuestionEditAreaDeta()
        {
            //加入、編輯按鈕文字
            var btnEditText = "加入";
            //編輯中的問題ID
            var qusID = this.Request.QueryString["QusID"];
            //問題
            QuestionModel question = new QuestionModel();
            //問題清單
            List<QuestionModel> qusList = (List<QuestionModel>)this.Session["questionDataList"];
            var qusListCount = (qusList == null) ? 0 : qusList.Count;
            //找到是哪筆問題
            for (var i = 0; i < qusListCount; i++)
            {
                if (qusList[i].QuestionID == qusID)
                {
                    btnEditText = "編輯";
                    question = qusList[i];
                    i = qusList.Count + 10;
                }
            }

            //套用資料進DropDownList
            //判斷session中是否有commonlyQuestion資料
            List<CommonlyQuestionModel> commonlyQuestionModelList = new List<CommonlyQuestionModel>();
            if (this.Session["commonlyQuestionList"] != null)
            {
                commonlyQuestionModelList = (List<CommonlyQuestionModel>)this.Session["commonlyQuestionList"];
            }
            else
            {
                commonlyQuestionModelList = CommonlyQuestionManager.GetCommonlyQuestionList();
                this.Session["commonlyQuestionList"] = commonlyQuestionModelList;
            }
            //套用資料進DropDownList
            this.listCommonlyQuestionType.Items.Add(new ListItem("自訂問題", qusID));

            for (var i = 0; i < commonlyQuestionModelList.Count; i++)
            {
                var text = commonlyQuestionModelList[i].Name;
                var value = commonlyQuestionModelList[i].QuestionID.ToString();
                this.listCommonlyQuestionType.Items.Add(new ListItem(text, value));
            }

            //更新其他表格
            this.txtQuestionContent.Text = question.QuestionContent;
            this.listQuestionType.SelectedIndex = question.QusType - 1;
            this.checkIsRequired.Checked = question.IsRequired;
            this.txtQuestionOption.Text = question.OptionContent;
            this.btnAddQuestion.Text = btnEditText;
        }

        //加入問卷問題按鈕
        protected void btnAddQuestion_Click(object sender, EventArgs e)
        {
            //判斷是否有人填問卷了，如果有就阻止問卷內容變更
            if (_CheckIsAnswered())
            {
                this.ltlIsAnsweredMsg.Visible = true;
                return;
            }
            //判斷問題內容欄位是否有填寫，如果沒有就阻止加入
            if (string.IsNullOrWhiteSpace(this.txtQuestionContent.Text))
            {
                return;
            }

            //將問題清單從session中取出
            var questionDataList = (List<QuestionModel>)this.Session["questionDataList"];
            var questionID = string.Empty;
            var questionNO = 0;
            //取得問題ID及NO
            if (string.Compare("自訂問題", this.listCommonlyQuestionType.SelectedValue, true) == 0)
            {
                //如果是新問題
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
            else if (Guid.TryParse(this.listCommonlyQuestionType.SelectedValue, out Guid qusGuid))
            {
                //如果是常用問題(新問題)
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
                //如果是舊問題
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

            //暫存資料
            var questionContent = this.txtQuestionContent.Text;
            var questionType = int.Parse(this.listQuestionType.SelectedValue);
            var isRequired = this.checkIsRequired.Checked;
            var questionOption = this.txtQuestionOption.Text;

            //如果題目為選擇題而未給選項的話，阻止加入
            if (questionType >= 5 && string.IsNullOrWhiteSpace(questionOption))
                return;


            //判斷如果資料為編輯或新增
            if (questionNO > questionDataList.Count)
            {
                //將資料暫存成model
                QuestionModel model = new QuestionModel()
                {
                    QuestionID = questionID,
                    QuestionnaireID = QuestionnaireID,
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
            Response.Redirect(Request.Path + $"?ID={QuestionnaireID}&State=Question");
        }

        //變換常用問題/自訂問題種類
        protected void listCommonlyQuestionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //所選的問題ID
            string qusID = this.listCommonlyQuestionType.SelectedValue;
            //問題
            QuestionModel question = new QuestionModel();

            if (Guid.TryParse(qusID, out Guid commonlyQusID))
            {
                //常用問題
                List<CommonlyQuestionModel> commonlyQusList = (List<CommonlyQuestionModel>)this.Session["commonlyQuestionList"];
                //找到是套用哪筆常用問題
                for (var i = 0; i < commonlyQusList.Count; i++)
                {
                    if (commonlyQusList[i].QuestionID == commonlyQusID)
                    {
                        question.QuestionContent = commonlyQusList[i].QuestionContent;
                        question.QusType = commonlyQusList[i].Type;
                        question.IsRequired = commonlyQusList[i].IsRequired;
                        question.OptionContent = commonlyQusList[i].QuestionOption;
                        i = commonlyQusList.Count + 10;
                    }
                }
            }
            else
            {
                Response.Redirect(Request.Path + $"?ID={QuestionnaireID}&State=Question");
            }


            //更新其他表格
            this.txtQuestionContent.Text = question.QuestionContent;
            this.listQuestionType.SelectedIndex = question.QusType - 1;
            this.checkIsRequired.Checked = question.IsRequired;
            this.txtQuestionOption.Text = question.OptionContent;
            this.btnAddQuestion.Text = "加入";
        }

        #endregion

        #region 填寫資料頁簽
        /// <summary>
        /// 判斷頁數並動態更改頁數選擇內容、QS的狀態
        /// </summary>
        /// <param name="pageState">網頁頁簽</param>
        private void _ChangeUserAnswerPage(string pageState)
        {
            if (this.Request.QueryString["UserID"] != null)
            {
                //如果使用者ID格式錯誤就自動跳回清單
                if (!Guid.TryParse(this.Request.QueryString["UserID"], out Guid id))
                {
                    Response.Redirect(Request.Path + $"?ID={QuestionnaireID}&State={pageState}");
                    return;
                }
                this.btnOutPutUserData.Visible = false;
                this.ucPageChange.Visible = false;

            }
            else
            {
                this.btnOutPutUserData.Visible = true;
                this.ucPageChange.Visible = true;
            }
            //判斷當前頁數
            string pageIndexText = this.Request.QueryString["Page"];
            List<string> keyQS = new List<string>() { "ID", "State" };
            List<string> keyQSValue = new List<string>() { QuestionnaireID.ToString(), pageState };
            if (string.IsNullOrWhiteSpace(pageIndexText) || !int.TryParse(pageIndexText, out PageIndex))
                PageIndex = 1;
            else
                PageIndex = Convert.ToInt32(pageIndexText);

            var sessionUserDataCount = this.Session["userDataListCount"];
            int totalRows = (sessionUserDataCount != null) ? (int)sessionUserDataCount : 0;

            this.ucPageChange.TotalRows = totalRows;
            this.ucPageChange.PageIndex = PageIndex;
            this.ucPageChange.PageSize = PageSize;
            this.ucPageChange.Bind(keyQS, keyQSValue);
        }

        /// <summary>
        /// 點擊匯出按鈕
        /// </summary>
        protected void btnOutPutUserData_Click(object sender, EventArgs e)
        {
            var userDataList = (List<UserDataModel>)this.Session["userDataList"];
            if (userDataList.Count == 0)
            {
                this.ltlUserAnswerOutPutFailMsg.Visible = true;
                return;
            }
            //獲得所有資料
            #region 獲得所有資料
            var outPutToCsvModelList = new List<QuestionAndUserAnswerModel>();
            var allUserAnswerList = UserAnswerManager.GetUserAnswerList(QuestionnaireID);
            Guid tempUserID = Guid.Empty;
            int tempDataPosition = 0;
            var tempQuestionList = (this.Session["questionDataList"] != null) ? (List<QuestionModel>)this.Session["questionDataList"] : QuestionManager.GetQuestionList(QuestionnaireID);

            for (var i = 0; i < userDataList.Count; i++)
            {
                var tempUserAnswerList = new List<UserAnswerModel>();

                //將資料切分成單一答題者
                //需將資料照UserID順序排列才能執行
                tempUserID = (tempUserID == Guid.Empty) ? allUserAnswerList[i].UserID : tempUserID;
                for (var j = tempDataPosition; j < allUserAnswerList.Count; j++)
                {
                    if (tempUserID == allUserAnswerList[j].UserID)
                    {
                        tempUserAnswerList.Add(allUserAnswerList[j]);
                        continue;
                    }
                    tempUserID = allUserAnswerList[j].UserID;
                    tempDataPosition = j;
                    break;
                }

                QuestionAndUserAnswerModel model = new QuestionAndUserAnswerModel()
                {
                    userData = userDataList[i],
                    userAnswerList = tempUserAnswerList,
                    questionList = tempQuestionList
                };
                outPutToCsvModelList.Add(model);
            }
            #endregion


            //輸出成CSV檔
            #region 輸出成CSV檔
            string driveText = (Directory.Exists("D:\\")) ? "D:\\" : "C:\\";
            string fillPath = $@"{driveText}tmp\問卷{this.QuestionnaireID}.csv";
            string fillFoldPath = $@"{driveText}tmp";
            List<string> dataTextList = new List<string>();
            dataTextList.Add(outPutToCsvModelList[0].CSVTitle());
            foreach (var item in outPutToCsvModelList)
            {
                dataTextList.Add(item.UserAnswerToString());
            }
            CSVHelper.CSVGenerator(fillFoldPath, fillPath, dataTextList);
            #endregion
            this.ltlUserAnswerOutPutSuccessMsg.Visible = true;
        }

        #endregion


    }
}