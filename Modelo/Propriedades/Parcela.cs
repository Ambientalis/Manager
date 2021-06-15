using NHibernate.Mapping.Attributes;
using Persistencia.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modelo
{
    [Serializable]
    [Class(Name = "Modelo.Parcela, Modelo", Table = "parcelas")]
    public partial class Parcela : ObjetoBase
    {
        public Parcela(int id) { this.Id = id; }
        public Parcela(Object id) { this.Id = Convert.ToInt32("0" + id.ToString()); }
        public Parcela() { }

        #region ___________ Atributos ___________

        private decimal valorNominal;
        private decimal descontos;
        private decimal acrescimos;
        private DateTime dataEmissao;
        private DateTime dataVencimento;
        private DateTime dataPagamento;
        private decimal multas;
        private decimal taxas;
        private int numero;
        private int quantidade;
        private bool transferencia;
        private Caixa caixa;
        private Fatura fatura;
        private MovimentacaoFinanceira movimentacaoFinanceira;

        #endregion

        #region _________ Propriedades __________

        [Property(Column = "valor_nominal")]
        public virtual decimal ValorNominal
        {
            get { return valorNominal; }
            set { valorNominal = value; }
        }

        [Property(Column = "descontos")]
        public virtual decimal Descontos
        {
            get { return descontos; }
            set { descontos = value; }
        }

        [Property(Column = "acrescimos")]
        public virtual decimal Acrescimos
        {
            get { return acrescimos; }
            set { acrescimos = value; }
        }

        [Property(Column = "data_emissao")]
        public virtual DateTime DataEmissao
        {
            get { return dataEmissao; }
            set { dataEmissao = value; }
        }

        [Property(Column = "data_vencimento")]
        public virtual DateTime DataVencimento
        {
            get { return dataVencimento; }
            set { dataVencimento = value; }
        }

        [Property(Column = "data_pagamento")]
        public virtual DateTime DataPagamento
        {
            get { return dataPagamento; }
            set { dataPagamento = value; }
        }

        [Property(Column = "multas")]
        public virtual decimal Multas
        {
            get { return multas; }
            set { multas = value; }
        }

        [Property(Column = "taxas")]
        public virtual decimal Taxas
        {
            get { return taxas; }
            set { taxas = value; }
        }

        [Property(Column = "numero")]
        public virtual int Numero
        {
            get { return numero; }
            set { numero = value; }
        }

        [Property(Column = "quantidade")]
        public virtual int Quantidade
        {
            get { return quantidade; }
            set { quantidade = value; }
        }

        [Property(Column = "transferencia", Type = "TrueFalse")]
        public virtual bool Transferencia
        {
            get { return transferencia; }
            set { transferencia = value; }
        }

        [ManyToOne(Name = "Caixa", Class = "Modelo.Caixa, Modelo", Column = "id_caixa")]
        public virtual Caixa Caixa
        {
            get { return caixa; }
            set { caixa = value; }
        }

        [ManyToOne(Name = "Fatura", Class = "Modelo.Fatura, Modelo", Column = "id_fatura")]
        public virtual Fatura Fatura
        {
            get { return fatura; }
            set { fatura = value; }
        }

        [ManyToOne(Name = "MovimentacaoFinanceira", Class = "Modelo.MovimentacaoFinanceira, Modelo", Column = "id_movimentacao_financeira")]
        public virtual MovimentacaoFinanceira MovimentacaoFinanceira
        {
            get { return movimentacaoFinanceira; }
            set { movimentacaoFinanceira = value; }
        }

        #endregion
    }
}
