
ALTER TABLE [dbo].[BrokerageDetails] DROP CONSTRAINT [FK_BrokerageDetails_SalesDetails]
GO
ALTER TABLE [dbo].[BrokerageDetails] DROP CONSTRAINT [FK_BrokerageDetails_BuyerDetails]
GO
/****** Object:  Table [dbo].[BrokerageDetails]    Script Date: 8/19/2017 9:39:13 PM ******/
DROP TABLE [dbo].[BrokerageDetails]
GO
/****** Object:  Table [dbo].[BrokerageDetails]    Script Date: 8/19/2017 9:39:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BrokerageDetails](
	[BDID] [int] IDENTITY(1,1) NOT NULL,
	[SaleID] [int] NOT NULL,
	[BrokerID] [int] NOT NULL,
	[Brokerage] [decimal](18, 2) NOT NULL,
	[IsPaid] [bit] NULL,
	[PayDate] [date] NULL,
	[PayComments] [nvarchar](1000) NULL,
 CONSTRAINT [PK_BrokerageDetails] PRIMARY KEY CLUSTERED 
(
	[BDID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[BrokerageDetails]  WITH CHECK ADD  CONSTRAINT [FK_BrokerageDetails_BuyerDetails] FOREIGN KEY([BrokerID])
REFERENCES [dbo].[BuyerDetails] ([BuyerID])
GO
ALTER TABLE [dbo].[BrokerageDetails] CHECK CONSTRAINT [FK_BrokerageDetails_BuyerDetails]
GO
ALTER TABLE [dbo].[BrokerageDetails]  WITH CHECK ADD  CONSTRAINT [FK_BrokerageDetails_SalesDetails] FOREIGN KEY([SaleID])
REFERENCES [dbo].[SalesDetails] ([SaleID])
GO
ALTER TABLE [dbo].[BrokerageDetails] CHECK CONSTRAINT [FK_BrokerageDetails_SalesDetails]
GO
