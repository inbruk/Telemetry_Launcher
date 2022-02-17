SET IDENTITY_INSERT [dbo].[tblUsersBOBUsers] ON
GO

if not exists (select * from [dbo].[tblUsersBOBUsers] where [Id] = 1)
INSERT [dbo].[tblUsersBOBUsers] ([id], [Name], [Login], [Password], [Solt], [Mail], [Phone], [id_Company], [Password1], [GUID], [isAdmin], [isSubmit], [isCompanyAdmin]) 
VALUES (1, N'dtc dtc', N'dtc', N'1022265761', N'f26cbf8c-b1e5-4f4a-be31-f69ac2d851ac', N'alex.v.belozerov@gmail.com', N'+79266846013', 2, N'sR1W2ziMUPrHKp1koHxupQ==', N'5d7e2810-6227-4d6d-b4e0-97fdf96a0a77', 1, 1, 1)
GO

if not exists (select * from [dbo].[tblUsersBOBUsers] where [Id] = 2)
INSERT [dbo].[tblUsersBOBUsers] ([id], [Name], [Login], [Password], [Solt], [Mail], [Phone], [id_Company], [Password1], [GUID], [isAdmin], [isSubmit], [isCompanyAdmin]) 
VALUES (2, N'Семен семеныч', N'dtc2', N'2041503489', N'f26cbf8c-b1e5-4f4a-be31-f69ac2d851ac', N'none', N'none', 2, N'sR1W2ziMUPrHKp1koHxupQ==', N'5d7e2810-6227-4d6d-b4e0-97fdf96a0a88', 0, 1, NULL)
GO

if not exists (select * from [dbo].[tblUsersBOBUsers] where [Id] = 3)
INSERT [dbo].[tblUsersBOBUsers] ([id], [Name], [Login], [Password], [Solt], [Mail], [Phone], [id_Company], [Password1], [GUID], [isAdmin], [isSubmit], [isCompanyAdmin]) 
VALUES (3, N'test test', N'test', N'-1434018559', N'de80a862-5e0d-43e3-91c0-38e16b174601', N'123123@123.ru', N'123123123', 3, N'19zgebu+FBWheJfu2QmgDA==', N'c39978be-cd31-49a4-87e8-58fa2564b23b', 0, 1, NULL)
GO

if not exists (select * from [dbo].[tblUsersBOBUsers] where [Id] = 4)
INSERT [dbo].[tblUsersBOBUsers] ([id], [Name], [Login], [Password], [Solt], [Mail], [Phone], [id_Company], [Password1], [GUID], [isAdmin], [isSubmit], [isCompanyAdmin]) 
VALUES (4, N'анастасия белозерова', N'an03', N'-975884032', N'ffee53ab-867c-41d4-bf0b-be906f190761', N'an.belozerova@gmail.com', N'89208389651', 4, N'jzl217BjsLpIPybqz3hY2WZBtcUKX3f6', N'373cc7af-df0b-44ce-a0ed-5f995dfa5b2a', 0, 1, NULL)
GO

if not exists (select * from [dbo].[tblUsersBOBUsers] where [Id] = 5)
INSERT [dbo].[tblUsersBOBUsers] ([id], [Name], [Login], [Password], [Solt], [Mail], [Phone], [id_Company], [Password1], [GUID], [isAdmin], [isSubmit], [isCompanyAdmin]) 
VALUES (5, N'Интоп Интоп', N'intop', N'305948688', N'24bcdf2b-488a-48b0-8b50-4ab7c05799be', N'administrator@dtcdev.ru', N'', 6, N'GA88gkVxBsyD7vgAY6Y6hg==', N'1e8bf8f8-1c50-4ed0-86ff-f79879f2c7ab', 0, 1, NULL)
GO

if not exists (select * from [dbo].[tblUsersBOBUsers] where [Id] = 6)
INSERT [dbo].[tblUsersBOBUsers] ([id], [Name], [Login], [Password], [Solt], [Mail], [Phone], [id_Company], [Password1], [GUID], [isAdmin], [isSubmit], [isCompanyAdmin]) 
VALUES (6, N'test carwash', N'testcw', N'2048818937', N'df9889ca-8f50-41e5-80a2-f3cb175ac982', N'ololo@dtcdev.ru', N'1234567', 7, N'GA88gkVxBsyD7vgAY6Y6hg==', N'11541dce-b327-4203-9741-a01527c1c067', 0, 1, NULL)
GO

if not exists (select * from [dbo].[tblUsersBOBUsers] where [Id] = 7)
INSERT [dbo].[tblUsersBOBUsers] ([id], [Name], [Login], [Password], [Solt], [Mail], [Phone], [id_Company], [Password1], [GUID], [isAdmin], [isSubmit], [isCompanyAdmin])
 VALUES (7, N'viewer viewer', N'viewer', N'1983626129', N'2bae60d3-3cf8-47f1-a51a-31c1c14d5771', N'viewer@vk.com', N'77777777777', 8, N'GA88gkVxBsyD7vgAY6Y6hg==', N'9ab1f90f-cd4e-447f-803a-3d3fc80f91de', 0, 1, NULL)
GO

if not exists (select * from [dbo].[tblUsersBOBUsers] where [Id] = 8)
INSERT [dbo].[tblUsersBOBUsers] ([id], [Name], [Login], [Password], [Solt], [Mail], [Phone], [id_Company], [Password1], [GUID], [isAdmin], [isSubmit], [isCompanyAdmin]) 
VALUES (8, N'Иванов Иван', N'ivan', N'iLjndnkTZLiv6KzsIZo94mQwNGE3YTg0LWQwNjYtNDk1My1hNTJhLTBiZWZhNWVkZGY2MQ==', N'd04a7a84-d066-4953-a52a-0befa5eddf61', N'', N'', 11, N'RHa/t+0kukc=', N'1db9110a-7f5d-445f-a4fd-31f1c2f67d21', 0, 0, NULL)
GO

SET IDENTITY_INSERT [dbo].[tblUsersBOBUsers] OFF
GO