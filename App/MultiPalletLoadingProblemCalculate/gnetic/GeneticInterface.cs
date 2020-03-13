using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiPalletLoadingProblemCalculate.gnetic
{
    interface GeneticInterface
    {
        bool Initialpopulation(int number);
        Object GetBestChromosome(int number);
        void SinglePointCrossover(int numberTopChromosome, int numberOtherChromosome);
        List<Object> getFitessValue(int number);
        bool IsMustMutation(double ratio);
        List<Object> Operation(int maxLoop);
    }
}
