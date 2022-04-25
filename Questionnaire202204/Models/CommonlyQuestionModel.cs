using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Questionnaire202204.Models
{
    public class CommonlyQuestionModel
    {
        public Guid QuestionID { get; set; }
        public int NO { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }
        /// <summary>
        /// 問題種類顯示於網頁的文字
        /// </summary>
        public string TypeText
        {
            get
            {
                var temp = "";
                switch (this.Type)
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
        public string QuestionOption { get; set; }
        public bool IsRequired { get; set; }
        public bool IsEnable { get; set; }
        public string IsEnableText
        {
            get
            {
                return (this.IsEnable) ? "已啟用" : "未啟用";
            }
        }
    }
}