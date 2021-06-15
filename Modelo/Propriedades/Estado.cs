using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping.Attributes;
using Persistencia.Modelo;

namespace Modelo
{
    [Serializable]
    [Class(Table = "estados", Name = "Modelo.Estado, Modelo")]
    public partial class Estado : ObjetoBase
    {
        #region ________ Construtores ________

        public Estado(int id) { this.Id = id; }
        public Estado(Object id) { this.Id = Convert.ToInt32("0" + id.ToString()); }
        public Estado() { }

        #endregion

        #region ________ Atributos ___________

        private string nome;
        private string sigla;
        private IList<Cidade> cidades;

        #endregion

        #region ________ Propriedades ________

        [Property(Column = "nome")]
        public virtual string Nome
        {
            get { return nome; }
            set { nome = value; }
        }

        [Property(Column = "sigla", Length = 2)]
        public virtual string Sigla
        {
            get { return sigla; }
            set { sigla = value; }
        }

        [Bag(Name = "Cidades", Table = "cidades")]
        [Cache(1, Region = "Longo", Usage = CacheUsage.NonStrictReadWrite)]
        [Key(2, Column = "id_estado")]
        [OneToMany(3, Class = "Modelo.Cidade, Modelo")]
        public virtual IList<Cidade> Cidades
        {
            get { return cidades; }
            set { cidades = value; }
        }

        #endregion

    }
}
