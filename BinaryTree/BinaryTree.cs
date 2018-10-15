using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryTree
{
    class BinaryTree<T> : IEnumerable<T>
        where T : IComparable<T>
    {
        BinaryTreeNode<T> head;
        int count;

        public void Add(T value)
        {
            // Case 1: The tree is empty - allocate to head
            if (head == null)
            {
                head = new BinaryTreeNode<T>(value);
            }

            // Case 2: The tree is not empty so find the right location to insert
            else
            {
                AddTo(head, value);
            }

            count++;
        }

        // Recursive add algorithm
        private void AddTo(BinaryTreeNode<T> node, T value)
        {
            // Case 1: Value is less than the current node value
            if (value.CompareTo(node.Value) < 0)
            {
                // If there is no Left child, make this the new Left
                if (node.Left == null)
                {
                    node.Left = new BinaryTreeNode<T>(value);
                }

                // Else add it to the Left node
                else
                {
                    AddTo(node.Left, value);
                }
            }

            // Case 2: Value is equal to or greater than the current value
            else
            {
                // If there is no Right child, make this the new Right
                if (node.Right == null)
                {
                    node.Right = new BinaryTreeNode<T>(value);
                }

                // Else add it to the Right node
                else
                {
                    AddTo(node.Right, value);
                }
            }
        }

        public bool Contains(T value)
        {
            // Defer to the node search helper function
            BinaryTreeNode<T> parent;
            return FindWithParent(value, out parent) != null;
        }


        // Finds and returns the first node containing the specified value.
        // If the value is not found, it returns null. 
        // Also returns the parent of the found node (or null).
        private BinaryTreeNode<T> FindWithParent(T value, out BinaryTreeNode<T> parent)
        {
            // Try to find the data in the tree
            BinaryTreeNode<T> current = head;
            parent = null;

            // While we dont have a match
            while (current != null)
            {
                int result = current.CompareTo(value);

                // If the value is less than current, go left
                if (result > 0)
                {
                    parent = current;
                    current = current.Left;
                }

                // If the value is more than current, go right
                else if (result < 0)
                {
                    parent = current;
                    current = current.Right;
                }

                // Else, we have a match
                else
                {
                    break;
                }
            }

            return current;
        }

        // Removes the first occurrence of of the specified value from the tree
        public bool Remove(T value)
        {
            BinaryTreeNode<T> current, parent;
            current = FindWithParent(value, out parent);

            // 
            if (current == null)
            {
                return false;
            }

            count--;

            // Case 1: If current has no right child, then the current's left child replaces current
            if (current.Right == null)
            {
                // If true, then we remove the root node
                if (parent == null)
                {
                    head = current.Left;
                }

                else
                {
                    int result = parent.CompareTo(current.Value);

                    // If parent value is greater than current value, make the current left child a left child of parent
                    if (result > 0)
                    {
                        parent.Left = current.Left;
                    }

                    // If parent value is less than current value, make the current left child a right child of parent
                    else if (result < 0)
                    {
                        parent.Right = current.Right;
                    }
                }
            }

            // Case 2: If the current's right child has no left, then current's right child replaces current.
            else if (current.Right.Left == null)
            {
                current.Right.Left = current.Left;

                if (parent == null)
                {
                    head = current.Right;
                }

                else
                {
                    int result = parent.CompareTo(current.Value);

                    // If parent value is greater than current value,
                    // make the current right child a left child of parent
                    if (result > 0)
                    {
                        parent.Left = current.Right;
                    }

                    // If parent value is less than current value,
                    // make the current right child a right child of parent
                    else if (result < 0)
                    {
                        parent.Right = current.Right;
                    }
                }
            }

            // Case 3: If current's right child has a left child,
            // replace current with current's right child's left-most child.
            else
            {
                // Find the right's left-most child
                BinaryTreeNode<T> leftmost = current.Right.Left;
                BinaryTreeNode<T> leftmostParent = current.Right;

                while (leftmost.Left != null)
                {
                    leftmostParent = leftmost;
                    leftmost = leftmost.Left;
                }

                // The parent's left subtree becomes the leftmost's right subtree
                leftmostParent.Left = leftmost.Right;

                // Assign leftmost's left and right to current's left and right children
                leftmost.Left = current.Left;
                leftmost.Right = current.Right;

                if (parent == null)
                {
                    head = leftmost;
                }

                else
                {
                    int result = parent.CompareTo(current.Value);

                    // If parent value is greater than current value, make leftmost the parent's left child
                    if (result > 0)
                    {
                        parent.Left = leftmost;
                    }

                    // If parent value is right than current value, make leftmost the parent's right child
                    else if (result < 0)
                    {
                        parent.Right = leftmost;
                    }
                }
            }

            return true;
        }

        public void PreOrderTraversal(Action<T> action)
        {
            PreOrderTraversal(action, head);
        }

        public void PreOrderTraversal(Action<T> action, BinaryTreeNode<T> node)
        {
            if (node != null)
            {
                action(node.Value);
                PreOrderTraversal(action, node.Left);
                PreOrderTraversal(action, node.Right);
            }
        }

        public void PostOrderTraversal(Action<T> action)
        {
            PostOrderTraversal(action, head);
        }

        public void PostOrderTraversal(Action<T> action, BinaryTreeNode<T> node)
        {
            if (node != null)
            {
                PostOrderTraversal(action, node.Left);
                PostOrderTraversal(action, node.Right);
                action(node.Value);
            }
        }

        public void InOrderTraversal(Action<T> action)
        {
            InOrderTraversal(action, head);
        }

        public void InOrderTraversal(Action<T> action, BinaryTreeNode<T> node)
        {
            //
            if (node != null)
            {
                InOrderTraversal(action, node.Left);
                action(node.Value);
                InOrderTraversal(action, node.Right);
            }
        }

        // Non-recursive algorithm using a stack to demonstrate removing 
        // recursion to make using the yield syntax easier.
        public IEnumerator<T> InOrderTraversal()
        {
            if (head != null)
            {
                // Store the nodes we've skipped in this stack (avoids recursion)
                Stack<BinaryTreeNode<T>> stack = new Stack<BinaryTreeNode<T>>();

                BinaryTreeNode<T> current = head;

                // When removing recursion we need to keep track of whether or not
                // we should go to the left node or the right nodes next.
                bool goLeftNext = true;

                // Start by pushing head onto the stack
                stack.Push(current);

                while (stack.Count > 0)
                {
                    // If we're heading left
                    if (goLeftNext)
                    {
                        // Push everything but the left-most node to the stack
                        // we'll yield the left-most after this block
                        while (current.Left != null)
                        {
                            stack.Push(current);
                            current = current.Left;
                        }
                    }

                    // In-order is left -> yield -> right
                    yield return current.Value;

                    // If we can go right
                    if (current.Right != null)
                    {
                        current = current.Right;

                        // Once we've gone right once, we need to start going left again
                        goLeftNext = true;
                    }

                    else
                    {
                        // If we can't go right, then we need to pop off the parent node
                        // so we can process it and then go to it's right node
                        current = stack.Pop();
                        goLeftNext = false;
                    }
                }
            }
        }

        public int Count
        {
            get
            {
                return count;
            }
        }

        public void Clear()
        {
            head = null;   
        }

        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();

        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
