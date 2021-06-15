using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;


public static class TimeSpanExtension
{
    public static string ToReadableString(this TimeSpan span)
    {
        return string.Join(", ", span.GetReadableStringElements().Where(str => !string.IsNullOrEmpty(str)).ToArray());
    }

    public static string ToFormatedString(this TimeSpan span)
    {
        return string.Join(" ", span.GetFormatedElements().Where(str => !string.IsNullOrEmpty(str)).ToArray());
    }

    public static string ToShortString(this TimeSpan span)
    {
        return ((int)Math.Floor(span.TotalDays) * 24 + span.Hours).ToString("00") + ":" + span.Minutes.ToString("00");
    }

    private static IEnumerable<string> GetFormatedElements(this TimeSpan span)
    {
        yield return GetHoras((int)Math.Floor(span.TotalDays), span.Hours);
        yield return GetMinutos(span.Minutes);
    }

    private static IEnumerable<string> GetReadableStringElements(this TimeSpan span)
    {
        yield return GetDaysString((int)Math.Floor(span.TotalDays));
        yield return GetHoursString(span.Hours);
        yield return GetMinutesString(span.Minutes);
        yield return GetSecondsString(span.Seconds);
    }

    public static string GetDaysString(int days)
    {
        if (days == 0)
            return string.Empty;

        if (days == 1)
            return "1 dia";

        return string.Format("{0:0} dias", days);
    }    

    public static string GetHoursString(int hours)
    {
        if (hours == 0)
            return string.Empty;

        if (hours == 1)
            return "1 hora";

        return string.Format("{0:0} horas", hours);
    }

    public static string GetMinutesString(int minutes)
    {
        if (minutes == 0)
            return string.Empty;

        if (minutes == 1)
            return "1 minuto";

        return string.Format("{0:0} minutos", minutes);
    }

    public static string GetSecondsString(int seconds)
    {
        if (seconds == 0)
            return string.Empty;

        if (seconds == 1)
            return "1 segundo";

        return string.Format("{0:0} segundos", seconds);
    }

    private static string GetHoras(int days, int hours)
    {
        hours += days * 24;

        if (hours == 1)
            return "1 hr";

        return string.Format("{0:0} hrs", hours);
    }

    private static string GetMinutos(int minutes)
    {
        if (minutes == 0)
            return string.Empty;

        return string.Format("{0:0} min", minutes);
    }


    public static string FormatarDiasHorasMinutos(this TimeSpan span)
    {
        return 
        GetHorasString(span.Hours, span.Days) +
        GetMinutosString(span.Minutes) +
        GetSegundosString(span.Seconds);
    }

    private static string GetDiasString(int days)
    {
        if (days == 0)
            return string.Empty;

        if (days == 1)
            return "1 d ";

        return string.Format("{0:0} d ", days);
    }

    private static string GetHorasString(int hours, int dias)
    {
        if (dias > 0)
            hours += dias * 24;

        if (hours == 0)
            return string.Empty;

        if (hours == 1)
            return "1 h ";

        return string.Format("{0:0} h ", hours);
    }

    private static string GetMinutosString(int minutes)
    {
        if (minutes == 0)
            return string.Empty;

        if (minutes == 1)
            return "1 min ";

        return string.Format("{0:0} min ", minutes);
    }

    private static string GetSegundosString(int seconds)
    {
        if (seconds == 0)
            return string.Empty;

        if (seconds == 1)
            return "1 s";

        return string.Format("{0:0} s", seconds);
    }
    
}

