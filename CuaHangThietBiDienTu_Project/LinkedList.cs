/* LinkedList.cs
 * Name: Nguyen Le Tam
 * Date: 21/07/2022
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class LinkedList<T>
{
    //Fields
    private Node<T> first;
    private Node<T> last;

    //Properties
    internal Node<T> First { get => first; set => first = value; }
    internal Node<T> Last { get => last; set => last = value; }

    //Constructors
    public LinkedList()
    {
        first = null;
        last = null;
    }

    //Methods
    public void AddLast(Node<T> value)
    {
        if (first == null) //Nếu ds rỗng
        {
            first = last = value;
        }
        else
        {
            last.Next = value;
            last = value;
        }
    }

    public void RemoveFirst()
    {
        first = first.Next;
    }

    public void Remove(Node<T> value)
    {
        if (first == value)
        {
            RemoveFirst();
        }
        else
        {
            Node<T> p = first;
            while (p.Next != null)
            {
                if (p.Next == value)
                {
                    p.Next = p.Next.Next;
                }
            }
        }
    }

    public int Count()
    {
        int dem = 0;
        Node<T> p = first;
        while (p != null)
        {
            dem++;
            p = p.Next;
        }
        return dem;
    }

    public override string ToString()
    {
        string s = "";
        Node<T> p = first;
        while (p != null)
        {
            s += "\n" + p.Data;
            p = p.Next;
        }
        return s;
    }
}