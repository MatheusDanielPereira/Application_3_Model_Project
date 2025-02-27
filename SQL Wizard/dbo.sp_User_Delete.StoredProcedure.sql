USE [DTB_MODEL_PROJECT]
GO
/****** Object:  StoredProcedure [dbo].[sp_User_Delete]    Script Date: 10/01/2025 16:06:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
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
