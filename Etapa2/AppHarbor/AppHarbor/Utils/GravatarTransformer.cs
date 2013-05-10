using System.Security.Cryptography;
using System.Text;

namespace Utils
{
    public class GravatarTransformer
    {
        public static string GetUserImageUrlFromEmail(string validEmail)
        {
            string preparedEmail = validEmail.Trim().ToLower();
            MD5 md5Hasher = MD5.Create();
            byte[] data = md5Hasher.ComputeHash(Encoding.UTF8.GetBytes(preparedEmail));
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                builder.Append(data[i].ToString("x2"));
            }
            string hash = builder.ToString();
            return string.Format("http://www.gravatar.com/avatar/{0}?d=mm", hash);//in case user is not a gravatar user d=mm returns a default image
        }
    }
}
