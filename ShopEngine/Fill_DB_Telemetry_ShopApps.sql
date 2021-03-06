USE [DB_Telemetry_ShopApps]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 11/07/2013 16:10:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- !!! ВНИМАНИЕ ХРАНИМАЯ ПРОЦЕДУРА НУЖНА ДЛЯ РАБОТЫ ПРОГРАММЫ !!!
CREATE PROCEDURE CreateCustomUser
	@userID int,
	@strUID VARCHAR(100) 
AS
BEGIN
	SET IDENTITY_INSERT dbo.Users ON
	INSERT INTO [dbo].[Users] (Id, UID) VALUES (@userID,@strUID)
	SET IDENTITY_INSERT dbo.Users OFF
END
GO


SET IDENTITY_INSERT [dbo].[Users] ON
GO
INSERT [dbo].[Users] ([id], [UID], [LastReadedNewsID], [LastReadedMessageID]) VALUES (1, N'd72880e7-e7ab-4200-9436-3f30ab85bbb5', NULL, NULL)
GO
INSERT [dbo].[Users] ([id], [UID], [LastReadedNewsID], [LastReadedMessageID]) VALUES (2, N'5d7e2810-6227-4d6d-b4e0-97fdf96a0a77', NULL, NULL)
GO
INSERT [dbo].[Users] ([id], [UID], [LastReadedNewsID], [LastReadedMessageID]) VALUES (3, N'373cc7af-df0b-44ce-a0ed-5f995dfa5b2a', NULL, NULL)
GO
INSERT [dbo].[Users] ([id], [UID], [LastReadedNewsID], [LastReadedMessageID]) VALUES (4, N'c39978be-cd31-49a4-87e8-58fa2564b23b', NULL, NULL)
GO
INSERT [dbo].[Users] ([id], [UID], [LastReadedNewsID], [LastReadedMessageID]) VALUES (5, N'06733edc-34c5-4c50-a35c-3e0277167269', NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
SET IDENTITY_INSERT [dbo].[tblAppsBOBCategories] ON
GO
INSERT [dbo].[tblAppsBOBCategories] ([id], [Name]) VALUES (1, N'Телеметрия')
GO
SET IDENTITY_INSERT [dbo].[tblAppsBOBCategories] OFF
GO
SET IDENTITY_INSERT [dbo].[tblAppsBOBApps] ON
GO
INSERT [dbo].[tblAppsBOBApps] 
       ([id], [AppName], [Description], [Icon], [Banner250x250URL], [Banner100x100URL], [ScreenshotsURL], [ImageShortcut], [ActualFileName], [id_Category], [counterInstall], [rate], [CoutRated], [SumAllRate], [ActualVersion], [ScreenshotsCount]) 
VALUES (1, N'Сервис общей телеметрии', N'Общий сервис телеметрии используется для общего просмотра мобильных и станционарных объектов', 
        N'../../Images/Telemetry.png', NULL, NULL, NULL, N'../../Images/Telemetry.png', N'Telemetry.exe', 1, 3, CAST(5.000 AS Decimal(9, 3)), 2, 10, 2.0, 0)
GO
INSERT [dbo].[tblAppsBOBApps] 
       ([id], [AppName], [Description], [Icon], [Banner250x250URL], [Banner100x100URL], [ScreenshotsURL], [ImageShortcut], [ActualFileName], [id_Category], [counterInstall], [rate], [CoutRated], [SumAllRate], [ActualVersion], [ScreenshotsCount]) 
VALUES (2, N'Автомойка', N'Сервис, предназначеный для владельцев автомоек. помагает решать вопросы учета клиентов, составления расписания, сбора и обработки информации. При установке аппаратного обеспечения сервис может контролировать несанкционированное включение аппаратуры (при заказах "на лево")',
        N'../../Images/automoika-po-vizovu.jpg', NULL, NULL, NULL, N'../../Images/automoika-po-vizovu.jpg', N'CarWash.exe', 1, 3, CAST(5.000 AS Decimal(9, 3)), 2, 10, 1.0, 0)
GO
INSERT [dbo].[tblAppsBOBApps] 
       ([id], [AppName], [Description], [Icon], [Banner250x250URL], [Banner100x100URL], [ScreenshotsURL], [ImageShortcut], [ActualFileName], [id_Category], [counterInstall], [rate], [CoutRated], [SumAllRate], [ActualVersion], [ScreenshotsCount]) 
VALUES (3, N'Тестовое приложение', N'Некое тестовое приложение',
        N'../../Images/some.jpg', NULL, NULL, NULL, N'../../Images/some.jpg', N'some.exe', 1, 3, CAST(5.000 AS Decimal(9, 3)), 2, 10, 1.5, 0)
GO
SET IDENTITY_INSERT [dbo].[tblAppsBOBApps] OFF
GO
SET IDENTITY_INSERT [dbo].[tblUsersM2MUserApps] ON
GO
INSERT [dbo].[tblUsersM2MUserApps] ([id], [id_User], [id_App], [IsUserRated], [DateTime], [CurrentRate], [CurrentVersion]) VALUES (1, 1, 1, 0, GETDATE(), 0, 1.0)
GO
INSERT [dbo].[tblUsersM2MUserApps] ([id], [id_User], [id_App], [IsUserRated], [DateTime], [CurrentRate], [CurrentVersion]) VALUES (2, 1, 2, 0, GETDATE(), 0, 1.0)
GO
INSERT [dbo].[tblUsersM2MUserApps] ([id], [id_User], [id_App], [IsUserRated], [DateTime], [CurrentRate], [CurrentVersion]) VALUES (3, 2, 2, 0, GETDATE(), 0, 1.0)
GO
INSERT [dbo].[tblUsersM2MUserApps] ([id], [id_User], [id_App], [IsUserRated], [DateTime], [CurrentRate], [CurrentVersion]) VALUES (4, 2, 1, 0, GETDATE(), 0, 2.0)
GO
INSERT [dbo].[tblUsersM2MUserApps] ([id], [id_User], [id_App], [IsUserRated], [DateTime], [CurrentRate], [CurrentVersion]) VALUES (5, 3, 2, 0, GETDATE(), 0, 1.0)
GO
INSERT [dbo].[tblUsersM2MUserApps] ([id], [id_User], [id_App], [IsUserRated], [DateTime], [CurrentRate], [CurrentVersion]) VALUES (6, 4, 2, 0, GETDATE(), 0, 1.0)
GO
INSERT [dbo].[tblUsersM2MUserApps] ([id], [id_User], [id_App], [IsUserRated], [DateTime], [CurrentRate], [CurrentVersion]) VALUES (7, 4, 1, 0, GETDATE(), 0, 2.0)
GO
INSERT [dbo].[tblUsersM2MUserApps] ([id], [id_User], [id_App], [IsUserRated], [DateTime], [CurrentRate], [CurrentVersion]) VALUES (8, 5, 1, 0, GETDATE(), 0, 1.0)
GO
INSERT [dbo].[tblUsersM2MUserApps] ([id], [id_User], [id_App], [IsUserRated], [DateTime], [CurrentRate], [CurrentVersion]) VALUES (9, 5, 2, 0, GETDATE(), 0, 1.0)
GO
