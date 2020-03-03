using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiPalletLoadingProblemCalculate.gnetic
{
    class GeneticProcess : GeneticInterface
    {
        int numberTopChromosomeCrossover = 5;
        int numberOtherChromosomeCrossover = 6;
        int selectMutationMax = 5;
        public List<Chromosome> Population { get; set; }
        int[] indexMuation = { 2, 3 };
        int[] indexCrossover = { 1, 2, 3 };
        List<BoxModel> lookUpBoxModel;
        List<PalletModel> lookUpPalletModel;
        Chromosome bestChromosomeOld;
        Chromosome bestChromosome;
        int numberPpopulation;
        double alpha;

        public GeneticProcess(List<BoxModel> lookUpBoxModel, List<PalletModel> lookUpPalletModel,double alpha)
        {
            this.lookUpBoxModel = lookUpBoxModel;
            this.lookUpPalletModel = lookUpPalletModel;
            Population = new List<Chromosome>();
            this.alpha = alpha;
        }

        public object GetBestChromosome(int number)
        {
            return bestChromosome;
        }

        public List<object> getFitessValue(int number)
        {
            Population.Sort(sortBestChromosome);
            bestChromosome = Population[0];
            return Population.GetRange(0, number).Cast<Object>().ToList();
        }

        private int sortBestChromosome(Chromosome chromosome1,Chromosome chromosome2)
        {
            double value1 = chromosome1.getValue();
            double value2 = chromosome2.getValue();
            return value1.CompareTo(value2) * -1;
        }

        

        public void Initialpopulation(int number)
        {
            this.numberPpopulation = number;
            for (int i=0; i<number;i++)
            {
                Chromosome chromosome = new Chromosome(this.lookUpBoxModel,this.lookUpPalletModel,this.lookUpBoxModel.Count, alpha);
                Population.Add(chromosome);
            }

        }

        public bool IsMustMutation(double ratio = 0.1)
        {
            if(bestChromosome!=null & bestChromosomeOld!=null)
            {
                double score = bestChromosome.getValue();
                double scoreOld = bestChromosomeOld.getValue();
                if(Math.Abs(score - scoreOld)/scoreOld > ratio)
                {
                    return true;
                }
            }
            return false;
        }

        public List<object> Operation(int maxLoop)
        {

            //while (demo.population.fittest < 5)
            //{
            //    ++demo.generationCount;

            //    //Do selection
            //    demo.selection();

            //    //Do crossover
            //    demo.crossover();

            //    //Do mutation under a random probability
            //    if (rn.nextInt() % 7 < 5)
            //    {
            //        demo.mutation();
            //    }

            //    //Add fittest offspring to population
            //    demo.addFittestOffspring();

            //    //Calculate new fitness value
            //    demo.population.calculateFitness();

            //    System.out.println("Generation: " + demo.generationCount + " Fittest: " + demo.population.fittest);
            //}

            for(int i = 0; i < maxLoop; i++)
            {
                Population = getFitessValue(this.numberPpopulation).Cast<Chromosome>().ToList();

                SinglePointCrossover(numberTopChromosomeCrossover, numberOtherChromosomeCrossover);

                if(IsMustMutation())
                {
                    for (int selectMutation=0; selectMutation < selectMutationMax && selectMutation < Population.Count; selectMutation++)
                    {
                        Population[selectMutation].mutation(indexMuation);
                    }
                }

            }

            return getFitessValue(this.numberPpopulation);

        }

        public void SinglePointCrossover(int numberTopChromosome, int numberOtherChromosome)
        {
            if (numberTopChromosome <= Population.Count && numberTopChromosome + numberOtherChromosome <= Population.Count)
            {
                this.bestChromosomeOld = this.bestChromosome;
                List<Chromosome> listTopChromosome = Population.GetRange(0, numberTopChromosome);
                //List<Chromosome> listOtherChromosome = Population.GetRange(numberTopChromosome, numberTopChromosome + numberOtherChromosome);
                List<Chromosome> listOtherChromosome = new List<Chromosome>();
                for(int i=0;i<numberOtherChromosome;i++)
                {
                    Random rnd = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);
                    int randomValue = rnd.Next(numberTopChromosome, numberTopChromosome + numberOtherChromosome);
                    listOtherChromosome.Add(Population[randomValue]);
                }

                //crossing
                for(int i=0;i<listTopChromosome.Count;i++)
                {
                    Chromosome chromosome1 = listTopChromosome[i];
                    for(int j=0;j<listOtherChromosome.Count;j++)
                    {
                        Chromosome chromosome2 = listOtherChromosome[j];
                        List<Object> offsprings = chromosome1.crossover(chromosome2, indexCrossover);
                        Population.AddRange(offsprings.Cast<Chromosome>().ToList());
                    }
                }
                
            }
        }
    }
}
