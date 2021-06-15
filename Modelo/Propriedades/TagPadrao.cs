using NHibernate.Mapping.Attributes;
using Persistencia.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modelo
{
    [Serializable]
    [Class(Table = "tags_padroes", Name = "Modelo.TagPadrao, Modelo")]
    public partial class TagPadrao : ObjetoBase
    {
        #region ________ Construtores ________

        public TagPadrao(int id) { this.Id = id; }
        public TagPadrao(Object id) { this.Id = Convert.ToInt32("0" + id.ToString()); }
        public TagPadrao() { }

        #endregion

        #region ___________Atributos_____________

        private String nome;
        private String metodo;
        private String descricao;
        private int tipo;

        #endregion

        #region ___________Propriedades_____________

        [Property(Column = "nome")]
        public virtual String Nome
        {
            get { return nome; }
            set { nome = value; }
        }

        [Property(Column = "metodo")]
        public virtual String Metodo
        {
            get { return metodo; }
            set { metodo = value; }
        }

        [Property(Column = "descricao")]
        public virtual String Descricao
        {
            get { return descricao; }
            set { descricao = value; }
        }

        [Property(Column = "tipo")]
        public virtual int Tipo
        {
            get { return tipo; }
            set { tipo = value; }
        }

        #endregion
    }
}
