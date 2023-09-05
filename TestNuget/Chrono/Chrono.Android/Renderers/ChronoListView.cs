using Android.App;
using Android.Content;
using Android.Views;
using Android.Widget;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Maui.Controls.Handlers.Compatibility;
using Microsoft.Maui.Controls.Platform;
using static Android.Widget.AdapterView;


namespace Chrono.Droid
{
    /// <summary>
    /// Chrono List View Renderer for Android
    /// </summary>
    public class ChronoListViewRenderer : ListViewRenderer
    {
        private Activity actContext;
        private ChronoListViewAdapter adapter;

        /// <summary>
        /// Initializes the instance of <see cref="ChronoListViewRenderer.cs"/>
        /// </summary>
        /// <param name="context">Context</param>
        public ChronoListViewRenderer(Context context) : base(context)
        {
            actContext = Microsoft.Maui.ApplicationModel.Platform.CurrentActivity;
        }

        /// <summary>
        /// Raises on intercept touch event
        /// </summary>
        /// <param name="ev"></param>
        /// <returns></returns>
        public override bool OnInterceptTouchEvent(MotionEvent ev)
        {
            switch (ev.Action & MotionEventActions.Mask)
            {
                case MotionEventActions.Down:
                    Control.NestedScrollingEnabled = false;
                    break;
                case MotionEventActions.Move:
                    Control.NestedScrollingEnabled = true;
                    break;
                default:
                    break;
            }
            return base.OnInterceptTouchEvent(ev);
        }

        /// <summary>
        /// Raises on element changed event
        /// </summary>
        /// <param name="e">Event arguments</param>
        protected override void OnElementChanged(ElementChangedEventArgs<Microsoft.Maui.Controls.ListView> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                Control.ItemClick -= OnItemClick;
                Control.ItemLongClick -= OnLongItemClick;
            }

            if (e.NewElement != null)
            {
                adapter = new ChronoListViewAdapter(actContext, e.NewElement as ChronoListView);
                Control.Adapter = adapter;
                Control.ItemClick += OnItemClick;
                Control.ItemLongClick += OnLongItemClick;
            }
        }

        /// <summary>
        /// Raises on element property change event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == ChronoListView.ItemsProperty.PropertyName)
            {
                if (Control.Adapter != null && adapter.Items.Any() && (Element as ChronoListView).Items.Any())
                {
                    if (adapter.Items.First().Date == (Element as ChronoListView).Items.First().Date && adapter.Items.Count() == (Element as ChronoListView).Items.Count())
                    {
                        adapter.Items = (Element as ChronoListView).Items;
                        adapter.NotifyDataSetChanged();
                    }
                    else
                    {
                       adapter = new ChronoListViewAdapter(actContext, Element as ChronoListView);
                       Control.Adapter = adapter;
                    }
                }
                else
                {
                    adapter = new ChronoListViewAdapter(actContext, Element as ChronoListView);
                    Control.Adapter = adapter;
                }
            }
        }

        /// <summary>
        /// Raises On Item Click event
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event arguments</param>
        public void OnItemClick(object sender, ItemClickEventArgs e)
        {
            ((ChronoListView)Element).NotifyItemSelected(((ChronoListView)Element).Items.ToList()[e.Position - 1], e.Position - 1);
        }

        /// <summary>
        /// Raises On Long Item Click event
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event arguments</param>
        private void OnLongItemClick(object sender, AdapterView.ItemLongClickEventArgs e)
        {
            var position = e.Position - 1;
            int num = position;
            List<int> itemsToSelect = new List<int>();
            while (num >= 0) 
            {
                var item = ((ChronoListView)Element).Items.ToList()[num];
                if (item.IntervalMode == IntervalMode.Empty)
                {
                    itemsToSelect.Add(num);
                    num--;
                }
                else
                {
                    num = -1;
                }
            }
            num = position;
            while (num <= ((ChronoListView)Element).Items.Count - 1)
            {
                var item = ((ChronoListView)Element).Items.ToList()[num];
                if (item.IntervalMode == IntervalMode.Empty)
                {
                    itemsToSelect.Add(num);
                    num++;
                }
                else
                {
                    num = ((ChronoListView)Element).Items.Count;
                }
            }
            for (int i = 0; i < ((ChronoListView)Element).Items.Count; i++)
            {
                if (itemsToSelect.Contains(i))
                {
                    (Element as ChronoListView).Items[i].IsSelected = !(Element as ChronoListView).Items[i].IsSelected;

                }
                else
                {
                    (Element as ChronoListView).Items[i].IsSelected = false;
                }
            }
            adapter.Items = (Element as ChronoListView).Items;
            adapter.NotifyDataSetChanged();
            if (e.Position > 0)
            {
                ((ChronoListView)Element).NotifyItemLongClick(((ChronoListView)Element).Items.ToList()[e.Position - 1], e.Position - 1);
            }
        }

    }
}