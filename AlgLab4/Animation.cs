using System;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace AlgSortWPF
{
    class Animation
    {
        public async Task BubbleSortAnimationAsync()
        {
            Logger clear = new();
            for (int i = 0; i < ArrayCreator.ArraySize - 1; i++)
            {
                for (int j = 0; j < ArrayCreator.ArraySize - i - 1; j++)
                {
                    ArrayCreator.Rectangles[j].Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFB841")); // Red
                    ArrayCreator.Rectangles[j + 1].Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFB841")); // Red

                    if (GetHeight(ArrayCreator.Rectangles[j]) > GetHeight(ArrayCreator.Rectangles[j + 1]))
                    {
                        await SwapAsync(j, j + 1);
                        Logger l = new(j, j + 1);
                    }

                    ArrayCreator.Rectangles[j].Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#755D9A")); // Blue
                    ArrayCreator.Rectangles[j + 1].Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#755D9A")); // Blue
                }

                ArrayCreator.Rectangles[ArrayCreator.ArraySize - i - 1].Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#2E8B57")); // Green
            }
            ArrayCreator.Rectangles[0].Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#2E8B57")); // Green
        }

        public async Task MergeSortAnimationAsync(int low, int high)
        {
            if (low < high)
            {
                int mid = (low + high) / 2;

                await MergeSortAnimationAsync(low, mid);
                await MergeSortAnimationAsync(mid + 1, high);

                await MergeAsync(low, mid, high);
            }
        }

        private async Task MergeAsync(int low, int mid, int high)
        {
            int n1 = mid - low + 1;
            int n2 = high - mid;

            double[] leftArray = new double[n1];
            double[] rightArray = new double[n2];
            int[] leftIndices = new int[n1];
            int[] rightIndices = new int[n2];

            for (int i = 0; i < n1; ++i)
            {
                leftArray[i] = GetHeight(ArrayCreator.Rectangles[low + i]);
                leftIndices[i] = low + i;
                ArrayCreator.Rectangles[low + i].Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFB841")); // Red
            }

            for (int j = 0; j < n2; ++j)
            {
                rightArray[j] = GetHeight(ArrayCreator.Rectangles[mid + 1 + j]);
                rightIndices[j] = mid + 1 + j;
                ArrayCreator.Rectangles[mid + 1 + j].Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFB841")); // Red
            }

            int k = low;
            int indexLeft = 0, indexRight = 0;

            while (indexLeft < n1 && indexRight < n2)
            {
                if (leftArray[indexLeft] <= rightArray[indexRight])
                {
                    SetHeight(ArrayCreator.Rectangles[k], leftArray[indexLeft]);
                    Logger l = new(leftIndices[indexLeft], k);

                    await Task.Delay(GetAnimationDelay());

                    ArrayCreator.Rectangles[k].Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#755D9A")); // Blue

                    indexLeft++;
                }
                else
                {
                    SetHeight(ArrayCreator.Rectangles[k], rightArray[indexRight]);
                    Logger l = new(rightIndices[indexRight], k);

                    await Task.Delay(GetAnimationDelay());

                    ArrayCreator.Rectangles[k].Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#755D9A")); // Blue

                    indexRight++;
                }

                k++;
            }

            while (indexLeft < n1)
            {
                SetHeight(ArrayCreator.Rectangles[k], leftArray[indexLeft]);
                Logger l = new(leftIndices[indexLeft], k);

                await Task.Delay(GetAnimationDelay());

                ArrayCreator.Rectangles[k].Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#755D9A")); // Blue

                indexLeft++;
                k++;
            }

            while (indexRight < n2)
            {
                SetHeight(ArrayCreator.Rectangles[k], rightArray[indexRight]);
                Logger l = new(rightIndices[indexRight], k);

                await Task.Delay(GetAnimationDelay());

                ArrayCreator.Rectangles[k].Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#755D9A")); // Blue

                indexRight++;
                k++;
            }

            for (int x = low; x <= high; x++)
            {
                ArrayCreator.Rectangles[x].Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#2E8B57")); // Green
            }
        }

        private int GetAnimationDelay()
        {
            return (int)(MainViewModel._window.speedSlider.Maximum - MainViewModel._window.speedSlider.Value) * 10;
        }
        public async Task SelectionSortAnimationAsync()
        {
            Logger clear = new();
            int n = ArrayCreator.ArraySize;

            for (int i = 0; i < n - 1; i++)
            {
                int minIndex = i;

                ArrayCreator.Rectangles[minIndex].Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFB841")); // Red

                for (int j = i + 1; j < n; j++)
                {
                    ArrayCreator.Rectangles[j].Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFB841")); // Red

                    if (GetHeight(ArrayCreator.Rectangles[j]) < GetHeight(ArrayCreator.Rectangles[minIndex]))
                    {
                        ArrayCreator.Rectangles[minIndex].Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#755D9A")); // Blue
                        minIndex = j;
                        ArrayCreator.Rectangles[minIndex].Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFB841")); // Red
                    }

                    await Task.Delay(50); // Задержка по необходимости
                }

                await SwapAsync(i, minIndex);
                Logger l = new(i, minIndex);

                ArrayCreator.Rectangles[i].Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#2E8B57")); // Green
            }

            ArrayCreator.Rectangles[n - 1].Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#2E8B57")); // Green
        }

        public async Task HeapSortAnimationAsync()
        {
            Logger clear = new();
            int n = ArrayCreator.ArraySize;

            for (int i = n / 2 - 1; i >= 0; i--)
                await HeapifyAsync(n, i);

            for (int i = n - 1; i > 0; i--)
            {
                ArrayCreator.Rectangles[i].Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFB841")); // Red
                ArrayCreator.Rectangles[0].Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFB841")); // Red

                await SwapAsync(0, i);

                ArrayCreator.Rectangles[i].Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#2E8B57")); // Green
                ArrayCreator.Rectangles[0].Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#755D9A")); // Blue

                await HeapifyAsync(i, 0);
            }

            for (int i = 0; i < n; i++)
            {
                ArrayCreator.Rectangles[i].Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#2E8B57")); // Green
                await Task.Delay(100);
            }
        }

        private async Task HeapifyAsync(int n, int i)
        {
            int largest = i;
            int left = 2 * i + 1;
            int right = 2 * i + 2;

            if (left < n && GetHeight(ArrayCreator.Rectangles[left]) > GetHeight(ArrayCreator.Rectangles[largest]))
                largest = left;

            if (right < n && GetHeight(ArrayCreator.Rectangles[right]) > GetHeight(ArrayCreator.Rectangles[largest]))
                largest = right;

            if (largest != i)
            {
                ArrayCreator.Rectangles[i].Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFB841")); // Red
                ArrayCreator.Rectangles[largest].Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFB841")); // Red

                await SwapAsync(i, largest);
                Logger l = new(i, largest);

                ArrayCreator.Rectangles[i].Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#755D9A")); // Blue
                ArrayCreator.Rectangles[largest].Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#755D9A")); // Blue

                await HeapifyAsync(n, largest);
            }
        }

        private async Task SwapAsync(int index1, int index2)
        {
            double tempHeight = GetHeight(ArrayCreator.Rectangles[index1]);
            SetHeight(ArrayCreator.Rectangles[index1], GetHeight(ArrayCreator.Rectangles[index2]));
            SetHeight(ArrayCreator.Rectangles[index2], tempHeight);

            int delayMilliseconds = (int)(MainViewModel._window.speedSlider.Maximum - MainViewModel._window.speedSlider.Value) * 10;
            await Task.Delay(delayMilliseconds);

            Canvas.SetLeft(ArrayCreator.Rectangles[index1], index2 * 40);
            Canvas.SetLeft(ArrayCreator.Rectangles[index2], index1 * 40);

            (ArrayCreator.Rectangles[index2], ArrayCreator.Rectangles[index1]) = (ArrayCreator.Rectangles[index1], ArrayCreator.Rectangles[index2]);
            (ArrayCreator.Rectangles[index2].Height, ArrayCreator.Rectangles[index1].Height) = (ArrayCreator.Rectangles[index1].Height, ArrayCreator.Rectangles[index2].Height);
        }

        private double GetHeight(Rectangle rectangle)
        {
            return rectangle.Height;
        }

        private void SetHeight(Rectangle rectangle, double height)
        {
            rectangle.Height = height;
        }
    }
}
