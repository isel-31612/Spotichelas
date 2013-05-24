using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace System.Web.Mvc
{
    public static class HelperExtensions
    {
        public static MvcHtmlString TrackPlayer(this HtmlHelper helper, string track)
        {
            string elem = string.Format("<iframe src=\"https://embed.spotify.com/?uri=spotify:track:{0}\" width=\"300\" height=\"380\" frameborder=\"0\" allowtransparency=\"true\"></iframe>",track);
            return MvcHtmlString.Create(elem);
        }

        public static MvcHtmlString PlaylistPlayer(this HtmlHelper helper, string name, string[] tracks)
        {
            StringBuilder sb = new StringBuilder();
            if (tracks.Length == 0)
                return MvcHtmlString.Empty;
            int i = 0;
            for (; i < tracks.Length - 1; i++)
            {
                sb.Append(tracks[i]);
                sb.Append(",");
            }
            sb.Append(tracks[i]);
            string trackset = sb.ToString();

            string elem = string.Format("<iframe src=\"https://embed.spotify.com/?uri=spotify:trackset:{0}:{1}\" width=\"300\" height=\"380\" frameborder=\"0\" allowtransparency=\"true\"></iframe>",name,trackset);
            return MvcHtmlString.Create(elem);
        }

        public static MvcHtmlString InputButton(this HtmlHelper helper, string text)
        {
            if(text==null || text.Length==0)
                return MvcHtmlString.Empty;
            return MvcHtmlString.Create(string.Format("<input type=\"submit\" value=\"{0}\" />",text));
        }

        public static MvcHtmlString ImageFromUrl(this HtmlHelper helper, string url)
        {
            if(url==null || url.Length==0)
                return MvcHtmlString.Empty;
            return MvcHtmlString.Create(string.Format("<img src=\"{0}\"/>",url));
        }
    }
}
