
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class PlayerLaser : MonoBehaviour
{
    public int Xbounds = 200;
    public int Zbounds = 150;

    private void FixedUpdate()
    {
        //if this instance is beyond the bounds, destroy it
        if (transform.position.x > Xbounds)
        {
            Destroy(gameObject);
        }
        if (transform.position.x < -Xbounds)
        {
            Destroy(gameObject);
        }
        if (transform.position.z > Zbounds)
        {
            Destroy(gameObject);
        }
        if (transform.position.z < -Zbounds)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Destroys this instance
        Destroy(gameObject);
    }

    
}

