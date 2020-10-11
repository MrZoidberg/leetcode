using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using Xunit;

namespace Leetcode.IntersectionTwoLinkedLists
{
    public class IntersectionTwoLinkedListsTests
    {
        [Theory]
        [ClassData(typeof(IntersectionTwoLinkedListsTestDate))]
        public void Test_Hashset(ListNode headA, ListNode headB, int? intersectionValue)
        {
            var solution = new IntersectionTwoLinkedListsSolution();
            var output = solution.GetIntersectionNode(headA, headB);
            if (!intersectionValue.HasValue)
            {
                output.Should().BeNull();
            }
            else
            {
                output.val.Should().Be(intersectionValue);
            }
        }
    }

    public class IntersectionTwoLinkedListsTestDate : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            var test = GetListNodesFromArray(new[] { 4, 1, 8, 4, 5 }, new[] { 5, 6, 1, 8, 4, 5 }, 8);
            yield return new object[] { test.a, test.b, 8 };

            test = GetListNodesFromArray(new[] { 1, 9, 1, 2, 4 }, new[] { 3, 2, 4 }, 2);
            yield return new object[] { test.a, test.b, 2 };

            test = GetListNodesFromArray(new[] { 2, 6, 4 }, new[] { 1, 5 }, null);
            yield return new object[] { test.a, test.b, null };
        }

        private static (ListNode a, ListNode b) GetListNodesFromArray(int[] arrayA, int[] arrayB, int? intersection)
        {
            ListNode nodeA = new ListNode(arrayA[0]);
            ListNode currentNodeA = nodeA;
            int posA = 1;
            while (posA < arrayA.Length && arrayA[posA] != intersection)
            {
                ListNode nextNode = new ListNode(arrayA[posA]);
                currentNodeA.next = nextNode;
                currentNodeA = nextNode;
                posA++;
            }

            ListNode nodeB = new ListNode(arrayB[0]);
            ListNode currentNodeB = nodeB;
            int posB = 1;
            while (posB < arrayB.Length && arrayB[posB] != intersection)
            {
                ListNode nextNode = new ListNode(arrayB[posB]);
                currentNodeB.next = nextNode;
                currentNodeB = nextNode;
                posB++;
            }

            if (intersection != null)
            {
                for (int i = posA; i < arrayA.Length; i++)
                {
                    ListNode nextNode = new ListNode(arrayA[i]);
                    currentNodeA.next = nextNode;
                    if (currentNodeB.next == null)
                    {
                        currentNodeB.next = nextNode;
                    }
                    currentNodeA = nextNode;
                }
            }

            return (nodeA, nodeB);
        }


        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }


    public class ListNode
    {
        public int val;
        public ListNode next;
        public ListNode(int x) { val = x; }
    }

    public class IntersectionTwoLinkedListsSolution
    {
        public ListNode GetIntersectionNode(ListNode headA, ListNode headB)
        {
            HashSet<ListNode> hashSet = new HashSet<ListNode>();
            ListNode curNodeA = headA;
            ListNode curNodeB = headB;
            while (curNodeA != null)
            {
                hashSet.Add(curNodeA);
                curNodeA = curNodeA.next;
            }

            while (curNodeB != null)
            {
                if (hashSet.Contains(curNodeB))
                {
                    return curNodeB;
                }

                hashSet.Add(curNodeB);
                curNodeB = curNodeB.next;
            }

            return null;
        }
    }
}
