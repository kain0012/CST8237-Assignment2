using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    
    public int MoveSpeed = 100;
    public int RotationSpeed = 200;
    public GameObject ShipGraphics;
    public float SpeedLimit = 100f;
    private float shipStartTime;
    private void FixedUpdate()
    {
        //Rotates the ship according to the axis' horizontal vertical position
        transform.Rotate(0f, (Input.GetAxis("Horizontal") * Time.deltaTime) * RotationSpeed, 0f);
        float num = 0f;

        //if the ship is moving vertically, activate the engine emitter
        if (Input.GetAxis("Vertical") > 0f)
        {
            num = Input.GetAxis("Vertical") * MoveSpeed;
        }
        

        //Verify it the velocity is lower than the speed limit
        if (GetComponent<Rigidbody>().velocity.magnitude < SpeedLimit)
        {
            //Adds a force to acelerate the ship on Z axis
            GetComponent<Rigidbody>().AddRelativeForce(0f, 0f, num);
        }
        else
        {
            //reduce the velocity gradually.
            GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity * 0.99f;
        }
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        //Check if the player collides an asteroid(rock)
        if (collision.gameObject.GetComponent<Rock>() != null)
        {
            //Destroy this instance
            Destroy(gameObject);
        }
    }


    private void Start()
    { 
        //activate the trigger notifications
        GetComponent<Rigidbody>().GetComponent<Collider>().isTrigger = true;
        
        //save the stark time
        shipStartTime = Time.time;
    }
    
    private void Update()
    {
        //Check if the player has an active trigger
        if (GetComponent<Rigidbody>().GetComponent<Collider>().isTrigger)
        {
            //calculate elapsed time
            float num = Time.time - shipStartTime;
            if (num < 1f)
            {
                //if the elapsed time is lower than 1
                foreach (Renderer renderer in ShipGraphics.GetComponentsInChildren<Renderer>())
                {
                    //Animates the color to fade in the mesh
                    float num3 = Mathf.Lerp(0f, 1f, num);
                    Color color = new Color(renderer.sharedMaterial.color.r, renderer.sharedMaterial.color.g, renderer.sharedMaterial.color.b, num3);
                    renderer.sharedMaterial.color = color;
                }
            }
            else
            {
                //disables the trigeer notifications
                GetComponent<Rigidbody>().GetComponent<Collider>().isTrigger = false;
            }
        }
    }
}

