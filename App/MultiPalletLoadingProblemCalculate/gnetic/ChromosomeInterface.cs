using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiPalletLoadingProblemCalculate.gnetic
{
    interface ChromosomeInterface
    {
        Object decodeChromosome();
        Object encodeChromosome(Object data);
        void mutation(int[] indexGene);
        List<Object> crossover(Object chromosome,int[] listIndexGene);
        double getValue();
        Object randomChromosome(int size);
    }

}
