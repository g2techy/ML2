
/****** Object:  StoredProcedure [dbo].[P_Loan_AddUpdate]    Script Date: 8/19/2017 9:39:13 PM ******/
DROP PROCEDURE [dbo].[P_Loan_AddUpdate]
GO
/****** Object:  StoredProcedure [dbo].[P_Loan_AddUpdate]    Script Date: 8/19/2017 9:39:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jitendra Chaudhari
-- Create date: 15-07-2017
-- Description:	Insert/Update loan details
-- =============================================
CREATE PROCEDURE [dbo].[P_Loan_AddUpdate](
	@ClientID int,
	@LoanID int output,
	@StartDate Date,
	@EndDate Date = NULL,
	@BorrowerID int,
	@PrincipalAmount float,
	@MonthlyInterest float,
	@Comments varchar(1000) = NULL
)
AS
BEGIN
	SET NOCOUNT ON;

	IF (@LoanID = 0)
		Begin
		
		Insert into LoanDetails(StartDate
		  ,EndDate
		  ,BorrowerID
		  ,PrincipalAmount
		  ,MonthlyInterest
		  ,Comments
		  ,UserID
		  ,Status
		  ,RefNo)
		Values(@StartDate, @EndDate, @BorrowerID, @PrincipalAmount, @MonthlyInterest, @Comments, @ClientID, 
			  1, dbo.F_Loan_GenerateRefNo(@StartDate));
      
        Set @LoanID = SCOPE_IDENTITY();
        
		End
	Else
		Begin			
			Update LoanDetails 
			   Set StartDate = @StartDate,
				   EndDate = @EndDate,
				   BorrowerID = @BorrowerID,
				   PrincipalAmount = @PrincipalAmount,
				   MonthlyInterest = @MonthlyInterest,
				   Comments = @Comments
			 Where LoanID = @LoanID
			   And UserID = @ClientID;
						
		End	    
END


GO
