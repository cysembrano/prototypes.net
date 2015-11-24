using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OracleClient;
using System.Configuration;
using System.Globalization;
using TicketLoadConsole.Services;
using TicketLoadConsole.Entities;

namespace TicketLoadConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Log4NetManager.EnsureConfigured();

            //NO ARGS SHIELD
            if (args.Length < 1)
            {
                Log4NetManager.Instance.Info(typeof(TicketLoadConsole.Program), "Program: No Program Arguments Found; Instructions Given");
                Console.WriteLine(Properties.Resources.CmdArguments);
                return;
            }


            #region SET ARGUMENT OBJECT
            ArgsObject argObj = new ArgsObject();
            try
            {
                foreach (var arg in args)
                {
                    if (arg.Contains("/mode")) argObj.ProgramModeStr = arg.Split(new char[] { ':' })[1];
                }
            }
            catch (Exception ex) { Log4NetManager.Instance.Error(typeof(TicketLoadConsole.Program), ex); }
            #endregion

            #region PROGRAM PROPER
            switch (argObj.ProgramMode)
            {
                case EProgramMode.TEST:
                    RunTestMode();
                    break;
                default:
                    RunDefaultMode();
                    break;
                    
            }
            #endregion
                  
        }

        static void RunTestMode()
        {
            Log4NetManager.Instance.Info(typeof(TicketLoadConsole.Program), "Program: Running on Test Mode");
            RunProcess(testMode: true);
        }

        static void RunDefaultMode()
        {
            Log4NetManager.Instance.Info(typeof(TicketLoadConsole.Program), "Program: Running on Default Mode");
            RunProcess(testMode: false);
        }

        private static void RunProcess(bool testMode)
        {
            int DaysAgo;
            if (!int.TryParse(ConfigurationManager.AppSettings["WT.DaysAgo"], out DaysAgo))
            {
                Log4NetManager.Instance.Error(typeof(TicketLoadConsole.Program), "Program: WT.DaysAgo is expected to be integer");
                return;
            }

            #region Webtracker Contract Metadata
            var WTContractMetadata = TicketService.GetWebTrackerContractMetadata(
                                                                        contractId: ConfigurationManager.AppSettings["WT.ContractId"],
                                                                        processId: ConfigurationManager.AppSettings["WT.ProcessId"],
                                                                        connStringId: "WT"
                                                                    );

            if (WTContractMetadata == null)
            {
                Log4NetManager.Instance.Error(typeof(TicketLoadConsole.Program), "Program: Web Tracker Contract not found.");
                return;
            }
            #endregion

            #region GetStartDate
            DateTime startDate;
            if (testMode)
            {
                if (!DateTime.TryParseExact(ConfigurationManager.AppSettings["WT.Test.sDate"],
                                           "MM/dd/yyyy",
                                           CultureInfo.InvariantCulture,
                                           DateTimeStyles.None,
                                           out startDate))
                {
                    Log4NetManager.Instance.Error(typeof(TicketLoadConsole.Program), "Program: Running on Test mode please set WT.Test.sDate on app.config to MM/dd/yyyy");
                    return;
                }
            }
            else
            {
                startDate = WTContractMetadata.Last_Processed_FileDate;
            }
            #endregion

            #region ESurvey Contract Metadata
            string eSurveyConstring = testMode ? "ES.Dev" : "ES.Prod";
            var ESContractMetadata = TicketService.GetESurveyContractMetadata(
                                                                        contractId: ConfigurationManager.AppSettings["ES.ContractId"],
                                                                        processId: ConfigurationManager.AppSettings["ES.ProcessId"],
                                                                        connStringId: eSurveyConstring
                                                                    );

            if (ESContractMetadata == null)
            {
                Log4NetManager.Instance.Error(typeof(TicketLoadConsole.Program), "Program: ESurvey Contract not found.");
                return;
            }
            #endregion

            //Query WebTracker
            DateTime endDate = DateTime.Now.AddDays((DaysAgo * -1));
            var list = TicketService.GetWebTrackerTickets(startDate, endDate);

            #region Duplicate Protection
            if (list.Count > 0)
            {
                var removeList = TicketService.ExistingUserIds(list, eSurveyConstring);
                foreach (var removable in removeList)
                {
                    var item = list.FirstOrDefault(d => d.Log_id == removable);
                    list.Remove(item);
                }
            }
            #endregion

            //If not testmode then on production you can update the source strm_contract_metadata.file_processed_date
            bool UpdateSource;
            if(Boolean.TryParse(ConfigurationManager.AppSettings["Prog.Def.UpdateSource"], out UpdateSource))
                if (!testMode && UpdateSource)
                    TicketService.UpdateWebTracker(endDate);

            List<ESurveyTicket> ESurveyTickets = new List<ESurveyTicket>();

            bool useEmailOverride = !String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["Prog.Test.OverrideToEmail"]);
            if (list.Count > 0)
            {
                //Insert to ESurvey
                ESurveyTickets = TicketService.InsertToESurvey(list, eSurveyConstring);

                //Email Sending
                foreach (var ticket in ESurveyTickets)
                {
                    if (!String.IsNullOrWhiteSpace(ticket.Email))
                    {
                        //Send Email
                        EmailService.SendEmailSurvey(ticket, ESContractMetadata, useOverrideEmail: useEmailOverride);
                        //Update Ticket 
                        TicketService.UpdateESurvey(ticket, eSurveyConstring);
                    }
                }
            }



           
        }
    }

    class ArgsObject
    {
        public string ProgramModeStr { get; set; }
        public EProgramMode ProgramMode 
        { 
            get
            {
                EProgramMode temp;
                if (Enum.TryParse<EProgramMode>(ProgramModeStr, true, out temp)) 
                    return temp;
                return EProgramMode.TEST;              
            }
        }

        public string StartDateStr { get; set; }
        public DateTime StartDate
        {
            get
            {
                return GetDate(StartDateStr);
            }
        }
        public string EndDateStr { get; set; }
        public DateTime EndDate
        {
            get
            {
                return GetDate(EndDateStr, 4);
            }
        }

        private DateTime GetDate(string dateArg, int additionalDays = 0)
        {
            var split = dateArg.Split(new char[] { '/' });
            try
            {
                return new DateTime(int.Parse(split[0]), int.Parse(split[1]), int.Parse(split[2]));
            }
            catch (Exception ex)
            {
                Log4NetManager.Instance.Error(this.GetType(), ex);
                return DateTime.Now.AddDays(additionalDays);
            }
        }        
    }

    enum EProgramMode
    {
        TEST,
        DEF,
    }
}

