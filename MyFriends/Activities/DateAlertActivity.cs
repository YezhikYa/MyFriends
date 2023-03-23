using Android.App;
using Android.Content;
using Android.OS;
using System;

namespace MyFriends.Activities
{
    [Activity(Label = "DateAlertActivity")]
    public class DateAlertActivity : Activity
    {
        private DatePickerDialog datePicker;

        private DateTime birthDate;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Create your application here

            PerformDatePicker();
        }

        private void PerformDatePicker()
        {
            DateTime today = DateTime.Today;

            datePicker = new DatePickerDialog(this, OnDateClick, today.Year, today.Month - 1, today.Day);
            datePicker.DatePicker.MaxDate = (long)(DateTime.Today - new DateTime(1970, 1, 1)).TotalMilliseconds;

            datePicker.Show();
        }

        private void OnDateClick(object sender, DatePickerDialog.DateSetEventArgs e)
        {
            birthDate = e.Date;

            Intent intent = new Intent();
            intent.PutExtra("BIRTHDATE", birthDate.ToString("dd/MM/yyyy"));
            SetResult(Result.Ok, intent);
            Finish();
        }
    }
}