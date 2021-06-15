using NHibernate.Mapping.Attributes;
using Persistencia.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modelo
{
    [Serializable]
    [Class(Name = "Modelo.FormaDePagamento, Modelo", Table = "formas_de_pagamento")]
    public partial class FormaDePagamento : ObjetoBase
    {
        public const String BOLETO = "Boleto";
        public const String CARTAO = "Cartão";
        public const String DINHEIRO = "Dinheiro";
        public const String CHEQUE = "Cheque";
        public const String DEPOSITOEMCONTA = "Depósito em conta corrente";
        public const String DEBITOEMCONTA = "Débito em conta corrente";
        public const String DEPOSITOCONTASALARIO = "Depósito em conta salário";

        public FormaDePagamento(int id) { this.Id = id; }
        public FormaDePagamento(Object id) { this.Id = Convert.ToInt32("0" + id.ToString()); }
        public FormaDePagamento() { }

        #region ___________ Atributos ___________

        private int prazoPrimeiroPagamento;
        private int qtdVezes;
        private decimal acrescimoDesconto;
        private string tipo = FormaDePagamento.BOLETO;

        private IList<Orcamento> orcamentosComoFormaSelecionada;
        private IList<Orcamento> orcamentosComoFormaDisponivel;

        #endregion

        #region _________ Propriedades __________

        [Property(Column = "prazo_primeiro_pagamento")]
        public virtual int PrazoPrimeiroPagamento
        {
            get { return prazoPrimeiroPagamento; }
            set { prazoPrimeiroPagamento = value; }
        }

        [Property(Column = "qtd_vezes")]
        public virtual int QtdVezes
        {
            get { return qtdVezes; }
            set { qtdVezes = value; }
        }

        [Property(Column = "acrescimo_desconto")]
        public virtual decimal AcrescimoDesconto
        {
            get { return acrescimoDesconto; }
            set { acrescimoDesconto = value; }
        }

        [Property(Column = "tipo")]
        public virtual string Tipo
        {
            get { return tipo; }
            set { tipo = value; }
        }

        [Bag(Name = "OrcamentosComoFormaSelecionada", Table = "orcamentos")]
        [Key(2, Column = "id_forma_pagamento_selecionada")]
        [OneToMany(3, Class = "Modelo.Orcamento, Modelo")]
        public virtual IList<Orcamento> OrcamentosComoFormaSelecionada
        {
            get { return orcamentosComoFormaSelecionada; }
            set { orcamentosComoFormaSelecionada = value; }
        }

        [Bag(Table = "orcamentos_formas_de_pagamento")]
        [Key(2, Column = "formasDePagamento_id")]
        [ManyToMany(3, Class = "Modelo.Orcamento, Modelo", Column = "orcamentos_id")]
        public virtual IList<Orcamento> OrcamentosComoFormaDisponivel
        {
            get { return orcamentosComoFormaDisponivel; }
            set { orcamentosComoFormaDisponivel = value; }
        }

        #endregion
    }

}
