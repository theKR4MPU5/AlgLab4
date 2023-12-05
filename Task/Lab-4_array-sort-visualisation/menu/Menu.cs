using System;
using System.Collections.Generic;
using System.Linq;
using AlgSort.menu.utils;

namespace AlgSort.menu
{
    public abstract class Menu : MenuItem
    {
        protected List<MenuItem> Items;

        protected Menu(string title, List<MenuItem> items = null, bool isSelected = false) : base(title, isSelected)
        {
            items ??= new List<MenuItem>();
            items.Add(new ExitMenuItem());
            if (!items.Any(x => x.IsSelected))
            {
                items.First().IsSelected = true;
            }

            Items = items;
        }

        public override void Execute()
        {
            Start();
        }

        public void Start()
        {
            Console.CursorVisible = false;
            bool canExit = false;
            do
            {
                DrawMenu();
                ConsoleKeyInfo inputKey = Console.ReadKey(true);
                switch (inputKey.Key)
                {
                    case ConsoleKey.DownArrow:
                        MenuNext();
                        break;
                    case ConsoleKey.UpArrow:
                        MenuPrev();
                        break;
                    case ConsoleKey.Enter:
                        MenuItem item = Items.First(item => item.IsSelected);
                        if (item.Equals(Items.Last()))
                        {
                            canExit = true;
                        }
                        else
                        {
                            item.Execute();
                        }

                        break;
                }
            } while (!canExit);
        }

        private void MenuPrev()
        {
            ConsoleUtil.ClearScreen();
            MenuItem select = Items.First(item => item.IsSelected);
            int selectindex = Items.IndexOf(select);
            Items[selectindex].IsSelected = false;
            selectindex = selectindex == 0
                ? Items.Count - 1
                : --selectindex;
            Items[selectindex].IsSelected = true;
        }

        private void MenuNext()
        {
            ConsoleUtil.ClearScreen();
            MenuItem select = Items.First(item => item.IsSelected);
            int selectIndex = Items.IndexOf(select);
            Items[selectIndex].IsSelected = false;
            selectIndex = selectIndex == Items.Count - 1
                ? 0
                : ++selectIndex;
            Items[selectIndex].IsSelected = true;
        }

        private void DrawMenu()
        {
            ConsoleUtil.ClearScreen();
            foreach (var item in Items)
            {
                Console.BackgroundColor = item.IsSelected
                    ? ConsoleColor.DarkCyan
                    : ConsoleColor.Black;
                Console.WriteLine(item.Title);
                Console.BackgroundColor = ConsoleColor.Black;
            }
        }
    }
}