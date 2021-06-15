using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping.Attributes;
using Persistencia.Modelo;

namespace Modelo
{
    [Serializable]
    [Class(Name = "Modelo.Setor, Modelo", Table = "setores")]
    public partial class Setor : ObjetoBase
    {
        public Setor(int id) { this.Id = id; }
        public Setor(Object id) { this.Id = Convert.ToInt32("0" + id.ToString()); }
        public Setor() { }

        #region ___________ Atributos ___________

        private string nome;
        private IList<Funcao> funcoes;
        private Departamento departamento;
        private IList<OrdemServico> ordensServico;
        
        #endregion

        #region _________ Propriedades __________

        [Property(Column = "nome")]
        public virtual string Nome
        {
            get { return nome; }
            set { nome = value; }
        }

        [Bag(Name = "Funcoes", Table = "funcoes", Cascade = "delete")]
        [Key(2, Column = "id_setor")]
        [OneToMany(3, Class = "Modelo.Funcao, Modelo")]
        public virtual IList<Funcao> Funcoes
        {
            get { return funcoes; }
            set { funcoes = value; }
        }

        [ManyToOne(Name = "Departamento", Class = "Modelo.Departamento, Modelo", Column = "id_departamento")]
        public virtual Departamento Departamento
        {
            get { return departamento; }
            set { departamento = value; }
        }

        [Bag(Name = "OrdensServico", Table = "ordens_servico", Cascade = "delete")]
        [Key(2, Column = "id_setor")]
        [OneToMany(3, Class = "Modelo.OrdemServico, Modelo")]
        public virtual IList<OrdemServico> OrdensServico
        {
            get { return ordensServico; }
            set { ordensServico = value; }
        }

        #endregion
    }
}
