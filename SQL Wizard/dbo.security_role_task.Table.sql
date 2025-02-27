USE [DTB_MODEL_PROJECT]
GO
/****** Object:  Table [dbo].[security_role_task]    Script Date: 10/01/2025 16:06:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[security_role_task](
	[idrole] [int] NOT NULL,
	[idtask] [varchar](30) NOT NULL,
 CONSTRAINT [PK_roletask] PRIMARY KEY CLUSTERED 
(
	[idrole] ASC,
	[idtask] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[security_role_task]  WITH CHECK ADD  CONSTRAINT [FK_roletask_role] FOREIGN KEY([idrole])
REFERENCES [dbo].[security_role] ([idrole])
GO
ALTER TABLE [dbo].[security_role_task] CHECK CONSTRAINT [FK_roletask_role]
GO
ALTER TABLE [dbo].[security_role_task]  WITH CHECK ADD  CONSTRAINT [FK_roletask_task] FOREIGN KEY([idtask])
REFERENCES [dbo].[security_task] ([idtask])
GO
ALTER TABLE [dbo].[security_role_task] CHECK CONSTRAINT [FK_roletask_task]
GO
