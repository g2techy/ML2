
/****** Object:  StoredProcedure [dbo].[P_Sale_Delete]    Script Date: 8/19/2017 9:39:13 PM ******/
DROP PROCEDURE [dbo].[P_Sale_Delete]
GO
/****** Object:  StoredProcedure [dbo].[P_Sale_Delete]    Script Date: 8/19/2017 9:39:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jitendra Chaudhari
-- Create date: 26-03-2016
-- Description:	Update isDeleted column for given SaleID
-- =============================================
CREATE PROCEDURE [dbo].[P_Sale_Delete]( 
	@ClientID int,
	@SaleID int
)
AS
BEGIN
	
	Update SalesDetails Set IsDeleted = 1
	 Where SaleID = @SaleID
	   And UserID = @ClientID;
	
	Select @SaleID As SaleID;
END



GO
