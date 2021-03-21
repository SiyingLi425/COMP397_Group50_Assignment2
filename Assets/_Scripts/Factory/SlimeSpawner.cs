using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeSpawner : MonoBehaviour
{
    
    [SerializeField]
    public float spawnRatePerMinute = 30;
    
    public int currentCount = 0;

    public int slimes = 0;

    public int maxSlimes = 3;
    
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
                inst.transform.position = new Vector3(Random.Range(330.0f, 335.0f), 6.0f, Random.Range(295.0f, 300.0f));
                slimes++;
                
            }
            currentCount++;
        }
    }
}
