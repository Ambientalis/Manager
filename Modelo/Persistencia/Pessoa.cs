using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Persistencia.Modelo;
using Persistencia.Fabrica;
using Persistencia.Filtros;
using Modelo;

namespace Modelo
{
    public partial class Pessoa : ObjetoBase
    {
        /// <summary>
        /// Salva a corrente instância em banco
        /// </summary>
        public virtual Pessoa Salvar()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().Salvar<Pessoa>(this);
        }

        /// <summary>
        /// Salva a corrente instância em banco forçando o id
        /// </summary>
        public virtual Pessoa SalvarComId()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarComId<Pessoa>(this);
        }

        /// <summary>
        /// Salva uma lista de objetos, tornando-os persistentes
        /// </summary>
        /// <param name="objetos">Os objetos a serem salvos</param>
        public virtual IList<Pessoa> SalvarTodos(IList<Pessoa> objs)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarTodos<Pessoa>(objs);
        }

        /// <summary>
        /// Salva uma lista de objetos, tornando-os persistentes
        /// </summary>
        /// <param name="objetos">Os objetos a serem salvos</param>
        public virtual IList<Pessoa> SalvarTodos(params Pessoa[] objs)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarTodos<Pessoa>(objs);
        }

        /// <summary>
        /// Destaca a corrente instancia do cache do Hibernate
        /// </summary>
        public virtual bool DestacarObjeto()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().DestacarObjeto<Pessoa>(this);
        }

        /// <summary>
        /// Exclui a corrente instância do banco
        /// </summary>
        public virtual bool Excluir()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().Excluir<Pessoa>(this);
        }

        /// <summary>
        /// Retorna todos os objetos armazenados em banco deste Tipo
        /// </summary>
        /// <returns>Uma lista de objetos armazenados</returns>
        public static IList<Pessoa> ConsultarTodos()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            Pessoa obj = Activator.CreateInstance<Pessoa>();
            return fabrica.GetDAOBase().ConsultarTodos<Pessoa>(obj);
        }

        /// <summary>
        /// Filtra uma certa quantidade de Pessoa
        /// </summary>
        /// <param name="qtd">A quantidade de objetos que devem ser retornados no máximo,valores menores que 1 retornam todos os objetos</param>
        /// <returns>Uma lista com a quantidade de objetos</returns>
        public virtual IList<Pessoa> Filtrar(int qtd)
        {
            Pessoa estado = new Pessoa();
            if (qtd > 0)
                estado.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Pessoa>(estado);
        }

        /// <summary>
        /// Retorna o ultimo Pessoa Inserido no banco de dados
        /// </summary>
        /// <returns>O ultimo Pessoa</returns>
        public virtual Pessoa UltimoInserido()
        {
            Pessoa estado = new Pessoa();
            estado.AdicionarFiltro(Filtros.Max("Id"));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarUnicoComFiltro<Pessoa>(estado);
        }

        public virtual Endereco GetEnderecoPrincipal
        {
            get
            {
                Endereco retorno = null;
                if (this.Enderecos != null && this.Enderecos.Count > 0)
                    retorno = this.Enderecos[0];
                return retorno;
            }
        }

        public virtual string GetCPFCNPJComMascara
        {
            get
            {
                if (this.CpfCnpj.Length > 11)
                {
                    return this.CpfCnpj.Substring(0, 2) + "." + this.CpfCnpj.Substring(2, 3) + "." + this.CpfCnpj.Substring(5, 3) + "/" + this.CpfCnpj.Substring(8, 4) + "-" + this.CpfCnpj.Substring(12, 2);
                }
                else
                {
                    return this.CpfCnpj.Substring(0, 3) + "." + this.CpfCnpj.Substring(3, 3) + "." + this.CpfCnpj.Substring(6, 3) + "-" + this.CpfCnpj.Substring(9, 2);
                }
                return "";
            }
        }

        public virtual string GetDescricaoUnidade
        {
            get
            {
                if (this.Emp == 0)
                    return "Todas";
                Unidade uni = new Unidade(this.Emp).ConsultarPorId();
                return (uni != null ? uni.Descricao : null);
            }
        }

        public virtual string GetDescricaoOrigem
        {
            get
            {
                return this.Origem != null ? this.Origem.Nome : string.Empty;
            }
        }
    }
}
