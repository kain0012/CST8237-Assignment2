/* 
 * Asteroids Demo for BlueLizard Games
 * 8 hours demo
 * By Alberto Guerra
*/

using UnityEngine;

/// <summary>
/// Helper class to rotate the object
/// </summary>
/// <remarks></remarks>
public class RockSpinner : MonoBehaviour
{
    /// <summary>
    /// Maximum rotation
    /// </summary>
    public float MaxRotation = 2f;
    /// <summary>
    /// Minimum rotation
    /// </summary>
    public float MinRotation = 1f;
    /// <summary>
    /// Current rotation X axis
    /// </summary>
    private float _rotationX = 1f;
    /// <summary>
    /// Current rotation Y axis
    /// </summary>
    private float _rotationY = 1f;
    /// <summary>
    /// Current rotation Z axis
    /// </summary>
    private float _rotationZ = 1f;

    /// <summary>
    /// Fixed update.
    /// </summary>
    /// <remarks></remarks>
    private void FixedUpdate()
    {
        //Rotates this instance
        transform.Rotate(_rotationX, _rotationY, _rotationZ);
    }

    /// <summary>
    /// Starts this instance.
    /// </summary>
    /// <remarks></remarks>
    private void Start()
    {
        //Calculates random rotations in all axis between the allowed values
        _rotationX = Random.Range(MinRotation, MaxRotation);
        _rotationY = Random.Range(MinRotation, MaxRotation);
        _rotationZ = Random.Range(MinRotation, MaxRotation);
    }

    /// <summary>
    /// Updates this instance.
    /// </summary>
    /// <remarks></remarks>
    private void Update()
    {
    }
}

