using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping.Attributes;
using Persistencia.Modelo;

namespace Modelo
{
    [Serializable]
    [Class(Table = "tipos_ocorrencias_veiculos", Name = "Modelo.TipoOcorrenciaVeiculo, Modelo")]
    public partial class TipoOcorrenciaVeiculo: ObjetoBase
    {
        public TipoOcorrenciaVeiculo(int id) { this.Id = id; }
        public TipoOcorrenciaVeiculo(Object id) { this.Id = Convert.ToInt32("0" + id.ToString()); }
        public TipoOcorrenciaVeiculo() { }

        #region ___________ Atributos ___________

        private string descricao;        
        private IList<Ocorrencia> ocorrencias;

        #endregion

        #region _________ Propriedades __________

        [Property(Column = "descricao")]
        public virtual string Descricao
        {
            get { return descricao; }
            set { descricao = value; }
        }

        [Bag(Name = "Ocorrencias", Table = "ocorrencias", Cascade = "delete")]
        [Key(2, Column = "id_tipo_ocorrencia_veiculo")]
        [OneToMany(3, Class = "Modelo.Ocorrencia, Modelo")]
        public virtual IList<Ocorrencia> Ocorrencias
        {
            get { return ocorrencias; }
            set { ocorrencias = value; }
        }

        #endregion

    }
}
