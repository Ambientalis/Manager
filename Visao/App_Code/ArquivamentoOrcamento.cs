using Modelo;
using Persistencia.Fabrica;
using Quartz;
using System;
using System.Collections.Generic;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Modelo;
using SisWebControls;
using Utilitarios;
using System.Drawing;

/// <summary>
/// Descrição resumida de ArquivamentoPedido
/// </summary>
public class ArquivamentoOrcamento : IJob
{  

    public void Execute(IJobExecutionContext context)
    {

        try
        {
            Transacao.Instance.Recarregar();
            IList<FormaDePagamento> formas = FormaDePagamento.ConsultarTodos();




        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        ///aqui vai entrar o que é preciso ser feito...exemplo
        ///Obs: Deverá ser implementado uma rotina que arquive automaticamente todos os pedidos que forem cancelados.
    }
}