USE [DB_Telemetry_Users]
GO
/****** Object:  Table [dbo].[tblCompanyBOBCompany]    Script Date: 05/04/2013 16:31:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblCompanyBOBCompany](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](200) NOT NULL,
	[Adress] [varchar](400) NULL,
	[CurrentBalance] [money] NOT NULL,
	[CompanyUID] [varchar](200) NULL,
 CONSTRAINT [PK_dbo.tblCompanyBOBCompany] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[tblCompanyBOBCompany] ON
INSERT [dbo].[tblCompanyBOBCompany] ([id], [Name], [Adress], [CurrentBalance], [CompanyUID]) VALUES (2, N'dimonsky', N'', 1000.0000, N'bd47ca95-3914-4a7d-afe3-54b6e1720034')
INSERT [dbo].[tblCompanyBOBCompany] ([id], [Name], [Adress], [CurrentBalance], [CompanyUID]) VALUES (3, N'123', N'', 1000.0000, N'bd47ca95-3914-4a7d-afe3-54b6e1720035')
INSERT [dbo].[tblCompanyBOBCompany] ([id], [Name], [Adress], [CurrentBalance], [CompanyUID]) VALUES (4, N'tt', N'', 1000.0000, N'bd47ca95-3914-4a7d-afe3-54b6e1720036')
INSERT [dbo].[tblCompanyBOBCompany] ([id], [Name], [Adress], [CurrentBalance], [CompanyUID]) VALUES (5, N'INTOP', NULL, 10000.0000, N'bd47ca95-3914-4a7d-afe3-54b6e1720037')
INSERT [dbo].[tblCompanyBOBCompany] ([id], [Name], [Adress], [CurrentBalance], [CompanyUID]) VALUES (6, N'Интоп', N'', 0.0000, N'bd47ca95-3914-4a7d-afe3-54b6e1720038')
INSERT [dbo].[tblCompanyBOBCompany] ([id], [Name], [Adress], [CurrentBalance], [CompanyUID]) VALUES (7, N'TCW', N'', 0.0000, N'bd47ca95-3914-4a7d-afe3-54b6e1720039')
INSERT [dbo].[tblCompanyBOBCompany] ([id], [Name], [Adress], [CurrentBalance], [CompanyUID]) VALUES (8, N'Viewer', N'', 0.0000, N'bd47ca95-3914-4a7d-afe3-54b6e1720040')
INSERT [dbo].[tblCompanyBOBCompany] ([id], [Name], [Adress], [CurrentBalance], [CompanyUID]) VALUES (9, N'Компания 1', NULL, 0.0000, N'dbad88cf-7b1e-409e-943b-25a28dad566f')
INSERT [dbo].[tblCompanyBOBCompany] ([id], [Name], [Adress], [CurrentBalance], [CompanyUID]) VALUES (10, N'Rosneft', NULL, 0.0000, N'9ad0bcba-34e1-47d2-9d08-5740bdda8d07')
INSERT [dbo].[tblCompanyBOBCompany] ([id], [Name], [Adress], [CurrentBalance], [CompanyUID]) VALUES (11, N'Auto 42', NULL, 0.0000, N'7e83d852-9adb-466f-8ff3-acd6e85f8475')
SET IDENTITY_INSERT [dbo].[tblCompanyBOBCompany] OFF
/****** Object:  Table [dbo].[tblUsersBOBUsers]    Script Date: 05/04/2013 16:31:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblUsersBOBUsers](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](200) NOT NULL,
	[Login] [varchar](200) NOT NULL,
	[Password] [varchar](max) NOT NULL,
	[Solt] [varchar](200) NOT NULL,
	[Mail] [varchar](200) NOT NULL,
	[Phone] [varchar](200) NULL,
	[id_Company] [int] NOT NULL,
	[Password1] [varchar](max) NOT NULL,
	[GUID] [varchar](100) NOT NULL,
	[isAdmin] [bit] NOT NULL,
	[isSubmit] [bit] NOT NULL,
	[isCompanyAdmin] [bit] NULL,
 CONSTRAINT [PK_dbo.tblUsersBOBUsers] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[tblUsersBOBUsers] ON
INSERT [dbo].[tblUsersBOBUsers] ([id], [Name], [Login], [Password], [Solt], [Mail], [Phone], [id_Company], [Password1], [GUID], [isAdmin], [isSubmit], [isCompanyAdmin]) VALUES (1, N'dtc dtc', N'dtc', N'1022265761', N'f26cbf8c-b1e5-4f4a-be31-f69ac2d851ac', N'alex.v.belozerov@gmail.com', N'+79266846013', 1, N'sR1W2ziMUPrHKp1koHxupQ==', N'5d7e2810-6227-4d6d-b4e0-97fdf96a0a77', 1, 1, 1)
INSERT [dbo].[tblUsersBOBUsers] ([id], [Name], [Login], [Password], [Solt], [Mail], [Phone], [id_Company], [Password1], [GUID], [isAdmin], [isSubmit], [isCompanyAdmin]) VALUES (2, N'Семен семеныч', N'dtc2', N'2041503489', N'f26cbf8c-b1e5-4f4a-be31-f69ac2d851ac', N'none', N'none', 1, N'sR1W2ziMUPrHKp1koHxupQ==', N'5d7e2810-6227-4d6d-b4e0-97fdf96a0a88', 0, 1, NULL)
INSERT [dbo].[tblUsersBOBUsers] ([id], [Name], [Login], [Password], [Solt], [Mail], [Phone], [id_Company], [Password1], [GUID], [isAdmin], [isSubmit], [isCompanyAdmin]) VALUES (3, N'test test', N'test', N'-1434018559', N'de80a862-5e0d-43e3-91c0-38e16b174601', N'123123@123.ru', N'123123123', 3, N'19zgebu+FBWheJfu2QmgDA==', N'c39978be-cd31-49a4-87e8-58fa2564b23b', 0, 1, NULL)
INSERT [dbo].[tblUsersBOBUsers] ([id], [Name], [Login], [Password], [Solt], [Mail], [Phone], [id_Company], [Password1], [GUID], [isAdmin], [isSubmit], [isCompanyAdmin]) VALUES (4, N'анастасия белозерова', N'an03', N'-975884032', N'ffee53ab-867c-41d4-bf0b-be906f190761', N'an.belozerova@gmail.com', N'89208389651', 4, N'jzl217BjsLpIPybqz3hY2WZBtcUKX3f6', N'373cc7af-df0b-44ce-a0ed-5f995dfa5b2a', 0, 1, NULL)
INSERT [dbo].[tblUsersBOBUsers] ([id], [Name], [Login], [Password], [Solt], [Mail], [Phone], [id_Company], [Password1], [GUID], [isAdmin], [isSubmit], [isCompanyAdmin]) VALUES (5, N'Интоп Интоп', N'intop', N'305948688', N'24bcdf2b-488a-48b0-8b50-4ab7c05799be', N'administrator@dtcdev.ru', N'', 6, N'GA88gkVxBsyD7vgAY6Y6hg==', N'1e8bf8f8-1c50-4ed0-86ff-f79879f2c7ab', 0, 1, NULL)
INSERT [dbo].[tblUsersBOBUsers] ([id], [Name], [Login], [Password], [Solt], [Mail], [Phone], [id_Company], [Password1], [GUID], [isAdmin], [isSubmit], [isCompanyAdmin]) VALUES (6, N'test carwash', N'testcw', N'2048818937', N'df9889ca-8f50-41e5-80a2-f3cb175ac982', N'ololo@dtcdev.ru', N'1234567', 7, N'GA88gkVxBsyD7vgAY6Y6hg==', N'11541dce-b327-4203-9741-a01527c1c067', 0, 1, NULL)
INSERT [dbo].[tblUsersBOBUsers] ([id], [Name], [Login], [Password], [Solt], [Mail], [Phone], [id_Company], [Password1], [GUID], [isAdmin], [isSubmit], [isCompanyAdmin]) VALUES (7, N'viewer viewer', N'viewer', N'1983626129', N'2bae60d3-3cf8-47f1-a51a-31c1c14d5771', N'viewer@vk.com', N'77777777777', 8, N'GA88gkVxBsyD7vgAY6Y6hg==', N'9ab1f90f-cd4e-447f-803a-3d3fc80f91de', 0, 1, NULL)
INSERT [dbo].[tblUsersBOBUsers] ([id], [Name], [Login], [Password], [Solt], [Mail], [Phone], [id_Company], [Password1], [GUID], [isAdmin], [isSubmit], [isCompanyAdmin]) VALUES (8, N'Иванов Иван', N'ivan', N'iLjndnkTZLiv6KzsIZo94mQwNGE3YTg0LWQwNjYtNDk1My1hNTJhLTBiZWZhNWVkZGY2MQ==', N'd04a7a84-d066-4953-a52a-0befa5eddf61', N'', N'', 13, N'RHa/t+0kukc=', N'1db9110a-7f5d-445f-a4fd-31f1c2f67d21', 0, 0, NULL)
SET IDENTITY_INSERT [dbo].[tblUsersBOBUsers] OFF
/****** Object:  Table [dbo].[tblCompanyBOBPayments]    Script Date: 05/04/2013 16:31:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblCompanyBOBPayments](
	[DateMessage] [datetime] NOT NULL,
	[id_Company] [int] NOT NULL,
	[MoneyMove] [money] NOT NULL,
	[Comment] [varchar](max) NULL,
 CONSTRAINT [PK_dbo.tblCompanyBOBPayments] PRIMARY KEY CLUSTERED 
(
	[DateMessage] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  ForeignKey [tblCompanyBOBCompany_tblUsersBOBUser]    Script Date: 05/04/2013 16:31:07 ******/
ALTER TABLE [dbo].[tblUsersBOBUsers]  WITH CHECK ADD  CONSTRAINT [tblCompanyBOBCompany_tblUsersBOBUser] FOREIGN KEY([id_Company])
REFERENCES [dbo].[tblCompanyBOBCompany] ([id])
GO
ALTER TABLE [dbo].[tblUsersBOBUsers] CHECK CONSTRAINT [tblCompanyBOBCompany_tblUsersBOBUser]
GO
/****** Object:  ForeignKey [tblCompanyBOBCompany_tblCompanyBOBPayment]    Script Date: 05/04/2013 16:31:07 ******/
ALTER TABLE [dbo].[tblCompanyBOBPayments]  WITH CHECK ADD  CONSTRAINT [tblCompanyBOBCompany_tblCompanyBOBPayment] FOREIGN KEY([id_Company])
REFERENCES [dbo].[tblCompanyBOBCompany] ([id])
GO
ALTER TABLE [dbo].[tblCompanyBOBPayments] CHECK CONSTRAINT [tblCompanyBOBCompany_tblCompanyBOBPayment]
GO
