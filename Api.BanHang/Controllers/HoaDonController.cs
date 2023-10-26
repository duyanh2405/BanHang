using BusinessLogicLayer;
using DataModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.BanHang.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HoaDonController : ControllerBase
    {
        private IHoaDonBusiness _hoadonBusiness;
        public HoaDonController(IHoaDonBusiness hoadonBusiness)
        {
            _hoadonBusiness = hoadonBusiness;
        }

        [Route("get-by-id/{MaHoaDon}")]
        [HttpGet]
        public HoaDonModel GetDatabyID(int MaHoaDon)
        {
            return _hoadonBusiness.GetDatabyID(MaHoaDon);
        }

        [Route("create-hoadon")]
        [HttpPost]
        public HoaDonModel CreateItem([FromBody] HoaDonModel model)
        {
            _hoadonBusiness.Create(model);
            return model;
        }

        [Route("update-HoaDon")]
        [HttpPut]
        public HoaDonModel UpdateItem([FromBody] HoaDonModel model)
        {
            _hoadonBusiness.Update(model);
            return model;
        }

        [Route("Delete-hoadon")]
        [HttpDelete]
        public IActionResult DeleteItem(string MaHoaDon)
        {
           /* _hoadonBusiness.Delete(MaHoaDon);*/
            return Ok(new { message = "Xóa thành công!" });
        }


        [Route("search-HoaDon")]
        [HttpPost]
        public IActionResult Search([FromBody] Dictionary<string, object> formData)
        {
            try
            {
                var page = int.Parse(formData["page"].ToString());
                var pageSize = int.Parse(formData["pageSize"].ToString());
                string ten_khach_hang = "";
                if (formData.Keys.Contains("ten_khach_hang") && !string.IsNullOrEmpty(Convert.ToString(formData["ten_khach"]))) { ten_khach_hang = Convert.ToString(formData["ten_khach_hang"]); }
                DateTime? fr_NgayTao = null;
                if (formData.Keys.Contains("fr_NgayTao") && formData["fr_NgayTao"] != null && formData["fr_NgayTao"].ToString() != "")
                {
                    var dt = Convert.ToDateTime(formData["fr_NgayTao"].ToString());
                    fr_NgayTao = new DateTime(dt.Year, dt.Month, dt.Day, 0, 0, 0, 0);
                }
                DateTime? to_NgayDuyet = null;
                if (formData.Keys.Contains("to_NgayDuyet") && formData["to_NgayDuyet"] != null && formData["to_NgayDuyet"].ToString() != "")
                {
                    var dt = Convert.ToDateTime(formData["to_NgayDuyet"].ToString());
                    to_NgayDuyet = new DateTime(dt.Year, dt.Month, dt.Day, 23, 59, 59, 999);
                }
                long total = 0;
                var data = _hoadonBusiness.Search(page, pageSize, out total, ten_khach_hang, fr_NgayTao, to_NgayDuyet);
                return Ok(
                    new
                    {
                        TotalItems = total,
                        Data = data,
                        Page = page,
                        PageSize = pageSize
                    }
                    );
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
