using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keep.UtilityTools.Utilities
{
    class LCList
    {
        public static List<T> GetSubList<T>(List<T> originalList, int startIndex, int endIndex) 
        {
            if (originalList != null && startIndex > 0 && endIndex > 0 && endIndex >= startIndex && originalList.Count >= endIndex)
            {
                var sublist = new List<T>();
                for (var i = startIndex; i <= endIndex; i++)
                {
                    sublist.Add(originalList[i]);
                }

                return sublist;
            }
       
            return originalList;
        }
    }
}
