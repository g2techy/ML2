
/****** Object:  StoredProcedure [dbo].[P_Sale_BrokerageChartData]    Script Date: 8/19/2017 9:39:13 PM ******/
DROP PROCEDURE [dbo].[P_Sale_BrokerageChartData]
GO
/****** Object:  StoredProcedure [dbo].[P_Sale_BrokerageChartData]    Script Date: 8/19/2017 9:39:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jitendra Chaudhari
-- Create date: 31-03-2017
-- Description:	Brokerage data chart
-- =============================================
CREATE PROCEDURE [dbo].[P_Sale_BrokerageChartData]
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
	
	With SD AS (
	Select (BD.SelfBrokerage * SD.NetSaleAmount) AS BrokerageSelf,
		   (BD.OtherBrokerage * SD.NetSaleAmount) As BrokerageOthers,
		   Format(SD.SaleDate, 'yyyy-MM') AS Category
	 From SalesDetails SD
	 Inner Join V_Sale_Total_Brokerage_All BD On BD.SaleID = SD.SaleID
	Where SD.IsDeleted = 0
	  ANd SD.UserID = @ClientID
	  And SD.SaleDate between @StDate And @EndDate
	)
	Select Round(Sum(BrokerageSelf),2) As BrokerageSelf,
		   Round(Sum(BrokerageOthers),2) As BrokerageOthers,
		   Category
	 From SD
	 Group by Category
	 order By Category;

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
