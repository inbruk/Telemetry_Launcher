CREATE TABLE [dbo].[tblCompanyBOBCompany]
(
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[Adress] [nvarchar](400) NULL,
	[CurrentBalance] [money] NOT NULL,
	[CompanyUID] [nvarchar](200) NULL,

 CONSTRAINT [PK_dbo.tblCompanyBOBCompany] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO