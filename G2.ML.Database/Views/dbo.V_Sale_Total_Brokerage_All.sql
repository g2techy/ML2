
/****** Object:  View [dbo].[V_Sale_Total_Brokerage_All]    Script Date: 8/19/2017 9:39:13 PM ******/
DROP VIEW [dbo].[V_Sale_Total_Brokerage_All]
GO
/****** Object:  View [dbo].[V_Sale_Total_Brokerage_All]    Script Date: 8/19/2017 9:39:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE View [dbo].[V_Sale_Total_Brokerage_All] AS
Select BD.SaleID, 
       sum(BD.Brokerage) As Brokerage,
       Sum((Case When BRO.IsSelf = 1 Then BD.Brokerage Else 0 End)) As SelfBrokerage,
       Sum((Case When BRO.IsSelf <> 1 Then BD.Brokerage Else 0 End)) As OtherBrokerage 
 From BrokerageDetails BD
 Inner Join BuyerDetails BRO On BRO.BuyerID = BD.BrokerID
Group By BD.SaleID;

GO
