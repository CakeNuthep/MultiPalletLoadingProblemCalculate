using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiPalletLoadingProblemCalculate.gnetic
{
    interface GeneticInterface
    {
        void Initialpopulation(int number);
        List<Object> GetBestChromosome(int number);
        Object SinglePointCrossover(int numberTopChromosome, int numberOtherChromosome);
        List<Object> getFitessValue(int number);
        bool IsMustMutation();
        List<Object> Operation();
    }
}
