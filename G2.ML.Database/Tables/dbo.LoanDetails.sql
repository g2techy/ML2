
ALTER TABLE [dbo].[LoanDetails] DROP CONSTRAINT [FK_LoanDetails_UserDetails]
GO
ALTER TABLE [dbo].[LoanDetails] DROP CONSTRAINT [FK_LoanDetails_BuyerDetails]
GO
/****** Object:  Table [dbo].[LoanDetails]    Script Date: 8/19/2017 9:39:13 PM ******/
DROP TABLE [dbo].[LoanDetails]
GO
/****** Object:  Table [dbo].[LoanDetails]    Script Date: 8/19/2017 9:39:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[LoanDetails](
	[LoanID] [int] IDENTITY(1,1) NOT NULL,
	[RefNo] [varchar](20) NOT NULL,
	[StartDate] [datetime] NOT NULL,
	[EndDate] [datetime] NULL,
	[BorrowerID] [int] NOT NULL,
	[PrincipalAmount] [decimal](18, 4) NOT NULL,
	[MonthlyInterest] [decimal](18, 4) NOT NULL,
	[Status] [int] NULL,
	[UserID] [int] NOT NULL,
	[IsDeleted] [int] NULL CONSTRAINT [DF_LoanDetails_IsDeleted]  DEFAULT ((0)),
	[Comments] [varchar](1000) NULL,
 CONSTRAINT [PK_LoanDetails] PRIMARY KEY CLUSTERED 
(
	[LoanID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[LoanDetails]  WITH CHECK ADD  CONSTRAINT [FK_LoanDetails_BuyerDetails] FOREIGN KEY([BorrowerID])
REFERENCES [dbo].[BuyerDetails] ([BuyerID])
GO
ALTER TABLE [dbo].[LoanDetails] CHECK CONSTRAINT [FK_LoanDetails_BuyerDetails]
GO
ALTER TABLE [dbo].[LoanDetails]  WITH CHECK ADD  CONSTRAINT [FK_LoanDetails_UserDetails] FOREIGN KEY([UserID])
REFERENCES [dbo].[UserDetails] ([UserID])
GO
ALTER TABLE [dbo].[LoanDetails] CHECK CONSTRAINT [FK_LoanDetails_UserDetails]
GO
