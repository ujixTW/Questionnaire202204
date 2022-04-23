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
        //刪除
        //修改
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
        public static List<UserAnswerModel> GetUserAnswerList(Guid userID, Guid questionnaireID)
        {
            //連接資料庫用文字
            string connStr = ConfigHelper.GetConnectionString();
            string commandText = $@"
                                SELECT
                                   [UserID], [UserAnswer].[QuestionnaireID], [UserAnswer].[QuestionID], [Answer]
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
    }
}