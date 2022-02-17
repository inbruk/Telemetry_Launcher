
SET IDENTITY_INSERT [dbo].[tblCompanyBOBCompany] ON
GO

if not exists (select * from [dbo].[tblCompanyBOBCompany] where [Id] = 2)
INSERT [dbo].[tblCompanyBOBCompany] ([id], [Name], [Adress], [CurrentBalance], [CompanyUID]) VALUES (2, N'dimonsky', N'', 1000.0000, N'bd47ca95-3914-4a7d-afe3-54b6e1720034')
GO

if not exists (select * from [dbo].[tblCompanyBOBCompany] where [Id] = 3)
INSERT [dbo].[tblCompanyBOBCompany] ([id], [Name], [Adress], [CurrentBalance], [CompanyUID]) VALUES (3, N'123', N'', 1000.0000, N'bd47ca95-3914-4a7d-afe3-54b6e1720035')
GO

if not exists (select * from [dbo].[tblCompanyBOBCompany] where [Id] = 4)
INSERT [dbo].[tblCompanyBOBCompany] ([id], [Name], [Adress], [CurrentBalance], [CompanyUID]) VALUES (4, N'tt', N'', 1000.0000, N'bd47ca95-3914-4a7d-afe3-54b6e1720036')
GO

if not exists (select * from [dbo].[tblCompanyBOBCompany] where [Id] = 5)
INSERT [dbo].[tblCompanyBOBCompany] ([id], [Name], [Adress], [CurrentBalance], [CompanyUID]) VALUES (5, N'INTOP', NULL, 10000.0000, N'bd47ca95-3914-4a7d-afe3-54b6e1720037')
GO

if not exists (select * from [dbo].[tblCompanyBOBCompany] where [Id] = 6)
INSERT [dbo].[tblCompanyBOBCompany] ([id], [Name], [Adress], [CurrentBalance], [CompanyUID]) VALUES (6, N'Интоп', N'', 0.0000, N'bd47ca95-3914-4a7d-afe3-54b6e1720038')
GO

if not exists (select * from [dbo].[tblCompanyBOBCompany] where [Id] = 7)
INSERT [dbo].[tblCompanyBOBCompany] ([id], [Name], [Adress], [CurrentBalance], [CompanyUID]) VALUES (7, N'TCW', N'', 0.0000, N'bd47ca95-3914-4a7d-afe3-54b6e1720039')
GO

if not exists (select * from [dbo].[tblCompanyBOBCompany] where [Id] = 8)
INSERT [dbo].[tblCompanyBOBCompany] ([id], [Name], [Adress], [CurrentBalance], [CompanyUID]) VALUES (8, N'Viewer', N'', 0.0000, N'bd47ca95-3914-4a7d-afe3-54b6e1720040')
GO

if not exists (select * from [dbo].[tblCompanyBOBCompany] where [Id] = 9)
INSERT [dbo].[tblCompanyBOBCompany] ([id], [Name], [Adress], [CurrentBalance], [CompanyUID]) VALUES (9, N'Компания 1', NULL, 0.0000, N'dbad88cf-7b1e-409e-943b-25a28dad566f')
GO

if not exists (select * from [dbo].[tblCompanyBOBCompany] where [Id] = 10)
INSERT [dbo].[tblCompanyBOBCompany] ([id], [Name], [Adress], [CurrentBalance], [CompanyUID]) VALUES (10, N'Rosneft', NULL, 0.0000, N'9ad0bcba-34e1-47d2-9d08-5740bdda8d07')
GO

if not exists (select * from [dbo].[tblCompanyBOBCompany] where [Id] = 11)
INSERT [dbo].[tblCompanyBOBCompany] ([id], [Name], [Adress], [CurrentBalance], [CompanyUID]) VALUES (11, N'Auto 42', NULL, 0.0000, N'7e83d852-9adb-466f-8ff3-acd6e85f8475')
GO

SET IDENTITY_INSERT [dbo].[tblCompanyBOBCompany] OFF
GO