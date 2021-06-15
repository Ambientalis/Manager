using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping.Attributes;
using Persistencia.Modelo;

namespace Modelo
{
    [Serializable]
    [Class(Table = "controles_viagens", Name = "Modelo.ControleViagem, Modelo")]
    public partial class ControleViagem : ObjetoBase
    {
        #region __________ Construtores _____________
        public static string CASTELO = "Castelo";
        public static string INTERMUNICIPAL = "Intermunicipal";
        public static string INTERESTADUAL = "Interestadual";
        #endregion

        #region __________ Construtores _____________

        public ControleViagem(int id) { this.Id = id; }
        public ControleViagem(Object id) { this.Id = Convert.ToInt32("0" + id.ToString()); }
        public ControleViagem() { }

        #endregion

        #region ___________ Atributos ___________

        private DateTime data;
        private DateTime dataHoraSaida;
        private DateTime dataHoraChegada;
        private decimal quilometragemSaida;
        private decimal quilometragemChegada;
        private String roteiro = ControleViagem.CASTELO;
        private String observacoes;

        private Funcionario responsavel;
        private Funcionario motorista;
        private Veiculo veiculo;
        private Setor setorQueUtilizou;
        private IList<Abastecimento> abastecimentos = new List<Abastecimento>();

        #endregion

        #region _________ Propriedades __________

        [Property(Column = "data")]
        public virtual DateTime Data
        {
            get { return data; }
            set { data = value; }
        }

        [Property(Column = "data_hora_saida")]
        public virtual DateTime DataHoraSaida
        {
            get { return dataHoraSaida; }
            set { dataHoraSaida = value; }
        }

        [Property(Column = "data_hora_chegada")]
        public virtual DateTime DataHoraChegada
        {
            get { return dataHoraChegada; }
            set { dataHoraChegada = value; }
        }

        [Property(Column = "quilometragem_saida")]
        public virtual decimal QuilometragemSaida
        {
            get { return quilometragemSaida; }
            set { quilometragemSaida = value; }
        }

        [Property(Column = "quilometragem_chegada")]
        public virtual decimal QuilometragemChegada
        {
            get { return quilometragemChegada; }
            set { quilometragemChegada = value; }
        }

        [Property(Column = "roteiro")]
        public virtual String Roteiro
        {
            get { return roteiro; }
            set { roteiro = value; }
        }

        [Property(Type = "StringClob")]
        [Column(1, Name = "observacoes", SqlType = "text")]
        public virtual String Observacoes
        {
            get { return observacoes; }
            set { observacoes = value; }
        }

        [ManyToOne(Name = "Responsavel", Class = "Modelo.Funcionario, Modelo", Column = "id_responsavel")]
        public virtual Funcionario Responsavel
        {
            get { return responsavel; }
            set { responsavel = value; }
        }

        [ManyToOne(Name = "Motorista", Class = "Modelo.Funcionario, Modelo", Column = "id_motorista")]
        public virtual Funcionario Motorista
        {
            get { return motorista; }
            set { motorista = value; }
        }

        [ManyToOne(Name = "Veiculo", Class = "Modelo.Veiculo, Modelo", Column = "id_veiculo")]
        public virtual Veiculo Veiculo
        {
            get { return veiculo; }
            set { veiculo = value; }
        }

        [ManyToOne(Name = "SetorQueUtilizou", Class = "Modelo.Setor, Modelo", Column = "id_setor_utilizacao")]
        public virtual Setor SetorQueUtilizou
        {
            get { return setorQueUtilizou; }
            set { setorQueUtilizou = value; }
        }

        [Bag(Name = "Abastecimentos", Table = "abastecimentos")]
        [Cache(1, Region = "Longo", Usage = CacheUsage.NonStrictReadWrite)]
        [Key(2, Column = "id_controle_viagem")]
        [OneToMany(3, Class = "Modelo.Abastecimento, Modelo")]
        public virtual IList<Abastecimento> Abastecimentos
        {
            get { return abastecimentos; }
            set { abastecimentos = value; }
        }

        #endregion

    }
}
