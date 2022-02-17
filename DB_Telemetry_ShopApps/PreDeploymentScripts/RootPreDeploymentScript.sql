/*
 Pre-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be executed before the build script.	
 Use SQLCMD syntax to include a file in the pre-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the pre-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

-- этот скрипт рефакторить буду по мере добавления новых фич, когда они коснутся таблиц отсюда 

USE [DB_Telemetry_ShopApps]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 11/07/2013 16:10:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Users](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[UID] [varchar](100) NOT NULL,
	[LastReadedNewsID] [int] NULL,
	[LastReadedMessageID] [int] NULL,
 CONSTRAINT [PK_dbo.Users] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON
INSERT [dbo].[Users] ([id], [UID], [LastReadedNewsID], [LastReadedMessageID]) VALUES (1, N'd72880e7-e7ab-4200-9436-3f30ab85bbb5', NULL, NULL)
INSERT [dbo].[Users] ([id], [UID], [LastReadedNewsID], [LastReadedMessageID]) VALUES (2, N'5d7e2810-6227-4d6d-b4e0-97fdf96a0a77', NULL, NULL)
INSERT [dbo].[Users] ([id], [UID], [LastReadedNewsID], [LastReadedMessageID]) VALUES (3, N'373cc7af-df0b-44ce-a0ed-5f995dfa5b2a', NULL, NULL)
INSERT [dbo].[Users] ([id], [UID], [LastReadedNewsID], [LastReadedMessageID]) VALUES (4, N'c39978be-cd31-49a4-87e8-58fa2564b23b', NULL, NULL)
INSERT [dbo].[Users] ([id], [UID], [LastReadedNewsID], [LastReadedMessageID]) VALUES (5, N'06733edc-34c5-4c50-a35c-3e0277167269', NULL, NULL)
SET IDENTITY_INSERT [dbo].[Users] OFF
/****** Object:  Table [dbo].[tblAppsBOBCategories]    Script Date: 11/07/2013 16:10:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblAppsBOBCategories](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](200) NOT NULL,
 CONSTRAINT [PK_dbo.tblAppsBOBCategories] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[tblAppsBOBCategories] ON
INSERT [dbo].[tblAppsBOBCategories] ([id], [Name]) VALUES (1, N'Телеметрия')
SET IDENTITY_INSERT [dbo].[tblAppsBOBCategories] OFF
/****** Object:  Table [dbo].[Review]    Script Date: 11/07/2013 16:10:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Review](
	[RevID] [int] IDENTITY(1,1) NOT NULL,
	[bylogin] [varchar](200) NOT NULL,
	[Date] [datetime] NOT NULL,
	[Message] [varchar](1024) NOT NULL,
	[id_App] [int] NOT NULL,
 CONSTRAINT [PK_Review] PRIMARY KEY CLUSTERED 
(
	[RevID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[News]    Script Date: 11/07/2013 16:10:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[News](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](1000) NOT NULL,
	[ImageURL] [varchar](1000) NULL,
	[Text] [varchar](max) NOT NULL,
	[Date] [datetime] NOT NULL,
 CONSTRAINT [PK_News] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Messages]    Script Date: 11/07/2013 16:10:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Messages](
	[Date] [datetime] NOT NULL,
	[Name] [varchar](1000) NOT NULL,
	[Text] [varchar](max) NOT NULL,
	[id_User] [int] NOT NULL,
	[id] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_Messages] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblAppsBOBApps]    Script Date: 11/07/2013 16:10:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblAppsBOBApps](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[AppName] [varchar](200) NOT NULL,
	[Description] [varchar](max) NOT NULL,
	[Icon] [varchar](200) NOT NULL,
	[ImagePreview1] [varchar](200) NULL,
	[ImagePreview2] [varchar](200) NULL,
	[ImagePreview3] [varchar](200) NULL,
	[ImageShortcut] [varchar](200) NULL,
	[AppURL] [varchar](200) NOT NULL,
	[id_Category] [int] NULL,
	[counterInstall] [int] NULL,
	[rate] [decimal](9, 3) NOT NULL,
	[CoutRated] [int] NOT NULL,
	[SumAllRate] [int] NOT NULL,
 CONSTRAINT [PK_dbo.tblAppsBOBApps] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[tblAppsBOBApps] ON
INSERT [dbo].[tblAppsBOBApps] ([id], [AppName], [Description], [Icon], [ImagePreview1], [ImagePreview2], [ImagePreview3], [ImageShortcut], [AppURL], [id_Category], [counterInstall], [rate], [CoutRated], [SumAllRate]) VALUES (1, N'Сервис общей телеметрии', N'Общий сервис телеметрии используется для общего просмотра мобильных и станционарных объектов', N'../../Images/Telemetry.png', NULL, NULL, NULL, N'../../Images/Telemetry.png', N'http://mytelemetry.ru', 1, 3, CAST(5.000 AS Decimal(9, 3)), 2, 10)
INSERT [dbo].[tblAppsBOBApps] ([id], [AppName], [Description], [Icon], [ImagePreview1], [ImagePreview2], [ImagePreview3], [ImageShortcut], [AppURL], [id_Category], [counterInstall], [rate], [CoutRated], [SumAllRate]) VALUES (2, N'Автомойка', N'Сервис, предназначеный для владельцев автомоек. помагает решать вопросы учета клиентов, составления расписания, сбора и обработки информации. При установке аппаратного обеспечения сервис может контролировать несанкционированное включение аппаратуры (при заказах "на лево")', N'../../Images/automoika-po-vizovu.jpg', NULL, NULL, NULL, N'../../Images/automoika-po-vizovu.jpg', N'http://localhost:3011/', 1, 3, CAST(5.000 AS Decimal(9, 3)), 2, 10)
SET IDENTITY_INSERT [dbo].[tblAppsBOBApps] OFF
/****** Object:  Table [dbo].[tblUsersM2MUserApps]    Script Date: 11/07/2013 16:10:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblUsersM2MUserApps](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[id_User] [int] NOT NULL,
	[id_App] [int] NOT NULL,
	[IsUserRated] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.tblUsersM2MUserApps] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[tblUsersM2MUserApps] ON
INSERT [dbo].[tblUsersM2MUserApps] ([id], [id_User], [id_App], [IsUserRated]) VALUES (1, 1, 1, 0)
INSERT [dbo].[tblUsersM2MUserApps] ([id], [id_User], [id_App], [IsUserRated]) VALUES (2, 1, 2, 0)
INSERT [dbo].[tblUsersM2MUserApps] ([id], [id_User], [id_App], [IsUserRated]) VALUES (3, 2, 2, 0)
INSERT [dbo].[tblUsersM2MUserApps] ([id], [id_User], [id_App], [IsUserRated]) VALUES (4, 2, 1, 0)
INSERT [dbo].[tblUsersM2MUserApps] ([id], [id_User], [id_App], [IsUserRated]) VALUES (5, 3, 2, 0)
INSERT [dbo].[tblUsersM2MUserApps] ([id], [id_User], [id_App], [IsUserRated]) VALUES (6, 4, 2, 0)
INSERT [dbo].[tblUsersM2MUserApps] ([id], [id_User], [id_App], [IsUserRated]) VALUES (7, 4, 1, 0)
INSERT [dbo].[tblUsersM2MUserApps] ([id], [id_User], [id_App], [IsUserRated]) VALUES (8, 5, 1, 0)
INSERT [dbo].[tblUsersM2MUserApps] ([id], [id_User], [id_App], [IsUserRated]) VALUES (9, 5, 2, 0)
SET IDENTITY_INSERT [dbo].[tblUsersM2MUserApps] OFF
/****** Object:  ForeignKey [FK_Messages_Users]    Script Date: 11/07/2013 16:10:52 ******/
ALTER TABLE [dbo].[Messages]  WITH CHECK ADD  CONSTRAINT [FK_Messages_Users] FOREIGN KEY([id_User])
REFERENCES [dbo].[Users] ([id])
GO
ALTER TABLE [dbo].[Messages] CHECK CONSTRAINT [FK_Messages_Users]
GO
/****** Object:  ForeignKey [FK_tblAppsBOBApps_tblAppsBOBCategories]    Script Date: 11/07/2013 16:10:52 ******/
ALTER TABLE [dbo].[tblAppsBOBApps]  WITH CHECK ADD  CONSTRAINT [FK_tblAppsBOBApps_tblAppsBOBCategories] FOREIGN KEY([id_Category])
REFERENCES [dbo].[tblAppsBOBCategories] ([id])
GO
ALTER TABLE [dbo].[tblAppsBOBApps] CHECK CONSTRAINT [FK_tblAppsBOBApps_tblAppsBOBCategories]
GO
/****** Object:  ForeignKey [tblAppsBOBCategory_tblAppsBOBApp]    Script Date: 11/07/2013 16:10:52 ******/
ALTER TABLE [dbo].[tblAppsBOBApps]  WITH CHECK ADD  CONSTRAINT [tblAppsBOBCategory_tblAppsBOBApp] FOREIGN KEY([id_Category])
REFERENCES [dbo].[tblAppsBOBCategories] ([id])
GO
ALTER TABLE [dbo].[tblAppsBOBApps] CHECK CONSTRAINT [tblAppsBOBCategory_tblAppsBOBApp]
GO
/****** Object:  ForeignKey [tblAppsBOBApp_tblUsersM2MUserApp]    Script Date: 11/07/2013 16:10:52 ******/
ALTER TABLE [dbo].[tblUsersM2MUserApps]  WITH CHECK ADD  CONSTRAINT [tblAppsBOBApp_tblUsersM2MUserApp] FOREIGN KEY([id_App])
REFERENCES [dbo].[tblAppsBOBApps] ([id])
GO
ALTER TABLE [dbo].[tblUsersM2MUserApps] CHECK CONSTRAINT [tblAppsBOBApp_tblUsersM2MUserApp]
GO
/****** Object:  ForeignKey [User_tblUsersM2MUserApp]    Script Date: 11/07/2013 16:10:52 ******/
ALTER TABLE [dbo].[tblUsersM2MUserApps]  WITH CHECK ADD  CONSTRAINT [User_tblUsersM2MUserApp] FOREIGN KEY([id_User])
REFERENCES [dbo].[Users] ([id])
GO
ALTER TABLE [dbo].[tblUsersM2MUserApps] CHECK CONSTRAINT [User_tblUsersM2MUserApp]
GO
