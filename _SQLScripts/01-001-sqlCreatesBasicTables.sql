CREATE TABLE [dbo].[plant](
	[plant_id] [varchar](10) NOT NULL,
	[plant_description] [nvarchar](150) NULL,
	[plant_time_zone] [nvarchar](100) NULL,
	[plant_flag] [varbinary](MAX) NULL,
 CONSTRAINT [PK_plant] PRIMARY KEY CLUSTERED 
(
	[plant_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY] 
GO




CREATE TABLE [dbo].[security_area](
	[idarea] [int] IDENTITY(1,1) NOT NULL,
	[area_name] [varchar](100) NOT NULL,
	[plant_id] [varchar](10) NOT NULL
CONSTRAINT [PK_security_area] PRIMARY KEY CLUSTERED 
(
	[idarea] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_userarea] UNIQUE NONCLUSTERED 
(
	[area_name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] 

ALTER TABLE [dbo].[security_area]  WITH CHECK ADD  CONSTRAINT [FK_securityarea_plant] FOREIGN KEY([plant_id])
REFERENCES [dbo].[plant] ([plant_id])
GO
ALTER TABLE [dbo].[security_area] CHECK CONSTRAINT [FK_securityarea_plant]
GO




CREATE TABLE [dbo].[security_user](
	[iduser] [int] IDENTITY(1,1) NOT NULL,
	[user_name] [nvarchar](100) NOT NULL,
	[user_email] [varchar](100) NOT NULL,
	[idarea] [int] NOT NULL,
	[plant_id] [varchar](10) NOT NULL
 CONSTRAINT [PK_user] PRIMARY KEY CLUSTERED 
(
	[iduser] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_user] UNIQUE NONCLUSTERED 
(
	[user_name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] 

ALTER TABLE [dbo].[security_user]  WITH CHECK ADD  CONSTRAINT [FK_user_area] FOREIGN KEY([idarea])
REFERENCES [dbo].[security_area] ([idarea])
GO
ALTER TABLE [dbo].[security_user] CHECK CONSTRAINT [FK_user_area]
GO

ALTER TABLE [dbo].[security_user]  WITH CHECK ADD  CONSTRAINT [FK_securityuser_plant] FOREIGN KEY([plant_id])
REFERENCES [dbo].[plant] ([plant_id])
GO
ALTER TABLE [dbo].[security_user] CHECK CONSTRAINT [FK_securityuser_plant]
GO




CREATE TABLE [dbo].[security_task](
	[idtask] [varchar](30) NOT NULL,
	[task_description] [varchar](100) NOT NULL 
CONSTRAINT [PK_task] PRIMARY KEY CLUSTERED 
(
	[idtask] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] 





CREATE TABLE [dbo].[security_role](
	[idrole] [int] IDENTITY(1,1) NOT NULL,
	[role_description] [nvarchar](100) NOT NULL,
	[plant_id] [varchar](10) NOT NULL
CONSTRAINT [PK_role] PRIMARY KEY CLUSTERED 
(
	[idrole] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] 

ALTER TABLE [dbo].[security_role]  WITH CHECK ADD  CONSTRAINT [FK_securityrole_plant] FOREIGN KEY([plant_id])
REFERENCES [dbo].[plant] ([plant_id])
GO
ALTER TABLE [dbo].[security_role] CHECK CONSTRAINT [FK_securityrole_plant]
GO





CREATE TABLE [dbo].[security_role_task](
	[idrole] [int] NOT NULL,
	[idtask] [varchar](30) NOT NULL
 CONSTRAINT [PK_roletask] PRIMARY KEY CLUSTERED 
(
	[idrole], [idtask] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
) ON [PRIMARY] 

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




CREATE TABLE [dbo].[security_user_role](
	[iduser] [int] NOT NULL,
	[idrole] [int] NOT NULL
 CONSTRAINT [PK_userrole] PRIMARY KEY CLUSTERED 
(
	[iduser], [idrole] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
) ON [PRIMARY] 

ALTER TABLE [dbo].[security_user_role]  WITH CHECK ADD  CONSTRAINT [FK_userrole_user] FOREIGN KEY([iduser])
REFERENCES [dbo].[security_user] ([iduser])
GO

ALTER TABLE [dbo].[security_user_role] CHECK CONSTRAINT [FK_userrole_user]
GO

ALTER TABLE [dbo].[security_user_role]  WITH CHECK ADD  CONSTRAINT [FK_userrole_role] FOREIGN KEY([idrole])
REFERENCES [dbo].[security_role] ([idrole])
GO

ALTER TABLE [dbo].[security_user_role] CHECK CONSTRAINT [FK_userrole_role]
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




CREATE TABLE [dbo].[parameter](
	[idparameter] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](100) NOT NULL, 
	[value] [varchar](200) NOT NULL, 
	[description] [nvarchar](300) NOT NULL, 
	[plant_id] [varchar](10) NOT NULL
CONSTRAINT [PK_parameter] PRIMARY KEY CLUSTERED 
(
	[idparameter] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_parametername] UNIQUE NONCLUSTERED 
(
	[name],[plant_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] 


ALTER TABLE [dbo].[parameter]  WITH CHECK ADD  CONSTRAINT [FK_parameter_plant] FOREIGN KEY([plant_id])
REFERENCES [dbo].[plant] ([plant_id])
GO
ALTER TABLE [dbo].[parameter] CHECK CONSTRAINT [FK_parameter_plant]
GO


