USE [DTB_MODEL_PROJECT]
GO
/****** Object:  User [igniouser_prod]    Script Date: 10/01/2025 16:06:16 ******/
CREATE USER [igniouser_prod] FOR LOGIN [igniouser_prod] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_datareader] ADD MEMBER [igniouser_prod]
GO
