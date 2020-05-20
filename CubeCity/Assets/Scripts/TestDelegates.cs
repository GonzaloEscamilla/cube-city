using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[HelpURL("https://docs.microsoft.com/dotnet/csharp/tour-of-csharp/attributes")]
public class TestDelegates : MonoBehaviour
{
    public delegate void MyDelegate(int num);
    public MyDelegate testDelegado;


    int nim;
    
    private void Start()
    {
        MyDelegate delegado = new MyDelegate(DoSomethign);

        MyDelegate delegado2;
        delegado2 = DoSomethign;

        MyDelegate delegado3 = DoSomethign;

        delegado(2);
        delegado2(3);
        delegado3(1);

        // -------- Multicasteo

        MyDelegate multiCastDelegate1 = new MyDelegate(DoSomethign);
        multiCastDelegate1 += DoSomethign;
        multiCastDelegate1 += DoSomethign;
        multiCastDelegate1 += DoSomethign;
        multiCastDelegate1(5);

        MyDelegate multiCastDelegate2 = DoSomethign;
        multiCastDelegate2 += DoSomethign;
        multiCastDelegate2(10);

        MyDelegate multiCastDelegate3;
        multiCastDelegate3 = DoSomethign;
        multiCastDelegate3 += DoSomethign;

        testDelegado += DoSomethign;

        MyDelegate d1 = new MyDelegate(DoSomethign);
        MyDelegate d2 = DoSomethign;
        MyDelegate dm3 = d1 + d2;
        dm3 += d1 + d2 + DoSomethign;
        dm3(55);


    }

    private void DoSomethign(int num)
    {
        Debug.Log("olis el numero es: " + num);
    }
}
