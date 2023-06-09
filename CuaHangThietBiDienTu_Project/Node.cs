/* Node.cs
 * Name: Nguyen Le Tam
 * Date: 21/07/2022
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Node<T>
{
    //Fields
    private T data;
    private Node<T> next;

    //Properties       
    internal T Data { get => data; set => data = value; }
    internal Node<T> Next { get => next; set => next = value; }

    //Constructors
    public Node(T data)
    {
        this.data = data;
        next = null;
    }

    public override string ToString()
    {
        return data.ToString();
    }
}