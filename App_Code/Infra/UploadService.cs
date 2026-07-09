using System;
using System.IO;
using System.Linq;
using System.Configuration;
using System.Web.UI.WebControls;

public static class UploadService
{
    public static readonly string[] AllowedExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".webp" };

    public const int MaxFileSize = 1024000;

    public static string SanitizeFileName(string fileName)
    {
        string ext = Path.GetExtension(fileName);
        return Guid.NewGuid().ToString("N") + ext;
    }

    public static bool HasValidExtension(string fileName)
    {
        string ext = Path.GetExtension(fileName).ToLowerInvariant();
        return AllowedExtensions.Contains(ext);
    }

    public static bool IsValidSize(int contentLength)
    {
        return contentLength > 0 && contentLength <= MaxFileSize;
    }

    public static string Save(FileUpload fileUpload)
    {
        string fileName = SanitizeFileName(fileUpload.FileName);
        string path = ConfigurationManager.AppSettings["uploadServer"] + fileName;
        fileUpload.SaveAs(path);
        return fileName;
    }
}
