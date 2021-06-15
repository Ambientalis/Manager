using NHibernate.Mapping.Attributes;
using Persistencia.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modelo
{
    [Serializable]
    [Class(Name = "Modelo.MovimentacaoFinanceira, Modelo", Table = "movimentacoes_financeiras")]
    [Discriminator(3, Column = "tipo_movimentacao", Type = "String")]
    public partial class MovimentacaoFinanceira : ObjetoBase
    {
        public const int ENTRADA = 1;
        public const int SAIDA = 2;

        public MovimentacaoFinanceira(int id) { this.Id = id; }
        public MovimentacaoFinanceira(Object id) { this.Id = Convert.ToInt32("0" + id.ToString()); }
        public MovimentacaoFinanceira() { }

        #region ___________ Atributos ___________

        private String descricao;
        private DateTime data;
        private decimal valorNominal;
        private decimal desconto;
        private decimal acrescimo;
        private int tipo = MovimentacaoFinanceira.ENTRADA;
        private String observacoes;
        private int chavePeriodicidade;
        private bool xMark = false;

        private Cliente clienteFornecedor;
        private Pedido pedido;
        private IList<Parcela> parcelas;
        private Setor setor;

        #endregion

        #region _________ Propriedades __________

        [Property(Column = "descricao")]
        public virtual String Descricao
        {
            get { return descricao; }
            set { descricao = value; }
        }
        [Property(Column = "data")]
        public virtual DateTime Data
        {
            get { return data; }
            set { data = value; }
        }
        [Property(Column = "valor_nominal")]
        public virtual decimal ValorNominal
        {
            get { return valorNominal; }
            set { valorNominal = value; }
        }
        [Property(Column = "desconto")]
        public virtual decimal Desconto
        {
            get { return desconto; }
            set { desconto = value; }
        }
        [Property(Column = "acrescimo")]
        public virtual decimal Acrescimo
        {
            get { return acrescimo; }
            set { acrescimo = value; }
        }
        [Property(Column = "tipo")]
        public virtual int Tipo
        {
            get { return tipo; }
            set { tipo = value; }
        }
        [Property(Column = "observacoes")]
        public virtual String Observacoes
        {
            get { return observacoes; }
            set { observacoes = value; }
        }
        [Property(Column = "chave_periodicidade")]
        public virtual int ChavePeriodicidade
        {
            get { return chavePeriodicidade; }
            set { chavePeriodicidade = value; }
        }

        [Property(Column = "x_mark", Type = "TrueFalse")]
        public virtual bool XMark
        {
            get { return xMark; }
            set { xMark = value; }
        }


        [ManyToOne(Name = "ClienteFornecedor", Class = "Modelo.Cliente, Modelo", Column = "id_cliente_fornecedor")]
        public virtual Cliente ClienteFornecedor
        {
            get { return clienteFornecedor; }
            set { clienteFornecedor = value; }
        }

        [ManyToOne(Name = "Pedido", Class = "Modelo.Pedido, Modelo", Column = "id_pedido")]
        public virtual Pedido Pedido
        {
            get { return pedido; }
            set { pedido = value; }
        }

        [Bag(Name = "Parcelas", Table = "parcelas")]
        [Key(2, Column = "id_movimentacao_financeira")]
        [OneToMany(3, Class = "Modelo.Parcela, Modelo")]
        public virtual IList<Parcela> Parcelas
        {
            get { return parcelas; }
            set { parcelas = value; }
        }

        [ManyToOne(Name = "Setor", Class = "Modelo.Setor, Modelo", Column = "id_setor")]
        public virtual Setor Setor
        {
            get { return setor; }
            set { setor = value; }
        }

        #endregion
    }
}
