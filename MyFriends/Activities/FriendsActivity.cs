using Android.App;
using Android.OS;
using MODEL;
using MyFriends.Adapters;
using AndroidX.RecyclerView.Widget;
using Google.Android.Material.FloatingActionButton;
using System;
using Android.Content;
using Android.Runtime;
using HELPER;

namespace MyFriends.Activities
{
    [Activity(Label = "FriendsActivity")]
    public class FriendsActivity : Activity
    {
        private RecyclerView rvFriends;
        private FloatingActionButton fabAdd;

        private Friends friends;
        private FriendsAdapter adapter;

        private Friend friendToRemove;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Create your application here

            SetContentView(Resource.Layout.friends_layout);

            InitializeViews();
            SetRecyclerView();
        }

        private void InitializeViews()
        {
            this.friends= new Friends();
            rvFriends = FindViewById<RecyclerView>(Resource.Id.rvFriends);
            fabAdd = FindViewById<FloatingActionButton>(Resource.Id.fabAdd);

            fabAdd.Click += fabAdd_click;
        }

        private void Adapter_ItemLongClick(object sender, FriendsAdapterClickEventArgs e)
        {
            Android.App.AlertDialog.Builder builder = new Android.App.AlertDialog.Builder(this, 1);
            builder.SetTitle("Delete friend?");
            builder.SetMessage("All the data would be deleted permanently!");
            builder.SetPositiveButton("Ok", (c, ev) => 
            {
                friends.RemoveAt(e.Position);
                adapter.NotifyDataSetChanged();
            });
            builder.SetNegativeButton("Cancel", (c, ev) => { });
            builder.Show();
        }

        private void Adapter_ItemClick(object sender, FriendsAdapterClickEventArgs e)
        {
            friendToRemove = friends[e.Position];

            Intent intent = new Intent(this, typeof(FriendActivity));
            intent.PutExtra("FRIEND", Serializer.ObjectToByteArray(friends[e.Position]));
            StartActivityForResult(intent, 1);
        }

        private void fabAdd_click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(FriendActivity));
            StartActivityForResult(intent, 0);
        }

        public void SetRecyclerView()
        {
            adapter = new FriendsAdapter(friends);
            rvFriends.SetAdapter(adapter);
            rvFriends.SetLayoutManager(new LinearLayoutManager(this));
            rvFriends.AddItemDecoration(new DividerItemDecoration(this, DividerItemDecoration.Vertical));
            adapter.ItemClick += Adapter_ItemClick;
            adapter.ItemLongClick += Adapter_ItemLongClick;
        }
        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            if (resultCode == Result.Ok)
            {
                Friend friend = Serializer.ByteArrayToObject(data.GetByteArrayExtra("FRIEND")) as Friend;
                if (friend != null)
                {
                    if (requestCode == 0)
                    {
                        if(friends.Exists(friend) == false)
                            friends.Add(friend);
                        else
                        {
                            Android.App.AlertDialog.Builder builder = new Android.App.AlertDialog.Builder(this, 1);
                            builder.SetTitle("Error!");
                            builder.SetMessage("The friend already exists");
                            builder.SetPositiveButton("OK", (c, ev) => { });
                            builder.Show();
                        }
                    }
                    else
                    {
                        if (friend != friendToRemove)
                        {
                            friends.Remove(friendToRemove);
                            friends.Add(friend);
                        }
                        else
                        {
                            Android.App.AlertDialog.Builder builder = new Android.App.AlertDialog.Builder(this, 1);
                            builder.SetTitle("Error!");
                            builder.SetMessage("The friend already exists");
                            builder.SetPositiveButton("OK", (c, ev) => { });
                            builder.Show();
                        }
                    }
                }
                adapter.NotifyDataSetChanged();
            }
        }
    }
}