using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformControl : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("MovingPlatform"))
        {
            if (Vector3.Dot(collision.GetContact(0).normal, Vector3.up) > 0.5f)
            {
                transform.parent = collision.transform;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("MovingPlatform"))
        {
            transform.SetParent(null);
        }
    }
}
