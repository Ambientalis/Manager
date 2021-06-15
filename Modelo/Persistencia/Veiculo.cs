using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Persistencia.Modelo;
using Persistencia.Fabrica;
using Persistencia.Filtros;

namespace Modelo
{
    public partial class Veiculo : ObjetoBase
    {
        /// <summary>
        /// Consulta um objeto armazenado com o id de parametro
        /// </summary>
        /// <param name="id">O Id do objeto armazenado</param>
        /// <returns>Um objeto persistente com o id de parametro ou nulo caso não exista</returns>
        public static Veiculo ConsultarPorId(int id)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            Veiculo classe = new Veiculo();
            if (id < 0)
                throw new ArgumentException("Id não pode ser negativo.");
            else
            {
                classe.Id = id;
                return fabrica.GetDAOBase().ConsultarPorID<Veiculo>(classe);
            }
        }

        /// <summary>
        /// Consulta o corrente objeto em banco, tornando-o persistente
        /// </summary>
        public virtual Veiculo ConsultarPorId()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            if (this.Id < 0)
                throw new ArgumentException("Id não pode ser negativo.");
            else
                return fabrica.GetDAOBase().ConsultarPorID<Veiculo>(this);
        }

        /// <summary>
        /// Salva a corrente instância em banco
        /// </summary>
        public virtual Veiculo Salvar()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().Salvar<Veiculo>(this);
        }

        /// <summary>
        /// Salva a corrente instância em banco forçando o id
        /// </summary>
        public virtual Veiculo SalvarComId()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarComId<Veiculo>(this);
        }

        /// <summary>
        /// Salva uma lista de objetos, tornando-os persistentes
        /// </summary>
        /// <param name="objetos">Os objetos a serem salvos</param>
        public virtual IList<Veiculo> SalvarTodos(IList<Veiculo> objs)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarTodos<Veiculo>(objs);
        }

        /// <summary>
        /// Salva uma lista de objetos, tornando-os persistentes
        /// </summary>
        /// <param name="objetos">Os objetos a serem salvos</param>
        public virtual IList<Veiculo> SalvarTodos(params Veiculo[] objs)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarTodos<Veiculo>(objs);
        }

        /// <summary>
        /// Destaca a corrente instancia do cache do Hibernate
        /// </summary>
        public virtual bool DestacarObjeto()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().DestacarObjeto<Veiculo>(this);
        }

        /// <summary>
        /// Exclui a corrente instância do banco
        /// </summary>
        public virtual bool Excluir()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().Excluir<Veiculo>(this);
        }

        /// <summary>
        /// Retorna todos os objetos armazenados em banco deste Tipo
        /// </summary>
        /// <returns>Uma lista de objetos armazenados</returns>
        public static IList<Veiculo> ConsultarTodos()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            Veiculo obj = Activator.CreateInstance<Veiculo>();
            return fabrica.GetDAOBase().ConsultarTodos<Veiculo>(obj);
        }

        /// <summary>
        /// Método que retorna uma lista limitada de objetos em ordem ascendente do Nome
        /// </summary>
        /// <param name="qtd">A quantidade de objetos a serem retornados na lista, o Valor 0 retorna todos</param>
        /// <returns>Uma lista limitada de objetos ordenados pelo Nome</returns>
        public virtual IList<Veiculo> ConsultarOrdemAcendente(int qtd)
        {
            Veiculo ee = new Veiculo();
            ee.AdicionarFiltro(Filtros.SetOrderAsc("Codigo"));
            if (qtd > 0)
                ee.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Veiculo>(ee);
        }

        /// <summary>
        /// Método que retorna uma lista limitada de objetos em ordem descendente do Nome
        /// </summary>
        /// <param name="qtd">A quantidade de objetos a serem retornados na lista, o Valor 0 retorna todos</param>
        /// <returns>Uma lista limitada de objetos ordenados pelo Nome</returns>
        public virtual IList<Veiculo> ConsultarOrdemDescendente(int qtd)
        {
            Veiculo ee = new Veiculo();
            ee.AdicionarFiltro(Filtros.SetOrderDesc("Codigo"));
            if (qtd > 0)
                ee.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Veiculo>(ee);
        }

        /// <summary>
        /// Filtra uma certa quantidade de ProjetoImplantacao
        /// </summary>
        /// <param name="qtd">A quantidade de objetos que devem ser retornados no máximo,valores menores que 1 retornam todos os objetos</param>
        /// <returns>Uma lista com a quantidade de objetos</returns>
        public virtual IList<Veiculo> Filtrar(int qtd)
        {
            Veiculo estado = new Veiculo();
            if (qtd > 0)
                estado.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Veiculo>(estado);
        }

        /// <summary>
        /// Retorna o ultimo ProjetoImplantacao Inserido no banco de dados
        /// </summary>
        /// <returns>O ultimo ProjetoImplantacao</returns>
        public virtual Veiculo UltimoInserido()
        {
            Veiculo estado = new Veiculo();
            estado.AdicionarFiltro(Filtros.Max("Id"));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarUnicoComFiltro<Veiculo>(estado);
        }

        /// <summary>
        /// Consulta todos os ProjetoImplantacaos armazenados, ordenando-os pelo Nome
        /// </summary>
        /// <returns>A lista de todos os ProjetoImplantacaos armazenados ordenados pelo Nome</returns>
        public static IList<Veiculo> ConsultarTodosOrdemAlfabetica()
        {
            Veiculo aux = new Veiculo();
            aux.AdicionarFiltro(Filtros.SetOrderAsc("Descricao"));
            aux.AdicionarFiltro(Filtros.Distinct());
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Veiculo>(aux);
        }

        public static Veiculo ConsultarJaExistente(string placa)
        {
            Veiculo veiculo = new Veiculo();
            veiculo.AdicionarFiltro(Filtros.Eq("Placa", placa));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarUnicoComFiltro<Veiculo>(veiculo);

        }


        public static IList<Veiculo> ConsultarQueODepartamentoUsaOrdemAlfabetica(int idDepartamento)
        {
            Veiculo veiculo = new Veiculo();
            veiculo.AdicionarFiltro(Filtros.SetOrderAsc("Descricao"));
            veiculo.AdicionarFiltro(Filtros.Distinct());

            veiculo.AdicionarFiltro(Filtros.CriarAlias("DepartamentosQuePodemUtilizar", "depts"));
            veiculo.AdicionarFiltro(Filtros.Eq("depts.Id", idDepartamento));

            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Veiculo>(veiculo);
        }

        public virtual string GetNomeGestor 
        {
            get 
            {
                return this.Gestor != null ? this.Gestor.NomeRazaoSocial : "";
            }
        }

        public virtual string GetNomeDepartamentoResponsavel 
        {
            get 
            {
                return this.DepartamentoResponsavel != null ? this.DepartamentoResponsavel.Nome : "";
            }
        }        

        public virtual string GetDescDepartamentosUtilizam 
        {
            get 
            {
                string retorno = "";

                if (this.DepartamentosQuePodemUtilizar != null && this.DepartamentosQuePodemUtilizar.Count > 0) 
                {
                    foreach (Departamento departamento in this.DepartamentosQuePodemUtilizar)
                    {
                        if (departamento == this.DepartamentosQuePodemUtilizar[this.DepartamentosQuePodemUtilizar.Count - 1])
                            retorno += departamento.Nome;
                        else
                            retorno += departamento.Nome + "; ";
                    }
                }

                return retorno;
            }
        }

        public static IList<Veiculo> Filtrar(string placa, string descricao, int idGestor, string status, int idDepartamentoResponsavel)
        {
            Veiculo veiculo = new Veiculo();
            veiculo.AdicionarFiltro(Filtros.Distinct());

            if (!string.IsNullOrEmpty(placa))
                veiculo.AdicionarFiltro(Filtros.Like("Placa", placa));

            if (!string.IsNullOrEmpty(descricao))
                veiculo.AdicionarFiltro(Filtros.Like("Descricao", descricao));

            if (idGestor > 0) 
            {
                veiculo.AdicionarFiltro(Filtros.CriarAlias("Gestor", "gest"));
                veiculo.AdicionarFiltro(Filtros.Eq("gest.Id", idGestor));
            }

            if (idDepartamentoResponsavel > 0)
            {
                veiculo.AdicionarFiltro(Filtros.CriarAlias("DepartamentoResponsavel", "dept"));
                veiculo.AdicionarFiltro(Filtros.Eq("dept.Id", idDepartamentoResponsavel));
            }

            if (status != "N")
                veiculo.AdicionarFiltro(Filtros.Eq("Ativo", status == "T"));

            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Veiculo>(veiculo);
        }
    }
}
