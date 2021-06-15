using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

public static class DateTimeExtension
{
    /// <summary>
    /// Transforma uma data em uma sua representação por extenso
    /// </summary>
    /// <param name="data">A data de referência</param>
    /// <returns>Uma string que representa a data por extenso</returns>
    public static string ToExtensionDate(this DateTime date)
    {
        return date.Day.ToString("00") + " de " +
            new CultureInfo("pt-BR").DateTimeFormat.GetMonthName(date.Month) +
            " de " + date.Year.ToString("0000");
    }

    public static string EmptyToMinValueSQL(this DateTime date)
    {
        if (date <= new DateTime().MinValueSQL() || date == DateTime.MaxValue)
            return "";
        else
            return date.ToShortDateString();
    }

    public static string EmptyToMinValue(this DateTime date)
    {
        if (date <= new DateTime().MinValueSQL() || date >= DateTime.MaxValue)
            return "";
        else
            return date.ToShortDateString();
    }

    public static string EmptyToMinValueLongFormat(this DateTime date)
    {
        if (date <= new DateTime().MinValueSQL() || date >= DateTime.MaxValue)
            return "";
        else
            return date.ToString("dd/MM/yyyy HH:mm:ss");
    }

    public static string NaoInformadaToMinValue(this DateTime date)
    {
        if (date <= new DateTime().MinValueSQL() || date >= DateTime.MaxValue)
            return "Não informado";
        else
            return date.ToShortDateString();
    }

    public static DateTime ToMaxHourOfDay(this DateTime data)
    {
        return Convert.ToDateTime(data.ToShortDateString() + " 23:59:59");
    }

    public static DateTime ToMinHourOfDay(this DateTime data)
    {
        return Convert.ToDateTime(data.ToShortDateString() + " 00:00:00");
    }

    public static DateTime MinValueSQL(this DateTime data)
    {
        return new DateTime(1753, 1, 1, 12, 0, 0);
    }
}