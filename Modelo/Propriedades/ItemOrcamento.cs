using NHibernate.Mapping.Attributes;
using Persistencia.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modelo
{
    [Serializable]
    [Class(Name = "Modelo.ItemOrcamento, Modelo", Table = "itens_orcamentos")]
    public partial class ItemOrcamento : ObjetoBase
    {
        public ItemOrcamento(int id) { this.Id = id; }
        public ItemOrcamento(Object id) { this.Id = Convert.ToInt32("0" + id.ToString()); }
        public ItemOrcamento() { }

        private int numero;
        private String descricao;
        private decimal quantidade;
        private decimal valorUnitario;
        private decimal desconto;
        private bool periodico;

        private Orcamento orcamento;
        private TipoOrdemServico tipo;
        private Setor setor;

        [Property(Column = "numero")]
        public virtual int Numero
        {
            get { return numero; }
            set { numero = value; }
        }

        [Property(Column = "descricao")]
        public virtual String Descricao
        {
            get { return descricao; }
            set { descricao = value; }
        }

        [Property(Column = "quantidade")]
        public virtual decimal Quantidade
        {
            get { return quantidade; }
            set { quantidade = value; }
        }

        [Property(Column = "valor_unitario")]
        public virtual decimal ValorUnitario
        {
            get { return valorUnitario; }
            set { valorUnitario = value; }
        }

        [Property(Column = "desconto")]
        public virtual decimal Desconto
        {
            get { return desconto; }
            set { desconto = value; }
        }

        [Property(Column = "periodico", Type = "TrueFalse")]
        public virtual bool Periodico
        {
            get { return periodico; }
            set { periodico = value; }
        }
        [ManyToOne(Name = "Orcamento", Column = "id_orcamento", Class = "Modelo.Orcamento, Modelo")]
        public virtual Orcamento Orcamento
        {
            get { return orcamento; }
            set { orcamento = value; }
        }

        [ManyToOne(Name = "Tipo", Column = "id_tipo", Class = "Modelo.TipoOrdemServico, Modelo")]
        public virtual TipoOrdemServico Tipo
        {
            get { return tipo; }
            set { tipo = value; }
        }

        [ManyToOne(Name = "Setor", Column = "id_setor", Class = "Modelo.Setor, Modelo")]
        public virtual Setor Setor
        {
            get { return setor; }
            set { setor = value; }
        }

    }
}
