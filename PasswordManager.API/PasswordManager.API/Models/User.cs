using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace PasswordManager.API.Models
{
    public class User
    {
        public long Id { get; set; }

        [Required]
        [StringLength(maximumLength: 50)]
        public string Email { get; set; }

        [StringLength(maximumLength: 50)]
        public string Username { get; set; }


        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }



        [Required(AllowEmptyStrings = false)]
        [StringLength(maximumLength: 50)]
        public string Firstname { get; set; }



        [Required(AllowEmptyStrings = false)]
        [StringLength(maximumLength: 50)]
        public string Surname { get; set; }



        //[Required(AllowEmptyStrings = false)]
        //[StringLength(maximumLength: 50)]
        //public string BusinessCode { get; set; }

        //[Required]
        //[StringLength(maximumLength: 25)]
        //public string UserType { get; set; }


        [DefaultValue(typeof(DateTime))]
        public DateTime? RegisterDate { get; set; }
    }
}
