using System.Collections.Generic;

namespace AlgSort.menu.menus
{
    public sealed class MainMenu : Menu
    {
        public MainMenu() : base(title: "", items: new List<MenuItem>()
        {
            //new NumSortMenu(),
            new SecondTaskMenu(),
            new StringSortMenu()
        }) { }
    }
}