using UnityEngine;

public class ScreenWrap : MonoBehaviour
{
    public int Xbounds = 175;
    public int Zbounds = 125;
    private void FixedUpdate()
    {
        //opposite position
        Vector3 newPosition;

        //checks if the X axis is exceeded in the left
        if (transform.position.x > Xbounds)
        {
            //reposition the object in the opposite direction in the same axis
            newPosition = new Vector3(-Xbounds, 0f, transform.position.z);
            transform.position = newPosition;
        }

        //checks if the X axis is exceeded in the right
        if (transform.position.x < -Xbounds)
        {
            //reposition the object in the opposite direction in the same axis
            newPosition = new Vector3(Xbounds, 0f, transform.position.z);
            transform.position = newPosition;
        }

        //checks if the Z axis is exceeded in the left
        if (transform.position.z > Zbounds)
        {
            //reposition the object in the opposite direction in the same axis
            newPosition = new Vector3(transform.position.x, 0f, (float)-this.Zbounds);
            transform.position = newPosition;
        }

        //checks if the Z axis is exceeded in the right
        if (transform.position.z < -this.Zbounds)
        {
            //reposition the object in the opposite direction in the same axis
            newPosition = new Vector3(transform.position.x, 0f, (float)this.Zbounds);
            transform.position = newPosition;
        }
    }

    /// <summary>
    /// Starts this instance.
    /// </summary>
    /// <remarks></remarks>
    private void Start()
    {
    }

    /// <summary>
    /// Updates this instance.
    /// </summary>
    /// <remarks></remarks>
    private void Update()
    {
    }
}

