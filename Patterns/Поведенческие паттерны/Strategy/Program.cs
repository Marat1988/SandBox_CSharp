namespace Strategy
{
    interface IReader
    {
        void Parse(string url);
    }

    class ResourceReader
    {
        private IReader _reader;

        public ResourceReader(IReader reader)
        {
            _reader = reader;
        }
        public void SetStrategy(IReader reader)
        {
            _reader = reader;
        }
        public void Read(string url)
        {
            _reader.Parse(url);
        }
    }

    class NewsReader : IReader
    {
        public void Parse(string url)
        {
            Console.WriteLine("Парсинг с новостного сайта: " + url);
        }
    }

    class SocialNetworkReader : IReader
    {
        public void Parse(string url)
        {
            Console.WriteLine("Парсинг ленты новостей социальной сети: " + url);
        }
    }

    class TelegramChannelReader : IReader
    {
        public void Parse(string url)
        {
            Console.WriteLine("Парсинг канала мессенджера Telegram: " + url);
        }
    }




    internal class Program
    {
        static void Main(string[] args)
        {
            ResourceReader resourseReader = new ResourceReader(new NewsReader());
            string url = "https://news.com";
            resourseReader.Read(url);

            url = "https://facebook.com";
            resourseReader.SetStrategy(new SocialNetworkReader());
            resourseReader.Read(url);

            url = "@news_channel_telegram";
            resourseReader.SetStrategy(new  TelegramChannelReader());

            resourseReader.Read(url);

            Console.ReadLine();
        }
    }
}
