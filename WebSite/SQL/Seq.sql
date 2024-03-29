USE [SCM]
GO
/****** 对象:  StoredProcedure [dbo].[SP_GetSeq]    脚本日期: 12/19/2012 17:05:14 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetSeq]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_GetSeq]

GO
/****** 对象:  StoredProcedure [dbo].[SP_GetSeq]    脚本日期: 12/19/2012 17:05:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE  PROCEDURE [dbo].[SP_GetSeq](
               @P_Bll_Type       NVARCHAR(10),
               @P_Out_New_Bll_No NVARCHAR(20)  OUTPUT)
AS
  DECLARE  @Seq INT
  
  DECLARE  @Formate NVARCHAR(20)
  
  DECLARE  @Cur_Formate NVARCHAR(20)
  
  DECLARE  @AttacheLen INT
  
  BEGIN
    SELECT 
      @Seq = seq,
      @AttacheLen = Attache_len,
      @Cur_Formate = Formate_Value,
      @Formate = (CASE 
                    WHEN bll_Formate = 'YYYYMMDD' THEN CAST(YEAR(Getdate()) AS NVARCHAR) + CAST(RIGHT(100 + MONTH(Getdate()),2) AS NVARCHAR) + CAST(RIGHT(100 + DAY(Getdate()),2) AS NVARCHAR)
                    WHEN bll_Formate = 'YYMM' THEN CAST(RIGHT(YEAR(Getdate()),2) AS NVARCHAR) + CAST(RIGHT(100 + MONTH(Getdate()),2) AS NVARCHAR)
                    WHEN bll_Formate = 'YYMMDD' THEN CAST(RIGHT(YEAR(Getdate()),2) AS NVARCHAR) + CAST(RIGHT(100 + MONTH(Getdate()),2) AS NVARCHAR) + CAST(RIGHT(100 + DAY(Getdate()),2) AS NVARCHAR)
                    ELSE ''
                  END)
    FROM   sys_seq
    WHERE  bll_Type = @P_Bll_Type
    
    IF @Formate <> ''
      BEGIN
        IF @Formate <> @Cur_Formate
          BEGIN
            UPDATE sys_seq
            SET    seq = 1
            WHERE  bll_Type = @P_Bll_Type
            SET @Seq = 1
          END
        
        SET @P_Out_New_Bll_No = (@P_Bll_Type + Rtrim(Ltrim(@Formate)) + Ltrim(Replicate('0',@AttacheLen - Len(@Seq)) + CAST(@Seq AS VARCHAR)))
        
        UPDATE sys_seq
        SET    seq = seq + 1,
               Formate_Value = @Formate
        WHERE  bll_Type = @P_Bll_Type
      END
    ELSE
      BEGIN
        SET @P_Out_New_Bll_No = ''
      END
  SELECT @P_Out_New_Bll_No
  END

--select datename(year,getdate()),datename(month,getdate()),datename(day,getdate()),Right(100+Month(GetDate()),2),Right(100+Day(GetDate()),2)



