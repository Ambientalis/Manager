using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.Data.SqlTypes;
using System.Web;

/// <summary>
/// Classe de extensão para efetuar operações recorrentes em strings
/// </summary>
public static class StringExtension
{
    /// <summary>
    /// Método para transformar uma string no formato padrão de um CPF: 000.000.000-00
    /// </summary>
    /// <param name="str">A string que representa o CPF</param>
    /// <returns>Uma string que representa o CPF informado formatado</returns>
    /// <exception cref="string">Caso a string passada não esteja em um formato válido é retornado o valor: "___.___.___-__"</exception>
    public static string ToFormatedCPF(this string str)
    {
        //Limpar a string
        str = str.Replace(".", "").Replace("-", "").Replace("/", "").Replace("\\", "");
        if (str.Length != 11)
            return "___.___.___-__";

        StringBuilder retorno = new StringBuilder();
        retorno.Append(str.Length > 2 ? str.Substring(0, 3) + "." : "");
        retorno.Append(str.Length > 5 ? str.Substring(3, 3) + "." : "");
        retorno.Append(str.Length > 8 ? str.Substring(6, 3) + "-" : "");
        retorno.Append(str.Length > 10 ? str.Substring(9, 2) : "");
        return retorno.ToString();
    }

    /// <summary>
    /// Método para transformar uma string no formato padrão de um CNPJ: 00.000.000/0000-00
    /// </summary>
    /// <param name="str">A string que representa o CNPJ</param>
    /// <returns>Uma string que representa o CNPJ informado formatado</returns>
    public static string ToFormatedCNPJ(this string str)
    {
        //Limpar a string
        str = str.Replace(".", "").Replace("-", "").Replace("/", "").Replace("\\", "");
        if (str.Length != 14)
            return "__.___.___/____-__";

        StringBuilder retorno = new StringBuilder();
        retorno.Append(str.Length > 1 ? str.Substring(0, 2) + "." : "");
        retorno.Append(str.Length > 4 ? str.Substring(2, 3) + "." : "");
        retorno.Append(str.Length > 7 ? str.Substring(5, 3) + "/" : "");
        retorno.Append(str.Length > 11 ? str.Substring(8, 4) + "-" : "");
        return retorno.ToString();
    }

    public static decimal ToDecimal(this string str)
    {
        if (String.IsNullOrEmpty(str))
            return new decimal(0);
        return Convert.ToDecimal(str);
    }

    public static string ToCurrency(this string str)
    {
        try
        {
            if (String.IsNullOrEmpty(str))
                return "R$ 0,00";
            Decimal d = Convert.ToDecimal(str);
            return String.Format("{0:C}", d);
        }
        catch (Exception)
        {
            return "R$ 0,00";
        }
    }

    public static int ToInt32(this string str)
    {
        try
        {
            if (String.IsNullOrEmpty(str))
                return new Int32();
            return Convert.ToInt32(str);
        }
        catch (Exception)
        {
            return new Int32();
        }
    }

    public static float ToFloat(this string str)
    {
        if (String.IsNullOrEmpty(str))
            return new float();
        return float.Parse(str);
    }

    public static DateTime ToDateTime(this string str)
    {
        return str.ToSqlDateTime();
    }

    public static DateTime ToSqlDateTime(this string str)
    {
        DateTime dateSQL = new DateTime(1753, 1, 1, 12, 0, 0);
        if (String.IsNullOrEmpty(str) || Convert.ToDateTime(str).CompareTo(dateSQL) < 0)
            return dateSQL;

        return Convert.ToDateTime(str);
    }

    public static double ToDouble(this string str)
    {
        if (String.IsNullOrEmpty(str))
            return new double();
        return Convert.ToDouble(str);
    }

    public static bool IsNotNullOrEmpty(this string str)
    {
        return !String.IsNullOrEmpty(str);
    }

    public static bool IsStrongPassword(this string str)
    {
        bool isStrong = Regex.IsMatch(str, @"[\d]");
        if (isStrong) isStrong = Regex.IsMatch(str, @"[a-z]");
        if (isStrong) isStrong = Regex.IsMatch(str, @"[A-Z]");
        if (isStrong) isStrong = Regex.IsMatch(str, @"[\s~!@#\$%\^&\*\(\)\{\}\|\[\]\\:;'?,.`+=<>\/]");
        if (isStrong) isStrong = str.Length > 7;
        return isStrong;
    }

    public static bool IsLetra(this string str)
    {
        bool isStrong = Regex.IsMatch(str, @"[a-z]") || Regex.IsMatch(str, @"[A-Z]");
        return isStrong;
    }

    public static bool ContainsAny(this string str, params string[] values)
    {
        if (!string.IsNullOrEmpty(str) || values.Length == 0)
        {
            foreach (string value in values)
            {
                if (str.Contains(value))
                    return true;
            }
        }

        return false;
    }

    public static bool IsDate(this string str)
    {
        if (!string.IsNullOrEmpty(str))
        {
            DateTime dt;
            return (DateTime.TryParse(str, out dt));
        }
        else
        {
            return false;
        }
    }

    public static string ConvertHtmlText(this string str)
    {
        return str.Replace("\n", "<br>");
    }

    /// <summary>
    /// Encrypt a string using dual encryption method. Return a encrypted cipher Text
    /// </summary>
    /// <param name="toEncrypt">string to be encrypted</param>
    /// <param name="useHashing">use hashing? send to for extra secirity</param>
    /// <returns></returns>
    public static string Encrypt(this string toEncrypt, bool useHashing)
    {
        byte[] keyArray;
        byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

        // Get the key from config file
        string key = "#@$%#$¨&¨56&*RTGD456456FSFSG56T456456Y¨&$%YT#R%¨&WEFW5fsdfsd456%¨&¨%&6465";
        //System.Windows.Forms.MessageBox.Show(key);
        if (useHashing)
        {
            MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
            keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
            hashmd5.Clear();
        }
        else
            keyArray = UTF8Encoding.UTF8.GetBytes(key);

        TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
        tdes.Key = keyArray;
        tdes.Mode = CipherMode.ECB;
        tdes.Padding = PaddingMode.PKCS7;

        ICryptoTransform cTransform = tdes.CreateEncryptor();
        byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
        tdes.Clear();
        return Convert.ToBase64String(resultArray, 0, resultArray.Length);
    }

    /// <summary>
    /// DeCrypt a string using dual encryption method. Return a DeCrypted clear string
    /// </summary>
    /// <param name="cipherString">encrypted string</param>
    /// <param name="useHashing">Did you use hashing to encrypt this data? pass true is yes</param>
    /// <returns></returns>
    public static string Decrypt(this string cipherString, bool useHashing)
    {
        byte[] keyArray;
        byte[] toEncryptArray = Convert.FromBase64String(cipherString);

        string key = "#@$%#$¨&¨56&*RTGD456456FSFSG56T456456Y¨&$%YT#R%¨&WEFW5fsdfsd456%¨&¨%&6465";
        if (useHashing)
        {
            MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
            keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
            hashmd5.Clear();
        }
        else
            keyArray = UTF8Encoding.UTF8.GetBytes(key);

        TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
        tdes.Key = keyArray;
        tdes.Mode = CipherMode.ECB;
        tdes.Padding = PaddingMode.PKCS7;

        ICryptoTransform cTransform = tdes.CreateDecryptor();
        byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

        tdes.Clear();
        return UTF8Encoding.UTF8.GetString(resultArray);
    }

    /// <summary>
    /// Remove os Caracteres Especiais de Uma String Passada
    /// <para>'=', '\\', ';', '.', ':', ',', '+', '*','-','!','#','£','%','[',']','\"','\'','´','`','_','/','|','¢','¬','&','<','>','~','^','(',')','☺'</para>
    /// </summary>
    /// <param name="x"> string </param>
    /// <returns>A string sem os caracteres</returns>
    public static string RemoverCaracteresEspeciais(this string x)
    {
        char[] trim = { '=', '\\', ';', '.', ':', ',', '+', '*', '-', '!', '#', '£', '%', '[', ']', '\"', '\'', '´', '`', '_', '/', '|', '¢', '¬', '&', '<', '>', '~', '^', '(', ')', '☺' };
        int pos;
        while ((pos = x.IndexOfAny(trim)) >= 0)
        {
            x = x.Remove(pos, 1);
        }
        return x;
    }

    /// <summary>
    /// Substitui Caracteres com assento ou ç
    /// <para> vogais e ç</para>
    /// </summary>
    /// <param name="x">string</param>
    /// <returns>A string modificada</returns>
    public static string SubstituirCaracteresPtBr(this string x)
    {

        x = x.Replace("á", "a");
        x = x.Replace("ã", "a");
        x = x.Replace("â", "a");
        x = x.Replace("à", "a");
        x = x.Replace("Á", "A");
        x = x.Replace("Ã", "A");
        x = x.Replace("Â", "A");
        x = x.Replace("À", "A");

        x = x.Replace("é", "e");
        x = x.Replace("ê", "e");
        x = x.Replace("è", "e");
        x = x.Replace("É", "E");
        x = x.Replace("Ê", "E");
        x = x.Replace("È", "E");

        x = x.Replace("í", "i");
        x = x.Replace("î", "i");
        x = x.Replace("ì", "i");
        x = x.Replace("Í", "I");
        x = x.Replace("Î", "I");
        x = x.Replace("Ì", "I");

        x = x.Replace("ó", "o");
        x = x.Replace("õ", "o");
        x = x.Replace("ô", "o");
        x = x.Replace("ò", "o");
        x = x.Replace("Ó", "O");
        x = x.Replace("Õ", "O");
        x = x.Replace("Ô", "O");
        x = x.Replace("Ò", "O");

        x = x.Replace("ú", "u");
        x = x.Replace("û", "u");
        x = x.Replace("ù", "u");
        x = x.Replace("Ú", "U");
        x = x.Replace("Û", "U");
        x = x.Replace("Ù", "U");

        x = x.Replace("ç", "c");
        x = x.Replace("Ç", "C");

        return x;
    }

    /// <summary>
    /// Remove Caracteres que podem comprometer códigos SQL
    /// <para> '' "" % + </para>
    /// </summary>
    /// <param name="x">string</param>
    /// <returns>A string modificada</returns>
    public static string TratarSQLInjection(this string x)
    {
        char[] trim = { '=', '\\', ';', ':', ',', '+', '*', '-', '!', '#', '£', '%', '[', ']', '\"', '\'', '_', '/', '|', '¢', '&', '<', '>', '(', ')' };
        int pos;
        while ((pos = x.IndexOfAny(trim)) >= 0)
        {
            x = x.Remove(pos, 1);
        }
        return x;
    }


    public static bool IsInt32(this string str)
    {
        if (!string.IsNullOrEmpty(str))
        {
            int numero;
            return (Int32.TryParse(str, out numero));
        }
        else
            return false;
    }

    public static bool IsDecimal(this string str)
    {
        if (!string.IsNullOrEmpty(str))
        {
            decimal numero;
            return (Decimal.TryParse(str, out numero));
        }
        else
            return false;
    }

    public static string ToHtml(this string str)
    {

        str = str.Replace("ó", "&oacute;");
        str = str.Replace("ò", "&ograve;");
        str = str.Replace("ô", "&ocirc;");
        str = str.Replace("õ", "&otilde;");
        str = str.Replace("ö", "&ouml;");
        str = str.Replace("á", "&aacute;");
        str = str.Replace("à", "&agrave;");
        str = str.Replace("â", "&acirc;");
        str = str.Replace("ã", "&atilde;");
        str = str.Replace("ä", "&auml;");
        str = str.Replace("é", "&eacute;");
        str = str.Replace("è", "&egrave;");
        str = str.Replace("ê", "&ecirc;");
        str = str.Replace("ú", "&uacute;");
        str = str.Replace("ù", "&ugrave;");
        str = str.Replace("û", "&ucirc;");
        str = str.Replace("ü", "&uuml;");
        str = str.Replace("í", "&iacute;");
        str = str.Replace("ì", "&igrave;");
        str = str.Replace("ç", "&ccedil;");
        str = str.Replace("Ó", "&Oacute;");
        str = str.Replace("Ò", "&Ograve;");
        str = str.Replace("Ô", "&Ocirc;");
        str = str.Replace("Õ", "&Otilde;");
        str = str.Replace("Ö", "&Ouml;");
        str = str.Replace("Á", "&Aacute;");
        str = str.Replace("À", "&Agrave;");
        str = str.Replace("Â", "&Acirc;");
        str = str.Replace("Ã", "&Atilde;");
        str = str.Replace("Ä", "&Auml;");
        str = str.Replace("É", "&Eacute;");
        str = str.Replace("È", "&Egrave;");
        str = str.Replace("Ê", "&Ecirc;");
        str = str.Replace("Ú", "&Uacute;");
        str = str.Replace("Ù", "&Ugrave;");
        str = str.Replace("Û", "&Ucirc;");
        str = str.Replace("Ü", "&Uuml;");
        str = str.Replace("Í", "&Iacute;");
        str = str.Replace("Ì", "&Igrave;");
        str = str.Replace("Ç", "&Ccedil;");
        str = str.Replace("º", "&ordm;");
        str = str.Replace("ª", "&ordf;");

        System.Text.Encoding iso88591 = System.Text.Encoding.GetEncoding("iso-8859-1");
        System.Text.Encoding utf8 = System.Text.Encoding.UTF8;

        byte[] abInput = iso88591.GetBytes(HttpUtility.HtmlDecode(str));

        str = utf8.GetString(System.Text.Encoding.Convert(iso88591, utf8, abInput));

        return str;
    }

    public static string ToHtmlToString(this string str)
    {
        str = str.Replace("&oacute;", "ó");
        str = str.Replace("&ograve;", "ò");
        str = str.Replace("&ocirc;", "ô");
        str = str.Replace("&otilde;", "õ");
        str = str.Replace("&ouml;", "ö");
        str = str.Replace("&aacute;", "á");
        str = str.Replace("&agrave;", "à");
        str = str.Replace("&acirc;", "â");
        str = str.Replace("&atilde;", "ã");
        str = str.Replace("&auml;", "ä");
        str = str.Replace("&eacute;", "é");
        str = str.Replace("&egrave;", "è");
        str = str.Replace("&ecirc;", "ê");
        str = str.Replace("&uacute;", "ú");
        str = str.Replace("&ugrave;", "ù");
        str = str.Replace("&ucirc;", "û");
        str = str.Replace("&uuml;", "ü");
        str = str.Replace("&iacute;", "í");
        str = str.Replace("&igrave;", "ì");
        str = str.Replace("&ccedil;", "ç");
        str = str.Replace("&Oacute;", "Ó");
        str = str.Replace("&Ograve;", "Ò");
        str = str.Replace("&Ocirc;", "Ô");
        str = str.Replace("&Otilde;", "Õ");
        str = str.Replace("&Ouml;", "Ö");
        str = str.Replace("&Aacute;", "Á");
        str = str.Replace("&Agrave;", "À");
        str = str.Replace("&Acirc;", "Â");
        str = str.Replace("&Atilde;", "Ã");
        str = str.Replace("&Auml;", "Ä");
        str = str.Replace("&Eacute;", "É");
        str = str.Replace("&Egrave;", "È");
        str = str.Replace("&Ecirc;", "Ê");
        str = str.Replace("&Uacute;", "Ú");
        str = str.Replace("&Ugrave;", "Ù");
        str = str.Replace("&Ucirc;", "Û");
        str = str.Replace("&Uuml;", "Ü");
        str = str.Replace("&Iacute;", "Í");
        str = str.Replace("&Igrave;", "Ì");
        str = str.Replace("&Ccedil;", "Ç");
        str = str.Replace("&ordm;", "º");
        str = str.Replace("&ordf;", "ª");
        //s = s.Replace("<o:p />", "");
        //s = s.Replace("<o:p>", "");
        //s = s.Replace("</o:p>", "");
        //s = s.Replace(">>", ">"); 
        return str;
    }


}

