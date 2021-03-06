USE [master]
GO
/****** Object:  Database [Questionnaire202204]    Script Date: 2022/4/29 下午 04:28:26 ******/
CREATE DATABASE [Questionnaire202204]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Questionnaire202204', FILENAME = N'D:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\Questionnaire202204.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Questionnaire202204_log', FILENAME = N'D:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\Questionnaire202204_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [Questionnaire202204] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Questionnaire202204].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Questionnaire202204] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Questionnaire202204] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Questionnaire202204] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Questionnaire202204] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Questionnaire202204] SET ARITHABORT OFF 
GO
ALTER DATABASE [Questionnaire202204] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Questionnaire202204] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Questionnaire202204] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Questionnaire202204] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Questionnaire202204] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Questionnaire202204] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Questionnaire202204] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Questionnaire202204] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Questionnaire202204] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Questionnaire202204] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Questionnaire202204] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Questionnaire202204] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Questionnaire202204] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Questionnaire202204] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Questionnaire202204] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Questionnaire202204] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Questionnaire202204] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Questionnaire202204] SET RECOVERY FULL 
GO
ALTER DATABASE [Questionnaire202204] SET  MULTI_USER 
GO
ALTER DATABASE [Questionnaire202204] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Questionnaire202204] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Questionnaire202204] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Questionnaire202204] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Questionnaire202204] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Questionnaire202204] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'Questionnaire202204', N'ON'
GO
ALTER DATABASE [Questionnaire202204] SET QUERY_STORE = OFF
GO
USE [Questionnaire202204]
GO
/****** Object:  Table [dbo].[CommonlyQuestion]    Script Date: 2022/4/29 下午 04:28:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CommonlyQuestion](
	[QuestionID] [uniqueidentifier] NOT NULL,
	[NO] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Type] [int] NOT NULL,
	[QuestionContent] [nvarchar](max) NOT NULL,
	[QuestionOption] [nvarchar](max) NULL,
	[IsRequired] [bit] NOT NULL,
	[IsEnable] [bit] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_CommonlyQuestion] PRIMARY KEY CLUSTERED 
(
	[NO] ASC,
	[QuestionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Question]    Script Date: 2022/4/29 下午 04:28:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Question](
	[QuestionID] [char](20) NOT NULL,
	[QuestionnaireID] [uniqueidentifier] NOT NULL,
	[NO] [int] NOT NULL,
	[Type] [int] NOT NULL,
	[QuestionContent] [nvarchar](max) NULL,
	[OptionContent] [nvarchar](max) NULL,
	[IsRequired] [bit] NOT NULL,
 CONSTRAINT [PK_Question] PRIMARY KEY CLUSTERED 
(
	[QuestionID] ASC,
	[QuestionnaireID] ASC,
	[NO] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Questionnaire]    Script Date: 2022/4/29 下午 04:28:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Questionnaire](
	[QuestionnaireID] [uniqueidentifier] NOT NULL,
	[NO] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](50) NULL,
	[Briefly] [nvarchar](max) NULL,
	[StartTime] [date] NOT NULL,
	[EndTime] [date] NULL,
	[CreateTime] [datetime] NOT NULL,
	[IsEnable] [bit] NOT NULL,
	[IsDelete] [bit] NOT NULL,
 CONSTRAINT [PK_Questionnaire] PRIMARY KEY CLUSTERED 
(
	[QuestionnaireID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserAnswer]    Script Date: 2022/4/29 下午 04:28:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserAnswer](
	[UserID] [uniqueidentifier] NOT NULL,
	[QuestionnaireID] [uniqueidentifier] NOT NULL,
	[QuestionID] [char](20) NOT NULL,
	[OptionNO] [int] NOT NULL,
	[Answer] [nvarchar](max) NULL,
 CONSTRAINT [PK_UserAnswer] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC,
	[QuestionID] ASC,
	[QuestionnaireID] ASC,
	[OptionNO] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserData]    Script Date: 2022/4/29 下午 04:28:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserData](
	[UserID] [uniqueidentifier] NOT NULL,
	[QuestionnaireID] [uniqueidentifier] NOT NULL,
	[NO] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Mobile] [varchar](50) NOT NULL,
	[Email] [varchar](50) NOT NULL,
	[Age] [int] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_UserData] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[CommonlyQuestion] ADD  CONSTRAINT [DF_CommonlyQuestion_QuestionID]  DEFAULT (newid()) FOR [QuestionID]
GO
ALTER TABLE [dbo].[CommonlyQuestion] ADD  CONSTRAINT [DF_CommonlyQuestion_IsRequired]  DEFAULT ((0)) FOR [IsRequired]
GO
ALTER TABLE [dbo].[CommonlyQuestion] ADD  CONSTRAINT [DF_CommonlyQuestion_CreateTime]  DEFAULT (getdate()) FOR [CreateTime]
GO
ALTER TABLE [dbo].[Question] ADD  CONSTRAINT [DF_Question_IsRequired]  DEFAULT ((0)) FOR [IsRequired]
GO
ALTER TABLE [dbo].[Questionnaire] ADD  CONSTRAINT [DF_Questionnaire_QuestionnaireID]  DEFAULT (newid()) FOR [QuestionnaireID]
GO
ALTER TABLE [dbo].[Questionnaire] ADD  CONSTRAINT [DF_Questionnaire_StartTime]  DEFAULT (getdate()) FOR [StartTime]
GO
ALTER TABLE [dbo].[Questionnaire] ADD  CONSTRAINT [DF_Questionnaire_CreateTime]  DEFAULT (getdate()) FOR [CreateTime]
GO
ALTER TABLE [dbo].[Questionnaire] ADD  CONSTRAINT [DF_Questionnaire_IsEnable]  DEFAULT ((1)) FOR [IsEnable]
GO
ALTER TABLE [dbo].[UserData] ADD  CONSTRAINT [DF_UserData_UserID]  DEFAULT (newid()) FOR [UserID]
GO
ALTER TABLE [dbo].[UserData] ADD  CONSTRAINT [DF_UserData_CreateTime]  DEFAULT (getdate()) FOR [CreateTime]
GO
ALTER TABLE [dbo].[Question]  WITH CHECK ADD  CONSTRAINT [FK_Questionnaire_Question] FOREIGN KEY([QuestionnaireID])
REFERENCES [dbo].[Questionnaire] ([QuestionnaireID])
GO
ALTER TABLE [dbo].[Question] CHECK CONSTRAINT [FK_Questionnaire_Question]
GO
ALTER TABLE [dbo].[UserAnswer]  WITH CHECK ADD  CONSTRAINT [FK_UserAnswer_Questionnaire] FOREIGN KEY([QuestionnaireID])
REFERENCES [dbo].[Questionnaire] ([QuestionnaireID])
GO
ALTER TABLE [dbo].[UserAnswer] CHECK CONSTRAINT [FK_UserAnswer_Questionnaire]
GO
ALTER TABLE [dbo].[UserAnswer]  WITH CHECK ADD  CONSTRAINT [FK_UserAnswer_UserData] FOREIGN KEY([UserID])
REFERENCES [dbo].[UserData] ([UserID])
GO
ALTER TABLE [dbo].[UserAnswer] CHECK CONSTRAINT [FK_UserAnswer_UserData]
GO
ALTER TABLE [dbo].[UserData]  WITH CHECK ADD  CONSTRAINT [FK_UserData_Questionnaire] FOREIGN KEY([QuestionnaireID])
REFERENCES [dbo].[Questionnaire] ([QuestionnaireID])
GO
ALTER TABLE [dbo].[UserData] CHECK CONSTRAINT [FK_UserData_Questionnaire]
GO
USE [master]
GO
ALTER DATABASE [Questionnaire202204] SET  READ_WRITE 
GO
