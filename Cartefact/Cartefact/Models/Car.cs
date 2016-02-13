using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Cartefact.Models
{
    public class Car
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; }

        public string Brand { get; set; }

        public string Model { get; set; }

        public string Color { get; set; }

        public string Description { get; set; }

        [Display(Name="Buying date")]
        [DataType(DataType.Date)]
        public DateTime BuyingDate { get; set; }

        public int Kilometers { get; set; }

        public virtual Status Status { get; set; }

        public virtual Location Location { get; set; }

        public virtual Person Person { get; set; }

        public virtual ICollection<Rental> Rentals { get; set; }

    }
}