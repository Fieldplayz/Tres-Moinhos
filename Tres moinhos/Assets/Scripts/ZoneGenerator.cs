using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ZoneGenerator : MonoBehaviour
{
    [SerializeField] Transform plane;
    [SerializeField] TMP_Text zoneLetter;
    [SerializeField] TMP_InputField treeInput;
    [SerializeField] TMP_InputField plantInput;

    [SerializeField] List<Zone> zones = new List<Zone>();

    //plane properties
    float xDim;
    float zDim;

    //current zone
    int zoneIndex = 0;
    int treeCount;
    int plantCount;

    private void Start()
    {
        // Get the length and width of the plane
        xDim = plane.GetComponent<MeshRenderer>().bounds.size.x;
        zDim = plane.GetComponent<MeshRenderer>().bounds.size.z;
        xDim /= 2f;
        zDim /= 2f;

        treeCount = zones[zoneIndex].treeCount;
        plantCount = zones[zoneIndex].plantCount;

        ClearTerrain();
        SpawnTrees(treeCount);
        SpawnPlants(plantCount);
        SetInput();
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (zones[zoneIndex].landMark != null)
            {
                zones[zoneIndex].landMark.SetActive(false);
            }

            SetInput();

            zoneIndex++;
            if(zoneIndex > 25)
            {
                zoneIndex = 0;
            }

            zoneLetter.text = zones[zoneIndex].zone.ToString();

            ClearTerrain();
            SpawnTrees(treeCount);
            SpawnPlants(plantCount);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (zones[zoneIndex].landMark != null)
            {
                zones[zoneIndex].landMark.SetActive(false);
            }

            SetInput();

            zoneIndex--;
            if (zoneIndex < 0)
            {
                zoneIndex = 25;
            }

            zoneLetter.text = zones[zoneIndex].zone.ToString();

            ClearTerrain();
            //SpawnTrees(treeCount);
            StartCoroutine(GetComponent<ApiCall>().GetTreesPerZone(zones[zoneIndex].zone.ToString()));
            SpawnPlants(plantCount);
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            int treeCount = int.Parse(treeInput.text.ToString());
            int plantCount = int.Parse(treeInput.text.ToString());

            Debug.Log(treeCount);

            ClearTerrain();
            SpawnTrees(treeCount);
            SpawnPlants(plantCount);

        }

    }

    public void ClearTerrain()
    {
        //clear the terrain
        foreach (Transform child in plane.transform)
        {
            Destroy(child.gameObject);
        }

        if (zones[zoneIndex].landMark != null)
        {
            zones[zoneIndex].landMark.SetActive(true);
        }
    }

    public void SpawnTrees(int treeAmmount)
    {

        for (int i = 0; i < treeAmmount; i++)
        {
            // Spawn the object as a child of the plane. This will solve any rotation issues
            GameObject obj = Instantiate(zones[zoneIndex].trees[UnityEngine.Random.Range(0, zones[zoneIndex].trees.Length)], Vector3.zero,
             Quaternion.identity, plane);

            /* Move the object to where you want withing in the dimensions of the plane */
            // random the x and z position between bounds
            var x_rand = UnityEngine.Random.Range(-xDim, xDim);
            var z_rand = UnityEngine.Random.Range(-zDim, zDim);

            // Random the y position from the smallest bewteen x and z
            //z_rand = x_rand > z_rand ? UnityEngine.Random.Range(0, z_rand) : UnityEngine.Random.Range(0, x_rand);

            // Now move the object
            // Since the object is a child of the plane it will automatically handle rotational offset
            obj.transform.position = new Vector3(x_rand, 0, z_rand);

            obj.gameObject.GetComponent<LandmarkCollision>().SavedLandmarkPos(xDim, zDim);
        }

    }

    public void SpawnPlants(int plantAmmount)
    {
        for (int i = 0; i < plantAmmount; i++)
        {
            // Spawn the object as a child of the plane. This will solve any rotation issues
            GameObject obj = Instantiate(zones[zoneIndex].plants[UnityEngine.Random.Range(0, zones[zoneIndex].plants.Length)], Vector3.zero,
             Quaternion.identity, plane);

            /* Move the object to where you want withing in the dimensions of the plane */
            // random the x and z position between bounds
            var x_rand = UnityEngine.Random.Range(-xDim, xDim);
            var z_rand = UnityEngine.Random.Range(-zDim, zDim);

            // Random the y position from the smallest bewteen x and z
            z_rand = x_rand > z_rand ? UnityEngine.Random.Range(0, z_rand) : UnityEngine.Random.Range(0, x_rand);

            // Now move the object
            // Since the object is a child of the plane it will automatically handle rotational offset
            obj.transform.position = new Vector3(x_rand, 0, z_rand);

            obj.gameObject.GetComponent<LandmarkCollision>().SavedLandmarkPos(xDim, zDim);
        }
    }

    public void SetInput()
    {
        treeInput.text = zones[zoneIndex].treeCount.ToString();
        plantInput.text = zones[zoneIndex].plantCount.ToString();
    }

}

[System.Serializable]
public class Zone
{
    public char zone;
    public int treeCount;
    public int plantCount;

    public GameObject[] trees;
    public GameObject[] plants;
    public GameObject landMark;
}
