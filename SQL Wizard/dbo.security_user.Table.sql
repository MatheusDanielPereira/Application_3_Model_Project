USE [DTB_MODEL_PROJECT]
GO
/****** Object:  Table [dbo].[security_user]    Script Date: 10/01/2025 16:06:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[security_user](
	[iduser] [int] IDENTITY(1,1) NOT NULL,
	[user_name] [nvarchar](100) NOT NULL,
	[user_email] [varchar](100) NOT NULL,
	[idarea] [int] NOT NULL,
	[plant_id] [varchar](10) NOT NULL,
	[active] [bit] NULL,
 CONSTRAINT [PK_user] PRIMARY KEY CLUSTERED 
(
	[iduser] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_user] UNIQUE NONCLUSTERED 
(
	[user_name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[security_user]  WITH CHECK ADD  CONSTRAINT [FK_securityuser_plant] FOREIGN KEY([plant_id])
REFERENCES [dbo].[plant] ([plant_id])
GO
ALTER TABLE [dbo].[security_user] CHECK CONSTRAINT [FK_securityuser_plant]
GO
ALTER TABLE [dbo].[security_user]  WITH CHECK ADD  CONSTRAINT [FK_user_area] FOREIGN KEY([idarea])
REFERENCES [dbo].[security_area] ([idarea])
GO
ALTER TABLE [dbo].[security_user] CHECK CONSTRAINT [FK_user_area]
GO
