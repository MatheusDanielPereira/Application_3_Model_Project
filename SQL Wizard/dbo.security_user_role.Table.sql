USE [DTB_MODEL_PROJECT]
GO
/****** Object:  Table [dbo].[security_user_role]    Script Date: 10/01/2025 16:06:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[security_user_role](
	[iduser] [int] NOT NULL,
	[idrole] [int] NOT NULL,
 CONSTRAINT [PK_userrole] PRIMARY KEY CLUSTERED 
(
	[iduser] ASC,
	[idrole] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[security_user_role]  WITH CHECK ADD  CONSTRAINT [FK_userrole_role] FOREIGN KEY([idrole])
REFERENCES [dbo].[security_role] ([idrole])
GO
ALTER TABLE [dbo].[security_user_role] CHECK CONSTRAINT [FK_userrole_role]
GO
ALTER TABLE [dbo].[security_user_role]  WITH CHECK ADD  CONSTRAINT [FK_userrole_user] FOREIGN KEY([iduser])
REFERENCES [dbo].[security_user] ([iduser])
GO
ALTER TABLE [dbo].[security_user_role] CHECK CONSTRAINT [FK_userrole_user]
GO
