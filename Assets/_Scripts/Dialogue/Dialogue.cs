using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
Author: Group 50
Vincent Tse - 301050515
Siying Li - 301054781
Derek Chan - 301021992
Last Modified: April 14, 2021
Description: saves string of dialogues
 */
[System.Serializable]
public class Dialogue
{

    public string name;

    [TextArea(3, 10)]
    public string[] sentences;

}