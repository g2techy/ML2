
ALTER TABLE [dbo].[BuyerTypeMapping] DROP CONSTRAINT [FK_BuyerTypeMapping_BuyerTypeDetails]
GO
ALTER TABLE [dbo].[BuyerTypeMapping] DROP CONSTRAINT [FK_BuyerTypeMapping_BuyerDetails]
GO
/****** Object:  Table [dbo].[BuyerTypeMapping]    Script Date: 8/19/2017 9:39:13 PM ******/
DROP TABLE [dbo].[BuyerTypeMapping]
GO
/****** Object:  Table [dbo].[BuyerTypeMapping]    Script Date: 8/19/2017 9:39:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BuyerTypeMapping](
	[BTMID] [int] IDENTITY(1,1) NOT NULL,
	[BuyerID] [int] NOT NULL,
	[BuyerTypeID] [int] NOT NULL,
	[Status] [int] NULL CONSTRAINT [DF_BuyerTypeMapping_Status]  DEFAULT ((1)),
 CONSTRAINT [PK_BuyerTypeMapping] PRIMARY KEY CLUSTERED 
(
	[BTMID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[BuyerTypeMapping]  WITH CHECK ADD  CONSTRAINT [FK_BuyerTypeMapping_BuyerDetails] FOREIGN KEY([BuyerID])
REFERENCES [dbo].[BuyerDetails] ([BuyerID])
GO
ALTER TABLE [dbo].[BuyerTypeMapping] CHECK CONSTRAINT [FK_BuyerTypeMapping_BuyerDetails]
GO
ALTER TABLE [dbo].[BuyerTypeMapping]  WITH CHECK ADD  CONSTRAINT [FK_BuyerTypeMapping_BuyerTypeDetails] FOREIGN KEY([BuyerTypeID])
REFERENCES [dbo].[BuyerTypeDetails] ([BuyerTypeID])
GO
ALTER TABLE [dbo].[BuyerTypeMapping] CHECK CONSTRAINT [FK_BuyerTypeMapping_BuyerTypeDetails]
GO
