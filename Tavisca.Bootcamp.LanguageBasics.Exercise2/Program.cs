using System;

namespace Tavisca.Bootcamp.LanguageBasics.Exercise2
{
    public static class Program
    {
        static void Main(string[] args)
        {
            Test(new[] {"12:12:12"}, new [] { "few seconds ago" }, "12:12:12");
            Test(new[] { "23:23:23", "23:23:23" }, new[] { "59 minutes ago", "59 minutes ago" }, "00:22:23");
            Test(new[] { "00:10:10", "00:10:10" }, new[] { "59 minutes ago", "1 hours ago" }, "impossible");
            Test(new[] { "11:59:13", "11:13:23", "12:25:15" }, new[] { "few seconds ago", "46 minutes ago", "23 hours ago" }, "11:59:23");
            Console.ReadKey(true);
        }

        private static void Test(string[] postTimes, string[] showTimes, string expected)
        {
            var result = GetCurrentTime(postTimes, showTimes).Equals(expected) ? "PASS" : "FAIL";
            var postTimesCsv = string.Join(", ", postTimes);
            var showTimesCsv = string.Join(", ", showTimes);
            Console.WriteLine($"[{postTimesCsv}], [{showTimesCsv}] => {result}");
        }

        public static string GetCurrentTime(string[] exactPostTime, string[] showPostTime)
        {
            //
            //checking for any conflicts in two showposttime
            //
           for (int i = 0; i < exactPostTime.Length; i++)
            {
                for (int j = i + 1; j < exactPostTime.Length; j++)
                 {
                    if (exactPostTime[i] == exactPostTime[j])
                        if (showPostTime[i] != showPostTime[j])
                            return "impossible";
                }
            }
            //
            //now this block will be executed if there is no conflict in showposttime
            //
            string[] resultant_date_time = new string[exactPostTime.Length];
            for(int i = 0; i<exactPostTime.Length; i++)
             {
                string[] split_hh_mm_ss_string=exactPostTime[i].Split(':');
                //
                //for showposttime containing seconds
                //
                if(showPostTime[i].Contains("seconds"))
                {
                    resultant_date_time[i]=exactPostTime[i];
                    
                }
                //
                //for showposttime containing minutes
                //
                if(showPostTime[i].Contains("minutes"))
                {
                    int r=Convert.ToInt32(showPostTime[i].Split(" ")[0]);
                    int q=Convert.ToInt32(split_hh_mm_ss_string[1]);
                    int y=Convert.ToInt32(split_hh_mm_ss_string[0]);
                    r=r+q;//calculating total minutes
                    //if minutes>59
                    if(r>59)
                    {
                        q=r%60;
                        y=y+r/60;
                    }
                    else{
                        q=r;
                    }
                
                    string t="";
                    //if after addition hour >23
                    if(y>23){
                        t="00";
                    }
                    else{
                        t=Convert.ToString(y);
                    }
                    string z=t+":"+Convert.ToString(q)+":"+split_hh_mm_ss_string[2];
                    
                    resultant_date_time[i]=z;
                    
                }
                //
                //for showposttime containing hours
                //
                if(showPostTime[i].Contains("hours"))
                {
                    int r=Convert.ToInt32(showPostTime[i].Split(" ")[0]);
                    int y=Convert.ToInt32(split_hh_mm_ss_string[0]);
                    r=r+y;
                    //if hours>23 after addition
                    if(r>23)
                    {
                        r=r-24;
                    }
                    string z=Convert.ToString(r)+":"+split_hh_mm_ss_string[1]+":"+split_hh_mm_ss_string[2];
                    resultant_date_time[i]=z;
                }
            }
            //soting string array for returning resultant value as specified
            Array.Sort(resultant_date_time ,StringComparer.InvariantCulture);
            return resultant_date_time[(exactPostTime.Length-1)];
            throw new NotImplementedException();
        }
    }
}
