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
        /// <summary>
        /// 新增常用問題資料
        /// </summary>
        /// <param name="model">欲增加的常用問題</param>
        public static void CreateCommonlyQuestion(CommonlyQuestionModel model)
        {
            string connStr = ConfigHelper.GetConnectionString();
            string commandText = $@"
                                INSERT INTO [CommonlyQuestion]
                                    ([QuestionID], [NO], [Name],[Type],[QuestionContent]
                                    ,[QuestionOption],[IsRequired], [IsEnable])
                                VALUES
                                    (@QuestionID, @NO, @Name, @Type, @QuestionContent, @QuestionOption, @IsRequired, @IsEnable)
                                ";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        command.Parameters.AddWithValue("@QuestionID", model.QuestionID);
                        command.Parameters.AddWithValue("@NO", model.NO);
                        command.Parameters.AddWithValue("@Name", model.Name);
                        command.Parameters.AddWithValue("@Type", model.Type);
                        command.Parameters.AddWithValue("@QuestionContent", model.QuestionContent);
                        command.Parameters.AddWithValue("@QuestionOption", (object)model.QuestionOption ?? DBNull.Value);
                        command.Parameters.AddWithValue("@IsRequired", model.IsRequired);
                        command.Parameters.AddWithValue("@IsEnable", model.IsEnable);

                        conn.Open();
                        command.ExecuteNonQuery();

                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("Questionnaire202204.Manager.QuestionnaireManager.CreateCommonlyQuestion", ex);
                throw;
            }
        }
        //刪除
        /// <summary>
        /// 刪除所選擇的常用問題
        /// </summary>
        /// <param name="questionIDList">欲刪除的常用問題ID</param>
        public static void DeleteCommonlyQuestion(List<Guid> questionIDList)
        {
            var questionIDText = "";
            for (var i = 0; i < questionIDList.Count; i++)
            {
                questionIDText += (i != 0) ? "," : "";
                questionIDText += $"@QuestionID{i}";
            }

            string connStr = ConfigHelper.GetConnectionString();
            string commandText = $@"
                                DELETE [CommonlyQuestion]
                                WHERE
                                    QuestionID IN ( {questionIDText} )

                                declare @num INT
                                select @num = 0
                                UPDATE CommonlyQuestion
                                SET @num = @num + 1,
                                     [CommonlyQuestion].[NO] = @num
                                ";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        for (var i = 0; i < questionIDList.Count; i++)
                        {
                            command.Parameters.AddWithValue("@QuestionID" + i, questionIDList[i]);
                        }


                        conn.Open();
                        command.ExecuteNonQuery();

                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("Questionnaire202204.Manager.QuestionnaireManager.DeleteCommonlyQuestion", ex);
                throw;
            }
        }
        //修改
        /// <summary>
        /// 修改單筆常用問題資料
        /// </summary>
        /// <param name="model">欲修改的常用問題</param>
        public static void UpdateCommonlyQuestion(CommonlyQuestionModel model)
        {
            string connStr = ConfigHelper.GetConnectionString();
            string commandText = $@"
                                UPDATE [CommonlyQuestion]
                                SET
                                    [Name] = @Name, [Type] = @Type, [QuestionContent] = @QuestionContent
                                    , [QuestionOption] = @QuestionOption, [IsRequired] = @IsRequired,[IsEnable] = @IsEnable
                                WHERE
                                    [QuestionID] = @QuestionID
                                ";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        command.Parameters.AddWithValue("@QuestionID", model.QuestionID);
                        command.Parameters.AddWithValue("@Name", model.Name);
                        command.Parameters.AddWithValue("@Type", model.Type);
                        command.Parameters.AddWithValue("@QuestionContent", model.QuestionContent);
                        command.Parameters.AddWithValue("@QuestionOption", (object)model.QuestionOption ?? DBNull.Value);
                        command.Parameters.AddWithValue("@IsRequired", model.IsRequired);
                        command.Parameters.AddWithValue("@IsEnable", model.IsEnable);

                        conn.Open();
                        command.ExecuteNonQuery();

                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("Questionnaire202204.Manager.QuestionnaireManager.UpdateCommonlyQuestion", ex);
                throw;
            }
        }
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
                                    [QuestionID],[NO],[Name],[Type],[QuestionContent]
                                    ,[QuestionOption],[IsRequired]
                                FROM [Questionnaire202204].[dbo].[CommonlyQuestion]
                                WHERE [IsEnable] = 'true'
                                ORDER BY [NO] DESC
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
                                NO = (int)reader["NO"],
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

        /// <summary>
        /// 查詢多筆管理用常用問題資料
        /// </summary>
        /// <returns>管理用常用問題資料</returns>
        public static List<CommonlyQuestionModel> GetEditCommonlyQuestionList(int pageSize, int pageIndex, out int totalRows)
        {
            //計算跳頁數
            int skip = pageSize * (pageIndex - 1);
            if (skip < 0)
                skip = 0;

            //連接資料庫用文字
            string connStr = ConfigHelper.GetConnectionString();
            string commandText = $@"
                                SELECT TOP {pageSize}
                                    [QuestionID],[NO],[Name],[Type],[QuestionContent]
                                    ,[QuestionOption],[IsRequired],[IsEnable]
                                FROM [Questionnaire202204].[dbo].[CommonlyQuestion]
                                WHERE 
                                      [CommonlyQuestion].[QuestionID] NOT IN(
                                            SELECT TOP {skip} [QuestionID]
                                            FROM [CommonlyQuestion] 
                                            ORDER BY [NO]
                                      ) 
                                ORDER BY [NO] DESC
                                ";
            string commandCountText =
                $@" SELECT COUNT([CommonlyQuestion].[QuestionID])
                    FROM [CommonlyQuestion]
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
                                NO = (int)reader["NO"],
                                Name = reader["Name"] as string,
                                Type = (int)reader["Type"],
                                QuestionContent = reader["QuestionContent"] as string,
                                QuestionOption = reader["QuestionOption"] as string,
                                IsRequired = (bool)reader["IsRequired"],
                                IsEnable = (bool)reader["IsEnable"]
                            };
                            commonlyQuestionDataList.Add(info);
                        }
                        reader.Close();

                        //取得總筆數
                        command.CommandText = commandCountText;

                        totalRows = (int)command.ExecuteScalar();

                        return commonlyQuestionDataList;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("Questionnaire202204.Manager.QuestionnaireManager.GetEditCommonlyQuestionList", ex);
                throw;
            }
        }
        public static CommonlyQuestionModel GetCommonlyQuestion(Guid commonlyQusID)
        {
            string connStr = ConfigHelper.GetConnectionString();
            string commandText = $@"
                                SELECT
                                    [QuestionID],[NO],[Name],[Type],[QuestionContent]
                                    ,[QuestionOption],[IsRequired],[IsEnable]
                                FROM [Questionnaire202204].[dbo].[CommonlyQuestion]
                                WHERE [QuestionID] = @QuestionID
                                ";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        command.Parameters.AddWithValue("@QuestionID", commonlyQusID);
                        conn.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        CommonlyQuestionModel model = new CommonlyQuestionModel();

                        //將資料取出放到List中
                        if (reader.Read())
                        {
                            model.QuestionID = (Guid)reader["QuestionID"];
                            model.NO = (int)reader["NO"];
                            model.Name = reader["Name"] as string;
                            model.Type = (int)reader["Type"];
                            model.QuestionContent = reader["QuestionContent"] as string;
                            model.QuestionOption = reader["QuestionOption"] as string;
                            model.IsRequired = (bool)reader["IsRequired"];
                            model.IsEnable = (bool)reader["IsEnable"];

                        }

                        return model;
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