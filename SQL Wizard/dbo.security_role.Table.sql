USE [DTB_MODEL_PROJECT]
GO
/****** Object:  Table [dbo].[security_role]    Script Date: 10/01/2025 16:06:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[security_role](
	[idrole] [int] IDENTITY(1,1) NOT NULL,
	[role_description] [nvarchar](100) NOT NULL,
	[plant_id] [varchar](10) NOT NULL,
 CONSTRAINT [PK_role] PRIMARY KEY CLUSTERED 
(
	[idrole] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[security_role]  WITH CHECK ADD  CONSTRAINT [FK_securityrole_plant] FOREIGN KEY([plant_id])
REFERENCES [dbo].[plant] ([plant_id])
GO
ALTER TABLE [dbo].[security_role] CHECK CONSTRAINT [FK_securityrole_plant]
GO
