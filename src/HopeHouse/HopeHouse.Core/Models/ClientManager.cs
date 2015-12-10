using System.Collections.Generic;
using System.Linq;
using HopeHouse.Core.Models;
using System;

namespace HopeHouse.Core.DataAccess
{
    public static class ClientManager
    {
        #region Private Fields

        private static List<Client> _clients = new List<Client>();
        private static bool _areClientsLoaded;

        #endregion

        #region Clients Loaded Event

        public delegate void ClientsLoadedHandler();
        public static event ClientsLoadedHandler ClientsLoaded;
        private static void OnClientsLoaded()
        {
            if (ClientsLoaded != null)
            {
                ClientsLoaded();
            }
        }

        #endregion

        #region Client Added Event

        public delegate void ClientAddedHandler(Client client);
        public static event ClientAddedHandler ClientAdded;
        private static void OnClientAdded(Client client)
        {
            if(ClientAdded != null)
            {
                ClientAdded(client);
            }
        }

        #endregion

        #region Client Deleted Event

        public delegate void ClientDeletedHandler(Client client);
        public static event ClientAddedHandler ClientDeleted;
        private static void OnClientDeleted(Client client)
        {
            if (ClientDeleted != null)
            {
                ClientDeleted(client);
            }
        }

        #endregion

        #region Public Methods

        public static bool AreClientsLoaded()
        {
            return _areClientsLoaded;
        }

        public static void LoadClients()
        {
            _areClientsLoaded = false;

            //
            // TO USE DATABASE CLIENTS, COMMENT OUT FIRST LINE AND UNCOMMENT SECOND LINE
            //
            //_clients.AddRange(ClientGenerator.GenerateClients(1000));
            _clients.AddRange(ClientDatabaseHelper.GetAllClients());

            _areClientsLoaded = true;
            OnClientsLoaded();
        }

        public static void AddClient(Client client)
        {
            ClientDatabaseHelper.AddClient(client);
            _clients.Add(client);
            OnClientAdded(client);
        }

        public static void AddClientRange(IEnumerable<Client> clientRange)
        {
            _clients.AddRange(clientRange);
        }

        public static int GetNumberOfClients()
        {
            return _clients.Count;
        }

        public static List<Client> GetAllClients()
        {
            return _clients;
        }

        public static List<Client> GetFilteredClients(Filter filter, List<Client> clientsToCheck = null)
        {
            if (clientsToCheck != null)
            {
                return clientsToCheck.Where(x => filter.CheckFilter(x) == true).ToList();
            }

            return _clients.Where(x => filter.CheckFilter(x)).ToList();
        } 

        public static void ImportClients(string fileName, Action<int> reportClientsImported)
        {
            List<Client> importedClients = ExcelParser.ReadExcelFile(fileName, reportClientsImported);
            _clients.AddRange(importedClients);
            
            foreach(Client client in importedClients)
            {
                ClientDatabaseHelper.AddClient(client);
            }
        }

        public static void DeleteClient(Client client)
        {
            ClientDatabaseHelper.DeleteClient(client);
            _clients.Remove(client);
            OnClientDeleted(client);
        }

        #endregion
    }
}
