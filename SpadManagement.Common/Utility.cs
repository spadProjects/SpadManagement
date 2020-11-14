using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpadManagement.Common
{
    public static class Utility
    {
        /// <summary>
        /// Version
        /// </summary>
        public static string Version = "1.0.0";
        public static int PageSize = 10;
        public static int GridTextLength = 30;

        public static int CalculatePageSize(int count, int size)
        {
            // TODO: Define behaviour for negative numbers
            int remainder;
            int quotient = Math.DivRem(count, size, out remainder);
            return remainder == 0 ? quotient : quotient + 1;
        }

        public static string SplitDigit(this long value)
        {
            return value.ToString("N0", new NumberFormatInfo() { NumberGroupSizes = new[] { 3 }, NumberGroupSeparator = "," });
        }

        public static DateTime ConvertToPersian(string date)
        {
            var splitDate = date.Split('/');
            var month = splitDate[0];
            var day = splitDate[1];
            var year = splitDate[2].Split(' ')[0];

            return DateTime.Parse($"{year}/{month}/{day}", new CultureInfo("fa-IR"));
        }

        public static string TimeAgo(DateTime dt)
        {
            TimeSpan span = DateTime.Now - dt;

            if (span.Days > 30)
                return String.Format("{0}:{1} - {2}", dt.Hour, dt.Minute, dt.ToPersianString());

            if (span.Days > 0)
                return String.Format("{0} روز پیش",
                span.Days);

            if (span.Hours > 0)
                return String.Format("{0} ساعت پیش",
                span.Hours);

            if (span.Minutes > 0)
                return String.Format("{0} دقیقه پیش",
                span.Minutes);

            if (span.Seconds > 0)
                return "هم اکنون";

            return string.Empty;
        }
        public static string GetTrackCode()
        {
            var bytes = Guid.NewGuid().ToByteArray();
            var code = "";

            for (int i = 0; code.Length <= 16 && i < bytes.Length; i++)
                code += (bytes[i] % 10).ToString();

            return code.ToLower();
        }
        public static string GetStringThumbnail(string Title, string context)
        {
            var thumbnail = $"<h6>{Title} : </h6>";
            var template = "<hr/><b style='color: blue; direction: rtl'> {0} </b><br />";
            //var template = "<hr/><b style='color: blue; direction: rtl'> {0} </b><span>({1}) : </span><br /><span>{2}</span>";

            if (!string.IsNullOrEmpty(context))
                thumbnail += string.Format(template, context);
            else
                thumbnail += "<b style='color: red; direction: rtl'>ندارد</b>";

            return thumbnail;
        }
        public static string NumberToCounterWords(long number)
        {
            var words = NumberToWords(number) + "م";
            words = words.Replace("سیم", "سی ام");

            if (number == 1)
                words = words.Replace("یکم", "اول");

            return words;
        }
        public static string NumberToWords(long number)
        {
            if (number == 0)
                return "صفر";

            if (number < 0)
                return "منفی " + NumberToWords(Math.Abs(number));

            string words = "";

            if ((number / 1000000000) > 0)
            {
                if (number % 1000000000 == 0)
                {
                    words += NumberToWords(number / 1000000000) + " میلیارد ";
                    number %= 1000000000;
                }
                else
                {
                    words += NumberToWords(number / 1000000000) + " میلیارد و ";
                    number %= 1000000000;
                }
            }

            if ((number / 1000000) > 0)
            {
                if (number % 1000000 == 0)
                {
                    words += NumberToWords(number / 1000000) + " میلیون ";
                    number %= 1000000;
                }
                else
                {
                    words += NumberToWords(number / 1000000) + " میلیون و ";
                    number %= 1000000;
                }
            }

            if ((number / 1000) > 0)
            {
                if (number % 1000 == 0)
                {
                    words += NumberToWords(number / 1000) + " هزار ";
                    number %= 1000;
                }
                else
                {
                    words += NumberToWords(number / 1000) + " هزار و ";
                    number %= 1000;

                }
            }

            if ((number / 100) > 0)
            {
                if (number % 100 == 0)
                {
                    words += NumberToWords(number / 100) + " صد ";
                    number %= 100;
                }
                else
                {
                    words += NumberToWords(number / 100) + " صد و ";
                    number %= 100;
                }
            }

            if (number > 0)
            {
                var unitsMap = new[] { "صفر", "یک", "دو", "سه", "چهار", "پنج", "شش", "هفت", "هشت", "نه", "ده", "یازده", "دوازده", "سیزده", "چهارده", "پانزده", "شانزده", "هفده", "هجده", "نوزده" };
                var tensMap = new[] { "صفر", "ده", "بیست", "سی", "چهل", "پنجاه", "شصت", "هفتاد", "هشتاد", "نود" };

                if (number < 20)
                    words += unitsMap[number];
                else if (number < 100)
                {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0)
                        words += " و " + unitsMap[number % 10];
                }
            }


            words = words.Replace("سو","سه");
            words = words.Replace("یک صد", "صد");
            words = words.Replace("دو صد", "دویست");
            words = words.Replace("سه صد", "سیصد");
            words = words.Replace("چهار صد", "چهارصد");
            words = words.Replace("پنج صد", "پانصد");
            words = words.Replace("شش صد", "ششصد");
            words = words.Replace("هفت صد", "هفتصد");
            words = words.Replace("هشت صد", "هشتصد");
            return words;
        }
        public static string ToPersianString(this DateTime date)
        {
            var pc = new System.Globalization.PersianCalendar();
            var month = pc.GetMonth(date);
            var day = pc.GetDayOfMonth(date);

            var monthStr = month < 10 ? "0" + month : month.ToString();
            var dayStr = day < 10 ? "0" + day : day.ToString();

            return string.Format("{0}/{1}/{2}", pc.GetYear(date), monthStr, dayStr);
        }
        public static string ToPersianString(this DateTime? date)
        {
            if (date == null)
                return "";

            return date.Value.ToPersianString();
        }

        public static string ToPersianStringDateTime(this DateTime date)
        {
            if (date == null)
                return "";

            var hour = date.Hour != 0 ? date.Hour.ToString() : "00";
            var minute = date.Hour != 0 ? date.Minute.ToString() : "00";
            var second = date.Hour != 0 ? date.Second.ToString() : "00";

            return $"{hour}:{minute}:{second}" + " - " + date.ToPersianString();
        }

        public static string ToPersianStringDateTimeRtl(this DateTime date)
        {
            if (date == null)
                return "";

            var hour = date.Hour != 0 ? date.Hour.ToString() : "00";
            var minute = date.Hour != 0 ? date.Minute.ToString() : "00";
            var second = date.Hour != 0 ? date.Second.ToString() : "00";

            return date.ToPersianString() + " - " + $"{hour}:{minute}:{second}";
        }
        public static string GetExcelCellName(int colIndex, int rowIndex)
        {
            int dividend = colIndex;
            string columnName = String.Empty;
            int modulo;

            while (dividend > 0)
            {
                modulo = (dividend - 1) % 26;
                columnName = Convert.ToChar(65 + modulo).ToString() + columnName;
                dividend = (int)((dividend - modulo) / 26);
            }

            return columnName + rowIndex.ToString();
        }

        public static string TruncateString(string text, int size)
        {
            if (text.Length <= size)
                return text;

            return (text.Substring(0, size) + "...");
        }

        public static void PeriodDate(string Period, out DateTime? contractEndDate, out DateTime? StartcontractEndDate)
        {
            contractEndDate = null;
            StartcontractEndDate = null;

            switch (Period)
            {
                case "today":
                    contractEndDate = StartcontractEndDate = DateTime.Today;
                    break;
                case "yesterday":
                    StartcontractEndDate = DateTime.Today.AddDays(-1);
                    contractEndDate = DateTime.Today;
                    break;
                case "Lastweek":
                    StartcontractEndDate = DateTime.Today.AddDays(-7);
                    contractEndDate = DateTime.Today;
                    break;
                case "Lastmonth":
                    StartcontractEndDate = DateTime.Today.AddMonths(-1);
                    contractEndDate = DateTime.Today;
                    break;
                case "Last3month":
                    StartcontractEndDate = DateTime.Today.AddMonths(-3);
                    contractEndDate = DateTime.Today;
                    break;
                case "Last6month":
                    StartcontractEndDate = DateTime.Today.AddMonths(-6);
                    contractEndDate = DateTime.Today;
                    break;
                case "Lastyear":
                    StartcontractEndDate = DateTime.Today.AddYears(-1);
                    contractEndDate = DateTime.Today;
                    break;
                case "tomorrow":
                    contractEndDate = DateTime.Today.AddDays(1);
                    StartcontractEndDate = DateTime.Today;
                    break;
                case "Nextweek":
                    contractEndDate = DateTime.Today.AddDays(7);
                    StartcontractEndDate = DateTime.Today;
                    break;
                case "Nextmonth":
                    contractEndDate = DateTime.Today.AddMonths(1);
                    StartcontractEndDate = DateTime.Today;
                    break;
                case "Next3month":
                    contractEndDate = DateTime.Today.AddMonths(3);
                    StartcontractEndDate = DateTime.Today;
                    break;
            }
        }
        public static T LogInfo<T>(T entity) where T : class
        {
            //if (!entity.HasKey())
            //{
            //    entity.InsertDate = DateTime.Now;
            //    entity.InsertUser = HttpContext.Current.Session["UserName"]?.ToString();
            //}
            //else
            //{
            //    entity.UpdateDate = DateTime.Now;
            //    entity.UpdateUser = HttpContext.Current.Session["UserName"]?.ToString();
            //}

            return entity;
        }
    }
}
