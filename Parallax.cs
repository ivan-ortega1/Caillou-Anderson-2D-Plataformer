using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{

    public GameObject[] backgrounds;
    public float[] backgroundsSpeeds;
    public float[] sizes;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ParallaxEffect();
    }

    private void ParallaxEffect()
    {
        for(int i = 0; i < backgrounds.Length; i++)
        {
            if(Mathf.Abs(backgrounds[i].transform.localPosition.x) > sizes[i])
            {
                //Return backgrounds to original position
                backgrounds[i].transform.localPosition = new Vector3(0.0f, 
                    backgrounds[i].transform.localPosition.y, backgrounds[i].transform.localPosition.z);
            }
            else
            {
                //Backgrounds movement
                float offset = Time.deltaTime * backgroundsSpeeds[i];
                backgrounds[i].transform.localPosition += new Vector3(offset, 0.0f);
            }
        }
    }
}
