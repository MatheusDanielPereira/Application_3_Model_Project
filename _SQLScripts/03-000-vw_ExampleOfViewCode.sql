
IF EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[vw_example]'))
BEGIN
	DROP VIEW [dbo].[vw_example]
END
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

