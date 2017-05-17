namespace Hello_Transifex
{
    using System;
    using System.Globalization;
    using System.Threading;
    using Hello_Transifex.Properties;

    internal class Program
    {
        private static void Main(string[] args)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CurrentCulture;
            Thread.CurrentThread.CurrentUICulture = CultureInfo.CurrentUICulture;
            Console.WriteLine(Resources.UsingCulture, Thread.CurrentThread.CurrentCulture);
            Console.WriteLine(Resources.UsingUiCulture, Thread.CurrentThread.CurrentUICulture);

            Console.WriteLine(Resources.HelloWorld);
            Console.WriteLine();
            Console.WriteLine(Resources.SeeTheWiki);
            Console.WriteLine();
            Console.WriteLine(Resources.ThankYou);

            Console.ReadLine();
        }
    }
}
