using Microsoft.JSInterop;
using System.ComponentModel;
using Aspose.Words;
using Aspose.Words.Saving;
namespace NewProject
{
    public enum AcceptStatus
    {
        ChuaGui = 0,
        DaGui = 1,
        DaXacNhan = 2,
        TuChoiXacNhan = 3,
    }

    public enum HistoryAction
    {
        DaXoa = -1,
        DaTao = 0,
        DaGui = 1,
        DaXacNhan = 2,
        TuChoiXacNhan = 3,

    }
    public class TypeDrop
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class Files
    {
        private readonly IJSRuntime js;

        public Files(IJSRuntime js)
        {
            this.js = js;
        }
        public static string ConvertToPDF(string filePath)
        {
            if (filePath.ToUpper().Contains(".DOC"))
            {
                var originalDoc = new Document($"wwwroot/" + filePath);
                //Bỏ phần mở rộng của file và tạo file pdf mới
                string newPath = "";
                if (filePath.ToUpper().Contains(".DOCX"))
                {
                    var lst = filePath.ToLower().Split(".docx").ToList();
                    newPath = lst[0] + ".pdf";
                }
                else
                {
                    if (filePath.ToUpper().Contains(".DOC"))
                    {
                        var lst = filePath.ToLower().Split(".doc").ToList();
                        newPath = lst[0] + ".pdf";
                    }
                }

                //Xoá file pdf nếu tồn tại
                if (System.IO.File.Exists($"wwwroot/{newPath}"))
                {
                    System.IO.File.Delete($"wwwroot/{newPath}");
                }

                //Chuyển file doc sang pdf
                originalDoc.Save($"wwwroot/{newPath}");
                return $"{newPath}";
            }
            if (filePath.ToUpper().Contains(".PDF")) return filePath;

            return "";
        }
        public async void Download(string filePath, string username)
        {

            try
            {
                await js.InvokeAsync<object>("open", $"/{filePath}", "_blank");
            }
            catch { }

        }

        public async Task View(string filePath)
        {
            if (filePath.Contains(".pdf"))
            {
                try
                {

                    await js.InvokeVoidAsync("eval", $"let _discard_ = open(`{filePath}`, `_blank`)");
                }
                catch
                {

                }
            }
            else
            {
                string newfilePath = "";
                if (filePath.ToLower().Contains(".doc") || filePath.ToLower().Contains(".xls"))
                {
                    newfilePath = ConvertToPDF(filePath);
                }
                else
                {
                    newfilePath = filePath;
                }
                try
                {
                    await js.InvokeVoidAsync("eval", $"let _discard_ = open(`{newfilePath}`, `_blank`)");
                }
                catch
                {

                }
            }
        }
    }
}