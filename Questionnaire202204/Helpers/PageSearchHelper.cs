using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Questionnaire202204.Helpers
{
    public class PageSearchHelper
    {
        /// <summary>
        /// 取得頁面QS資料
        /// </summary>
        /// <param name="context">本頁Context</param>
        /// <param name="keyword">關鍵字</param>
        /// <param name="startTimeText">起始時間</param>
        /// <param name="endTimeText">結束時間</param>
        public static void GetQuestionnatreSearchText(HttpContext context,out string keyword,out string startTimeText, out string endTimeText)
        {
            keyword = context.Request.QueryString["Caption"];   //使用者輸入的關鍵字
            startTimeText = context.Request.QueryString["StartDate"];  //使用者輸入的開始時間
            endTimeText = context.Request.QueryString["EndDate"];      //使用者輸入的結束時間

            //確認關鍵字QS格式是否正確
            keyword = (string.IsNullOrEmpty(keyword)) ? string.Empty : keyword;
            //確認起始時間QS格式是否正確
            if (DateTime.TryParse(startTimeText, out DateTime startTime))
            {
                startTimeText = startTime.ToString("yyyy-MM-dd");
            }
            else
            {
                startTimeText = string.Empty;
            }
            //確認結束時間QS格式是否正確
            if (DateTime.TryParse(endTimeText, out DateTime endTime))
            {
                endTimeText = endTime.ToString("yyyy-MM-dd");
            }
            else
            {
                endTimeText = string.Empty;
            }
        }
    }
}