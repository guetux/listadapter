using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Android.App;
using Android.Views;
using Android.Widget;

namespace <PutYourNameSpaceHere> {
	/// <summary>
	/// A list based adapter for the impatient.
	/// </summary>
	public abstract class ListAdapter<T> : BaseAdapter<T> {

		protected ObservableCollection<T> _items;

		/// <summary>
		/// The underlying collection for this adapter. 
		/// Changing items will update the ListView directly.
		/// </summary>
		/// <value>The items.</value>
		public ObservableCollection<T> Items {
			get { return _items; }
			set {
				_items = value;
				NotifyDataSetChanged();
			}
		}

		public ListAdapter() {
			_items = new ObservableCollection<T>();
			_items.CollectionChanged += CollectionChanged;
		}

		public ListAdapter(IEnumerable<T> items) {
			_items = new ObservableCollection<T>(items);
			_items.CollectionChanged += CollectionChanged;
		}

		public ListAdapter(ObservableCollection<T> items) {
			_items = items;
			_items.CollectionChanged += CollectionChanged;
		}

		private void CollectionChanged (object sender, NotifyCollectionChangedEventArgs e) {
			NotifyDataSetChanged();
		}

		public override T this[int index] {
			get {
				return _items[index];
			}
		}

		public override int Count {
			get {
				return _items.Count;
			}
		}
			
		public override Java.Lang.Object GetItem(int position) {
			return null;
		}

		public override long GetItemId(int position) {
			return position;
		}

		/// <summary>
		/// Creates the view for one item of the list.
		/// </summary>
		/// <param name="position">Position within in the list.</param>
		/// <param name="convertView">Convert view to reuse.</param>
		/// <param name="parent">View will be attached to this group.</param>
		public abstract override View GetView(int position, View convertView, ViewGroup parent);
	}


	/// <summary>
	/// Base clase for view holder implementations.
	/// </summary>
	public abstract class ViewHolder : Java.Lang.Object {
		public View View { get; private set; }
		public ViewHolder(View view) {
			View = view;
		}
	}

	/// <summary>
	/// A view holder based variant for faster list adapters.
	/// </summary>
	public abstract class ListAdapter<T,VH> : ListAdapter<T> where VH : ViewHolder {

		public ListAdapter() {}
		public ListAdapter(IEnumerable<T> items) : base(items) {}
		public ListAdapter(ObservableCollection<T> items) : base(items) {}

		/// <summary>
		/// This method should inflate the view and attach the views to the view holder.
		/// </summary>
		/// <param name="parent">Parent.</param>
		/// <returns>The view holder.</returns>
		public abstract VH CreateViewHolder(ViewGroup parent);

		/// <summary>
		/// Bind an item of the list to the corresponding view holder view.
		/// </summary>
		/// <param name="viewHoler">The view holer.</param>
		/// <param name="item">The Item.</param>
		public abstract void BindViewHolder(VH viewHoler, T item);

		public override View GetView(int position, View convertView, ViewGroup parent) {
			VH viewHolder;
			if (convertView != null) {
				viewHolder = convertView.Tag as VH;
			} else {
				viewHolder = CreateViewHolder(parent);
				if (viewHolder.View != null) {
					viewHolder.View.Tag = viewHolder;
				} else {
					throw new NullReferenceException("ViewHolder must return a view!");
				}
			}
			BindViewHolder(viewHolder, _items[position]);
			return viewHolder.View;
		}
	}
}