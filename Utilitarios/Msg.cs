using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Net.Mail;
using System.Net;
using System.Runtime.Remoting.Messaging;
using System.IO;
using System.Runtime.InteropServices;

namespace Utilitarios
{
    /// <summary>
    /// Classe que define as mensagens dos sistemas WEB e Desktop
    /// </summary>
    public class Msg
    {
        #region __________ ATRIBUTOS __________

        private string mensagem = null;
        private string caption = null;
        private MsgIcons ico;
        private MsgStyle estilo;
        private MsgBotoes[] botoes = null;
        private Exception ex;

        #endregion

        #region __________ Propriedades _______

        public string Mensagem
        {
            get { return mensagem; }
        }

        public string Caption
        {
            get { return caption; }
        }
        public MsgIcons Ico
        {
            get { return ico; }
        }

        public MsgStyle Estilo
        {
            get { return estilo; }
        }

        public MsgBotoes[] Botoes
        {
            get { return botoes; }
        }
        public Exception Ex
        {
            get { return ex; }
            set { ex = value; }
        }

        #endregion

        #region __________ Métodos ____________

        /// <summary>
        /// Cria uma mensagem no estilo do sistema
        /// </summary>
        /// <param name="ex">A excessão base para a criação da mensagem</param>
        /// <returns>Um Objeto do tipo Msg formatado</returns>
        public Msg CriarMensagem(Exception ex)
        {
            this.Ex = ex;

            if (ex.GetType() == typeof(FormatException) && (ex.Message.Contains("DateTime")))
            {
                this.mensagem = "Data com valor inválido.";
                this.caption = "Alerta";
                this.ico = MsgIcons.Alerta;
                this.estilo = MsgStyle.TemaPopUp;
                this.botoes = null;
            }
            else
            {
                this.mensagem = "Ops.. Ocorreu um erro inesperado! Desculpe-nos pelo incômodo. ";
                this.mensagem +=
                    HttpContext.Current == null ?
                    "Entre em contato com o administrador do sistema para que as devidas providências sejam realizadas" :
                    "As devidas providências já estão sendo realizadas para que você trabalhe sem problemas. ";
                this.mensagem += "Descrição do Erro = " + ex.Message;
                this.caption = "Leve erro do Sistema.";
                this.ico = MsgIcons.Erro;
                this.estilo = MsgStyle.TemaPopUp;
                this.botoes = null;

                //this.GravarLog();
            }
            return this;
        }

        /// <summary>
        /// Cria uma mensagem no estilo do sistema
        /// </summary>
        /// <param name="mensagem">A mensagem a ser exibida</param>
        /// <param name="caption">O título da janela da mensagem</param>
        /// <returns>um objeto do tipo Msg formatado</returns>
        public Msg CriarMensagem(string mensagem, string caption)
        {
            this.mensagem = mensagem;
            this.caption = caption;
            this.ico = MsgIcons.Sucesso;
            this.estilo = MsgStyle.TemaPopUp;
            this.botoes = null;
            return this;
        }

        /// <summary>
        /// Cria uma mensagem no estilo do sistema
        /// </summary>
        /// <param name="mensagem">A mensagem a ser exibida</param>
        /// <param name="caption">O título da janela da mensagem</param>
        /// <param name="icone">O ícone a ser mostrado na mensagem</param>
        /// <returns></returns>
        public Msg CriarMensagem(string mensagem, string caption, MsgIcons icone)
        {
            this.mensagem = mensagem;
            this.caption = caption;
            this.ico = icone;
            this.estilo = MsgStyle.TemaPopUp;
            this.botoes = null;
            return this;
        }

        /// <summary>
        /// Cria uma mensagem no estilo do sistema
        /// </summary>
        /// <param name="mensagem">A mensagem a ser exibida</param>
        /// <param name="caption">O título da janela da mensagem</param>
        /// <param name="icone">O ícone a ser mostrado na mensagem</param>
        /// <param name="botoes">Os botões que serão exibidos na mensagem</param>
        /// <returns></returns>
        public Msg CriarMensagem(string mensagem, string caption, MsgIcons icone, params MsgBotoes[] botoes)
        {
            this.mensagem = mensagem;
            this.caption = caption;
            this.ico = icone;
            this.estilo = MsgStyle.TemaPopUp;
            this.botoes = botoes;
            return this;
        }

        /// <summary>
        /// Cria uma mensagem no estilo do sistema
        /// </summary>
        /// <param name="mensagem">A mensagem a ser exibida</param>
        /// <param name="caption">O título da janela da mensagem</param>
        /// <param name="icone">O ícone a ser mostrado na mensagem</param>
        /// <param name="estilo">O estilo da mensagem</param>
        /// <param name="botoes">Os botões que serão exibidos na mensagem</param>
        /// <returns></returns>
        public Msg CriarMensagem(string mensagem, string caption, MsgIcons icone, MsgStyle estilo, params MsgBotoes[] botoes)
        {
            this.mensagem = mensagem;
            this.caption = caption;
            this.ico = icone;
            this.estilo = estilo;
            this.botoes = botoes;
            return this;
        }

        //private void GravarLog()
        //{
        //    StreamWriter writer = null;
        //    try
        //    {
        //        if (CallContext.GetData("pathAplicacao") != null)
        //        {
        //            string caminhoLog = CallContext.GetData("pathAplicacao").ToString() + "/Log";
        //            if (!Directory.Exists(caminhoLog))
        //                Directory.CreateDirectory(caminhoLog);
        //            writer = new StreamWriter(caminhoLog + "/logErros.log", true);
        //            string nomeEmpresa = CallContext.GetData("nomeEmpresa") != null ? CallContext.GetData("nomeEmpresa").ToString() : "Desconhecida";

        //            StringBuilder builder = new StringBuilder();
        //            builder.AppendLine("Data e hora: " + DateTime.Now.ToString());
        //            builder.AppendLine("Descrição:");
        //            builder.AppendLine(ex.Message);
        //            int entradas = 0;
        //            Exception inner = ex.InnerException;

        //            while (inner != null && inner.InnerException != null && entradas < 5)
        //            {
        //                builder.AppendLine("Inner:" + entradas.ToString() + " >>>> " + inner.Message + " <<<<");
        //                inner = inner.InnerException;
        //                ++entradas;
        //            }

        //            builder.AppendLine(ex.StackTrace != null ? ex.StackTrace : "");
        //            builder.AppendLine("========================================================================");
        //            builder.AppendLine();

        //            writer.WriteLine(builder.ToString());
        //            writer.Close();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        if (writer != null)
        //            writer.Dispose();
        //    }
        //}

        #endregion
    }

    /// <summary>
    /// Enumeração de ícones das mensagens
    /// </summary>
    public enum MsgIcons
    {
        Nenhum, Alerta, Exclamacao, Informacao, Interrogacao, Erro, Sucesso, AcessoNegado
    }

    /// <summary>
    /// Enumeração de estilo das mensagens
    /// </summary>
    public enum MsgStyle
    {
        TemaPopUp,
    }

    /// <summary>
    /// Enumeração que define quais os botões que serão exibidos nas mensagens
    /// </summary>
    public enum MsgBotoes
    {
        Ok, Salvar, Excluir, Alterar, Atualizar, Cancelar
    }
}
