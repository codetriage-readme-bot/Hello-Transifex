namespace Hello_Transifex
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Threading;
    using Hello_Transifex.Properties;

    internal class Program
    {
        private static void Main(string[] args)
        {
            var culture = CultureInfo.CurrentCulture;
            var uiCulture = CultureInfo.CurrentUICulture;
            if (args.Any()) {
                uiCulture = CultureInfo.CreateSpecificCulture(args.Last());
            }
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = uiCulture;
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
