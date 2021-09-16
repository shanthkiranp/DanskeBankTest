using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DanskeTest.Interfaces
{
    public  interface INumberSorting 
    {
        Task<bool> SortGivenNumbers(int[] unSortedNumbes);
        Task<int[]> LoadLatestResultFile();
    }
}
