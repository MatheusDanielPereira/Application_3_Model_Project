USE [DTB_MODEL_PROJECT]
GO
/****** Object:  View [dbo].[vw_example]    Script Date: 10/01/2025 16:06:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =================================================================================================
-- Author:		Luiz Alberto Mariano
-- Create date: 2020-02-13
-- Description:	For example purpose, lists all users
-- =================================================================================================

CREATE VIEW [dbo].[vw_example]
AS
	SELECT * FROM [dbo].[security_user]
GO
