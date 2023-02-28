using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using De.Hdodenhof.CircleImageView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MODEL;

namespace MyFriends.Activities
{
    [Activity(Label = "FriendActivity")]
    public class FriendActivity : Activity
    {
        private CircleImageView ivPicture;
        private EditText etFamily;
        private EditText etName;
        private EditText etbirthDate;
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
        }

        private void InitializeViews()
        {
            ivPicture = FindViewById<CircleImageView>(Resource.Id.ivPicture);
            etFamily = FindViewById<EditText>(Resource.Id.etFamily);
            etName = FindViewById<EditText>(Resource.Id.etName);
            etbirthDate = FindViewById<EditText>(Resource.Id.etBirthDate);
            etPhone = FindViewById<EditText>(Resource.Id.etPhone);
            etEmail = FindViewById<EditText>(Resource.Id.etEmail);
            etPassword = FindViewById<EditText>(Resource.Id.etPassword);
            etRetype = FindViewById<EditText>(Resource.Id.etRetype);
            btnSave = FindViewById<Button>(Resource.Id.btnSave);
            btnCancel = FindViewById<Button>(Resource.Id.btnCancel);

            btnSave.Click += btnSave_click;
            btnCancel.Click += btnCancel_click;
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
                intent.PutExtra("Friend", (IParcelable)friend);
                SetResult(Result.Ok);
                Finish();
            }
        }
    }
}