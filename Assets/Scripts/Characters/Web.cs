using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Web : MonoBehaviour
{
    public float slowModifier = 0.2f;
    public float lifeTime = 2f;

    public void init()
    {
        Destroy(this.gameObject, lifeTime);
        this.tag = this.transform.parent.gameObject.tag;
        this.transform.parent = null;
        this.gameObject.layer = 2;
    }
}
