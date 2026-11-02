using System;
using System.Collections.Generic;

namespace ListasDinamicas
{
    /// <summary>
    /// Uso de las clases genéricas dinámicas predefinidas de .NET (System.Collections.Generic):
    /// Stack&lt;T&gt;, Queue&lt;T&gt;, List&lt;T&gt; y LinkedList&lt;T&gt;.
    /// Todas crecen automáticamente en tiempo de ejecución, sin que el programador gestione la memoria.
    /// </summary>
    public static class ColeccionesNativas
    {
        // ---------------------------------------------------------------- Stack<T> (LIFO)
        // Internamente es un arreglo que se redimensiona (como el DynamicArray del curso).
        public static void DemoPila()
        {
            Console.WriteLine("=== Stack<T>: Historial de páginas visitadas (LIFO) ===");
            Stack<string> historial = new Stack<string>();

            historial.Push("inicio.com");
            historial.Push("noticias.com");
            historial.Push("deportes.com");
            Console.WriteLine("Push x3 -> Count = " + historial.Count);
            Console.WriteLine("Peek (página actual) : " + historial.Peek());
            Console.WriteLine("Contains(\"inicio.com\"): " + historial.Contains("inicio.com"));

            Console.WriteLine("Botón 'atrás' (Pop)  : " + historial.Pop());
            Console.WriteLine("Página actual ahora  : " + historial.Peek());

            Console.Write("Contenido (cima -> base): ");
            foreach (string pagina in historial) Console.Write(pagina + "  ");
            Console.WriteLine("\n");
        }

        // ---------------------------------------------------------------- Queue<T> (FIFO)
        // Internamente es un arreglo circular que se redimensiona.
        public static void DemoCola()
        {
            Console.WriteLine("=== Queue<T>: Cola de impresión (FIFO) ===");
            Queue<string> impresion = new Queue<string>();

            impresion.Enqueue("informe.pdf");
            impresion.Enqueue("foto.png");
            impresion.Enqueue("tarea.docx");
            Console.WriteLine("Enqueue x3 -> Count = " + impresion.Count);
            Console.WriteLine("Peek (siguiente a imprimir): " + impresion.Peek());

            while (impresion.Count > 0)
                Console.WriteLine("Imprimiendo (Dequeue)      : " + impresion.Dequeue());

            Console.WriteLine("¿Cola vacía? Count = " + impresion.Count + "\n");
        }

        // ---------------------------------------------------------------- List<T>
        // Internamente es un arreglo dinámico: duplica su capacidad cuando se llena.
        public static void DemoLista()
        {
            Console.WriteLine("=== List<T>: Lista de calificaciones ===");
            List<int> notas = new List<int>();

            Console.WriteLine("Capacity inicial: " + notas.Capacity);
            for (int i = 0; i < 6; i++)
            {
                notas.Add(70 + i * 5);
                Console.WriteLine("Add(" + (70 + i * 5) + ") -> Count = " + notas.Count + ", Capacity = " + notas.Capacity);
            }

            notas.Insert(2, 100);
            Console.WriteLine("Insert(2, 100)  : " + string.Join(", ", notas));

            notas.Remove(75);
            Console.WriteLine("Remove(75)      : " + string.Join(", ", notas));

            Console.WriteLine("IndexOf(100)    : " + notas.IndexOf(100));
            Console.WriteLine("Contains(999)   : " + notas.Contains(999));

            notas.Sort();
            Console.WriteLine("Sort()          : " + string.Join(", ", notas));

            notas.Reverse();
            Console.WriteLine("Reverse()       : " + string.Join(", ", notas));

            int primeraMayorA90 = notas.Find(n => n > 90);
            Console.WriteLine("Find(n > 90)    : " + primeraMayorA90 + "\n");
        }

        // ---------------------------------------------------------------- LinkedList<T>
        // Lista doblemente enlazada de .NET: equivalente a la ListaDoble implementada a mano.
        public static void DemoListaEnlazada()
        {
            Console.WriteLine("=== LinkedList<T>: Lista de reproducción (doblemente enlazada) ===");
            LinkedList<string> canciones = new LinkedList<string>();

            canciones.AddLast("Canción B");
            canciones.AddLast("Canción C");
            canciones.AddFirst("Canción A");
            Console.WriteLine("AddLast x2, AddFirst : " + string.Join(" <-> ", canciones));

            // LinkedListNode<T> permite insertar relativo a un nodo concreto en O(1).
            LinkedListNode<string> nodoB = canciones.Find("Canción B");
            canciones.AddAfter(nodoB, "Canción B2");
            canciones.AddBefore(nodoB, "Canción A2");
            Console.WriteLine("AddAfter/AddBefore   : " + string.Join(" <-> ", canciones));

            Console.WriteLine("First / Last         : " + canciones.First.Value + " / " + canciones.Last.Value);
            Console.WriteLine("Siguiente de B       : " + nodoB.Next.Value);
            Console.WriteLine("Anterior de B        : " + nodoB.Previous.Value);

            canciones.Remove("Canción A2");
            canciones.RemoveFirst();
            canciones.RemoveLast();
            Console.WriteLine("Remove/First/Last    : " + string.Join(" <-> ", canciones));

            Console.Write("Recorrido inverso    : ");
            for (LinkedListNode<string> n = canciones.Last; n != null; n = n.Previous)
                Console.Write(n.Value + "  ");
            Console.WriteLine("\n");
        }
    }
}
