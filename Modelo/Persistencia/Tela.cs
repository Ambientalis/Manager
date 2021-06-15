using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Persistencia.Modelo;
using Persistencia.Fabrica;
using Persistencia.Filtros;
using Modelo.Util;

namespace Modelo
{
    public partial class Tela : ObjetoBase
    {
        /// <summary>
        /// Consulta um objeto armazenado com o id de parametro
        /// </summary>
        /// <param name="id">O Id do objeto armazenado</param>
        /// <returns>Um objeto persistente com o id de parametro ou nulo caso não exista</returns>
        public static Tela ConsultarPorId(int id)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            Tela classe = new Tela();
            if (id < 0)
                throw new ArgumentException("Id não pode ser negativo.");
            else
            {
                classe.Id = id;
                return fabrica.GetDAOBase().ConsultarPorID<Tela>(classe);
            }
        }

        /// <summary>
        /// Consulta o corrente objeto em banco, tornando-o persistente
        /// </summary>
        public virtual Tela ConsultarPorId()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            if (this.Id < 0)
                throw new ArgumentException("Id não pode ser negativo.");
            else
                return fabrica.GetDAOBase().ConsultarPorID<Tela>(this);
        }

        /// <summary>
        /// Salva a corrente instância em banco
        /// </summary>
        public virtual Tela Salvar()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().Salvar<Tela>(this);
        }

        /// <summary>
        /// Salva a corrente instância em banco forçando o id
        /// </summary>
        public virtual Tela SalvarComId()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarComId<Tela>(this);
        }

        /// <summary>
        /// Salva uma lista de objetos, tornando-os persistentes
        /// </summary>
        /// <param name="objetos">Os objetos a serem salvos</param>
        public virtual IList<Tela> SalvarTodos(IList<Tela> objs)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarTodos<Tela>(objs);
        }

        /// <summary>
        /// Salva uma lista de objetos, tornando-os persistentes
        /// </summary>
        /// <param name="objetos">Os objetos a serem salvos</param>
        public virtual IList<Tela> SalvarTodos(params Tela[] objs)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarTodos<Tela>(objs);
        }

        /// <summary>
        /// Destaca a corrente instancia do cache do Hibernate
        /// </summary>
        public virtual bool DestacarObjeto()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().DestacarObjeto<Tela>(this);
        }

        /// <summary>
        /// Exclui a corrente instância do banco
        /// </summary>
        public virtual bool Excluir()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().Excluir<Tela>(this);
        }

        /// <summary>
        /// Retorna todos os objetos armazenados em banco deste Tipo
        /// </summary>
        /// <returns>Uma lista de objetos armazenados</returns>
        public static IList<Tela> ConsultarTodos()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            Tela obj = Activator.CreateInstance<Tela>();
            return fabrica.GetDAOBase().ConsultarTodos<Tela>(obj);
        }

        /// <summary>
        /// Método que retorna uma lista limitada de objetos em ordem ascendente do Nome
        /// </summary>
        /// <param name="qtd">A quantidade de objetos a serem retornados na lista, o Valor 0 retorna todos</param>
        /// <returns>Uma lista limitada de objetos ordenados pelo Nome</returns>
        public virtual IList<Tela> ConsultarOrdemAcendente(int qtd)
        {
            Tela ee = new Tela();
            ee.AdicionarFiltro(Filtros.SetOrderAsc("Nome"));
            if (qtd > 0)
                ee.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Tela>(ee);
        }

        /// <summary>
        /// Método que retorna uma lista limitada de objetos em ordem descendente do Nome
        /// </summary>
        /// <param name="qtd">A quantidade de objetos a serem retornados na lista, o Valor 0 retorna todos</param>
        /// <returns>Uma lista limitada de objetos ordenados pelo Nome</returns>
        public virtual IList<Tela> ConsultarOrdemDescendente(int qtd)
        {
            Tela ee = new Tela();
            ee.AdicionarFiltro(Filtros.SetOrderDesc("Nome"));
            if (qtd > 0)
                ee.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Tela>(ee);
        }

        /// <summary>
        /// Filtra uma certa quantidade de Tela
        /// </summary>
        /// <param name="qtd">A quantidade de objetos que devem ser retornados no máximo,valores menores que 1 retornam todos os objetos</param>
        /// <returns>Uma lista com a quantidade de objetos</returns>
        public virtual IList<Tela> Filtrar(int qtd)
        {
            Tela estado = new Tela();
            if (qtd > 0)
                estado.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Tela>(estado);
        }

        /// <summary>
        /// Retorna o ultimo Tela Inserido no banco de dados
        /// </summary>
        /// <returns>O ultimo Tela</returns>
        public virtual Tela UltimoInserido()
        {
            Tela estado = new Tela();
            estado.AdicionarFiltro(Filtros.Max("Id"));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarUnicoComFiltro<Tela>(estado);
        }

        /// <summary>
        /// Consulta todos os Telas armazenados, ordenando-os pelo Nome
        /// </summary>
        /// <returns>A lista de todos os Telas armazenados ordenados pelo Nome</returns>
        public static IList<Tela> ConsultarTodosOrdemAlfabetica()
        {
            Tela aux = new Tela();
            aux.AdicionarFiltro(Filtros.SetOrderAsc("Nome"));
            aux.AdicionarFiltro(Filtros.Distinct());
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Tela>(aux);
        }

        /// <summary>
        /// Deleta todas as permissões da tela com o id passado como parametro, independente dos filtros FIL e EMP
        /// </summary>
        /// <param name="idTela">O id da tela para exclusão</param>
        public static void DeletarPermissoesTela(int idTela)
        {
            try
            {
                string sqlDelecao =
                    @"delete from permissoes where permissoes.id in(
                        select p.id from permissoes p, telas t
                        where
                            p.id_tela = t.id and
                            t.id = '" + (idTela) + @"'
                    );";
                new FabricaDAONHibernateBase().GetDAOBase().ExecutarComandoSql(sqlDelecao);
            }
            catch (Exception) { }

        }

        public static Tela ConsultarPorNome(string nome)
        {
            Tela estado = new Tela();
            estado.AdicionarFiltro(Filtros.Max("Id"));
            estado.AdicionarFiltro(Filtros.Eq("Nome", nome));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarUnicoComFiltro<Tela>(estado);
        }

        public static IList<Tela> ConsultarTodosOrdemPrioridade()
        {
            Tela aux = new Tela();
            aux.AdicionarFiltro(Filtros.SetOrderAsc("Prioridade"));
            aux.AdicionarFiltro(Filtros.Distinct());
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Tela>(aux);
        }

        public static IList<Tela> ConsultarTodasTelasOrdemPrioridade()
        {
            Tela aux = new Tela();
            aux.AdicionarFiltro(Filtros.SetOrderAsc("Prioridade"));
            aux.AdicionarFiltro(Filtros.Eq("Relatorio", false));
            aux.AdicionarFiltro(Filtros.Distinct());
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Tela>(aux);
        }

        public static IList<Tela> ConsultarTodasTelasRelatoriosOrdemPrioridade(int financeiro, int graficos)
        {
            Tela aux = new Tela();
            aux.AdicionarFiltro(Filtros.SetOrderAsc("Prioridade"));
            if (financeiro > 0)
                aux.AdicionarFiltro(Filtros.Eq("TelaFinanceiro", financeiro == 1));
            if (graficos > 0)
                aux.AdicionarFiltro(Filtros.Eq("RelatorioGrafico", graficos == 1));
            aux.AdicionarFiltro(Filtros.Eq("Relatorio", true));
            aux.AdicionarFiltro(Filtros.Distinct());
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Tela>(aux);
        }

        public static IList<Tela> ConsultarTelasDaPermissaoPorOrdemPrioridade(Permissao permissao)
        {
            Tela aux = new Tela();
            aux.AdicionarFiltro(Filtros.Distinct());
            aux.AdicionarFiltro(Filtros.SetOrderAsc("Prioridade"));
            aux.AdicionarFiltro(Filtros.Eq("Relatorio", false));
            aux.AdicionarFiltro(Filtros.CriarAlias("Permissoes", "permi"));
            aux.AdicionarFiltro(Filtros.Eq("permi.Id", permissao.Id));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Tela>(aux);
        }

        public static IList<Tela> ConsultarTelasTodosRelatorioDaPermissaoPorOrdemPrioridade(Permissao permissao)
        {
            Tela aux = new Tela();
            aux.AdicionarFiltro(Filtros.Distinct());
            aux.AdicionarFiltro(Filtros.SetOrderAsc("Prioridade"));
            aux.AdicionarFiltro(Filtros.Eq("Relatorio", true));
            aux.AdicionarFiltro(Filtros.CriarAlias("Permissoes", "permi"));
            aux.AdicionarFiltro(Filtros.Eq("permi.Id", permissao.Id));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Tela>(aux);
        }

        public static IList<Tela> ConsultarTelasRelatorioDaPermissaoPorOrdemPrioridade(Permissao permissao)
        {
            Tela aux = new Tela();
            aux.AdicionarFiltro(Filtros.Distinct());
            aux.AdicionarFiltro(Filtros.SetOrderAsc("Prioridade"));
            aux.AdicionarFiltro(Filtros.Eq("Relatorio", true));
            aux.AdicionarFiltro(Filtros.CriarAlias("Permissoes", "permi"));
            aux.AdicionarFiltro(Filtros.Eq("permi.Id", permissao.Id));
            aux.AdicionarFiltro(Filtros.Eq("RelatorioGrafico", false));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Tela>(aux);
        }

        public static IList<Tela> ConsultarTelasRelatoriosGraficosDaPermissaoPorOrdemPrioridade(Permissao permissao)
        {
            Tela aux = new Tela();
            aux.AdicionarFiltro(Filtros.Distinct());
            aux.AdicionarFiltro(Filtros.SetOrderAsc("Prioridade"));
            aux.AdicionarFiltro(Filtros.Eq("Relatorio", true));
            aux.AdicionarFiltro(Filtros.CriarAlias("Permissoes", "permi"));
            aux.AdicionarFiltro(Filtros.Eq("permi.Id", permissao.Id));
            aux.AdicionarFiltro(Filtros.Eq("RelatorioGrafico", true));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Tela>(aux);
        }

        public static bool UsuarioPossuiAlgumRelatorio(Permissao permissao)
        {
            Tela aux = new Tela();
            aux.AdicionarFiltro(Filtros.Distinct());
            aux.AdicionarFiltro(Filtros.Eq("Relatorio", true));
            aux.AdicionarFiltro(Filtros.CriarAlias("Permissoes", "permi"));
            aux.AdicionarFiltro(Filtros.Eq("permi.Id", permissao.Id));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            IList<Tela> telas = fabrica.GetDAOBase().ConsultarComFiltro<Tela>(aux);
            return telas != null && telas.Count > 0;
        }

        public static Tela ConsultarPorPath(string urlTela)
        {
            Tela aux = new Tela();
            aux.AdicionarFiltro(Filtros.Distinct());
            aux.AdicionarFiltro(Filtros.Eq("Url", urlTela));
            IList<Tela> telas = new FabricaDAONHibernateBase().GetDAOBase().ConsultarComFiltro<Tela>(aux);
            if (telas.Count > 0)
                return telas[0];
            return null;
        }

        public virtual string GetURLFormatada(Configuracoes confg, Funcionario usuario, Funcao funcao, int idConfigBanco)
        {
            if (!this.TelaFinanceiro)
                return this.url;
            else
            {
                String prefix = confg.CaminhoSistemaFinanceiro;
                if (string.IsNullOrEmpty(prefix))
                    return this.url;

                prefix = (prefix.EndsWith("/") ? prefix : prefix + "/");
                String urlAux = this.url.Replace("../", "").Replace("./", "");
                String sufix = CriptografiaSimples.Encrypt(usuario.Id + ";" + funcao.Id + ";" + idConfigBanco);

                return prefix + urlAux + "?permission=" + sufix;
            }
        }
    }
}
