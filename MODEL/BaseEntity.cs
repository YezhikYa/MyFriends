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
    [Serializable]
    public abstract class BaseEntity
    {
        protected int id;

        public BaseEntity() { }
        protected virtual int Id { get => id; }
    }
}