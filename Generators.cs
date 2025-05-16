using System.Collections;
using UnityEngine;

public class Generators : MonoBehaviour
{

    public GameObject generatedObject;
    public float spawnChance;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SpawnPlatform();
    }

    private void SpawnPlatform()
    {
        float random = Random.Range(0.0f, spawnChance);
        if(random < 1.0f)
        {
            var lastGeneratedObject = GameObject.Instantiate(generatedObject, transform.position, transform.rotation);
            Destroy(lastGeneratedObject, 7.0f);
        }
    }
}
