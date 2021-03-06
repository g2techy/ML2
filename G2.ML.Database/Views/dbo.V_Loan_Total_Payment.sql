
/****** Object:  View [dbo].[V_Loan_Total_Payment]    Script Date: 8/19/2017 9:39:13 PM ******/
DROP VIEW [dbo].[V_Loan_Total_Payment]
GO
/****** Object:  View [dbo].[V_Loan_Total_Payment]    Script Date: 8/19/2017 9:39:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE View [dbo].[V_Loan_Total_Payment] As
Select PD.LoanID, sum(PD.PayAmount) As PayAmount, Max(PD.PayDate) As PayDate, 
	   SUM(Case When PD.PayType = 1 Then PD.PayAmount Else 0 End) As TotalPrincipalPaid,
	   SUM(Case When PD.PayType = 2 Then PD.PayAmount Else 0 End) As TotalInterestPaid
From LoanPaymentDetails PD
Group By PD.LoanID;




GO
