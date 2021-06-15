using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Persistencia.Modelo;
using Persistencia.Fabrica;
using Persistencia.Filtros;

namespace Modelo
{
    public partial class Menu : ObjetoBase
    {
        /// <summary>
        /// Consulta um objeto armazenado com o id de parametro
        /// </summary>
        /// <param name="id">O Id do objeto armazenado</param>
        /// <returns>Um objeto persistente com o id de parametro ou nulo caso não exista</returns>
        public static Menu ConsultarPorId(int id)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            Menu classe = new Menu();
            if (id < 0)
                throw new ArgumentException("Id não pode ser negativo.");
            else
            {
                classe.Id = id;
                return fabrica.GetDAOBase().ConsultarPorID<Menu>(classe);
            }
        }

        /// <summary>
        /// Consulta o corrente objeto em banco, tornando-o persistente
        /// </summary>
        public virtual Menu ConsultarPorId()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            if (this.Id < 0)
                throw new ArgumentException("Id não pode ser negativo.");
            else
                return fabrica.GetDAOBase().ConsultarPorID<Menu>(this);
        }

        /// <summary>
        /// Salva a corrente instância em banco
        /// </summary>
        public virtual Menu Salvar()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().Salvar<Menu>(this);
        }

        /// <summary>
        /// Salva a corrente instância em banco forçando o id
        /// </summary>
        public virtual Menu SalvarComId()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarComId<Menu>(this);
        }

        /// <summary>
        /// Salva uma lista de objetos, tornando-os persistentes
        /// </summary>
        /// <param name="objetos">Os objetos a serem salvos</param>
        public virtual IList<Menu> SalvarTodos(IList<Menu> objs)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarTodos<Menu>(objs);
        }

        /// <summary>
        /// Salva uma lista de objetos, tornando-os persistentes
        /// </summary>
        /// <param name="objetos">Os objetos a serem salvos</param>
        public virtual IList<Menu> SalvarTodos(params Menu[] objs)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarTodos<Menu>(objs);
        }

        /// <summary>
        /// Destaca a corrente instancia do cache do Hibernate
        /// </summary>
        public virtual bool DestacarObjeto()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().DestacarObjeto<Menu>(this);
        }

        /// <summary>
        /// Exclui a corrente instância do banco
        /// </summary>
        public virtual bool Excluir()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().Excluir<Menu>(this);
        }

        /// <summary>
        /// Retorna todos os objetos armazenados em banco deste Tipo
        /// </summary>
        /// <returns>Uma lista de objetos armazenados</returns>
        public static IList<Menu> ConsultarTodos()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            Menu obj = Activator.CreateInstance<Menu>();
            return fabrica.GetDAOBase().ConsultarTodos<Menu>(obj);
        }

        /// <summary>
        /// Método que retorna uma lista limitada de objetos em ordem ascendente do Nome
        /// </summary>
        /// <param name="qtd">A quantidade de objetos a serem retornados na lista, o Valor 0 retorna todos</param>
        /// <returns>Uma lista limitada de objetos ordenados pelo Nome</returns>
        public virtual IList<Menu> ConsultarOrdemAcendente(int qtd)
        {
            Menu ee = new Menu();
            ee.AdicionarFiltro(Filtros.SetOrderAsc("Nome"));
            if (qtd > 0)
                ee.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Menu>(ee);
        }

        /// <summary>
        /// Método que retorna uma lista limitada de objetos em ordem descendente do Nome
        /// </summary>
        /// <param name="qtd">A quantidade de objetos a serem retornados na lista, o Valor 0 retorna todos</param>
        /// <returns>Uma lista limitada de objetos ordenados pelo Nome</returns>
        public virtual IList<Menu> ConsultarOrdemDescendente(int qtd)
        {
            Menu ee = new Menu();
            ee.AdicionarFiltro(Filtros.SetOrderDesc("Nome"));
            if (qtd > 0)
                ee.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Menu>(ee);
        }

        /// <summary>
        /// Filtra uma certa quantidade de Menu
        /// </summary>
        /// <param name="qtd">A quantidade de objetos que devem ser retornados no máximo,valores menores que 1 retornam todos os objetos</param>
        /// <returns>Uma lista com a quantidade de objetos</returns>
        public virtual IList<Menu> Filtrar(int qtd)
        {
            Menu estado = new Menu();
            if (qtd > 0)
                estado.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Menu>(estado);
        }

        /// <summary>
        /// Retorna o ultimo Menu Inserido no banco de dados
        /// </summary>
        /// <returns>O ultimo Menu</returns>
        public virtual Menu UltimoInserido()
        {
            Menu estado = new Menu();
            estado.AdicionarFiltro(Filtros.Max("Id"));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarUnicoComFiltro<Menu>(estado);
        }

        /// <summary>
        /// Retorna menus auxiliares com as funcionalidades do usuário
        /// </summary>
        /// <param name="telasUsuario">As telas do Usuário</param>
        /// <returns>Uma lista de menus auxiliares com as telas do usuário</returns>
        //public static IList<Menu> GetMenusDasTelas(IList<Tela> telasUsuario)
        //{
        //    if (telasUsuario == null)
        //        return null;

        //    IList<Menu> todosOsMenus = new List<Menu>();
        //    IList<Menu> retorno = new List<Menu>();

        //    foreach (Tela tela in telasUsuario)
        //        Menu.AdicionarMenu(todosOsMenus, retorno, tela.Menu, tela);
        //    return retorno;
        //}

        //private static Menu GetPrimeiroMenuPai(Menu menu)
        //{
        //    if (menu.MenuPai == null)
        //        return menu;
        //    else return Menu.GetPrimeiroMenuPai(menu.MenuPai);
        //}

        //private static void AdicionarMenu(IList<Menu> todosOsMenus, IList<Menu> retorno, Menu menu, Tela tela)
        //{
        //    Menu.AdicionarMenuNoMenuPai(todosOsMenus, retorno, menu);
        //    Menu.GetMenu(retorno, menu).Telas.Remove(tela.CloneObject<Tela>());
        //    Menu.GetMenu(retorno, menu).Telas.Add(tela.CloneObject<Tela>());
        //}

        //private static Menu GetMenu(IList<Menu> retorno, Menu menu)
        //{
        //    return Menu.GetMenuRecursivo(retorno, menu, null);
        //}

        //private static Menu GetMenuRecursivo(IList<Menu> menus, Menu menu, Menu retorno)
        //{
        //    if (menu != null && retorno == null)
        //    {
        //        List<Menu> subMenus = new List<Menu>();
        //        foreach (Menu aux in menus)
        //        {
        //            if (aux.Equals(menu))
        //            {
        //                retorno = aux;
        //                break;
        //            }
        //            if (aux.SubMenus != null)
        //                subMenus.AddRange(aux.SubMenus);
        //        }
        //        if (retorno == null && subMenus.Count > 0)
        //            retorno = Menu.GetMenuRecursivo(subMenus, menu, retorno);
        //    }
        //    return retorno;
        //}

        //private static void AdicionarMenuNoMenuPai(IList<Menu> todosOsMenus, IList<Menu> retorno, Menu menu)
        //{
        //    if (menu != null && !todosOsMenus.Contains(menu))
        //    {
        //        if (menu.MenuPai != null)
        //        {
        //            Menu.AdicionarMenuNoMenuPai(todosOsMenus, retorno, menu.MenuPai);
        //            Menu aux = menu.CloneObject<Menu>();
        //            aux.Telas = new List<Tela>();
        //            aux.SubMenus = new List<Menu>();
        //            if (!Menu.GetMenu(retorno, menu.MenuPai).SubMenus.Contains(aux))
        //                Menu.GetMenu(retorno, menu.MenuPai).SubMenus.Add(aux);
        //        }
        //        else
        //        {
        //            Menu aux = menu.CloneObject<Menu>();
        //            aux.Telas = new List<Tela>();
        //            aux.SubMenus = new List<Menu>();
        //            todosOsMenus.Add(aux);
        //            retorno.Add(aux);
        //        }
        //    }
        //}

        public static IList<Menu> ConsultarTodosOrdemPrioridade()
        {
            Menu aux = new Menu();
            aux.AdicionarFiltro(Filtros.SetOrderAsc("PrioridadeExibicao"));
            aux.AdicionarFiltro(Filtros.Distinct());
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Menu>(aux);
        }

        //public static IList<Menu> OrdenarPelaPrioridade(IList<Menu> menus)
        //{
        //    if (menus == null)
        //        return null;
        //    IList<Menu> retorno = new List<Menu>();

        //    foreach (Menu aux in menus)
        //    {
        //        int i = 0;
        //        for (i = 0; i < retorno.Count; i++)
        //        {
        //            if (retorno.Count < 1 || retorno[i].PrioridadeExibicao > aux.PrioridadeExibicao)
        //                break;
        //        }
        //        retorno.Insert(i, aux);
        //    }

        //    return retorno;
        //}

        public static IList<Menu> ConsultarMenusPaiOrdemPrioridade()
        {
            Menu aux = new Menu();
            aux.AdicionarFiltro(Filtros.IsNull("MenuPai"));
            aux.AdicionarFiltro(Filtros.SetOrderAsc("PrioridadeExibicao"));
            aux.AdicionarFiltro(Filtros.Distinct());
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Menu>(aux);
        }

        public static void DeletarPermissoesMenu(int id)
        {
            try
            {
                string sqlDelecao =
                    @"delete from permissoes where permissoes.id in(
                        select p.id from permissoes p, telas t, menus m
                        where
                            p.id_tela = t.id and
                            t.id_menu = m.id and
                            m.id = '" + (id) + @"'
                    );";
                new FabricaDAONHibernateBase().GetDAOBase().ExecutarComandoSql(sqlDelecao);
            }
            catch (Exception) { }
        }

        public static IList<Menu> GetMenusDasTelas(IList<Tela> telasUsuario)
        {
            if (telasUsuario == null)
                return null;

            IList<Menu> retorno = new List<Menu>();
            foreach (Tela tela in telasUsuario)
            {
                if (tela.Menu != null && !retorno.Contains(tela.Menu))
                {
                    Menu menuAux = tela.Menu.CloneObject<Menu>();
                    menuAux.Telas = new List<Tela>();
                    retorno.Add(menuAux);
                }
                retorno[retorno.IndexOf(tela.Menu)].Telas.Add(tela);
            }
            return retorno;
        }

        private static void AdicionarMenu(IList<Menu> todosOsMenus, IList<Menu> retorno, Menu menu, Tela tela)
        {
            Menu.AdicionarMenuNoMenuPai(todosOsMenus, retorno, menu);
            Menu.GetMenu(retorno, menu).Telas.Remove(tela.CloneObject<Tela>());
            Menu.GetMenu(retorno, menu).Telas.Add(tela.CloneObject<Tela>());
        }

        private static void AdicionarMenuNoMenuPai(IList<Menu> todosOsMenus, IList<Menu> retorno, Menu menu)
        {
            if (menu != null && !todosOsMenus.Contains(menu))
            {
                if (menu.MenuPai != null)
                {
                    Menu.AdicionarMenuNoMenuPai(todosOsMenus, retorno, menu.MenuPai);
                    Menu aux = menu.CloneObject<Menu>();
                    aux.Telas = new List<Tela>();
                    aux.SubMenus = new List<Menu>();
                    if (!Menu.GetMenu(retorno, menu.MenuPai).SubMenus.Contains(aux))
                        Menu.GetMenu(retorno, menu.MenuPai).SubMenus.Add(aux);
                }
                else
                {
                    Menu aux = menu.CloneObject<Menu>();
                    aux.Telas = new List<Tela>();
                    aux.SubMenus = new List<Menu>();
                    todosOsMenus.Add(aux);
                    retorno.Add(aux);
                }
            }
        }

        private static Menu GetMenu(IList<Menu> retorno, Menu menu)
        {
            return Menu.GetMenuRecursivo(retorno, menu, null);
        }

        private static Menu GetMenuRecursivo(IList<Menu> menus, Menu menu, Menu retorno)
        {
            if (menu != null && retorno == null)
            {
                List<Menu> subMenus = new List<Menu>();
                foreach (Menu aux in menus)
                {
                    if (aux.Equals(menu))
                    {
                        retorno = aux;
                        break;
                    }
                    if (aux.SubMenus != null)
                        subMenus.AddRange(aux.SubMenus);
                }
                if (retorno == null && subMenus.Count > 0)
                    retorno = Menu.GetMenuRecursivo(subMenus, menu, retorno);
            }
            return retorno;
        }
    }
}
