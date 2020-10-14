using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using KNNEnvirement;
using System.IO;

public class Accelerometer : MonoBehaviour
{
    Gyroscope jiroskop;

    double xNormalGravityMean, yNormalGravityMean, xNormalGravitySd, xNormalGravityMax, yNormalGravityMax, zNormalGravityMax, xNormalGravityMin, yNormalGravityMin;
    double xAccValue, yAccValue, zAccValue, xNormalAccValue, yNormalAccValue, zNormalAccValue;
    double xNormalAccMax, xyNormalAccCorrelation;
    bool isGameStart = false;

    List<double> xAccValueList = new List<double>();
    List<double> yAccValueList = new List<double>();
    List<double> zAccValueList = new List<double>();

    List<double> xNormalAccValueList = new List<double>();
    List<double> yNormalAccValueList = new List<double>();
    List<double> zNormalAccValueList = new List<double>();

    List<double> xGravityValueList = new List<double>();
    List<double> yGravityValueList = new List<double>();
    List<double> zGravityValueList = new List<double>();

    List<double> xNormalGravityValueList = new List<double>();
    List<double> yNormalGravityValueList = new List<double>();
    List<double> zNormalGravityValueList = new List<double>();

    List<double> finaltable = new List<double>();

    double[] table = new double[]{};

    KNN knn = new KNN();
    string unknownclass = null;
    float speed = 0;

    void Start () {
        jiroskop = Input.gyro;
        jiroskop.enabled = true;
          
     }

    // Update is called once per frame
    void Update()
    {
        Vector3 rawAcceleration = Input.acceleration;
        Vector3 rawGravity = jiroskop.gravity;

        Vector3 acceleration = new Vector3(rawAcceleration.x,rawAcceleration.y,rawAcceleration.z);  //tek bir ivmeolcer verisi
        Vector3 gravity = new Vector3(rawGravity.x,rawGravity.y,rawGravity.z);  // tek bir yercekimi verisi                       

        xAccValueList.Add( acceleration.x );
        yAccValueList.Add( acceleration.y );
        zAccValueList.Add( acceleration.z );

        xGravityValueList.Add(gravity.x);
        yGravityValueList.Add(gravity.y);
        zGravityValueList.Add(gravity.z);

        if(xAccValueList.Count >= 128){

            isGameStart = true;

            xNormalAccMax = xAccValueList.Max();
            xyNormalAccCorrelation = Correlation(xAccValueList,yAccValueList);
            xNormalGravityMean = xGravityValueList.Average();
            yNormalGravityMean = yGravityValueList.Average();
            xNormalGravitySd = StandardDeviation(xNormalGravityValueList);
            xNormalGravityMax = xGravityValueList.Max();
            yNormalGravityMax = yGravityValueList.Max();
            zNormalGravityMax = zGravityValueList.Max();
            xNormalGravityMin = xGravityValueList.Min();
            yNormalGravityMin = yGravityValueList.Min();

            double[] table = {xNormalAccMax, xyNormalAccCorrelation, xNormalGravityMean, yNormalGravityMean, xNormalGravitySd, xNormalGravityMax, yNormalGravityMax, zNormalGravityMax, xNormalGravityMin, yNormalGravityMin};

            knn.LoadData(table);
            knn.LoadData("data5",KNN.DataType.TRAININGDATA);

            unknownclass = knn.Classify(1);
            Debug.Log(unknownclass);

            speed = float.Parse(unknownclass/*,System.Globalization.CultureInfo.InvariantCulture*/);

            PlayerControl.MovementSettings.forwardVelocity = speed;

            table = new double[]{};
            clearLists();
        } else if (!isGameStart)
        {
            PlayerControl.MovementSettings.forwardVelocity = 0;
        }
    }

    public static double Correlation(List<double> Xs, List<double> Ys) {
    double sumX = 0;
    double sumX2 = 0;
    double sumY = 0;
    double sumY2 = 0;
    double sumXY = 0;

    int n = Xs.Count< Ys.Count ? Xs.Count : Ys.Count;

    for (int i = 0; i < n; ++i) {
      double x = Xs[i];
      double y = Ys[i];

      sumX += x;
      sumX2 += x * x;
      sumY += y;
      sumY2 += y * y;
      sumXY += x * y;
    }

    double stdX = System.Math.Sqrt(sumX2 / n - sumX * sumX / n / n);
    double stdY = System.Math.Sqrt(sumY2 / n - sumY * sumY / n / n);
    double covariance = (sumXY / n - sumX * sumY / n / n);

    return covariance / stdX / stdY; 
  }

  public static List<double> NormalizeData(IEnumerable<double> data, int min, int max){
    double dataMax = data.Max();
    double dataMin = data.Min();
    double valueRange = dataMax - dataMin;

    double scaleRange = max - min;

    return data
        .Select (i =>
        ((scaleRange * (i - dataMin))
            / valueRange)
        + min).ToList();
    }

    public void clearLists(){
        xAccValueList = new List<double>();
        yAccValueList = new List<double>();
        zAccValueList =  new List<double>();

        xNormalAccValueList = new List<double>();
        yNormalAccValueList = new List<double>();
        zNormalAccValueList = new List<double>();

        xGravityValueList = new List<double>();
        yGravityValueList = new List<double>();
        zGravityValueList = new List<double>();

        xNormalGravityValueList = new List<double>();
        yNormalGravityValueList = new List<double>();
        zNormalGravityValueList = new List<double>();

        finaltable = new List<double>();
    }

    private double StandardDeviation(IEnumerable<double> values){   
        double standardDeviation = 0;

        if (values.Any()) 
        {      
            // Compute the average.     
            double avg = values.Average();

            // Perform the Sum of (value-avg)_2_2.      
            double sum = values.Sum(d => System.Math.Pow(d - avg, 2));

            // Put it all together.      
            standardDeviation = System.Math.Sqrt((sum) / (values.Count()-1));   
        }  

        return standardDeviation;
    }
}



