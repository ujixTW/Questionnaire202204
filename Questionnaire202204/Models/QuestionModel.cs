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
        /// 1.文字方塊, 2.文字方塊 (數字), 3.文字方塊(Email)
        /// 4.文字方塊 (日期), 5.單選方塊, 6.複選方塊
        /// </summary>
        public int QusType { get; set; }
        
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
    public class StatisticsQuestionAnswerModel
    {
        public int NO { get; set; }
        /// <summary>
        /// 問題種類
        /// 1.文字方塊, 2.文字方塊 (數字), 3.文字方塊(Email)
        /// 4.文字方塊 (日期), 5.單選方塊, 6.複選方塊
        /// </summary>
        public int QusType { get; set; }
        public string QuestionContent { get; set; }
        public string OptionContent { get; set; }
        /// <summary>
        /// 選項是否為必填
        /// </summary>
        public bool IsRequired { get; set; }
        public string StatisticsAnswer { get; set; }

    }

}