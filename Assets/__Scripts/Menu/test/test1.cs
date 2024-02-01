using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test1 : MonoBehaviour
{
    public static int test1Var = 0;
    public int a;

    void Update()
    {
        a = test1Var;
        test1Var++;
    }
}
