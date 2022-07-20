<script type="text/javascript">
 var ttConversionOptions = ttConversionOptions || [];
ttConversionOptions.push({
    type: 'sales',
    campaignID: '%MerchantId%',
    productID: '%ProductId%',
    transactionID: '%OrderId%',
    transactionAmount: '%OrderTotal%',
    quantity: '%Quantity%',
    email: '%CustomerEmail%',
    descrMerchant: '',
    descrAffiliate: '',
    currency: '%Currency%'
});
</script>
<noscript>
 <img src="https://ts.tradetracker.net/?cid=%MerchantId%&amp;pid=%ProductId%&amp;tid=%OrderId%&amp;tam=%OrderTotal%&amp;data=&amp;qty=%Quantity%&amp;eml=%CustomerEmail%&amp;descrMerchant=&amp;descrAffiliate=&amp;event=sales&amp;currency=%Currency%" alt="" />
</noscript>
<script type="text/javascript">
 // No editing needed below this line.
 (function(ttConversionOptions) {
     var campaignID = 'campaignID' in ttConversionOptions ? ttConversionOptions.campaignID : ('length' in ttConversionOptions && ttConversionOptions.length ? ttConversionOptions[0].campaignID : null);
     var tt = document.createElement('script'); tt.type = 'text/javascript'; tt.async = true; tt.src = 'https://tm.tradetracker.net/conversion?s=' + encodeURIComponent(campaignID) + '&t=m';
     var s = document.getElementsByTagName('script'); s = s[s.length - 1]; s.parentNode.insertBefore(tt, s);
 })(ttConversionOptions);
</script>    
