using DanskeTest.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DanskeTest.Repository
{
    public class SortingRepository : INumberSorting
    {

        #region Sort Given Numbers
        //To sort given numbers and save in txt file
        public async Task<bool> SortGivenNumbers(int[] unSortedNumbes)
        {

            bool result = false;
            int[] sortedNumbers = new int[unSortedNumbes.Length];
            try
            {
                sortedNumbers= await Quick_Sort(unSortedNumbes, 0, unSortedNumbes.Length - 1);
                // To Create Directory in local folder and save sorted number in same folder
                string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "SavedFiles");
                // To check folder is already created or not 
                if (!System.IO.Directory.Exists(uploadsFolder))
                {
                    // To Create New Folder
                    System.IO.Directory.CreateDirectory(uploadsFolder);
                }
                string fileName = Guid.NewGuid().ToString() + "_" + "result.txt";
                string filePath = Path.Combine(uploadsFolder, fileName);
                // Checking the above file
                if (!System.IO.File.Exists(filePath))
                {
                    // Creating the same file if it doesn't exist
                    using (StreamWriter sw = System.IO.File.CreateText(filePath))
                    {
                        foreach (int num in sortedNumbers)
                        {
                            sw.Write(num);
                            sw.Write(" ");
                        }
                    }
                }                
                result = true;
            }
            catch(Exception ex)
            {
                throw new Exception("Error in SortGivenNumbers. Details : "+ ex.Message);
            }
            return result;
        }

        // To get sorted numbers from recently saved txt file
        public async Task<int[]> LoadLatestResultFile()
        {
            int[] sortedNumbers = new int[] { };
            try
            {
                string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "SavedFiles");
                var directory = new DirectoryInfo(folderPath);
                // To get recent created file
                var latestFile = directory.GetFiles().OrderByDescending(f => f.LastWriteTime).First();
                // Opening the file for reading
                using (StreamReader sr = System.IO.File.OpenText(Path.Combine(folderPath, latestFile.FullName)))
                {
                    string content = "";
                    while ((content = sr.ReadLine()) != null)
                    {
                        sortedNumbers =  content.Trim().ToString().Split(' ').Select(Int32.Parse).ToArray();
                    }
                }
               
            }
            catch(Exception ex)
            {
                throw new Exception("Error in LoadLatestResultFile. Details : " + ex.Message);
            }
            await Task.CompletedTask;
            return sortedNumbers;
        }
        #endregion

        #region QuickSort Alogirthm
        // QuickSort alogirthm
        private static async Task<int[]>  Quick_Sort(int[] arr, int left, int right)
        {
            try
            {
                if (left < right)
                {
                    int pivot = Partition(arr, left, right);

                    if (pivot > 1)
                    {
                      await  Quick_Sort(arr, left, pivot - 1);
                    }
                    if (pivot + 1 < right)
                    {
                      await  Quick_Sort(arr, pivot + 1, right);
                    }
                }
            }catch(Exception ex)
            {
                throw new Exception("Error in Quick_Sort. Details : " + ex.Message);
            }
            return arr;

        }
        private static int Partition(int[] arr, int left, int right)
        {
            try
            {
                int pivot = arr[left];
                while (true)
                {

                    while (arr[left] < pivot)
                    {
                        left++;
                    }

                    while (arr[right] > pivot)
                    {
                        right--;
                    }

                    if (left < right)
                    {
                        if (arr[left] == arr[right]) return right;

                        int temp = arr[left];
                        arr[left] = arr[right];
                        arr[right] = temp;
                    }
                    else
                    {
                        return right;
                    }
                }
            }catch(Exception ex)
            {
                throw new Exception("Error in Partition. Details : " + ex.Message);
            }
        }
        #endregion

    }
}
