using NHibernate.Mapping.Attributes;
using Persistencia.Fabrica;
using Persistencia.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modelo
{
    [Serializable]
    [Class(Name = "Modelo.LogEventosOS, Modelo", Table = "logs_eventos_os")]
    public partial class LogEventosOS : ObjetoBase
    {
        private DateTime data;
        private String tipoLog;
        private String usuario;
        private String os;
        private String descricao;

        [Property(Column = "data")]
        public virtual DateTime Data { get => data; set => data = value; }
        [Property(Column = "tipo_log")]
        public virtual string TipoLog { get => tipoLog; set => tipoLog = value; }
        [Property(Column = "usuario")]
        public virtual string Usuario { get => usuario; set => usuario = value; }
        [Property(Column = "os")]
        public virtual string Os { get => os; set => os = value; }
        [Property(Column = "descricao")]
        public virtual string Descricao { get => descricao; set => descricao = value; }

        public static LogEventosOS SalvarLog(Funcionario func, String tipoLog, String os, String descricao)
        {
            LogEventosOS aux = new LogEventosOS();
            aux.data = DateTime.Now;
            aux.tipoLog = tipoLog;
            aux.usuario = (func != null ? func.NomeRazaoSocial : "N/I");
            aux.os = os;
            return aux.adicionarLog(descricao);
        }

        public virtual LogEventosOS Salvar()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().Salvar<LogEventosOS>(this);
        }

        public virtual LogEventosOS adicionarLog(String log)
        {
            this.Descricao = this.Descricao += (this.Usuario + " (" + DateTime.Now.ToString("dd/MM/yyyy HH:mm") + ") - " + log + "<br />");
            return this.Salvar();
        }
    }
}
