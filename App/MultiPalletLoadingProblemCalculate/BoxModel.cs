using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiPalletLoadingProblemCalculate
{
    class BoxModel
    {
        public int Index { get; set; }
        public int NumberBoxs { get; set; }
        public float Volumn { get; set; }
        public float Weight { get; set; }

        private BoxModel()
        {
            
        }
        public BoxModel(int index)
        {
            this.Index = index;
        }
        public BoxModel Copy()
        {
            return new BoxModel 
            { 
                Index = this.Index,
                NumberBoxs = this.NumberBoxs, 
                Volumn = this.Volumn, 
                Weight = this.Weight 
            };
        }
    }
}
