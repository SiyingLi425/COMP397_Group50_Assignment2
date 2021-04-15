using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
Author: Group 50
Vincent Tse - 301050515
Siying Li - 301054781
Derek Chan - 301021992
Last Modified: April 14, 2021
Description: dont destroy script to load over to another scene.
 */
public class DontDestroy : MonoBehaviour
{
    public bool load = false;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        
    }

}
