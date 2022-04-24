USE [Questionnaire202204]
GO
SET IDENTITY_INSERT [dbo].[Questionnaire] ON 

INSERT [dbo].[Questionnaire] ([QuestionnaireID], [NO], [Title], [Briefly], [StartTime], [EndTime], [CreateTime], [IsEnable], [IsDelete]) VALUES (N'1e3b42d6-4a9c-4769-8856-0596fde0d222', 1, N'123', N'sadasdsada', CAST(N'2022-04-19' AS Date), CAST(N'2024-05-20' AS Date), CAST(N'2022-04-19T00:37:22.027' AS DateTime), 1, 0)
INSERT [dbo].[Questionnaire] ([QuestionnaireID], [NO], [Title], [Briefly], [StartTime], [EndTime], [CreateTime], [IsEnable], [IsDelete]) VALUES (N'563cb276-4ea6-4a85-9ca1-4f0b384d3424', 5, N'156465', N'sdadaswq8484', CAST(N'2022-04-19' AS Date), CAST(N'2022-04-30' AS Date), CAST(N'2022-04-19T00:38:50.757' AS DateTime), 1, 0)
INSERT [dbo].[Questionnaire] ([QuestionnaireID], [NO], [Title], [Briefly], [StartTime], [EndTime], [CreateTime], [IsEnable], [IsDelete]) VALUES (N'eb047e49-181c-46d1-a140-50073aa473dd', 11, N'新問卷', NULL, CAST(N'2022-04-22' AS Date), NULL, CAST(N'2022-04-20T22:59:50.570' AS DateTime), 1, 0)
INSERT [dbo].[Questionnaire] ([QuestionnaireID], [NO], [Title], [Briefly], [StartTime], [EndTime], [CreateTime], [IsEnable], [IsDelete]) VALUES (N'3e4fab46-6c2a-49f4-840c-51328419aa87', 8, N'新問卷', N'54645645464', CAST(N'2022-04-20' AS Date), NULL, CAST(N'2022-04-20T20:20:17.443' AS DateTime), 1, 0)
INSERT [dbo].[Questionnaire] ([QuestionnaireID], [NO], [Title], [Briefly], [StartTime], [EndTime], [CreateTime], [IsEnable], [IsDelete]) VALUES (N'9a5a1663-f1b6-4fe0-bf51-518acb6b7592', 4, N'sadda', N'd656+6wqe5w', CAST(N'2022-04-22' AS Date), CAST(N'2022-04-20' AS Date), CAST(N'2022-04-19T00:37:55.287' AS DateTime), 1, 0)
INSERT [dbo].[Questionnaire] ([QuestionnaireID], [NO], [Title], [Briefly], [StartTime], [EndTime], [CreateTime], [IsEnable], [IsDelete]) VALUES (N'a7eb9572-b774-48c0-b59a-53451117c976', 9, N'新問卷123', N'546', CAST(N'2022-04-20' AS Date), NULL, CAST(N'2022-04-20T22:24:40.063' AS DateTime), 1, 0)
INSERT [dbo].[Questionnaire] ([QuestionnaireID], [NO], [Title], [Briefly], [StartTime], [EndTime], [CreateTime], [IsEnable], [IsDelete]) VALUES (N'61d28415-8c53-43cc-8bae-c44a6e7922f8', 7, N' ', N' ', CAST(N'2022-04-20' AS Date), NULL, CAST(N'2022-04-20T15:28:10.313' AS DateTime), 1, 0)
SET IDENTITY_INSERT [dbo].[Questionnaire] OFF
GO
INSERT [dbo].[Question] ([QuestionID], [QuestionnaireID], [NO], [Type], [QuestionContent], [OptionContent], [IsRequired]) VALUES (N'12345678901234567980', N'1e3b42d6-4a9c-4769-8856-0596fde0d222', 1, 1, N'223123', NULL, 0)
INSERT [dbo].[Question] ([QuestionID], [QuestionnaireID], [NO], [Type], [QuestionContent], [OptionContent], [IsRequired]) VALUES (N'20220422014854227107', N'a7eb9572-b774-48c0-b59a-53451117c976', 1, 5, N'123', N'1564;hgjhhg;66', 1)
INSERT [dbo].[Question] ([QuestionID], [QuestionnaireID], [NO], [Type], [QuestionContent], [OptionContent], [IsRequired]) VALUES (N'20220422014952204805', N'a7eb9572-b774-48c0-b59a-53451117c976', 2, 1, N'123', N'1564;hgjhhg;66', 0)
INSERT [dbo].[Question] ([QuestionID], [QuestionnaireID], [NO], [Type], [QuestionContent], [OptionContent], [IsRequired]) VALUES (N'20220422015000074031', N'a7eb9572-b774-48c0-b59a-53451117c976', 3, 2, N'問題內容', N'選項1;選項2', 0)
INSERT [dbo].[Question] ([QuestionID], [QuestionnaireID], [NO], [Type], [QuestionContent], [OptionContent], [IsRequired]) VALUES (N'20220422015104867583', N'eb047e49-181c-46d1-a140-50073aa473dd', 1, 5, N'1', N'1;2;1;3', 0)
INSERT [dbo].[Question] ([QuestionID], [QuestionnaireID], [NO], [Type], [QuestionContent], [OptionContent], [IsRequired]) VALUES (N'20220422015204486557', N'eb047e49-181c-46d1-a140-50073aa473dd', 2, 2, N'問題內容', N'選項1;選項2', 1)
INSERT [dbo].[Question] ([QuestionID], [QuestionnaireID], [NO], [Type], [QuestionContent], [OptionContent], [IsRequired]) VALUES (N'20220422020101392172', N'a7eb9572-b774-48c0-b59a-53451117c976', 4, 1, N'123', N'', 0)
INSERT [dbo].[Question] ([QuestionID], [QuestionnaireID], [NO], [Type], [QuestionContent], [OptionContent], [IsRequired]) VALUES (N'20220424014919546539', N'1e3b42d6-4a9c-4769-8856-0596fde0d222', 2, 5, N'問題內容', N'選項1;選項2', 1)
GO
SET IDENTITY_INSERT [dbo].[UserData] ON 

INSERT [dbo].[UserData] ([UserID], [QuestionnaireID], [NO], [Name], [Mobile], [Email], [Age], [CreateTime]) VALUES (N'ad0d672d-6eac-4ee7-8fe3-4d0fc032ff03', N'1e3b42d6-4a9c-4769-8856-0596fde0d222', 2, N'BBB', N'AAAA', N'123456BB@gmail.com', 5, CAST(N'2022-04-22T16:23:39.183' AS DateTime))
INSERT [dbo].[UserData] ([UserID], [QuestionnaireID], [NO], [Name], [Mobile], [Email], [Age], [CreateTime]) VALUES (N'222e6159-ebe3-43cc-988e-c6e637f1a0b7', N'1e3b42d6-4a9c-4769-8856-0596fde0d222', 3, N'CCC', N'CCC', N'123456BB@gmail.com', 28, CAST(N'2022-04-22T16:24:01.793' AS DateTime))
INSERT [dbo].[UserData] ([UserID], [QuestionnaireID], [NO], [Name], [Mobile], [Email], [Age], [CreateTime]) VALUES (N'24c13673-830f-4f9e-ad80-d0ddaf0f021a', N'1e3b42d6-4a9c-4769-8856-0596fde0d222', 1, N'AAA', N'AAAA', N'123456AA@gmail.com', 10, CAST(N'2022-04-22T16:23:13.493' AS DateTime))
SET IDENTITY_INSERT [dbo].[UserData] OFF
GO
INSERT [dbo].[UserAnswer] ([UserID], [QuestionnaireID], [QuestionID], [OptionNO], [Answer]) VALUES (N'ad0d672d-6eac-4ee7-8fe3-4d0fc032ff03', N'1e3b42d6-4a9c-4769-8856-0596fde0d222', N'12345678901234567980', 0, N'asdsadasds')
INSERT [dbo].[UserAnswer] ([UserID], [QuestionnaireID], [QuestionID], [OptionNO], [Answer]) VALUES (N'ad0d672d-6eac-4ee7-8fe3-4d0fc032ff03', N'1e3b42d6-4a9c-4769-8856-0596fde0d222', N'20220424014919546539', 1, N'True')
INSERT [dbo].[UserAnswer] ([UserID], [QuestionnaireID], [QuestionID], [OptionNO], [Answer]) VALUES (N'ad0d672d-6eac-4ee7-8fe3-4d0fc032ff03', N'1e3b42d6-4a9c-4769-8856-0596fde0d222', N'20220424014919546539', 2, N'True')
INSERT [dbo].[UserAnswer] ([UserID], [QuestionnaireID], [QuestionID], [OptionNO], [Answer]) VALUES (N'222e6159-ebe3-43cc-988e-c6e637f1a0b7', N'1e3b42d6-4a9c-4769-8856-0596fde0d222', N'12345678901234567980', 0, N'456')
INSERT [dbo].[UserAnswer] ([UserID], [QuestionnaireID], [QuestionID], [OptionNO], [Answer]) VALUES (N'24c13673-830f-4f9e-ad80-d0ddaf0f021a', N'1e3b42d6-4a9c-4769-8856-0596fde0d222', N'12345678901234567980', 0, N'123')
GO
INSERT [dbo].[CommonlyQuestion] ([QuestionID], [Name], [Type], [QuestionContent], [QuestionOption], [IsRequired], [CreateTime]) VALUES (N'41343334-bc1e-4901-97cf-49a3a1b1725e', N'常用的問題1', 2, N'問題內容', N'選項1;選項2', 0, CAST(N'2022-04-21T13:36:24.113' AS DateTime))
GO
