using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping.Attributes;
using Persistencia.Modelo;

namespace Modelo
{
    [Serializable]
    [Class(Table = "funcoes", Name = "Modelo.Funcao, Modelo")]
    public partial class Funcao : ObjetoBase
    {
        public Funcao(int id)
        {
            this.MultiEmpresa = false;
            this.Id = id;
        }
        public Funcao(Object id)
        {
            this.MultiEmpresa = false;
            this.Id = Convert.ToInt32("0" + id.ToString());
        }
        public Funcao()
        {
            this.MultiEmpresa = false;
        }

        #region ___________ Atributos ___________

        private IList<Funcionario> funcionarios;
        private IList<Permissao> permissoes;

        private Cargo cargo;
        private Setor setor;

        #endregion

        #region _________ Propriedades __________

        [Bag(Table = "funcionarios_funcoes")]
        [Key(2, Column = "id_funcao")]
        [ManyToMany(3, Class = "Modelo.Funcionario, Modelo", Column = "id_funcionario")]
        public virtual IList<Funcionario> Funcionarios
        {
            get { return funcionarios; }
            set { funcionarios = value; }
        }

        [Bag(Name = "Permissoes", Table = "permissoes", Cascade = "delete")]
        [Key(2, Column = "id_funcao")]
        [OneToMany(3, Class = "Modelo.Permissao, Modelo")]
        public virtual IList<Permissao> Permissoes
        {
            get { return permissoes; }
            set { permissoes = value; }
        }

        [ManyToOne(Name = "Cargo", Class = "Modelo.Cargo, Modelo", Column = "id_cargo")]
        public virtual Cargo Cargo
        {
            get { return cargo; }
            set { cargo = value; }
        }

        [ManyToOne(Name = "Setor", Class = "Modelo.Setor, Modelo", Column = "id_setor")]
        public virtual Setor Setor
        {
            get { return setor; }
            set { setor = value; }
        }

        #endregion


    }
}
