using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snow : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(new Vector3(0f,0f,GetNoise(Time.time, 0.5f) * 10 + Time.time));
    }

    static float GetNoise(float time, float speed)
    {
        return Mathf.PerlinNoise(time * (float)speed, 0f);
    }
}
