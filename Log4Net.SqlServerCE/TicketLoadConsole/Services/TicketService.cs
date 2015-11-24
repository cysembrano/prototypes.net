using System;
using System.Collections.Generic;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using TicketLoadConsole.Entities;
using TicketLoadConsole.Util;
using System.Net.Mail;
using System.Data;

namespace TicketLoadConsole.Services
{
    public static class TicketService
    {
        /// <summary>
        /// Uses WebTrackerTicketFormat Query to Oracle.
        /// </summary>
        public static List<WebTrackerTicket> GetWebTrackerTickets(DateTime sDate, DateTime eDate)
        {
            Log4NetManager.Instance.Info(typeof(TicketLoadConsole.Services.TicketService), "GetWebTrackerTickets: Start");
            decimal contractId;
            if (!Decimal.TryParse(ConfigurationManager.AppSettings["WT.ContractId"], out contractId))
            {
                Log4NetManager.Instance.Error(typeof(TicketLoadConsole.Services.TicketService), "GetWebTrackerTickets: WT.ContractId in app.config not a number.");
                return null;
            }

            List<WebTrackerTicket> returnList = new List<WebTrackerTicket>();
            var connSettings = ConfigurationManager.ConnectionStrings["WT"];
            //TODO: Replace sDate and eDate
            string sourceQueryString = String.Format(Properties.Resources.WebTrackerTicketFormat,
                                                    ConfigurationManager.AppSettings["WT.AgentSite"],
                                                    contractId,
                                                    sDate.ToString("MM/dd/yyyy"),
                                                    eDate.ToString("MM/dd/yyyy"));
            using (OracleConnection connection = new OracleConnection(connSettings.ConnectionString))
            {
                OracleCommand command = connection.CreateCommand();
                command.CommandText = sourceQueryString;
                try
                {
                    connection.Open();
                    OracleDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        WebTrackerTicket wt = new WebTrackerTicket();
                        wt.Contract_id = contractId;
                        wt.Log_id = reader.IsDBNull(EWTTicket.Log_id.GetHashCode()) ? null : reader.GetDecimal(EWTTicket.Log_id.GetHashCode()) as decimal?;
                        wt.UserId = reader.IsDBNull(EWTTicket.UserId.GetHashCode()) ? null : reader.GetDecimal(EWTTicket.UserId.GetHashCode()) as decimal?;
                        wt.Cust_FName = reader.IsDBNull(EWTTicket.Cust_FName.GetHashCode()) ? null : reader.GetString(EWTTicket.Cust_FName.GetHashCode());
                        wt.Cust_LName = reader.IsDBNull(EWTTicket.Cust_LName.GetHashCode()) ? null : reader.GetString(EWTTicket.Cust_LName.GetHashCode());
                        wt.Cust_Email = reader.IsDBNull(EWTTicket.Cust_Email.GetHashCode()) ? null : reader.GetString(EWTTicket.Cust_Email.GetHashCode());
                        wt.Cust_Company = reader.IsDBNull(EWTTicket.Cust_Company.GetHashCode()) ? null : reader.GetString(EWTTicket.Cust_Company.GetHashCode());
                        wt.Cust_Phone = reader.IsDBNull(EWTTicket.Cust_Phone.GetHashCode()) ? null : reader.GetString(EWTTicket.Cust_Phone.GetHashCode());
                        wt.Cust_Addr1 = reader.IsDBNull(EWTTicket.Cust_Addr1.GetHashCode()) ? null : reader.GetString(EWTTicket.Cust_Addr1.GetHashCode());
                        wt.Cust_Addr2 = reader.IsDBNull(EWTTicket.Cust_Addr2.GetHashCode()) ? null : reader.GetString(EWTTicket.Cust_Addr2.GetHashCode());
                        wt.Cust_City = reader.IsDBNull(EWTTicket.Cust_City.GetHashCode()) ? null : reader.GetString(EWTTicket.Cust_City.GetHashCode());
                        wt.Cust_State = reader.IsDBNull(EWTTicket.Cust_State.GetHashCode()) ? null : reader.GetString(EWTTicket.Cust_State.GetHashCode());
                        wt.Cust_ZipCode = reader.IsDBNull(EWTTicket.Cust_ZipCode.GetHashCode()) ? null : reader.GetString(EWTTicket.Cust_ZipCode.GetHashCode());
                        wt.Cust_Country = reader.IsDBNull(EWTTicket.Cust_Country.GetHashCode()) ? null : reader.GetString(EWTTicket.Cust_Country.GetHashCode());
                        wt.OTS_Product = reader.IsDBNull(EWTTicket.OTS_Product.GetHashCode()) ? null : reader.GetString(EWTTicket.OTS_Product.GetHashCode());
                        wt.OTS_Platform = reader.IsDBNull(EWTTicket.OTS_Platform.GetHashCode()) ? null : reader.GetString(EWTTicket.OTS_Platform.GetHashCode());
                        wt.OTS_Version = reader.IsDBNull(EWTTicket.OTS_Version.GetHashCode()) ? null : reader.GetString(EWTTicket.OTS_Version.GetHashCode());
                        wt.Agnt_Id = reader.IsDBNull(EWTTicket.Agnt_Id.GetHashCode()) ? null : reader.GetDecimal(EWTTicket.Agnt_Id.GetHashCode()) as decimal?;
                        wt.Agnt_FName = reader.IsDBNull(EWTTicket.Agnt_FName.GetHashCode()) ? null : reader.GetString(EWTTicket.Agnt_FName.GetHashCode());
                        wt.Agnt_LName = reader.IsDBNull(EWTTicket.Agnt_LName.GetHashCode()) ? null : reader.GetString(EWTTicket.Agnt_LName.GetHashCode());
                        wt.Creation_TS = reader.IsDBNull(EWTTicket.Creation_TS.GetHashCode()) ? null : reader.GetString(EWTTicket.Creation_TS.GetHashCode());
                        wt.Resolution_TS = reader.IsDBNull(EWTTicket.Resolution_TS.GetHashCode()) ? null : reader.GetString(EWTTicket.Resolution_TS.GetHashCode());
                        wt.Short_Desc = reader.IsDBNull(EWTTicket.Short_Desc.GetHashCode()) ? null : reader.GetString(EWTTicket.Short_Desc.GetHashCode());
                        wt.OTS_Category = reader.IsDBNull(EWTTicket.OTS_Category.GetHashCode()) ? null : reader.GetString(EWTTicket.OTS_Category.GetHashCode());
                        wt.OTS_SubCategory = reader.IsDBNull(EWTTicket.OTS_SubCategory.GetHashCode()) ? null : reader.GetString(EWTTicket.OTS_SubCategory.GetHashCode());
                        wt.FLD_Val1 = reader.IsDBNull(EWTTicket.FLD_Val1.GetHashCode()) ? null : reader.GetString(EWTTicket.FLD_Val1.GetHashCode());
                        wt.FLD_Val2 = reader.IsDBNull(EWTTicket.FLD_Val2.GetHashCode()) ? null : reader.GetString(EWTTicket.FLD_Val2.GetHashCode());
                        wt.OTS_Media_Origin = reader.IsDBNull(EWTTicket.OTS_Media_Origin.GetHashCode()) ? null : reader.GetString(EWTTicket.OTS_Media_Origin.GetHashCode());
                        wt.OTS_System_Origin = reader.IsDBNull(EWTTicket.OTS_System_Origin.GetHashCode()) ? null : reader.GetString(EWTTicket.OTS_System_Origin.GetHashCode());
                        wt.UCase_Email = reader.IsDBNull(EWTTicket.UCase_Email.GetHashCode()) ? null : reader.GetString(EWTTicket.UCase_Email.GetHashCode());
                        wt.Agent_Site = reader.IsDBNull(EWTTicket.Agent_Site.GetHashCode()) ? null : reader.GetString(EWTTicket.Agent_Site.GetHashCode());
                        wt.Cust_Lang = reader.IsDBNull(EWTTicket.Cust_Lang.GetHashCode()) ? null : reader.GetString(EWTTicket.Cust_Lang.GetHashCode());

                        Log4NetManager.Instance.Info(typeof(TicketLoadConsole.Services.TicketService),
                            String.Format("GetWebTrackerTickets: Found UserId - {0}, CustFName - {1}, CustLName - {2}, CustEmail - {3}",
                                                                            wt.UserId, wt.Cust_FName, wt.Cust_LName, wt.Cust_Email));

                        returnList.Add(wt);
                    }
                    Log4NetManager.Instance.Info(typeof(TicketLoadConsole.Services.TicketService), "GetWebTrackerTickets: End");
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Log4NetManager.Instance.Error(typeof(TicketLoadConsole.Services.TicketService), ex);
                }
                finally
                {
                    connection.Close();
                    connection.Dispose();
                }
            }

            return returnList;
        }

        public static ContractMetadata GetWebTrackerContractMetadata(string contractId, string processId, string connStringId)
        {
            Log4NetManager.Instance.Info(typeof(TicketLoadConsole.Services.TicketService), "GetWebTrackerContractMetadata: Start");

            ContractMetadata conMeta = null;

            List<WebTrackerTicket> returnList = new List<WebTrackerTicket>();
            var connSettings = ConfigurationManager.ConnectionStrings[connStringId];
            //TODO: Replace sDate and eDate
            string sourceQueryString = String.Format(Properties.Resources.WebTrackerContractFormat, contractId, processId);
            using (OracleConnection connection = new OracleConnection(connSettings.ConnectionString))
            {
                OracleCommand command = connection.CreateCommand();
                command.CommandText = sourceQueryString;
                try
                {
                    connection.Open();
                    OracleDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        conMeta = new ContractMetadata();

                        while (reader.Read())
                        {
                            conMeta.Doc_Id = reader.GetValueOrDefault<decimal?>(EContractMetadata.Doc_Id.GetHashCode());
                            conMeta.Contract_Id = reader.GetValueOrDefault<decimal?>(EContractMetadata.Contract_Id.GetHashCode());
                            conMeta.Client_Name = reader.GetValueOrDefault<string>(EContractMetadata.Client_Name.GetHashCode());
                            conMeta.Contract_Status = reader.GetValueOrDefault<string>(EContractMetadata.Contract_Status.GetHashCode());
                            conMeta.Invit_Freq = reader.GetValueOrDefault<string>(EContractMetadata.Invit_Freq.GetHashCode());
                            conMeta.Send_Email_Invit = reader.GetValueOrDefault<string>(EContractMetadata.Send_Email_Invit.GetHashCode());
                            conMeta.From_Email = reader.GetValueOrDefault<string>(EContractMetadata.From_Email.GetHashCode());
                            conMeta.Email_Admin = reader.GetValueOrDefault<string>(EContractMetadata.Email_Admin.GetHashCode());
                            conMeta.Email_Client = reader.GetValueOrDefault<string>(EContractMetadata.Email_Client.GetHashCode());
                            conMeta.Domain_Name = reader.GetValueOrDefault<string>(EContractMetadata.Domain_Name.GetHashCode());
                            conMeta.Strm_FTP_Srvr = reader.GetValueOrDefault<string>(EContractMetadata.Strm_FTP_Srvr.GetHashCode());
                            conMeta.Strm_FTP_Dir = reader.GetValueOrDefault<string>(EContractMetadata.Strm_FTP_Dir.GetHashCode());
                            conMeta.Strm_FTP_Login = reader.GetValueOrDefault<string>(EContractMetadata.Strm_FTP_Login.GetHashCode());
                            conMeta.Strm_FTP_Password = reader.GetValueOrDefault<string>(EContractMetadata.Strm_FTP_Password.GetHashCode());
                            conMeta.Remote_FTP_Host = reader.GetValueOrDefault<string>(EContractMetadata.Remote_FTP_Host.GetHashCode());
                            conMeta.Remote_FTP_Dir = reader.GetValueOrDefault<string>(EContractMetadata.Remote_FTP_Dir.GetHashCode());
                            conMeta.Remote_FTP_Login = reader.GetValueOrDefault<string>(EContractMetadata.Remote_FTP_Login.GetHashCode());
                            conMeta.Remote_FTP_Password = reader.GetValueOrDefault<string>(EContractMetadata.Remote_FTP_Password.GetHashCode());
                            conMeta.Input_File_Format = reader.GetValueOrDefault<string>(EContractMetadata.Input_File_Format.GetHashCode());
                            conMeta.Field_Delimiter = reader.GetValueOrDefault<string>(EContractMetadata.Field_Delimiter.GetHashCode());
                            conMeta.Last_Processed_File = reader.GetValueOrDefault<string>(EContractMetadata.Last_Processed_File.GetHashCode());
                            conMeta.Last_Processed_FileDate = reader.GetValueOrDefault<DateTime>(EContractMetadata.Last_Processed_FileDate.GetHashCode());
                            conMeta.Last_Processed_FileIndex = reader.GetValueOrDefault<decimal?>(EContractMetadata.Last_Processed_FileIndex.GetHashCode());
                            conMeta.Zip1 = reader.GetValueOrDefault<string>(EContractMetadata.Zip1.GetHashCode());
                            conMeta.Zip2 = reader.GetValueOrDefault<string>(EContractMetadata.Zip2.GetHashCode());
                            conMeta.PGP1 = reader.GetValueOrDefault<string>(EContractMetadata.PGP1.GetHashCode());
                            conMeta.PGP2 = reader.GetValueOrDefault<string>(EContractMetadata.PGP2.GetHashCode());
                            conMeta.In_Folder = reader.GetValueOrDefault<string>(EContractMetadata.In_Folder.GetHashCode());
                            conMeta.Temp_Folder = reader.GetValueOrDefault<string>(EContractMetadata.Temp_Folder.GetHashCode());
                            conMeta.Out_Folder = reader.GetValueOrDefault<string>(EContractMetadata.Out_Folder.GetHashCode());
                            conMeta.Creation_Ts = reader.GetValueOrDefault<DateTime>(EContractMetadata.Creation_Ts.GetHashCode());
                            conMeta.Delta_Ts = reader.GetValueOrDefault<DateTime>(EContractMetadata.Delta_Ts.GetHashCode());
                            conMeta.Date_1 = reader.GetValueOrDefault<DateTime>(EContractMetadata.Date_1.GetHashCode());
                            conMeta.From_File = reader.GetValueOrDefault<string>(EContractMetadata.From_File.GetHashCode());
                            conMeta.URL = reader.GetValueOrDefault<string>(EContractMetadata.URL.GetHashCode());
                            conMeta.File_Extension = reader.GetValueOrDefault<string>(EContractMetadata.File_Extension.GetHashCode());
                            conMeta.Reply_To_Email = reader.GetValueOrDefault<string>(EContractMetadata.Reply_To_Email.GetHashCode());
                            conMeta.Subject_Email = reader.GetValueOrDefault<string>(EContractMetadata.Subject_Email.GetHashCode());
                            conMeta.URL_App = reader.GetValueOrDefault<string>(EContractMetadata.URL_App.GetHashCode());
                            conMeta.Process_Id = reader.GetValueOrDefault<string>(EContractMetadata.Process_Id.GetHashCode());
                            conMeta.Run_Flag = reader.GetValueOrDefault<string>(EContractMetadata.Run_Flag.GetHashCode());
                            conMeta.Run_Date = reader.GetValueOrDefault<DateTime>(EContractMetadata.Run_Date.GetHashCode());
                            conMeta.Stream_FTP_Transfer_Mode = reader.GetValueOrDefault<decimal?>(EContractMetadata.Stream_FTP_Transfer_Mode.GetHashCode());
                            conMeta.Remote_FTP_Transfer_Mode = reader.GetValueOrDefault<decimal?>(EContractMetadata.Remote_FTP_Transfer_Mode.GetHashCode());
                            conMeta.Last_Row = reader.GetValueOrDefault<decimal?>(EContractMetadata.Last_Row.GetHashCode());
                            conMeta.Task_Id = reader.GetValueOrDefault<string>(EContractMetadata.Task_Id.GetHashCode());

                            Log4NetManager.Instance.Info(typeof(TicketLoadConsole.Services.TicketService), String.Format("GetWebTrackerContractMetadata: Found Doc_Id - {0}, Contract_Id - {1}, Process_Id = {2}, Last_Processed_FileDate = {3}", conMeta.Doc_Id, conMeta.Contract_Id, conMeta.Process_Id, conMeta.Last_Processed_FileDate));
                        }
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    Log4NetManager.Instance.Error(typeof(TicketLoadConsole.Services.TicketService), ex);
                }
                finally
                {
                    connection.Close();
                    connection.Dispose();
                }
            }
            Log4NetManager.Instance.Info(typeof(TicketLoadConsole.Services.TicketService), "GetWebTrackerContractMetadata: End");
            return conMeta;
        }

        public static ContractMetadata GetESurveyContractMetadata(string contractId, string processId, string connStringId)
        {
            Log4NetManager.Instance.Info(typeof(TicketLoadConsole.Services.TicketService), "GetESurveyContractMetadata: Start");

            ContractMetadata conMeta = null;

            List<WebTrackerTicket> returnList = new List<WebTrackerTicket>();
            var connSettings = ConfigurationManager.ConnectionStrings[connStringId];
            //TODO: Replace sDate and eDate
            string sourceQueryString = String.Format(Properties.Resources.ESurveyContractFormat, contractId, processId);
            using (OracleConnection connection = new OracleConnection(connSettings.ConnectionString))
            {
                OracleCommand command = connection.CreateCommand();
                command.CommandText = sourceQueryString;
                try
                {
                    connection.Open();
                    OracleDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        conMeta = new ContractMetadata();

                        while (reader.Read())
                        {
                            conMeta.Doc_Id = reader.GetValueOrDefault<decimal?>(EContractMetadata.Doc_Id.GetHashCode());
                            conMeta.Contract_Id = reader.GetValueOrDefault<decimal?>(EContractMetadata.Contract_Id.GetHashCode());
                            conMeta.Client_Name = reader.GetValueOrDefault<string>(EContractMetadata.Client_Name.GetHashCode());
                            conMeta.Contract_Status = reader.GetValueOrDefault<string>(EContractMetadata.Contract_Status.GetHashCode());
                            conMeta.Invit_Freq = reader.GetValueOrDefault<string>(EContractMetadata.Invit_Freq.GetHashCode());
                            conMeta.Send_Email_Invit = reader.GetValueOrDefault<string>(EContractMetadata.Send_Email_Invit.GetHashCode());
                            conMeta.From_Email = reader.GetValueOrDefault<string>(EContractMetadata.From_Email.GetHashCode());
                            conMeta.Email_Admin = reader.GetValueOrDefault<string>(EContractMetadata.Email_Admin.GetHashCode());
                            conMeta.Email_Client = reader.GetValueOrDefault<string>(EContractMetadata.Email_Client.GetHashCode());
                            conMeta.Domain_Name = reader.GetValueOrDefault<string>(EContractMetadata.Domain_Name.GetHashCode());
                            conMeta.Strm_FTP_Srvr = reader.GetValueOrDefault<string>(EContractMetadata.Strm_FTP_Srvr.GetHashCode());
                            conMeta.Strm_FTP_Dir = reader.GetValueOrDefault<string>(EContractMetadata.Strm_FTP_Dir.GetHashCode());
                            conMeta.Strm_FTP_Login = reader.GetValueOrDefault<string>(EContractMetadata.Strm_FTP_Login.GetHashCode());
                            conMeta.Strm_FTP_Password = reader.GetValueOrDefault<string>(EContractMetadata.Strm_FTP_Password.GetHashCode());
                            conMeta.Remote_FTP_Host = reader.GetValueOrDefault<string>(EContractMetadata.Remote_FTP_Host.GetHashCode());
                            conMeta.Remote_FTP_Dir = reader.GetValueOrDefault<string>(EContractMetadata.Remote_FTP_Dir.GetHashCode());
                            conMeta.Remote_FTP_Login = reader.GetValueOrDefault<string>(EContractMetadata.Remote_FTP_Login.GetHashCode());
                            conMeta.Remote_FTP_Password = reader.GetValueOrDefault<string>(EContractMetadata.Remote_FTP_Password.GetHashCode());
                            conMeta.Input_File_Format = reader.GetValueOrDefault<string>(EContractMetadata.Input_File_Format.GetHashCode());
                            conMeta.Field_Delimiter = reader.GetValueOrDefault<string>(EContractMetadata.Field_Delimiter.GetHashCode());
                            conMeta.Last_Processed_File = reader.GetValueOrDefault<string>(EContractMetadata.Last_Processed_File.GetHashCode());
                            conMeta.Last_Processed_FileDate = reader.GetValueOrDefault<DateTime>(EContractMetadata.Last_Processed_FileDate.GetHashCode());
                            conMeta.Last_Processed_FileIndex = reader.GetValueOrDefault<decimal?>(EContractMetadata.Last_Processed_FileIndex.GetHashCode());
                            conMeta.Zip1 = reader.GetValueOrDefault<string>(EContractMetadata.Zip1.GetHashCode());
                            conMeta.Zip2 = reader.GetValueOrDefault<string>(EContractMetadata.Zip2.GetHashCode());
                            conMeta.PGP1 = reader.GetValueOrDefault<string>(EContractMetadata.PGP1.GetHashCode());
                            conMeta.PGP2 = reader.GetValueOrDefault<string>(EContractMetadata.PGP2.GetHashCode());
                            conMeta.In_Folder = reader.GetValueOrDefault<string>(EContractMetadata.In_Folder.GetHashCode());
                            conMeta.Temp_Folder = reader.GetValueOrDefault<string>(EContractMetadata.Temp_Folder.GetHashCode());
                            conMeta.Out_Folder = reader.GetValueOrDefault<string>(EContractMetadata.Out_Folder.GetHashCode());
                            conMeta.Creation_Ts = reader.GetValueOrDefault<DateTime>(EContractMetadata.Creation_Ts.GetHashCode());
                            conMeta.Delta_Ts = reader.GetValueOrDefault<DateTime>(EContractMetadata.Delta_Ts.GetHashCode());
                            conMeta.Date_1 = reader.GetValueOrDefault<DateTime>(EContractMetadata.Date_1.GetHashCode());
                            conMeta.From_File = reader.GetValueOrDefault<string>(EContractMetadata.From_File.GetHashCode());
                            conMeta.URL = reader.GetValueOrDefault<string>(EContractMetadata.URL.GetHashCode());
                            conMeta.File_Extension = reader.GetValueOrDefault<string>(EContractMetadata.File_Extension.GetHashCode());
                            conMeta.Reply_To_Email = reader.GetValueOrDefault<string>(EContractMetadata.Reply_To_Email.GetHashCode());
                            conMeta.Subject_Email = reader.GetValueOrDefault<string>(EContractMetadata.Subject_Email.GetHashCode());
                            conMeta.URL_App = reader.GetValueOrDefault<string>(EContractMetadata.URL_App.GetHashCode());
                            conMeta.Process_Id = reader.GetValueOrDefault<string>(EContractMetadata.Process_Id.GetHashCode());
                            conMeta.Run_Flag = reader.GetValueOrDefault<string>(EContractMetadata.Run_Flag.GetHashCode());
                            conMeta.Run_Date = reader.GetValueOrDefault<DateTime>(EContractMetadata.Run_Date.GetHashCode());

                            Log4NetManager.Instance.Info(typeof(TicketLoadConsole.Services.TicketService), String.Format("GetESurveyContractMetadata: Found Doc_Id - {0}, Contract_Id - {1}, Process_Id = {2}, Last_Processed_FileDate = {3}", conMeta.Doc_Id, conMeta.Contract_Id, conMeta.Process_Id, conMeta.Last_Processed_FileDate));
                        }
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Log4NetManager.Instance.Error(typeof(TicketLoadConsole.Services.TicketService), ex);
                }
                finally
                {
                    connection.Close();
                    connection.Dispose();
                }
            }
            Log4NetManager.Instance.Info(typeof(TicketLoadConsole.Services.TicketService), "GetESurveyContractMetadata: End");
            return conMeta;
        }

        public static List<ESurveyTicket> InsertToESurvey(List<WebTrackerTicket> tickets, string connectionId)
        {
            Log4NetManager.Instance.Info(typeof(TicketLoadConsole.Services.TicketService), "InsertToESurvey: Start");
            List<ESurveyTicket> returnTickets = new List<ESurveyTicket>();
            var connSettings = ConfigurationManager.ConnectionStrings[connectionId];
            // Create the InsertCommand.
            using (OracleConnection connection = new OracleConnection(connSettings.ConnectionString))
            {
                #region Prepare Insert Command

                connection.Open();
                foreach (var ticket in tickets)
                {

                    OracleCommand cmdInsert = new OracleCommand(Properties.Resources.ESurveyInsertTicketFormat, connection);

                    cmdInsert.Parameters.Add("pCONTRACT_ID", OracleType.Number, 10, "CONTRACT_ID");
                    cmdInsert.Parameters[cmdInsert.Parameters.Count - 1].Value = GetValue<decimal>(ticket.Contract_id);
                    cmdInsert.Parameters.Add("pTICKET_ID", OracleType.VarChar, 50, "TICKET_ID");
                    cmdInsert.Parameters[cmdInsert.Parameters.Count - 1].Value = GetValue<decimal>(ticket.Log_id);
                    cmdInsert.Parameters.Add("pCUSTOMER_ID", OracleType.VarChar, 50, "CUSTOMER_ID");
                    cmdInsert.Parameters[cmdInsert.Parameters.Count - 1].Value = GetValue<decimal>(ticket.UserId);
                    cmdInsert.Parameters.Add("pCUSTOMER_FNAME", OracleType.VarChar, 50, "CUSTOMER_FNAME");
                    cmdInsert.Parameters[cmdInsert.Parameters.Count - 1].Value = GetValue<string>(ticket.Cust_FName);
                    cmdInsert.Parameters.Add("pCUSTOMER_LNAME", OracleType.VarChar, 50, "CUSTOMER_LNAME");
                    cmdInsert.Parameters[cmdInsert.Parameters.Count - 1].Value = GetValue<string>(ticket.Cust_LName);
                    cmdInsert.Parameters.Add("pEMAIL", OracleType.VarChar, 100, "EMAIL");
                    cmdInsert.Parameters[cmdInsert.Parameters.Count - 1].Value = GetValue<string>(ticket.Cust_Email);
                    cmdInsert.Parameters.Add("pCOMPANY", OracleType.VarChar, 80, "COMPANY");
                    cmdInsert.Parameters[cmdInsert.Parameters.Count - 1].Value = GetValue<string>(ticket.Cust_Company);
                    cmdInsert.Parameters.Add("pPHONE", OracleType.VarChar, 50, "PHONE");
                    cmdInsert.Parameters[cmdInsert.Parameters.Count - 1].Value = GetValue<string>(ticket.Cust_Phone);
                    cmdInsert.Parameters.Add("pADDRESS1", OracleType.VarChar, 200, "ADDRESS1");
                    cmdInsert.Parameters[cmdInsert.Parameters.Count - 1].Value = GetValue<string>(ticket.Cust_Addr1);
                    cmdInsert.Parameters.Add("pADDRESS2", OracleType.VarChar, 250, "ADDRESS2");
                    cmdInsert.Parameters[cmdInsert.Parameters.Count - 1].Value = GetValue<string>(ticket.Cust_Addr2);
                    cmdInsert.Parameters.Add("pCITY", OracleType.VarChar, 100, "CITY");
                    cmdInsert.Parameters[cmdInsert.Parameters.Count - 1].Value = GetValue<string>(ticket.Cust_City);
                    cmdInsert.Parameters.Add("pSTATE", OracleType.VarChar, 75, "STATE");
                    cmdInsert.Parameters[cmdInsert.Parameters.Count - 1].Value = GetValue<string>(ticket.Cust_State);
                    cmdInsert.Parameters.Add("pZIP", OracleType.VarChar, 30, "ZIP");
                    cmdInsert.Parameters[cmdInsert.Parameters.Count - 1].Value = GetValue<string>(ticket.Cust_ZipCode);
                    cmdInsert.Parameters.Add("pCOUNTRY", OracleType.VarChar, 75, "COUNTRY");
                    cmdInsert.Parameters[cmdInsert.Parameters.Count - 1].Value = GetValue<string>(ticket.Cust_Country);
                    cmdInsert.Parameters.Add("pPRODUCT_NAME", OracleType.VarChar, 100, "PRODUCT_NAME");
                    cmdInsert.Parameters[cmdInsert.Parameters.Count - 1].Value = GetValue<string>(ticket.OTS_Product);
                    cmdInsert.Parameters.Add("pPRODUCT_CODE", OracleType.VarChar, 50, "PRODUCT_CODE");
                    cmdInsert.Parameters[cmdInsert.Parameters.Count - 1].Value = GetValue<string>(ticket.OTS_Platform);
                    cmdInsert.Parameters.Add("pPRODUCT_CAT", OracleType.VarChar, 50, "PRODUCT_CAT");
                    cmdInsert.Parameters[cmdInsert.Parameters.Count - 1].Value = GetValue<string>(ticket.OTS_Version);
                    cmdInsert.Parameters.Add("pAGENT_ID", OracleType.VarChar, 50, "AGENT_ID");
                    cmdInsert.Parameters[cmdInsert.Parameters.Count - 1].Value = GetValue<decimal>(ticket.Agnt_Id);
                    cmdInsert.Parameters.Add("pAGENT_FNAME", OracleType.VarChar, 50, "AGENT_FNAME");
                    cmdInsert.Parameters[cmdInsert.Parameters.Count - 1].Value = GetValue<string>(ticket.Agnt_FName);
                    cmdInsert.Parameters.Add("pAGENT_LNAME", OracleType.VarChar, 50, "AGENT_LNAME");
                    cmdInsert.Parameters[cmdInsert.Parameters.Count - 1].Value = GetValue<string>(ticket.Agnt_LName);
                    cmdInsert.Parameters.Add("pDATE_SVC_START", OracleType.DateTime, 50, "DATE_SVC_START");
                    cmdInsert.Parameters[cmdInsert.Parameters.Count - 1].Value = GetValue<DateTime>(ticket.Creation_TS_DateTime);
                    cmdInsert.Parameters.Add("pDATE_SVC_RESOLVE", OracleType.DateTime, 50, "DATE_SVC_RESOLVE");
                    cmdInsert.Parameters[cmdInsert.Parameters.Count - 1].Value = GetValue<DateTime>(ticket.Resolution_TS_DateTime);
                    cmdInsert.Parameters.Add("pSVC_SHORT_DESC", OracleType.VarChar, 250, "SVC_SHORT_DESC");
                    cmdInsert.Parameters[cmdInsert.Parameters.Count - 1].Value = GetValue<string>(ticket.Short_Desc);
                    cmdInsert.Parameters.Add("pSVC_CAT", OracleType.VarChar, 80, "SVC_CAT");
                    cmdInsert.Parameters[cmdInsert.Parameters.Count - 1].Value = GetValue<string>(ticket.OTS_Category);
                    cmdInsert.Parameters.Add("pSVC_SUB_CAT", OracleType.VarChar, 80, "SVC_SUB_CAT");
                    cmdInsert.Parameters[cmdInsert.Parameters.Count - 1].Value = GetValue<string>(ticket.OTS_SubCategory);
                    cmdInsert.Parameters.Add("pTXT_FLD1", OracleType.VarChar, 65, "TXT_FLD1");
                    cmdInsert.Parameters[cmdInsert.Parameters.Count - 1].Value = GetValue<string>(ticket.FLD_Val1);
                    cmdInsert.Parameters.Add("pTXT_FLD2", OracleType.VarChar, 65, "TXT_FLD2");
                    cmdInsert.Parameters[cmdInsert.Parameters.Count - 1].Value = GetValue<string>(ticket.FLD_Val2);
                    cmdInsert.Parameters.Add("pOTS_MEDIA_ORIGIN", OracleType.VarChar, 50, "OTS;_MEDIA_ORIGIN");
                    cmdInsert.Parameters[cmdInsert.Parameters.Count - 1].Value = GetValue<string>(ticket.OTS_Media_Origin);
                    cmdInsert.Parameters.Add("pOTS_SYSTEM_ORIGIN", OracleType.VarChar, 50, "OTS_SYSTEM_ORIGIN");
                    cmdInsert.Parameters[cmdInsert.Parameters.Count - 1].Value = GetValue<string>(ticket.OTS_System_Origin);
                    cmdInsert.Parameters.Add("pUCASE_EMAIL", OracleType.VarChar, 64, "UCASE_EMAIL");
                    cmdInsert.Parameters[cmdInsert.Parameters.Count - 1].Value = GetValue<string>(ticket.UCase_Email);
                    cmdInsert.Parameters.Add("pAGENT_SITE", OracleType.VarChar, 35, "AGENT_SITE");
                    cmdInsert.Parameters[cmdInsert.Parameters.Count - 1].Value = GetValue<string>(ticket.Agent_Site);
                    cmdInsert.Parameters.Add("pCUST_LANGUAGE", OracleType.VarChar, 50, "CUST_LANGUAGE");
                    cmdInsert.Parameters[cmdInsert.Parameters.Count - 1].Value = GetValue<string>(ticket.Cust_Lang);
                    cmdInsert.Parameters.Add("pLOG_ID", OracleType.Number, 8, "LOG_ID");
                    cmdInsert.Parameters[cmdInsert.Parameters.Count - 1].Value = GetValue<decimal>(ticket.Log_id);

                    Log4NetManager.Instance.Info(typeof(TicketLoadConsole.Services.TicketService),
                                String.Format("InsertToESurvey: Adding to Insert Queue eSurvey; Customer_id - {0}, Ticket_Id - {1}, UCASE_EMAIL - {2}",
                                              ticket.UserId, ticket.Log_id, ticket.UCase_Email));


                    OracleCommand cmdSelect = connection.CreateCommand();
                    cmdSelect.CommandText = Properties.Resources.ESurveyGetInsertedIdFormat;

                    // Start a transaction
                    OracleTransaction txn = connection.BeginTransaction(IsolationLevel.ReadCommitted);
                    cmdInsert.Transaction = txn;
                    try
                    {

                        cmdInsert.ExecuteNonQuery();
                        txn.Commit();

                        var returneddocId = cmdSelect.ExecuteScalar(); //Retrieve inserted primary key (Doc_id)
                        var eSurvTicket = ticket.ToESurveyTicket();
                        eSurvTicket.Doc_Id = returneddocId == null ? null : returneddocId as decimal?;
                        returnTickets.Add(eSurvTicket);
                    }
                    catch (Exception ex)
                    {
                        Log4NetManager.Instance.Info(typeof(TicketLoadConsole.Services.TicketService), ex);
                        txn.Rollback();
                    }

                    txn.Dispose();
                    cmdInsert.Dispose();
                    cmdSelect.Dispose();
                }

                #endregion


                connection.Close();
                connection.Dispose();

                Log4NetManager.Instance.Info(typeof(TicketLoadConsole.Services.TicketService), "InsertToESurvey: End");
                return returnTickets;
            }
        }

        public static void UpdateESurvey(ESurveyTicket ticket, string connectionId)
        {
            Log4NetManager.Instance.Info(typeof(TicketLoadConsole.Services.TicketService), "UpdateESurvey: Start");
            int SurveyNum;
            switch (ticket.Cust_Language)
            {
                case "DUTCH":
                    SurveyNum = 4;
                    break;
                case "FRENCH":
                    SurveyNum = 3;
                    break;
                case "GERMAN":
                    SurveyNum = 2;
                    break;
                default: //ENGLISH
                    SurveyNum = 1;
                    break;
            }


            var connSettings = ConfigurationManager.ConnectionStrings[connectionId];
            // Create the InsertCommand.
            using (OracleConnection connection = new OracleConnection(connSettings.ConnectionString))
            {
                connection.Open();
                string updateQuery = String.Format("UPDATE STRM_TICKETS SET INVITATION_SENT = SYSDATE, SURVEY_NUMBER = {0} WHERE DOC_ID = {1}", SurveyNum, ticket.Doc_Id);
                OracleCommand cmdUpdate = new OracleCommand(updateQuery, connection);

                Log4NetManager.Instance.Info(typeof(TicketService), String.Format("UpdateESurvey: Update Doc_id-{0}, Survey_Number-{1}", ticket.Doc_Id, SurveyNum));

                // Start a transaction
                OracleTransaction txn = connection.BeginTransaction(IsolationLevel.ReadCommitted);
                cmdUpdate.Transaction = txn;
                try
                {
                    cmdUpdate.ExecuteNonQuery();
                    txn.Commit();
                }
                catch (Exception ex)
                {
                    Log4NetManager.Instance.Info(typeof(TicketLoadConsole.Services.TicketService), ex);
                    txn.Rollback();
                }
                txn.Dispose();
                cmdUpdate.Dispose();
                connection.Close();
                connection.Dispose();
            }
            Log4NetManager.Instance.Info(typeof(TicketLoadConsole.Services.TicketService), "UpdateESurvey: End");
        }

        public static void UpdateWebTracker(DateTime endDate)
        {
            Log4NetManager.Instance.Info(typeof(TicketLoadConsole.Services.TicketService), "UpdateWebTracker: Start");

            ConnectionStringSettings connSettings = ConfigurationManager.ConnectionStrings["WT"];
            string contractId = ConfigurationManager.AppSettings["WT.ContractId"];
            string processId = ConfigurationManager.AppSettings["WT.ProcessId"];
            // Create the InsertCommand.
            using (OracleConnection connection = new OracleConnection(connSettings.ConnectionString))
            {
                connection.Open();
                StringBuilder sb = new StringBuilder();
                sb.Append("UPDATE STRM_CONTRACT_METADATA SET ");
                sb.Append(String.Format("LAST_PROCESSED_FILEDATE = TO_DATE('{0}','MM/DD/YYYY'), ", endDate.ToString("MM/dd/yyyy")));
                sb.Append("RUN_FLAT = 'N', ");
                sb.Append("RUN_DATE = SYSDATE ");
                sb.Append(String.Format("WHERE CONTRACT_ID = {0} AND PROCESS_ID = {1}", contractId, processId));
                OracleCommand cmdUpdate = new OracleCommand(sb.ToString(), connection);

                Log4NetManager.Instance.Info(typeof(TicketService), String.Format("UpdateWebTracker: Update Last_Process_FileDate-{0}}", endDate.ToString("MM/dd/yyyy")));

                // Start a transaction
                OracleTransaction txn = connection.BeginTransaction(IsolationLevel.ReadCommitted);
                cmdUpdate.Transaction = txn;
                try
                {
                    cmdUpdate.ExecuteNonQuery();
                    txn.Commit();
                }
                catch (Exception ex)
                {
                    Log4NetManager.Instance.Info(typeof(TicketLoadConsole.Services.TicketService), ex);
                    txn.Rollback();
                }
                txn.Dispose();
                cmdUpdate.Dispose();
                connection.Close();
                connection.Dispose();
            }
            Log4NetManager.Instance.Info(typeof(TicketLoadConsole.Services.TicketService), "UpdateWebTracker: End");
        }

        /// <summary>
        /// Returns user ids of already existing tickets.
        /// </summary>
        public static List<decimal> ExistingUserIds(List<WebTrackerTicket> tickets, string connectionId)
        {
            Log4NetManager.Instance.Info(typeof(TicketLoadConsole.Services.TicketService), "ExistingUserIds: Start");
            var contractId = ConfigurationManager.AppSettings["ES.ContractId"];

            if (tickets.Count < 1)
            {
                Log4NetManager.Instance.Error(typeof(TicketService), "No Ticket found.");
                return null;
            }

            List<decimal> returnList = new List<decimal>();
            var connSettings = ConfigurationManager.ConnectionStrings[connectionId];

            StringBuilder sb = new StringBuilder();
            foreach (var tix in tickets)
            {
                sb.Append(String.Format("'{0}',", tix.Log_id));
            }

            //TODO: Replace sDate and eDate
            string sourceQueryString = String.Format("select ticket_id from strm_tickets where contract_id = {0} and ticket_id in ({1})", contractId, sb.ToString().Remove(sb.Length - 1, 1));
            using (OracleConnection connection = new OracleConnection(connSettings.ConnectionString))
            {
                OracleCommand command = connection.CreateCommand();
                command.CommandText = sourceQueryString;
                try
                {
                    connection.Open();
                    OracleDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        string decTemp = reader.GetString(0);

                        decimal d;
                        if (!Decimal.TryParse(decTemp, out d))
                        {
                            Log4NetManager.Instance.Error(typeof(TicketService), "ExistingUserIds: Unable to parse ticketid to decimal");
                            continue;
                        }
                        returnList.Add(d);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Log4NetManager.Instance.Error(typeof(TicketLoadConsole.Services.TicketService), ex);
                }
                finally
                {
                    connection.Close();
                    connection.Dispose();
                }
            }
            Log4NetManager.Instance.Info(typeof(TicketLoadConsole.Services.TicketService), "ExistingUserIds: End");
            return returnList;
        }

        public static T GetValueOrDefault<T>(this OracleDataReader rdr, int index)
        {
            object val = rdr[index];

            if (!(val is DBNull))
                return (T)val;

            return default(T);
        }

        public static object GetValue<T>(object obj)
        {
            if (obj == null)
                return DBNull.Value;
            else
                return (T)obj;
        }



    }
}
