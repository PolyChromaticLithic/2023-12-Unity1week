using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SortTree : MonoBehaviour
{
    private void Start()
    {
        
    }

    [ContextMenu("Sort Trees")]
    void Sort()
    {
        Transform[] trees = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            trees[i] = transform.GetChild(i);
        }
        Array.Sort(trees, (a, b) => (int)((b.transform.position.y - a.transform.position.y) * 1000));
        for (int i = 0; i < trees.Length; i++)
        {
            trees[i].position = new Vector3(trees[i].position.x, trees[i].position.y,10 - i * 0.01f);
        }
    }
}
