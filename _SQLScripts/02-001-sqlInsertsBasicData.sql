--Please modify this with your AD user and plant bellow:
DECLARE @PlantID varchar(10) = 'BR03'

INSERT INTO [dbo].[plant] ([plant_id], [plant_description]) VALUES (@PlantID, 'Sorocaba')

INSERT INTO [dbo].[security_area] ([area_name],[plant_id]) VALUES ('Administration', @PlantID)

INSERT INTO [dbo].[security_task] ([idtask],[task_description]) VALUES ('ADM_MENU', 'Administration main menu')
INSERT INTO [dbo].[security_task] ([idtask],[task_description]) VALUES ('ADM_CADPARAM', 'Parameter database')
INSERT INTO [dbo].[security_task] ([idtask],[task_description]) VALUES ('ADM_CADPLANT', 'Plant database')
INSERT INTO [dbo].[security_task] ([idtask],[task_description]) VALUES ('ADM_SEG_MENU', 'Security main menu')
INSERT INTO [dbo].[security_task] ([idtask],[task_description]) VALUES ('ADM_SEG_CADAREAUSUARIO', 'User area database')
INSERT INTO [dbo].[security_task] ([idtask],[task_description]) VALUES ('ADM_SEG_CADUSUARIO', 'User database')
INSERT INTO [dbo].[security_task] ([idtask],[task_description]) VALUES ('ADM_SEG_CADPERFIL', 'Security role database')

INSERT INTO [dbo].[security_task] ([idtask],[task_description]) VALUES ('MAIN_SCREEN', 'Main screen - Dashboard')

--Some examples:
INSERT INTO [dbo].[security_task] ([idtask],[task_description]) VALUES ('OTHER_MENU', 'Other main menu-Example')
INSERT INTO [dbo].[security_task] ([idtask],[task_description]) VALUES ('OTHER_MENU_OPT1', 'Other sub menu-Example1')
INSERT INTO [dbo].[security_task] ([idtask],[task_description]) VALUES ('OTHER_MENU_OPT2', 'Other sub menu-Example2')
INSERT INTO [dbo].[security_task] ([idtask],[task_description]) VALUES ('OTHER_MENU_OPT3', 'Other sub menu-Example3')

INSERT INTO [dbo].[security_role] ([role_description],[plant_id]) VALUES ('Administrator', @PlantID)

DECLARE @idrole int, @idarea int, @iduser int
--Gets role id
SELECT @idrole = MIN([idrole]) FROM [dbo].[security_role]

--Inserts all task into the role
INSERT INTO [dbo].[security_role_task] ([idrole],[idtask]) (SELECT @idrole, [idtask] FROM [dbo].[security_task])

--Gets area id
SELECT @idarea = MIN([idarea]) FROM [dbo].[security_area]

--Please modify this with your AD user
INSERT INTO [dbo].[security_user] ([user_name],[user_email],[idarea],[plant_id])
VALUES ('Luiz Mariano','luiz.mariano@mogrouppartners.com',@idarea,@PlantID)

--Gets user id
SELECT @iduser = MIN([iduser]) FROM [dbo].[security_user]

--Inserts plant to the user, so the plant selector menu will be available
INSERT INTO [dbo].[security_user_plants] ([iduser],[plant_id]) VALUES (@iduser, @PlantID)

--Grants user access to the role
INSERT INTO [dbo].[security_user_role] ([iduser],[idrole]) VALUES (@iduser, @idrole)

INSERT INTO [dbo].[parameter] ([name],[value],[description],[plant_id]) VALUES ('DashboardRefreshInterval','60','Tempo para atualização automática do Dashboard - Em segundos.','BR03')








