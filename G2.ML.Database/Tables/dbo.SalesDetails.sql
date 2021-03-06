
ALTER TABLE [dbo].[SalesDetails] DROP CONSTRAINT [FK_SalesDetails_UserDetails]
GO
ALTER TABLE [dbo].[SalesDetails] DROP CONSTRAINT [FK_SalesDetails_BuyerDetails_Saller]
GO
ALTER TABLE [dbo].[SalesDetails] DROP CONSTRAINT [FK_SalesDetails_BuyerDetails_Buyer]
GO
/****** Object:  Table [dbo].[SalesDetails]    Script Date: 8/19/2017 9:39:13 PM ******/
DROP TABLE [dbo].[SalesDetails]
GO
/****** Object:  Table [dbo].[SalesDetails]    Script Date: 8/19/2017 9:39:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SalesDetails](
	[SaleID] [int] IDENTITY(1,1) NOT NULL,
	[SaleDate] [datetime] NOT NULL,
	[UserID] [int] NOT NULL,
	[SallerID] [int] NOT NULL,
	[BuyerID] [int] NOT NULL,
	[Weight] [decimal](18, 4) NOT NULL,
	[RejectionWt] [decimal](18, 4) NULL,
	[UnitPrice] [decimal](18, 4) NULL,
	[Status] [int] NULL CONSTRAINT [DF_SalesDetails_Status]  DEFAULT ((1)),
	[DueDays] [int] NULL,
	[IsDeleted] [int] NULL DEFAULT ((0)),
	[RefNo] [varchar](20) NOT NULL,
	[LessPer] [decimal](18, 4) NULL,
	[SelectionWt]  AS ([Weight]-[RejectionWt]),
	[NetSaleAmount]  AS (([Weight]-[RejectionWt])*[UnitPrice]-((([Weight]-[RejectionWt])*[UnitPrice])*[LessPer])/(100)),
 CONSTRAINT [PK_SalesDetails] PRIMARY KEY CLUSTERED 
(
	[SaleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[SalesDetails]  WITH CHECK ADD  CONSTRAINT [FK_SalesDetails_BuyerDetails_Buyer] FOREIGN KEY([BuyerID])
REFERENCES [dbo].[BuyerDetails] ([BuyerID])
GO
ALTER TABLE [dbo].[SalesDetails] CHECK CONSTRAINT [FK_SalesDetails_BuyerDetails_Buyer]
GO
ALTER TABLE [dbo].[SalesDetails]  WITH CHECK ADD  CONSTRAINT [FK_SalesDetails_BuyerDetails_Saller] FOREIGN KEY([SallerID])
REFERENCES [dbo].[BuyerDetails] ([BuyerID])
GO
ALTER TABLE [dbo].[SalesDetails] CHECK CONSTRAINT [FK_SalesDetails_BuyerDetails_Saller]
GO
ALTER TABLE [dbo].[SalesDetails]  WITH CHECK ADD  CONSTRAINT [FK_SalesDetails_UserDetails] FOREIGN KEY([UserID])
REFERENCES [dbo].[UserDetails] ([UserID])
GO
ALTER TABLE [dbo].[SalesDetails] CHECK CONSTRAINT [FK_SalesDetails_UserDetails]
GO
