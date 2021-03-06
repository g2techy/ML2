
/****** Object:  StoredProcedure [dbo].[P_Loan_ChartData]    Script Date: 8/19/2017 9:39:13 PM ******/
DROP PROCEDURE [dbo].[P_Loan_ChartData]
GO
/****** Object:  StoredProcedure [dbo].[P_Loan_ChartData]    Script Date: 8/19/2017 9:39:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jitendra Chaudhari
-- Create date: 22-07-2017
-- Description:	Loan data chart
-- =============================================
CREATE PROCEDURE [dbo].[P_Loan_ChartData]
(
	@ClientID Int
)
AS  
BEGIN
Begin Try    
	Set NoCount On 
	Declare @MonthCnt int = -24;
	
	Declare @EndDate Date = GetDate();
	Declare @StDate Date = dbo.GetDateBeforeMonths(@EndDate, @MonthCnt);
	
	With SD AS (
	Select L.PrincipalAmount,
		   L.TotalInterestPaid,
		   L.TotalPrincipalPaid,
		   L.Borrower
	 From V_Loan_LoanList L
	Where L.UserID = @ClientID
	  And L.StartDate between @StDate And @EndDate
	)
	Select Round(Sum(PrincipalAmount),2) As TotalPrincipalAmount,
		   Round(Sum(TotalInterestPaid),2) As TotalInterestPaid,
		   Round(Sum(TotalPrincipalPaid),2) As TotalPrincipalPaid,
		   Borrower
	 From SD
	 Group by Borrower
	 order By Borrower;

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
