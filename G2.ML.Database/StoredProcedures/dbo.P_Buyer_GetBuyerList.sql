
/****** Object:  StoredProcedure [dbo].[P_Buyer_GetBuyerList]    Script Date: 8/19/2017 9:39:13 PM ******/
DROP PROCEDURE [dbo].[P_Buyer_GetBuyerList]
GO
/****** Object:  StoredProcedure [dbo].[P_Buyer_GetBuyerList]    Script Date: 8/19/2017 9:39:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jitendra Chaudhari
-- Create date: 09-04-2016
-- Description:	Search buyer list
-- =============================================
CREATE PROCEDURE [dbo].[P_Buyer_GetBuyerList](
	@ClientID int,
	@BuyerCode varchar(100) = '',
	@FirstName varchar(100) = '',
	@LastName varchar(100) = '',
	@StartIndex int = 1,
	@PageSize int = 20
)
AS
  Declare @v_Query varchar(4000);
  Declare @v_WhereClause varchar(4000);
  
BEGIN
	
  Set @v_WhereClause  = ' And 1 = 1';
  
  Set @v_Query = 'Select *, 
                        Row_Number() Over(Order By BuyerID) As Row_Index,
					    Count(1) Over() As Row_Count 
				   From V_Buyer_BuyerList V
				  Where V.IsDeleted = 0 And V.UserID = ' + Cast(@ClientID as Varchar);
				 
  If @BuyerCode != ''
  BEGIN
	Set @v_WhereClause = ' And BuyerCode like ''' + @BuyerCode + '%''';
  END
  
  If @FirstName != ''
  BEGIN
	Set @v_WhereClause = ' And FirstName like ''' + @FirstName + '%''';
  END
  
  If @LastName != ''
  BEGIN
	Set @v_WhereClause = ' And LastName like ''' + @LastName + '%''';
  END
    
  Set @v_Query = ' With Qry As (' + @v_Query + @v_WhereClause + ')
				   Select * From Qry
				    Where Row_Index between ' + Cast(@StartIndex As varchar) + ' And ' 
						+ Cast((@StartIndex + @PageSize -1) as varchar);
  
   --Print ' Where: ' + @v_WhereClause
   Print ' Query: ' + @v_Query
    
   Execute(@v_Query);
      
END


GO
