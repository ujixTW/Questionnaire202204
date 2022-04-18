﻿using Questionnaire202204.Helpers;
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
        //增加

        //刪除
        /// <summary>
        /// 軟刪除所選擇的問卷
        /// </summary>
        /// <param name="questionnaireIDList">問卷ID</param>
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
                Logger.WriteLog("Questionnaire202204.Manager.QuestionnaireManager.GetQuestionnaireList", ex);
                throw;
            }

        }
        //修改
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
            DateTime startDate = DateTime.Today;
            DateTime endDate = DateTime.Today;
            string whereText = string.Empty;

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
            string whatTimeStartCondition = string.Empty;
            if (!string.IsNullOrWhiteSpace(startTime))
            {
                startDate = DateTime.Parse(startTime);
                whatTimeStartCondition = " AND CreateTime >= '%'+@startDate+'%' ";
            }

            //帶入結束時間並將輸入內容轉換為DateTime
            string whatTimeEndCondition = string.Empty;
            if (!string.IsNullOrWhiteSpace(endTime))
            {
                endDate = DateTime.Parse(endTime);
                whatTimeEndCondition = " AND EndTime <= '%'+@endDate+'%' ";
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
                                ORDER BY [CreateTime]
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
                        command.Parameters.AddWithValue("@startTime", startDate);
                        command.Parameters.AddWithValue("@endTime", endDate);
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
                                EndTime = (DateTime?)reader["EndTime"],
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

    }
}