using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Persistencia.Modelo;
using NHibernate.Mapping.Attributes;

namespace Modelo
{
    [Serializable]
    [Class(Name = "Modelo.Menu, Modelo", Table = "menus")]
    public partial class Menu : ObjetoBase
    {
        public Menu(int id)
        {
            this.Id = id;
            this.MultiEmpresa = false;
        }
        public Menu(Object id)
        {
            this.Id = Convert.ToInt32("0" + id.ToString());
            this.MultiEmpresa = false;
        }
        public Menu()
        {
            this.MultiEmpresa = false;
        }

        private string nome;
        private Menu menuPai;
        private IList<Menu> subMenus;
        private IList<Tela> telas;

        [Property(Column = "nome")]
        public virtual string Nome
        {
            get { return nome; }
            set { nome = value; }
        }

        [ManyToOne(Class = "Modelo.Menu, Modelo", Name = "MenuPai", Column = "id_menu_pai", Lazy = Laziness.False)]
        public virtual Menu MenuPai
        {
            get { return menuPai; }
            set { menuPai = value; }
        }

        [Bag(Name = "SubMenus", Table = "menus", Cascade = "delete", Lazy = CollectionLazy.False)]
        [Key(2, Column = "id_menu_pai")]
        [OneToMany(3, Class = "Modelo.Menu, Modelo")]
        public virtual IList<Menu> SubMenus
        {
            get { return subMenus; }
            set { subMenus = value; }
        }

        [Bag(Name = "Telas", Table = "telas", Cascade = "delete")]
        [Key(2, Column = "id_menu")]
        [OneToMany(3, Class = "Modelo.Tela, Modelo")]
        public virtual IList<Tela> Telas
        {
            get { return telas; }
            set { telas = value; }
        }

    }
}
