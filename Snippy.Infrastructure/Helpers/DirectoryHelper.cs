namespace Snippy.Infrastructure.Helpers
{
    public static class DirectoryHelper
    {
        public static string GetCurrentDirectory(string path)
        {
            return System.Web.HttpContext.Current.Server.MapPath(path);
        }
    }
}