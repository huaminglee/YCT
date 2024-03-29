﻿using System;
using System.Collections.Generic;
using System.Text;
using EDoc2.Data.DataEntities;

namespace EDoc2.ManagementService
{
    public class FolderXinxiRepairer : ServiceDataRepairer
    {
        private const string EXCUTE_SQL = @"--重新计算文件夹个数及容量
--DECLARE @FLD_NAME NVARCHAR(100)
DECLARE @FLD_ID INT
DECLARE @FLD_FILE_COUNT INT

DECLARE @FLD_FILE_COUNT_SUM INT
DECLARE @FLD_FILE_COUNT_CHD INT

DECLARE CUR CURSOR
 FOR SELECT FOLDER_ID/*, FOLDER_NAME*/ FROM DMS_FOLDER ORDER BY LEN(FOLDER_PATH) DESC, FOLDER_NAME DESC

OPEN CUR
FETCH NEXT FROM CUR INTO @FLD_ID--, @FLD_NAME

WHILE @@FETCH_STATUS = 0
BEGIN
	SELECT @FLD_FILE_COUNT_SUM = 0
	SELECT @FLD_FILE_COUNT_CHD = 0
	
	SELECT @FLD_FILE_COUNT_SUM = COUNT(FILE_ID) FROM DMS_FILE WHERE FOLDER_ID = @FLD_ID
	SELECT @FLD_FILE_COUNT_CHD = SUM(FOLDER_CHILDFILESCOUNT) FROM DMS_FOLDER WHERE FOLDER_PARENTFOLDERID = @FLD_ID AND FOLDER_PARENTFOLDERID <> FOLDER_ID

	SELECT @FLD_FILE_COUNT = ISNULL(@FLD_FILE_COUNT_SUM, 0) + ISNULL(@FLD_FILE_COUNT_CHD, 0)

	UPDATE DMS_FOLDER SET FOLDER_CHILDFILESCOUNT = @FLD_FILE_COUNT WHERE FOLDER_ID = @FLD_ID

	--PRINT CAST(@FLD_ID AS NVARCHAR(20)) +','+ @FLD_NAME +','+ CAST(@FLD_FOLD_COUNT AS NVARCHAR(20)) +','+ CAST(@FLD_FILE_COUNT AS NVARCHAR(20)) +','+ CAST(@FLD_SIZE AS NVARCHAR(20))
	--PRINT @FLD_SIZE

	FETCH NEXT FROM CUR INTO @FLD_ID--, @FLD_NAME
END

CLOSE CUR
DEALLOCATE CUR
";

        public FolderXinxiRepairer() : 
            base()
        {
            
        }

        public override void Repair()
        {
            LogManagement.Log.Info("进入FolderXinxiRepairer");
            this.ExcuteSql(EXCUTE_SQL);
            LogManagement.Log.Info("退出FolderXinxiRepairer");
        }
    }
}
