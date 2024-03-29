
/****** Object:  StoredProcedure [dbo].[P_Loan_DeletePayment]    Script Date: 8/19/2017 9:39:13 PM ******/
DROP PROCEDURE [dbo].[P_Loan_DeletePayment]
GO
/****** Object:  StoredProcedure [dbo].[P_Loan_DeletePayment]    Script Date: 8/19/2017 9:39:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Jitendra Chaudhari
-- Create date: 15-07-2017
-- Description:	Delete payment details for given LoanID
-- =============================================
CREATE PROCEDURE [dbo].[P_Loan_DeletePayment]( 
	@LoanPayID int,
	@LoanID int OutPut
)
AS
BEGIN
	
	Select @LoanID = LoanID
	  From LoanPaymentDetails PD
	 Where PD.LoanPayID = @LoanPayID;
	 
	Delete From LoanPaymentDetails
	 Where LoanPayID = @LoanPayID;
	 
	Exec P_Loan_UpdateStatus @LoanID = @LoanID;
	
END



GO
