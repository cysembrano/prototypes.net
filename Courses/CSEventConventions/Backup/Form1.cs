using System;
using System.Windows.Forms;
using System.IO; // used only for the FileSystemWatcher example

namespace CpEventSamples20
{
   public partial class Form1 : Form
   {
      /**********************************************
       * Declarations
       **********************************************/
      FileMover fileMover = new FileMover();
      FileSystemWatcher fsWatcher = new FileSystemWatcher();

      static string fsWatcherTestPath = @"C:\FSWatcherTest";
      string setupNotice = @"The FileSystemWatcher example requires a folder named C:\FSWatcher to exist, and for at least one file to be placed in that folder.";

      /**********************************************
       * Constructor
       **********************************************/
      public Form1()
      {
         InitializeComponent();

         // Wire up event handling methods
         fileMover.MoveFile += new FileMover.MoveFileEventHandler(fileMover_MoveFile);
         fsWatcher.Renamed += new RenamedEventHandler(fsWatcher_Renamed);
      }

      /**********************************************
       * Process end-user gestures
       **********************************************/
      // User Initiates File Move
      private void MoveFileButton_Click(object sender, EventArgs e)
      {
         fileMover.UserInitiatesFileMove();
      }

      // User Initiates File Rename
      private void RenameFileButton_Click(object sender, EventArgs e)
      {
         if (Directory.Exists(fsWatcherTestPath))
         {
            fsWatcher.Path = fsWatcherTestPath;
            fsWatcher.EnableRaisingEvents = true;
            fsWatcher.SynchronizingObject = this;
            string[] files = Directory.GetFiles(fsWatcherTestPath);
            if (files.Length > 0)
            {
               File.Move(files[0], Path.Combine(fsWatcherTestPath, Path.GetFileNameWithoutExtension(files[0]) + "_Renamed" + Path.GetExtension(files[0])));
            }
            else
            {
               MessageBox.Show("The FileSystemWatcher example found no files in the folder: " + fsWatcherTestPath + Environment.NewLine + setupNotice);
            }
         }
         else
         {
            MessageBox.Show("The FileSystemWatcher example did not find the folder: " + fsWatcherTestPath + Environment.NewLine + setupNotice);
         }
         
      }

      /**********************************************
       * Event Handling Methods
       **********************************************/
      void fileMover_MoveFile(object sender, MoveFileEventArgs e)
      {
         this.Results.Text = sender.ToString() + " moved the file, " + e.FileName + ", from " + e.SourceFolder + " to " + e.DestinationFolder;
      }

      void fsWatcher_Renamed(object sender, RenamedEventArgs e)
      {
         this.Results.Text = "The file, " + e.OldName + ", was renamed to " + e.Name;
      }

   }
}