using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Cartefact.Models
{
    public class Person
    {
        public Person()
        {
            PasswordSalt = Person.Salt();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; }

        public string Name { get; set; }

        [Display(Name="Pseudo")]
        public string Nickname { get; set; }

        [Display(Name = "Driving habits")]
        [DataType(DataType.MultilineText)]
        public string DrivingHabits { get; set; }

        [Display(Name = "Driver experience")]
        [DataType(DataType.MultilineText)]
        public string DriverExperience { get; set; }

        public string PasswordSalt { get; set; }

        public string PasswordHash { get; set; }

        public virtual Role Role { get; set; }

        public virtual ICollection<Car> Cars { get; set; }

        public virtual ICollection<Rental> Rentals { get; set; }

        [NotMapped]
        public bool IsAdmin {
            get { return (Role != null)?(Role.RoleName == "Admin"):false; }
            private set { throw new Exception("Cannot set Admin flag to an user outside from a context"); }
        }

        [NotMapped]
        [DataType(DataType.Password)]
        public string Password {
            get { return ""; }
            set {
                if (value != "") {
                    PasswordHash = Person.Hash(value, PasswordSalt);
                }
            }
        }

        public bool CheckPassword(string pass)
        {
            return PasswordHash == Person.Hash(pass, PasswordSalt);
        }

        public static string Hash(string pass, string salt = "")
        {
            SHA1 sha1 = SHA1.Create();
            byte[] hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(pass + salt));

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("x2"));
            }

            return sb.ToString();
        }

        public static string Salt()
        {
            RNGCryptoServiceProvider rngcsp = new RNGCryptoServiceProvider();
            byte[] salt = new byte[25];
            rngcsp.GetBytes(salt);

            StringBuilder sb = new StringBuilder();
            for(int i = 0; i < salt.Length; i++)
            {
                sb.Append(salt[i].ToString("x2"));
            }

            return sb.ToString();
        }
    }
}