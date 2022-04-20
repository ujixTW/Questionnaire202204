using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Questionnaire202204.SystemAdmin
{
    public partial class AdminMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void linkbtnList_Click(object sender, EventArgs e)
        {
            this.Session.Clear();
            Response.Redirect("List.aspx");
        }

        protected void linkbtnCommonlyQuestionList_Click(object sender, EventArgs e)
        {
            this.Session.Clear();
            Response.Redirect("CommonlyQuestionList.aspx");
        }
    }
}