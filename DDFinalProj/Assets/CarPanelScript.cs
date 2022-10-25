using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//References:
//Jimmy Vegas: https://www.youtube.com/watch?v=78OGu2o5-SA&list=PLZ1b66Z1KFKgkE9ji0tF2iDO0LGxmlwIm&index=15
public class CarPanelScript : MonoBehaviour {

    public static int CarType;
    public GameObject TrackWindow;

    public void RedCar()
    {
        CarType = 0;
        PlayerPrefs.SetInt("selectedCar", CarType);
        TrackWindow.SetActive(true);
    }

    public void BlueCar()
    {
        CarType = 1;
        PlayerPrefs.SetInt("selectedCar", CarType);
        TrackWindow.SetActive(true); 
    }

    public void Bus()
    {
        CarType = 2;
        PlayerPrefs.SetInt("selectedCar", CarType);
        TrackWindow.SetActive(true); 
    }

    public void Borsche()
    {
        CarType = 3;
        PlayerPrefs.SetInt("selectedCar", CarType);
        TrackWindow.SetActive(true); 
    }
    
}
