using NHibernate.Mapping.Attributes;
using Persistencia.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modelo
{
    [Serializable]
    [Class(Name = "Modelo.Orcamento, Modelo", Table = "orcamentos")]
    public partial class Orcamento : ObjetoBase
    {
        public const String NAOENVIADO = "Não enviado";
        public const String ENVIADO = "Enviado";
        public const String CANCELADO = "Cancelado";
        public const String APROVADO = "Aprovado";
        public const String RECUSADO = "Recusado";

        public Orcamento(int id) { this.Id = id; }
        public Orcamento(Object id) { this.Id = Convert.ToInt32("0" + id.ToString()); }
        public Orcamento() { }

        private String numero;
        private DateTime data;
        private DateTime dataAceite;
        private DateTime dataCancelamento;
        private String funcionarioAceite;
        private String motivoAceiteFuncionario;
        private DateTime dataEnvio;
        private bool analiseCriticaRealizada;

        private int validade;
        private String status = Orcamento.NAOENVIADO;
        private String observacoes;
        private decimal valorTotal;

        private DateTime dataRecusa;
        private String justificativaReprovacao;

        private IList<FormaDePagamento> formasDePagamento;
        private Departamento departamento;
        private Orgao orgaoResponsavel;
        private IList<ItemOrcamento> itens;
        private Funcionario diretorResponsavel;
        private IList<Arquivo> arquivos;
        private Contato contatoCliente;
        private Pedido pedido;
        private FormaDePagamento formaDePagamentoSelecionada;
        private Orcamento orcamentoPai;
        private IList<Orcamento> revisoes;
        private TipoPedido tipoPedido;

        [Property(Column = "numero")]
        public virtual String Numero
        {
            get { return numero; }
            set { numero = value; }
        }
        [Property(Column = "data")]
        public virtual DateTime Data
        {
            get { return data; }
            set { data = value; }
        }
        [Property(Column = "data_aceite")]
        public virtual DateTime DataAceite
        {
            get { return dataAceite; }
            set { dataAceite = value; }
        }
        [Property(Column = "data_cancelamento")]
        public virtual DateTime DataCancelamento
        {
            get { return dataCancelamento; }
            set { dataCancelamento = value; }
        }
        [Property(Column = "funcionario_aceite")]
        public virtual String FuncionarioAceite
        {
            get { return funcionarioAceite; }
            set { funcionarioAceite = value; }
        }
        [Property(Column = "motivo_aceite_funcionario")]
        public virtual String MotivoAceiteFuncionario
        {
            get { return motivoAceiteFuncionario; }
            set { motivoAceiteFuncionario = value; }
        }
        [Property(Column = "data_envio")]
        public virtual DateTime DataEnvio
        {
            get { return dataEnvio; }
            set { dataEnvio = value; }
        }
        [Property(Column = "analise_critica_realizada", Type = "TrueFalse")]
        public virtual bool AnaliseCriticaRealizada
        {
            get { return analiseCriticaRealizada; }
            set { analiseCriticaRealizada = value; }
        }

        [Property(Column = "validade")]
        public virtual int Validade
        {
            get { return validade; }
            set { validade = value; }
        }
        [Property(Column = "status")]
        public virtual String Status
        {
            get { return status; }
            set { status = value; }
        }
        [Property(Type = "StringClob")]
        [Column(1, Name = "observacoes", SqlType = "text")]
        public virtual String Observacoes
        {
            get { return observacoes; }
            set { observacoes = value; }
        }
        [Property(Column = "valor_total")]
        public virtual decimal ValorTotal
        {
            get { return valorTotal; }
            set { valorTotal = value; }
        }
        [Property(Column = "data_recusa")]
        public virtual DateTime DataRecusa
        {
            get { return dataRecusa; }
            set { dataRecusa = value; }
        }
        [Property(Type = "StringClob")]
        [Column(1, Name = "justificativa_reprovacao", SqlType = "text")]
        public virtual String JustificativaReprovacao
        {
            get { return justificativaReprovacao; }
            set { justificativaReprovacao = value; }
        }

        [Bag(Table = "orcamentos_formas_de_pagamento")]
        [Key(2, Column = "orcamentos_id")]
        [ManyToMany(3, Class = "Modelo.FormaDePagamento, Modelo", Column = "formasDePagamento_id")]
        public virtual IList<FormaDePagamento> FormasDePagamento
        {
            get { return formasDePagamento; }
            set { formasDePagamento = value; }
        }

        [ManyToOne(Name = "Departamento", Column = "id_departamento", Class = "Modelo.Departamento, Modelo")]
        public virtual Departamento Departamento
        {
            get { return departamento; }
            set { departamento = value; }
        }

        [ManyToOne(Name = "OrgaoResponsavel", Column = "id_orgao_responsavel", Class = "Modelo.Orgao, Modelo")]
        public virtual Orgao OrgaoResponsavel
        {
            get { return orgaoResponsavel; }
            set { orgaoResponsavel = value; }
        }

        [Bag(Name = "Itens", Table = "itens_orcamentos")]
        [Key(2, Column = "id_orcamento")]
        [OneToMany(3, Class = "Modelo.ItemOrcamento, Modelo")]
        public virtual IList<ItemOrcamento> Itens
        {
            get { return itens; }
            set { itens = value; }
        }

        [ManyToOne(Name = "DiretorResponsavel", Column = "id_diretor_responsavel", Class = "Modelo.Funcionario, Modelo")]
        public virtual Funcionario DiretorResponsavel
        {
            get { return diretorResponsavel; }
            set { diretorResponsavel = value; }
        }

        [Bag(Name = "Arquivos", Table = "arquivos", Cascade = "delete")]
        [Key(2, Column = "id_orcamento")]
        [OneToMany(3, Class = "Modelo.Arquivo, Modelo")]
        public virtual IList<Arquivo> Arquivos
        {
            get { return arquivos; }
            set { arquivos = value; }
        }

        [ManyToOne(Name = "ContatoCliente", Column = "id_contato_cliente", Class = "Modelo.Contato, Modelo")]
        public virtual Contato ContatoCliente
        {
            get { return contatoCliente; }
            set { contatoCliente = value; }
        }

        [ManyToOne(Name = "Pedido", Column = "id_pedido", Class = "Modelo.Pedido, Modelo")]
        public virtual Pedido Pedido
        {
            get { return pedido; }
            set { pedido = value; }
        }

        [ManyToOne(Name = "FormaDePagamentoSelecionada", Column = "id_forma_pagamento_selecionada", Class = "Modelo.FormaDePagamento, Modelo")]
        public virtual FormaDePagamento FormaDePagamentoSelecionada
        {
            get { return formaDePagamentoSelecionada; }
            set { formaDePagamentoSelecionada = value; }
        }

        [ManyToOne(Name = "OrcamentoPai", Column = "id_orcamento_pai", Class = "Modelo.Orcamento, Modelo")]
        public virtual Orcamento OrcamentoPai
        {
            get { return orcamentoPai; }
            set { orcamentoPai = value; }
        }

        [Bag(Name = "Revisoes", Table = "orcamentos")]
        [Key(2, Column = "id_orcamento_pai")]
        [OneToMany(3, Class = "Modelo.Orcamento, Modelo")]
        public virtual IList<Orcamento> Revisoes
        {
            get { return revisoes; }
            set { revisoes = value; }
        }

        [ManyToOne(Name = "TipoPedido", Column = "id_tipo_pedido", Class = "Modelo.TipoPedido, Modelo")]
        public virtual TipoPedido TipoPedido
        {
            get { return tipoPedido; }
            set { tipoPedido = value; }
        }

    }
}
