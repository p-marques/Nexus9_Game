using System.Collections.Generic;

public static class TLinkExtensions
{
    public static T[] ToArray<T>(this LinkedList<T> linkedList)
    {
        T[] result = new T[linkedList.Count];
        LinkedListNode<T> node = linkedList.First;

        for (int i = 0; node != null; node = node.Next, i++)
        {
            result[i] = node.Value;
        }

        return result;
    }
}
