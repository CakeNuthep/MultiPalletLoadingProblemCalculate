using MultiPalletLoadingProblemCalculate.gnetic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiPalletLoadingProblemCalculate
{
    class Program
    {
        static void Main(string[] args)
        {
            BoxModel box1 = new BoxModel(0) { NumberBoxs = 10, Volumn = 30, Weight = 2 };
            BoxModel box2 = new BoxModel(1) { NumberBoxs = 10, Volumn = 40, Weight = 1 };
            BoxModel box3 = new BoxModel(2) { NumberBoxs = 10, Volumn = 50, Weight = 1 };

            PalletModel pallet1 = new PalletModel(0) { Number = 100, VolumnMax = 100, WeightMax = 5 };
            PalletModel pallet2 = new PalletModel(1) { Number = 100, VolumnMax = 80, WeightMax = 10 };
            PalletModel pallet3 = new PalletModel(2) { Number = 100, VolumnMax = 60, WeightMax = 3 };
            PalletModel pallet4 = new PalletModel(3) { Number = 100, VolumnMax = 80, WeightMax = 4 };

            List<BoxModel> listBoxModel = new List<BoxModel>() { box1, box2, box3 };
            List<PalletModel> listPalletModel = new List<PalletModel>() {pallet1,pallet2,pallet3,pallet4};


            GeneticProcess genetic = new GeneticProcess(listBoxModel,listPalletModel,0.8);
            genetic.Initialpopulation(50);
            List<Object> result = genetic.Operation(100);
            List<Chromosome> listBestChromosome = result.Cast<Chromosome>().ToList();
        }
    }
}
