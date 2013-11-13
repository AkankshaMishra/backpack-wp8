using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Usebackpack.Common
{
    public class HTMLParser
    {
        public string ParseHTMLContent(string data)
        {
            if (data.Contains("<ul>"))
            {
                data=data.Replace("<ul>", "");
            }
            if (data.Contains("</ul>"))
            {
                data=data.Replace("</ul>", "");
            }
            if (data.Contains("<ol>"))
            {
                data=data.Replace("<ol>", "");
            }
            if (data.Contains("</ol>"))
            {
                data=data.Replace("</ol>", "");
            }

            if (data.Contains("<li>"))
            {
                data=data.Replace("<li>", "");
            }

            if (data.Contains("</li>"))
            {
                data=data.Replace("</li>", "\n");
            }

            if (data.Contains("&nbsp;"))
            {
                data=data.Replace("&nbsp;", " ");
            }

            if(data.Contains("&gt;"))
            {
                data = data.Replace("&gt;", ">");
            }
            if (data.Contains("&lt;"))
            {
                data = data.Replace("&lt;", "<");
            }
            if (data.Contains("&amp;"))
            {
                data = data.Replace("&amp;", "&");
            }
            if (data.Contains("<br/>"))
            {
                data = data.Replace("<br/>", "\n");
            }
            if (data.Contains("<br>"))
            {
                data = data.Replace("<br>", "\n");
            }
            if (data.Contains("<div>"))
            {
                data = data.Replace("<div>", "\n");
            }
            if (data.Contains("</div>"))
            {
                data = data.Replace("</div>", "\n");
            }
            if (data.Contains("</a>"))
            {
                data = data.Replace("</a>", "");
            }
            if (data.Contains("</span>"))
            {
                data = data.Replace("</span>", "");
            }
            data = Regex.Replace(data, @"<a.*>", "");
            data = Regex.Replace(data, @"<span.*>", " ");
            return data;
        }
    }
}
