
/****** Object:  StoredProcedure [dbo].[P_Sale_DeleteBrokerage]    Script Date: 8/19/2017 9:39:13 PM ******/
DROP PROCEDURE [dbo].[P_Sale_DeleteBrokerage]
GO
/****** Object:  StoredProcedure [dbo].[P_Sale_DeleteBrokerage]    Script Date: 8/19/2017 9:39:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jitendra Chaudhari
-- Create date: 26-03-2016
-- Description:	Delete brokerage details for given SaleID
-- =============================================
CREATE PROCEDURE [dbo].[P_Sale_DeleteBrokerage]( 
	@BDID int,
	@SaleID int OutPut
)
AS
BEGIN
	
	Select @SaleID = SaleID
	  From BrokerageDetails BD
	 Where BD.BDID = @BDID;
	 
	Delete From BrokerageDetails
	 Where BDID = @BDID;
	
END

GO
