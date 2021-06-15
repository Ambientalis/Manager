using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using NHibernate.Mapping.Attributes;
using Persistencia.Modelo;

namespace Modelo
{
    [Serializable]
    [Class(Table = "reservas", Name = "Modelo.Reserva, Modelo")]
    public partial class Reserva: ObjetoBase
    {
        public Reserva(int id) { this.Id = id; }
        public Reserva(Object id) { this.Id = Convert.ToInt32("0" + id.ToString()); }
        public Reserva() { }

        #region _________Atributos__________

        private DateTime dataInicio;
        private DateTime dataFim;
        private string descricao;        
        private Veiculo veiculo;
        private Visita visita;
        private TipoReservaVeiculo tipoReservaVeiculo;
        private Funcionario responsavel;
        private decimal quilometragem;
        private decimal consumo;
        private char status;

        public const char APROVADA = 'A';
        public const char AGUARDANDO = 'G';
        public const char ENCERRADA = 'E';

        private IList<Ocorrencia> ocorrencias;

        #endregion

        #region ________Propriedades________

        [Property(Column = "data_inicio")]
        public virtual DateTime DataInicio
        {        
            get
            {
                if (dataInicio <= SqlDateTime.MinValue.Value)
                    return SqlDateTime.MinValue.Value;
                else
                    return dataInicio;
            }
            set { dataInicio = value; }
        }

        [Property(Column = "data_fim")]
        public virtual DateTime DataFim
        {
            get
            {
                if (dataFim == null || dataFim <= SqlDateTime.MinValue.Value)
                    return SqlDate.MinValue;
                else
                    return dataFim;
            }            
            set { dataFim = value; }
        }

        [Property(Type = "StringClob")]
        [Column(1, Name = "descricao", SqlType = "text")]
        public virtual string Descricao
        {
            get { return descricao; }
            set { descricao = value; }
        }

        [ManyToOne(Name = "Veiculo", Class = "Modelo.Veiculo, Modelo", Column = "id_veiculo")]
        public virtual Veiculo Veiculo
        {
            get { return veiculo; }
            set { veiculo = value; }
        }

        [ManyToOne(Class = "Modelo.Visita, Modelo", Column = "id_visita")]
        public virtual Visita Visita
        {
            get { return visita; }
            set { visita = value; }
        }

        [ManyToOne(Class = "Modelo.TipoReservaVeiculo, Modelo", Column = "id_tipo_reserva_veiculo")]
        public virtual TipoReservaVeiculo TipoReservaVeiculo
        {
            get { return tipoReservaVeiculo; }
            set { tipoReservaVeiculo = value; }
        }

        [ManyToOne(Name = "Responsavel", Class = "Modelo.Funcionario, Modelo", Column = "id_responsavel")]
        public virtual Funcionario Responsavel
        {
            get { return responsavel; }
            set { responsavel = value; }
        }

        [Property(Column = "quilometragem")]
        public virtual decimal Quilometragem
        {
            get { return quilometragem; }
            set { quilometragem = value; }
        }

        [Property(Column = "consumo")]
        public virtual decimal Consumo
        {
            get { return consumo; }
            set { consumo = value; }
        }

        [Property(Column = "status")]
        public virtual char Status
        {
            get { return status; }
            set { status = value; }
        }

        [Bag(Name = "Ocorrencias", Table = "ocorrencias", Cascade = "delete")]
        [Key(2, Column = "id_reserva")]
        [OneToMany(3, Class = "Modelo.Ocorrencia, Modelo")]
        public virtual IList<Ocorrencia> Ocorrencias
        {
            get { return ocorrencias; }
            set { ocorrencias = value; }
        }

        #endregion
    }
}
