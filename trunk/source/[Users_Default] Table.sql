USE [SBOWEB]
GO

/****** Object:  Table [dbo].[Users_Default]    Script Date: 10/09/2012 18:39:16 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Users_Default](
	[ID] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](200) NOT NULL,
	[DefaultCode] [nvarchar](50) NULL,
	[DefaultValue] [nvarchar](500) NULL,
	[DefaultType] [smallint] NULL,
 CONSTRAINT [PK_Users_Default] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

Du lieu vi du
ID	UserId	DefaultCode		DefaultValue									DefaultType
1	C00001	SAPConnection	DMS_VHF;manager;1111;FRANK;sa;sap;FRANK;2008	1
