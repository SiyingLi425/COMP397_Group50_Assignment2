using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
Author: Group 50
Vincent Tse - 301050515
Siying Li - 301054781
Derek Chan - 301021992
Last Modified: April 14, 2021
Description: Slime Spawner that spawns in Slime enemies based in properties
 */
public class SlimeSpawner : MonoBehaviour
{
    
    [SerializeField]
    public float spawnRatePerMinute = 30;
    
    public int currentCount = 0;

    public int slimes = 0;

    public int maxSlimes = 3;
    public int range = 5;
    
    [SerializeField]
    private TimedObjectFactory factory;

  
    private void Update()
    {
        var targetCount = Time.time * (spawnRatePerMinute / 60);
        while (targetCount > currentCount)
        {
            if (slimes < maxSlimes)
            {
                var inst = factory.GetNewInstance();
                float x = this.transform.position.x;
                float z = this.transform.position.z;
                inst.transform.position = new Vector3(Random.Range(x - range, x + range), 6.0f, Random.Range(z - range, z + range));
                //inst.transform.position = new Vector3(Random.Range(330.0f, 335.0f), 6.0f, Random.Range(295.0f, 300.0f));
                slimes++;
                
            }
            currentCount++;
        }
    }
}
