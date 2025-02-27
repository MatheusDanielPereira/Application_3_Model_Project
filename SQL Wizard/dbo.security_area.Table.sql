USE [DTB_MODEL_PROJECT]
GO
/****** Object:  Table [dbo].[security_area]    Script Date: 10/01/2025 16:06:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[security_area](
	[idarea] [int] IDENTITY(1,1) NOT NULL,
	[area_name] [varchar](100) NOT NULL,
	[plant_id] [varchar](10) NOT NULL,
 CONSTRAINT [PK_security_area] PRIMARY KEY CLUSTERED 
(
	[idarea] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_userarea] UNIQUE NONCLUSTERED 
(
	[area_name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[security_area]  WITH CHECK ADD  CONSTRAINT [FK_securityarea_plant] FOREIGN KEY([plant_id])
REFERENCES [dbo].[plant] ([plant_id])
GO
ALTER TABLE [dbo].[security_area] CHECK CONSTRAINT [FK_securityarea_plant]
GO
