
/****** Object:  StoredProcedure [dbo].[P_Sale_GetSalesList]    Script Date: 8/19/2017 9:39:13 PM ******/
DROP PROCEDURE [dbo].[P_Sale_GetSalesList]
GO
/****** Object:  StoredProcedure [dbo].[P_Sale_GetSalesList]    Script Date: 8/19/2017 9:39:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jitendra Chaudhari
-- Create date: 01-04-2016
-- Description:	Search sale list
-- =============================================
CREATE PROCEDURE [dbo].[P_Sale_GetSalesList](
	@ClientID int,
	@StartDate varchar(100) = '',
	@EndDate varchar(100) = '',
	@SallerID int = 0,
	@BuyerID int = 0,
	@StartIndex int = 1,
	@PageSize int = 20,
	@RefNo varchar(100) = ''
)
AS
  Declare @v_Query varchar(4000);
  Declare @v_WhereClause varchar(4000);
  
BEGIN
	
  Set @v_WhereClause  = ' And 1 = 1';
  
  Set @v_Query = 'Select *, 
                        Row_Number() Over(Order By SaleID) As Row_Index,
					    Count(1) Over() As Row_Count 
				   From V_Sale_SaleList V
				  Where V.UserID = ' + Cast(@ClientID as Varchar);
				 
  If @StartDate != '' And @EndDate != ''
  BEGIN
	Set @v_WhereClause = ' And V.SaleDate Between ''' + @StartDate + ''' And ''' + @EndDate + '''';
  END
  Else If @StartDate != ''
  BEGIN
	Set @v_WhereClause = ' And V.SaleDate >= ''' + @StartDate + '''';
  END
  Else If @EndDate != ''
  BEGIN
	Set @v_WhereClause = ' And V.SaleDate <= ''' + @EndDate + '''';
  END

  If @SallerID != 0 
  Begin
	Set @v_WhereClause = @v_WhereClause + ' And V.SallerID = ' + Cast(@SallerID As varchar(10));
  End	
  
  If @BuyerID != 0 
  Begin
	Set @v_WhereClause = @v_WhereClause + ' And V.BuyerID = ' + Cast(@BuyerID As varchar(10));
  End
  
  If @RefNo != '' 
  Begin
	Set @v_WhereClause = @v_WhereClause + ' And V.RefNo = ''' + @RefNo + '''';
  End
  
  Set @v_Query = ' With Qry As (' + @v_Query + @v_WhereClause + ')
				   Select * From Qry
				    Where Row_Index between ' + Cast(@StartIndex As varchar) + ' And ' 
						+ Cast((@StartIndex + @PageSize -1) as varchar);
  
   --Print ' Where: ' + @v_WhereClause
   Print ' Query: ' + @v_Query
    
   Execute(@v_Query);
      
END

GO
