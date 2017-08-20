
/****** Object:  View [dbo].[V_Sale_Total_Brokerage]    Script Date: 8/19/2017 9:39:13 PM ******/
DROP VIEW [dbo].[V_Sale_Total_Brokerage]
GO
/****** Object:  View [dbo].[V_Sale_Total_Brokerage]    Script Date: 8/19/2017 9:39:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE View [dbo].[V_Sale_Total_Brokerage] As
Select BD.SaleID, sum(BD.Brokerage) As Brokerage From BrokerageDetails BD
Group By BD.SaleID;

GO
