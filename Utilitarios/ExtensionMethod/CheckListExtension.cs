using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

public static class CheckListExtension
{
    public static ListItemCollection GetSelectedItems(this CheckBoxList chkl)
    {
        ListItemCollection retorno = new ListItemCollection();
        if (chkl.Items != null)
            foreach (ListItem item in chkl.Items)
                if (item.Selected)
                    retorno.Add(item);
        return retorno;
    }

    public static string GetDescricaoItensSelecionados(this CheckBoxList chkl)
    {
        StringBuilder aux = new StringBuilder();
        if (chkl.Items != null)
            foreach (ListItem item in chkl.Items)
                if (item.Selected)
                    aux.Append(item.Text + ";");
        return aux.ToString();
    }
}