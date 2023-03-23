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
    public class City : BaseEntity
    {
        private string name;

        public City() { }

        public int Id { get => id; }
        public string Name { get => name; set => name = value; }

        public override bool Equals(object obj)
        {
            return obj is City city &&
                   Id == city.Id &&
                   Name == city.Name;
        }

        public static bool operator ==(City left, City right)
        {
            return EqualityComparer<City>.Default.Equals(left, right);
        }

        public static bool operator !=(City left, City right)
        {
            return !(left == right);
        }
        public override string ToString()
        {
            return name;
        }
    }
}