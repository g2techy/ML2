
/****** Object:  View [dbo].[V_Buyer_BuyerList]    Script Date: 8/19/2017 9:39:13 PM ******/
DROP VIEW [dbo].[V_Buyer_BuyerList]
GO
/****** Object:  View [dbo].[V_Buyer_BuyerList]    Script Date: 8/19/2017 9:39:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE View [dbo].[V_Buyer_BuyerList] AS 
With BTM (BuyerID, BuyerTypeNames) AS
(
 SELECT O.BuyerID
       ,STUFF((SELECT '/ ' + CAST(BTD.BuyerTypeName AS VARCHAR(20)) [text()]
         From BuyerTypeMapping BTM Inner Join BuyerTypeDetails BTD On BTD.BuyerTypeID = BTM.BuyerTypeID 
         WHERE BTM.BuyerID = O.BuyerID
         FOR XML PATH(''), TYPE)
        .value('.','NVARCHAR(MAX)'),1,2,' ') List_Output
FROM BuyerTypeMapping O
GROUP BY O.BuyerID
)
Select BD.*, BTM.BuyerTypeNames 
  From BuyerDetails BD Left Join BTM On BTM.BuyerID = BD.BuyerID

GO
