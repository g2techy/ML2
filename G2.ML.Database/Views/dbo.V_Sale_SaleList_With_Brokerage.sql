
/****** Object:  View [dbo].[V_Sale_SaleList_With_Brokerage]    Script Date: 8/19/2017 9:39:13 PM ******/
DROP VIEW [dbo].[V_Sale_SaleList_With_Brokerage]
GO
/****** Object:  View [dbo].[V_Sale_SaleList_With_Brokerage]    Script Date: 8/19/2017 9:39:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE View [dbo].[V_Sale_SaleList_With_Brokerage] as 
With Sale_List As
(
Select SD.*,
	   IsNull(TBR.SelfBrokerage, 0) As SelfBrokerage,
	   IsNull(TBR.OtherBrokerage, 0) As OtherBrokerage,
	   TPAY.PayAmount As TotalPayAmount,
	   TPAY.PayDate
 From V_Sale_SaleList_Basic SD
Left Join V_Sale_Total_Brokerage_All TBR on TBR.SaleID = SD.SaleID
Left Join V_Sale_Total_Payment TPAY on TPAY.SaleID = SD.SaleID
)
Select *,
	   (NetSaleAmount * SelfBrokerage/100) As SelfBrokerageAmount,
	   (NetSaleAmount * OtherBrokerage/100) As OtherBrokerageAmount
  From Sale_List;






GO
