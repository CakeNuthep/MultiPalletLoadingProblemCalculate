using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiPalletLoadingProblemCalculate.gnetic
{
    public class GeneModel
    {
        public List<double> gene;

        public delegate double randomValue();

        private GeneModel()
        {

        }
        public GeneModel(int number, randomValue callBack)
        {
            gene = new List<double>();
            for(int i=0;i<number;i++)
            {
                gene.Add(callBack());
            }
        }

        public GeneModel Copy()
        {
            GeneModel newGene = new GeneModel();
            List<double> newData = new List<double>();
            for(int i=0; i<gene.Count;i++)
            {
                newData.Add(gene[i]);
            }
            newGene.gene = newData;
            return newGene;
        }
    }
}
