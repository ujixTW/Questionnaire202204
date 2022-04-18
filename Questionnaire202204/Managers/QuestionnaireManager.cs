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
        //增加
        //刪除
        //修改
        //查詢
        /// <summary>
        /// 查詢多筆問卷資料
        /// </summary>
        /// <param name="pageSize">每頁最大筆數</param>
        /// <param name="pageIndex">目前頁數</param>
        /// <param name="totalRows">每頁實際筆數</param>
        /// <returns>多筆問卷資料，及OUT該頁實際資料筆數</returns>
        public static List<QuestionnaireModel> GetQuestionnaireList(int pageSize, int pageIndex, out int totalRows)
        {
           
            //計算跳頁數
            int skip = pageSize * (pageIndex - 1);
            if (skip < 0)
                skip = 0;
           
            string connStr = ConfigHelper.GetConnectionString();
            string commandText = $@"
                                 SELECT TOP ({pageSize})
                                    [QuestionnaireID],[Title],[Briefly]
                                    ,[StartTime],[EndTime],[CreateTime],[IsEnable]
                                 FROM [Questionnaire]
                                WHERE 
                                      [Questionnaire].[QuestionnaireID] NOT IN(
                                            SELECT TOP {skip} [QuestionnaireID]
                                            FROM [Questionnaire] 
                                            ORDER BY [CreateTime]
                                      )
                                ORDER BY [CreateTime]
                                ";
            string commandCountText =
                $@" SELECT COUNT([Questionnaire].[QuestionnaireID])
                    FROM [Questionnaire]
                ";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        
                        conn.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        List<QuestionnaireModel> QuestionnaireDataList = new List<QuestionnaireModel>();
                        //將資料取出放到List中
                        while (reader.Read())
                        {
                            QuestionnaireModel info = new QuestionnaireModel()
                            {

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
                Logger.WriteLog("Sakei.Manager.TestSystemManagers.TestDataManager.GetTestDataList", ex);
                throw;
            }
        }

    }
}