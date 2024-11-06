using System.Globalization;

namespace WebApplication6.Models
{
    public static class DateTimeHelper
    {
        public static DateTime? ConvertStringToDateTime(string dateString, string format = "yyyy-MM-d")
        {
            if (DateTime.TryParseExact(dateString, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date))
            {
                return date;
            }
            return null;
        }
        public static string ConvertStringToFormattedDate(string dateString, string inputFormat = "yyyy-MM-d", string outputFormat = "dd-MM-yyyy")
        {
            if (DateTime.TryParseExact(dateString, inputFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date))
            {
                return date.ToString(outputFormat); // Trả về chuỗi định dạng dd-MM-yyyy
            }
            return null; // Trả về null nếu chuyển đổi không thành công
        }

    }
}
