using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

using System.Threading;
using System.Threading.Tasks;


namespace AsianOptions
{

	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		//
		// Methods:
		//
		public MainWindow()
		{
			InitializeComponent();
		}

		//
		// To cancel tasks, we need a shared token source.  In order to cancel running tasks at app
		// shutdown and wait for them to finish, we also keep a running list of tasks, and a flag to
		// know when app is closing.
		//
		private CancellationTokenSource m_cts;
		private List<Task> m_running;
		private bool m_closing;

		/// <summary>
		/// Called at startup.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			m_cts = new CancellationTokenSource();
			m_running = new List<Task>();
			m_closing = false;
		}

		/// <summary>
		/// Exit the app.
		/// </summary>
		private void mnuFileExit_Click(object sender, RoutedEventArgs e)
		{
			this.Close();  // trigger "closed" event as if user had hit "X" on window:
		}

		/// <summary>
		/// Saves the contents of the list box.
		/// </summary>
		private void mnuFileSave_Click(object sender, RoutedEventArgs e)
		{
			using (StreamWriter file = new StreamWriter("results.txt"))
			{
				foreach (string item in this.lstPrices.Items)
					file.WriteLine(item);
			}
		}

		/// <summary>
		/// Called when form is starting to close; we can stop if we want.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			m_closing = true;

			//
			// cancel any running tasks:
			//
			mnuFileCancel_Click(null, null);
		}
		
		/// <summary>
		/// Cancels all running tasks.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void mnuFileCancel_Click(object sender, RoutedEventArgs e)
		{
			//
			// This is only enabled --- and clickable --- when one or more tasks are running...
			//
			System.Diagnostics.Debug.Assert(m_running.Count > 0);

			//
			// tell running tasks to cancel:
			//
			m_cts.Cancel();

			//
			// are we closing?  If so, wait before we let app shutdown...
			//
			if (m_closing)
			{
				try
				{
					Task.WaitAll(m_running.ToArray(), 5000 /*wait at most 5 secs*/);
				}
				catch
				{
					/* ignore all task exceptions during shutdown... */
				}
			}

			//
			// after tasks are cancelled, create new token source for future tasks:
			//
			m_cts = new CancellationTokenSource();
		}

		/// <summary>
		/// Main button to run the simulation.
		/// </summary>
		private void cmdPriceOption_Click(object sender, RoutedEventArgs e)
		{
			this.spinnerWait.Visibility = System.Windows.Visibility.Visible;
			this.spinnerWait.Spin = true;

			double initial = Convert.ToDouble(txtInitialPrice.Text);
			double exercise = Convert.ToDouble(txtExercisePrice.Text);
			double up = Convert.ToDouble(txtUpGrowth.Text);
			double down = Convert.ToDouble(txtDownGrowth.Text);
			double interest = Convert.ToDouble(txtInterestRate.Text);
			long periods = Convert.ToInt64(txtPeriods.Text);
			long sims = Convert.ToInt64(txtSimulations.Text);

			int count = System.Convert.ToInt32(this.lblCount.Content);
			count++;
			this.lblCount.Content = count.ToString();

			this.mnuFileCancel.IsEnabled = true;
			
			//
			// Run simulation on a separate task to price option:
			//
			CancellationToken token = m_cts.Token;

			Task<string> T = new Task<string>(() =>
				{
					Random rand = new Random();
					int start = System.Environment.TickCount;

					//
					// NOTE: we pass cancellation token to simulation code so it can check for
					// cancellation and respond accordingly.
					//
					double price = AsianOptionsPricing.Simulation(token, rand, initial, exercise, up, down, interest, periods, sims);

					int stop = System.Environment.TickCount;

					double elapsedTimeInSecs = (stop - start) / 1000.0;

					string result = string.Format("{0:C}  [{1:#,##0.00} secs]",
						price, elapsedTimeInSecs);

					return result;
				}, 

				token  // for cancellation:
			);

			// add task to list so we can cancel computation if necessary:
			m_running.Add(T);

			// and start it running:
			T.Start();

			//
			// Display the results once computation task is done, but ensure
			// task is run on UI's thread context so UI update is legal:
			//
			T.ContinueWith((antecedent) =>
				{
					string  result;

					// computation task has finished, remove from cancellation list:
					m_running.Remove(antecedent);

					//
					// Properly handle exceptions from computation task, e.g. possible cancellation:
					//
					try
					{
						result = antecedent.Result;
					}
					catch (AggregateException ae)
					{
						if (ae.InnerException is OperationCanceledException)
							result = "<<canceled>>";
						else
							result = string.Format("<<error: {0}>>", ae.InnerException.Message);
					}
					catch (Exception ex)
					{
						result = string.Format("<<error: {0}>>", ex.Message);
					}

					this.lstPrices.Items.Insert(0, result);

					//
					// update rest of UI:
					//
					count = System.Convert.ToInt32(this.lblCount.Content);
					count--;
					this.lblCount.Content = count.ToString();

					if (count == 0)
					{
						this.mnuFileCancel.IsEnabled = false;

						this.spinnerWait.Spin = false;
						this.spinnerWait.Visibility = System.Windows.Visibility.Collapsed;
					}
				},

				//
				// must run on UI thread since updating GUI:
				//
				TaskScheduler.FromCurrentSynchronizationContext()
			);
		}

	}//class
}//namespace
