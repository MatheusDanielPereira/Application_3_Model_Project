USE [DTB_MODEL_PROJECT]
GO
/****** Object:  Table [dbo].[plant]    Script Date: 10/01/2025 16:06:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[plant](
	[plant_id] [varchar](10) NOT NULL,
	[plant_description] [nvarchar](150) NULL,
	[plant_time_zone] [nvarchar](100) NULL,
	[plant_flag] [varbinary](max) NULL,
 CONSTRAINT [PK_plant] PRIMARY KEY CLUSTERED 
(
	[plant_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
