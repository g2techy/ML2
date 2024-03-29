
/****** Object:  StoredProcedure [dbo].[P_Loan_GetPaymentList]    Script Date: 8/19/2017 9:39:13 PM ******/
DROP PROCEDURE [dbo].[P_Loan_GetPaymentList]
GO
/****** Object:  StoredProcedure [dbo].[P_Loan_GetPaymentList]    Script Date: 8/19/2017 9:39:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Jitendra Chaudhari
-- Create date: 26-03-2016
-- Description:	Get list of payment for given LoanID
-- =============================================
CREATE PROCEDURE [dbo].[P_Loan_GetPaymentList]( 
	@ClientID int,
	@LoanID int	
)
AS
		
BEGIN
	SET NOCOUNT ON;

	Select PD.LoanPayID, PD.PayDate, PD.PayAmount, PD.PayType, PD.PayComments,
		   (Case When PD.PayType = 1 Then 'Principal' When PD.PayType = 2 Then 'Interest' Else '' End) As PayTypeName 
	  From LoanPaymentDetails PD
	 Where PD.LoanID = @LoanID
	 Order By PD.PayDate;	
    
END



GO
