using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Questionnaire202204.Models
{
    public class QuestionModel
    {
        public string QuestionID { get; set; }
        public Guid QuestionnaireID { get; set; }
        public int NO { get; set; }
        /// <summary>
        /// 問題種類
        /// </summary>
        public int QusType { get; set; }
        /// <summary>
        /// 問題種類顯示於網頁的文字
        /// </summary>
        public string QusTypeText
        {
            get
            {
                string temp = string.Empty;
                switch (QusType)
                {
                    case 1:
                        temp = "文字方塊";
                        break;
                    case 2:
                        temp = "文字方塊 (數字) ";
                        break;
                    case 3:
                        temp = "文字方塊(Email)";
                        break;
                    case 4:
                        temp = "文字方塊 (日期)";
                        break;
                    case 5:
                        temp = "單選方塊";
                        break;
                    case 6:
                        temp = "複選方塊";
                        break;
                }
                return temp;
            }
        }
        public string QuestionContent { get; set; }
        /// <summary>
        /// 於清單中顯示問題的短內容
        /// </summary>
        public string ShortQuestionContent
        {
            get
            {
                if (QuestionContent.Length > 10)
                {
                    return QuestionContent.Substring(0, 10) + "...";
                }
                else
                {
                    return QuestionContent;
                }
            }
        }
        public string OptionContent { get; set; }
        /// <summary>
        /// 選項是否為必填
        /// </summary>
        public bool IsRequired { get; set; }
    }

}