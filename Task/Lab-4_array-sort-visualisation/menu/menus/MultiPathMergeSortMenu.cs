using AlgSort.task_2;

namespace AlgSort.menu.menus
{
    public class MultiPathMergeSortMenu : MergeSort
    {
        public MultiPathMergeSortMenu(bool isSelected = false) : base(sortType: SortType.Multipath, title: "Многопутевая сортировка слиянием", isSelected) { }
    }
}