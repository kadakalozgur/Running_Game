using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using TMPro;

public class LevelManager : MonoBehaviour
{

    public GameObject lightFloor1;
    public GameObject darkFloor1;
    public GameObject lightFloor2;
    public GameObject darkFloor2;
    public GameObject lightFloor3;
    public GameObject darkFloor3;
    public GameObject emptyFloor;
    public GameObject floorParent;

    public AudioSource gameMusic;

    public Transform player;

    public Player _player;

    private List<GameObject> floors = new List<GameObject>();

    private Vector3 playerStartPos;


    void Start()
    {

        createFloor();

    }


    void Update()
    {

        GameObject firstFloor = floors[0];

        if (player.position.z - firstFloor.transform.position.z > 20f)
        {
            floors.RemoveAt(0);

            GameObject lastFloor = floors[floors.Count - 1];

            firstFloor.transform.position = lastFloor.transform.position + new Vector3(0, 0, 10f);

            floors.Add(firstFloor);

        }
    }

    private void createFloor()
    {

        GameObject[] lightFloors = { lightFloor1, lightFloor2, lightFloor3 };
        GameObject[] darkFloors = { darkFloor1, darkFloor2, darkFloor3 };

        floorParent = new GameObject("Map");

        float startZ = player.position.z;

        for (int i = 0; i < 20; i++)
        {

            GameObject choseFloor;

            if (i < 2)
            {

                choseFloor = emptyFloor;

            }

            else
            {

                if (i % 2 == 0)
                    choseFloor = lightFloors[Random.Range(0, lightFloors.Length)];

                else
                    choseFloor = darkFloors[Random.Range(0, darkFloors.Length)];
            }

            GameObject newFloor = Instantiate(choseFloor, floorParent.transform);

            newFloor.transform.position = new Vector3(0, 0, startZ + (i * 10));

            floors.Add(newFloor);

        }
    }
}