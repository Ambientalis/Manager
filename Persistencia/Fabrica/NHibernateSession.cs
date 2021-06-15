using NHibernate;

namespace Persistencia.Fabrica
{
    public class NHibernateSession
    {
        private ISession sessao;
        public ISession Sessao
        {
            get { return sessao; }
            set { sessao = value; }
        }
    }
}
