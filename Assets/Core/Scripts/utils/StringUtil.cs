using System;
using System.Text;

namespace ZGGame
{
    public class StringUtil
    {

        public static string addStr(string s, StringBuilder sb)
        {
            int ck = sb.Capacity - (s.Length + sb.Length);
            if (ck < 0)
            {
                sb.Remove(0, Math.Min(sb.Length,-ck));
            }

            sb.Append(s);

            return sb.ToString();
        }
    }
}