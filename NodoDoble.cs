namespace ListasDinamicas
{
    /// <summary>
    /// Nodo de una lista doblemente enlazada.
    ///
    /// Es una CLASE (tipo por referencia): cada nodo se crea en el Heap con 'new' en el momento
    /// en que se necesita (memoria dinámica) y se enlaza con sus vecinos mediante referencias.
    /// Cuando un nodo queda sin referencias, el Garbage Collector libera su memoria.
    /// </summary>
    public class NodoDoble<T>
    {
        public T Valor;                       // Dato almacenado
        public NodoDoble<T> Siguiente;        // Referencia al nodo posterior (null si es el último)
        public NodoDoble<T> Anterior;         // Referencia al nodo previo (null si es el primero)

        public NodoDoble(T valor)
        {
            Valor = valor;
            Siguiente = null;
            Anterior = null;
        }
    }
}
