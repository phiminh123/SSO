using Dapper;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Security.Claims;
namespace api.Services.GeneralAPIServices
{
    public class APIService<TItem> : IAPIService<TItem> where TItem : class
    {
        private readonly dbAPIContext _context;
        private readonly DbSet<TItem> _dbSet;
        private static SqlConnection _db;
        public APIService(dbAPIContext context)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                   .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                   .AddJsonFile("appsettings.json")
                   .Build();
            _db = new SqlConnection(configuration.GetConnectionString("dbAcountConnection"));
            _context = context;
            _dbSet = context.Set<TItem>();
            try
            {
                _db.Open();
            }
            catch { }
            
        }
        
        /// <summary>
        /// Phương thức lấy thông tin một nhóm người dùng
        /// </summary>
        /// <param name="id">Mã định danh nhóm người dùng</param>
        /// <returns>Thông tin nhóm người dùng</returns>
        public async Task<ServiceResponse<TItem>> GetById(int id)
        {
            var response = new ServiceResponse<TItem>();
            try
            {
                //TItem item = await _context.tbNhoms.FirstOrDefaultAsync(item => item.IdNhom == id) ?? new tbNhom();
                //if (nhom.IdNhom == 0)
                //{
                //    throw new Exception($"Chức năng không tồn tại");
                //}
                //response.Data = nhom;
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Message = e.Message;
            }
            return response;

        }

        /// <summary>
        /// Cài đặt phương thức lấy thông tin toàn bộ các nhóm người dùng
        /// </summary>
        /// <param name="pageNumber">Số trang</param>
        /// <param name="pageSize">Số bản ghi hiển thị trên trang</param>
        /// <returns>DS các nhóm người dùng</returns>
        public async Task<ServiceResponse<PageDataReturn<TItem>>> GetAll(int pageNumber, int pageSize, string lamda)
        {
            var response = new ServiceResponse<PageDataReturn<TItem>>();
            try
            {
                //Đoạn code này chuyển một chuổi biểu thức Lamda sang biểu thức Lamda
                //filterCon = "p => p.IdNguoi>55 && p.HoTen==\"Nguyễn Đình Nghĩa\"";
                //var options = ScriptOptions.Default.AddReferences(typeof(TItem).Assembly);
                //Func<TItem, bool> filterExpression = await CSharpScript.EvaluateAsync<Func<TItem, bool>>(filterCon, options);
                //if (lamda != "\" \"")
                //{
                //    lamda = string.Join("", lamda.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries));
                //    strLamda = string.Join("", strLamda.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries));
                //    lamda = lamda.Replace(strLamda, lamdaVal);
                //}

                // this will be used in all querys; add 'where' clauses etc if you want
             

                IQueryable<TItem> data;
                data = (lamda == "\" \"") ? data = _dbSet.AsQueryable() : _dbSet.Where(lamda).AsQueryable();
                PagedList<TItem> list = await PagedList<TItem>.ToPagedList(data, pageNumber, pageSize);
                response.Data = new PageDataReturn<TItem>() { Items = list, TotalCount = list.TotalCount };
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Message = e.Message;
            }
            return response;
        }
        /// <summary>
        /// Cài đặt phương thức lấy thông tin toàn bộ các nhóm người dùng
        /// </summary>
        /// <returns>DS các nhóm người dùng</returns>
        public async Task<ServiceResponse<List<TItem>>> GetAll(string lamda="")
        {
            var response = new ServiceResponse<List<TItem>>();
            try
            {
                //if (lamda != "\" \"")
                //{
                //    lamda = string.Join("", lamda.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries));
                //    strLamda = string.Join("", strLamda.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries));
                //    lamda = lamda.Replace(strLamda, lamdaVal);
                //}
                List<TItem> data;
                data = (lamda == "\" \"") ? data = _dbSet.ToList() : _dbSet.Where(lamda).ToList();
                response.Data = data;
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Message = e.Message;
            }
            return response;
        }

        /// <summary>
        /// Cài đặt phương thức tạo một nhóm người dùng mới
        /// </summary>
        /// <param name="record">Dữ liệu nhóm mới</param>
        /// <returns>True nếu tạo thành công; Flase nếu không thành công</returns>
        public async Task<ServiceResponse<bool>> Create(TItem item)
        {
            var response = new ServiceResponse<bool>();
            try
            {
                await _dbSet.AddAsync(item);
                await _context.SaveChangesAsync();
                response.Data = true;
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Message = e.Message;
            }
            return response;
        }
        /// <summary>
        /// Cài đặt phương thức cập nhật thông tin nhóm người dùng
        /// </summary>
        /// <param name="record">Thông tin nhóm người dùng cần cập nhật</param>
        /// <returns>true:cập nhật thành công; false:cập nhật không thành công</returns>
        public async Task<ServiceResponse<bool>> Update(TItem item)
        {
            var response = new ServiceResponse<bool>();
            try
            {
                _dbSet.Update(item);
                await _context.SaveChangesAsync();
                response.Data = true;
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Message = e.Message;
            }
            return response;
        }

        /// <summary>
        /// Cài đặt phương thức xóa một nhóm người dùng
        /// </summary>
        /// <param name="record">Thông tin nhóm người dùng cần xóa</param>
        /// <returns>true: xóa thành công; flase: xóa không thành công</returns>
        public async Task<ServiceResponse<bool>> Delete(string lamda)
        {
            var response = new ServiceResponse<bool>();
            try
            {
                //lamda = string.Join("", lamda.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries));
                //strLamda = string.Join("", strLamda.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries));
                //lamda = lamda.Replace(strLamda, lamdaVal);
                _dbSet.RemoveRange(_dbSet.Where(lamda).ToList());
                await _context.SaveChangesAsync();
                response.Data = true;

            }
            catch (Exception e)
            {
                response.Success = false;
                response.Message = e.Message;
            }
            return response;

        }
        public async Task<ServiceResponse<int>> GetId(string seqName)
        {
            var response = new ServiceResponse<int>();
            try
            {
                response.Data = _db.ExecuteScalar<int>($"SELECT NEXT VALUE FOR {seqName}"); ;

            }
            catch (Exception e)
            {
                response.Success = false;
                response.Message = e.Message;
            }
            return response;

        }

    }

}
