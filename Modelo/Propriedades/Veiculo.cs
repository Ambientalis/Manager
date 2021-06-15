using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping.Attributes;
using Persistencia.Modelo;


namespace Modelo
{
    [Serializable]
    [Class(Table = "veiculos", Name = "Modelo.Veiculo, Modelo")]
    public partial class Veiculo : ObjetoBase
    {
        #region ________Construtores_________

        public Veiculo(int id) { this.Id = id; }
        public Veiculo(Object id) { this.Id = Convert.ToInt32("0" + id.ToString()); }
        public Veiculo() { }

        #endregion

        #region _________Atributos__________
                   
        private string descricao;
        private string placa;
        private IList<Reserva> reservas;        
        private IList<Departamento> departamentosQuePodemUtilizar;
        private Funcionario gestor;
        private Departamento departamentoResponsavel;        
        
        #endregion

        #region ________Propriedades________

        [Property(Column = "placa")]
        public virtual string Placa
        {
            get { return placa; }
            set { placa = value; }
        }

        [Property(Column = "descricao")]
        public virtual string Descricao
        {
            get { return descricao; }
            set { descricao = value; }
        }

        [Bag(Name = "Reservas", Table = "reservas", Cascade = "delete")]
        [Key(2, Column = "id_veiculo")]
        [OneToMany(3, Class = "Modelo.Reserva, Modelo")]
        public virtual IList<Reserva> Reservas
        {
            get { return reservas; }
            set { reservas = value; }
        }

        [Bag(Table = "veiculos_departamentos")]
        [Key(2, Column = "id_veiculo")]
        [ManyToMany(3, Class = "Modelo.Departamento, Modelo", Column = "id_departamento")]
        public virtual IList<Departamento> DepartamentosQuePodemUtilizar
        {
            get { return departamentosQuePodemUtilizar; }
            set { departamentosQuePodemUtilizar = value; }
        }

        [ManyToOne(Name = "Gestor", Class = "Modelo.Funcionario, Modelo", Column = "id_gestor")]
        public virtual Funcionario Gestor
        {
            get { return gestor; }
            set { gestor = value; }
        }

        [ManyToOne(Name = "DepartamentoResponsavel", Class = "Modelo.Departamento, Modelo", Column = "id_departamento_responsavel")]
        public virtual Departamento DepartamentoResponsavel
        {
            get { return departamentoResponsavel; }
            set { departamentoResponsavel = value; }
        }

        #endregion
    }
}
