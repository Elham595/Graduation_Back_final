namespace graduation_project.Extensions
{
    public static class PathExtensions
    {
        public static string UrlFromFilePath(this string filePath, string pathString, string baseUrl)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentNullException(nameof(filePath));

            var fileName = Path.GetFileName(filePath);
            return string.Concat(baseUrl,"/",pathString,"/", fileName);
        }
    }
}
