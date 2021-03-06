
/****** Object:  StoredProcedure [dbo].[P_Loan_GetCalcInterest]    Script Date: 8/19/2017 9:39:13 PM ******/
DROP PROCEDURE [dbo].[P_Loan_GetCalcInterest]
GO
/****** Object:  StoredProcedure [dbo].[P_Loan_GetCalcInterest]    Script Date: 8/19/2017 9:39:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jitendra Chaudhari
-- Create date: 06-08-2017
-- Description:	Calculate interest for given loan
-- =============================================
CREATE PROCEDURE [dbo].[P_Loan_GetCalcInterest](
	@ClientID int,
	@LoanID int,
	@IntAsOn date
)
AS
  Declare @TotalIntPaid Float;
  
BEGIN
	
   Select @TotalIntPaid = Sum(LP.PayAmount) 
     From LoanPaymentDetails LP 
	Where LP.LoanID = (Select LoanID From LoanDetails L Where L.UserID = @ClientID And L.LoanID = @LoanID)
	  And LP.PayType = 2;
	  
	With LP AS (
	Select L.StartDate, L.PrincipalAmount, L.MonthlyInterest, Isnull(LP.PayAmount, 0) As PayAmount, 
		   IsNull(LP.PayDate, @IntAsOn) As PayDate,
		   Round((L.MonthlyInterest * 12 / 365), 4) As DailyRate,
		   (Case When LP.PayDate Is Not Null Then 'P' Else 'U' End) AS PaidFlag
	  From LoanDetails L 
	  LEFT JOIN LoanPaymentDetails LP ON LP.LoanID = L.LoanID And LP.PayType  = 1 And LP.PayDate <= @IntAsOn
	Where L.LoanID = @LoanID And L.UserID = @ClientID
	),
	LP_I As (
	Select LP.*, 
		   SUM(LP.PayAmount) Over() AS TotalPaidAmount,
		   Count(1) Over() TotalRecords,
		   ROW_NUMBER() Over (Order By PayDate) As RowIndex
	  From LP
	),
	LP_II As (
		Select StartDate, PrincipalAmount, MonthlyInterest, PayAmount, PayDate, DailyRate, PaidFlag
		 From LP_I
		Union All
		Select StartDate, PrincipalAmount, MonthlyInterest, (L.PrincipalAmount - L.TotalPaidAmount) As PayAmount, @IntAsOn AS PayDate, DailyRate, 'U' As PaidFlag
		 From LP_I L 
		 WHERE L.PrincipalAmount > L.TotalPaidAmount And TotalRecords = RowIndex And PaidFlag = 'P'
	),
	LP_III AS
	(
		Select LP.*,
		   IsNull(LAG(LP.PayDate) Over(Order By PayDate), StartDate) As PrevPayDate,
		   IsNull(LAG(LP.PayAmount) Over(Order By PayDate), 0) As PrevPayAmount 
		  From LP_II LP
	),
	LP_IV AS (
	Select L.*,
		   DATEDIFF(D, L.PrevPayDate, L.PayDate) As IntForDays,
		   Sum(L.PrevPayAmount) Over(Order By PayDate ROWS Between Unbounded Preceding And Current Row) As PaidPrincipal        
	  From LP_III L
	),
	LP_V AS (
	Select L.*,
		   (L.PrincipalAmount - L.PaidPrincipal) As IntOnAmount 
	  FRom LP_IV L
	)
	Select (Case When L.PaidFlag = 'P' Then L.PayAmount Else NUll End) As PayAmount,
		   (Case When L.PaidFlag = 'P' Then L.PayDate Else NUll End) As PayDate,
		   L.DailyRate, L.IntForDays, L.IntOnAmount, 
		   ((IntForDays * DailyRate * IntOnAmount)/100) As CalcIntAmount,
		   IsNull(@TotalIntPaid, 0) As TotalIntPaid 
	  From LP_V L;
      
END



GO
