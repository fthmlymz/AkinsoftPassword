using System.ComponentModel.DataAnnotations;

namespace PasswordManager.API.Dtos.User
{
    public class UserRegisterDto
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Email adresi boş bırakılamaz.")]
        [StringLength(maximumLength: 50, ErrorMessage = "Email adresi en fazla 50 karakter girilebilir.")]
        public string Email { get; set; }

        [StringLength(maximumLength: 50)]
        public string Username { get; set; }


        [Required(AllowEmptyStrings = false, ErrorMessage = "Şifre alanı boş bırakılamaz.")]
        [StringLength(maximumLength: 50, ErrorMessage = "Şifre en fazla 50 karakter girilebilir.")]
        public string Password { get; set; }


        [Required(AllowEmptyStrings = false, ErrorMessage = "Kullanıcı ismi boş bırakılamaz.")]
        [StringLength(maximumLength: 50, ErrorMessage = "Kullanıcı ismi en fazla 50 karakter girilebilir.")]
        public string Firstname { get; set; }


        [Required(AllowEmptyStrings = false, ErrorMessage = "Kullanıcı soyadı boş bırakılamaz.")]
        [StringLength(maximumLength: 50, ErrorMessage = "Kullanıcı soyadı en fazla 50 karakter girilebilir.")]
        public string Surname { get; set; }



        //[Required(AllowEmptyStrings = false, ErrorMessage = "Şirket kodu boş bırakılamaz.")]
        //[StringLength(maximumLength: 50, ErrorMessage = "Şirket kodu en fazla 50 karakter girilebilir.")]
        //public string BusinessCode { get; set; }

        //[Required(AllowEmptyStrings = false, ErrorMessage = "Kullanıcı tipi boş girilemez.")]
        //[StringLength(maximumLength: 25, ErrorMessage = "Kullanıcı tipi en fazla 25 karakter girilebilir.")]
        //public string UserType { get; set; }


        //public long CompanyId { get; set; }

        //[DefaultValue(typeof(DateTime))]
        public DateTime? RegisterDate { get; set; }
    }
}
