using AlgSort.task_2;

namespace AlgSort.menu.menus
{
    public class DirectMergeSortMenu : MergeSort
    {
        public DirectMergeSortMenu(bool isSelected = false) : base(sortType: SortType.Direct, title: "Прямая сортировка слиянием", isSelected) { }
    }
}