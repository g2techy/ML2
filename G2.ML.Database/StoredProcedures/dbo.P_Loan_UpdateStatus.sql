
/****** Object:  StoredProcedure [dbo].[P_Loan_UpdateStatus]    Script Date: 8/19/2017 9:39:13 PM ******/
DROP PROCEDURE [dbo].[P_Loan_UpdateStatus]
GO
/****** Object:  StoredProcedure [dbo].[P_Loan_UpdateStatus]    Script Date: 8/19/2017 9:39:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Jitendra Chaudhari
-- Create date: 15-07-2017
-- Description:	Update loan status column for given loanID
-- =============================================
CREATE PROCEDURE [dbo].[P_Loan_UpdateStatus]( 
	@LoanID int,
	@Status int = 0
)
AS
    Declare @PrincipalAmout float;
    Declare @PrincipalPaid float;
    Declare @NewStatus int;
    Declare @OldStaus int;    
BEGIN
	
	Select @OldStaus = Status From LoanDetails
	Where LoanID = @LoanID;
	
	If @OldStaus = 4
	BEGIN
		Return;
	END
	
	IF @Status <> 0 
	BEGIN	
		Update LoanDetails Set Status = @Status
		 Where LoanID = @LoanID;
	END
	ELSE
	BEGIN
		Select @PrincipalAmout = ISNull(V.PrincipalAmount,0),
			   @PrincipalPaid = ISNull(V.TotalPrincipalPaid,0)
		  From V_Loan_LoanList V
		Where V.LoanID = @LoanID;
		
		If @PrincipalPaid >= @PrincipalAmout 
			Select @NewStatus = 3;
		Else If @PrincipalPaid = 0 
			Select @NewStatus = 1;
		Else
			 Select @NewStatus = 2;
		
		Update LoanDetails Set Status = @NewStatus
		 Where LoanID = @LoanID;
		
	END	
	
END



GO
