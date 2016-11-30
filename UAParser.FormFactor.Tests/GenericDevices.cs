using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace UAParser.FormFactor.Tests
{
	[TestFixture]
    public class GenericDevices
    {
		[TestCase("Mozilla/5.0 (Android 4.4; Mobile; rv:41.0) Gecko/41.0 Firefox/41.0")]
		[TestCase("Mozilla/5.0 (SymbianOS/9.4; Series60/5.0 NokiaN97-1/10.0.012; Profile/MIDP-2.1 Configuration/CLDC-1.1; en-us) AppleWebKit/525 (KHTML, like Gecko) WicKed/7.1.12344")]
		[TestCase("Mozilla/5.0 (Android 4.4; Mobile; rv:41.0) Gecko/41.0 Firefox/41.0")]
		[TestCase("Mozilla/5.0 (Mobile; rv:26.0) Gecko/26.0 Firefox/26.0")]
		[TestCase("Mozilla/5.0 (iPhone; CPU iPhone OS 7_0 like Mac OS X) AppleWebKit/537.51.1 (KHTML, like Gecko) Mobile/11A465 Twitter for iPhone")]
		[TestCase("Mozilla/5.0 (compatible; MSIE 10.0; Windows Phone 8.0; Trident/6.0; IEMobile/10.0; ARM; Touch; NOKIA; Lumia 920)")]
		public void MobileDevices(string userAgent)
		{
			var parser = Parser.GetDefault();
			var result = parser.ParseDeviceWithFormFactor(userAgent);

			Assert.IsTrue(result.FormFactor == DeviceFormFactor.Mobile);
		}

		[TestCase("Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/40.0.2214.85 Safari/537.36")]
		[TestCase("Mozilla/5.0 (Windows NT 6.2; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/39.0.2171.95 Safari/537.36")]
		[TestCase("Mozilla/5.0 (Macintosh; Intel Mac OS X 10.10; rv:34.0) Gecko/20100101 Firefox/34.0")]
		[TestCase("Mozilla/5.0 (Windows NT 6.3; WOW64; rv:34.0) Gecko/20100101 Firefox/34.0")]
		[TestCase("Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/45.0.2454.85 Safari/537.36")]
		[TestCase("Mozilla/5.0 (X11; U; Linux x86_64; de; rv:1.9.2.8) Gecko/20100723 Ubuntu/10.04 (lucid) Firefox/3.6.8")]
		[TestCase("Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; FSL 7.0.5.01003)")]
		[TestCase("Mozilla/5.0 (Macintosh; Intel Mac OS X 10_11_6) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/54.0.2840.71 Safari/537.36")]
		public void DesktopDevices(string userAgent)
		{
			var parser = Parser.GetDefault();
			var result = parser.ParseDeviceWithFormFactor(userAgent);

			Assert.IsTrue(result.FormFactor == DeviceFormFactor.Desktop);
		}

		[TestCase("Mozilla/5.0 (iPad; CPU OS 10_0_2 like Mac OS X) AppleWebKit/602.1.50 (KHTML, like Gecko) Version/10.0 Mobile/14A456 Safari/602.1")]
		[TestCase("Mozilla/5.0 (Tablet; rv:26.0) Gecko/26.0 Firefox/26.0")]
		[TestCase("Mozilla/5.0 (iPad; CPU OS 7_0 like Mac OS X) AppleWebKit/537.51.1 (KHTML, like Gecko) CriOS/30.0.1599.12 Mobile/11A465 Safari/8536.25 (3B92C18B-D9DE-4CB7-A02A-22FD2AF17C8F)")]
		[TestCase("Mozilla/5.0 (Linux; Android 4.1.2; Transformer Build/JZO54K) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/29.0.1547.59 Safari/537.36")]
		public void TabletDevices(string userAgent)
		{
			var parser = Parser.GetDefault();
			var result = parser.ParseDeviceWithFormFactor(userAgent);

			Assert.IsTrue(result.FormFactor == DeviceFormFactor.Tablet);
		}

		[TestCase("Googlebot/2.1 (+http://www.google.com/bot.html)")]
		[TestCase("Mozilla/5.0+(compatible; UptimeRobot/2.0; http://www.uptimerobot.com/)")]
		[TestCase("Jamie Brown Spider 1.0 (http://jamiembrown.com/)")]
		[TestCase("Mozilla/5.0 (compatible; MixrankBot; crawler@mixrank.com)")]
		[TestCase("Mozilla/5.0 (compatible; Cliqzbot/1.0 +http://cliqz.com/company/cliqzbot)")]
		[TestCase("Xenu's Link Sleuth 1.1c")]
		[TestCase("Mozilla/5.0 (compatible; Baiduspider/2.0; +http://www.baidu.com/search/spider.html)")]
		[TestCase("Mozilla/5.0 (iPhone; CPU iPhone OS 7_0 like Mac OS X) AppleWebKit/537.51.1 (KHTML, like Gecko) Version/7.0 Mobile/11A465 Safari/9537.53 (compatible; bingbot/2.0; http://www.bing.com/bingbot.htm)")]
		[TestCase("DuckDuckBot/1.0; (+http://duckduckgo.com/duckduckbot.html)")]
		public void SpiderDevices(string userAgent)
		{
			var parser = Parser.GetDefault();
			var result = parser.ParseDeviceWithFormFactor(userAgent);

			Assert.IsTrue(result.FormFactor == DeviceFormFactor.Spider);
		}
	}
}
