using Android.App;
using Android.OS;
using MODEL;
using MyFriends.Adapters;
using AndroidX.RecyclerView.Widget;
using Google.Android.Material.FloatingActionButton;
using System;
using Android.Content;

namespace MyFriends.Activities
{
    [Activity(Label = "FriendsActivity")]
    public class FriendsActivity : Activity
    {
        private RecyclerView rvFriends;
        private FloatingActionButton fabAdd;

        private Friends friends;
        private FriendsAdapter adapter;
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

        }
    }
}