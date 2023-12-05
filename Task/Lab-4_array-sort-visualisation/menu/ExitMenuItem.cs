using System;

namespace AlgSort.menu
{
    public sealed class ExitMenuItem : MenuItem
    {
        public ExitMenuItem() : base("Выход") { }

        public override void Execute()
        {
            Console.WriteLine("Выход");
        }
    }
}