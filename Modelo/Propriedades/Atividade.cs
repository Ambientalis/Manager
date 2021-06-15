using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping.Attributes;
using Persistencia.Modelo;

namespace Modelo
{
    [Serializable]
    [Class(Name = "Modelo.Atividade, Modelo", Table = "atividades")]
    public partial class Atividade: ObjetoBase
    {
        public Atividade(int id) { this.Id = id; }
        public Atividade(Object id) { this.Id = Convert.ToInt32("0" + id.ToString()); }
        public Atividade() { }

        #region ___________ Atributos ___________

        private DateTime data;
        private string descricao;
        private Funcionario executor;
        private TipoAtividade tipoAtividade;
        private OrdemServico ordemServico;
        private IList<Arquivo> arquivos;
        private IList<Detalhamento> detalhamentos;
        
        #endregion

        #region _________ Propriedades __________

        [Property(Column = "data")]
        public virtual DateTime Data
        {
            get { return data; }
            set { data = value; }
        }

        [Property(Type = "StringClob")]
        [Column(1, Name = "descricao", SqlType = "text")]
        public virtual string Descricao
        {
            get { return descricao; }
            set { descricao = value; }
        }

        [ManyToOne(Name = "Executor", Class = "Modelo.Funcionario, Modelo", Column = "id_executor")]
        public virtual Funcionario Executor
        {
            get { return executor; }
            set { executor = value; }
        }

        [ManyToOne(Name = "TipoAtividade", Class = "Modelo.TipoAtividade, Modelo", Column = "id_tipo_atividade")]
        public virtual TipoAtividade TipoAtividade
        {
            get { return tipoAtividade; }
            set { tipoAtividade = value; }
        }

        [ManyToOne(Name = "OrdemServico", Class = "Modelo.OrdemServico, Modelo", Column = "id_ordem_servico")]
        public virtual OrdemServico OrdemServico
        {
            get { return ordemServico; }
            set { ordemServico = value; }
        }

        [Bag(Name = "Arquivos", Table = "arquivos", Cascade = "delete")]
        [Key(2, Column = "id_atividade")]
        [OneToMany(3, Class = "Modelo.Arquivo, Modelo")]
        public virtual IList<Arquivo> Arquivos
        {
            get { return arquivos; }
            set { arquivos = value; }
        }

        [Bag(Name = "Detalhamentos", Table = "detalhamentos", Cascade = "delete")]
        [Key(2, Column = "id_atividade")]
        [OneToMany(3, Class = "Modelo.Detalhamento, Modelo")]
        public virtual IList<Detalhamento> Detalhamentos
        {
            get { return detalhamentos; }
            set { detalhamentos = value; }
        }

        #endregion

    }
}
