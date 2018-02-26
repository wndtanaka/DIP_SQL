using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserDetails<T>
{
    public T[] data;
    public int amount;

    public UserDetails()
    {
        amount = 0;
    }

    public void Add(T item)
    {
        T[] cache = new T[amount + 1];
        if (data != null)
        {
            for (int i = 0; i < data.Length; i++)
            {
                cache[i] = data[i];
            }
        }
        cache[amount] = item;
        data = cache;
        amount++;
    }
}
