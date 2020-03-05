using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiPalletLoadingProblemCalculate.gnetic
{
    class Chromosome : ChromosomeInterface
    {
        double alpha;
        int size;
        double score;
        ChromosomeModel chromosome;
        const int INDEX_GENE1 = 1;
        const int INDEX_GENE2 = 2;
        const int INDEX_GENE3 = 3;
        List<BoxModel> lookUpBoxModel, lookUpBoxModelTemp;
        List<PalletModel> lookUpPalletModel;

        public Object getChromosome()
        {
            return this.chromosome;
        }

        private Chromosome(List<BoxModel> lookUpBoxModel, List<PalletModel> lookUpPalletModel, ChromosomeModel chromosome , int size, double alpha)
        {
            this.lookUpBoxModel = lookUpBoxModel;
            this.lookUpPalletModel = lookUpPalletModel;
            this.size = size;
            this.alpha = alpha;
            this.chromosome = chromosome;

        }

        public Chromosome(List<BoxModel> lookUpBoxModel, List<PalletModel> lookUpPalletModel,int size,double alpha)
        {
            this.lookUpBoxModel = lookUpBoxModel;
            this.lookUpPalletModel = lookUpPalletModel;
            this.size = size;
            this.alpha = alpha;
            chromosome = (ChromosomeModel)randomChromosome(size);
            
        }

        public List<Object> crossover(object chromosome, int[] listIndexGene)
        {
            Object chromosomeData = ((Chromosome)chromosome).getChromosome();
            ChromosomeModel offspringChromosome1 = ((ChromosomeModel)chromosomeData).Copy();
            ChromosomeModel offspringChromosome2 = (this.chromosome).Copy();
            for (int i = 0; i < listIndexGene.Length; i++)
            {
                Random random = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);
                if (listIndexGene[i] < this.chromosome.listGene.Count)
                {
                    int indexGene = listIndexGene[i];
                    if (indexGene == INDEX_GENE1 || indexGene == INDEX_GENE2 || indexGene == INDEX_GENE3)
                    {
                        GeneModel parentGene1 = ((ChromosomeModel)chromosomeData).listGene[indexGene];
                        GeneModel parentGene2 = this.chromosome.listGene[indexGene];
                        GeneModel offspringGene1 = offspringChromosome1.listGene[indexGene];
                        GeneModel offspringGene2 = offspringChromosome2.listGene[indexGene];
                        int subIndexGene = random.Next(0, parentGene2.gene.Count - 1);
                        for (int crossIndex=subIndexGene;i<parentGene2.gene.Count;i++)
                        {
                            offspringGene1.gene[crossIndex] = parentGene2.gene[crossIndex];
                            offspringGene2.gene[crossIndex] = parentGene1.gene[crossIndex];
                        }
                    }
                }
            }
            return new List<object> { new Chromosome(this.lookUpBoxModel,this.lookUpPalletModel,offspringChromosome1,this.size,this.alpha)
                                     , new Chromosome(this.lookUpBoxModel,this.lookUpPalletModel,offspringChromosome2,this.size,this.alpha) };

        }

        public object decodeChromosome()
        {
            DecodeModel decodeData = new DecodeModel();
            List<BoxModel> listBoxModel = copyListBoxModel();
            List<PalletModel> listPalletModel = copyListPalletModel();
            //Gene
            GeneModel gene1 = this.chromosome.listGene[INDEX_GENE1];
            GeneModel gene2 = this.chromosome.listGene[INDEX_GENE2];
            GeneModel gene3 = this.chromosome.listGene[INDEX_GENE3];

            //decode Gene1
            foreach (float index in gene1.gene)
            {
                decodeData.ListBoxIndex.Add(listBoxModel[(int)index-1].Index);
                listBoxModel.RemoveAt((int)index-1);
            }

            foreach(float ratio in gene2.gene)
            {
                decodeData.ListRatio.Add(ratio);
            }

            foreach(float palletIndex in gene3.gene)
            {
                decodeData.ListPalletIndex.Add((int)palletIndex);
            }
            return decodeData;
        }

        private List<BoxModel> copyListBoxModel()
        {
            List<BoxModel> listBoxModel = new List<BoxModel>();
            foreach(BoxModel box in this.lookUpBoxModel)
            {
                listBoxModel.Add(box.Copy());
            }
            return listBoxModel;
        }

        private List<PalletModel> copyListPalletModel()
        {
            List<PalletModel> listPalletModel = new List<PalletModel>();
            foreach(PalletModel pallet in this.lookUpPalletModel)
            {
                listPalletModel.Add(pallet.Copy());
            }
            return listPalletModel;
        }

        public object encodeChromosome(object data)
        {
            throw new NotImplementedException();
        }

        public double getValue()
        {
            DecodeModel decodeData = (DecodeModel)decodeChromosome();
            float score = 0;
            for(int i=0;i<size;i++)
            {
                int indexBox = -1, indexPallet = -1;
                float ratio = -1;
                if(i < decodeData.ListBoxIndex.Count)
                {
                    indexBox = decodeData.ListBoxIndex[i];
                }

                if (i < decodeData.ListPalletIndex.Count)
                {
                    indexPallet = decodeData.ListPalletIndex[i];
                }

                if (i < decodeData.ListRatio.Count)
                {
                    ratio = decodeData.ListRatio[i];
                }

                if(indexBox != -1 && indexPallet != -1 && ratio != -1 )
                {
                    float numberBox1 = ratio * lookUpBoxModel[indexBox].NumberBoxs;
                    float numberBox2 = lookUpBoxModel[indexBox].NumberBoxs - numberBox1;
                    int numberBox = 0;
                    if (numberBox1 > numberBox2)
                    {
                        numberBox = (int)Math.Ceiling(numberBox1);
                    }
                    else
                    {
                        numberBox = (int)Math.Floor(numberBox1);
                    }

                    float totalVolumnBox = lookUpBoxModel[indexBox].Volumn * numberBox;
                    float totalWeight = lookUpBoxModel[indexBox].Weight * numberBox;

                    int numberPallet = Math.Min((int)Math.Ceiling(totalVolumnBox*alpha / lookUpPalletModel[indexPallet].VolumnMax),
                        (int)Math.Ceiling(totalWeight/lookUpPalletModel[indexPallet].WeightMax));
                    score += this.lookUpPalletModel[indexPallet].CrossSectionalArea * numberPallet;
                }
            }
            return score;
        }

        public void mutation(int[] listIndexGene)
        {
            for (int i = 0; i < listIndexGene.Length; i++)
            {
                Random random = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);
                if (listIndexGene[i] < this.chromosome.listGene.Count)
                {
                    int indexGene = listIndexGene[i];
                    if(indexGene == INDEX_GENE2)
                    {
                        double randValue = randomGene2();
                        GeneModel gene = chromosome.listGene[indexGene];
                        int subIndexGene = random.Next(0, gene.gene.Count - 1);
                        gene.gene[subIndexGene] = randValue;
                        
                    }
                    else if(indexGene == INDEX_GENE3)
                    {

                        double randValue = randomGene3();
                        GeneModel gene = chromosome.listGene[indexGene];
                        int subIndexGene = random.Next(0, gene.gene.Count - 1);
                        gene.gene[subIndexGene] = randValue;
                    }
                }
            }
        }

        public object randomChromosome(int size)
        {
            ChromosomeModel tempChromosome = new ChromosomeModel();
            lookUpBoxModelTemp = copyListBoxModel();
            tempChromosome.generateGene(INDEX_GENE1,
                                        size,
                                        randomGene1);
            tempChromosome.generateGene(INDEX_GENE2,
                                        size, 
                                        randomGene2);
            tempChromosome.generateGene(INDEX_GENE3,
                                        size,
                                        randomGene3);
            lookUpBoxModelTemp = null;
            return tempChromosome;

        }

        public double randomGene1()
        {
            int sizeGene1 = lookUpBoxModelTemp.Count;
            Random rnd = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);
            int randomValue = rnd.Next(1, sizeGene1);
            lookUpBoxModelTemp.RemoveAt(randomValue-1);
            return randomValue;
        }

        public double randomGene2()
        {
            Random random = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);
            double randomValue = random.NextDouble();
            return randomValue;
        }

        public double randomGene3()
        {
            int sizeGene3 = lookUpPalletModel.Count;
            Random rnd = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);
            double randomValue = rnd.Next(1, sizeGene3);
            return randomValue;
            
        }
    }
}
