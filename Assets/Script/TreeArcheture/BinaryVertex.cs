using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinaryVertex<T>
{
    public T value;
    public BinaryVertex<T> left;
    public BinaryVertex<T> right;
}
