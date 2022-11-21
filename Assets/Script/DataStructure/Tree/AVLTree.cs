using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class AVLTree:BinaryTree
{
    private enum UnBalanceType
    {
        LL,
        RR,
        LR,
        RL,
    }

    private Dictionary<UnBalanceType, Action<BinaryTreeNode<int>>> rotate_handle_tab = new Dictionary<UnBalanceType, Action<BinaryTreeNode<int>>>();

    private BinaryTreeNode<int> new_insert_node;


    public AVLTree(int value)
    {
        root = new BinaryTreeNode<int>(value);
        rotate_handle_tab.Add(UnBalanceType.LL, LL_Rotate);
        rotate_handle_tab.Add(UnBalanceType.RR, RR_Rotate);
        rotate_handle_tab.Add(UnBalanceType.LR, LR_Rotate);
        rotate_handle_tab.Add(UnBalanceType.RL, RL_Rotate);
    }

    private UnBalanceType GetUnBalanceType(int left_height, int right_height,bool is_left_insert)
    {
        if (left_height > right_height && is_left_insert)
        {
            return UnBalanceType.LL;
        }

        if (left_height > right_height && !is_left_insert)
        {
            return UnBalanceType.LR;
        }

        if (left_height < right_height && is_left_insert)
        {
            return UnBalanceType.RL;
        }

        return UnBalanceType.RR;
    }
    

    public void LL_Rotate(BinaryTreeNode<int> unbalance_node)
    {
        root = LeftRotate(unbalance_node);
    }

    public void RR_Rotate(BinaryTreeNode<int> unbalance_node)
    {
        root = RightRotate(unbalance_node);
    }

    public BinaryTreeNode<int> LeftRotate(BinaryTreeNode<int> unbalance_node)
    {
        var right_child = unbalance_node.right;
        BinaryTreeNode<int> temp_right_child_left = right_child.left;
    

        right_child.left = unbalance_node;
        right_child.parent = unbalance_node.parent;

        if (right_child.parent != null)
        {
            right_child.parent.left = right_child;
        }

        unbalance_node.parent = right_child;
        unbalance_node.right = temp_right_child_left;

        if (temp_right_child_left != null)
        {
            temp_right_child_left.parent = unbalance_node;
        }

        return right_child;
    }

    public BinaryTreeNode<int> RightRotate(BinaryTreeNode<int> unbalance_node)
    {

        var left_child = unbalance_node.left;
        BinaryTreeNode<int> temp_left_child_right = left_child.right;

        left_child.right = unbalance_node;
        left_child.parent = unbalance_node.parent;

        if (left_child.parent != null)
        {
            left_child.parent.right = left_child;
        }
        unbalance_node.parent = left_child;
        unbalance_node.left = temp_left_child_right;

        if (temp_left_child_right != null)
        {
            temp_left_child_right.parent = unbalance_node;
        }

        return left_child;
    }

    public void RL_Rotate(BinaryTreeNode<int> unbalance_node)
    {
        RightRotate(unbalance_node.right);
        root = LeftRotate(unbalance_node);
    }

    public void LR_Rotate(BinaryTreeNode<int> unbalance_node)
    {
        LeftRotate(unbalance_node.left);
        Debug.Log("-----右旋-------");
        DFS();
        Debug.Log("-----右旋-------");
        root = RightRotate(unbalance_node);
    }

    private void Insert(int value, BinaryTreeNode<int> header)
    {
        if (value <= header.value)
        {
            if (header.left == null)
            {
                header.left = new BinaryTreeNode<int>(value);
                header.left.parent = header;
                new_insert_node = header.left;
            }
            else
            {
                Insert(value, header.left);
            }
        }
        else
        {
            if (header.right == null)
            {
                header.right = new BinaryTreeNode<int>(value);
                header.right.parent = header;
                new_insert_node = header.right;
            }
            else
            {
                Insert (value, header.right);
            }
        }
    }

    public bool IsBalance(BinaryTreeNode<int> node)
    {
        var left_height = FindHeight(node.left);
        var right_height = FindHeight(node.right);
        return Mathf.Abs(left_height - right_height) <= 1;
    }

    public BinaryTreeNode<int> SearchMinUnbalanceNode(BinaryTreeNode<int> node)
    {
        if (node == null)
        {
            return null;
        }

        if (IsBalance(node))
        {
            return SearchMinUnbalanceNode(node.parent);
        }
        else
        {
            return node;
        }

    }

    public override void Insert(int value)
    {
        //一般二叉树插入
        var header = root;
        Insert(value, root);
        //检查最小失衡树
        if (new_insert_node.parent != null)
        {
            var min_unbalance_node = SearchMinUnbalanceNode(new_insert_node.parent);
            if (min_unbalance_node != null)
            {
                var left_height = FindHeight(min_unbalance_node.left);
                var right_height = FindHeight(min_unbalance_node.right);
                var is_left_insert = new_insert_node.left != null;
                var unbalance_type = GetUnBalanceType(left_height, right_height, is_left_insert);
                Debug.Log("失衡类型:" + unbalance_type);

                if (rotate_handle_tab.ContainsKey(unbalance_type))
                {
                    rotate_handle_tab[unbalance_type](min_unbalance_node);
                }
            }
        }
        
       
    }
}
