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
        /// 用於前台問卷清單，紀錄目前是否可以投票
        /// </summary>
        public string VoteStateText
        {
            get
            {
                if ((EndTime is null || (DateTime)EndTime > DateTime.Today) && IsEnable == true)
                {
                    return "投票中";
                }
                else if (StartTime > DateTime.Today && IsEnable == true)
                {

                    return "尚未開始";
                }
                else
                {
                    return "已完結";
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
                return (this.IsEnable) ? "開放" : "已關閉";
            }
        }

    }
}