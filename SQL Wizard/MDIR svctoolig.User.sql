USE [DTB_MODEL_PROJECT]
GO
/****** Object:  User [MDIR\svctoolig]    Script Date: 10/01/2025 16:06:16 ******/
CREATE USER [MDIR\svctoolig] FOR LOGIN [MDIR\svctoolig] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_datareader] ADD MEMBER [MDIR\svctoolig]
GO
