using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Persistencia.Modelo;
using NHibernate.Mapping.Attributes;

namespace Modelo
{
    [Serializable]
    [Class(Name = "Modelo.Tela, Modelo", Table = "telas")]
    public partial class Tela : ObjetoBase
    {
        public Tela(int id)
        {
            this.Id = id;
            this.MultiEmpresa = false;
        }
        public Tela(Object id)
        {
            this.Id = Convert.ToInt32("0" + id.ToString());
            this.MultiEmpresa = false;
        }
        public Tela()
        {
            this.MultiEmpresa = false;
        }

        private string nome;
        private string url;
        private string icone;
        private string toolTip;
        private bool exibirNoMenu;
        private bool relatorio;
        private Menu menu;
        private IList<Permissao> permissoes;
        private int prioridade;
        private bool relatorioGrafico;
        private bool telaFinanceiro;

        [Property(Column = "nome")]
        public virtual string Nome
        {
            get { return nome; }
            set { nome = value; }
        }

        [Property(Column = "url")]
        public virtual string Url
        {
            get { return url; }
            set { url = value; }
        }

        [Property(Column = "icone")]
        public virtual string Icone
        {
            get { return icone; }
            set { icone = value; }
        }

        [Property(Type = "StringClob")]
        [Column(1, Name = "tooltip", SqlType = "text")]
        public virtual string ToolTip
        {
            get { return toolTip; }
            set { toolTip = value; }
        }

        [Property(Column = "exibir_menu", Type = "TrueFalse")]
        public virtual bool ExibirNoMenu
        {
            get { return exibirNoMenu; }
            set { exibirNoMenu = value; }
        }

        [Property(Column = "relatorio", Type = "TrueFalse")]
        public virtual bool Relatorio
        {
            get { return relatorio; }
            set { relatorio = value; }
        }

        [ManyToOne(Class = "Modelo.Menu, Modelo", Name = "Menu", Column = "id_menu", Lazy = Laziness.False)]
        public virtual Menu Menu
        {
            get { return menu; }
            set { menu = value; }
        }

        [Bag(Table = "telas_permissoes", Lazy = CollectionLazy.False)]
        [Key(2, Column = "id_tela")]
        [ManyToMany(3, Class = "Modelo.Permissao, Modelo", Column = "id_permissao")]
        public virtual IList<Permissao> Permissoes
        {
            get { return permissoes; }
            set { permissoes = value; }
        }

        [Property(Column = "prioridade")]
        public virtual int Prioridade
        {
            get { return prioridade; }
            set { prioridade = value; }
        }

        [Property(Column = "relatorio_grafico", Type = "TrueFalse")]
        public virtual bool RelatorioGrafico
        {
            get { return relatorioGrafico; }
            set { relatorioGrafico = value; }
        }

        [Property(Column = "tela_financeiro", Type = "TrueFalse")]
        public virtual bool TelaFinanceiro
        {
            get { return telaFinanceiro; }
            set { telaFinanceiro = value; }
        }


    }
}
