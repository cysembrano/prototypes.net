using System;

namespace CpEventSamples20
{
   class FileMover
   {
      // Declare Event Handler (Delegate)
      public delegate void MoveFileEventHandler(object sender, MoveFileEventArgs e);

      // Declare the Event (field-like syntax)
      public event MoveFileEventHandler MoveFile;


      // Constructor
      public FileMover()
      {
      }

      // Method that performs the work, then causes the event to be raised
      public void UserInitiatesFileMove()
      {
         // code here moves the file.

         // Then we call the method that raises the event
         OnMoveFile();
      }

      // Method that raises the event
      private void OnMoveFile()
      {
         if (MoveFile != null) // The event will be null when the event has no subscribers
         {
            // Raise the event, passing a new instance of MoveFileEventArgs
            MoveFile(this, new MoveFileEventArgs("SomeFileName.txt", @"C:\TempSource", @"C:\TempDestination"));
         }
      }

   }

   public class MoveFileEventArgs : EventArgs
   {
      // Fields
      private string m_FileName = string.Empty;
      private string m_SourceFolder = string.Empty;
      private string m_DestinationFolder = string.Empty;

      // Constructor
      public MoveFileEventArgs(string fileName, string sourceFolder, string destinationFolder)
      {
         m_FileName = fileName;
         m_SourceFolder = sourceFolder;
         m_DestinationFolder = destinationFolder;
      }

      // Properties
      public string FileName
      {
         get { return m_FileName; }
      }

      public string SourceFolder
      {
         get { return m_SourceFolder; }
      }

      public string DestinationFolder
      {
         get { return m_DestinationFolder; }
      }
   }
}
