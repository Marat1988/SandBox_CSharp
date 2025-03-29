using System.Linq.Expressions;
using System.Threading.Channels;

namespace Template_method
{

    abstract class Transmitter
    {
        protected virtual void VoiceRecord() => Console.WriteLine("Запись фрагметра речи");
        protected virtual void Simpling() { }
        protected virtual void Digitization() { }
        protected virtual void Modulation() { }
        protected virtual void Transmission() => Console.WriteLine("Передача сигнала по радиоканалу");

        public void ProcessStart()
        {
            VoiceRecord();
            Simpling();
            Digitization();
            Modulation();
            Transmission();
        }
    }

    class AnalogTransmitter: Transmitter
    {
        protected override void Modulation()
        {
            Console.WriteLine("Модуляция аналогового сигнала");
        }
    }

    class DigitTransmitter: Transmitter
    {
        protected override void Simpling()
        {
            Console.WriteLine("Дискретизация записанного фрагмента");
        }

        protected override void Digitization()
        {
            Console.WriteLine("Оцифровка");
        }

        protected override void Modulation()
        {
            Console.WriteLine("Модуляция цифрового сигнала");
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Transmitter analogTransmitter = new AnalogTransmitter();
            analogTransmitter.ProcessStart();
            Console.WriteLine();

            Transmitter digitTransmitter = new DigitTransmitter();
            digitTransmitter.ProcessStart();


        }
    }
}
