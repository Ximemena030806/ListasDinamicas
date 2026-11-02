using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ListasDinamicas
{
    /// <summary>
    /// Lista doblemente enlazada genérica implementada desde cero con nodos dinámicos.
    ///
    /// Referencias principales:
    ///   head = primer nodo (null si la lista está vacía)
    ///   tail = último nodo
    /// A diferencia de un arreglo, NO tiene capacidad fija: crece o se reduce nodo por nodo.
    ///
    /// Complejidad:
    ///   AddFirst / AddLast / RemoveFirst / RemoveLast : O(1)
    ///   InsertAt / RemoveAt / IndexOf / Remove(valor) : O(n)
    /// </summary>
    public class ListaDoble<T> : IEnumerable<T>
    {
        private NodoDoble<T> head;
        private NodoDoble<T> tail;
        private int count;

        public int Count { get { return count; } }
        public bool IsEmpty() { return count == 0; }

        // ---------------------------------------------------------------- Inserción

        // Inserta al inicio: el nuevo nodo pasa a ser head.
        public void AddFirst(T valor)
        {
            NodoDoble<T> nuevo = new NodoDoble<T>(valor); // Reserva dinámica en el Heap

            if (head == null)           // Lista vacía: el nodo es a la vez head y tail
            {
                head = nuevo;
                tail = nuevo;
            }
            else
            {
                nuevo.Siguiente = head; // nuevo -> antiguo head
                head.Anterior = nuevo;  // antiguo head <- nuevo
                head = nuevo;
            }
            count++;
        }

        // Inserta al final: el nuevo nodo pasa a ser tail.
        public void AddLast(T valor)
        {
            NodoDoble<T> nuevo = new NodoDoble<T>(valor);

            if (tail == null)
            {
                head = nuevo;
                tail = nuevo;
            }
            else
            {
                nuevo.Anterior = tail;  // antiguo tail <- nuevo
                tail.Siguiente = nuevo; // antiguo tail -> nuevo
                tail = nuevo;
            }
            count++;
        }

        // Inserta en una posición intermedia: el nuevo nodo queda justo ANTES del nodo que ocupa 'indice'.
        public void InsertAt(int indice, T valor)
        {
            if (indice < 0 || indice > count)
                throw new ArgumentOutOfRangeException("indice", "Índice fuera de rango (0 a " + count + ").");

            if (indice == 0) { AddFirst(valor); return; }
            if (indice == count) { AddLast(valor); return; }

            NodoDoble<T> actual = ObtenerNodo(indice);
            NodoDoble<T> nuevo = new NodoDoble<T>(valor);

            // Se reenlazan las 4 referencias involucradas:
            nuevo.Anterior = actual.Anterior;
            nuevo.Siguiente = actual;
            actual.Anterior.Siguiente = nuevo;
            actual.Anterior = nuevo;
            count++;
        }

        // ---------------------------------------------------------------- Eliminación

        public T RemoveFirst()
        {
            if (head == null) throw new InvalidOperationException("La lista está vacía.");

            T valor = head.Valor;
            DesenlazarNodo(head);
            return valor;
        }

        public T RemoveLast()
        {
            if (tail == null) throw new InvalidOperationException("La lista está vacía.");

            T valor = tail.Valor;
            DesenlazarNodo(tail);
            return valor;
        }

        public T RemoveAt(int indice)
        {
            ValidarIndice(indice);

            NodoDoble<T> nodo = ObtenerNodo(indice);
            T valor = nodo.Valor;
            DesenlazarNodo(nodo);
            return valor;
        }

        // Elimina la primera aparición de 'valor'. Devuelve false si no existe.
        public bool Remove(T valor)
        {
            NodoDoble<T> nodo = BuscarNodo(valor);
            if (nodo == null) return false;

            DesenlazarNodo(nodo);
            return true;
        }

        public void Clear()
        {
            // Basta con soltar head y tail: los nodos quedan sin referencias y el GC los libera.
            head = null;
            tail = null;
            count = 0;
        }

        // ---------------------------------------------------------------- Consulta

        public T this[int indice]
        {
            get
            {
                ValidarIndice(indice);
                return ObtenerNodo(indice).Valor;
            }
            set
            {
                ValidarIndice(indice);
                ObtenerNodo(indice).Valor = value;
            }
        }

        public int IndexOf(T valor)
        {
            int posicion = 0;
            for (NodoDoble<T> n = head; n != null; n = n.Siguiente)
            {
                if (EqualityComparer<T>.Default.Equals(n.Valor, valor)) return posicion;
                posicion++;
            }
            return -1;
        }

        public bool Contains(T valor) { return IndexOf(valor) != -1; }

        // ---------------------------------------------------------------- Operaciones

        /// <summary>
        /// Invierte la lista en el sitio: en cada nodo se intercambian Siguiente y Anterior,
        /// y luego se intercambian head y tail. No se crean nodos nuevos. O(n).
        /// </summary>
        public void Reverse()
        {
            NodoDoble<T> actual = head;
            while (actual != null)
            {
                NodoDoble<T> siguienteOriginal = actual.Siguiente;
                actual.Siguiente = actual.Anterior;
                actual.Anterior = siguienteOriginal;
                actual = siguienteOriginal; // Se avanza usando el enlace original
            }

            NodoDoble<T> temporal = head;
            head = tail;
            tail = temporal;
        }

        // ---------------------------------------------------------------- Impresión

        // Recorre desde head siguiendo 'Siguiente'.
        public void ImprimirAdelante()
        {
            StringBuilder sb = new StringBuilder("   head -> null <-> ");
            for (NodoDoble<T> n = head; n != null; n = n.Siguiente)
                sb.Append("[" + n.Valor + "] <-> ");
            sb.Append("null   (Count = " + count + ")");
            Console.WriteLine(sb.ToString());
        }

        // Recorre desde tail siguiendo 'Anterior' (posible gracias a que la lista es doble).
        public void ImprimirAtras()
        {
            StringBuilder sb = new StringBuilder("   tail -> null <-> ");
            for (NodoDoble<T> n = tail; n != null; n = n.Anterior)
                sb.Append("[" + n.Valor + "] <-> ");
            sb.Append("null");
            Console.WriteLine(sb.ToString());
        }

        // ---------------------------------------------------------------- Internos

        // Busca el nodo desde el extremo más cercano al índice (mitad del recorrido en promedio).
        private NodoDoble<T> ObtenerNodo(int indice)
        {
            NodoDoble<T> actual;
            if (indice < count / 2)
            {
                actual = head;
                for (int i = 0; i < indice; i++) actual = actual.Siguiente;
            }
            else
            {
                actual = tail;
                for (int i = count - 1; i > indice; i--) actual = actual.Anterior;
            }
            return actual;
        }

        private NodoDoble<T> BuscarNodo(T valor)
        {
            for (NodoDoble<T> n = head; n != null; n = n.Siguiente)
                if (EqualityComparer<T>.Default.Equals(n.Valor, valor)) return n;
            return null;
        }

        /// <summary>
        /// Saca un nodo de la cadena reconectando a sus vecinos entre sí.
        /// Casos: nodo en el extremo (actualiza head/tail) o nodo intermedio.
        /// </summary>
        private void DesenlazarNodo(NodoDoble<T> nodo)
        {
            if (nodo.Anterior != null) nodo.Anterior.Siguiente = nodo.Siguiente;
            else head = nodo.Siguiente;      // Era el primero

            if (nodo.Siguiente != null) nodo.Siguiente.Anterior = nodo.Anterior;
            else tail = nodo.Anterior;       // Era el último

            // Se cortan sus referencias: el nodo queda huérfano y el GC lo liberará.
            nodo.Siguiente = null;
            nodo.Anterior = null;
            count--;
        }

        private void ValidarIndice(int indice)
        {
            if (indice < 0 || indice >= count)
                throw new ArgumentOutOfRangeException("indice", "Índice " + indice + " fuera de rango (Count = " + count + ").");
        }

        // Permite usar foreach sobre la lista.
        public IEnumerator<T> GetEnumerator()
        {
            for (NodoDoble<T> n = head; n != null; n = n.Siguiente)
                yield return n.Valor;
        }

        IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }
    }
}
