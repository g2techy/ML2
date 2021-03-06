
/****** Object:  StoredProcedure [dbo].[P_Sale_GetPaymentList]    Script Date: 8/19/2017 9:39:13 PM ******/
DROP PROCEDURE [dbo].[P_Sale_GetPaymentList]
GO
/****** Object:  StoredProcedure [dbo].[P_Sale_GetPaymentList]    Script Date: 8/19/2017 9:39:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jitendra Chaudhari
-- Create date: 26-03-2016
-- Description:	Get list of payment for given SaleID
-- =============================================
CREATE PROCEDURE [dbo].[P_Sale_GetPaymentList]( 
	@ClientID int,
	@SaleID int	
)
AS
		
BEGIN
	SET NOCOUNT ON;

	Select PD.PayID, PD.PayDate, PD.PayAmount, PD.PayCourierFrom, PD.PayCourierTo 
	  From PaymentDetails PD
	 Where PD.SaleID = @SaleID;	
    
END


GO
