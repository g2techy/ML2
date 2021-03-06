
/****** Object:  StoredProcedure [dbo].[P_Loan_AddPayment]    Script Date: 8/19/2017 9:39:13 PM ******/
DROP PROCEDURE [dbo].[P_Loan_AddPayment]
GO
/****** Object:  StoredProcedure [dbo].[P_Loan_AddPayment]    Script Date: 8/19/2017 9:39:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Jitendra Chaudhari
-- Create date: 15-07-2017
-- Description:	Insert payment details for given LoanID
-- =============================================
CREATE PROCEDURE [dbo].[P_Loan_AddPayment]( 
	@LoanPayID int output,
	@LoanID int,
	@PayDate Date,
	@PayAmount float,
	@PayType Int,
	@PayComments varchar(1000)
)
AS
BEGIN
	
	Insert Into LoanPaymentDetails(LoanID, PayDate, PayAmount, PayType, PayComments)
	Values(@LoanID, @PayDate, @PayAmount, @PayType, @PayComments);
	
	Exec P_Loan_UpdateStatus @LoanID = @LoanID;
	
	Set @LoanPayID = SCOPE_IDENTITY();
	
END



GO
