
/****** Object:  View [dbo].[V_Sale_SaleList]    Script Date: 8/19/2017 9:39:13 PM ******/
DROP VIEW [dbo].[V_Sale_SaleList]
GO
/****** Object:  View [dbo].[V_Sale_SaleList]    Script Date: 8/19/2017 9:39:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE View [dbo].[V_Sale_SaleList] As
With Sale_List As
(
Select SD.*,
	   TBR.Brokerage As Brokerage,
	   TPAY.PayAmount As TotalPayAmount,
	   TPAY.PayDate
 From V_Sale_SaleList_Basic SD
Left Join V_Sale_Total_Brokerage TBR on TBR.SaleID = SD.SaleID
Left Join V_Sale_Total_Payment TPAY on TPAY.SaleID = SD.SaleID
)
Select *,
	   LTrim(Str(Brokerage,10,2)) + '/' + LTrim(Str((NetSaleAmount * Brokerage/100),10,2)) As TotalBrokerage
  From Sale_List;



GO
