using System.Web;


namespace Utilitarios.Criptografia
{
    /// <summary>
    /// Classe responsável por cifrar e cifrar valores string
    /// </summary>
    public class Seguranca
    {
        #region Construtores
        /// <summary>
        /// Construtor
        /// </summary>
        public Seguranca()
        {

        }

        #endregion

        /// <summary>
        /// Decifra um parâmetro
        /// </summary>
        /// <param name="variavel">a variável passada</param>
        /// <param name="queryString">o Valor cifrado</param>
        /// <returns></returns>
        public static string RecuperarParametro(string variavel, HttpRequest request)
        {
            try
            {
                SecureQueryString crip = new SecureQueryString(request.QueryString["parametros"].ToString());
                return crip[variavel];
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Cifra uma string
        /// </summary>
        /// <param name="parametro">A string a ser cifrada</param>
        /// <returns></returns>
        public static string MontarParametros(string parametro)
        {
            SecureQueryString crip = new SecureQueryString();
            string[] listaParametros = parametro.Split('&');
            string[] subParametos;
            foreach (string item in listaParametros)
            {
                subParametos = item.Split('=');
                crip[subParametos[0]] = subParametos[1];
            }
            return "?parametros=" + crip.EncryptedString; ;

        }


    }
}
