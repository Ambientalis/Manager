using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping.Attributes;
using Persistencia.Modelo;
using System.Data.SqlTypes;

namespace Modelo
{
    [Serializable]
    [Class(Name = "Modelo.OrdemServico, Modelo", Table = "ordens_servico")]
    public partial class OrdemServico : ObjetoBase
    {
        public OrdemServico(int id) { this.Id = id; }
        public OrdemServico(Object id) { this.Id = Convert.ToInt32("0" + id.ToString()); }
        public OrdemServico() { }

        #region ___________ Atributos ___________

        public const char DIAS = 'D';
        public const char MESES = 'M';

        public const char REPROVADA = 'R';
        public const char APROVADA = 'A';
        public const char ABERTA = 'B';
        public const char PENDENTEAPROVACAO = 'P';

        private string codigo;
        private bool semCusto;
        private DateTime data;
        private string descricao;
        private DateTime prazoPadrao;
        private DateTime prazoLegal;
        private DateTime prazoDiretoria;
        private string numeroProcessoOrgao;
        private bool renovavel;
        private int prazoRenovacao;
        private char tipoRenovacao;
        private bool jaRenovada;
        private DateTime dataEncerramento;
        private String protocoloOficioEncerramento;
        private DateTime dataProtocoloEncerramento;
        private char status;
        private string justificativaAprovacao;
        private DateTime dataAprovacao;
        private string usuarioAprovou;
        private bool possuiProtocolo;
        private decimal valorNominal;
        private decimal desconto;

        private Pedido pedido;
        private IList<Funcionario> coResponsaveis;
        private Funcionario responsavel;

        private Setor setor;
        private IList<Visita> visitas;
        private IList<Atividade> atividades;
        private IList<SolicitacaoAdiamento> solicitacoesAdiamento;
        private IList<SolicitacaoAprovacao> solicitacoesAprovacao;
        private Orgao orgao;
        private TipoOrdemServico tipo;
        private IList<Arquivo> arquivos;
        private IList<Detalhamento> detalhamentos;
        private IList<Detalhamento> observacoes;

        private OrdemServico osMatriz;
        private IList<OrdemServico> ossVinculadas;
        private PesquisaSatisfacao pesquisaSatisfacao;

        #endregion

        #region ___________ Propriedades ___________

        [Property(Column = "codigo")]
        public virtual string Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        [Property(Column = "sem_custo", Type = "TrueFalse")]
        public virtual bool SemCusto
        {
            get { return semCusto; }
            set { semCusto = value; }
        }

        [Property(Column = "data")]
        public virtual DateTime Data
        {
            get { return data; }
            set { data = value; }
        }

        [Property(Type = "StringClob")]
        [Column(1, Name = "descricao", SqlType = "text")]
        public virtual string Descricao
        {
            get { return descricao; }
            set { descricao = value; }
        }

        [Property(Column = "prazo_padrao")]
        public virtual DateTime PrazoPadrao
        {
            get
            {
                if (prazoPadrao == null || prazoPadrao <= SqlDateTime.MinValue.Value)
                    return SqlDate.MinValue;
                else
                    return prazoPadrao;
            }
            set { prazoPadrao = value; }
        }

        [Property(Column = "prazo_legal")]
        public virtual DateTime PrazoLegal
        {
            get
            {
                if (prazoLegal == null || prazoLegal <= SqlDateTime.MinValue.Value)
                    return SqlDate.MinValue;
                else
                    return prazoLegal;
            }
            set { prazoLegal = value; }
        }

        [Property(Column = "prazo_diretoria")]
        public virtual DateTime PrazoDiretoria
        {
            get
            {
                if (prazoDiretoria == null || prazoDiretoria <= SqlDateTime.MinValue.Value)
                    return SqlDate.MinValue;
                else
                    return prazoDiretoria;
            }
            set { prazoDiretoria = value; }
        }

        [Property(Column = "numero_processo_orgao")]
        public virtual string NumeroProcessoOrgao
        {
            get { return numeroProcessoOrgao; }
            set { numeroProcessoOrgao = value; }
        }

        [Property(Column = "renovavel", Type = "TrueFalse")]
        public virtual bool Renovavel
        {
            get { return renovavel; }
            set { renovavel = value; }
        }

        [Property(Column = "prazo_renovacao")]
        public virtual int PrazoRenovacao
        {
            get { return prazoRenovacao; }
            set { prazoRenovacao = value; }
        }

        [Property(Column = "tipo_renovacao")]
        public virtual char TipoRenovacao
        {
            get { return tipoRenovacao; }
            set { tipoRenovacao = value; }
        }

        [Property(Column = "ja_renovada", Type = "TrueFalse")]
        public virtual bool JaRenovada
        {
            get { return jaRenovada; }
            set { jaRenovada = value; }
        }

        [Property(Column = "data_encerramento")]
        public virtual DateTime DataEncerramento
        {
            get
            {
                if (dataEncerramento == null || dataEncerramento <= SqlDateTime.MinValue.Value)
                    return SqlDate.MinValue;
                else
                    return dataEncerramento;
            }
            set { dataEncerramento = value; }
        }

        [Property(Column = "protocolo_oficio_encerramento")]
        public virtual String ProtocoloOficioEncerramento
        {
            get { return protocoloOficioEncerramento; }
            set { protocoloOficioEncerramento = value; }
        }

        [Property(Column = "data_protocolo_encerramento")]
        public virtual DateTime DataProtocoloEncerramento
        {
            get
            {
                if (dataProtocoloEncerramento == null || dataProtocoloEncerramento <= SqlDateTime.MinValue.Value)
                    return SqlDate.MinValue;
                else
                    return dataProtocoloEncerramento;
            }
            set { dataProtocoloEncerramento = value; }
        }

        [Property(Column = "status")]
        public virtual char Status
        {
            get { return status; }
            set { status = value; }
        }

        [Property(Type = "StringClob")]
        [Column(1, Name = "justificativa_aprovacao", SqlType = "text")]
        public virtual string JustificativaAprovacao
        {
            get { return justificativaAprovacao; }
            set { justificativaAprovacao = value; }
        }

        [Property(Column = "data_aprovacao")]
        public virtual DateTime DataAprovacao
        {
            get
            {
                if (dataAprovacao == null || dataAprovacao <= SqlDateTime.MinValue.Value)
                    return SqlDate.MinValue;
                else
                    return dataAprovacao;
            }
            set { dataAprovacao = value; }
        }

        [Property(Column = "usuario_aprovou")]
        public virtual string UsuarioAprovou
        {
            get { return usuarioAprovou; }
            set { usuarioAprovou = value; }
        }

        [Property(Column = "possui_protocolo", Type = "TrueFalse")]
        public virtual bool PossuiProtocolo
        {
            get { return possuiProtocolo; }
            set { possuiProtocolo = value; }
        }

        [Property(Column = "valor_nominal")]
        public virtual decimal ValorNominal
        {
            get { return valorNominal; }
            set { valorNominal = value; }
        }

        [Property(Column = "desconto")]
        public virtual decimal Desconto
        {
            get { return desconto; }
            set { desconto = value; }
        }

        [ManyToOne(Name = "Pedido", Class = "Modelo.Pedido, Modelo", Column = "id_pedido")]
        public virtual Pedido Pedido
        {
            get { return pedido; }
            set { pedido = value; }
        }

        [Bag(Table = "ordens_servico_funcionarios")]
        [Key(2, Column = "id_ordem_servico")]
        [ManyToMany(3, Class = "Modelo.Funcionario, Modelo", Column = "id_funcionario")]
        public virtual IList<Funcionario> CoResponsaveis
        {
            get { return coResponsaveis; }
            set { coResponsaveis = value; }
        }

        [ManyToOne(Name = "Responsavel", Class = "Modelo.Funcionario, Modelo", Column = "id_responsavel")]
        public virtual Funcionario Responsavel
        {
            get { return responsavel; }
            set { responsavel = value; }
        }

        [ManyToOne(Name = "Setor", Class = "Modelo.Setor, Modelo", Column = "id_setor")]
        public virtual Setor Setor
        {
            get { return setor; }
            set { setor = value; }
        }

        [Bag(Name = "Visitas", Table = "visitas", Cascade = "delete")]
        [Key(2, Column = "id_ordem_servico")]
        [OneToMany(3, Class = "Modelo.Visita, Modelo")]
        public virtual IList<Visita> Visitas
        {
            get { return visitas; }
            set { visitas = value; }
        }

        [Bag(Name = "Atividades", Table = "atividades", Cascade = "delete")]
        [Key(2, Column = "id_ordem_servico")]
        [OneToMany(3, Class = "Modelo.Atividade, Modelo")]
        public virtual IList<Atividade> Atividades
        {
            get { return atividades; }
            set { atividades = value; }
        }

        [Bag(Name = "SolicitacoesAdiamento", Table = "solicitacoes_adiamentos", Cascade = "delete")]
        [Key(2, Column = "id_ordem_servico")]
        [OneToMany(3, Class = "Modelo.SolicitacaoAdiamento, Modelo")]
        public virtual IList<SolicitacaoAdiamento> SolicitacoesAdiamento
        {
            get { return solicitacoesAdiamento; }
            set { solicitacoesAdiamento = value; }
        }

        [Bag(Name = "SolicitacoesAprovacao", Table = "solicitacoes_aprovacoes", Cascade = "delete")]
        [Key(2, Column = "id_ordem_servico")]
        [OneToMany(3, Class = "Modelo.SolicitacaoAprovacao, Modelo")]
        public virtual IList<SolicitacaoAprovacao> SolicitacoesAprovacao
        {
            get { return solicitacoesAprovacao; }
            set { solicitacoesAprovacao = value; }
        }

        [ManyToOne(Name = "Orgao", Class = "Modelo.Orgao, Modelo", Column = "id_orgao")]
        public virtual Orgao Orgao
        {
            get { return orgao; }
            set { orgao = value; }
        }

        [ManyToOne(Name = "Tipo", Class = "Modelo.TipoOrdemServico, Modelo", Column = "id_tipo_ordem_servico")]
        public virtual TipoOrdemServico Tipo
        {
            get { return tipo; }
            set { tipo = value; }
        }

        [Bag(Name = "Arquivos", Table = "arquivos", Cascade = "delete")]
        [Key(2, Column = "id_ordem_servico")]
        [OneToMany(3, Class = "Modelo.Arquivo, Modelo")]
        public virtual IList<Arquivo> Arquivos
        {
            get { return arquivos; }
            set { arquivos = value; }
        }

        [Bag(Name = "Detalhamentos", Table = "detalhamentos", Cascade = "delete")]
        [Key(2, Column = "id_ordem_servico")]
        [OneToMany(3, Class = "Modelo.Detalhamento, Modelo")]
        public virtual IList<Detalhamento> Detalhamentos
        {
            get { return detalhamentos; }
            set { detalhamentos = value; }
        }

        [Bag(Name = "Observacoes", Table = "detalhamentos", Cascade = "delete")]
        [Key(2, Column = "id_ordem_servico_observacao")]
        [OneToMany(3, Class = "Modelo.Detalhamento, Modelo")]
        public virtual IList<Detalhamento> Observacoes
        {
            get { return observacoes; }
            set { observacoes = value; }
        }

        [ManyToOne(Name = "OsMatriz", Class = "Modelo.OrdemServico, Modelo", Column = "id_ordem_matriz")]
        public virtual OrdemServico OsMatriz
        {
            get { return osMatriz; }
            set { osMatriz = value; }
        }

        [Bag(Name = "OssVinculadas", Table = "ordens_servico", Cascade = "delete")]
        [Key(2, Column = "id_ordem_matriz")]
        [OneToMany(3, Class = "Modelo.OrdemServico, Modelo")]
        public virtual IList<OrdemServico> OssVinculadas
        {
            get { return ossVinculadas; }
            set { ossVinculadas = value; }
        }

        [OneToOne(PropertyRef = "OrdemServico", Class = "Modelo.PesquisaSatisfacao, Modelo")]
        public virtual PesquisaSatisfacao PesquisaSatisfacao
        {
            get { return pesquisaSatisfacao; }
            set { pesquisaSatisfacao = value; }
        }

        #endregion
    }
}
