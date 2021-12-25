using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using Random = System.Random;

public class CubeGenerator : MonoBehaviour
{
    private Random randomInt = new Random();
    public GameObject Cube; // Cube prefab

    public static List<Vector3> cubePositions = new List<Vector3>();

    void Start()
    {
        cubePositions.Clear();
    }

    void Update()
    {
        GenerateCubes();
    }

    void GenerateCubes()
    {
        int leftorright = randomInt.Next(0, 2);
        
        if(leftorright == 1)
        {
            leftorright = -2;
        }
        else
        {
            leftorright = 2;
        }

        if (cubePositions.Count > 2 && cubePositions[cubePositions.Count - 2].x == leftorright && cubePositions[cubePositions.Count - 1].x == leftorright)
        {
            if (leftorright == -2) leftorright = 2;
            else leftorright = -2;
        }

        Vector3 position;
        if (!cubePositions.Any())
        {
            position = new Vector3(leftorright, 2, transform.position.z + 20);
            cubePositions.Add(position);
            Instantiate(Cube, position, Cube.transform.rotation);
        }
        else if((transform.position.z + 100 > cubePositions.Last().z))
        {
            if(cubePositions.Count % 5 == 0)
            {
                //CoinGenerator.GenerateCoin(cubePositions.Last()); // Coin Generator code
            }
            position = new Vector3(leftorright, 2, cubePositions.Last().z + 30);
            cubePositions.Add(position);
            Instantiate(Cube, position, Cube.transform.rotation);
        }
    }
}
