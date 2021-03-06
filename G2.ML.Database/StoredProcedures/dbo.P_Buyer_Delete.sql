
/****** Object:  StoredProcedure [dbo].[P_Buyer_Delete]    Script Date: 8/19/2017 9:39:13 PM ******/
DROP PROCEDURE [dbo].[P_Buyer_Delete]
GO
/****** Object:  StoredProcedure [dbo].[P_Buyer_Delete]    Script Date: 8/19/2017 9:39:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Jitendra Chaudhari
-- Create date: 26-03-2016
-- Description:	Update isDeleted column for given BuyerID
-- =============================================
CREATE PROCEDURE [dbo].[P_Buyer_Delete]( 
	@ClientID int,
	@BuyerID int
)
AS
BEGIN
	
	Update BuyerDetails Set IsDeleted = 1
	 Where BuyerID = @BuyerID
	   And UserID = @ClientID;
	
	Select @BuyerID As BuyerID;
END




GO
