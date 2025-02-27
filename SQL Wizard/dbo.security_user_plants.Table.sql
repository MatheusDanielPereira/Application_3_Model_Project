USE [DTB_MODEL_PROJECT]
GO
/****** Object:  Table [dbo].[security_user_plants]    Script Date: 10/01/2025 16:06:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[security_user_plants](
	[iduser] [int] NOT NULL,
	[plant_id] [varchar](10) NOT NULL,
 CONSTRAINT [PK_userplants] PRIMARY KEY CLUSTERED 
(
	[iduser] ASC,
	[plant_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[security_user_plants]  WITH CHECK ADD  CONSTRAINT [FK_userplants_plant] FOREIGN KEY([plant_id])
REFERENCES [dbo].[plant] ([plant_id])
GO
ALTER TABLE [dbo].[security_user_plants] CHECK CONSTRAINT [FK_userplants_plant]
GO
ALTER TABLE [dbo].[security_user_plants]  WITH CHECK ADD  CONSTRAINT [FK_userplants_user] FOREIGN KEY([iduser])
REFERENCES [dbo].[security_user] ([iduser])
GO
ALTER TABLE [dbo].[security_user_plants] CHECK CONSTRAINT [FK_userplants_user]
GO
