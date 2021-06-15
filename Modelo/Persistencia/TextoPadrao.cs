using Modelo.Util;
using Persistencia.Fabrica;
using Persistencia.Filtros;
using Persistencia.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modelo
{
    public partial class TextoPadrao : ObjetoBase
    {
        /// <summary>
        /// Consulta um objeto armazenado com o id de parametro
        /// </summary>
        /// <param name="id">O Id do objeto armazenado</param>
        /// <returns>Um objeto persistente com o id de parametro ou nulo caso não exista</returns>
        public static TextoPadrao ConsultarPorId(int id)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            TextoPadrao classe = new TextoPadrao();
            if (id < 0)
                throw new ArgumentException("Id não pode ser negativo.");
            else
            {
                classe.Id = id;
                return fabrica.GetDAOBase().ConsultarPorID<TextoPadrao>(classe);
            }
        }

        /// <summary>
        /// Consulta o corrente objeto em banco, tornando-o persistente
        /// </summary>
        public virtual TextoPadrao ConsultarPorId()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            if (this.Id < 0)
                throw new ArgumentException("Id não pode ser negativo.");
            else
                return fabrica.GetDAOBase().ConsultarPorID<TextoPadrao>(this);
        }

        /// <summary>
        /// Salva a corrente instância em banco
        /// </summary>
        public virtual TextoPadrao Salvar()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().Salvar<TextoPadrao>(this);
        }

        /// <summary>
        /// Salva a corrente instância em banco forçando o id
        /// </summary>
        public virtual TextoPadrao SalvarComId()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarComId<TextoPadrao>(this);
        }

        /// <summary>
        /// Salva uma lista de objetos, tornando-os persistentes
        /// </summary>
        /// <param name="objetos">Os objetos a serem salvos</param>
        public virtual IList<TextoPadrao> SalvarTodos(IList<TextoPadrao> objs)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarTodos<TextoPadrao>(objs);
        }

        /// <summary>
        /// Salva uma lista de objetos, tornando-os persistentes
        /// </summary>
        /// <param name="objetos">Os objetos a serem salvos</param>
        public virtual IList<TextoPadrao> SalvarTodos(params TextoPadrao[] objs)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarTodos<TextoPadrao>(objs);
        }

        /// <summary>
        /// Destaca a corrente instancia do cache do Hibernate
        /// </summary>
        public virtual bool DestacarObjeto()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().DestacarObjeto<TextoPadrao>(this);
        }

        /// <summary>
        /// Exclui a corrente instância do banco
        /// </summary>
        public virtual bool Excluir()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().Excluir<TextoPadrao>(this);
        }

        /// <summary>
        /// Retorna todos os objetos armazenados em banco deste Tipo
        /// </summary>
        /// <returns>Uma lista de objetos armazenados</returns>
        public static IList<TextoPadrao> ConsultarTodos()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            TextoPadrao obj = Activator.CreateInstance<TextoPadrao>();
            return fabrica.GetDAOBase().ConsultarTodos<TextoPadrao>(obj);
        }

        /// <summary>
        /// Filtra uma certa quantidade de Cidade
        /// </summary>
        /// <param name="qtd">A quantidade de objetos que devem ser retornados no máximo,valores menores que 1 retornam todos os objetos</param>
        /// <returns>Uma lista com a quantidade de objetos</returns>
        public virtual IList<TextoPadrao> Filtrar(int qtd)
        {
            TextoPadrao estado = new TextoPadrao();
            if (qtd > 0)
                estado.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<TextoPadrao>(estado);
        }

        public static TextoPadrao ConsultarPorTipo(String modelo)
        {
            TextoPadrao auxTexto = new TextoPadrao();
            auxTexto.AdicionarFiltro(Filtros.MaxResults(1));
            auxTexto.AdicionarFiltro(Filtros.Eq("Tipo", modelo));
            TextoPadrao aux = new FabricaDAONHibernateBase().GetDAOBase().ConsultarUnicoComFiltro<TextoPadrao>(auxTexto);
            if (aux != null)
                return aux;

            TextoPadrao retorno = new TextoPadrao();
            retorno.tipo = modelo;
            return retorno.Salvar();
        }

        private Configuracoes configuracaoSistema;
        private IList<TagPadrao> tags = new List<TagPadrao>();
        private PesquisaSatisfacao pesquisaSatisfacao;
        private Pedido pedido;
        private Cliente cliente;
        private Orcamento orcamento;

        public virtual TextoPadrao AtualizarVariaveis(OrdemServico os)
        {
            this.configuracaoSistema = Configuracoes.GetConfiguracoesSistema();
            this.tags = TagPadrao.ConsultarTodos();
            this.pesquisaSatisfacao = os.PesquisaSatisfacao;
            this.cliente = os.Pedido.Cliente;
            this.orcamento = os.Pedido.Orcamento;
            return this;
        }

        public virtual TextoPadrao AtualizarVariaveis(Pedido pedido)
        {
            this.configuracaoSistema = Configuracoes.GetConfiguracoesSistema();
            this.tags = TagPadrao.ConsultarTodos();
            this.pedido = pedido;
            this.orcamento = pedido.Orcamento;
            this.cliente = pedido.Cliente;
            return this;
        }

        private String AtualizarTextoPelasTags(String texto, IList<TagPadrao> tags)
        {
            if (string.IsNullOrEmpty(texto))
                return null;
            if (tags == null || tags.Count < 1)
                return texto;
            foreach (TagPadrao tag in tags)
            {
                Object aux = this.GetValorMetodo(tag.Metodo);
                texto = texto.Replace(tag.Nome, (aux != null ? aux.ToString() : ""));
            }
            return texto;
        }

        public virtual String GetTextoComSubstituicaoDeTags()
        {
            return this.AtualizarTextoPelasTags(this.texto, this.tags);
        }

        public virtual String getLinkPesquisaSatisfacao()
        {
            if (this.pesquisaSatisfacao == null || this.pesquisaSatisfacao == null)
                return string.Empty;
            return this.configuracaoSistema.CaminhoSistemaFinanceiro + "Telas/pesquisaSatisfacao.xhtml?src=" + CriptografiaSimples.Encrypt(this.pesquisaSatisfacao.Id.ToString());
        }

        public virtual String getLinkContratoPedido()
        {
            if (this.configuracaoSistema == null || this.pedido == null)
                return string.Empty;
            return this.configuracaoSistema.CaminhoSistemaFinanceiro + "Telas/aceiteContrato.xhtml?src=" + CriptografiaSimples.Encrypt(this.pedido.Id.ToString());
        }

        #region ==================== Cliente ====================

        public virtual String getCodigoCliente()
        {
            return this.cliente != null ? this.cliente.Codigo : "";
        }

        public virtual String getNomeRazaoSocialCliente()
        {
            return this.cliente != null ? this.cliente.NomeRazaoSocial : "";
        }

        public virtual String getApelidoNomeFantasiaCliente()
        {
            return this.cliente != null ? this.cliente.ApelidoNomeFantasia : "";
        }

        public virtual String getCpfCnpjCliente()
        {
            return this.cliente != null ? this.cliente.CpfCnpj : "";
        }

        public virtual String getInscricaoEstadualCliente()
        {
            return this.cliente != null ? this.cliente.InscricaoEstadual : "";
        }

        public virtual String getInscricaoMunicipalCliente()
        {
            return this.cliente != null ? this.cliente.InscricaoMunicipal : "";
        }

        public virtual String getRgCliente()
        {
            return this.cliente != null ? this.cliente.Rg : "";
        }

        public virtual String getEmissorRgCliente()
        {
            return this.cliente != null ? this.cliente.EmissorRg : "";
        }

        public virtual String getNacionalidadeCliente()
        {
            return this.cliente != null ? this.cliente.Nacionalidade : "";
        }

        public virtual String getDataNascimentoCliente()
        {
            return this.cliente != null && this.cliente.DataNascimento != null && this.cliente.DataNascimento > SqlDate.MinValue
                    ? this.cliente.DataNascimento.ToShortDateString() : "";
        }

        public virtual String getEstadoCivilCliente()
        {
            return this.cliente != null ? this.cliente.EstadoCivil : "";
        }

        public virtual String getSexoCliente()
        {
            return this.cliente != null ? (this.cliente.Sexo == 'M' ? "Masculino" : "Feminino") : "";
        }

        public virtual String getDescricaoEnderecoCliente()
        {
            return this.cliente != null ? this.cliente.GetDescricaoEndereco : "";
        }

        public virtual String getTelefoneCliente()
        {
            return this.cliente != null ? this.cliente.Telefone1 : "";
        }

        public virtual String getEmailCliente()
        {
            return this.cliente != null ? this.cliente.Email : "";
        }

        public virtual String getNomeContato1Cliente()
        {
            return this.cliente != null && this.cliente.Contatos != null && this.cliente.Contatos.Count > 0
                    ? this.cliente.Contatos[0].Nome : "";
        }

        public virtual String getFuncaoContato1Cliente()
        {
            return this.cliente != null && this.cliente.Contatos != null && this.cliente.Contatos.Count > 0
                    ? this.cliente.Contatos[0].Funcao : "";
        }

        public virtual String getTelefoneContato1Cliente()
        {
            return this.cliente != null && this.cliente.Contatos != null && this.cliente.Contatos.Count > 0
                    ? this.cliente.Contatos[0].Telefone1 : "";
        }

        public virtual String getEmailContato1Cliente()
        {
            return this.cliente != null && this.cliente.Contatos != null && this.cliente.Contatos.Count > 0
                    ? this.cliente.Contatos[0].Email : "";
        }

        public virtual String getTipoPessoaCliente()
        {
            return this.cliente != null ? this.cliente.GetDescricaoTipoPessoa : "";
        }

        #endregion

        #region ==================== Pedidos ====================

        public virtual String getCodigoPedido()
        {
            return this.pedido != null ? this.pedido.Codigo : "";
        }

        public virtual String getDataPedido()
        {
            return this.pedido != null && this.pedido.Data != null && this.pedido.Data > SqlDate.MinValue
                    ? this.pedido.Data.ToString("dd/MM/yyyy") : "";
        }

        public virtual String getDataPedidoPorExtenso()
        {
            return this.pedido != null && this.pedido.Data != null && this.pedido.Data > SqlDate.MinValue
                    ? this.pedido.Data.ToString("dd 'de' MMMM 'de' yyyy") : "";
        }

        public virtual String getDataEnvioContratoPedido()
        {
            return this.pedido != null && this.pedido.DataEnvioContrato != null && this.pedido.DataEnvioContrato > SqlDate.MinValue
                   ? this.pedido.DataEnvioContrato.ToString("dd/MM/yyyy") : "";
        }

        public virtual String getDataEnvioContratoPedidoPorExtenso()
        {
            return this.pedido != null && this.pedido.DataEnvioContrato != null && this.pedido.DataEnvioContrato > SqlDate.MinValue
                  ? this.pedido.DataEnvioContrato.ToString("dd 'de' MMMM 'de' yyyy") : "";
        }

        public virtual String getDataAceiteContratoPedido()
        {
            return this.pedido != null && this.pedido.DataAceiteContrato != null && this.pedido.DataAceiteContrato > SqlDate.MinValue
                 ? this.pedido.DataAceiteContrato.ToString("dd/MM/yyyy") : "";
        }

        public virtual String getTipoPedido()
        {
            return this.pedido != null && this.pedido.TipoPedido != null ? this.pedido.TipoPedido.Nome : "";
        }

        public virtual String getVendedorPedido()
        {
            return this.pedido != null && this.pedido.Vendedor != null ? this.pedido.Vendedor.NomeRazaoSocial : "";
        }

        public virtual String getOrdensServicoPedido()
        {
            return this.pedido != null ? this.pedido.GetDescricaoOrdensServico : "";
        }

        public virtual String getOrdensServicoPedidoSimplificada()
        {
            return this.pedido != null ? this.pedido.GetDescricaoOrdensServicoSimplificada : "";
        }

        public virtual String getDescricaoReceitaPedido()
        {
            return this.pedido != null && this.pedido.Receita != null ? this.pedido.Receita.Descricao : "";
        }

        public virtual String getValorNominalReceitaPedido()
        {
            return this.pedido != null && this.pedido.Receita != null ? String.Format("{0:c}", this.pedido.Receita.ValorNominal) : "";
        }

        public virtual String getDescontoReceitaPedido()
        {
            return this.pedido != null && this.pedido.Receita != null ? String.Format("{0:c}", this.pedido.Receita.Desconto) : "";
        }

        public virtual String getAcrescimoReceitaPedido()
        {
            return this.pedido != null && this.pedido.Receita != null ? String.Format("{0:c}", this.pedido.Receita.Acrescimo) : "";
        }

        public virtual String getValorTotalReceitaPedido()
        {
            return this.pedido != null && this.pedido.Receita != null ? String.Format("{0:c}", this.pedido.Receita.ValorTotal) : "";
        }

        public virtual String getValorReceitaPedidoPorExtenso()
        {
            return this.pedido != null && this.pedido.Receita != null ? new NumeroPorExtenso(this.pedido.Receita.ValorTotal).ToString() : "";
        }

        public virtual String getDescricaoParcelasReceitaPedido()
        {
            return this.pedido != null && this.pedido.Receita != null ? this.pedido.Receita.GetDescricaoParcelas : "";
        }

        public virtual String getSetorReceitaPedido()
        {
            return this.pedido != null && this.pedido.Receita != null && this.pedido.Receita.Setor != null ? this.pedido.Receita.Setor.Nome : "";
        }

        #endregion

        #region ==================== Orçamento ====================

        public virtual String getNumeroOrcamento()
        {
            return this.orcamento != null ? this.orcamento.Numero : "";
        }

        public virtual String getDataOrcamento()
        {
            return this.orcamento != null && this.orcamento.Data > SqlDate.MinValue ? this.orcamento.Data.ToShortDateString() : "";
        }

        public virtual String getDataOrcamentoPorExtenso()
        {
            return this.orcamento != null && this.orcamento.Data > SqlDate.MinValue ? this.orcamento.Data.ToString("dd 'de' MMMM 'de' yyyy") : "";
        }

        public virtual String getDataAceiteOrcamento()
        {
            return this.orcamento != null && this.orcamento.DataAceite > SqlDate.MinValue ? this.orcamento.DataAceite.ToShortDateString() : "";
        }

        public virtual String getDataValidadeOrcamento()
        {
            return this.orcamento != null ? this.orcamento.Data.AddDays(this.orcamento.Validade).ToShortDateString() : "";
        }

        public virtual String getStatusOrcamento()
        {
            return this.orcamento != null ? this.orcamento.Status : "";
        }

        public virtual String getValorTotalOrcamento()
        {
            return this.orcamento != null ? String.Format("{0:c}", this.orcamento.ValorTotal) : "";
        }

        public virtual String getFormasPagamentoOrcamento()
        {
            return this.orcamento != null ? this.orcamento.GetDescricaoFormasDePagamento : "";
        }

        public virtual String getDepartamentoOrcamento()
        {
            return this.orcamento != null && this.orcamento.Departamento != null ? this.orcamento.Departamento.Nome : "";
        }

        public virtual String getOrgaoResponsavelOrcamento()
        {
            return this.orcamento != null && this.orcamento.OrgaoResponsavel != null ? this.orcamento.OrgaoResponsavel.Nome : "";
        }

        public virtual String getItensOrcamento()
        {
            return this.orcamento != null ? this.orcamento.GetDescricaoItens : "";
        }

        public virtual String getDiretorResponsavelOrcamento()
        {
            return this.orcamento != null && this.orcamento.DiretorResponsavel != null ? this.orcamento.DiretorResponsavel.NomeRazaoSocial : "";
        }

        public virtual String getFormaDePagamentoSelecionadaOrcamento()
        {
            return this.orcamento != null && this.orcamento.FormaDePagamentoSelecionada != null ? this.orcamento.FormaDePagamentoSelecionada.GetDescricaoFormatada : "";
        }

        public virtual String getNomeContatoOrcamento()
        {
            return this.orcamento != null && this.orcamento.ContatoCliente != null ? this.orcamento.ContatoCliente.Nome : "";
        }

        public virtual String getFuncaoContatoOrcamento()
        {
            return this.orcamento != null && this.orcamento.ContatoCliente != null ? this.orcamento.ContatoCliente.Funcao : "";
        }

        public virtual String getTelefoneContatoOrcamento()
        {
            return this.orcamento != null && this.orcamento.ContatoCliente != null ? this.orcamento.ContatoCliente.Telefone1 : "";
        }

        public virtual String getEmailContatoOrcamento()
        {
            return this.orcamento != null && this.orcamento.ContatoCliente != null ? this.orcamento.ContatoCliente.Email : "";
        }

        public virtual String getTipoPedidoOrcamento()
        {
            return this.orcamento != null && this.orcamento.TipoPedido != null
                    ? this.orcamento.TipoPedido.Nome : "";
        }

        public virtual String getDataRecusaOrcamento()
        {
            return this.orcamento != null && this.orcamento.IsRecusado && this.orcamento.DataRecusa != null
                    ? this.orcamento.DataRecusa.ToShortDateString() : "";
        }

        public virtual String getJustificativaRecusaOrcamento()
        {
            return this.orcamento != null && this.orcamento.IsRecusado ? this.orcamento.JustificativaReprovacao : "";
        }

        public virtual String getValorOrcamentoPorExtenso()
        {
            return this.orcamento != null ? new NumeroPorExtenso(this.orcamento.ValorTotal).ToString() : "";
        }

        #endregion
    }
}
