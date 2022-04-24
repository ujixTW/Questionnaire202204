using Questionnaire202204.Helpers;
using Questionnaire202204.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Questionnaire202204.Managers
{
    public class CommonlyQuestionManager
    {
        //增加
        public static void CreateCommonlyQuestion()
        {

        }
        //刪除
        //修改
        //查詢
        /// <summary>
        /// 查詢多筆常用問題資料
        /// </summary>
        /// <returns>常用問題資料</returns>
        public static List<CommonlyQuestionModel> GetCommonlyQuestionList()
        {
            string connStr = ConfigHelper.GetConnectionString();
            string commandText = $@"
                                SELECT
                                    [QuestionID],[Name],[Type],[QuestionContent]
                                    ,[QuestionOption],[IsRequired]
                                FROM [Questionnaire202204].[dbo].[CommonlyQuestion]
                                ORDER BY [CreateTime] DESC
                                ";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        conn.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        List<CommonlyQuestionModel> commonlyQuestionDataList = new List<CommonlyQuestionModel>();

                        //將資料取出放到List中
                        while (reader.Read())
                        {
                            CommonlyQuestionModel info = new CommonlyQuestionModel()
                            {
                                QuestionID = (Guid)reader["QuestionID"],
                                Name = reader["Name"] as string,
                                Type = (int)reader["Type"],
                                QuestionContent = reader["QuestionContent"] as string,
                                QuestionOption = reader["QuestionOption"] as string,
                                IsRequired = (bool)reader["IsRequired"]
                            };
                            commonlyQuestionDataList.Add(info);
                        }
                        return commonlyQuestionDataList;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("Questionnaire202204.Manager.QuestionnaireManager.GetQuestionnaireList", ex);
                throw;
            }
        }
    }
}