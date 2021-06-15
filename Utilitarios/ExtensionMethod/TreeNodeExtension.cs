using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;


public static class TreeNodeExtension
{
    public static void DeselectChildsNodes(this TreeNode node)
    {
        node.Checked = false;
        if (node.ChildNodes.Count != 0)
        {
            foreach (TreeNode childNode in node.ChildNodes)
            {
                node.DeselectChildsNodes();
            }
        }
    }

    public static void CheckChildNodes(this TreeNode node, string value)
    {
        if (node.ChildNodes.Count > 0)
            foreach (TreeNode no in node.ChildNodes)
            {
                if (no.Value.Equals(value))
                    no.Checked = true;
                no.CheckChildNodes(value);
            }
    }

    public static TreeNode GetNode(this TreeNode node, string value)
    {
        return TreeNodeExtension.GetNodeRecursivo(node, null, value);
    }

    private static TreeNode GetNodeRecursivo(TreeNode node, TreeNode retorno, string value)
    {
        if (node.Value == value)
            retorno = node;
        if (retorno == null)
            foreach (TreeNode noFilho in node.ChildNodes)
            {
                retorno = GetNodeRecursivo(noFilho, retorno, value);
                if (retorno != null)
                    break;
            }
        return retorno;
    }
}

