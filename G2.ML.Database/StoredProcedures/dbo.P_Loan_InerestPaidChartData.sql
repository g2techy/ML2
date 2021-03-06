
/****** Object:  StoredProcedure [dbo].[P_Loan_InerestPaidChartData]    Script Date: 8/19/2017 9:39:13 PM ******/
DROP PROCEDURE [dbo].[P_Loan_InerestPaidChartData]
GO
/****** Object:  StoredProcedure [dbo].[P_Loan_InerestPaidChartData]    Script Date: 8/19/2017 9:39:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Jitendra Chaudhari
-- Create date: 31-03-2017
-- Description:	Loan data chart
-- =============================================
CREATE PROCEDURE [dbo].[P_Loan_InerestPaidChartData]
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
	Select LP.PayAmount,
		   LP.LoanID,
		   Format(LP.PayDate, 'yyyy-MM') AS PayMonth,
		   L.Borrower
	 From LoanPaymentDetails LP
	Inner Join V_Loan_LoanList_Basic L On L.LoanID = LP.LoanID
	Where L.UserID = @ClientID
	  And LP.PayDate between @StDate And @EndDate
	  And LP.PayType = 2
	)
	Select Round(Sum(PayAmount),2) As TotalPayAmount,
		   Borrower,
		   PayMonth
	 From SD
	 Group by PayMonth, Borrower
	 order By PayMonth, Borrower;

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
