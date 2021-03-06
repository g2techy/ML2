
/****** Object:  StoredProcedure [dbo].[P_Buyer_GetDetails]    Script Date: 8/19/2017 9:39:13 PM ******/
DROP PROCEDURE [dbo].[P_Buyer_GetDetails]
GO
/****** Object:  StoredProcedure [dbo].[P_Buyer_GetDetails]    Script Date: 8/19/2017 9:39:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Jitendra Chaudhari
-- Create date: 25-03-2016
-- Description:	Get buyer details for given BuyerID and ClientID
-- =============================================
CREATE PROCEDURE [dbo].[P_Buyer_GetDetails]( 
	@ClientID int,
	@BuyerID int	
)
AS
	Declare @BuyerTypeID varchar(100);
BEGIN
	
	SELECT @BuyerTypeID = COALESCE(@BuyerTypeID + ',','') + Cast(BTM.BuyerTypeID as varchar)
     FROM BuyerTypeMapping BTM Where BuyerID = @BuyerID;

	Select BD.*, @BuyerTypeID AS BuyerTypes From BuyerDetails BD Where BuyerID = @BuyerID And UserID = @ClientID;
	
END


GO
