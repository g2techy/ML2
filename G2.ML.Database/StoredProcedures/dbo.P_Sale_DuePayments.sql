
/****** Object:  StoredProcedure [dbo].[P_Sale_DuePayments]    Script Date: 8/19/2017 9:39:13 PM ******/
DROP PROCEDURE [dbo].[P_Sale_DuePayments]
GO
/****** Object:  StoredProcedure [dbo].[P_Sale_DuePayments]    Script Date: 8/19/2017 9:39:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Jitendra Chaudhari
-- Create date: 31-03-2017
-- Description:	Due payments list
-- =============================================
CREATE PROCEDURE [dbo].[P_Sale_DuePayments](
	@ClientID int,
	@StartIndex int = 1,
	@PageSize int = 10
)
AS
    Declare @DueDays int = 7;
BEGIN
Begin Try    
  Set NoCount On; 

  With Qry As (Select *, 
					  Row_Number() Over(Order By SaleID) As Row_Index,
					  Count(1) Over() As Row_Count 
				 From V_Sale_SaleList V
				Where V.SaleStatusID In (1, 2)
				  And V.DueDate <= DateAdd(DAY, @DueDays, GETDATE())
				  And V.UserID = @ClientID)
	Select * 
	 From Qry
	Where Row_Index between @StartIndex And (@StartIndex + @PageSize -1)
	 Order By DueDate;

End Try    
Begin Catch    
	DECLARE @ErrorMessage NVARCHAR(4000);    
    DECLARE @ErrorSeverity INT;    
    DECLARE @ErrorState INT;    
     
	SET @ErrorMessage = ERROR_MESSAGE();    
	SET @ErrorSeverity = ERROR_SEVERITY();    
	SET @ErrorState = ERROR_STATE();    
     
	Raiserror(@ErrorMessage,@ErrorSeverity,@ErrorState)    
End Catch

END
GO
