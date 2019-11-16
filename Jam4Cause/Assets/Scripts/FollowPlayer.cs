using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform lookAtObject;

    float offsetX;
    // Start is called before the first frame update
    void Start()
    {
        offsetX = transform.position.x - lookAtObject.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        float newX = lookAtObject.transform.position.x;
        transform.position = new Vector3(offsetX + newX, transform.position.y, transform.position.z);

    }
}
