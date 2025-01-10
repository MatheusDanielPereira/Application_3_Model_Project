
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_User_Delete]') AND type in (N'P', N'PC'))
BEGIN
	DROP PROCEDURE [dbo].[sp_User_Delete]
END
GO

-- ===================================================================================================================
-- Author:		Luiz Alberto Mariano
-- Create date: 2020-02-13
-- Description: Excludes the user
-- Example: EXEC [dbo].[sp_User_Delete] 10
-- ===================================================================================================================
CREATE PROCEDURE [dbo].[sp_User_Delete] 
	@iduser int
AS
BEGIN

	DELETE FROM [dbo].[security_user_plants] WHERE iduser = @iduser
	DELETE FROM [dbo].[security_user_role] WHERE iduser = @iduser
	DELETE FROM [dbo].[security_user] WHERE iduser = @iduser

END

GO