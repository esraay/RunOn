using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Accelerometer : MonoBehaviour
{
    float Accx;
    float Accy;
    float Accz;
    List<Vector3> datasAcc = new List<Vector3>();
    List<Vector3> datasGra = new List<Vector3>();
    float[] fixedData;

    // Update is called once per frame
    void Update()
    {
        Vector3 rawAcceleration = Input.acceleration;
        Vector3 rawGravity = Input.gyro.gravity;

        Vector3 acceleration = new Vector3(rawAcceleration.x,rawAcceleration.y,rawAcceleration.z);
        Vector3 gravity = new Vector3(rawGravity.x,rawGravity.y,rawGravity.z);
        Vector3 linearAccel = acceleration - gravity;

        datasAcc.Add(linearAccel);
        datasGra.Add(gravity);
        
      
        if(datasAcc.Count >= 128){
            Vector3 maxVector = Vector3.zero;
             for(int i = 0; i < datasAcc.Count; i++){
                maxVector = (datasAcc[i].x > maxVector.x) ?  datasAcc[i] : maxVector;
            }
            
            datasAcc.Clear();
            Debug.Log(datasAcc.Count);
        }
    

    }
}
