using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform lookAtObject;

    float offsetX;
    float offsetY;
    // Start is called before the first frame update
    void Start()
    {
        offsetX = transform.position.x - lookAtObject.position.x;
        offsetY = transform.position.y - lookAtObject.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        float newX = lookAtObject.transform.position.x;
        float newY = lookAtObject.transform.position.y;
        transform.position = new Vector3(offsetX + newX, offsetY + newY, transform.position.z);

    }
}
