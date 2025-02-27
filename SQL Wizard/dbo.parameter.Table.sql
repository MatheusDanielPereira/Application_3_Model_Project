USE [DTB_MODEL_PROJECT]
GO
/****** Object:  Table [dbo].[parameter]    Script Date: 10/01/2025 16:06:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[parameter](
	[idparameter] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](100) NOT NULL,
	[value] [varchar](200) NOT NULL,
	[description] [nvarchar](300) NOT NULL,
	[plant_id] [varchar](10) NOT NULL,
 CONSTRAINT [PK_parameter] PRIMARY KEY CLUSTERED 
(
	[idparameter] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_parametername] UNIQUE NONCLUSTERED 
(
	[name] ASC,
	[plant_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[parameter]  WITH CHECK ADD  CONSTRAINT [FK_parameter_plant] FOREIGN KEY([plant_id])
REFERENCES [dbo].[plant] ([plant_id])
GO
ALTER TABLE [dbo].[parameter] CHECK CONSTRAINT [FK_parameter_plant]
GO
