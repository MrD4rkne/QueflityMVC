namespace QueflityMVC.Web.Common;

public class MessageDateTimeFormatter : IMessageDateTimeFormatter
{
    private readonly DateTime _now = DateTime.Now;

    public string FormatDateTime(DateTime dateTime)
    {
        return GetFormat(dateTime, DateTime.Now);
    }
    
    private static string GetFormat(DateTime dateTime, DateTime now)
    {
        string time = dateTime.ToString("HH:mm");
        if(dateTime.Date == now.Date)
            return time;
        string day = dateTime.Date switch
        {
            var date when date == now.Date.AddDays(-1) => "Yesterday",
            var date when date > now.Date.AddDays(-7) => dateTime.ToString("dddd"),
            _ => dateTime.ToString(dateTime.Year == now.Year ? "dd/MM" : "dd/MM/yyyy")
        };
        return $"{time} | {day}";
    }
}