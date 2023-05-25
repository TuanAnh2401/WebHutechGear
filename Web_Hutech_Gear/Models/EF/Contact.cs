using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Web_Hutech_Gear.Models.Support;

namespace Web_Hutech_Gear.Models.EF
{
    [Table("tb_Contact")]
    public class Contact : CommonAbstract
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên không được để trống")]
        [StringLength(150, ErrorMessage = "Không được vượt quá 150 ký tự")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Xin vui lòng nhập địa chỉ Email của bạn")]
        [EmailAddress(ErrorMessage = "Xin vui lòng nhập đúng định dạng địa chỉ Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Xin vui lòng nhập số điện thoại của bạn")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^(0|84)([0-9]{9})$", ErrorMessage = "Xin vui lòng nhập đúng định dạng số điện thoại")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Vui lòng để lại tin nhắn bạn muốn gửi")]
        [StringLength(4000, ErrorMessage = "Tin nhắn quá dài")]
        public string Message { get; set; }
        public bool IsRead { get; set; }
        public bool IsActivate { get; set; }

    }
}