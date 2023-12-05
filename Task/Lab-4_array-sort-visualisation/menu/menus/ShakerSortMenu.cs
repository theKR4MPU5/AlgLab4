using AlgSort.algorithms;

namespace AlgSort.menu.menus
{
    public class ShakerSortMenu : AlgorithmStringSortMenu
    {
        public ShakerSortMenu(bool isSelected = false) : base(sort: new ShakerSort(),
            title: "Shaker Сортировка текста", isSelected) { }
    }
}