using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace Demo.Presentation.Extensions
{
    [ExcludeFromCodeCoverage]
    public class FadeAnimateItemsBehavior : Behavior<ListView>
    {
        public DoubleAnimation AddingAnimation { get; set; }
        public TimeSpan Tick { get; set; }


        protected override void OnAttached()
        {
            base.OnAttached();

            AssociatedObject.Loaded += AssociatedObject_Loaded;
        }

        void AssociatedObject_Loaded(object sender, RoutedEventArgs e)
        {
            IEnumerable<ListBoxItem> items;
            ItemContainerGenerator containerGenerator = AssociatedObject.ItemContainerGenerator;

            IEnumerable itemsSource = AssociatedObject.ItemsSource;

            if (itemsSource == null)
                items = AssociatedObject.Items.Cast<ListBoxItem>().ToArray();
            else
            {
                if (itemsSource is INotifyCollectionChanged collection)
                    collection.CollectionChanged += OnCollectionChanged;

                ListViewItem[] itemsSub = new ListViewItem[AssociatedObject.Items.Count];

                for (int i = 0; i < itemsSub.Length; i++)
                    itemsSub[i] = containerGenerator.ContainerFromIndex(i) as ListViewItem;

                items = itemsSub;
            }

            if (items.Any(x => x == null))
                items = items.Where(x => x != null);

            MakeItemsTransparent(items);

            TriggerItemsAnimations(items);
        }

        private static void MakeItemsTransparent(IEnumerable<ListBoxItem> items)
        {
            foreach (var item in items) item.Opacity = 0;
        }

        private void TriggerItemsAnimations(IEnumerable<ListBoxItem> items)
        {
            IEnumerator<ListBoxItem> enumerator = items.GetEnumerator();

            if (!enumerator.MoveNext()) return;

            var timer = new DispatcherTimer {Interval = Tick};
            timer.Tick += (s, timerE) =>
            {
                var item = enumerator.Current;
                item?.BeginAnimation(UIElement.OpacityProperty, AddingAnimation);

                if (enumerator.MoveNext()) return;

                timer.Stop();
                enumerator.Dispose();
            };

            timer.Start();
        }

        private void OnCollectionChanged(object s, NotifyCollectionChangedEventArgs cce)
        {
            if (cce.Action != NotifyCollectionChangedAction.Add) return;

            var itemContainer = AssociatedObject.ItemContainerGenerator.ContainerFromItem(cce.NewItems[0]) as UIElement;
            itemContainer?.BeginAnimation(UIElement.OpacityProperty, AddingAnimation);
        }
    }
}