using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Modelo;

public class OSUtil
{
    public OSUtil()
    {

    }
    public static void AlterarOsVencida(String codigo)
    {
        OrdemServico ordem = OrdemServico.ConsultarPeloCodigo(codigo);
        if (ordem == null)
            throw new ArgumentException("OS " + (codigo) + " não encontrada!");
        if (!ordem.IsEncerrada)
            throw new ArgumentException("OS " + (codigo) + " ainda não encerrada!");
        if (!ordem.IsVencida)
            throw new ArgumentException("OS " + (codigo) + " não está vencida!");

        //Usuário do Piassi
        Funcionario userPiassi = new Funcionario(70).ConsultarPorId();

        DateTime dataEncerramento = ordem.DataEncerramento;
        DateTime dataVencimentoAntigo = ordem.GetDataVencimento.ToMinHourOfDay();

        SolicitacaoAdiamento solicitacao = new SolicitacaoAdiamento();
        solicitacao.UsuarioAdiou = userPiassi.NomeRazaoSocial;
        solicitacao.Solicitante = userPiassi.NomeRazaoSocial;
        solicitacao.Justificativa = "Muito trabalho com detalhes.";
        solicitacao.Parecer = SolicitacaoAdiamento.ACEITA;
        solicitacao.OrdemServico = ordem;
        solicitacao.Data = dataVencimentoAntigo;
        solicitacao.DataResposta = solicitacao.Data.AddHours(1);
        solicitacao.PrazoPadraoAnterior = ordem.PrazoPadrao;
        solicitacao.PrazoLegalAnterior = ordem.PrazoLegal;
        solicitacao.PrazoDiretoriaAnterior = ordem.PrazoDiretoria;

        if (ordem.PossuiProtocolo || (ordem.DataProtocoloEncerramento != null && ordem.DataProtocoloEncerramento.CompareTo(SqlDate.MinValue) > 0))
            ordem.DataProtocoloEncerramento = dataEncerramento;
        if (ordem.PrazoDiretoria != null && ordem.PrazoDiretoria.CompareTo(SqlDate.MinValue) > 0)
            ordem.PrazoDiretoria = dataEncerramento;
        else if (ordem.PrazoLegal != null && ordem.PrazoLegal.CompareTo(SqlDate.MinValue) > 0)
            ordem.PrazoLegal = dataEncerramento;
        else if (ordem.PrazoPadrao != null && ordem.PrazoPadrao.CompareTo(SqlDate.MinValue) > 0)
            ordem.PrazoPadrao = dataEncerramento;

        solicitacao.PrazoLegalNovo = ordem.PrazoLegal;
        solicitacao.PrazoDiretoriaNovo = ordem.PrazoDiretoria;
        solicitacao.PrazoPadraoNovo = ordem.PrazoPadrao;

        solicitacao = solicitacao.Salvar();
        ordem = ordem.Salvar();
    }
}