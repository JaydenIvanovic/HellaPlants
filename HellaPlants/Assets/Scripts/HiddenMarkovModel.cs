using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Accord.Math;
using Accord.Statistics.Models.Markov.Topology;
using Accord.Statistics.Models.Markov.Learning;
using Accord.Statistics.Models.Markov;

// Basic implementation of a hidden markov model.
// I need to look into the details a bit more but the 
// general gist is here.
public class HiddenMarkovModel 
{
    // Inner classes instead of enums so
    // I don't have to cast them to ints everywhere.
    private static class Directions 
    {
        public const int N = 0;
        public const int NE = 1;
        public const int NW = 2;
        public const int S = 3; 
        public const int SE = 4;
        public const int SW = 5;
        public const int E = 6;
        public const int W = 7;
    }

    private static class Classes 
    {
        public const int SUN = 0;
        public const int RAIN = 1;
        public const int WIND = 2;
        public const int FERT = 3;
    }
    
    private HiddenMarkovClassifier classifier;

    public HiddenMarkovModel()
    {
        // Training data for each spell. What
        // type of sequence of directions the gesture expects.
        int[][] inputSequences =
        {
            // Expected sun sequences.
            new[] { Directions.E, Directions.S, Directions.W, Directions.N},        
            new[] { Directions.W, Directions.S, Directions.E, Directions.N},     
            new[] { Directions.S, Directions.W, Directions.N, Directions.E},     

            // Expected rain sequences.
            new[] { Directions.SW, Directions.E, Directions.SW},
            new[] { Directions.SW, Directions.S, Directions.SW, Directions.E, Directions.SE},
            new[] { Directions.SW, Directions.E, Directions.SE, Directions.S, Directions.SE, Directions.S},

            // Expected wind sequences.
            new[] {Directions.SE, Directions.S, Directions.SW},
            new[] {Directions.E, Directions.SE, Directions.SW, Directions.W},
            new[] {Directions.SE, Directions.W},
            new[] {Directions.SE, Directions.SW},

            // Expected fertilizer sequences.
            new[] {Directions.SW, Directions.E},
            new[] {Directions.W, Directions.SW, Directions.SE, Directions.E},
            new[] {Directions.W, Directions.SW, Directions.SE},
            new[] {Directions.SW, Directions.S, Directions.SE}
        };

        // The class classification labels assigned to each sequence.
        // In other words, the class which the input sequence is being used to train.
        int[] outputLabels =
        {
            /* Sequences  1-3 are for training Sun gesture: */ Classes.SUN, Classes.SUN, Classes.SUN,
            /* Sequences  4-6 are for training Rain gesture: */ Classes.RAIN, Classes.RAIN, Classes.RAIN,
            /* Sequences  7-10 are for training Wind gesture: */ Classes.WIND, Classes.WIND, Classes.WIND, Classes.WIND,
            /* Sequences  11-14 are for training Fertilizer gesture: */ Classes.FERT, Classes.FERT, Classes.FERT, Classes.FERT
        };

        // We will use a single topology for all inner models, but we 
        // could also have explicitled different topologies for each:
        ITopology forward = new Ergodic(states: 8);

        // Now we create a hidden Markov classifier with the given topology
        classifier = new HiddenMarkovClassifier(classes: 4, topology: forward, symbols: 8);

        // And create a algorithms to teach each of the inner models
        var teacher = new HiddenMarkovClassifierLearning(classifier,

            // We can specify individual training options for each inner model:
            modelIndex => new BaumWelchLearning(classifier.Models[modelIndex])
            {
                Tolerance = 0.001, // iterate until log-likelihood changes less than 0.001
                Iterations = 0     // don't place an upper limit on the number of iterations
            }
        );

        // Then let's call its Run method to start learning
        double error = teacher.Run(inputSequences, outputLabels);
    }


    // Give this the sequence of directions and it will spit
    // out the spell to perform.
    public int classifySequence(List<Gestures.direction> sequence)
    {
        Gestures.direction[] directions = sequence.ToArray();

        // Have to convert the enum to an integer array...
        int[] intDirections = new int[directions.Length];
        
        int count = 0;
        foreach(Gestures.direction d in directions)
        {
            intDirections[count] = (int)d;
            count++;
        }

        int classification = classifier.Compute(intDirections);
        
        Debug.Log("HMM Recognized: " + classification);
        return classification;
    }
}
