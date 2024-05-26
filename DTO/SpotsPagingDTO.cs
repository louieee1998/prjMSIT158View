using prjMSIT158View.Models;

namespace prjMSIT158View.DTO
{
    public class SpotsPagingDTO
    {
        //後端回應前端的需求:
        //用來丟回去給前端的物件
        //1.分頁 //2.要顯示的資料
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }
        public List<SpotImagesSpot>? SpotsResult { get; set; }

    }
}
