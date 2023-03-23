using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using De.Hdodenhof.CircleImageView;
using System;
using MODEL;
using HELPER;
using Android.Provider;
using Android.Graphics;

namespace MyFriends.Activities
{
    [Activity(Label = "FriendActivity")]
    public class FriendActivity : Activity
    {
        private CircleImageView ivPicture;
        private EditText etFamily;
        private EditText etName;
        private EditText etbirthDate;
        private ImageButton ibcalendar;
        private EditText etPhone;
        private EditText etEmail;
        private EditText etPassword;
        private EditText etRetype;
        private Button btnSave;
        private Button btnCancel;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.friend_layout);
            // Create your application here

            InitializeViews();
            GetExtras();
        }

        private void InitializeViews()
        {
            ivPicture = FindViewById<CircleImageView>(Resource.Id.ivPicture);
            etFamily = FindViewById<EditText>(Resource.Id.etFamily);
            etName = FindViewById<EditText>(Resource.Id.etName);
            etbirthDate = FindViewById<EditText>(Resource.Id.etBirthDate);
            ibcalendar = FindViewById<ImageButton>(Resource.Id.ibCalendar);
            etPhone = FindViewById<EditText>(Resource.Id.etPhone);
            etEmail = FindViewById<EditText>(Resource.Id.etEmail);
            etPassword = FindViewById<EditText>(Resource.Id.etPassword);
            etRetype = FindViewById<EditText>(Resource.Id.etRetype);
            btnSave = FindViewById<Button>(Resource.Id.btnSave);
            btnCancel = FindViewById<Button>(Resource.Id.btnCancel);

            ivPicture.Click += ivPicture_click;
            ibcalendar.Click += ibCalendar_click;
            btnSave.Click += btnSave_click;
            btnCancel.Click += btnCancel_click;
        }

        private void ivPicture_click(object sender, EventArgs e)
        {
            Android.App.AlertDialog.Builder builder = new Android.App.AlertDialog.Builder(this, 1);
            builder.SetTitle("Where you want to go?");
            builder.SetMessage("");
            builder.SetPositiveButton("Gallery", (c, ev) =>
            {
                Intent = new Intent();
                Intent.SetType("image/*");
                Intent.SetAction(Intent.ActionGetContent);
                StartActivityForResult(Intent, 5);
            });
            builder.SetNegativeButton("Camera", (c, ev) =>
            {
                Intent takePicture = new Intent(MediaStore.ActionImageCapture);
                StartActivityForResult(takePicture, 6);
            });
            builder.Show();
        }

        private void ibCalendar_click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(DateAlertActivity));
            StartActivityForResult(intent, 0);
        }

        private void btnCancel_click(object sender, EventArgs e)
        {
            Intent intent = new Intent();
            SetResult(Result.Canceled);
            Finish();
        }

        private void btnSave_click(object sender, EventArgs e)
        {
            DateTime birthDate = new DateTime();
            bool isValid = true;

            string[] date = etbirthDate.Text.Split(new char[] { '/', '-', '.', ' ' });

            try
            {
                birthDate = new DateTime(Convert.ToInt32(date[2]), Convert.ToInt32(date[1]), Convert.ToInt32(date[0]));
            }
            catch (Exception ex)
            {
                isValid = false;
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(etEmail.Text);
                isValid = addr.Address == etEmail.Text;
            }
            catch (Exception ex)
            {
                isValid = false;
            }
            if (etName.Text.Length < 2 || etFamily.Text.Length < 2)
                isValid = false;
            if (etPhone.Text.Length < 9)
                isValid = false;
            if (etPassword.Text.Length < 2 || etPassword.Text != etRetype.Text)
                isValid = false;

            if (isValid)
            {
                
                Intent intent = new Intent();
                Friend friend = new Friend() { Family = etFamily.Text, Name = etName.Text, BirthDate = birthDate, Email = etEmail.Text, Phone = etPhone.Text, Picture = "" };
                intent.PutExtra("FRIEND", Serializer.ObjectToByteArray(friend));
                SetResult(Result.Ok, intent);
                Finish();
            }
        }

        public void GetExtras()
        {
            Intent intent = Intent;
            if(true /*Are there extras?*/)
                if (intent.HasExtra("FRIEND"))
                {
                    Friend friend = Serializer.ByteArrayToObject(intent.GetByteArrayExtra("FRIEND")) as Friend;
                    if(friend != null)
                    {
                        etName.Text = friend.Name;
                        etFamily.Text = friend.Family;
                        etbirthDate.Text = friend.BirthDate.ToString("dd/MM/yyyy");
                        etPhone.Text = friend.Phone;
                        etEmail.Text = friend.Email;
                    }
                }
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            if (resultCode == Result.Ok)
            {
                etbirthDate.Text = data.GetStringExtra("BIRTHDATE");
            }
            if (requestCode == 6)
            {
                if (resultCode == Result.Ok)
                {
                    Bitmap bitmap = (Bitmap)data.Extras.Get("data");
                    ivPicture.SetImageBitmap(bitmap);
                }
            }

            else if(requestCode == 5)
            {
                if (resultCode == Result.Ok)
                {
                    Android.Net.Uri uri = data.Data;

                    ImageDecoder.Source source = ImageDecoder.CreateSource(ContentResolver, uri);
                    Bitmap bitmap = ImageDecoder.DecodeBitmap(source);
                    ivPicture.SetImageBitmap(bitmap);
                }
            }
        }
    }
}