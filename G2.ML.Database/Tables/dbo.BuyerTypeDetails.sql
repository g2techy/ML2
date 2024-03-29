
/****** Object:  Table [dbo].[BuyerTypeDetails]    Script Date: 8/19/2017 9:39:13 PM ******/
DROP TABLE [dbo].[BuyerTypeDetails]
GO
/****** Object:  Table [dbo].[BuyerTypeDetails]    Script Date: 8/19/2017 9:39:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BuyerTypeDetails](
	[BuyerTypeID] [int] IDENTITY(1,1) NOT NULL,
	[BuyerTypeName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_BuyerTypeDetails] PRIMARY KEY CLUSTERED 
(
	[BuyerTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
