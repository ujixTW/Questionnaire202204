using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Questionnaire202204.Models
{
    /// <summary>
    /// 存放使用者於該筆問卷的作答紀錄
    /// </summary>
    public class UserAnswerModel
    {
        public Guid UserID { get; set; }
        public Guid QuestionnaireID { get; set; }
        public string QuestionID { get; set; }
        public string Answer { get; set; }
    }

    /// <summary>
    /// 存放使用者於該筆問卷所填寫的個人資料
    /// </summary>
    public class UserDataModel
    {
        public Guid UserID { get; set; }
        public Guid QuestionnaireID { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public DateTime CreateTime { get; set; }
    }
}