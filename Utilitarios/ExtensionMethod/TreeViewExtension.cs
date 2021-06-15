using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;


public static class TreeViewExtension
{
    public static void DeselectAll(this TreeView trv)
    {
        foreach (TreeNode node in trv.Nodes)
        {
            node.Checked = false;
            if (node.ChildNodes.Count != 0)
            {
                node.DeselectChildsNodes();
            }
        }
    }

    public static void CheckNode(this TreeView trv, string value)
    {
        foreach (TreeNode no in trv.Nodes)
        {
            if (no.Value.Equals(value))
                no.Checked = true;
            if (no.ChildNodes.Count > 0)
                no.CheckChildNodes(value);
        }
    }

    public static TreeNode GetNode(this TreeView trv, string value)
    {
        TreeNode retorno = null;
        foreach (TreeNode no in trv.Nodes)
        {
            retorno = no.GetNode(value);
            if (retorno != null)
                break;
        }
        return retorno;
    }

    //public static void ExpandNode(this TreeView trv, TreeNode node)
    //{
    //    String[] nodesValue = node.ValuePath.Split('/');
    //    if (nodesValue != null && nodesValue.Length > 0 && trv.Nodes != null && trv.Nodes.Count > 0)
    //    {
    //        TreeNodeCollection nodes = trv.Nodes;
    //        foreach (string valueNode in nodesValue)
    //        {
    //            foreach (TreeNode n in nodes)
    //            {
    //                if (valueNode == n.Value)
    //                {
    //                    n.Expand();
    //                    nodes = n.ChildNodes;
    //                    break;
    //                }
    //            }
    //        }
    //    }
    //}    

}

