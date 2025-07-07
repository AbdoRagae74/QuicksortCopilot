using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using quicksort.Models;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace quicksort.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Handles the main page request, processes user input, and prepares data for the view.
        /// </summary>
        /// <param name="numbers">
        /// A string containing numbers separated by commas, spaces, semicolons, or newlines.
        /// </param>
        /// <returns>
        /// The Index view with the original and sorted arrays.
        /// </returns>
        /// <process>
        /// 1. Receives the input string from the user.
        /// 2. Parses the string into an integer array, ignoring invalid entries.
        /// 3. Clones the original array for sorting.
        /// 4. Applies the QuickSort algorithm to the cloned array.
        /// 5. Passes both arrays and the input string to the view using ViewBag.
        /// </process>
        /// 

        


        public IActionResult Index(string numbers)
        {
            int[] original = null;
            int[] sorted = null;

            if (!string.IsNullOrWhiteSpace(numbers))
            {
                // Parse input string to int array
                original = numbers
                    .Split(new[] { ',', ' ', ';', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(s => int.TryParse(s, out var n) ? n : (int?)null)
                    .Where(n => n.HasValue)
                    .Select(n => n.Value)
                    .ToArray();

                sorted = (int[])original.Clone();
                QuickSort(sorted, 0, sorted.Length - 1);
            }

            ViewBag.Original = original;
            ViewBag.Sorted = sorted;
            ViewBag.Input = numbers;

            return View();
        }

        /// <summary>
        /// Sorts an integer array in-place using the QuickSort algorithm (Lomuto partition scheme).
        /// </summary>
        /// <remarks>
        /// <para>
        /// <b>Time Complexity:</b>
        /// <list type="bullet">
        ///   <item><description>Best Case: O(n log n) – occurs when the pivot divides the array into two nearly equal halves at each step.</description></item>
        ///   <item><description>Average Case: O(n log n) – typical for random or unsorted input.</description></item>
        ///   <item><description>Worst Case: O(n^2) – occurs when the smallest or largest element is always chosen as the pivot (e.g., already sorted or reverse-sorted input).</description></item>
        /// </list>
        /// </para>
        /// <para>
        /// <b>Space Complexity:</b>
        /// <list type="bullet">
        ///   <item><description>O(log n) – due to the recursion stack in the best/average case.</description></item>
        ///   <item><description>O(n) – in the worst case, when the recursion stack is as deep as the array length (highly unbalanced partitions).</description></item>
        /// </list>
        /// </para>
        /// <para>
        /// <b>Notes:</b>
        /// <list type="bullet">
        ///   <item><description>This implementation is in-place and does not require additional arrays.</description></item>
        ///   <item><description>Randomizing the pivot or using median-of-three can improve performance and avoid the worst-case scenario.</description></item>
        /// </list>
        /// </para>
        /// </remarks>
        private void QuickSort(int[] arr, int low, int high)
        {
            if (low < high)
            {
                int p = Partition(arr, low, high);
                QuickSort(arr, low, p - 1);
                QuickSort(arr, p + 1, high);
            }
        }

        private int Partition(int[] arr, int low, int high)
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

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

/*
Comparison: QuickSort vs. Other Sorting Algorithms
        ---------------------------------------------------------------------------------------------------
        QuickSort:
            -Time Complexity: Best / Average O(n log n), Worst O(n^2)
            -Space Complexity: O(log n)(in -place, due to recursion stack)
            - Not stable(relative order of equal elements may change)
            - Very fast in practice for large, in-memory arrays
            - Performance can degrade to O(n^2) if pivot selection is poor (e.g., already sorted input)
            - Can be improved with randomized or median-of-three pivot selection

        Merge Sort:
            -Time Complexity: O(n log n) in all cases
            - Space Complexity: O(n)(requires additional array for merging)
    -Stable(preserves order of equal elements)
    - Preferred for linked lists or when stability is required
    - Consistent performance, but higher memory usage


Heap Sort:

    -Time Complexity: O(n log n) in all cases
    - Space Complexity: O(1)(in -place)
    - Not stable
    - Good for large datasets where memory usage is a concern
    - Slightly slower than QuickSort in practice due to cache performance


Bubble Sort, Insertion Sort, Selection Sort:

    -Time Complexity: O(n ^ 2) in average and worst cases
    - Space Complexity: O(1)(in -place)
    - Simple to implement, but inefficient for large datasets

    -Insertion Sort is efficient for small or nearly sorted arrays


Summary:

    -QuickSort is generally the fastest for large, in-memory arrays, but not stable.

    - Merge Sort is stable and has consistent O(n log n) performance, but uses more memory.

    - Heap Sort is in-place and O(n log n), but not stable and often slower than QuickSort.

    - Simpler algorithms(Bubble, Insertion, Selection) are only suitable for small or nearly sorted data.

===================================================================================================
*/