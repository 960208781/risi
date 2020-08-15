namespace ZGGame
{
    public class FormatUtil
	{
		/// <summary>
		/// {0}:{1}:{2}
		/// </summary>
		public const string TYPE_1 = "{0}:{1}:{2}";
		/// <summary>
		/// {0}时{1}分{2}秒
		/// </summary>
		public const string TYPE_2 = "{0}时{1}分{2}秒";
		
		public static string secondsToString(int sec,string format)
		{
			string h = getHour(sec);
			string m = getMin(sec);
			string s = getSec(sec);
			
			return string.Format(format, h, m, s);
		}
		
		public static string getHour(int sec)
		{
			int h = sec / 3600;
			if (h > 0)
			{
				if (h < 10)
					return "0" + h;
				else
					return h.ToString();
			}
			else
				return "00";
			
		}
		/// <summary>
		/// 24小时制
		/// </summary>
		/// <param name="sec"></param>
		/// <returns></returns>
		public static string getHourDay(int sec)
		{
			int h = (sec / 3600)%24;
			if (h > 0)
			{
				if (h < 10)
					return "0" + h;
				else
					return h.ToString();
			}
			else
				return "00";
		}
		
		public static string getMin(int sec)
		{
			int m = (sec / 60) % 60;
			if (m > 0)
			{
				if (m < 10)
					return "0" + m;
				else
					return m.ToString();
			}
			else
				return "00";
		}
		
		public static string getSec(int sec)
		{
			int s = sec % 60;
			if (s < 10)
				return "0" + s;
			else
				return s.ToString();
		}
	}
}
