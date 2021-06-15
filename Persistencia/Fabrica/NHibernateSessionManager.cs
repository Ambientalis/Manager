using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using System.Web;
using System.Runtime.Remoting.Messaging;
using NHibernate.Exceptions;
using Persistencia.Utilitarios;
using System.Web.Caching;

namespace Persistencia.Fabrica
{
    public sealed class NHibernateSessionManager
    {
        #region Thread-safe, lazy Singleton

        public static NHibernateSessionManager Instance
        {
            get
            {
                return Nested.NHibernateSessionManager;
            }
        }

        private NHibernateSessionManager()
        {
        }

        private class Nested
        {
            static Nested() { }
            internal static readonly NHibernateSessionManager NHibernateSessionManager = new NHibernateSessionManager();
        }

        #endregion

        public ISession GetSession(int idConfig)
        {
            if (this.ContextSession == null)
            {
                if ((ISessionFactory)HttpRuntime.Cache.Get(idConfig.ToString()) == null)
                {
                    SessionFactoryNHibernate SF = new SessionFactoryNHibernate();
                    ISessionFactory i = SF.GetSessionFactory(idConfig);
                    HttpRuntime.Cache.Add(idConfig.ToString(), i, null, DateTime.Now.AddDays(7), TimeSpan.Zero, CacheItemPriority.High, null);

                    i.Close();
                    i.Dispose();
                }

                this.ContextSession = ((ISessionFactory)HttpRuntime.Cache.Get(idConfig.ToString())).OpenSession();
            }
            return this.ContextSession;
        }

        public void CloseSession()
        {
            ISession session = ContextSession;

            if (session != null && session.IsOpen)
            {
                if (this.IndiceTransacao == 0)
                {
                    session.Close();
                    session.Dispose();
                    ContextSession.Dispose();
                    ContextSession = null;
                }
            }
        }

        public void DestacarSessao()
        {
            ISession session = ContextSession;

            if (session != null && session.IsOpen)
                session.Clear();
        }

        public void BeginTransaction(int idConfig)
        {
            if (idConfig < 0)
                throw new ArgumentException("Id não pode ser negativo");
            try
            {
                if (this.ContextTransaction == null)
                {
                    this.IndiceTransacao = 1;
                    this.ContextTransaction = this.GetSession(idConfig).BeginTransaction();
                }
                else
                {
                    this.IndiceTransacao++;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CommitTransaction()
        {
            try
            {
                if (HasOpenTransaction())
                {
                    if (this.IndiceTransacao > 1)
                        this.IndiceTransacao--;
                    else
                    {
                        this.IndiceTransacao = 0;
                        this.ContextTransaction.Commit();

                        this.ContextTransaction.Dispose();
                        this.ContextTransaction = null;
                    }
                }
            }
            catch (Exception ex)
            {
                RollbackTransaction();
                throw ex;
            }
        }

        public bool HasOpenTransaction()
        {
            return this.ContextTransaction != null && !this.ContextTransaction.WasCommitted && !this.ContextTransaction.WasRolledBack;
        }

        public void RollbackTransaction()
        {
            ITransaction transaction = ContextTransaction;
            try
            {
                if (HasOpenTransaction())
                {
                    if (this.IndiceTransacao > 1)
                        this.IndiceTransacao--;
                    else
                        this.IndiceTransacao = 0;
                    transaction.Rollback();
                }
                if (ContextTransaction != null)
                    ContextTransaction.Dispose();
                ContextTransaction = null;
            }
            finally
            {
                this.CloseSession();
            }
        }

        private ITransaction ContextTransaction
        {
            get
            {
                return (ITransaction)(HttpContext.Current.Items[TRANSACTION_KEY]);
            }
            set
            {
                if (PersistenciaUtil.IsInWebContext)
                {
                    HttpContext.Current.Items[TRANSACTION_KEY] = value;
                }

            }
        }

        public ISession ContextSession
        {
            get
            {
                return (ISession)(HttpContext.Current.Items[SESSION_KEY]);
            }
            set
            {
                if (PersistenciaUtil.IsInWebContext)
                {
                    HttpContext.Current.Items[SESSION_KEY] = value;
                }
            }
        }

        private int IndiceTransacao
        {
            get
            {
                return 0;
            }
            set { }
        }

        private const string TRANSACTION_KEY = "CONTEXT_TRANSACTION";
        private const string SESSION_KEY = "CONTEXT_SESSION";
        private const string INDICE_Transacao = "TRANSACTION_INDEX";

    }
}
