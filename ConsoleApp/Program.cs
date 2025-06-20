using System;
using System.Collections.Generic;
using DocumentFilteringApp.Application;
using DocumentFilteringApp.Domain.Entities;
using ConsoleApp;

namespace DocumentFilteringApp.ConsoleApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                // Lê os documentos da entrada padrão
                List<Document> documents = ConsoleInputReader.ReadDocuments();

                // Cria instância do serviço de aplicação
                var service = new DocumentService();

                // Processa os documentos e obtém os IDs descartados
                List<int> discardedIds = service.GetDiscardedScanIds(documents);

                // Escreve os IDs descartados na saída padrão
                ConsoleOutputWriter.WriteDiscardedScanIds(discardedIds);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}