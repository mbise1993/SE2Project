using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using HopeHouse.Core.Models;
using HopeHouse.Common.Logging;

namespace HopeHouse.Core.DataAccess
{
    public static class ExcelParser
    {
        /// <summary>
        /// Code is currently in the process of being worked on. Method currently reads in the first 
        /// row of the Clients worksheet. 
        /// </summary>
        /// <param name="fileName">Excel path to be read</param>
        /// <param name="reportClientsImported">Action taken when another client is imported</param>
        public static List<Client> ReadExcelFile(string fileName, Action<int> reportClientsImported)
        {
            List<Client> existingClientList = new List<Client>();
            string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileName +
                                      ";Extended Properties='Excel 12.0;HDR=YES;TypeGuessRows=0;ImportMixedTypes=Text;IMEX=1'";
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string sqlCmd = "SELECT  * FROM [Clients$]";
                    OleDbCommand command = new OleDbCommand(sqlCmd, connection);
                    command.CommandType = CommandType.Text;
                    OleDbDataReader reader = command.ExecuteReader();
                    ParserHelper readHelper = new ParserHelper(reader);
                    while (reader != null && reader.Read())
                    {
                        if (readHelper.GetClientName() == "" || readHelper.GetClientName() == "Clients") continue;
                        Client parsedClient = new Client();
                        parsedClient.FirstName = readHelper.GetClientFirstName();
                        parsedClient.LastName = readHelper.GetClientLastName();
                        parsedClient.MiddleInit = readHelper.GetClientMiddleInit();
                        parsedClient.MaritalStatus = readHelper.GetClientMartialStatus();
                        parsedClient.Race = readHelper.GetClientEthnicity();
                        parsedClient.CurrentlyPregnant = readHelper.IsClientCurrentlyPregnant();
                        parsedClient.Smoke = readHelper.DoesClientSmoke();
                        parsedClient.DateEntered = readHelper.GetClientAppDate();
                        existingClientList.Add(parsedClient);
                        reportClientsImported(existingClientList.Count);
                    }

                }
                catch (Exception e)
                {
                    if(e.Message.Contains("not registered"))
                    {
                        Logger.WriteErrorLogEntry(new ErrorLogEntry(e));
                        throw;
                    }

                    Logger.WriteErrorLogEntry(new ErrorLogEntry(e));
                }

                return existingClientList;
            }
        }
    }
}
