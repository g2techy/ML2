
/****** Object:  View [dbo].[V_Loan_LoanList]    Script Date: 8/19/2017 9:39:13 PM ******/
DROP VIEW [dbo].[V_Loan_LoanList]
GO
/****** Object:  View [dbo].[V_Loan_LoanList]    Script Date: 8/19/2017 9:39:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




CREATE View [dbo].[V_Loan_LoanList] As
Select LD.*,
	   TPAY.PayAmount As TotalPayAmount,
	   TPAY.TotalPrincipalPaid,
	   TPAY.TotalInterestPaid,
	   TPAY.PayDate
 From V_Loan_LoanList_Basic LD
Left Join V_Loan_Total_Payment TPAY on TPAY.LoanID = LD.LoanID






GO
