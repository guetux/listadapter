Generic List Adapter for Xamarin.Android
=================================

Simplifies adapter implementation and promotes the view holder pattern.

Example of usage:

```c#
public class TodoItemViewHolder : ViewHolder {
	public TodoItemViewHolder(View view) : base(view) {}
	public TextView Title;
}

class TodoListAdapter : ListAdapter<TodoItem, TodoItemViewHolder> {

	Activity _ctx;

	public TodoListAdapter(Activity ctx, List<TodoItem> list) : base(list) {
		_ctx = ctx;
	}

	public override TodoItemViewHolder CreateViewHolder(ViewGroup parent) {
		var view = _ctx.LayoutInflater.Inflate(
			Android.Resource.Layout.SimpleListItem1, parent, false);
		return new TodoItemViewHolder(view) {
			Title = view.FindViewById<TextView>(Android.Resource.Id.Text1),
		};
	}

	public override void BindViewHolder(TodoItemViewHolder viewHolder, TodoItem item) {
		viewHolder.Title.Text = item.Title;
	}
}
```