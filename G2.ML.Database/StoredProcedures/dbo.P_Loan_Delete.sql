
/****** Object:  StoredProcedure [dbo].[P_Loan_Delete]    Script Date: 8/19/2017 9:39:13 PM ******/
DROP PROCEDURE [dbo].[P_Loan_Delete]
GO
/****** Object:  StoredProcedure [dbo].[P_Loan_Delete]    Script Date: 8/19/2017 9:39:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jitendra Chaudhari
-- Create date: 15-07-2017
-- Description:	Update isDeleted column for given LoanID
-- =============================================
CREATE PROCEDURE [dbo].[P_Loan_Delete]( 
	@ClientID int,
	@LoanID int
)
AS
BEGIN
	
	Update LoanDetails Set IsDeleted = 1
	 Where LoanID = @LoanID
	   And UserID = @ClientID;
	
	Select @LoanID As LoanID;
END


GO
