﻿using Questionnaire202204.Helpers;
using Questionnaire202204.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Questionnaire202204.Managers
{
    public class QuestionManager
    {
        //增加
        //刪除
        //修改
        //查詢
        /// <summary>
        /// 後台查詢單筆問卷的所有問題
        /// </summary>
        /// <param name="questionnaireID">問卷ID</param>
        /// <returns></returns>
        public static List<QuestionModel> GetQuestionList(Guid questionnaireID)
        {
            string connStr = ConfigHelper.GetConnectionString();
            string commandText = $@"
                                SELECT
                                    [QuestionID],[QuestionnaireID],[NO],[Type]
                                    ,[QuestionContent],[OptionContent],[IsRequired]
                                FROM [Questionnaire202204].[dbo].[Question]
                                WHERE [QuestionnaireID] = @questionnaireID
                                ORDER BY [NO]
                                ";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        command.Parameters.AddWithValue("@questionnaireID", questionnaireID);
                        conn.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        List<QuestionModel> questionDataList = new List<QuestionModel>();

                        //將資料取出放到List中
                        while (reader.Read())
                        {
                            QuestionModel info = new QuestionModel()
                            {
                                QuestionID = reader["QuestionID"] as string,
                                QuestionnaireID = questionnaireID,
                                NO = (int)reader["NO"],
                                QusType = (int)reader["Type"],
                                QuestionContent = reader["QuestionContent"] as string,
                                OptionContent = reader["OptionContent"] as string
                            };
                            questionDataList.Add(info);
                        }
                        return questionDataList;
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