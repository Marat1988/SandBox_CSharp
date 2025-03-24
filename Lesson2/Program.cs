namespace Lesson2
{
    public delegate void Notify(string message);



    class MessagePublisher
    {
        public Notify OnNotify;

        public void RaiseEvent(string message)
        {
            OnNotify?.Invoke(message);
        }   
    }

    class SmsSubscriber
    {
        public void ReceiveSms(string message)
        {
            Console.WriteLine($"SMS: {message}");
        }
    }

    class EmailSubscriber
    {
        public void ReceiveEmail(string message)
        {
            Console.WriteLine($"Email: {message}");
        }
    }



    internal class Program
    {
        static void Main(string[] args)
        {
            var publisher = new MessagePublisher();

            var smsPublisher = new SmsSubscriber();
            var emailSubscriber = new EmailSubscriber();

            publisher.OnNotify += smsPublisher.ReceiveSms;
            publisher.OnNotify += emailSubscriber.ReceiveEmail;

            publisher.RaiseEvent("Hello world!");
            Console.WriteLine();

            #region что тут не так
            #endregion
        }
    }
}
