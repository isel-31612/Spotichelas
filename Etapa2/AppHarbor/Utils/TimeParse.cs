using System;

namespace Utils
{
    public class TimeParse
    {
        public static string Seconds(int secs)
        {
            int hours=0;
            int minutes=0;
            int seconds = 0;
            if (secs >= 3600)
            {
                hours = secs / 3600;
                seconds = secs % 3600;
            }
            if (secs > 60)
            {
                minutes = secs / 60;
                secs = secs % 60;
            }
            seconds = secs;

            return String.Format("{0,5}:{1:2}:{2:2}", hours, minutes.ToString("D2"), seconds.ToString("D2"));
        }

        public static string Seconds(string seconds)
        {
            int secs;
            try
            {
                secs = Int32.Parse(seconds);
            }
            catch (FormatException)
            {
                secs = 0;
            }
            catch (NullReferenceException)
            {
                secs = 0;
            }
            return Seconds(secs);
        }
    }
}
