using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Persistencia.Fabrica;
using System.Web;
using System.Runtime.Remoting.Messaging;

namespace Utilitarios
{
    public class Transacao
    {
        public static Transacao New
        {
            get { return new Transacao(); }
        }

        #region Thread-safe, lazy Singleton

        public static Transacao Instance
        {
            get
            {
                return Nested.Transacao;
            }
        }

        private Transacao()
        {
        }

        private class Nested
        {
            static Nested() { }
            internal static readonly Transacao Transacao = new Transacao();
        }

        #endregion

        public void Abrir()
        {
            this.BeginTransaction();
        }

        public void Abrir(int id)
        {
            NHibernateSessionManager.Instance.BeginTransaction(id);
        }

        public void Fechar()
        {
            this.CommitAndCloseSession();
        }

        public void Recarregar()
        {
            this.Fechar();
            this.Abrir();
        }

        public void Fechar(ref Msg mensagem)
        {
            this.CommitAndCloseSession(ref mensagem);
        }

        public void Fechar(ref Msg mensagem, bool roolBack)
        {
            if (roolBack)
                this.RollBackAndCloseSession(ref mensagem);
            else
                this.CommitAndCloseSession(ref mensagem);
        }

        private void BeginTransaction()
        {
            if (HttpContext.Current.Session != null && HttpContext.Current.Session["idConfig"] != null)
                NHibernateSessionManager.Instance.BeginTransaction(Convert.ToInt32(HttpContext.Current.Session["idConfig"]));
        }

        private void RollBackAndCloseSession(ref Msg mensagem)
        {
            try
            {
                NHibernateSessionManager.Instance.RollbackTransaction();
            }
            catch (Exception ex)
            {
                mensagem = mensagem.CriarMensagem(ex);
            }
            finally
            {
                NHibernateSessionManager.Instance.CloseSession();
            }
        }

        private void CommitAndCloseSession()
        {
            try
            {
                NHibernateSessionManager.Instance.CommitTransaction();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                NHibernateSessionManager.Instance.CloseSession();
            }
        }

        private void CommitAndCloseSession(ref Msg mensagem)
        {
            try
            {
                NHibernateSessionManager.Instance.CommitTransaction();
            }
            catch (Exception ex)
            {
                mensagem = mensagem.CriarMensagem(ex);
            }
            finally
            {
                NHibernateSessionManager.Instance.CloseSession();
            }
        }

        public void DestacarSessao()
        {
            NHibernateSessionManager.Instance.DestacarSessao();
        }

        public bool HasOpen()
        {
            return NHibernateSessionManager.Instance.HasOpenTransaction();
        }

        /// <summary>
        /// Fecha todas as transações abertas
        /// </summary>
        public void Finalizar(ref Msg msg)
        {
            while (this.HasOpen())
                this.Fechar(ref msg);
        }

        /// <summary>
        /// Fecha todas as transações abertas
        /// </summary>
        public void Finalizar()
        {
            while (this.HasOpen())
                this.Fechar();
        }
    }
}
