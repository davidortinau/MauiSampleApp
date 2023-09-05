using Android.App;
using Android.Views;
using Android.Widget;
using Widgets;
using System.Collections.Generic;
using System.Linq;

namespace Chrono.Droid
{
	/// <summary>
	/// Chrono List View Adapter
	/// </summary>
	public class ChronoListViewAdapter : BaseAdapter<TimeInterval>
	{
		private const string TimeFormat = "hh\\:mm";

		readonly Activity context;
		private IList<TimeInterval> items = new List<TimeInterval>();

		/// <summary>
		/// Sets Items
		/// </summary>
		public IEnumerable<TimeInterval> Items
		{
			set
			{
				items = value.ToList();
			}
		    get 
			{ 
				return items;
			}
		}

		/// <summary>
		/// Initializes the instance of <see cref="ChronoListViewAdapter.cs"/>
		/// </summary>
		/// <param name="context"></param>
		/// <param name="view"></param>
		public ChronoListViewAdapter(Activity context, ChronoListView view)
		{
			this.context = context;
			items = view.Items.ToList();
		}

        #region Overridables
        /// <summary>
        /// Gets Time Interval at position
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public override TimeInterval this[int position]
		{
			get
			{
				return items[position];
			}
		}

		/// <summary>
		/// Gets Item Id at position
		/// </summary>
		/// <param name="position"></param>
		/// <returns></returns>
		public override long GetItemId(int position)
		{
			return position;
		}

		/// <summary>
		/// Gets Items Count
		/// </summary>
		public override int Count
		{
			get { return items.Count; }
		}

		/// <summary>
		/// Gets View
		/// </summary>
		/// <param name="position">Position</param>
		/// <param name="convertView">Convert View</param>
		/// <param name="parent">Parent</param>
		/// <returns></returns>
		public override Android.Views.View GetView(int position, Android.Views.View convertView, ViewGroup parent)
		{
			var item = items[position];

			TimeIntervalViewHolder viewHolder;
			if (convertView == null)
			{
				// no view to re-use, create new
				convertView = context.LayoutInflater.Inflate(Resource.Layout.ChronoListViewCell, null);
				viewHolder = new TimeIntervalViewHolder();
				viewHolder.Position = position;
				viewHolder.TimeFrom = convertView.FindViewById<TextView>(Resource.Id.startTime);
				viewHolder.TimeTo = convertView.FindViewById<TextView>(Resource.Id.endTime);
				viewHolder.Title = convertView.FindViewById<TextView>(Resource.Id.title);
				viewHolder.HasConflict = convertView.FindViewById<Android.Views.View>(Resource.Id.hasConflict);
				convertView.Tag = viewHolder;
				convertView.SetTag(Resource.Id.startTime, viewHolder.TimeFrom);
				convertView.SetTag(Resource.Id.endTime, viewHolder.TimeTo);
				convertView.SetTag(Resource.Id.title, viewHolder.Title);
				convertView.SetTag(Resource.Id.hasConflict, viewHolder.HasConflict);
			}
			else
			{
				viewHolder = (TimeIntervalViewHolder)convertView.Tag;
			}
			viewHolder.TimeFrom.Text = item.StartTime.ToString(TimeFormat);
			viewHolder.TimeTo.Text = item.EndTime.ToString(TimeFormat);
			if (item.ActivityInfo != null)
			{
				viewHolder.Title.Text = item.ActivityInfo.ActivityTitle;
			}
			else
			{
				viewHolder.Title.Text = string.Empty;
			}
			var resId = Resource.Drawable.ChronoCellStateEmpty;
			switch (item.IntervalMode)
			{
				case IntervalMode.EmptyDisabled:
				case IntervalMode.Exported:
					resId = Resource.Color.chrono_cell_state_disabled;
					break;
				case IntervalMode.Readonly:
					resId = Resource.Color.chrono_cell_state_readonly;
					break;
				case IntervalMode.Editable:
					if (NIQThemeController.Theme == Theme.NIQRebrand)
					{
						resId = Resource.Drawable.ChronoBlueCellStateEditable;
					}
					else
					{
						resId = Resource.Drawable.ChronoCellStateEditable;
					}
					break;
				case IntervalMode.Empty:
					if (NIQThemeController.Theme == Theme.NIQRebrand)
					{
						resId = item.IsSelected ? Resource.Drawable.ChronoBlueCellStateSelected : Resource.Drawable.ChronoBlueCellStateEmpty;
					}
					else
					{
						resId = item.IsSelected ? Resource.Drawable.ChronoCellStateSelected : Resource.Drawable.ChronoCellStateEmpty;
					}
					break;
				case IntervalMode.EmptyOptional:
					if (NIQThemeController.Theme == Theme.NIQRebrand)
					{
						resId = Resource.Drawable.ChronoBlueCellStateOptional;
					}
					else
					{
						resId = Resource.Drawable.ChronoCellStateOptional;
					}
					break;
			}
			viewHolder.HasConflict.Visibility = item.HasConflict ? ViewStates.Visible : ViewStates.Gone;
			convertView.SetBackgroundResource(resId);
			return convertView;
		}
        #endregion

        /// <summary>
        /// Time Interval View Holder
        /// </summary>
        private class TimeIntervalViewHolder : Java.Lang.Object
		{
			public TextView TimeFrom;
			public TextView TimeTo;
			public TextView Title;
			public Android.Views.View HasConflict;
			public int Position;
		}
	}
}