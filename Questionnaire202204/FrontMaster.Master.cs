﻿using System;
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
            string startTimeText = string.Empty;
            string EndTimeText = string.Empty;
            if (this.Session["questionnaireData"] != null)
            {
                var questionnaireData = (QuestionnaireModel)this.Session["questionnaireData"];
                startTimeText = questionnaireData.StartTimeText;
                EndTimeText = questionnaireData.EndTimeText;
            }

            switch (pagePath)
            {
                case "List.aspx":
                    this.Page.Title = "前台列表頁";
                    this.lblIsVote.Text = string.Empty;
                    this.lblVoteTime.Text = string.Empty;
                    break;
                case "Form.aspx":
                    this.Page.Title = "前台內頁";
                    this.lblIsVote.Text = "投票中";
                    this.lblVoteTime.Text = $"{startTimeText}～{EndTimeText}";
                    break;
                case "ConfirmPage.aspx":
                    this.Page.Title = "前台確認頁";
                    this.lblIsVote.Text = "投票中";
                    this.lblVoteTime.Text = $"{startTimeText}～{EndTimeText}";
                    break;
                case "Stastic.aspx":
                    this.Page.Title = "前台統計頁";
                    this.lblIsVote.Text = string.Empty;
                    this.lblVoteTime.Text = string.Empty;
                    break;
            }
        }
    }
}