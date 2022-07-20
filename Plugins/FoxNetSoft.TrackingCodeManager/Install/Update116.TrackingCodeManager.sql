IF EXISTS (SELECT 1 FROM [FNS_TrackingCode] WHERE [TypeTag]=4)
BEGIN
	DELETE FROM [FNS_TrackingCode] WHERE [TypeTag]=4
END
GO

IF EXISTS (SELECT 1 FROM [FNS_TrackingCode] WHERE [TypeTag]=3)
BEGIN
	UPDATE F
	SET TrackingScriptAfterStartBody=F2.TrackingScriptAfterStartBody
	FROM [FNS_TrackingCode] F, [FNS_TrackingCode] F2
	WHERE F.[TypeTag]=2 and F2.[TypeTag]=3
		and F.StoreId=F2.StoreId

	DELETE FROM [FNS_TrackingCode] WHERE [TypeTag]=3
END
GO
IF EXISTS (SELECT 1 FROM [FNS_TrackingCode] WHERE [TrackingScriptBeforeEndBody] like '%TwengaHTMLCode%')
BEGIN
	UPDATE [FNS_TrackingCode]
	SET TrackingScriptBeforeEndBody=REPLACE(TrackingScriptBeforeEndBody,'TwengaHTMLCode','TEMPLATE_DYNAMIC_CODE')
	WHERE [TrackingScriptBeforeEndBody] like '%TwengaHTMLCode%'
END
GO
IF EXISTS (SELECT 1 FROM [FNS_TrackingCode] WHERE [TrackingScriptBeforeEndHead] like '%CriteoOneTag%')
BEGIN
	UPDATE [FNS_TrackingCode]
	SET TrackingScriptBeforeEndHead=REPLACE(TrackingScriptBeforeEndHead,'CriteoOneTag','TEMPLATE_DYNAMIC_CODE')
	WHERE [TrackingScriptBeforeEndHead] like '%CriteoOneTag%'
END
GO
IF EXISTS (SELECT 1 FROM [FNS_TrackingCode] WHERE [TypeTag]=2 and [TrackingScriptBeforeEndHead] not like '%TEMPLATE_DYNAMIC_CODE%')
BEGIN
	UPDATE [FNS_TrackingCode]
	SET TrackingScriptBeforeEndHead='%TEMPLATE_DYNAMIC_CODE%'+CHAR(13)+TrackingScriptBeforeEndHead
	WHERE [TypeTag]=2 and [TrackingScriptBeforeEndHead] not like '%TEMPLATE_DYNAMIC_CODE%'
END
GO
IF EXISTS (SELECT 1 FROM [FNS_TrackingCode] WHERE [TrackingScriptAfterStartBody] like '%GoogleDynamicRemarketing%')
BEGIN
	UPDATE [FNS_TrackingCode]
	SET TrackingScriptAfterStartBody=REPLACE(TrackingScriptAfterStartBody,'GoogleDynamicRemarketing','TEMPLATE_DYNAMIC_CODE')
	WHERE [TrackingScriptAfterStartBody] like '%GoogleDynamicRemarketing%'
END
GO
IF EXISTS (SELECT 1 FROM [FNS_TrackingCode] WHERE [TrackingScriptAfterStartBody] like '%Facebook.Pixel%')
BEGIN
	UPDATE [FNS_TrackingCode]
	SET TrackingScriptAfterStartBody=REPLACE(TrackingScriptAfterStartBody,'Facebook.Pixel','TEMPLATE_DYNAMIC_CODE')
	WHERE [TrackingScriptAfterStartBody] like '%Facebook.Pixel%'
END
GO
IF EXISTS (SELECT 1 FROM [FNS_TrackingCode] WHERE [TrackingScriptBeforeEndHead] like '%Facebook.Pixel%')
BEGIN
	UPDATE [FNS_TrackingCode]
	SET TrackingScriptBeforeEndHead=REPLACE(TrackingScriptBeforeEndHead,'Facebook.Pixel','TEMPLATE_DYNAMIC_CODE')
	WHERE [TrackingScriptBeforeEndHead] like '%Facebook.Pixel%'
END
GO
IF EXISTS (SELECT 1 FROM [FNS_TrackingCode] WHERE [TrackingScriptBeforeEndBody] like '%Facebook.Pixel%')
BEGIN
	UPDATE [FNS_TrackingCode]
	SET TrackingScriptBeforeEndBody=REPLACE(TrackingScriptBeforeEndBody,'Facebook.Pixel','TEMPLATE_DYNAMIC_CODE')
	WHERE [TrackingScriptBeforeEndBody] like '%Facebook.Pixel%'
END
IF EXISTS (SELECT 1 FROM [FNS_TrackingCode] WHERE [TrackingScriptAfterStartBody] like '%Google.Analytics.OrderCompleted%')
BEGIN
	UPDATE [FNS_TrackingCode]
	SET TrackingScriptAfterStartBody=REPLACE(TrackingScriptAfterStartBody,'Google.Analytics.OrderCompleted','TEMPLATE_DYNAMIC_CODE')
	WHERE [TrackingScriptAfterStartBody] like '%Google.Analytics.OrderCompleted%'
END
GO
IF EXISTS (SELECT 1 FROM [FNS_TrackingCode] WHERE [TrackingScriptBeforeEndHead] like '%Google.Analytics.OrderCompleted%')
BEGIN
	UPDATE [FNS_TrackingCode]
	SET TrackingScriptBeforeEndHead=REPLACE(TrackingScriptBeforeEndHead,'Google.Analytics.OrderCompleted','TEMPLATE_DYNAMIC_CODE')
	WHERE [TrackingScriptBeforeEndHead] like '%Google.Analytics.OrderCompleted%'
END
GO
IF EXISTS (SELECT 1 FROM [FNS_TrackingCode] WHERE [TrackingScriptBeforeEndBody] like '%Google.Analytics.OrderCompleted%')
BEGIN
	UPDATE [FNS_TrackingCode]
	SET TrackingScriptBeforeEndBody=REPLACE(TrackingScriptBeforeEndBody,'Google.Analytics.OrderCompleted','TEMPLATE_DYNAMIC_CODE')
	WHERE [TrackingScriptBeforeEndBody] like '%Google.Analytics.OrderCompleted%'
END
IF EXISTS (SELECT 1 FROM [FNS_TrackingCode] WHERE [TrackingScriptAfterStartBody] like '%Google.UniversalAnalytics.OrderCompleted%')
BEGIN
	UPDATE [FNS_TrackingCode]
	SET TrackingScriptAfterStartBody=REPLACE(TrackingScriptAfterStartBody,'Google.UniversalAnalytics.OrderCompleted','TEMPLATE_DYNAMIC_CODE')
	WHERE [TrackingScriptAfterStartBody] like '%Google.UniversalAnalytics.OrderCompleted%'
END
GO
IF EXISTS (SELECT 1 FROM [FNS_TrackingCode] WHERE [TrackingScriptBeforeEndHead] like '%Google.UniversalAnalytics.OrderCompleted%')
BEGIN
	UPDATE [FNS_TrackingCode]
	SET TrackingScriptBeforeEndHead=REPLACE(TrackingScriptBeforeEndHead,'Google.UniversalAnalytics.OrderCompleted','TEMPLATE_DYNAMIC_CODE')
	WHERE [TrackingScriptBeforeEndHead] like '%Google.UniversalAnalytics.OrderCompleted%'
END
GO
IF EXISTS (SELECT 1 FROM [FNS_TrackingCode] WHERE [TrackingScriptBeforeEndBody] like '%Google.UniversalAnalytics.OrderCompleted%')
BEGIN
	UPDATE [FNS_TrackingCode]
	SET TrackingScriptBeforeEndBody=REPLACE(TrackingScriptBeforeEndBody,'Google.UniversalAnalytics.OrderCompleted','TEMPLATE_DYNAMIC_CODE')
	WHERE [TrackingScriptBeforeEndBody] like '%Google.UniversalAnalytics.OrderCompleted%'
END
GO
IF EXISTS (SELECT 1 FROM [FNS_TrackingCode] WHERE [TrackingScriptBeforeEndBody] like '%Facebook.Pixel%')
BEGIN
	UPDATE [FNS_TrackingCode]
	SET TrackingScriptBeforeEndBody=REPLACE(TrackingScriptBeforeEndBody,'Facebook.Pixel','TEMPLATE_DYNAMIC_CODE')
	WHERE [TrackingScriptBeforeEndBody] like '%Facebook.Pixel%'
END
GO
IF EXISTS (SELECT 1 FROM [FNS_TrackingCode] WHERE [TypeTag] in (9) 
		and ltrim(rtrim(isnull([TrackingScriptAfterStartBody],'')))!='')
BEGIN
	--Google Dynamic Remarketing
	update [FNS_TrackingCode] 
	set [TrackingScriptBeforeEndHead] = [TrackingScriptAfterStartBody], [TrackingScriptAfterStartBody]=NULL
	where [TypeTag] in (9) 
		and ltrim(rtrim(isnull([TrackingScriptAfterStartBody],'')))!=''
END
GO
IF EXISTS (SELECT 1 FROM [FNS_TrackingCode] WHERE [TrackingScriptBeforeEndHead] like '%-- >%')
BEGIN
	--Google Dynamic Remarketing
	update [FNS_TrackingCode] 
	set [TrackingScriptBeforeEndHead] = REPLACE([TrackingScriptBeforeEndHead],'-- >','-->')
	WHERE [TrackingScriptBeforeEndHead] like '%-- >%'
END
GO
IF EXISTS (SELECT 1 FROM [FNS_TrackingCode] WHERE [TrackingScriptAfterStartBody] like '%-- >%')
BEGIN
	update [FNS_TrackingCode] 
	set [TrackingScriptAfterStartBody] = REPLACE([TrackingScriptAfterStartBody],'-- >','-->')
	WHERE [TrackingScriptAfterStartBody] like '%-- >%'
END
GO
if exists(select top 1 * from [FNS_TrackingCode] 
	WHERE TypeTag = 9 and TrackingScriptBeforeEndHead not like '%data-fixedscript="1"%')
begin
	update [FNS_TrackingCode] 
	set TrackingScriptBeforeEndHead = REPLACE(TrackingScriptBeforeEndHead,
		'%TEMPLATE_DYNAMIC_CODE%',
		'<script data-fixedscript="1">'+CHAR(13)+CHAR(10)+'%TEMPLATE_DYNAMIC_CODE%'+CHAR(13)+CHAR(10)+'</script>')
	WHERE TypeTag = 9 and TrackingScriptBeforeEndHead not like '%data-fixedscript="1"%'

	update [FNS_TrackingCode] 
	set TrackingScriptBeforeEndHead = REPLACE(TrackingScriptBeforeEndHead,
		'<script>',
		'<script data-fixedscript="1">')
	WHERE TypeTag = 9 and TrackingScriptBeforeEndHead like '%<script>%'
end
GO
if exists(select top 1 * from [FNS_TrackingCode] 
	WHERE TypeTag = 8 and TrackingScriptBeforeEndHead like '[%]TEMPLATE_DYNAMIC_CODE%')
begin
	update [FNS_TrackingCode] 
	set TrackingScriptBeforeEndHead = REPLACE(TrackingScriptBeforeEndHead,
		'%TEMPLATE_DYNAMIC_CODE%',
		'<script>'+CHAR(13)+CHAR(10)+'%TEMPLATE_DYNAMIC_CODE%'+CHAR(13)+CHAR(10)+'</script>')
	WHERE TypeTag = 8 and TrackingScriptBeforeEndHead like '[%]TEMPLATE_DYNAMIC_CODE%'
end

if exists(select top 1 * from [FNS_TrackingCode] 
	WHERE TypeTag = 8 and TrackingScriptAfterStartBody like '[%]TEMPLATE_DYNAMIC_CODE%')
begin
	update [FNS_TrackingCode] 
	set TrackingScriptAfterStartBody = REPLACE(TrackingScriptAfterStartBody,
		'%TEMPLATE_DYNAMIC_CODE%',
		'<script>'+CHAR(13)+CHAR(10)+'%TEMPLATE_DYNAMIC_CODE%'+CHAR(13)+CHAR(10)+'</script>')
	WHERE TypeTag = 8 and TrackingScriptAfterStartBody like '[%]TEMPLATE_DYNAMIC_CODE%'
end
GO

if exists(select top 1 * from [FNS_TrackingCode] 
	WHERE TypeTag = 2 and TrackingScriptBeforeEndHead like '[%]TEMPLATE_DYNAMIC_CODE%')
begin
	update [FNS_TrackingCode] 
	set TrackingScriptBeforeEndHead = REPLACE(TrackingScriptBeforeEndHead,
		'%TEMPLATE_DYNAMIC_CODE%',
		'<script>'+CHAR(13)+CHAR(10)
			+'window.dataLayer = window.dataLayer || [];'+CHAR(13)+CHAR(10)+
			'%TEMPLATE_DYNAMIC_CODE%'+CHAR(13)+CHAR(10)+
		'</script>')
	WHERE TypeTag = 2 and TrackingScriptBeforeEndHead like '[%]TEMPLATE_DYNAMIC_CODE%'
end
GO
IF EXISTS (SELECT 1 FROM [FNS_TrackingCode] WHERE [TypeTag]=18)
BEGIN
	DELETE FROM [FNS_TrackingCode] WHERE [TypeTag]=18
END
GO