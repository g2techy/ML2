
/****** Object:  StoredProcedure [dbo].[P_Sale_AddBrokerage]    Script Date: 8/19/2017 9:39:13 PM ******/
DROP PROCEDURE [dbo].[P_Sale_AddBrokerage]
GO
/****** Object:  StoredProcedure [dbo].[P_Sale_AddBrokerage]    Script Date: 8/19/2017 9:39:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jitendra Chaudhari
-- Create date: 26-03-2016
-- Description:	Insert brokerage details for given SaleID
-- =============================================
CREATE PROCEDURE [dbo].[P_Sale_AddBrokerage]( 
	@BDID int output,
	@SaleID int,
	@BrokerID int,
	@Brokerage float	
)
AS
BEGIN
	
	Insert INto BrokerageDetails(SaleID, BrokerID, Brokerage, IsPaid, PayDate)
	Values(@SaleID, @BrokerID, @Brokerage, 0, '1900-01-01');
	
	Set @BDID = SCOPE_IDENTITY();
	
END

GO
