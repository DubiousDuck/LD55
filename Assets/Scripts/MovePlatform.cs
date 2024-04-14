using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

//Moving Platform 
public class MovePlatform : MonoBehaviour
{
    [SerializeField] private float speed = 3; 
    private Vector3 velocity;
    // Start is called before the first frame update
    void Start()
    {
        velocity = new Vector3(x:speed, y:0, z:0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += velocity * Time.deltaTime;
        if (transform.position.x > 10) 
        {
            velocity = new Vector3(x:-speed, y:0, z:0) ;
        } else if (transform.position.x < -10) 
        {
            velocity = new Vector3(x: speed, y: 0, z: 0); 
        }
    }
}

