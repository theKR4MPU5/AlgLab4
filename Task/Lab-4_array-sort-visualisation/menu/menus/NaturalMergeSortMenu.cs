using AlgSort.task_2;

namespace AlgSort.menu.menus
{
    public class NaturalMergeSortMenu : MergeSort
    {
        public NaturalMergeSortMenu(bool isSelected = false) : base(sortType: SortType.Natural, title: "Естественная сортировка слиянием", isSelected) { }
    }
}