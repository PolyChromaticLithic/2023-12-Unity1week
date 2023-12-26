using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceData : MonoBehaviour
{
    static public Sprite[] sprites;
    [SerializeField] private Sprite[] sprites2;
    
    private void Awake()
    {
        sprites = sprites2;
    }
}
