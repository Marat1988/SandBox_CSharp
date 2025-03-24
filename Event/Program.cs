namespace Event
{
    internal class Program
    {

        delegate void Notify(string message);


        static void SendSms(string message)
        {
            Console.WriteLine($"SMS: {message}");
        }

        static void SendEmail(string message)
        {
            Console.WriteLine($"Email: {message}");
        }

        static void Main(string[] args)
        {
            Notify notify = SendSms;

            notify += SendEmail;

            notify("Hello World!");
        }
    }
}
