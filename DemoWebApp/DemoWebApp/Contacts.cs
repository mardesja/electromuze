using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace DemoWebApp.Models
{
    public class Contacts
    {
        [ScaffoldColumn(false)]
        [Key]
        public int ContactId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}