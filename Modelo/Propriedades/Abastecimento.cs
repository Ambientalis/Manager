using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping.Attributes;
using Persistencia.Modelo;

namespace Modelo
{
    [Serializable]
    [Class(Name = "Modelo.Abastecimento, Modelo", Table = "abastecimentos")]
    public partial class Abastecimento : ObjetoBase
    {
        #region ________ Construtores ________

        public Abastecimento(int id) { this.Id = id; }
        public Abastecimento(Object id) { this.Id = Convert.ToInt32("0" + id.ToString()); }
        public Abastecimento() { }

        #endregion

        #region ________ Atributos ________

        private DateTime data;
        private decimal qtdLitros = 0;
        private decimal quilometragemGeral = 0;
        private decimal valorUnitario = 0;
        private decimal valorTotal = 0;
        private ControleViagem controleViagem;

        #endregion

        #region ________ Propriedades ________

        [Property(Column = "data")]
        public virtual DateTime Data
        {
            get { return data; }
            set { data = value; }
        }

        [Property(Column = "qtd_litros")]
        public virtual decimal QtdLitros
        {
            get { return qtdLitros; }
            set { qtdLitros = value; }
        }

        [Property(Column = "quilometragem_geral")]
        public virtual decimal QuilometragemGeral
        {
            get { return quilometragemGeral; }
            set { quilometragemGeral = value; }
        }

        [Property(Column = "valor_unitario")]
        public virtual decimal ValorUnitario
        {
            get { return valorUnitario; }
            set { valorUnitario = value; }
        }

        [Property(Column = "valor_total")]
        public virtual decimal ValorTotal
        {
            get { return valorTotal; }
            set { valorTotal = value; }
        }

        [ManyToOne(Name = "ControleViagem", Column = "id_controle_viagem", Class = "Modelo.ControleViagem, Modelo")]
        public virtual ControleViagem ControleViagem
        {
            get { return controleViagem; }
            set { controleViagem = value; }
        }

        #endregion
    }
}
