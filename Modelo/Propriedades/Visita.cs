using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping.Attributes;
using Persistencia.Modelo;

namespace Modelo
{
    [Serializable]
    [Class(Name = "Modelo.Visita, Modelo", Table = "visitas")]
    public partial class Visita : ObjetoBase
    {
        public Visita(int id) { this.Id = id; }
        public Visita(Object id) { this.Id = Convert.ToInt32("0" + id.ToString()); }
        public Visita() { }

        #region ___________ Atributos ___________

        private DateTime dataInicio;
        private DateTime dataFim;
        private string descricao;
        private Funcionario visitante;
        private TipoVisita tipoVisita;
        private Reserva reserva;
        private OrdemServico ordemServico;
        private IList<Detalhamento> detalhamentos;
        private IList<Arquivo> arquivos;
        private bool aceita;
        private string emailAceitou;
        private string motivoNaoAceite;

        #endregion

        #region _________ Propriedades __________

        [Property(Column = "data_inicio")]
        public virtual DateTime DataInicio
        {
            get { return dataInicio; }
            set { dataInicio = value; }
        }

        [Property(Column = "data_fim")]
        public virtual DateTime DataFim
        {
            get { return dataFim; }
            set { dataFim = value; }
        }

        [Property(Type = "StringClob")]
        [Column(1, Name = "descricao", SqlType = "text")]
        public virtual string Descricao
        {
            get { return descricao; }
            set { descricao = value; }
        }

        [ManyToOne(Name = "Visitante", Class = "Modelo.Funcionario, Modelo", Column = "id_visitante")]
        public virtual Funcionario Visitante
        {
            get { return visitante; }
            set { visitante = value; }
        }

        [ManyToOne(Name = "TipoVisita", Class = "Modelo.TipoVisita, Modelo", Column = "id_tipo_visita")]
        public virtual TipoVisita TipoVisita
        {
            get { return tipoVisita; }
            set { tipoVisita = value; }
        }

        [OneToOne(PropertyRef = "Visita", Class = "Modelo.Reserva, Modelo", Cascade="delete")]
        public virtual Reserva Reserva
        {
            get { return reserva; }
            set { reserva = value; }
        }

        [ManyToOne(Name = "OrdemServico", Class = "Modelo.OrdemServico, Modelo", Column = "id_ordem_servico")]
        public virtual OrdemServico OrdemServico
        {
            get { return ordemServico; }
            set { ordemServico = value; }
        }

        [Bag(Name = "Detalhamentos", Table = "detalhamentos", Cascade = "delete")]
        [Key(2, Column = "id_visita")]
        [OneToMany(3, Class = "Modelo.Detalhamento, Modelo")]
        public virtual IList<Detalhamento> Detalhamentos
        {
            get { return detalhamentos; }
            set { detalhamentos = value; }
        }

        [Bag(Name = "Arquivos", Table = "arquivos", Cascade = "delete")]
        [Key(2, Column = "id_visita")]
        [OneToMany(3, Class = "Modelo.Arquivo, Modelo")]
        public virtual IList<Arquivo> Arquivos
        {
            get { return arquivos; }
            set { arquivos = value; }
        }

        [Property(Column = "aceita", Type = "TrueFalse")]
        public virtual bool Aceita
        {
            get { return aceita; }
            set { aceita = value; }
        }

        [Property(Column = "email_aceitou")]
        public virtual string EmailAceitou
        {
            get { return emailAceitou; }
            set { emailAceitou = value; }
        }

        [Property(Type = "StringClob")]
        [Column(1, Name = "motivo_nao_aceite", SqlType = "text")]
        public virtual string MotivoNaoAceite
        {
            get { return motivoNaoAceite; }
            set { motivoNaoAceite = value; }
        }

        #endregion
    }
}
