
/****** Object:  StoredProcedure [dbo].[P_Sale_GetBuyerList]    Script Date: 8/19/2017 9:39:13 PM ******/
DROP PROCEDURE [dbo].[P_Sale_GetBuyerList]
GO
/****** Object:  StoredProcedure [dbo].[P_Sale_GetBuyerList]    Script Date: 8/19/2017 9:39:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jitendra Chaudhari
-- Create date: 25-03-2016
-- Description:	Get list of Buyer/Saller/Broker
-- =============================================
CREATE PROCEDURE [dbo].[P_Sale_GetBuyerList](
	@ClientID int,
	@BuyerTypeID int
)
AS
BEGIN
	
	Select Distinct BD.BuyerID,
		   BD.BuyerName 
	  From BuyerDetails BD Inner Join BuyerTypeMapping BTM On BTM.BuyerID = BD.BuyerID
	Where BTM.Status = 1 And BTM.BuyerTypeID = @BuyerTypeID
	  And BD.UserID = @ClientID
	Order By BuyerName;

END

GO
