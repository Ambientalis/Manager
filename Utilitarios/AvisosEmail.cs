using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Net;
using System.Configuration;
using System.Web.Mail;
using System.IO;


namespace Utilitarios
{
    public class AvisosEmail
    {
        #region _____________ TEMPLATES ____________

        public static string TextoAgendaIndividual(string responsavel, string tipoAtendimento, string titulo, string cliente, string obs, string inicio, string fim)
        {
            return @"<div style='height: 20px; font-family: Arial, Helvetica, sans-serif; font-size: 14px;
        margin-top: 20px; margin-right: 20px; border-bottom: 1px solid silver; font-weight: 700;'
        align='left'>
        Responsável: " +
        responsavel + @"
    </div>
    <table style='width: 100%; margin-top: 10px; height: auto; font-family: Arial, Helvetica, sans-serif;
        font-size: 14px;'>
        <tr>
            <td align='right' width='50%' style='font-weight: bold;'>
                Tipo de Atendimento:
            </td>
            <td align='left' width='50%'>
                " + tipoAtendimento + @"
            </td>
        </tr>
        <tr>
            <td align='right' width='50%' style='font-weight: bold;'>
                Título:
            </td>
            <td align='left' width='50%'>
                " + titulo + @"
            </td>
        </tr>
        <tr>
            <td align='right' width='50%' style='font-weight: bold;'>
                Cliente:
            </td>
            <td align='left' width='50%'>
                " + cliente + @"
            </td>
        </tr>
        <tr>
            <td align='right' width='50%' style='font-weight: bold;'>
                Observação:
            </td>
            <td align='left' width='50%'>
                " + obs + @"
            </td>
        </tr>
        <tr>
            <td align='right' width='50%' style='font-weight: bold;'>
                Data de início::
            </td>
            <td align='left' width='50%'>
                " + inicio + @"
            </td>
        </tr>
        <tr>
            <td align='right' width='50%' style='font-weight: bold;'>
                Data de fim:
            </td>
            <td align='left' width='50%'>
                " + fim + @"
            </td>
        </tr>
    </table>";
        }

        public static string TextoAgendaIndividualParaEmailResumoSemanal(string tipoAtendimento, string titulo, string cliente, string obs, string inicio, string fim, string diaSemana)
        {
            return @"
<div style='height: 20px; font-family: Arial, Helvetica, sans-serif; font-size: 14px;
        margin-top: 20px; margin-right: 20px; border-bottom: 1px solid silver; font-weight: 700;'
        align='left'>
        DIA: " +
        diaSemana + @"
    </div>
    <table style='width: 100%; margin-top: 10px; height: auto; font-family: Arial, Helvetica, sans-serif;
        font-size: 14px;'>
        <tr>
            <td align='right' width='50%' style='font-weight: bold;'>
                Tipo de Atendimento:
            </td>
            <td align='left' width='50%'>
                " + tipoAtendimento + @"
            </td>
        </tr>
        <tr>
            <td align='right' width='50%' style='font-weight: bold;'>
                Título:
            </td>
            <td align='left' width='50%'>
                " + titulo + @"
            </td>
        </tr>
        <tr>
            <td align='right' width='50%' style='font-weight: bold;'>
                Cliente:
            </td>
            <td align='left' width='50%'>
                " + cliente + @"
            </td>
        </tr>
        <tr>
            <td align='right' width='50%' style='font-weight: bold;'>
                Observação:
            </td>
            <td align='left' width='50%'>
                " + obs + @"
            </td>
        </tr>
        <tr>
            <td align='right' width='50%' style='font-weight: bold;'>
                Data de início::
            </td>
            <td align='left' width='50%'>
                " + inicio + @"
            </td>
        </tr>
        <tr>
            <td align='right' width='50%' style='font-weight: bold;'>
                Data de fim:
            </td>
            <td align='left' width='50%'>
                " + fim + @"
            </td>
        </tr>
    </table>";
        }

        #endregion

        #region _____________ ATRIBUTOS ____________


        #endregion

        #region _____________ PROPRIEDADES ____________


        #endregion

        #region _____________ CONTRUTOR ____________


        #endregion

        #region _____________ EVENTOS ____________


        #endregion

        #region _____________ MÉTODOS ____________

        

        

        #endregion

    }
}
