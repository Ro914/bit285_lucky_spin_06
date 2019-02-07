using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LuckySpin.ViewModels
{
    public class SpinViewModel
    {
        public long playerId { get; set; }
        public string Name { get; set; }
        public decimal Balance { get; set; }
        public int Luck { get; set; }
        public Boolean IsWinning { get; set; }
        public int A { get; set; }

        public int B { get; set; }

        public int C { get; set; }
    }
}
