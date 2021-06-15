using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping.Attributes;
using Persistencia.Modelo;

namespace Modelo
{
    [Serializable]
    [Class(Name = "Modelo.Orgao, Modelo", Table = "orgaos")]
    public partial class Orgao: ObjetoBase
    {
        public Orgao(int id) { this.Id = id; }
        public Orgao(Object id) { this.Id = Convert.ToInt32("0" + id.ToString()); }
        public Orgao() { }

        #region ___________ Atributos ___________

        private string nome;
        private IList<OrdemServico> ordensServico;
        
        #endregion

        #region _________ Propriedades __________

        [Property(Column = "nome")]
        public virtual string Nome
        {
            get { return nome; }
            set { nome = value; }
        }

        [Bag(Name = "OrdensServico", Table = "ordens_servico", Cascade = "delete")]
        [Key(2, Column = "id_orgao")]
        [OneToMany(3, Class = "Modelo.OrdemServico, Modelo")]
        public virtual IList<OrdemServico> OrdensServico
        {
            get { return ordensServico; }
            set { ordensServico = value; }
        }

        #endregion
    }
}
