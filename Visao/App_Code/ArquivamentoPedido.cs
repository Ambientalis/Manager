﻿using Modelo;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descrição resumida de ArquivamentoPedido
/// </summary>
public class ArquivamentoPedido : IJob
{
    public void Execute(IJobExecutionContext context)
    {
        try
        {

            Pedido.PostCancellStatus();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        ///aqui vai entrar o que é preciso ser feito...exemplo
        ///Obs: Deverá ser implementado uma rotina que arquive automaticamente todos os pedidos que forem cancelados.
    }
}

