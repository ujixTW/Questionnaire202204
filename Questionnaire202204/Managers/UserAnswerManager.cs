﻿using Questionnaire202204.Helpers;
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
                                NO=(int)reader["NO"],
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
    }
}