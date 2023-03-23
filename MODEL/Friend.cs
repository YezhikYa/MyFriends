using System;
using System.Collections.Generic;
using SQLite;

namespace MODEL
{
    [Table ("Friends")]
    [Serializable]
    public class Friend : BaseEntity
    {
        private string family;
        private string name;
        private DateTime birthDate;
        private string phone;
        private string email;
        private string password;
        private string picture;

        public Friend() { }

        [AutoIncrement, PrimaryKey]
        public int Id { get => id; }
        public string Family { get => family; set => family = value; }
        public string Name { get => name; set => name = value; }
        public DateTime BirthDate { get => birthDate; set => birthDate = value; }
        public string Phone { get => phone; set => phone = value; }
        public string Email { get => email; set => email = value; }
        public string Password { get => password; set => password = value; }
        public string Picture { get => picture; set => picture = value; }

        public override bool Equals(object obj)
        {
            return obj is Friend friend &&
                   Id == friend.Id &&
                   Family == friend.Family &&
                   Name == friend.Name &&
                   BirthDate == friend.BirthDate &&
                   Phone == friend.Phone &&
                   Email == friend.Email &&
                   Password == friend.Password &&
                   Picture == friend.Picture;
        }

        public static bool operator ==(Friend left, Friend right)
        {
            return EqualityComparer<Friend>.Default.Equals(left, right);
        }

        public static bool operator !=(Friend left, Friend right)
        {
            return !(left == right);
        }

        public int Age
        {
            get
            {
                DateTime now = DateTime.Today;
                int age = now.Year - birthDate.Year;
                if (birthDate > now.AddYears(-age))
                    age--;

                return age;
            }
        }

        public string FullName
        {
            get { return family + " " + name; }
        }
    }
}