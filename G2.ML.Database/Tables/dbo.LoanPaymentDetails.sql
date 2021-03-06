
ALTER TABLE [dbo].[LoanPaymentDetails] DROP CONSTRAINT [FK_LoanPaymentDetails_LoanDetails]
GO
/****** Object:  Table [dbo].[LoanPaymentDetails]    Script Date: 8/19/2017 9:39:13 PM ******/
DROP TABLE [dbo].[LoanPaymentDetails]
GO
/****** Object:  Table [dbo].[LoanPaymentDetails]    Script Date: 8/19/2017 9:39:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[LoanPaymentDetails](
	[LoanPayID] [int] IDENTITY(1,1) NOT NULL,
	[LoanID] [int] NOT NULL,
	[PayAmount] [decimal](18, 4) NOT NULL,
	[PayType] [int] NOT NULL,
	[PayDate] [datetime] NOT NULL,
	[PayComments] [varchar](1000) NULL,
 CONSTRAINT [PK_LoanPaymentDetails] PRIMARY KEY CLUSTERED 
(
	[LoanPayID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[LoanPaymentDetails]  WITH CHECK ADD  CONSTRAINT [FK_LoanPaymentDetails_LoanDetails] FOREIGN KEY([LoanID])
REFERENCES [dbo].[LoanDetails] ([LoanID])
GO
ALTER TABLE [dbo].[LoanPaymentDetails] CHECK CONSTRAINT [FK_LoanPaymentDetails_LoanDetails]
GO
