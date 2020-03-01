using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiPalletLoadingProblemCalculate
{
    class DecodeModel
    {
        public List<int> ListBoxIndex { get; set; }
        public List<float> ListRatio { get; set; }
        public List<int> ListPalletIndex { get; set; }

        public DecodeModel()
        {
            this.ListBoxIndex = new List<int>();
            this.ListPalletIndex = new List<int>();
            this.ListRatio = new List<float>();
        }
    }
}
