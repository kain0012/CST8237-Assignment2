
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class Rock : MonoBehaviour
{
   
    public AudioClip ExplosionSound;
    

    private GameSceneController currentGameSceneController;
    //public GameObject mainCamera;
    public int MaxScale = 10;
    public int MaxSpeed = 10;
    public int MinScale = 5;
    public int MinSpeed = 5;
    private float actualScale;
    private float startTimeOfRock;
    public int Points = 100;
    private float velocityXOfRock;
    private float velocityZOfRock;
    private void FixedUpdate()
    {
        //check it the velocity is higher than the maximum
        if (GetComponent<Rigidbody>().velocity.magnitude > MaxSpeed)
        {
            //reduce gradually the velocity
            GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity * 0.99f;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //check if this instance collided the player or the player's laser
        if ((collision.gameObject.GetComponent<Player>() != null) || 
            (collision.gameObject.GetComponent<PlayerLaser>() != null))
        {
            //removes the rock
            currentGameSceneController.RemoveRock(gameObject);
            //plays the explosion sound
            currentGameSceneController.GetComponent<AudioSource>().PlayOneShot(ExplosionSound);
            //destroy this instance
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        //setup the current game scene controller
        currentGameSceneController = GameObject.Find("MainCamera").GetComponent<GameSceneController>();
        
		//get random velocity in X and X axis
        velocityXOfRock = Random.Range(MinSpeed, MaxSpeed);
        velocityZOfRock = Random.Range(MinSpeed, MaxSpeed);
        
		//get random direction for velocity in X axis
        if (Random.Range(1, 10) > 5)
        {
            velocityXOfRock *= -1f;
        }
        
		//get random direction for velocity in X axis
        if (Random.Range(1, 10) > 5)
        {
            velocityZOfRock *= -1f;
        }
        
		//gets a random scale between the minimum and maximum
        actualScale = Random.Range(MinScale, MaxScale)*10;
        
        //sets the local scale to 0
        Vector3 vector = new Vector3(0f, 0f, 0f);
        transform.localScale = vector;
        //sets the instance's mass according to the random scale
        GetComponent<Rigidbody>().mass = actualScale;
        
        startTimeOfRock = Time.time;
        
        //sets the instance's collider to raise trigger events.
        GetComponent<Rigidbody>().GetComponent<Collider>().isTrigger = true;
        
        //adds the random velocity to this instance
        GetComponent<Rigidbody>().AddRelativeForce(velocityXOfRock, 0f, velocityZOfRock);
    }

    private void Update()
    {
        //checks if this instance is setup as trigger
        if (GetComponent<Rigidbody>().GetComponent<Collider>().isTrigger)
        {
            //calculates the elapsed time from the start
            float num = Time.time - startTimeOfRock;
            
            //Start increasing de local size to achieve an effect
            //of growing up
            if (num < 1f)
            {
                //calculates the new scale
                float num2 = Mathf.Lerp(0f, actualScale, num);
                Vector3 vector = new Vector3(num2, num2, num2);
                //increase the local scale
                transform.localScale = vector;
            }
            else
            {
                //disable trigger notifications
                GetComponent<Rigidbody>().GetComponent<Collider>().isTrigger = false;
            }
        }
    }
}

