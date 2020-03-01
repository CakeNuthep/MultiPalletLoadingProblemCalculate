using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiPalletLoadingProblemCalculate.gnetic
{
    public class ChromosomeModel
    {
        public Dictionary<int, GeneModel> listGene;
        public ChromosomeModel()
        {
            listGene = new Dictionary<int, GeneModel>();
        }

        private ChromosomeModel(Dictionary<int,GeneModel> listGene)
        {
            this.listGene = listGene;
        }

        public void generateGene(int index,int numberGene,GeneModel.randomValue callback)
        {
            GeneModel gene = new GeneModel(numberGene, callback);
            listGene.Add(index, gene);
        }

        public ChromosomeModel Copy()
        {
            Dictionary<int, GeneModel> listGene = new Dictionary<int, GeneModel>();
            foreach (var geneDic in this.listGene) 
            {
                listGene.Add(geneDic.Key, geneDic.Value.Copy());
            }
            ChromosomeModel newChromosome = new ChromosomeModel(listGene);
            return newChromosome;
        }
    }
}
