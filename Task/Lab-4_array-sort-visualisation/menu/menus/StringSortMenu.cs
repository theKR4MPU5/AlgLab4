using System.Collections.Generic;

namespace AlgSort.menu.menus
{
    public class StringSortMenu : Menu
    {
        public StringSortMenu(bool isSelected = false) : base(title: "Сортировка слов", items: new List<MenuItem>()
        {
            new ShakerSortMenu(),
            new ABCSortMenu()
        }, isSelected) { }
    }
}