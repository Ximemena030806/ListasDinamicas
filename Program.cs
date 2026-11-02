using System;
using System.Text;

namespace ListasDinamicas
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            bool salir = false;

            while (!salir)
            {
                Console.WriteLine("\n==========================================");
                Console.WriteLine("   LISTAS Y COLECCIONES DINÁMICAS");
                Console.WriteLine("==========================================");
                Console.WriteLine(" -- Implementación propia --");
                Console.WriteLine("1. Lista doblemente enlazada (menú interactivo)");
                Console.WriteLine("2. Lista doblemente enlazada (demostración automática)");
                Console.WriteLine(" -- Clases genéricas predefinidas de .NET --");
                Console.WriteLine("3. Stack<T>   (pila)");
                Console.WriteLine("4. Queue<T>   (cola)");
                Console.WriteLine("5. List<T>    (lista / arreglo dinámico)");
                Console.WriteLine("6. LinkedList<T> (lista doblemente enlazada)");
                Console.WriteLine("7. Ejecutar las 4 demostraciones de .NET");
                Console.WriteLine("0. Salir");
                Console.Write("Opción: ");
                string opcion = Console.ReadLine();
                Console.WriteLine();

                switch (opcion)
                {
                    case "1": MenuListaDoble(); break;
                    case "2": DemoAutomaticaListaDoble(); break;
                    case "3": ColeccionesNativas.DemoPila(); break;
                    case "4": ColeccionesNativas.DemoCola(); break;
                    case "5": ColeccionesNativas.DemoLista(); break;
                    case "6": ColeccionesNativas.DemoListaEnlazada(); break;
                    case "7":
                        ColeccionesNativas.DemoPila();
                        ColeccionesNativas.DemoCola();
                        ColeccionesNativas.DemoLista();
                        ColeccionesNativas.DemoListaEnlazada();
                        break;
                    case "0": salir = true; break;
                    default: Console.WriteLine("Opción no válida."); break;
                }
            }
        }

        // ------------------------------------------------------------ Menú interactivo
        private static void MenuListaDoble()
        {
            ListaDoble<int> lista = new ListaDoble<int>();
            bool volver = false;

            Console.WriteLine("Lista doblemente enlazada vacía creada (sin capacidad fija).");

            while (!volver)
            {
                Console.WriteLine("\n1.AddFirst  2.AddLast  3.InsertAt  4.RemoveFirst  5.RemoveLast");
                Console.WriteLine("6.RemoveAt  7.Remove(valor)  8.Buscar  9.Invertir  10.Imprimir  0.Volver");
                Console.Write("Opción: ");
                string op = Console.ReadLine();

                try
                {
                    switch (op)
                    {
                        case "1": lista.AddFirst(Entrada.LeerEntero("Valor: ", int.MinValue, int.MaxValue)); break;
                        case "2": lista.AddLast(Entrada.LeerEntero("Valor: ", int.MinValue, int.MaxValue)); break;
                        case "3":
                            int pos = Entrada.LeerEntero("Posición (0 - " + lista.Count + "): ", 0, int.MaxValue);
                            lista.InsertAt(pos, Entrada.LeerEntero("Valor: ", int.MinValue, int.MaxValue));
                            break;
                        case "4": Console.WriteLine("   Eliminado: " + lista.RemoveFirst()); break;
                        case "5": Console.WriteLine("   Eliminado: " + lista.RemoveLast()); break;
                        case "6":
                            Console.WriteLine("   Eliminado: " + lista.RemoveAt(Entrada.LeerEntero("Posición: ", 0, int.MaxValue)));
                            break;
                        case "7":
                            int valor = Entrada.LeerEntero("Valor a eliminar: ", int.MinValue, int.MaxValue);
                            Console.WriteLine(lista.Remove(valor) ? "   Eliminado." : "   El valor no existe en la lista.");
                            break;
                        case "8":
                            int buscado = Entrada.LeerEntero("Valor a buscar: ", int.MinValue, int.MaxValue);
                            int indice = lista.IndexOf(buscado);
                            Console.WriteLine(indice >= 0 ? "   Encontrado en la posición " + indice : "   No encontrado.");
                            break;
                        case "9": lista.Reverse(); Console.WriteLine("   Lista invertida."); break;
                        case "10": break;
                        case "0": volver = true; continue;
                        default: Console.WriteLine("Opción no válida."); continue;
                    }
                }
                catch (InvalidOperationException ex) { Console.WriteLine("   Error: " + ex.Message); }
                catch (ArgumentOutOfRangeException ex) { Console.WriteLine("   Error: " + ex.Message); }

                lista.ImprimirAdelante();
                lista.ImprimirAtras();
            }
        }

        // ------------------------------------------------------------ Demostración automática
        private static void DemoAutomaticaListaDoble()
        {
            Console.WriteLine("=== LISTA DOBLEMENTE ENLAZADA: DEMOSTRACIÓN ===\n");
            ListaDoble<string> lista = new ListaDoble<string>();

            Console.WriteLine("AddLast(\"B\"), AddLast(\"C\"), AddFirst(\"A\"):");
            lista.AddLast("B"); lista.AddLast("C"); lista.AddFirst("A");
            lista.ImprimirAdelante();

            Console.WriteLine("\nInsertAt(2, \"B2\") -> queda entre B y C:");
            lista.InsertAt(2, "B2");
            lista.ImprimirAdelante();
            lista.ImprimirAtras();

            Console.WriteLine("\nIndexOf(\"C\") = " + lista.IndexOf("C") + " | lista[1] = " + lista[1]);

            Console.WriteLine("\nReverse():");
            lista.Reverse();
            lista.ImprimirAdelante();

            Console.WriteLine("\nRemove(\"B2\"), RemoveFirst(), RemoveLast():");
            lista.Remove("B2");
            Console.WriteLine("   RemoveFirst -> " + lista.RemoveFirst());
            Console.WriteLine("   RemoveLast  -> " + lista.RemoveLast());
            lista.ImprimirAdelante();

            Console.WriteLine("\nRecorrido con foreach:");
            foreach (string elemento in lista) Console.WriteLine("   " + elemento);

            lista.Clear();
            Console.WriteLine("\nClear():");
            lista.ImprimirAdelante();
        }
    }
}
