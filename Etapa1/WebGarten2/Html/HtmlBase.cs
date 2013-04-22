using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebGarten2.Html
{
    public class HtmlBase
    {
        public static IWritable Text(String s) { return new HtmlText(s); }
        public static IWritable H1(params IWritable[] c) { return new HtmlElem("h1", c); }
        public static IWritable H2(params IWritable[] c) { return new HtmlElem("h2", c); }
        public static IWritable H3(params IWritable[] c) { return new HtmlElem("h3", c); }
        public static IWritable Form(String method, String url, params IWritable[] c)
        {
            return new HtmlElem("form", c)
                .WithAttr("method", method)
                .WithAttr("action", url);
        }
        public static IWritable Label(String to, String text)
        {
            return new HtmlElem("label", new HtmlText(text))
                .WithAttr("for", to);
        }
        public static IWritable InputText(String name)
        {
            return new HtmlElem("input")
                .WithAttr("type", "text")
                .WithAttr("name", name);
        }

        // ******* ADDED *******************************
        public static IWritable InputText(String name, String value)
        {
            return new HtmlElem("input")
                .WithAttr("type", "text")
                .WithAttr("name", name)
                .WithAttr("value", value);
        }

        public static IWritable TrackPlayer(string track)
        {
            return new HtmlElem("iframe")
                .WithAttr("src",string.Format("https://embed.spotify.com/?uri=spotify:track:{0}",track))
                .WithAttr("width","300")
                .WithAttr("height","380")
                .WithAttr("frameborder","0")
                .WithAttr("allowtransparency","true");
        }

        public static IWritable PlaylistPlayer(string name,string[] tracks)
        {
            StringBuilder sb = new StringBuilder();
            int i=0;
            for (;i<tracks.Length-1;i++)
            {
                sb.Append(tracks[i]);
                sb.Append(",");
            }
            sb.Append(tracks[i]);
            string trackset = sb.ToString();
            return new HtmlElem("iframe")
                .WithAttr("src", string.Format("https://embed.spotify.com/?uri=spotify:trackset:{0}:{1}", name,trackset))
                .WithAttr("width", "300")
                .WithAttr("height", "380")
                .WithAttr("frameborder", "0")
                .WithAttr("allowtransparency", "true");
        }
        // *********************************************
        public static IWritable InputSubmit(String value)
        {
            return new HtmlElem("input")
                .WithAttr("type", "submit")
                .WithAttr("value", value);
        }
        public static IWritable Ul(params IWritable[] c)
        {
            return new HtmlElem("ul", c);
        }
        public static IWritable Li(params IWritable[] c)
        {
            return new HtmlElem("li", c);
        }
        public static IWritable P(params IWritable[] c)
        {
            return new HtmlElem("p", c);
        }
        public static IWritable A(String href, String t)
        {
            return new HtmlElem("a", Text(t))
                .WithAttr("href", href);
        }
        public static IWritable Img(string src, string alt)
        {
            return new HtmlElem("img")
                .WithAttr("src", src)
                .WithAttr("alt", alt);
        }
    }

    public class HtmlDoc : HtmlBase, IWritable
    {
        private readonly IWritable[] _c;
        private readonly string _t;

        public HtmlDoc(string t, params IWritable[] content)
        {
            _t = t;
            _c = content;
        }

        public void WriteTo(TextWriter tw)
        {
            new HtmlElem("html",
                    new HtmlElem("head", new HtmlElem("title", new HtmlText(_t))),
                    new HtmlElem("body", _c)
                ).WriteTo(tw);
        }

        public string ContentType
        {
            get { return "text/html"; }
        }
    }
}
