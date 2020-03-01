using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiPalletLoadingProblemCalculate
{
    class PalletModel
    {
        public int Index { get; set; }
        public int Number { get; set; }
        public float CrossSectionalArea { get; set; }
        public float VolumnMax { get; set; }
        public float WeightMax { get; set; }

        private PalletModel()
        {

        }
        public PalletModel(int index)
        {
            this.Index = index;
        }
        public PalletModel Copy()
        {
            return new PalletModel
            {
                Index = this.Index,
                Number = this.Number,
                CrossSectionalArea = this.CrossSectionalArea,
                VolumnMax = this.VolumnMax,
                WeightMax = this.WeightMax
            };
        }
    }
}
