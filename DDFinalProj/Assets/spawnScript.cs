using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnScript : MonoBehaviour
{
    [SerializeField] public GameObject[] carList;
    public GameObject newCar;  
    public void Awake(){
        int car = PlayerPrefs.GetInt("selectedCar");
        // newCar = Instantiate(carList[car], this.transform.position, Quaternion.identity);
        if(car == 0)
        {
            carList[0].SetActive(true);
        }
        else if(car==1)
        {
            carList[1].SetActive(true);
        }
        else if(car==2)
        {
            carList[2].SetActive(true);
        }
        else if(car==3)
        {
            carList[3].SetActive(true);
        }
        var script = this.GetComponentInChildren<CameraFollowFOVUpdate>();
        var script2 = this.GetComponentInChildren<Speedometer>();
        script.target = carList[car].GetComponent<Transform>();
        script.car = carList[car].GetComponent<Rigidbody>();
        script2.target = carList[car].GetComponent<Rigidbody>();
        script2.enabled = true;
        script.enabled = true;
    }
    
}
