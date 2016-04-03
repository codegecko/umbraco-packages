CodeGecko XML Sitemap

For this to work in your Umbraco site, you need to add a corresponding entry to your UrlRewriting.config,
or configure the template as an alternative template for your root site node.

An example URL rewrite to add to UrlRewriting.config would be as follows:

<add name="XmlSitemap"
	 virtualUrl="~/sitemap.xml"
	 destinationUrl="~/your-homepage?altTemplate=XmlSitemap" />