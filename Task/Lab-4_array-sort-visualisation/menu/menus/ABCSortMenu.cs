using AlgSort.algorithms;

namespace AlgSort.menu.menus
{
    public class ABCSortMenu : AlgorithmStringSortMenu
    {
        public ABCSortMenu(bool isSelected = false) : base(sort: new ABCSort(), title: "ABC Сортировка текста",
            isSelected) { }
    }
}