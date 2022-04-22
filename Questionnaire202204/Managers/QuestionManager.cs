using Questionnaire202204.Helpers;
using Questionnaire202204.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Questionnaire202204.Managers
{
    public class QuestionManager
    {

        //修改
        /// <summary>
        /// 更新所選擇的問題
        /// </summary>
        /// <param name="questionList">問卷問題資料</param>
        /// <param name="questionList">資料庫現有相關問卷問題資料數</param>
        public static void UpDateQuestionList(List<QuestionModel> questionList, int dbDataCount)
        {
            if (dbDataCount > questionList.Count)
            {
                //資料數少於資料庫
                UpDateQuestionListLess(questionList);
            }
            else if (dbDataCount < questionList.Count)
            {
                //資料數多於資料庫
                UpDateQuestionListMore(questionList, dbDataCount);
            }
            else if (dbDataCount == questionList.Count && dbDataCount != 0)
            {
                //資料數等於資料庫
                UpDateQuestionListEqual(questionList);
            }
        }

        /// <summary>
        /// 更新所選擇的問題(問題數多於資料庫問題數)
        /// </summary>
        /// <param name="questionList">問卷問題資料</param>
        private static void UpDateQuestionListEqual(List<QuestionModel> questionList)
        {

            string connStr = ConfigHelper.GetConnectionString();
            string commandText = "";
            for (var i = 0; i < questionList.Count; i++)
            {
                commandText = UpDateQuestionListText(commandText, i);
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        SqlUpDateParametersAdd(questionList, command);

                        conn.Open();
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("Questionnaire202204.Manager.QuestionnaireManager.UpDateQuestionListEqual", ex);
                throw;
            }
        }


        /// <summary>
        /// 更新所選擇的問題(問題數多於資料庫問題數)
        /// </summary>
        /// <param name="questionList">問卷問題資料</param>
        /// <param name="questionList">資料庫現有相關問卷問題資料數</param>
        private static void UpDateQuestionListMore(List<QuestionModel> questionList, int dbDataCount)
        {
            string insertText = string.Empty;
            string qusUpDateText = string.Empty;

            for (var i = dbDataCount; i < questionList.Count; i++)
            {
                insertText += $@"
                                INSERT INTO [Question]
                                    ( [QuestionID], [QuestionnaireID], [NO], [Type], [QuestionContent], [OptionContent], [IsRequired] )
                                VALUES
                                    ( @QuestionID{i}, @QuestionnaireID{i}, @NO{i}, @QusType{i}, @QuestionContent{i}, @OptionContent{i}, @IsRequired{i} )
                                ";
            }
            for (var i = 0; i < questionList.Count; i++)
            {
                qusUpDateText = UpDateQuestionListText(qusUpDateText, i);
            }


            string connStr = ConfigHelper.GetConnectionString();
            string commandText = $@"{qusUpDateText}{insertText}";

            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        SqlUpDateParametersAdd(questionList, command);
                        conn.Open();
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("Questionnaire202204.Manager.QuestionnaireManager.UpDateQuestionListMore", ex);
                throw;
            }
        }



        /// <summary>
        /// 更新所選擇的問題(問題數少於資料庫問題數)
        /// </summary>
        /// <param name="questionList">問卷問題資料</param>
        private static void UpDateQuestionListLess(List<QuestionModel> questionList)
        {

            string qusUpDateText = string.Empty;
            string deleteText = string.Empty;
            for (var i = 0; i < questionList.Count; i++)
            {
                qusUpDateText += UpDateQuestionListText(qusUpDateText, i);
            }

            //刪除資料
            deleteText = $@"
                          DELETE [Question]
                          WHERE
                              [QuestionnaireID] = @QuestionnaireID0 AND
                              [NO] >{questionList.Count}
                          ";


            string connStr = ConfigHelper.GetConnectionString();
            string commandText = $@"{deleteText}{qusUpDateText}";

            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        SqlUpDateParametersAdd(questionList, command);

                        conn.Open();
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("Questionnaire202204.Manager.QuestionnaireManager.DeleteQuestionList", ex);
                throw;
            }

        }

        /// <summary>
        /// 更新多筆資料庫問題資料用字串
        /// </summary>
        /// <param name="upDateText">存放字串本體用記憶體空間</param>
        /// <param name="i">現在為第幾筆資料</param>
        /// <returns>更新多筆資料庫問題資料用字串</returns>
        private static string UpDateQuestionListText(string upDateText, int i)
        {
            upDateText += $@"
                                UPDATE [Question]
                                SET
                                    [QuestionID] = @QuestionID{i}, [QuestionnaireID] = @QuestionnaireID{i}, [Type] = @QusType{i},
                                    [QuestionContent] = @QuestionContent{i}, [OptionContent] = @OptionContent{i}, [IsRequired] = @IsRequired{i}
                                
                                WHERE
                                    [NO] =@NO{i} AND
                                    [QuestionnaireID] = @QuestionnaireID{i}
                                    ";
            return upDateText;
        }

        /// <summary>
        /// 加入資料進更新問卷問題用的Parameters
        /// </summary>
        /// <param name="questionList">問題清單</param>
        /// <param name="command">連接資料庫用SqlCommand資料</param>
        private static void SqlUpDateParametersAdd(List<QuestionModel> questionList, SqlCommand command)
        {
            for (var i = 0; i < questionList.Count; i++)
            {
                command.Parameters.AddWithValue("@QuestionID" + i, questionList[i].QuestionID);
                command.Parameters.AddWithValue("@QuestionnaireID" + i, questionList[i].QuestionnaireID);
                command.Parameters.AddWithValue("@NO" + i, questionList[i].NO);
                command.Parameters.AddWithValue("@QusType" + i, questionList[i].QusType);
                command.Parameters.AddWithValue("@QuestionContent" + i, questionList[i].QuestionContent);
                command.Parameters.AddWithValue("@OptionContent" + i, questionList[i].OptionContent);
                command.Parameters.AddWithValue("@IsRequired" + i, questionList[i].IsRequired);
            }
        }


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
                                OptionContent = reader["OptionContent"] as string,
                                IsRequired = (bool)reader["IsRequired"]
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