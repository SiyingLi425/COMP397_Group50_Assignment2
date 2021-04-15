using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
Author: Group 50
Vincent Tse - 301050515
Siying Li - 301054781
Derek Chan - 301021992
Last Modified: April 14, 2021
Description: Generic Factory that has instatiate 
 */
public class GenericFactory<T> : MonoBehaviour where T : MonoBehaviour
{
    // Reference to prefab of whatever type.
    [SerializeField]
    private T prefab;

    /// <summary>
    /// Creating new instance of prefab.
    /// </summary>
    /// <returns>New instance of prefab.</returns>
    public T GetNewInstance()
    {
        return Instantiate(prefab);
    }
}
