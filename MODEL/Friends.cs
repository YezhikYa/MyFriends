using System;
using System.Collections.Generic;

namespace MODEL
{
    public class Friends : List<Friend>
    {
        public Friends() : base()
        {
            Add(new Friend() { Family = "Yazdan", Name = "Ori", BirthDate = new DateTime(2006, 7, 7), Email = "oriyaz123@gmail.com", Phone = "0587979771", Picture = "" });
            Add(new Friend() { Family = "Reznik", Name = "Natanel", BirthDate = new DateTime(2006, 8, 9), Email = "nr6890@pb.amalnet.k12.il", Phone = "0584467594", Picture = "" });
            Add(new Friend() { Family = "Rottenberg", Name = "Uri", BirthDate = new DateTime(1995, 6, 26), Email = "microm2001@hotmail.com", Phone = "0505237976"});
            Add(new Friend() { Family = "Joseph", Name = "Omer", BirthDate = new DateTime(2006, 2, 7), Email = "oy0702@gmail.com", Phone = "0535260220" });
            Add(new Friend() { Family = "Shvarts", Name = "Liam", BirthDate = new DateTime(2006, 1, 12), Email = "kaftor06@gmail.com", Phone = "0542748567" });
            Add(new Friend() { Family = "Moshe", Name = "Yarden", BirthDate = new DateTime(2006, 6, 13), Email = "ym8624@pb.amalnet.k12.il", Phone = "0532451452" });
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