using UnityEngine;
using System.Collections;

public class DaeNightController : MonoBehaviour {

    public bool day;
    public GameObject directionalLight;
    public GameObject pointLight;
    public float time;
    public float timeSpeed;

    public float dayTime;
    public float nightTime;
    public float morningTime;
    public float noonTime;

    public float intensityDirLightMax;
    public float intensityDirLightEdge;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime * timeSpeed;


        //Every period of day
        if (time >= morningTime && time <= noonTime)
        {
           
            day = true;
            directionalLight.GetComponent<Light>().intensity = Mathf.Lerp(directionalLight.GetComponent<Light>().intensity, intensityDirLightMax, Time.deltaTime * 0.1f);
            pointLight.GetComponent<Light>().intensity = Mathf.Lerp(pointLight.GetComponent<Light>().intensity, 0f, Time.deltaTime * 0.1f);
        }
        else if(time < morningTime || time > nightTime)
        {
            day = false;
        }
        else if(time >= noonTime && time <= nightTime)
        {
            
            day = true;
            directionalLight.GetComponent<Light>().intensity = Mathf.Lerp(directionalLight.GetComponent<Light>().intensity, 0f, Time.deltaTime * 0.1f);
            pointLight.GetComponent<Light>().intensity = Mathf.Lerp(pointLight.GetComponent<Light>().intensity, intensityDirLightEdge, Time.deltaTime * 0.1f);
        }

        //Midnight
        if ( time >= dayTime)
        {
            time = 0;
        }

        //Check for the day
        if (day)
        {
            directionalLight.SetActive(true);
        }
        else
        {
            directionalLight.GetComponent<Light>().intensity = 0f;
            directionalLight.SetActive(false);
            pointLight.SetActive(true);
        }
    }
}
