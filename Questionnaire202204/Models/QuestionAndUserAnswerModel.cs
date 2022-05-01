﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Questionnaire202204.Models
{
    /// <summary>
    /// 暫存問題與使用者回答資料用class，用作於一次傳輸前述資料
    /// </summary>
    public class QuestionAndUserAnswerModel
    {
        /// <summary>
        /// 使用者資料
        /// </summary>
        public UserDataModel userData { get; set; }
        /// <summary>
        /// 問題List
        /// </summary>
        public List<QuestionModel> questionList { get; set; }
        /// <summary>
        /// 使用者回答List
        /// </summary>
        public List<UserAnswerModel> userAnswerList { get; set; }

        /// <summary>
        /// 將使用者資料與答案組成一個以，號分開的字串
        /// </summary>
        /// <returns>以，號組合使用者資料與答案的字串</returns>
        public string UserAnswerToString()
        {
            var tempText = "";
            var tempOptionNO = 1;
            foreach (var item in userAnswerList)
            {
                if (item.OptionNO == 0)
                {
                    tempText += $",{item.Answer}";
                    tempOptionNO = 1;
                    continue;
                }
                if (item.OptionNO == tempOptionNO)
                {
                    tempText += (tempOptionNO == 1) ? $",{item.Answer}" : $";{item.Answer}";
                    tempOptionNO += 1;
                }else
                {
                    tempOptionNO = 2;
                    tempText +=  $",{item.Answer}";
                }
            }
            return $"{userData.Name},{userData.Age},{userData.Mobile},{userData.Email}{tempText}";
        }
        /// <summary>
        /// 將使用者資料項目名稱與題目組成一個以，號分開的字串
        /// </summary>
        /// <returns>以，號組合使用者資料項目名稱與題目的字串</returns>
        public string CSVTitle()
        {
            var tempText = "";
            foreach (var item in questionList)
            {
                tempText += $",{item.QuestionContent}";
            }
            return $"姓名,年齡,手機號碼,信箱{tempText}";
        }
    }

    public class QuestionAndAnswerStatisticsModel
    {
        /// <summary>
        /// 是否有人作答
        /// </summary>
        public bool IsAnswered { get; set; }
        /// <summary>
        /// 問題List
        /// </summary>
        public List<QuestionModel> questionList { get; set; }

        /// <summary>
        /// 使用者回答統計List
        /// </summary>
        public List<UserAnswerStatisticsModel> answerStatisticsList { get; set; }
    }
}