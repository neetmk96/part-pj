namespace PartsInfo.Dto
{
    public class QueryParam
    {
        public int? page { get; set; }
        public int? pageSize { get; set; }
        public int? total { get; set; }
        public int? totalPages { get; set; }
        public string? search { get; set; }
    }
}
