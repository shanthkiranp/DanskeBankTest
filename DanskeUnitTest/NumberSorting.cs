using DanskeTest.Interfaces;
using DanskeTest.Repository;
using System;
using Xunit;

namespace DanskeUnitTest
{
    public class NumberSorting
    {
        SortingRepository _SortNumber = new SortingRepository();

        // To Test SortGivenNumbers Method
        [Fact]
        public async void ut_SortGivenNumbers()
        {
            try
            {
                bool actualValue = true;
                int[] numbersToSort = { 5, 9, 6, 8, 1, 3, 7, 2 };
                //To sort given numbers and save in txt file
                bool expectedValue = await _SortNumber.SortGivenNumbers(numbersToSort);
                Assert.Equal(expectedValue, actualValue); 
            }
            catch (Exception ex)
            {
                throw new Exception("Error in ut_SortGivenNumbers. Details: " + ex.Message);
            }
        }

        // To Test LoadLatestResultFile Method
        [Fact]
        public async void ut_LoadLatestResultFile()
        {
            try
            {
                bool actualValue = true, expectedValue = false;
                // To get sorted numbers from recently saved txt file
                int[] sortedNumber = await _SortNumber.LoadLatestResultFile();
                if(sortedNumber.Length > 0)
                {
                    expectedValue = true;
                }
                Assert.Equal(expectedValue, actualValue);
            }
            catch (Exception ex)
            {
                throw new Exception("Error in ut_LoadLatestResultFile. Details: " + ex.Message);
            }
        }
    }
}
