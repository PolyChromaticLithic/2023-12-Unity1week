using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Tree : MonoBehaviour
{
    [SerializeField] private SpriteRenderer tree;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (transform.position.y >= Player.playerTransform.position.y - 0.2)
        {
            tree.sortingOrder = 99;
        }
        else
        {
            tree.sortingOrder = 101;
        }
        tree.color = new Color(255, 255, 255, 150 - Mathf.Floor(Player.playerTransform.position.y - 0.2f - transform.position.y) * 10) / 150;
    }
}
