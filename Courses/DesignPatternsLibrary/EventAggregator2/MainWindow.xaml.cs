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

using System.Collections.ObjectModel;

using EventAggregator;
using EventAggregatorUI.Events;

namespace EventAggregatorUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        IEventAggregator ea;

        public MainWindow()
        {           

            InitializeComponent();

            this.ea = new EventAggregator.EventAggregator();

            this.ItemListView.EventAggregator = this.ea;


            var tabs = this.ItemView.Items;

            tabs.Add(new TabItem() { Header = "Item Header", Content = new ItemView(this.ea) });
            tabs.Add(new TabItem() { Header = "Item Details", Content = new ItemDetailsView(this.ea) });            

        }   

    }
}
