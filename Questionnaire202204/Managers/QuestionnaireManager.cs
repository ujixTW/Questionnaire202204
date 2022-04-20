using Questionnaire202204.Helpers;
using Questionnaire202204.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Questionnaire202204.Managers
{
    public class QuestionnaireManager
    {
        /// <summary>
        /// 儲存單筆問卷資料
        /// </summary>
        /// <param name="questionnaire">存入的問卷資料</param>
        public static void SaveQuestionnaireData(QuestionnaireModel questionnaire)
        {
            //判斷是否為新問卷
            if (questionnaire.NO == null)
            {
                //新
                CreateQuestionnaireData(questionnaire);
            }else
            {
                //已有
                UpDateQuestionnaireDeta(questionnaire);
            }
        }
        //增加
        /// <summary>
        /// 新增單筆問卷資料
        /// </summary>
        /// <param name="questionnaire">存入的問卷資料</param>
        public static void CreateQuestionnaireData(QuestionnaireModel questionnaire)
        {
           
            string connStr = ConfigHelper.GetConnectionString();
            string commandText = $@"
                                INSERT INTO [Questionnaire]
                                    ( [QuestionnaireID], [Title], [Briefly], [StartTime], [EndTime], [IsEnable], [IsDelete])
                                VALUES
                                    ( @questionnaireID, @title, @briefly, @startTime, @endTime, @isEnable,'false' )
                                ";

            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        command.Parameters.AddWithValue("@questionnaireID", questionnaire.QuestionnaireID);
                        command.Parameters.AddWithValue("@title", (object)questionnaire.Title ?? DBNull.Value);
                        command.Parameters.AddWithValue("@briefly", (object)questionnaire.Briefly ?? DBNull.Value);
                        command.Parameters.AddWithValue("@startTime", questionnaire.StartTime);
                        command.Parameters.AddWithValue("@endTime", (object)questionnaire.EndTime ?? DBNull.Value);
                        command.Parameters.AddWithValue("@isEnable", questionnaire.IsEnable);
                        conn.Open();
                        command.ExecuteNonQuery();


                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("Questionnaire202204.Manager.QuestionnaireManager.CreateQuestionnaireData", ex);
                throw;
            }
        }

        //刪除
        /// <summary>
        /// 軟刪除所選擇的問卷
        /// </summary>
        /// <param name="questionnaireIDList">被刪除的問卷ID</param>
        public static void DeleteQuestionnaireList(List<Guid> questionnaireIDList)
        {
            string idText = string.Empty;
            for (var i = 0; i < questionnaireIDList.Count; i++)
            {
                if (i != 0)
                    idText += "," + $"'{questionnaireIDList[i]}'";
                else
                    idText += $"'{questionnaireIDList[i]}'";
            }

            string connStr = ConfigHelper.GetConnectionString();
            string commandText = $@"
                                UPDATE [Questionnaire]
                                SET
                                    [IsDelete]='true'
                                WHERE
                                    [QuestionnaireID] IN ({idText})
                                ";

            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {

                        conn.Open();
                        command.ExecuteNonQuery();


                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("Questionnaire202204.Manager.QuestionnaireManager.DeleteQuestionnaireList", ex);
                throw;
            }

        }
        //修改
        /// <summary>
        /// 更新單筆現有問卷資料
        /// </summary>
        /// <param name="questionnaire">更新的問卷資料</param>
        public static void UpDateQuestionnaireDeta(QuestionnaireModel questionnaire)
        {
            string connStr = ConfigHelper.GetConnectionString();
            string commandText = $@"
                                UPDATE [Questionnaire]
                                SET
                                    [Title] = @title,
                                    [Briefly] = @briefly,
                                    [StartTime] = @startTime,
                                    [EndTime] = @endTime,
                                    [IsEnable] = @isEnable
                                WHERE
                                    [QuestionnaireID] = @questionnaireID
                                ";

            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        command.Parameters.AddWithValue("@questionnaireID", questionnaire.QuestionnaireID);
                        command.Parameters.AddWithValue("@title", questionnaire.Title);
                        command.Parameters.AddWithValue("@briefly", (object)questionnaire.Briefly ?? DBNull.Value);
                        command.Parameters.AddWithValue("@startTime", questionnaire.StartTime);
                        command.Parameters.AddWithValue("@endTime", (object)questionnaire.EndTime ?? DBNull.Value);
                        command.Parameters.AddWithValue("@isEnable", questionnaire.IsEnable);
                        conn.Open();
                        command.ExecuteNonQuery();


                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("Questionnaire202204.Manager.QuestionnaireManager.UpDateQuestionnaireDeta", ex);
                throw;
            }

        }
        //查詢
        /// <summary>
        /// 查詢多筆問卷資料
        /// </summary>
        /// <param name="pageSize">每頁最大筆數</param>
        /// <param name="pageIndex">目前頁數</param>
        /// <param name="totalRows">實際筆數</param>
        /// <returns>多筆問卷資料，及OUT該頁實際資料筆數</returns>
        public static List<QuestionnaireModel> GetQuestionnaireList(string keyword, string startTime, string endTime, int pageSize, int pageIndex, out int totalRows)
        {
            //計算跳頁數
            int skip = pageSize * (pageIndex - 1);
            if (skip < 0)
                skip = 0;

            //帶入關鍵字
            string whereCondition = string.Empty;
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                whereCondition = " AND Title LIKE '%'+@keyword+'%' ";
            }
            else
            {
                keyword = string.Empty;
            }

            //帶入起始時間並將輸入內容轉換為DateTime
            string startDate = string.Empty;
            string whatTimeStartCondition = string.Empty;
            if (!string.IsNullOrWhiteSpace(startTime))
            {
                startDate = DateTime.Parse(startTime).ToString("yyyy-MM-dd");
                whatTimeStartCondition = " AND StartTime >= @startDate ";
            }

            //帶入結束時間並將輸入內容轉換為DateTime
            string endDate = string.Empty;
            string whatTimeEndCondition = string.Empty;
            if (!string.IsNullOrWhiteSpace(endTime))
            {
                endDate = DateTime.Parse(endTime).ToString("yyyy-MM-dd");
                whatTimeEndCondition = " AND EndTime <= @endDate ";
            }

            //連接資料庫用文字
            string connStr = ConfigHelper.GetConnectionString();
            string commandText = $@"
                                 SELECT TOP ({pageSize})
                                    [QuestionnaireID],[NO],[Title],[Briefly]
                                    ,[StartTime],[EndTime],[CreateTime],[IsEnable]
                                 FROM [Questionnaire]
                                WHERE 
                                      [IsDelete]='false' AND
                                      [Questionnaire].[QuestionnaireID] NOT IN(
                                            SELECT TOP {skip} [QuestionnaireID]
                                            FROM [Questionnaire] 
                                            ORDER BY [CreateTime]
                                      ) 
                                        {whereCondition} {whatTimeStartCondition} {whatTimeEndCondition}
                                ORDER BY [NO] DESC
                                ";
            string commandCountText =
                $@" SELECT COUNT([Questionnaire].[QuestionnaireID])
                    FROM [Questionnaire]
                    WHERE [IsDelete]='false' {whereCondition} {whatTimeStartCondition} {whatTimeEndCondition}
                ";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        command.Parameters.AddWithValue("@keyword", keyword);
                        command.Parameters.AddWithValue("@startDate", startDate);
                        command.Parameters.AddWithValue("@endDate", endDate);
                        conn.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        List<QuestionnaireModel> QuestionnaireDataList = new List<QuestionnaireModel>();

                        //將資料取出放到List中
                        while (reader.Read())
                        {
                           
                            QuestionnaireModel info = new QuestionnaireModel()
                            {
                                QuestionnaireID = (Guid)reader["QuestionnaireID"],
                                NO = (int)reader["NO"],
                                Title = reader["Title"] as string,
                                Briefly = reader["Briefly"] as string,
                                StartTime = (DateTime)reader["StartTime"],
                                EndTime = reader["EndTime"] as DateTime?,
                                IsEnable = (bool)reader["IsEnable"]
                            };
                            QuestionnaireDataList.Add(info);
                        }
                        reader.Close();

                        //取得總筆數
                        command.CommandText = commandCountText;

                        totalRows = (int)command.ExecuteScalar();
                        return QuestionnaireDataList;
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
        /// 後台查詢單筆問卷資料
        /// </summary>
        /// <param name="questionnaireID">問卷ID</param>
        /// <returns></returns>
        public static QuestionnaireModel GetQuestionnaireData(Guid questionnaireID)
        {
            string connStr = ConfigHelper.GetConnectionString();
            string commandText = $@"
                                SELECT TOP (1000) 
                                    [QuestionnaireID],[NO],[Title],[Briefly],[StartTime]
                                     ,[EndTime],[CreateTime],[IsEnable]
                                FROM [Questionnaire202204].[dbo].[Questionnaire]
                                WHERE [QuestionnaireID] = @questionnaireID
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
                        QuestionnaireModel model = new QuestionnaireModel();
                        //將資料取出放到List中
                        if (reader.Read())
                        {
                            model.QuestionnaireID = questionnaireID;
                            model.NO = (int?)reader["NO"];
                            model.Title = reader["Title"] as string;
                            model.Briefly = reader["Briefly"] as string;
                            model.StartTime = (DateTime)reader["StartTime"];
                            model.EndTime = reader["EndTime"] as DateTime?;
                            model.IsEnable = (bool)reader["IsEnable"];
                        }
                        else
                        {
                            model.QuestionnaireID = questionnaireID;
                            model.Title = "新問卷";
                            model.StartTime = DateTime.Today;
                            model.IsEnable = true;
                        }
                        return model;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("Questionnaire202204.Manager.QuestionnaireManager.GetQuestionnaireData", ex);
                throw;
            }
        }

    }
}