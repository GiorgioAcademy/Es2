using System;
using System.IO;
using System.Collections.Generic;

namespace Es2
{
    class Program
    {
        // mappina di associazione tra tasto premuto e livello
        private static Dictionary<ConsoleKey, Livello> _map = 
            new Dictionary<ConsoleKey, Livello>()
            { { ConsoleKey.B, Livello.Basso},
            { ConsoleKey.M, Livello.Medio},
            { ConsoleKey.A, Livello.Alto}};

        private static TaskManager taskManager = new TaskManager();
        static void Main(string[] args)
        {
            Console.WriteLine("Gestione tasks");

            do
            {
                Console.WriteLine();
                Console.WriteLine("1. Crea task");
                Console.WriteLine("2. Visualizza tasks");
                Console.WriteLine("3. Filtra tasks");
                Console.WriteLine("4. Salva su file");
                Console.WriteLine("5. Elimina task");
                Console.WriteLine("0. Esci");

                switch (Console.ReadKey().KeyChar)
                {
                    case '1':
                        CreaTask();
                        break;
                    case '2':
                        Visualizza();
                        break;
                    case '3':
                        Filtra();
                        break;
                    case '4':
                        Salva();
                        break;
                    case '5':
                        EliminaTask();
                        break;
                    case '0':
                        return;
                    default:
                        Console.WriteLine("Scelta non valida");
                        break;
                }
            } while (true);
        }

        // visualizza i tasks in base al livello richiesto
        private static void Filtra()
        {
            Console.WriteLine();
            ConsoleKey key;

            do
            {
                Console.Write("Quale livello vuoi visualizzare (Basso, Medio, Alto): ");
                key = Console.ReadKey().Key;
            } while (!_map.ContainsKey(key));

            Console.WriteLine();
            Console.WriteLine(taskManager.OttieniElenco(_map[key], Formato.Plain));
        }

        // richiede l'id per eliminare il task
        private static void EliminaTask()
        {
            Console.WriteLine();
            int id;
            do
                Console.Write("ID del task da eliminare: ");
            while (!int.TryParse(Console.ReadLine(), out id));

            if (taskManager.EliminaTask(id))
                Console.WriteLine($"Eliminato task {id}");
            else
                Console.WriteLine("Task inesistente");
        }

        private static void Salva() // salva l'elenco dei task in formato csv
        {
            const string fileName = @"tasks.csv";   // nome file fissato da codice per comodità
            Formato formato = Formato.CSV;          // così come il formato

            using (StreamWriter sw = new StreamWriter(fileName))
                sw.WriteLine(taskManager.OttieniElenco(Livello.Tutti, formato));
        }

        private static void Visualizza()
        {
            Console.WriteLine();
            Console.WriteLine(taskManager.OttieniElenco(Livello.Tutti, Formato.Plain));
        }

        // richiede tutti i dati per la creazione di un nuovo task
        private static void CreaTask()
        {
            string descrizione;
            DateTime dataScadenza;

            Console.WriteLine();
            Console.Write("Descrizione del task: ");
            descrizione = Console.ReadLine();

            do
            {
                Console.Write("Data di scadenza (a partire da oggi): ");
            } while (!DateTime.TryParse(Console.ReadLine(), out dataScadenza) || dataScadenza < DateTime.Today);

            ConsoleKey key;

            do
            {
                Console.Write("Livello di importanza (Basso, Medio, Alto): ");
                key = Console.ReadKey().Key;
            } while (!_map.ContainsKey(key));

            Console.WriteLine();

            Task t = taskManager.CreaTask(descrizione, dataScadenza, _map[key]);
            Console.WriteLine($"Task {t.Id} creato.");
        }
    }
}
