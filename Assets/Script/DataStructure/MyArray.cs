using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using System;

public class MyArray :IDisposable
{
    System.IntPtr base_address;
    public MyArray(int lenth)
    {
        base_address = Marshal.AllocHGlobal(sizeof(int));
    }
    public int Find(int index)
    {
        return 0;
    }

    public void Dispose()
    {
        Marshal.FreeHGlobal(base_address);
    }
}

