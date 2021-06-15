using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping.Attributes;
using Persistencia.Modelo;
using Modelo;
using System.Data.SqlTypes;


namespace Modelo
{
    [Serializable]
    [Class(Table = "pessoas", Name = "Modelo.Pessoa, Modelo")]
    public partial class Pessoa : ObjetoBase
    {
        public Pessoa(int id) { this.Id = id; }
        public Pessoa(Object id) { this.Id = Convert.ToInt32("0" + id.ToString()); }
        public Pessoa() { }

        #region ___________Atributos_____________

        public const char FISICA = 'F';
        public const char JURIDICA = 'J';

        private string codigo;        
        private char tipo;                
        private string nomeRazaoSocial;
        private string apelidoNomeFantasia;
        private string cpfCnpj;
        private string inscricaoEstadual;
        private bool isentoICMS;
        private string inscricaoMunicipal;
        private string rg;
        private string emissorRg;
        private string nacionalidade;        
        private DateTime dataNascimento;
        private string estadoCivil;
        private char sexo;        
        private string observacoes;        
        private IList<Endereco> enderecos;        
        private Imagem imagem;
        private Origem origem;

        #endregion

        #region _________ Propriedades _________

        [Property(Column = "codigo", Length = 18)]
        public virtual string Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        [Property(Column = "tipo")]
        public virtual char Tipo
        {
            get { return tipo; }
            set { tipo = value; }
        }        

        [Property(Column = "nome_razao_social", Length = 255)]
        public virtual string NomeRazaoSocial
        {
            get { return nomeRazaoSocial; }
            set { nomeRazaoSocial = value; }
        }

        [Property(Column = "apelido_nome_fantasia", Length = 255)]
        public virtual string ApelidoNomeFantasia
        {
            get { return apelidoNomeFantasia; }
            set { apelidoNomeFantasia = value; }
        }

        [Property(Column = "cpf_cnpj", Length = 18)]
        public virtual string CpfCnpj
        {
            get { return cpfCnpj; }
            set { cpfCnpj = value; }
        }

        [Property(Column = "inscricao_estadual", Length = 80)]
        public virtual string InscricaoEstadual
        {
            get { return inscricaoEstadual; }
            set { inscricaoEstadual = value; }
        }

        [Property(Type = "TrueFalse")]
        public virtual bool IsentoICMS
        {
            get { return isentoICMS; }
            set { isentoICMS = value; }
        }

        [Property(Column = "inscricao_municipal", Length = 80)]
        public virtual string InscricaoMunicipal
        {
            get { return inscricaoMunicipal; }
            set { inscricaoMunicipal = value; }
        }

        [Property(Column = "rg", Length = 80)]
        public virtual string Rg
        {
            get { return rg; }
            set { rg = value; }
        }

        [Property(Column = "emissor_rg", Length = 80)]
        public virtual string EmissorRg
        {
            get { return emissorRg; }
            set { emissorRg = value; }
        }

        [Property(Length = 30)]
        public virtual string Nacionalidade
        {
            get { return nacionalidade; }
            set { nacionalidade = value; }
        }        

        [Property(Column = "data_nascimento")]
        public virtual DateTime DataNascimento
        {
            get
            {
                if (dataNascimento <= SqlDateTime.MinValue.Value)
                    return SqlDateTime.MinValue.Value;
                else
                    return dataNascimento;
            }
            set { dataNascimento = value; }            
        }

        [Property(Column = "estado_civil", Length = 30)]
        public virtual string EstadoCivil
        {
            get { return estadoCivil; }
            set { estadoCivil = value; }
        }

        [Property(Column = "sexo")]
        public virtual char Sexo
        {
            get { return sexo; }
            set { sexo = value; }
        }        

        [Property]
        [Column(1, SqlType = "text", Name = "observacoes")]
        public virtual string Observacoes
        {
            get { return observacoes; }
            set { observacoes = value; }
        }        

        [Bag(Name = "Enderecos", Table = "enderecos", Cascade = "delete")]
        [Key(2, Column = "id_pessoa")]
        [OneToMany(3, Class = "Modelo.Endereco, Modelo")]
        public virtual IList<Endereco> Enderecos
        {
            get { return enderecos; }
            set { enderecos = value; }
        } 

        [ManyToOne(Class = "Modelo.Imagem, Modelo", Column = "id_imagem")]
        public virtual Imagem Imagem
        {
            get { return imagem; }
            set { imagem = value; }
        }

        [ManyToOne(Class = "Modelo.Origem, Modelo", Column = "id_origem")]
        public virtual Origem Origem
        {
            get { return origem; }
            set { origem = value; }
        }

        #endregion
    }
}
