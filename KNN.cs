using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using System.IO;
using System.Threading.Tasks;

namespace KNNEnvirement
{
    class KNN
    {
        private int lines;

        // this holds the values of the training data
        private List<double[]>   trainingSetValues = new List<double[]>();
        // this holds the class associated with the values
        private List<string>     trainingSetClasses = new List<string>();
        //private double[] aLine = new double[10];


        // same for the test input
        private List<double[]>   testSetValues = new List<double[]>();
        private List<string>     testSetClasses = new List<string>();

        private int K;
        
        int numFeatures = 10; // the number of features // v2.0
        int numClasses = 6; // the number of classes // v2.0
        double[] unknown = new double[]{};
        string result;


        public enum DataType
        {
            TRAININGDATA, TESTDATA
        };

        public void Load()
        {
            TextAsset level = Resources.Load<TextAsset>("data5");
        }
        public void LoadData(double[] dataType){
            // create an appropiate array to hold the doubles from this line
           this.unknown = dataType; // unknown values will classify, in here the values from accelerometer will equal to unknown // v2.0
        }

        public void LoadData(string path, DataType dataType)
        {
            TextAsset level = Resources.Load<TextAsset>(path);
            if (level != null)
            {
                using (StreamReader file = new StreamReader(new MemoryStream(level.bytes)))
                {
                    string line;

                    this.lines = 0;

                    Console.WriteLine("[i] reading data from {0} ...", path);

                    while ((line = file.ReadLine()) != null)
                    {
                        // as we have a CSV file basically, split the line at each ','
                        string[] splitLine = line.Split(',').ToArray();

                        // and add them to a list
                        List<string> lineItems = new List<string>(splitLine.Length);
                        lineItems.AddRange(splitLine);

                        // create an appropiate array to hold the doubles from this line
                        double[] lineDoubles = new double[lineItems.Count - 1];
                        // and a string holding the class
                        string lineClass = lineItems.ElementAt(lineItems.Count - 1);

                        for (int i = 0; i < lineItems.Count - 1; i++)    // last item is the set class
                        {
                            double val = Double.Parse(lineItems.ElementAt(i), System.Globalization.CultureInfo.InvariantCulture);
                            lineDoubles[i] = val;
                        }

                        // finally, save them

                        if (dataType == DataType.TRAININGDATA)
                        {
                            this.trainingSetValues.Add(lineDoubles);
                            this.trainingSetClasses.Add(lineClass);
                        }
                        else if (dataType == DataType.TESTDATA)
                        {
                            this.testSetValues.Add(lineDoubles);
                            this.testSetClasses.Add(lineClass);
                        }
                        this.lines++;
                    }
                }
                //StreamReader file = new StreamReader(path);
            }
            //Debug.Log( this.lines);
            //file.Close();
        }

        public string Classify(int neighborsNumber)
        {
            this.K = neighborsNumber;

            int n = trainingSetValues.Count; // the number of data we labeled // v2.0
            IndexAndDistance[] info = new IndexAndDistance[n]; // the array we will put our distances // v2.0

            // create an array where we store the distance from our test data and the training data -> [0]
            // plus the index of the training data element -> [1]
            //double[][] distances = new double[trainingSetValues.Count][];

            for (int i = 0; i < n; ++i) {
                IndexAndDistance curr = new IndexAndDistance();
                double dist = Distance(unknown, trainingSetValues[i]);
                curr.idx = i; // current index of trainingSetValues
                curr.dist = dist; //current distance of the values
                info[i] = curr; // equals the actual IndexAndDistance list
            }
            
            Array.Sort(info);  // sort by distance
            for (int i = 0; i < K; ++i) { //1 yerine K
                string c = trainingSetClasses[info[0].idx].ToString();
                //string dist = info[i].dist.ToString("F3");
                this.result = c;
            }

            int result = Vote(info, trainingSetValues, numClasses, K);
            return Convert.ToString(result);
        }

        static double Distance(double[] unknown, double[] trainingSetValues)
        {
            double sum = 0.0;
            for (int i = 0; i < unknown.Length; ++i)
                sum += (unknown[i] - trainingSetValues[i]) * (unknown[i] - trainingSetValues[i]);
            return Math.Sqrt(sum);
        }

        static int Vote(IndexAndDistance[] info, List<double[]> trainData, int numClasses, int K)
        {
            int[] votes = new int[numClasses];  // One cell per class
            for (int i = 0; i < K; ++i) {       // Just first k
                int idx = info[i].idx;            // Which train item
                int c = (int)trainData[idx][2];   // Class in last cell
                ++votes[c];
            }
            int mostVotes = 0;
            int classWithMostVotes = 0;
            for (int j = 0; j < numClasses; ++j) {
                if (votes[j] > mostVotes) {
                    mostVotes = votes[j];
                    classWithMostVotes = j;
                }
            }
            return classWithMostVotes;
        }
    }
    
}
public class IndexAndDistance : IComparable<IndexAndDistance>
{
  public int idx;  // index of a training item
  public double dist;  // distance to unknown
  public int CompareTo(IndexAndDistance other) {
    if (this.dist < other.dist) return -1;
    else if (this.dist > other.dist) return +1;
    else return 0;
  }
}
