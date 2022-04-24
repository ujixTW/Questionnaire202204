using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Questionnaire202204.ShareControls
{
    public partial class ucQuestionnaireSearchBar : System.Web.UI.UserControl
    {
        public string Keyword { get; set; }
        public string StartTimeText { get; set; }
        public string EndTimeText { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               
                this.txtSearchText.Text = Keyword;
                this.txtStartTime.Text = StartTimeText;
                this.txtEndTime.Text = EndTimeText;
            }
        }


        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string caption = string.Empty;
            string startTime = string.Empty;
            string endTime = string.Empty;
            string qs = string.Empty;
            //關鍵字
            if (!string.IsNullOrWhiteSpace(this.txtSearchText.Text))
            {
                caption = "Caption=" + Server.UrlEncode(this.txtSearchText.Text.TrimStart().TrimEnd());
            }
            //開始時間
            //如果前面有值就加上&符號
            if (DateTime.TryParse(this.txtStartTime.Text, out DateTime tempStartTime))
            {
                if (string.IsNullOrWhiteSpace(this.txtSearchText.Text))
                {
                    startTime = "StartDate=" + tempStartTime.ToString("d");
                }
                else
                {
                    startTime = "&" + "StartDate=" + tempStartTime.ToString("d");
                }
            }
            else
                this.ltlSearchErrorMsg.Visible = true;
            //結束時間
            //如果前面有值就加上&符號
            if (DateTime.TryParse(this.txtEndTime.Text, out DateTime tempEndTime))
            {
                if (string.IsNullOrWhiteSpace(this.txtSearchText.Text))
                {
                    endTime = "EndDate=" + tempEndTime.ToString("d");
                }
                else
                {
                    endTime = "&" + "EndDate=" + tempEndTime.ToString("d");
                }
            }
            else
                this.ltlSearchErrorMsg.Visible = true;

            qs = (!string.IsNullOrEmpty(caption) || !string.IsNullOrEmpty(startTime) || !string.IsNullOrEmpty(endTime)) ? "?" : "";

            this.Response.Redirect("List.aspx" + qs + caption + startTime + endTime);
        }

    }
}