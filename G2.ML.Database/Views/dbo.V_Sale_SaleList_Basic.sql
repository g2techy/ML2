
/****** Object:  View [dbo].[V_Sale_SaleList_Basic]    Script Date: 8/19/2017 9:39:13 PM ******/
DROP VIEW [dbo].[V_Sale_SaleList_Basic]
GO
/****** Object:  View [dbo].[V_Sale_SaleList_Basic]    Script Date: 8/19/2017 9:39:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE View [dbo].[V_Sale_SaleList_Basic] As
Select SD.SaleID,
	   SD.RefNo,
	   SD.SaleDate,
	   SD.UserID,
	   Saller.BuyerName AS Saller,
	   Buyer.BuyerName AS Buyer,
	   SD.Weight As TotalWeight,
	   SD.RejectionWt,
	   SD.SelectionWt,
	   SD.NetSaleAmount,
	   SD.UnitPrice,	   
	   SD.DueDays,
	   SD.SallerID,
	   SD.BuyerID,
	   SD.Status As SaleStatusID,
	   SSD.SaleStatusValue As Status,
	   DateAdd(DAY, SD.DueDays, SD.SaleDate) AS DueDate,
	   SD.LessPer	   
 From SalesDetails SD
Left Join BuyerDetails Saller On Saller.BuyerID = SD.SallerID
Left Join BuyerDetails Buyer On Buyer.BuyerID = SD.BuyerID
LEFT Join SalesStatusDetails SSD ON SSD.SaleStatusID = SD.Status
Where SD.IsDeleted = 0




GO
