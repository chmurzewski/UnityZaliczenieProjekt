using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hardwareSpawner : MonoBehaviour
{
    private float speed = 1f;

    public Rigidbody2D hardware;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnHardware", 1.0f, 2f);
    }

    void SpawnHardware()
    {
        int numHard = 8;
        int maxHard = 0;

        for (int i = 0; i < numHard; i++)
        {
            int chanse;
            chanse = Random.Range(0, 3);
            if (chanse == 0 && maxHard < 1)
            {
                Rigidbody2D instance = Instantiate(hardware, transform);
                instance.transform.position = instance.transform.position + Vector3.right * i;
                instance.velocity = Vector2.down * speed;
                maxHard += 1;
            }
        }

        speed *= 1.05f;
    }
}
