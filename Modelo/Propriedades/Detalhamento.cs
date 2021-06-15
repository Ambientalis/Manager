using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping.Attributes;
using Persistencia.Modelo;

namespace Modelo
{
    [Serializable]
    [Class(Table = "detalhamentos", Name = "Modelo.Detalhamento, Modelo")]
    public partial class Detalhamento: ObjetoBase
    {
        public Detalhamento(int id) { this.Id = id; }
        public Detalhamento(Object id) { this.Id = Convert.ToInt32("0" + id.ToString()); }
        public Detalhamento() { }

        #region ___________ Atributos ___________

        private string usuario;
        private DateTime dataSalvamento;
        private string conteudo;        

        #endregion

        #region _________ Propriedades __________

        [Property(Column = "usuario")]
        public virtual string Usuario
        {
            get { return usuario; }
            set { usuario = value; }
        }

        [Property(Column = "data_salvamento")]
        public virtual DateTime DataSalvamento
        {
            get { return dataSalvamento; }
            set { dataSalvamento = value; }
        }

        [Property(Type = "StringClob")]
        [Column(1, Name = "conteudo", SqlType = "text")]
        public virtual string Conteudo
        {
            get { return conteudo; }
            set { conteudo = value; }
        }

        #endregion
    }
}
