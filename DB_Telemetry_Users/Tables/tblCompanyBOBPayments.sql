CREATE TABLE [dbo].[tblCompanyBOBPayments]
(
	[DateMessage] [datetime] NOT NULL,
	[id_Company] [int] NOT NULL FOREIGN KEY REFERENCES [dbo].[tblCompanyBOBCompany] ([id]),
	[MoneyMove] [money] NOT NULL,
	[Comment] NVARCHAR(MAX) NULL,
 CONSTRAINT [PK_dbo.tblCompanyBOBPayments] PRIMARY KEY CLUSTERED 
(
	[DateMessage] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
