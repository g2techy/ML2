
/****** Object:  StoredProcedure [dbo].[P_Sale_GetDetails]    Script Date: 8/19/2017 9:39:13 PM ******/
DROP PROCEDURE [dbo].[P_Sale_GetDetails]
GO
/****** Object:  StoredProcedure [dbo].[P_Sale_GetDetails]    Script Date: 8/19/2017 9:39:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jitendra Chaudhari
-- Create date: 25-03-2016
-- Description:	Get sale details for given SaleID and ClientID
-- =============================================
CREATE PROCEDURE [dbo].[P_Sale_GetDetails]( 
	@ClientID int,
	@SaleID int	
)
AS
BEGIN
	
	Select * From SalesDetails SD Where SD.SaleID = @SaleID And SD.UserID = @ClientID;
	
END

GO
