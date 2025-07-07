using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using quicksort.Models;
using System.Linq;

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
