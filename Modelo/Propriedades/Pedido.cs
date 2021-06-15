using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping.Attributes;
using Persistencia.Modelo;

namespace Modelo
{
    [Serializable]
    [Class(Table = "pedidos", Name = "Modelo.Pedido, Modelo")]
    public partial class Pedido : ObjetoBase
    {
        public Pedido(int id) { this.Id = id; }
        public Pedido(Object id) { this.Id = Convert.ToInt32("0" + id.ToString()); }
        public Pedido() { }

        #region ___________ Atributos ___________

        private string codigo;
        private DateTime data;
        private String destinatariosContrato;
        private String textoContrato;
        private DateTime dataEnvioContrato;
        private DateTime dataAceiteContrato;
        private bool contratoFixo;

        private Cliente cliente;
        private TipoPedido tipoPedido;
        private IList<Arquivo> arquivos;
        private IList<Detalhamento> detalhamentos;
        private IList<OrdemServico> ordensServico;

        private Funcionario vendedor;
        private Orcamento orcamento;
        private MovimentacaoFinanceira receita;

        #endregion

        #region _________ Propriedades __________

        [Property(Column = "codigo")]
        public virtual string Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        [Property(Column = "data")]
        public virtual DateTime Data
        {
            get { return data; }
            set { data = value; }
        }

        [Property(Column = "destinatarios_contrato")]
        public virtual String DestinatariosContrato
        {
            get { return destinatariosContrato; }
            set { destinatariosContrato = value; }
        }

        [Property]
        [Column(1, SqlType = "text", Name = "texto_contrato")]
        public virtual String TextoContrato
        {
            get { return textoContrato; }
            set { textoContrato = value; }
        }

        [Property(Column = "data_envio_contrato")]
        public virtual DateTime DataEnvioContrato
        {
            get { return dataEnvioContrato; }
            set { dataEnvioContrato = value; }
        }

        [Property(Column = "data_aceite_contrato")]
        public virtual DateTime DataAceiteContrato
        {
            get { return dataAceiteContrato; }
            set { dataAceiteContrato = value; }
        }

        [Property(Column = "contrato_fixo", Type = "TrueFalse")]
        public virtual bool ContratoFixo
        {
            get { return contratoFixo; }
            set { contratoFixo = value; }
        }

        [ManyToOne(Name = "Cliente", Class = "Modelo.Cliente, Modelo", Column = "id_cliente")]
        public virtual Cliente Cliente
        {
            get { return cliente; }
            set { cliente = value; }
        }

        [ManyToOne(Name = "TipoPedido", Class = "Modelo.TipoPedido, Modelo", Column = "id_tipo_pedido")]
        public virtual TipoPedido TipoPedido
        {
            get { return tipoPedido; }
            set { tipoPedido = value; }
        }

        [Bag(Name = "Arquivos", Table = "arquivos", Cascade = "delete")]
        [Key(2, Column = "id_pedido")]
        [OneToMany(3, Class = "Modelo.Arquivo, Modelo")]
        public virtual IList<Arquivo> Arquivos
        {
            get { return arquivos; }
            set { arquivos = value; }
        }

        [Bag(Name = "Detalhamentos", Table = "detalhamentos", Cascade = "delete")]
        [Key(2, Column = "id_pedido")]
        [OneToMany(3, Class = "Modelo.Detalhamento, Modelo")]
        public virtual IList<Detalhamento> Detalhamentos
        {
            get { return detalhamentos; }
            set { detalhamentos = value; }
        }

        [Bag(Name = "OrdensServico", Table = "ordens_servico", Cascade = "delete")]
        [Key(2, Column = "id_pedido")]
        [OneToMany(3, Class = "Modelo.OrdemServico, Modelo")]
        public virtual IList<OrdemServico> OrdensServico
        {
            get { return ordensServico; }
            set { ordensServico = value; }
        }

        [ManyToOne(Name = "Vendedor", Class = "Modelo.Funcionario, Modelo", Column = "id_vendedor")]
        public virtual Funcionario Vendedor
        {
            get { return vendedor; }
            set { vendedor = value; }
        }

        [OneToOne(PropertyRef = "Pedido", Class = "Modelo.Orcamento, Modelo")]
        public virtual Orcamento Orcamento
        {
            get { return orcamento; }
            set { orcamento = value; }
        }

        [OneToOne(PropertyRef = "Pedido", Class = "Modelo.MovimentacaoFinanceira, Modelo")]
        public virtual MovimentacaoFinanceira Receita
        {
            get { return receita; }
            set { receita = value; }
        }

        #endregion
    }
}
