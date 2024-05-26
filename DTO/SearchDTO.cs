namespace prjMSIT158View.DTO
{
    public class SearchDTO
    {
        //前端的需求:
        //用來接前端json的物件
        //json檔的key = 物件的屬性
        public int categoryId { get; set; } = 0;//分類編號預設0 => 表示尚未選擇
        public string? keyword { get; set; }
        public int page { get; set; } = 1;//當前頁面預設1
        public int pageSize { get; set; } = 9;//一頁幾筆資料預設9
        public string? sortBy { get; set; }
        public string? sortType { get; set; }
    }
}
