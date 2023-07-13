using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesMove : MonoBehaviour
{
    void FixedUpdate()
    {
        transform.position = new Vector2(Input.mousePosition.x - 352, Input.mousePosition.y - 152);
    }
}
