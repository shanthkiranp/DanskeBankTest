using DanskeTest.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DanskeTest.Controllers
{
    [ApiController]
    [Route("Api/NumberSorting")]
    public class NumberSorting : ControllerBase
    {
        #region variables
        private INumberSorting _NumberSorting;
        #endregion
        #region Constructor
        public NumberSorting(INumberSorting iNumberSorting)
        {
            _NumberSorting = iNumberSorting;
        }
        #endregion

        #region General Methods

        [Route("SortGivenNumbers")]
        [HttpPost]
        public async Task<string> SortGivenNumbers(string numbers)
        {
            string result = String.Empty;
            try
            {
                int[]  numbersToSort = numbers.ToString().Split(' ').Select(Int32.Parse).ToArray();
                //To sort given numbers and save in txt file
               bool  status = await _NumberSorting.SortGivenNumbers(numbersToSort);
                if (status)
                {
                    result = "Response save in txt file, Kindly run second api to get result";
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error in SortGivenNumbers. Details: " + ex.Message);
            }
            return result;
        }
        [Route("LoadLatestResultFile")]
        [HttpGet]
        public async Task<int[]> LoadLatestResultFile()
        {
            int[] sortedNumbers = new int[] { };
            try
            {
                // To get sorted numbers from recently saved txt file
                sortedNumbers = await _NumberSorting.LoadLatestResultFile();
            }
            catch(Exception ex)
            {
                throw new Exception("Error in LoadLatestResultFile. Details: " + ex.Message);
            }
            return sortedNumbers;
        }
        #endregion
    }
}
