using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Questionnaire202204.Models;

namespace Questionnaire202204
{
    public partial class FrontMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var pagePath = Path.GetFileName(Request.PhysicalPath);
            QuestionnaireModel model = new QuestionnaireModel();
            if (this.Session["questionnaireData"] != null)
            {
                model = (QuestionnaireModel)this.Session["questionnaireData"];
            }



            switch (pagePath)
            {
                case "List.aspx":
                    this.Page.Title = "前台列表頁";
                    this.lblIsVote.Text = string.Empty;
                    this.lblVoteTime.Text = string.Empty;
                    this.lblQuestionnatreTitle.Text = string.Empty;
                    this.lblQuestionnatreBriefly.Text = string.Empty;
                    break;
                case "Form.aspx":
                    this.Page.Title = "前台內頁";
                    if (model.StartTime > DateTime.Today || model.EndTime < DateTime.Today || model.IsEnable == false)
                    {
                        this.lblIsVote.Text = "已結束投票";
                        this.lblVoteTime.Text = string.Empty;
                    }
                    else
                    {
                        this.lblIsVote.Text = "投票中";
                        this.lblVoteTime.Text = $"{model.StartTimeText}～{model.EndTimeText}";
                    }

                    this.lblQuestionnatreTitle.Text = model.Title;
                    this.lblQuestionnatreBriefly.Text = model.Briefly;
                    break;
                case "ConfirmPage.aspx":
                    this.Page.Title = "前台確認頁";
                    if (model.StartTime > DateTime.Today || model.EndTime < DateTime.Today || model.IsEnable == false)
                    {
                        this.lblIsVote.Text = "已結束投票";
                        this.lblVoteTime.Text = string.Empty;
                    }
                    else
                    {
                        this.lblIsVote.Text = "投票中";
                        this.lblVoteTime.Text = $"{model.StartTimeText}～{model.EndTimeText}";
                    }
                    this.lblQuestionnatreTitle.Text = model.Title;
                    this.lblQuestionnatreBriefly.Text = string.Empty;
                    break;
                case "Stastic.aspx":
                    this.Page.Title = "前台統計頁";
                    this.lblIsVote.Text = string.Empty;
                    this.lblVoteTime.Text = string.Empty;
                    this.lblQuestionnatreTitle.Text = model.Title;
                    this.lblQuestionnatreBriefly.Text = string.Empty;
                    break;
            }
        }
    }
}