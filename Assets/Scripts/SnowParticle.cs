using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowParticle : MonoBehaviour
{
    [SerializeField] Player player;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnParticleCollision(GameObject obj)
    {
        if (Player.instance == null) return;
        if (obj.gameObject.tag == "Player")
        {
            Player.instance.HP -= 0.15;
        }
    }
}
