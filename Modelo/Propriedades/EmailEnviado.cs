using NHibernate.Mapping.Attributes;
using Persistencia.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modelo
{
    [Serializable]
    [Class(Table = "emails", Name = "Modelo.EmailEnviado, Modelo")]
    public partial class EmailEnviado : ObjetoBase
    {
        public static String GERAL = "Geral";
        public static String ORCAMENTO = "Orçamento";

        private String nomeEmail;
        private String remetente;
        private String destinatarios;
        private DateTime dataEnvio;
        private String emailsCopia;
        private String assunto;
        private String textoEmail;
        private String observacoesInternas;
        private DateTime dataCriacao = DateTime.Now;
        private bool enviado;
        private String tipoEmail = EmailEnviado.GERAL;

        private Funcionario funcionarioEnvio;
        private Pedido pedido;

        [Property(Column = "nome_email")]
        public virtual String NomeEmail
        {
            get { return nomeEmail; }
            set { nomeEmail = value; }
        }
        [Property(Column = "remetente")]
        public virtual String Remetente
        {
            get { return remetente; }
            set { remetente = value; }
        }
        [Property(Column = "destinatarios")]
        public virtual String Destinatarios
        {
            get { return destinatarios; }
            set { destinatarios = value; }
        }
        [Property(Column = "data_envio")]
        public virtual DateTime DataEnvio
        {
            get { return dataEnvio; }
            set { dataEnvio = value; }
        }
        [Property(Column = "emails_copia")]
        public virtual String EmailsCopia
        {
            get { return emailsCopia; }
            set { emailsCopia = value; }
        }
        [Property(Column = "assunto")]
        public virtual String Assunto
        {
            get { return assunto; }
            set { assunto = value; }
        }
        [Property(Type = "StringClob")]
        [Column(1, Name = "texto_email", SqlType = "text")]
        public virtual String TextoEmail
        {
            get { return textoEmail; }
            set { textoEmail = value; }
        }
        [Property(Column = "observacoes_internas")]
        public virtual String ObservacoesInternas
        {
            get { return observacoesInternas; }
            set { observacoesInternas = value; }
        }
        [Property(Column = "data_criacao")]
        public virtual DateTime DataCriacao
        {
            get { return dataCriacao; }
            set { dataCriacao = value; }
        }
        [Property(Column = "enviado", Type = "TrueFalse")]
        public virtual bool Enviado
        {
            get { return enviado; }
            set { enviado = value; }
        }
        [Property(Column = "tipo_email")]
        public virtual String TipoEmail
        {
            get { return tipoEmail; }
            set { tipoEmail = value; }
        }

        [ManyToOne(Name = "FuncionarioEnvio", Column = "id_funcionario_envio", Class = "Modelo.Funcionario, Modelo")]
        public virtual Funcionario FuncionarioEnvio
        {
            get { return funcionarioEnvio; }
            set { funcionarioEnvio = value; }
        }
        [ManyToOne(Name = "Pedido", Column = "id_pedido", Class = "Modelo.Pedido, Modelo")]
        public virtual Pedido Pedido
        {
            get { return pedido; }
            set { pedido = value; }
        }
    }
}
