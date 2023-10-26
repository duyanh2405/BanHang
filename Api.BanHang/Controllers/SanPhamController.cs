using BusinessLogicLayer;
using DataModel;
using Microsoft.AspNetCore.Mvc;

namespace Api.BanHang.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SanPhamController : ControllerBase
    {
        private ISanPhamBusiness _sanPhamBusiness;
        public SanPhamController(ISanPhamBusiness sanPhamBusiness)
        {
            _sanPhamBusiness = sanPhamBusiness;
        }

        [Route("get-by-id/{MaSanPham}")]
        [HttpGet]
        public SanPhamModel GetDatabyID(string MaSanPham)
        {
            return _sanPhamBusiness.GetDatabyID(MaSanPham);
        }

        [Route("create-SanPham")]
        [HttpPost]
        public SanPhamModel CreateItem([FromBody] SanPhamModel model)
        {
            _sanPhamBusiness.Create(model);
            return model;
        }

        [Route("update-SanPham")]
        [HttpPut]
        public SanPhamModel UpdateItem([FromBody] SanPhamModel model)
        {
            _sanPhamBusiness.Update(model);
            return model;
        }

        [Route("delete-SanPham")]
        [HttpDelete]
        public IActionResult DeleteItem(string MaSanPham)
        {
            _sanPhamBusiness.Delete(MaSanPham);
            return Ok(new { message = "Xóa thành công!" });
        }

        [Route("Search-SanPham")]
        [HttpPost]
        public IActionResult Search([FromBody] Dictionary<string, object> formData)
        {
            try
            {
                var page = int.Parse(formData["page"].ToString());
                var pageSize = int.Parse(formData["pageSize"].ToString());
                string TenSanPham = "";
                if (formData.Keys.Contains("ten_san_pham") && !string.IsNullOrEmpty(Convert.ToString(formData["ten_san_pham"]))) { TenSanPham = Convert.ToString(formData["ten_san_pham"]); }
                string Gia = "";
                if (formData.Keys.Contains("gia") && !string.IsNullOrEmpty(Convert.ToString(formData["gia"]))) { Gia = Convert.ToString(formData["gia"]); }
                long total = 0;
                var data = _sanPhamBusiness.Search(page, pageSize, out total, TenSanPham, Gia);
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

