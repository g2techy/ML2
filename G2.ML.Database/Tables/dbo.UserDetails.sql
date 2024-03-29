
/****** Object:  Table [dbo].[UserDetails]    Script Date: 8/19/2017 9:39:13 PM ******/
DROP TABLE [dbo].[UserDetails]
GO
/****** Object:  Table [dbo].[UserDetails]    Script Date: 8/19/2017 9:39:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserDetails](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](100) NULL,
	[FirstName] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NULL,
	[CompanyName] [nvarchar](1000) NULL,
	[CompanyAddress] [nvarchar](1000) NULL,
	[PhoneNo] [nvarchar](50) NULL,
	[MobileNo] [nvarchar](50) NULL,
	[PwdAttemptCnt] [int] NULL,
	[IsDeleted] [int] NULL,
 CONSTRAINT [PK_UserDetails] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
