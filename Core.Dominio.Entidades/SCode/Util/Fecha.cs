
namespace Core.Dominio.Util
{
    using System;

    public class Fecha
    {
        public static int getFechaActual()
        {
            
            return DateTime.Now.Millisecond;
        }

        public static int getFechaEmpty()
        {
            return 0;
        }

        public static long getFechaActualMilliseconds() {
            DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            long millisecond = (long)(DateTime.UtcNow - epoch).TotalMilliseconds;

            return millisecond;
        }
        public static DateTime getFechaDateTimeActual()
        {
            DateTime fechaActual = DateTime.Now;
            return fechaActual;
        }

        public static string MillisecondsToString(long milliseconds, string format)
        {
            string dateString = "";
            if (milliseconds > 0)
            {
                double ticks = double.Parse(milliseconds.ToString());
                TimeSpan time = TimeSpan.FromMilliseconds(ticks);
                DateTime date = new DateTime(1970, 1, 1) + time;
                dateString = date.ToString(format);
            }
            return dateString;
        }
    }
}
