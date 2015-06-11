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
    /// Interaction logic for ItemListView.xaml
    /// </summary>
    public partial class ItemListView : UserControl
    {
        ObservableCollection<Item> itemsList;

        public ItemListView()
        {
            InitializeComponent();           
        }

        public IEventAggregator EventAggregator
        {
            get;
            set;
        }      

       

        private void itemList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Item selectedItem = this.itemList.SelectedItem as Item;

            if (this.EventAggregator != null)
            {
                this.EventAggregator.PublishEvent(new ItemSelected() { Item = selectedItem });
            }
        }

        private void New_Click(object sender, RoutedEventArgs e)
        {
            var newItem = new Item() { ItemNumber = 4000, ItemDescription = "New Item" };

            itemsList.Add(newItem);

            if (this.EventAggregator != null)
            {
                this.EventAggregator.PublishEvent(new ItemCreated() { Item = newItem });
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            var savedItem = this.itemList.SelectedItem as Item;

            this.EventAggregator.PublishEvent(new ItemSaved() { Item = savedItem });
        }       

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            itemsList = new ObservableCollection<Item>();

            itemsList.Add(new Item() { ItemNumber = 1000, ItemDescription = "First item" });
            itemsList.Add(new Item() { ItemNumber = 2000, ItemDescription = "Second item" });
            itemsList.Add(new Item() { ItemNumber = 3000, ItemDescription = "Third item" });

            this.itemList.ItemsSource = itemsList;
            this.itemList.SelectedIndex = 0;
            itemsList.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(items_CollectionChanged);

            this.New.Click += new RoutedEventHandler(New_Click);
            this.Save.Click += new RoutedEventHandler(Save_Click);
        }

        private void items_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                this.itemList.SelectedItem = e.NewItems[0];
            }
        }

    }
}
