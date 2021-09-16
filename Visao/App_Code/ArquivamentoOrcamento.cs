using Modelo;
using Persistencia.Fabrica;
using Quartz;
using System;

/// <summary>
/// Descrição resumida de ArquivamentoPedido
/// </summary>
public class ArquivamentoOrcamento : IJob
{
    public void Execute(IJobExecutionContext context)
    {
        try
        {
            Orcamento.PostCancellStatus();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        ///aqui vai entrar o que é preciso ser feito...exemplo
        ///Obs: Deverá ser implementado uma rotina que arquive automaticamente todos os pedidos que forem cancelados.
    }
}