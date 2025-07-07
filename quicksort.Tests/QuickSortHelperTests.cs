using quicksort.Utils;
using Xunit;

namespace quicksort.Tests
{
    public class QuickSortHelperTests
    {
        [Fact]
        public void QuickSort_SortsArray_Correctly()
        {
            int[] arr = { 5, 3, 8, 4, 2 };
            int[] expected = { 2, 3, 4, 5, 8 };

            QuickSortHelper.QuickSort(arr);

            Assert.Equal(expected, arr);
        }

        [Fact]
        public void QuickSort_EmptyArray_NoException()
        {
            int[] arr = { };
            int[] expected = { };

            QuickSortHelper.QuickSort(arr);

            Assert.Equal(expected, arr);
        }

        [Fact]
        public void QuickSort_SingleElement_NoChange()
        {
            int[] arr = { 42 };
            int[] expected = { 42 };

            QuickSortHelper.QuickSort(arr);

            Assert.Equal(expected, arr);
        }

        [Fact]
        public void QuickSort_AlreadySorted_NoChange()
        {
            int[] arr = { 1, 2, 3, 4, 5 };
            int[] expected = { 1, 2, 3, 4, 5 };

            QuickSortHelper.QuickSort(arr);

            Assert.Equal(expected, arr);
        }

        [Fact]
        public void QuickSort_ReverseSorted_SortsCorrectly()
        {
            int[] arr = { 5, 4, 3, 2, 1 };
            int[] expected = { 1, 2, 3, 4, 5 };

            QuickSortHelper.QuickSort(arr);

            Assert.Equal(expected, arr);
        }

        [Fact]
        public void QuickSort_WithDuplicates_SortsCorrectly()
        {
            int[] arr = { 3, 1, 2, 3, 1 };
            int[] expected = { 1, 1, 2, 3, 3 };

            QuickSortHelper.QuickSort(arr);

            Assert.Equal(expected, arr);
        }
    }
}