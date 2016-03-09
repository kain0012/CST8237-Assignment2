using UnityEngine;


[RequireComponent(typeof(AudioSource))]
public class PlayerWeapon : MonoBehaviour
{
    
    public Rigidbody LaserPrefab;
    public AudioClip ShotSound;
    public float Speed = 150f;

    private void Update()
    {
        //Shoots when the user presses the fire button (left mouse)
        if (Input.GetButtonUp("Fire1"))
        {
            //instantiates the laser prefab
            Rigidbody rigidbody = Instantiate(LaserPrefab, transform.position, transform.rotation) as Rigidbody;
            
            //ignores the collision with the player
            Physics.IgnoreCollision(transform.root.GetComponent<Collider>(), rigidbody.GetComponent<Collider>(), true);
            
            //asigns velocity to this instance
            Vector3 velocity = new Vector3(0f, 0f, Speed);
            rigidbody.velocity = transform.TransformDirection(velocity);
            
            //plays the auudio
            GetComponent<AudioSource>().PlayOneShot(ShotSound);
        }
    }
}

