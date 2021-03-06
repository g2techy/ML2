
/****** Object:  StoredProcedure [dbo].[P_Sale_DeletePayment]    Script Date: 8/19/2017 9:39:13 PM ******/
DROP PROCEDURE [dbo].[P_Sale_DeletePayment]
GO
/****** Object:  StoredProcedure [dbo].[P_Sale_DeletePayment]    Script Date: 8/19/2017 9:39:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Jitendra Chaudhari
-- Create date: 26-03-2016
-- Description:	Delete payment details for given SaleID
-- =============================================
CREATE PROCEDURE [dbo].[P_Sale_DeletePayment]( 
	@PayID int,
	@SaleID int OutPut
)
AS
BEGIN
	
	Select @SaleID = SaleID
	  From PaymentDetails PD
	 Where PD.PayID = @PayID;
	 
	Delete From PaymentDetails
	 Where PayID = @PayID;
	 
	Exec P_Sale_UpdateStatus @SaleID = @SaleID;
	
END


GO
