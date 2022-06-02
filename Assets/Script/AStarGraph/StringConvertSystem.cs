using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public static class StringConvertSystem 
{
    public static int[] ConvertToIntArray(string text)
    {
        var splitString = text.Split('+');
        var length = splitString.Length;
        var intArray = new int[length];

        for (int i = 0; i < length; i++)
        {
            intArray[i] = Convert.ToInt32(splitString[i]);
        }

        return intArray;
    }
}
