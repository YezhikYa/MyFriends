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
    public class Friends : List<Friend>
    {
        public Friends() : base()
        {
            Add(new Friend() { Family = "Yazdan", Name = "Ori", BirthDate = new DateTime(2006, 7, 7), Email = "oriyaz123@gmail.com", Phone = "0587979771", Picture = "" });
            Add(new Friend() { Family = "Reznik", Name = "Natanel", BirthDate = new DateTime(2006, 8, 9), Email = "nr6890@pb.amalnet.k12.il", Phone = "0584467594", Picture = "" });
            Add(new Friend() { Family = "Rottenberg", Name = "Uri", BirthDate = new DateTime(1995, 6, 26), Email = "microm2001@hotmail.com", Phone = "0505237976"});
        }

        public void Sort()
        {
            base.Sort((item1, item2) =>
            {
                int compare = item1.Family.CompareTo(item2.Family);
                return (compare != 0) ? compare : item1.Name.CompareTo(item2.Name);
            });
        }

        public bool Exists(Friend f)
        {
            return Find(item => item.Phone == f.Phone) != null;
        }
    }
}