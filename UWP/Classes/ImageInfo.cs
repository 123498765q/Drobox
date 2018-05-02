namespace UWP.Classes
{
    public class ImageInfo
    {
        public string Path { get; set; }
        public string Sub { get; set; }
        public string Location { get; set; }
        public string Message { get; set; }

        public ImageInfo()
        {
        }

        public ImageInfo(string path, string sub, string location, string message)
        {
            Path = path;
            Sub = sub;
            Location = location;
            Message = message;
        }
    }
}