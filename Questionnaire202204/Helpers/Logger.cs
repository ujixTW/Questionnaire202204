using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Questionnaire202204.Helpers
{
    public class Logger
    {
        private const string _savePath = "C:\\Logs\\QuestionnaireLog.log";

        /// <summary> 紀錄錯誤 </summary>
        /// <param name="moduleName">出錯的方法</param>
        /// <param name="ex">錯誤內容</param>
        public static void WriteLog(string moduleName, Exception ex)
        {
            // -----
            // yyyy/MM/dd HH:mm:ss
            //   Module Name
            //   Error Content
            // -----

            string content =
$@"-----
{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")}
    {moduleName}
    {ex.ToString()}
-----

";

            File.AppendAllText(Logger._savePath, content);
        }
    }
}