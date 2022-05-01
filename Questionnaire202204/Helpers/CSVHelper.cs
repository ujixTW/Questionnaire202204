using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Questionnaire202204.Helpers
{
    public class CSVHelper
    {



        /// <summary>
        /// CSV Generator
        /// </summary>
        /// <param name="filePath">目標CSV路徑資料夾</param>
        /// <param name="filePath">目標CSV路徑</param>
        /// <param name="data">目標資料字串List</param>
        public static void CSVGenerator(string fillFoldPath, string filePath, List<string> data)
        {
            if (!File.Exists(filePath))
            {
                File.Create(filePath);
            }
            if (!Directory.Exists(fillFoldPath))
            {
                Directory.CreateDirectory(fillFoldPath);
            }

            using (var file = new StreamWriter(filePath, true, System.Text.Encoding.UTF8))
            {
                foreach (var i in data)
                {
                    file.WriteLineAsync(i);
                }
            }
        }

    }
}