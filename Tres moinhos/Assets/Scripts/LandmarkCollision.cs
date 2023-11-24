using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandmarkCollision : MonoBehaviour
{
    bool inLandmark = false;
    float planeX, planeZ;

    private void FixedUpdate()
    {
        if (inLandmark)
        {
            InLandmark(planeX, planeZ);
        }
    }

    public void InLandmark(float width, float height)
    {
        Debug.Log("In Landmark");
        var x_rand = UnityEngine.Random.Range(-planeX, planeX);
        var z_rand = UnityEngine.Random.Range(-planeZ, planeZ);

        transform.position = new Vector3(x_rand, 0, z_rand);
        
        inLandmark = false;
    }

    public void SavedLandmarkPos(float width, float height)
    {
        planeX = width;
        planeZ = height;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Landmark"))
        {
            inLandmark = true;
        }
    }
}
