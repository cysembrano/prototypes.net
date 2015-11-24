using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketLoadConsole.Entities
{
    public class ContractMetadata
    {
        public decimal? Doc_Id { get; set; }
        public decimal? Contract_Id { get; set; }
        public string Client_Name { get; set; }
        public string Contract_Status { get; set; }
        public string Invit_Freq { get; set; }
        public string Send_Email_Invit { get; set; }
        public string From_Email { get; set; }
        public string Email_Admin { get; set; }
        public string Email_Client { get; set; }
        public string Domain_Name { get; set; }
        public string Strm_FTP_Srvr { get; set; }
        public string Strm_FTP_Dir { get; set; }
        public string Strm_FTP_Login { get; set; }
        public string Strm_FTP_Password { get; set; }
        public string Remote_FTP_Host { get; set; }
        public string Remote_FTP_Dir { get; set; }
        public string Remote_FTP_Login { get; set; }
        public string Remote_FTP_Password { get; set; }
        public string Input_File_Format { get; set; }
        public string Field_Delimiter { get; set; }
        public string Last_Processed_File { get; set; }
        public DateTime Last_Processed_FileDate { get; set; }
        public decimal? Last_Processed_FileIndex { get; set; }
        public string Zip1 { get; set; }
        public string Zip2 { get; set; }
        public string PGP1 { get; set; }
        public string PGP2 { get; set; }
        public string In_Folder { get; set; }
        public string Temp_Folder { get; set; }
        public string Out_Folder { get; set; }
        public DateTime Creation_Ts {   get; set; }
        public DateTime Delta_Ts { get; set; }
        public DateTime Date_1 { get; set; }
        public string From_File { get; set; }
        public string To_File { get; set; }
        public string URL { get; set; }
        public string File_Extension { get; set; }
        public string Reply_To_Email { get; set; }
        public string Subject_Email { get; set; }
        public string URL_App { get; set; }
        public string Process_Id { get; set; }
        public string Run_Flag { get; set; }
        public DateTime Run_Date { get; set; }
        public decimal? Stream_FTP_Transfer_Mode { get; set; }
        public decimal? Remote_FTP_Transfer_Mode { get; set; }
        public decimal? Last_Row { get; set; }
        public string Task_Id { get; set; }


    }
}
