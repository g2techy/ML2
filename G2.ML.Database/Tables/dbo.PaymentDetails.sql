
ALTER TABLE [dbo].[PaymentDetails] DROP CONSTRAINT [FK_PaymentDetails_SalesDetails]
GO
/****** Object:  Table [dbo].[PaymentDetails]    Script Date: 8/19/2017 9:39:13 PM ******/
DROP TABLE [dbo].[PaymentDetails]
GO
/****** Object:  Table [dbo].[PaymentDetails]    Script Date: 8/19/2017 9:39:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PaymentDetails](
	[PayID] [int] IDENTITY(1,1) NOT NULL,
	[SaleID] [int] NOT NULL,
	[PayDate] [datetime] NOT NULL,
	[PayAmount] [decimal](18, 4) NULL,
	[PayCourierFrom] [nvarchar](50) NULL,
	[PayCourierTo] [nvarchar](50) NULL,
 CONSTRAINT [PK_PaymentDetails] PRIMARY KEY CLUSTERED 
(
	[PayID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[PaymentDetails]  WITH CHECK ADD  CONSTRAINT [FK_PaymentDetails_SalesDetails] FOREIGN KEY([SaleID])
REFERENCES [dbo].[SalesDetails] ([SaleID])
GO
ALTER TABLE [dbo].[PaymentDetails] CHECK CONSTRAINT [FK_PaymentDetails_SalesDetails]
GO
