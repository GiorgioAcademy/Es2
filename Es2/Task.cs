using System;
using System.Collections.Generic;

namespace Es2
{
    enum Formato
    {
        Plain,
        CSV
    }

    class Task
    {
        public int Id { get; }
        public string Descrizione { get; }
        public DateTime DataScadenza { get; }
        public Livello Livello { get; }

        public Task(int id, string descrizione, DateTime dataScadenza, Livello livello)
        {
            if (dataScadenza < DateTime.Today)
                throw new ArgumentOutOfRangeException();

            Id = id;
            Descrizione = descrizione;
            DataScadenza = dataScadenza;
            Livello = livello;
        }

        public string OttieniDati(Formato formato)
        {
            switch (formato)
            {
                case Formato.Plain:
                    return $"Id: {Id}, {Descrizione}, Scadenza: {DataScadenza.ToShortDateString()}, Livello: {Livello}";
                case Formato.CSV:
                    return $"{Id}\t{Descrizione}\t{DataScadenza.ToShortDateString()}\t{Livello}";
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
