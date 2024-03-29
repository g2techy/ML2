
/****** Object:  Table [dbo].[BuyerDetails]    Script Date: 8/19/2017 9:39:13 PM ******/
DROP TABLE [dbo].[BuyerDetails]
GO
/****** Object:  Table [dbo].[BuyerDetails]    Script Date: 8/19/2017 9:39:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BuyerDetails](
	[BuyerID] [int] IDENTITY(1,1) NOT NULL,
	[BuyerCode] [nvarchar](10) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NULL,
	[PhoneNo] [nvarchar](50) NULL,
	[MobileNo] [nvarchar](50) NULL,
	[IsDeleted] [int] NULL CONSTRAINT [DF_BuyerDetails_Status]  DEFAULT ((1)),
	[IsSelf] [int] NULL CONSTRAINT [DF_BuyerDetails_IsSelf]  DEFAULT ((0)),
	[UserID] [int] NULL,
	[BuyerName]  AS ((((([FirstName]+' ')+[LastName])+' (')+[BuyerCode])+')'),
 CONSTRAINT [PK_BuyerDetails] PRIMARY KEY CLUSTERED 
(
	[BuyerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
