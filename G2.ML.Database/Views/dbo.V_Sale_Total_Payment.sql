
/****** Object:  View [dbo].[V_Sale_Total_Payment]    Script Date: 8/19/2017 9:39:13 PM ******/
DROP VIEW [dbo].[V_Sale_Total_Payment]
GO
/****** Object:  View [dbo].[V_Sale_Total_Payment]    Script Date: 8/19/2017 9:39:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE View [dbo].[V_Sale_Total_Payment] As
Select PD.SaleID, sum(PD.PayAmount) As PayAmount, Max(PD.PayDate) As PayDate
From PaymentDetails PD
Group By PD.SaleID;

GO
