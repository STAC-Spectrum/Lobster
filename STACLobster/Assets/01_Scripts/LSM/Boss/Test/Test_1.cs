using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface TestInterface
{
    public void Initialize(CrystalBoss boss);
}


public class Test_1 : MonoBehaviour
{

    delegate void TestDelegate<T>(T a, T b);
    TestDelegate<int> test;

    



}


