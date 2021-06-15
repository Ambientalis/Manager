using NHibernate.Mapping.Attributes;
using Persistencia.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modelo
{
    [Serializable]
    [Class(Name = "Modelo.Fatura, Modelo", Table = "faturas")]
    public partial class Fatura : ObjetoBase
    {
        public const string ENTRADA = "Entrada";
        public const string SAIDA = "Saída";

        public Fatura(int id) { this.Id = id; }
        public Fatura(Object id) { this.Id = Convert.ToInt32("0" + id.ToString()); }
        public Fatura() { }

        #region ___________ Atributos ___________

        private String numero;
        private DateTime dataEmissao;
        private DateTime dataVencimentoOriginal;
        private DateTime dataVencimento;
        private DateTime dataLimitePagamento;
        private DateTime dataPagamento;
        private decimal valor;
        private decimal valorPago;
        private decimal valorLiquidoCreditado;
        private decimal multas;
        private decimal taxas;
        private decimal acrescimoDesconto;
        private int qtdMaximaParcelas = 0;
        private String formaPagamentoEscolhida;
        private int qtdParcelasEscolhida = 0;
        private String tipo = Fatura.ENTRADA;
        private bool transferencia;
        private String nota;

        #endregion

        #region _________ Propriedades __________

        [Property(Column = "numero")]
        public virtual String Numero
        {
            get { return numero; }
            set { numero = value; }
        }

        [Property(Column = "data_emissao")]
        public virtual DateTime DataEmissao
        {
            get { return dataEmissao; }
            set { dataEmissao = value; }
        }

        [Property(Column = "data_vencimento_original")]
        public virtual DateTime DataVencimentoOriginal
        {
            get { return dataVencimentoOriginal; }
            set { dataVencimentoOriginal = value; }
        }

        [Property(Column = "data_vencimento")]
        public virtual DateTime DataVencimento
        {
            get { return dataVencimento; }
            set { dataVencimento = value; }
        }

        [Property(Column = "data_limite_pagamento")]
        public virtual DateTime DataLimitePagamento
        {
            get { return dataLimitePagamento; }
            set { dataLimitePagamento = value; }
        }

        [Property(Column = "data_pagamento")]
        public virtual DateTime DataPagamento
        {
            get { return dataPagamento; }
            set { dataPagamento = value; }
        }

        [Property(Column = "valor")]
        public virtual decimal Valor
        {
            get { return valor; }
            set { valor = value; }
        }

        [Property(Column = "valor_pago")]
        public virtual decimal ValorPago
        {
            get { return valorPago; }
            set { valorPago = value; }
        }

        [Property(Column = "valor_liquido_creditado")]
        public virtual decimal ValorLiquidoCreditado
        {
            get { return valorLiquidoCreditado; }
            set { valorLiquidoCreditado = value; }
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
        [Property(Column = "acrescimo_desconto")]
        public virtual decimal AcrescimoDesconto
        {
            get { return acrescimoDesconto; }
            set { acrescimoDesconto = value; }
        }
        [Property(Column = "qtd_maxima_parcelas")]
        public virtual int QtdMaximaParcelas
        {
            get { return qtdMaximaParcelas; }
            set { qtdMaximaParcelas = value; }
        }

        [Property(Column = "forma_pagamento_escolhida")]
        public virtual String FormaPagamentoEscolhida
        {
            get { return formaPagamentoEscolhida; }
            set { formaPagamentoEscolhida = value; }
        }

        [Property(Column = "qtd_parcelas_escolhida")]
        public virtual int QtdParcelasEscolhida
        {
            get { return qtdParcelasEscolhida; }
            set { qtdParcelasEscolhida = value; }
        }

        [Property(Column = "tipo")]
        public virtual String Tipo
        {
            get { return tipo; }
            set { tipo = value; }
        }
        
        [Property(Column = "transferencia", Type = "TrueFalse")]
        public virtual bool Transferencia
        {
            get { return transferencia; }
            set { transferencia = value; }
        }

        [Property(Column = "nota")]
        public virtual String Nota
        {
            get { return nota; }
            set { nota = value; }
        }

        #endregion
    }
}
