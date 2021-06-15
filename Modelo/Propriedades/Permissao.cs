using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Persistencia.Modelo;
using NHibernate.Mapping.Attributes;


namespace Modelo
{
    [Serializable]
    [Class(Name = "Modelo.Permissao, Modelo", Table = "permissoes")]
    public partial class Permissao : ObjetoBase
    {
        public Permissao(int id)
        {
            this.Id = id;
            this.MultiEmpresa = false;
        }
        public Permissao(Object id)
        {
            this.Id = Convert.ToInt32("0" + id.ToString());
            this.MultiEmpresa = false;
        }
        public Permissao()
        {
            this.MultiEmpresa = false;
        }

        #region ___________ Atributos ___________

        public const char NENHUMA = 'N';
        public const char RESPONSAVEL = 'R';
        public const char SETOR = 'S';
        public const char DEPARTAMENTO = 'D';
        public const char TODOS = 'T';

        private char acessaOS = Permissao.NENHUMA;
        private char aprovaOS = Permissao.NENHUMA;
        private char adiaPrazoLegalOS = Permissao.NENHUMA;
        private char adiaPrazoDiretoriaOS = Permissao.NENHUMA;
        private char visualizaControleViagens = Permissao.NENHUMA;
        private char aprovaDespesa = Permissao.NENHUMA;

        private Funcao funcao;
        private Funcionario funcionario;
        private IList<Tela> telas;

        #endregion

        #region __________ Propriedade __________

        [Property(Column = "acessa_os")]
        public virtual char AcessaOS
        {
            get { return acessaOS; }
            set { acessaOS = value; }
        }

        [Property(Column = "aprova_os")]
        public virtual char AprovaOS
        {
            get { return aprovaOS; }
            set { aprovaOS = value; }
        }

        [Property(Column = "adia_prazo_legal_os")]
        public virtual char AdiaPrazoLegalOS
        {
            get { return adiaPrazoLegalOS; }
            set { adiaPrazoLegalOS = value; }
        }

        [Property(Column = "adia_prazo_diretoria_os")]
        public virtual char AdiaPrazoDiretoriaOS
        {
            get { return adiaPrazoDiretoriaOS; }
            set { adiaPrazoDiretoriaOS = value; }
        }

        [Property(Column = "visualiza_controle_viagens")]
        public virtual char VisualizaControleViagens
        {
            get { return visualizaControleViagens; }
            set { visualizaControleViagens = value; }
        }

        [Property(Column = "aprova_despesa")]
        public virtual char AprovaDespesa
        {
            get { return aprovaDespesa; }
            set { aprovaDespesa = value; }
        }

        [ManyToOne(Class = "Modelo.Funcao, Modelo", Column = "id_funcao")]
        public virtual Funcao Funcao
        {
            get { return funcao; }
            set { funcao = value; }
        }

        [ManyToOne(Class = "Modelo.Funcionario, Modelo", Column = "id_funcionario")]
        public virtual Funcionario Funcionario
        {
            get { return funcionario; }
            set { funcionario = value; }
        }

        [Bag(Table = "telas_permissoes")]
        [Key(2, Column = "id_permissao")]
        [ManyToMany(3, Class = "Modelo.Tela, Modelo", Column = "id_tela")]
        public virtual IList<Tela> Telas
        {
            get { return telas; }
            set { telas = value; }
        }

        #endregion

    }
}
