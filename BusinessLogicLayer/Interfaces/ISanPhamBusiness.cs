using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public partial interface ISanPhamBusiness
    {
        SanPhamModel GetDatabyID(string id);
        bool Create(SanPhamModel model);
        bool Update(SanPhamModel model);
        bool Delete(string MaSanPham);
        public List<SanPhamModel> Search(int pageIndex, int pageSize, out long total, string TenSanPham, string Gia);
    }
}
