
/****** Object:  StoredProcedure [dbo].[P_Report_GetLoanList]    Script Date: 8/19/2017 9:39:13 PM ******/
DROP PROCEDURE [dbo].[P_Report_GetLoanList]
GO
/****** Object:  StoredProcedure [dbo].[P_Report_GetLoanList]    Script Date: 8/19/2017 9:39:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jitendra Chaudhari
-- Create date: 22-07-2017
-- Description:	Search loan list
-- =============================================
CREATE PROCEDURE [dbo].[P_Report_GetLoanList](
	@ClientID int,
	@StartDate varchar(100) = '',
	@EndDate varchar(100) = '',
	@BorrowerID int = 0,
	@Status int = 0
)
AS
  Declare @v_Query varchar(4000);
  Declare @v_WhereClause varchar(4000);

BEGIN
	
  Set @v_WhereClause  = ' And 1 = 1';
  
  Set @v_Query = 'Select RefNo
				  ,Replace(Convert(varchar(11),StartDate,106), '' '', ''-'')  As [Start Date]
				  ,Replace(Convert(varchar(11),EndDate,106), '' '', ''-'')  As [End Date]
				  ,Borrower
				  ,cast(PrincipalAmount as Decimal(10,2)) As [Principal Amount]
				  ,cast(MonthlyInterest as Decimal(10,2))  As [Monthly Interest]
				  ,cast(TotalPrincipalPaid as Decimal(10,2)) as [Principal Paid]
				  ,cast(TotalInterestPaid as Decimal(10,2)) as [Interest Paid]
				  ,cast(TotalPayAmount as Decimal(10,2))  AS [Total Payment]
				  ,Replace(Convert(varchar(11),PayDate,106), '' '', ''-'')  As [Pay Date]
				  ,StatusName As Status
			 From V_Loan_LoanList V
			Where V.UserID = ' + Cast(@ClientID as Varchar);
				 
  If @StartDate != '' And @EndDate != ''
  BEGIN
	Set @v_WhereClause = ' And V.StartDate Between ''' + @StartDate + ''' And ''' + @EndDate + '''';
  END
  Else If @StartDate != ''
  BEGIN
	Set @v_WhereClause = ' And V.StartDate >= ''' + @StartDate + '''';
  END
  Else If @EndDate != ''
  BEGIN
	Set @v_WhereClause = ' And V.StartDate <= ''' + @EndDate + '''';
  END

  If @BorrowerID != 0 
  Begin
	Set @v_WhereClause = @v_WhereClause + ' And V.BorrowerID = ' + Cast(@BorrowerID As varchar(10));
  End	
  
  If @Status != 0 
  Begin
	Set @v_WhereClause = @v_WhereClause + ' And V.StatusID = ' + Cast(@Status As varchar(10));
  End
  
  Set @v_Query = @v_Query + @v_WhereClause;
  
  Print ' Query: ' + @v_Query
    
  Execute(@v_Query);
      
END

GO
