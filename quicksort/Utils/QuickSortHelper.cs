namespace quicksort.Utils
{
    public static class QuickSortHelper
    {
        public static void QuickSort(int[] arr)
        {
            if (arr == null || arr.Length < 2) return;
            QuickSort(arr, 0, arr.Length - 1);
        }

        private static void QuickSort(int[] arr, int low, int high)
        {
            if (low < high)
            {
                int p = Partition(arr, low, high);
                QuickSort(arr, low, p - 1);
                QuickSort(arr, p + 1, high);
            }
        }

        private static int Partition(int[] arr, int low, int high)
        {
            int pivot = arr[high];
            int i = low;
            for (int j = low; j < high; j++)
            {
                if (arr[j] < pivot)
                {
                    (arr[i], arr[j]) = (arr[j], arr[i]);
                    i++;
                }
            }
            (arr[i], arr[high]) = (arr[high], arr[i]);
            return i;
        }
    }
}