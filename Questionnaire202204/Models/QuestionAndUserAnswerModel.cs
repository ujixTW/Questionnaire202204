using System;
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
    }
}