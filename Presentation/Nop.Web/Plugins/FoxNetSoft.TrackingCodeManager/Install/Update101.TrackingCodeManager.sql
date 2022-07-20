IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id=object_id('[FNS_TrackingCode]') and NAME='DisplayOrder')
BEGIN
	ALTER TABLE [FNS_TrackingCode]
	ADD [DisplayOrder] int NOT NULL CONSTRAINT DF_FNS_TrackingCode_DisplayOrder DEFAULT 0
END
GO
IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id=object_id('[FNS_TrackingCode]') and NAME='ProductKey')
BEGIN
	ALTER TABLE [FNS_TrackingCode]
	ADD [ProductKey] int NOT NULL CONSTRAINT DF_FNS_TrackingCode_ProductKey DEFAULT 0
END
GO
IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id=object_id('[FNS_TrackingCode]') and NAME='TrackingScriptBeforeEndHead')
BEGIN
	ALTER TABLE [FNS_TrackingCode]
	ADD [TrackingScriptBeforeEndHead] nvarchar(max) NULL

	ALTER TABLE [FNS_TrackingCode]
	ADD [TrackingScriptAfterStartBody] nvarchar(max) NULL

	ALTER TABLE [FNS_TrackingCode]
	ADD [TrackingScriptBeforeEndBody] nvarchar(max) NULL
END
GO
IF EXISTS (SELECT 1 FROM sys.columns WHERE object_id=object_id('[FNS_TrackingCode]') and NAME='ScriptPosition')
BEGIN
	update [FNS_TrackingCode]
	set [TrackingScriptBeforeEndHead]=[TrackingScript]
	where ScriptPosition=0

	update [FNS_TrackingCode]
	set [TrackingScriptAfterStartBody]=[TrackingScript]
	where ScriptPosition=1

	update [FNS_TrackingCode]
	set [TrackingScriptBeforeEndBody]=[TrackingScript]
	where ScriptPosition=2

	ALTER TABLE [FNS_TrackingCode] DROP COLUMN [ScriptPosition]
	ALTER TABLE [FNS_TrackingCode] DROP COLUMN [TrackingScript]
END
GO
IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id=object_id('[FNS_TrackingCode]') and NAME='TypeTag')
BEGIN
	ALTER TABLE [FNS_TrackingCode]
	ADD [TypeTag] int NOT NULL CONSTRAINT DF_FNS_TrackingCode_TypeTag DEFAULT 0

		update FNS_TrackingCode 
	set TypeTag=1 
	where TrackingScriptBeforeEndHead like '%Facebook.Pixel%'
			or TrackingScriptAfterStartBody like '%Facebook.Pixel%'
			or TrackingScriptBeforeEndBody like '%Facebook.Pixel%'

	update FNS_TrackingCode 
	set TypeTag=5 
	where TrackingScriptBeforeEndHead like '%Google.Analytics.OrderCompleted%'
			or TrackingScriptAfterStartBody like '%Google.Analytics.OrderCompleted%'
			or TrackingScriptBeforeEndBody like '%Google.Analytics.OrderCompleted%'

	update FNS_TrackingCode set TypeTag=6 
		where TrackingScriptBeforeEndHead like '%Google.UniversalAnalytics.OrderCompleted%'
			or TrackingScriptAfterStartBody like '%Google.UniversalAnalytics.OrderCompleted%'
			or TrackingScriptBeforeEndBody like '%Google.UniversalAnalytics.OrderCompleted%'
END
GO
