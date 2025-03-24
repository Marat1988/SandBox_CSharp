namespace Lesson3
{
    internal class Program
    {
        public delegate void Notify(string message);

        class MessagePublisher
        {
            public event Notify OnNotify;

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



        static void Main(string[] args)
        {
            var publisher = new MessagePublisher();

            var smsSubscriber = new SmsSubscriber();
            var emailSubscriber = new EmailSubscriber();

            publisher.OnNotify += smsSubscriber.ReceiveSms;
            publisher .OnNotify += emailSubscriber.ReceiveEmail;

            publisher.RaiseEvent("Hello world");
        }
    }
}
