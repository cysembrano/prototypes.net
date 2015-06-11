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

using EventAggregator;
using EventAggregatorUI.Events;

namespace EventAggregatorUI
{
    /// <summary>
    /// Interaction logic for ItemDetailsView.xaml
    /// </summary>
    public partial class ItemDetailsView : UserControl, ISubscriber<ItemSaved>, ISubscriber<ItemSelected>, ISubscriber<ItemCreated>
    {
        public ItemDetailsView(IEventAggregator ea)
        {            
            InitializeComponent();

            ea.SubsribeEvent(this);
        }

        #region ISubscriber<ItemSelected> Members

        public void OnEventHandler(ItemSelected e)
        {
            this.Label.Content = string.Format("Item Selected {0}", e.Item.ItemNumber);
        }

        #endregion

        #region ISubscriber<ItemSaved> Members

        public void OnEventHandler(ItemSaved e)
        {
            this.Label.Content = string.Format("Item Saved {0}", e.Item.ItemNumber);
        }

        #endregion

        #region ISubscriber<ItemCreated> Members

        public void OnEventHandler(ItemCreated e)
        {
            this.Label.Content = string.Format("Item Created {0}", e.Item.ItemNumber);
        }

        #endregion
    }
}
