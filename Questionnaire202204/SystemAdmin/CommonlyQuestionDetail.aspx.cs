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
    public partial class CommonlyQuestionDetail : System.Web.UI.Page
    {
        private static Guid _commonlyQusID;
        private static int _commonlyQusNO;
        protected void Page_Load(object sender, EventArgs e)
        {
            

            if (!IsPostBack)
            {
                string commonlyQusIDText = this.Request.QueryString["ID"];
                //檢查QS上的ID是否正確
                if (!string.IsNullOrWhiteSpace(commonlyQusIDText))
                {
                    //檢查QS傳遞內容是否為GUID
                    if (!Guid.TryParse(commonlyQusIDText, out _commonlyQusID))
                    {
                        //不符條件，送回列表頁
                        Response.Redirect("CommonlyQuestionList.aspx");
                        return;
                    }
                }
                else
                {
                    //不符條件，送回列表頁
                    Response.Redirect("CommonlyQuestionList.aspx");
                    return;
                }

                var commonlyQusData = CommonlyQuestionManager.GetCommonlyQuestion(_commonlyQusID);

                this.txtCommlyQusName.Text = (string.IsNullOrEmpty(commonlyQusData.Name)) ? "" : commonlyQusData.Name;
                this.txtQuestionContent.Text = (string.IsNullOrEmpty(commonlyQusData.QuestionContent)) ? "" : commonlyQusData.QuestionContent;
                this.txtQuestionOption.Text = (string.IsNullOrEmpty(commonlyQusData.QuestionOption)) ? "" : commonlyQusData.QuestionOption;

                this.checkIsRequired.Checked = commonlyQusData.IsRequired;
                this.checkBoxIsEnable.Checked = (commonlyQusData.QuestionID == Guid.Empty) ? true : commonlyQusData.IsEnable;

                this.listQuestionType.SelectedIndex = (commonlyQusData.QuestionID == Guid.Empty) ? 0 : commonlyQusData.Type - 1;
                _commonlyQusNO = (commonlyQusData.NO == 0) ? (int)this.Session["commonlyQuestionListCount"] + 1 : commonlyQusData.NO;
            }

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("CommonlyQuestionList.aspx");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            this.ltlQusContentMsg.Visible = false;
            this.ltlCommlyQusNameMsg.Visible = false;

            if (string.IsNullOrWhiteSpace(this.txtCommlyQusName.Text))
            {
                this.ltlCommlyQusNameMsg.Visible = true;
                return;
            }
            if (string.IsNullOrWhiteSpace(this.txtQuestionContent.Text))
            {
                this.ltlQusContentMsg.Visible = true;
                return;
            }
            CommonlyQuestionModel model = new CommonlyQuestionModel();
            model.QuestionID = _commonlyQusID;
            model.NO = _commonlyQusNO;
            model.Name = this.txtCommlyQusName.Text;
            model.QuestionContent = this.txtQuestionContent.Text;
            model.Type = int.Parse(this.listQuestionType.SelectedValue);
            model.QuestionOption = this.txtQuestionOption.Text;
            model.IsRequired = this.checkIsRequired.Checked;
            model.IsEnable = this.checkBoxIsEnable.Checked;

            if ((int)this.Session["commonlyQuestionListCount"] < model.NO)
            {
                CommonlyQuestionManager.CreateCommonlyQuestion(model);
            }else
            {
                CommonlyQuestionManager.UpdateCommonlyQuestion(model);
            }
            Response.Redirect("CommonlyQuestionList.aspx");
        }

    }
}