using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping.Attributes;
using Persistencia.Modelo;

namespace Modelo
{
    [Serializable]
    [Class(Table = "preferencias_relatorio", Name = "Modelo.PreferenciaRelatorio, Modelo")]
    public partial class PreferenciaRelatorio: ObjetoBase
    {
        public PreferenciaRelatorio(int id) { this.Id = id; }
        public PreferenciaRelatorio(Object id) { this.Id = Convert.ToInt32("0" + id.ToString()); }
        public PreferenciaRelatorio() { }

        #region ________ Atributos ________

        private string preferencias;
        private Funcionario funcionario;
        private Tela tela;

        #endregion

        #region ________ Propriedades ________

        [Property(Column = "preferencia")]
        public virtual string Preferencia
        {
            get { return preferencias; }
            set { preferencias = value; }
        }

        [ManyToOne(Class = "Modelo.Funcionario, Modelo", Name = "Funcionario", Column = "id_funcionario")]
        public virtual Funcionario Funcionario
        {
            get { return funcionario; }
            set { funcionario = value; }
        }

        [ManyToOne(Class = "Modelo.Tela, Modelo", Name = "Tela", Column = "id_tela")]
        public virtual Tela Tela
        {
            get { return tela; }
            set { tela = value; }
        }

        #endregion
    }
}
