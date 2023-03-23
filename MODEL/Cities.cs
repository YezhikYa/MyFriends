using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MODEL
{
    public class Cities : List<City>
    {
        public Cities() : base()
        {
            Add(new City() {  Name = "Tel-Aviv" });
            Add(new City() {  Name = "Bat-Yam" });
            Add(new City() {  Name = "Petah-Tikva" });
            Add(new City() {  Name = "Nesher" });
            Add(new City() {  Name = "Haifa" });
        }
    }
}