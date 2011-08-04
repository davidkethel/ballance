using System;
using System.ComponentModel.DataAnnotations;

namespace ballanceApp.Models
{
    [MetadataType(typeof(ballanceMetaData))]
    public partial class ballance
    {

        public decimal Difference;

        public int daysSinceStart;

        public decimal idealBallance;
    
    }


    public class ballanceMetaData
    {
        [DataType(DataType.Date)]
        [Required]
        public DateTime Date;

        [Required]       
        public Decimal Amount;

    }

}

