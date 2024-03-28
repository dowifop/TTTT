using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Fizzler.Systems.HtmlAgilityPack;
namespace QLTT.Template
{
    public class SuKienCrawler : Crawler
    {
        public override Blog Request(string url)
        {
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(url);

            HtmlNode title_tag = doc.DocumentNode.QuerySelector(".title-detail");
            HtmlNode imageNode = doc.DocumentNode.SelectSingleNode("//div[contains(@class, 'fig-picture')]//img");
            HtmlNode content_tag = doc.DocumentNode.QuerySelector(".Normal");

            string title = title_tag != null ? HtmlEntity.DeEntitize(title_tag.InnerText.Trim()) : string.Empty;

            string imageUrl = imageNode != null
                ? HtmlEntity.DeEntitize(imageNode.Attributes["data-src"]?.Value ?? imageNode.Attributes["src"]?.Value)
                : string.Empty;
            string content = content_tag != null ? HtmlEntity.DeEntitize(content_tag.InnerText.Trim()) : string.Empty;

            return new Blog(title, imageUrl, content);
        }



    }
}