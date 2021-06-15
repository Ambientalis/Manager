using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping.Attributes;
using Persistencia.Modelo;

namespace Modelo
{
    [Serializable]
    [Class(Table = "departamentos", Name = "Modelo.Departamento, Modelo")]
    public partial class Departamento : ObjetoBase
    {
        #region __________ Construtores _____________

        public Departamento(int id) { this.Id = id; }
        public Departamento(Object id) { this.Id = Convert.ToInt32("0" + id.ToString()); }
        public Departamento() { }

        #endregion

        #region ___________ Atributos ___________

        private string nome;
        private IList<Setor> setores;
        private IList<Veiculo> veiculosQuePodemUtilizar;
        private IList<Veiculo> veiculosSobResponsabilidade;
        
        #endregion

        #region ___________ Propriedades ___________

        [Property(Column = "nome")]
        public virtual string Nome
        {
            get { return nome; }
            set { nome = value; }
        }

        [Bag(Name = "Setores", Table = "setores", Cascade = "delete")]
        [Key(2, Column = "id_departamento")]
        [OneToMany(3, Class = "Modelo.Setor, Modelo")]
        public virtual IList<Setor> Setores
        {
            get { return setores; }
            set { setores = value; }
        }

        [Bag(Table = "veiculos_departamentos", Lazy = CollectionLazy.False)]
        [Key(2, Column = "id_departamento")]
        [ManyToMany(3, Class = "Modelo.Veiculo, Modelo", Column = "id_veiculo")]
        public virtual IList<Veiculo> VeiculosQuePodemUtilizar
        {
            get { return veiculosQuePodemUtilizar; }
            set { veiculosQuePodemUtilizar = value; }
        }

        [Bag(Name = "VeiculosSobResponsabilidade", Table = "veiculos")]
        [Key(2, Column = "id_departamento_responsavel")]
        [OneToMany(3, Class = "Modelo.Veiculo, Modelo")]
        public virtual IList<Veiculo> VeiculosSobResponsabilidade
        {
            get { return veiculosSobResponsabilidade; }
            set { veiculosSobResponsabilidade = value; }
        }

        #endregion
    }
}
