using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class Rotation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }
 
    // Update is called once per frame
    void Update()
    {
        float speed = 2.0f;
        GameObject target = this.gameObject;
        Transform myTransform = target.transform;
        Vector3 pos = myTransform.position;
 
        pos.x = Mathf.Sin(Time.time * speed) * 150f;
        pos.y = Mathf.Cos(Time.time * speed) * 100f;
        myTransform.position = pos;
    }
}