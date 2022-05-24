using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uu_library_app.Core.Helpers
{
    public static class ArrayFindersHelper
    {
        public static string[] mostFrequentMultiple(string[] arr, int n)
        {
            List<string> bookList = new List<string>();
            // Sort the array
            Array.Sort(arr);

            // find the max frequency using
            // linear traversal
            int max_count = 1;
            string res = arr[0];
            int curr_count = 1;

            for (int i = 1; i < n; i++)
            {
                if (arr[i] == arr[i - 1])
                    curr_count++;
                else
                    curr_count = 1;

                // If last element is most frequent
                if (curr_count > max_count)
                {
                    max_count = curr_count;
                    res = arr[i - 1];
                }
            }
            bookList.Add(res);
            string[] booksArr = bookList.ToArray();

            return booksArr;
        }

    }
}
