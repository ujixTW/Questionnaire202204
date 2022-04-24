using Questionnaire202204.Helpers;
using Questionnaire202204.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Questionnaire202204.Managers
{
    public class UserAnswerManager
    {
        //增加
        //查詢
        /// <summary>
        /// 查詢單筆問卷的填寫紀錄
        /// </summary>
        /// <param name="questionnaireID">查詢問卷ID</param>
        /// <param name="totalRows">實際筆數</param>
        public static List<UserDataModel> GetUserDataList(Guid questionnaireID, out int totalRows)
        {
            //連接資料庫用文字
            string connStr = ConfigHelper.GetConnectionString();
            string commandText = $@"
                                 SELECT
                                    [UserID], [QuestionnaireID], [NO], [Name], [Mobile],
                                    [Email], [Age], [CreateTime]
                                 FROM [UserData]
                                 WHERE 
                                       [QuestionnaireID] = @QuestionnaireID 
                                 ORDER BY [CreateTime] DESC
                                 ";
            string commandCountText =
                $@" SELECT COUNT([UserData].[UserID])
                    FROM [UserData]
                    WHERE  [QuestionnaireID] = @QuestionnaireID
                ";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        command.Parameters.AddWithValue("@QuestionnaireID", questionnaireID);
                        conn.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        List<UserDataModel> UserDataList = new List<UserDataModel>();

                        //將資料取出放到List中
                        while (reader.Read())
                        {

                            UserDataModel info = new UserDataModel()
                            {
                                UserID = (Guid)reader["UserID"],
                                QuestionnaireID = (Guid)reader["QuestionnaireID"],
                                NO = (int)reader["NO"],
                                Name = reader["Name"] as string,
                                Mobile = reader["Mobile"] as string,
                                Email = reader["Email"] as string,
                                Age = (int)reader["Age"],
                                CreateTime = (DateTime)reader["CreateTime"]
                            };
                            UserDataList.Add(info);
                        }
                        reader.Close();

                        //取得總筆數
                        command.CommandText = commandCountText;

                        totalRows = (int)command.ExecuteScalar();
                        return UserDataList;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("Questionnaire202204.Manager.UserAnswerManager.GetUserDataList", ex);
                throw;
            }
        }

        /// <summary>
        /// 取得使用者作答的詳細資料
        /// </summary>
        /// <param name="userID">使用者ID</param>
        /// <param name="questionnaireID">問卷ID</param>
        /// <returns></returns>
        public static List<UserAnswerModel> GetUserAnswer(Guid userID, Guid questionnaireID)
        {
            //連接資料庫用文字
            string connStr = ConfigHelper.GetConnectionString();
            string commandText = $@"
                                SELECT
                                   [UserID], [UserAnswer].[QuestionnaireID], [UserAnswer].[QuestionID], [OptionNO], [Answer]
                                FROM [UserAnswer]
                                INNER JOIN [Question]
                                ON [UserAnswer].[QuestionID]=[Question].[QuestionID]
                                WHERE
                                    [UserAnswer].[UserID] = @UserID AND
                                    [UserAnswer].[QuestionnaireID] = @QuestionnaireID
                                ORDER BY [Question].[NO]
                                ";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        command.Parameters.AddWithValue("@UserID", userID);
                        command.Parameters.AddWithValue("@QuestionnaireID", questionnaireID);
                        conn.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        List<UserAnswerModel> UserAnswerList = new List<UserAnswerModel>();
                        //將資料取出放到List中
                        while (reader.Read())
                        {
                            UserAnswerModel info = new UserAnswerModel()
                            {
                                UserID = (Guid)reader["UserID"],
                                QuestionnaireID = (Guid)reader["QuestionnaireID"],
                                QuestionID = reader["QuestionID"] as string,
                                OptionNO = reader["OptionNO"] as int?,
                                Answer = reader["Answer"] as string
                            };
                            UserAnswerList.Add(info);
                        }

                        return UserAnswerList;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("Questionnaire202204.Manager.UserAnswerManager.GetUserAnswer", ex);
                throw;
            }
        }

        /// <summary>
        /// 取得該問卷所有使用者作答的詳細資料
        /// </summary>
        /// <param name="questionnaireID">問卷ID</param>
        /// <returns></returns>
        public static List<UserAnswerModel> GetUserAnswerList(Guid questionnaireID)
        {
            //連接資料庫用文字
            string connStr = ConfigHelper.GetConnectionString();
            string commandText = $@"
                                    SELECT
                                       [UserID], [User].[QuestionnaireID], [User].[QuestionID], [OptionNO], [Answer]
                                    FROM [Question]
                                    INNER JOIN (
                                    SELECT
                                      [UserAnswer].[UserID], [UserAnswer].[QuestionnaireID], [UserAnswer].[QuestionID], [OptionNO], [Answer],[NO]
                                    FROM [UserAnswer]
                                    INNER JOIN [UserData]
                                    ON [UserAnswer].[UserID]=[UserData].[UserID]
                                    WHERE
                                        [UserAnswer].[QuestionnaireID] = @QuestionnaireID
                                    )  AS [User]
                                    ON [User].[QuestionID]=[Question].[QuestionID]
                                    WHERE
                                        [User].[QuestionnaireID] = @QuestionnaireID
                                    ORDER BY [User].[NO] , [Question].[NO]
                                ";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        command.Parameters.AddWithValue("@QuestionnaireID", questionnaireID);
                        conn.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        List<UserAnswerModel> UserAnswerList = new List<UserAnswerModel>();
                        //將資料取出放到List中
                        while (reader.Read())
                        {
                            UserAnswerModel info = new UserAnswerModel()
                            {
                                UserID = (Guid)reader["UserID"],
                                QuestionnaireID = (Guid)reader["QuestionnaireID"],
                                QuestionID = reader["QuestionID"] as string,
                                OptionNO = reader["OptionNO"] as int?,
                                Answer = reader["Answer"] as string
                            };
                            UserAnswerList.Add(info);
                        }

                        return UserAnswerList;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("Questionnaire202204.Manager.UserAnswerManager.GetUserAnswerList", ex);
                throw;
            }
        }
        /// <summary>
        /// 取得單一問卷選擇題的統計資料清單
        /// </summary>
        /// <param name="questionnaireID">問卷ID</param>
        /// <returns>單一問卷選擇題的統計資料清單</returns>
        public static List<UserAnswerStatisticsModel> GetAnswerStatisticsList(Guid questionnaireID)
        {
            //連接資料庫用文字
            string connStr = ConfigHelper.GetConnectionString();
            string commandText = $@"
                                SELECT QuestionID
                                      ,[OptionNO]
                                      ,COUNT([Answer]) AS 'AnswerStatistics'
                                  FROM [Questionnaire202204].[dbo].[UserAnswer]
                                  WHERE OptionNO IS NOT NULL  AND QuestionnaireID = @QuestionnaireID
                                  GROUP BY [UserAnswer].QuestionID,[OptionNO]
                                  ORDER BY QuestionID,[OptionNO]
                                ";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        command.Parameters.AddWithValue("@QuestionnaireID", questionnaireID);
                        conn.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        List<UserAnswerStatisticsModel> answerStatisticsList = new List<UserAnswerStatisticsModel>();
                        //將資料取出放到List中
                        while (reader.Read())
                        {
                            UserAnswerStatisticsModel info = new UserAnswerStatisticsModel()
                            {
                                QuestionID = reader["QuestionID"] as string,
                                OptionNO = (int)reader["OptionNO"],
                                AnswerStatistics = (int)reader["AnswerStatistics"]
                            };
                            answerStatisticsList.Add(info);
                        }

                        return answerStatisticsList;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("Questionnaire202204.Manager.UserAnswerManager.GetUserAnswerList", ex);
                throw;
            }
        }

    }
}