
/****** Object:  StoredProcedure [dbo].[P_Buyer_BuyerTypeList]    Script Date: 8/19/2017 9:39:13 PM ******/
DROP PROCEDURE [dbo].[P_Buyer_BuyerTypeList]
GO
/****** Object:  StoredProcedure [dbo].[P_Buyer_BuyerTypeList]    Script Date: 8/19/2017 9:39:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Jitendra Chaudhari
-- Create date: 25-03-2016
-- Description:	Get Buyer Type List
-- =============================================
CREATE PROCEDURE [dbo].[P_Buyer_BuyerTypeList]
AS
BEGIN
	
	Select * From BuyerTypeDetails;
	
END


GO
