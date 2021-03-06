
/****** Object:  View [dbo].[V_Loan_LoanList_Basic]    Script Date: 8/19/2017 9:39:13 PM ******/
DROP VIEW [dbo].[V_Loan_LoanList_Basic]
GO
/****** Object:  View [dbo].[V_Loan_LoanList_Basic]    Script Date: 8/19/2017 9:39:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE View [dbo].[V_Loan_LoanList_Basic] As
Select LD.LoanID,
	   LD.RefNo,
	   LD.StartDate,
	   LD.EndDate,
	   LD.BorrowerID,
	   B.BuyerName AS Borrower,
	   LD.PrincipalAmount,
	   LD.MonthlyInterest,
	   LD.Comments,
	   LD.Status As StatusID,
	   LD.UserID,
	   SD.StatusValue As StatusName
 From LoanDetails LD
Left Join BuyerDetails B On B.BuyerID = LD.BorrowerID
LEFT Join LoanStatusDetails SD ON SD.StatusID = LD.Status
Where LD.IsDeleted = 0





GO
