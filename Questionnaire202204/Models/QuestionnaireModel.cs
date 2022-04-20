using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Questionnaire202204.Models
{
    public class QuestionnaireModel
    {
        public Guid QuestionnaireID { get; set; }
        /// <summary>
        /// 問卷編號
        /// </summary>
        public int? NO { get; set; }
        /// <summary>
        /// 問卷標題
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 問卷內容描述
        /// </summary>
        public string Briefly { get; set; }
        /// <summary>
        /// 投票開始時間
        /// </summary>
        public DateTime StartTime { get; set; }
        /// <summary>
        /// 顯示於介面上的投票開始時間
        /// </summary>
        public string StartTimeText
        {
            get
            {
                return StartTime.ToString("d");
            }
        }
        /// <summary>
        /// 投票結束時間
        /// </summary>
        public DateTime? EndTime { get; set; }
        /// <summary>
        /// 顯示於介面上的投票開始時間
        /// </summary>
        public string EndTimeText
        {
            get
            {
                if (EndTime == null)
                {
                    return "-";
                }
                else
                {
                    DateTime temp = (DateTime)EndTime;
                    return temp.ToString("d");
                }
            }
        }
        /// <summary>
        /// 是否啟用
        /// </summary>
        public bool IsEnable { get; set; }
        /// <summary>
        /// 啟用文字
        /// </summary>
        public string IsEnableText
        {
            get
            {
                string temp;
                return temp = IsEnable ? "開放" : "已關閉";
            }
        }

    }
}