﻿using Algorithms.BinarySearchTree;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Algorithms.Tests.BinarySearchTreeTests
{
    public class When_Remove_Is_Called
    {
        [Fact]
        public void If_Tree_Is_Empty_Then_InvalidOperationException_Is_Thrown()
        {
            var tree = new BinarySearchTree<int>();

            tree.IsEmpty.Should().BeTrue();

            Action badRemove = () => tree.Remove(10);

            badRemove.Should().Throw<InvalidOperationException>("The tree is empty.");
        }

        [Fact]
        public void If_Element_Is_Not_In_Tree_Then_InvalidOperationException_Is_Thrown()
        {
            var tree = new BinarySearchTree<int>();

            tree.Insert(10);

            Action badRemove = () => tree.Remove(0);

            badRemove.Should().Throw<InvalidOperationException>("The element does not exist.");
        }

        [Fact]
        public void If_Element_Is_A_Leaf_Node_Then_Element_Is_Removed()
        {
            var node = BuildNodeWithFilledOutSubtrees();
            var tree = new BinarySearchTree<int>(node);

            tree.Remove(node.LeftChild.LeftChild.Data);

            node.LeftChild.LeftChild.Should().BeNull();

            tree.Remove(node.LeftChild.RightChild.Data);

            node.LeftChild.RightChild.Should().BeNull();

            tree.Remove(node.RightChild.LeftChild.Data);

            node.RightChild.LeftChild.Should().BeNull();

            tree.Remove(node.RightChild.RightChild.Data);

            node.RightChild.RightChild.Should().BeNull();
        }

        [Fact]
        public void If_Element_Has_One_Child_Then_Element_Is_Removed_And_Child_Replaces_Element()
        {
            var node = BuildNodeWithFilledOutSubtrees();
            var newLeftChild = node.LeftChild.LeftChild.Data;
            var newRightChild = node.RightChild.RightChild.Data;

            node.LeftChild.RightChild = null;
            node.RightChild.LeftChild = null;

            var tree = new BinarySearchTree<int>(node);

            tree.Remove(node.LeftChild.Data);

            node.LeftChild.Data.Should().Be(newLeftChild);

            tree.Remove(node.RightChild.Data);

            node.RightChild.Data.Should().Be(newRightChild);
        }

        [Fact]
        public void If_Element_Has_Two_Children_Then_Elements_In_Order_Successor_Replaces_Element()
        {
            var rootNode = BuildNodeWithFilledOutSubtrees();
            var tree = new BinarySearchTree<int>(rootNode);
            var newLeftChild = rootNode.LeftChild.RightChild.Data;
            var newRightChild = rootNode.RightChild.RightChild.Data;
            var newRootNode = rootNode.RightChild.LeftChild.Data;

            tree.Remove(rootNode.LeftChild.Data);

            rootNode.LeftChild.Data.Should().Be(newLeftChild);

            tree.Remove(rootNode.RightChild.Data);

            rootNode.RightChild.Data.Should().Be(newRightChild);

            tree.Remove(rootNode.Data);

            rootNode.Data.Should().Be(newRootNode);
            rootNode.RightChild.Data.Should().Be(newRightChild);
            rootNode.LeftChild.Data.Should().Be(newLeftChild);
        }

        private static Node<int> BuildNodeWithFilledOutSubtrees()
        {
            var rootNode = new Node<int>
            {
                Data = 5
            };
            var tree = new BinarySearchTree<int>(rootNode);

            // Populate left subtree.
            tree.Insert(3);
            tree.Insert(2);
            tree.Insert(4);

            // Populate right subtree.
            tree.Insert(7);
            tree.Insert(6);
            tree.Insert(8);

            return rootNode;
        }
    }
}
