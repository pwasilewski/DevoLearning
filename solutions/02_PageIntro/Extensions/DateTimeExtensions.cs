namespace Nihdi.DevoLearning.Presentation.Extensions
{
    public static class DateTimeExtensions
    {
        public static string ToShortDisplayFormat(this DateTime? dateTime)
        {
            if (!dateTime.HasValue)
            {
                return null;
            }

            return dateTime.Value.ToShortDisplayFormat();
        }

        public static string ToShortDisplayFormat(this DateTime dateTime)
        {
            return dateTime.ToString("dd/MM/yyyy");
        }
    }
}
