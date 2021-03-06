
/****** Object:  StoredProcedure [dbo].[P_Sale_BrokDistributionChartData]    Script Date: 8/19/2017 9:39:13 PM ******/
DROP PROCEDURE [dbo].[P_Sale_BrokDistributionChartData]
GO
/****** Object:  StoredProcedure [dbo].[P_Sale_BrokDistributionChartData]    Script Date: 8/19/2017 9:39:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jitendra Chaudhari
-- Create date: 31-03-2017
-- Description:	Brokerage distribution chart data
-- =============================================
CREATE PROCEDURE [dbo].[P_Sale_BrokDistributionChartData]
(
	@ClientID Int
)
AS  
BEGIN
Begin Try    
	Set NoCount On 
	Declare @MonthCnt int = -12;
	
	Declare @EndDate Date = GetDate();
	Declare @StDate Date = dbo.GetDateBeforeMonths(@EndDate, @MonthCnt);
	
	With BD As (
	Select BD.SaleID, 
		   ((BD.Brokerage * SD.NetSaleAmount)/100) As BrokerageAmt,
		   BD.BrokerID
	 From BrokerageDetails BD
	 Inner Join SalesDetails SD ON SD.SaleID = BD.SaleID
	 Where SD.IsDeleted = 0
	   And SD.UserID = @ClientID
	   And SD.SaleDate between @StDate And @EndDate
	),
	BD_I As (Select BrokerID,
		   Round(Sum(BrokerageAmt),2) As BrokerageAmt
	  From BD
	 Group By BrokerID)
	Select BD.BuyerName As BrokerName,
		   BrokerageAmt 
	  From BD_I Left Outer Join BuyerDetails BD On BD.BuyerID = BrokerID
	 Order By BrokerName;


End Try    
Begin Catch    
	DECLARE @ErrorMessage NVARCHAR(4000);    
    DECLARE @ErrorSeverity INT;    
    DECLARE @ErrorState INT;    
     
	SET @ErrorMessage = ERROR_MESSAGE();    
	SET @ErrorSeverity = ERROR_SEVERITY();    
	SET @ErrorState = ERROR_STATE();    
     
	Raiserror(@ErrorMessage,@ErrorSeverity,@ErrorState)    
End Catch

END




GO
