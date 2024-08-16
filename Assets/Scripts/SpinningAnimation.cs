using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningAnimation : MonoBehaviour
{
    public float speed = 100f;
    private void FixedUpdate()
    {
        transform.rotation = Quaternion.AngleAxis(Time.fixedTime * speed, transform.up);
    }
}
