using System.ComponentModel.DataAnnotations;

namespace Lab_8_2_Net_4.ViewModel
{
    //Đây là một view model được sử dụng để đại diện cho dữ liệu của form đăng nhập trong ứng dụng của bạn.
    public class LoginViewModel
    {
        [Required]
        public string? Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }

}
