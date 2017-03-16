namespace Hello_Transifex
{
    using System;
    using Hello_Transifex.Properties;

    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine(Resources.HelloWorld);
            Console.WriteLine();
            Console.WriteLine(Resources.SeeTheWiki);
            Console.WriteLine();
            Console.WriteLine(Resources.ThankYou);

            Console.ReadLine();
        }
    }
}
