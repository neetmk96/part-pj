using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace PartsInfo.Dto
{
    public class PartsInfoDto
    {

        public int? Id { get; set; }
        [JsonProperty("code")]
        [Required(ErrorMessage ="Vui lòng nhập mã đơn vị")]
        [RegularExpression(@"^([a-zA-Z0-9]|-){1,20}$", ErrorMessage = "Mã đơn vị chỉ bao gồm số chữ và dấu - , có độ dài từ 3 - 20 ký tự")]

        public string Code { get; set; }
    
        [JsonProperty("name")]
        [Required(ErrorMessage = "Vui lòng nhập tên đơn vị")]
        [RegularExpression(@"^([a-zA-ZÀ-ỹ0-9- ]){1,100}$", ErrorMessage = "Tên đơn vị chỉ bao gồm số chữ và dấu - , có độ dài từ 3 - 100 ký tự")]

        public string Name { get; set; }
        public int? ParentId { get; set; }
        [JsonProperty("parentCode")]
        [RegularExpression(@"^([a-zA-Z0-9]|-){1,20}$", ErrorMessage ="Mã đơn vị cha chỉ bao gồm số chữ và dấu - , có độ dài từ 3 - 20 ký tự")]
        public string? ParentCode { get; set; }

        public string? ParentName { get; set; }
        public DateTime? CreateAt { get; set; }
        public bool? IsDeleted { get; set; }
        [JsonProperty("description")]
        [RegularExpression(@"^([a-zA-ZÀ-ỹ0-9- ]){1,100}$", ErrorMessage = "Tên đơn vị chỉ bao gồm số chữ và dấu - , có độ dài từ 3 - 100 ký tự")]

        public string? Description { get; set; }
        public string? Username { get; set; }

    }
}
