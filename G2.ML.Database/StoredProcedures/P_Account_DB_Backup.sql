
/****** Object:  StoredProcedure [dbo].[P_Account_DB_Backup]    Script Date: 8/19/2017 9:39:13 PM ******/
DROP PROCEDURE [dbo].[P_Account_DB_Backup]
GO
/****** Object:  StoredProcedure [dbo].[P_Account_DB_Backup]    Script Date: 8/19/2017 9:39:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jitendra Chaudhari
-- Create date: 20-08-2022
-- Description:	Take databae backup
-- =============================================
CREATE PROCEDURE [dbo].[P_Account_DB_Backup](
	@DatabaseName varchar(50),
	@BackUpFilePath varchar(500)	
)
AS
BEGIN
	BACKUP DATABASE @DatabaseName TO DISK = @BackUpFilePath	
END

GO
