USE [DTB_MODEL_PROJECT]
GO
/****** Object:  User [MDIR\svcnordcdi]    Script Date: 10/01/2025 16:06:16 ******/
CREATE USER [MDIR\svcnordcdi] FOR LOGIN [MDIR\svcnordcdi] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_datareader] ADD MEMBER [MDIR\svcnordcdi]
GO
