using Questionnaire202204.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Questionnaire202204.ShareControls
{
    public partial class ucDeleteButton : System.Web.UI.UserControl
    {
        private static List<Guid> _questionnaireIDList = new List<Guid>();

        /// <summary>
        /// 開啟刪除確認視窗，存入待刪除資料
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDelete_Click(object sender, ImageClickEventArgs e)
        {
            //重置選取的ID資料
            _questionnaireIDList.Clear();
            //讀取被選取的checkbox的value
            var questionnaire = Request["checkboxQus"];

            if (questionnaire != null)
            {
                var questionnaireIDs = questionnaire.ToString().Split(',');
                foreach (var item in questionnaireIDs)
                {
                    _questionnaireIDList.Add(Guid.Parse(item));
                }
            }
            //開啟警告畫面
            this.divDeleteMsg.Visible = true;
        }
        /// <summary>
        /// 刪除資料或取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (sender == this.btnDeleteYes)
            {
                if (_questionnaireIDList.Count != 0)
                {
                    QuestionnaireManager.DeleteQuestionnaireList(_questionnaireIDList);
                }
                this.divDeleteMsg.Visible = false;
                Response.Redirect(Request.RawUrl);
            }
            else if (sender == this.btnDeleteNo)
            {
                this.divDeleteMsg.Visible = false;
            }
        }

    }
}