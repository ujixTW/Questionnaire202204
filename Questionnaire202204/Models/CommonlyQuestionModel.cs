using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Questionnaire202204.Models
{
    public class CommonlyQuestionModel
    {
        public Guid QuestionID { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }
        public string QuestionContent { get; set; }
        public string QuestionOption { get; set; }
        public bool IsRequired { get; set; }
    }
}