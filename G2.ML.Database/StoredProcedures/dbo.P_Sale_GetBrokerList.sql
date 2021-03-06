
/****** Object:  StoredProcedure [dbo].[P_Sale_GetBrokerList]    Script Date: 8/19/2017 9:39:13 PM ******/
DROP PROCEDURE [dbo].[P_Sale_GetBrokerList]
GO
/****** Object:  StoredProcedure [dbo].[P_Sale_GetBrokerList]    Script Date: 8/19/2017 9:39:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jitendra Chaudhari
-- Create date: 26-03-2016
-- Description:	Get list of brokerage for given SaleID
-- =============================================
CREATE PROCEDURE [dbo].[P_Sale_GetBrokerList]( 
	@ClientID int,
	@SaleID int	
)
AS
	Declare @NetSaleAmount float;
	
BEGIN
	SET NOCOUNT ON;

	Select @NetSaleAmount = SD.NetSaleAmount
	  From SalesDetails SD 
	 Where SD.UserID = @ClientID
	   And SD.SaleID = @SaleID;
	   
	Select BD.BDID,
		   BD.SaleID,
		   BD.BrokerID,
		   BYD.FirstName + ' ' + BYD.LastName + '(' + BYD.BuyerCode + ')' As BrokerName,
		   BD.Brokerage,
		   (@NetSaleAmount * (BD.Brokerage/100)) As BrokerageAmount,
		   BD.IsPaid,
		   BD.PayDate,
		   BD.PayComments
	  From BrokerageDetails BD
	  Left Join BuyerDetails BYD On BYD.BuyerID = BD.BrokerID
	 Where BD.SaleID = @SaleID;	
    
END

GO
