CREATE TABLE [dbo].[tblUsersBOBUsers]
(
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[Login] [nvarchar](200) NOT NULL,
	[Password] [nvarchar](max) NOT NULL,
	[Solt] [nvarchar](200) NOT NULL,
	[Mail] [nvarchar](200) NOT NULL,
	[Phone] [nvarchar](200) NULL,
	[id_Company] [int] NOT NULL FOREIGN KEY REFERENCES [dbo].[tblCompanyBOBCompany] ([id]),
	[Password1] [nvarchar](max) NOT NULL,
	[GUID] [nvarchar](100) NOT NULL,
	[isAdmin] [bit] NOT NULL,
	[isSubmit] [bit] NOT NULL,
	[isCompanyAdmin] [bit] NULL,

	CONSTRAINT [PK_dbo.tblUsersBOBUsers] PRIMARY KEY CLUSTERED
	(
		[id] ASC
	)
	WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
