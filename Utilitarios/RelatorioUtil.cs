using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Modelo;

namespace Utilitarios
{
    public static class RelatorioUtil
    {
        public static void CriarColunas(GridView grvRelatorio, CheckBoxList ckbColunas, Page page)
        {
            IList<string> preferencas = PreferenciaRelatorio.ConsultarPreferencias(page.AppRelativeVirtualPath.Replace("~", "..").Replace("../Relatorios/", ""), WebUtil.FuncionarioLogado);
            foreach (DataControlField item in grvRelatorio.Columns)
            {
                ListItem x = new ListItem(item.HeaderText, item.HeaderText);
                ckbColunas.Items.Add(x);
                if (preferencas.Count > 0)
                {
                    x.Selected = preferencas.Contains(item.HeaderText);
                }
                else
                {
                    x.Selected = item.ShowHeader;
                }
            }
        }

        public static void CriarColunasAdm(GridView grvRelatorio, CheckBoxList ckbColunas, Page page)
        {
            foreach (DataControlField item in grvRelatorio.Columns)
            {
                ListItem x = new ListItem(item.HeaderText, item.HeaderText);
                ckbColunas.Items.Add(x);
                x.Selected = item.ShowHeader;
            }
        }

        public static void SalvarPreferencias(CheckBoxList ckbColunas, Page page)
        {
            Funcionario funcionario = Funcionario.ConsultarPorId(WebUtil.FuncionarioLogado.Id);
            PreferenciaRelatorio pref = PreferenciaRelatorio.Consultar(page.AppRelativeVirtualPath.Replace("~", "..").Replace("../Relatorios/", ""), funcionario);
            if (pref == null)
            {
                pref = new PreferenciaRelatorio();
                pref.Tela = Modelo.Tela.ConsultarPorPath(page.AppRelativeVirtualPath.Replace("~", "..").Replace("../Relatorios/", ""));
                pref.Funcionario = funcionario;
            }
            pref.Preferencia = RelatorioUtil.PegarPreferencias(ckbColunas);
            pref.Salvar();
        }

        private static string PegarPreferencias(CheckBoxList ckbColunas)
        {
            string retorno = string.Empty;
            foreach (ListItem item in ckbColunas.Items)
            {
                if (item.Selected)
                {
                    retorno += item.Text + ";";
                }
            }
            return retorno;
        }

        public static void ExibirColunas(GridView grvRelatorio, CheckBoxList ckbColunas)
        {
            int indexItem = -1;
            ListItemCollection itensMarcados = ckbColunas.GetSelectedItems();
            for (int i = 0; i < grvRelatorio.Columns.Count; i++)
            {
                indexItem = itensMarcados.IndexOf(new ListItem(grvRelatorio.Columns[i].HeaderText, grvRelatorio.Columns[i].HeaderText));
                grvRelatorio.Columns[i].Visible = indexItem > -1 && itensMarcados[indexItem].Selected;
            }
        }

        public static void OcultarFiltros(Page page)
        {
            ((HiddenField)page.Master.FindControl("hfMostrarFiltros")).Value = "F";
        }

        public static string GetDescricaoData(DateTime dataDe, DateTime dataAteh)
        {
            string retorno = "Todas";
            if (dataDe.CompareTo(SqlDateTime.MinValue.Value) > 0 || dataAteh.CompareTo(SqlDateTime.MaxValue.Value) < 0)
            {
                if (dataDe.CompareTo(SqlDateTime.MinValue.Value) > 0 && dataAteh.CompareTo(SqlDateTime.MaxValue.Value) < 0)
                    retorno = "de " + dataDe.ToShortDateString() + " até " + dataAteh.ToShortDateString();
                else
                    retorno = dataDe.CompareTo(SqlDateTime.MinValue.Value) > 0 ? "após " + dataDe.ToShortDateString() : "antes de " + dataAteh.ToShortDateString();
            }
            return retorno;
        }

        public static string GetDescricaoQuantidade(decimal intDe, decimal intAteh)
        {
            string retorno = "Todos";
            if (intDe > int.MinValue || intAteh < int.MaxValue)
            {
                if (intDe > int.MinValue && intAteh < int.MaxValue)
                    retorno = "de " + intDe + " até " + intAteh;
                else
                    retorno = intDe > int.MinValue ? "maior que " + intDe : " menor que " + intAteh;
            }
            return retorno;
        }

        public static string GetItensSelecionadosCheckBoxList(CheckBoxList ckb)
        {
            string ret = "";
            foreach (ListItem item in ckb.Items)
            {
                if (item.Selected)
                    ret += item.Text + " - ";
            }
            return ret;
        }

        public static void CarregarDropDownSimNao(DropDownList ddl)
        {
            ddl.Items.Clear();
            ddl.Items.Add(new ListItem("-- Todos --", "0"));
            ddl.Items.Add(new ListItem("SIM", "1"));
            ddl.Items.Add(new ListItem("NÃO", "2"));
            ddl.SelectedIndex = 0;
        }

        public static void CriarColunasComercial(GridView grvRelatorio, CheckBoxList ckbColunas, Page page)
        {
            foreach (DataControlField item in grvRelatorio.Columns)
            {
                ListItem x = new ListItem(item.HeaderText, item.HeaderText);
                ckbColunas.Items.Add(x);
                x.Selected = item.ShowHeader;
            }
        }
    }
}
