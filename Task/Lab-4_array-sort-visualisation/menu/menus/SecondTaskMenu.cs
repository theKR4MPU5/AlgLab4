using System.Collections.Generic;

namespace AlgSort.menu.menus
{
    public class SecondTaskMenu : Menu
    {
        public SecondTaskMenu(bool isSelected = false) : base(title: "Внешние сортировки слиянием", items: new List<MenuItem>()
        {
            new DirectMergeSortMenu(),
            new NaturalMergeSortMenu(),
            new MultiPathMergeSortMenu()
        }, isSelected) { }
    }
}