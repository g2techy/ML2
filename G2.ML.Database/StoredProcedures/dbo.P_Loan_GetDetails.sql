
/****** Object:  StoredProcedure [dbo].[P_Loan_GetDetails]    Script Date: 8/19/2017 9:39:13 PM ******/
DROP PROCEDURE [dbo].[P_Loan_GetDetails]
GO
/****** Object:  StoredProcedure [dbo].[P_Loan_GetDetails]    Script Date: 8/19/2017 9:39:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jitendra Chaudhari
-- Create date: 15-07-2017
-- Description:	Get loan details for given LoanID and ClientID
-- =============================================
CREATE PROCEDURE [dbo].[P_Loan_GetDetails]( 
	@ClientID int,
	@LoanID int	
)
AS
BEGIN
	
	Select * From LoanDetails LD Where LD.LoanID = @LoanID And LD.UserID = @ClientID;
	
END


GO
