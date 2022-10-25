using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

// Referenced
// Youtube channel name: Saad Zaheer
// Link to video: https://www.youtube.com/watch?v=w9lSZPmget4
public class CheckpointsAndLaps : MonoBehaviour
{

   [Header("Checkpoints")]
    public GameObject start;
    public GameObject end;
    public GameObject[] checkpoints;
    public GameObject completeLevelUI;
    public GameObject Bronze;
    public GameObject Silver;
    public GameObject Gold;


    public Text TotalLapTimeText;
    public Text currentLapTimeText;
    public Text bestLapTimeText;
    public Text TotalLapTimeTextF;
    public Text bestLapTimeTextF;
    public Text allTimeBestText;
    public Text allTimeBestTextF;

    public TMP_Text TotalLapTimeTMP;




    [Header("Settings")]
    public float laps = 1;

    [Header("Information")]
    private float currentCheckpoint;
    private float currentLap;
    private bool started;
    private bool finished;

    private float currentLapTime;
    private float bestLapTime;
    private float bestLap;
    private float formattedBestTime;
    private float totalLapTime;
    public string filepath = "laptime.txt";
    private string allTimeBest;
    public float allTimeBestFloat;
    public float silverTime;
    public float goldTime;


    private void Start()
    {

        if (!File.Exists(filepath))
        {
            File.Create(filepath).Close();
            File.AppendAllText(filepath,("1000000000000000000000"));


        }
        currentCheckpoint =0;
        currentLap = 1;

        started = false;
        finished = false;

        currentLapTime =0;
        bestLapTime = 0;
        totalLapTime = 0;


        if (finished)
        {
            WriteFile();
        }



    }

    private void WriteFile()
    {
        File.AppendAllText(filepath,(totalLapTime).ToString());


    }
    private void Update()
    {
        if (finished)
        {

            completeLevelUI.SetActive(true);
            if (totalLapTime < goldTime)
            {
                Gold.SetActive(true);

            }
            else if (totalLapTime< silverTime)
            {
                Silver.SetActive(true);
            }
            else
            {
                Bronze.SetActive(true);
            }


            TotalLapTimeText.text=  ($"Total Time Elapsed: {Mathf.FloorToInt(totalLapTime/ 60)}:{totalLapTime %60:00.000}").ToString();
            TotalLapTimeTextF.text=  ($"Total Time Elapsed: {Mathf.FloorToInt(totalLapTime/ 60)}:{totalLapTime %60:00.000}").ToString();
            allTimeBestText.text=  ($"Total Time Elapsed: {Mathf.FloorToInt(allTimeBestFloat/ 60)}:{allTimeBestFloat %60:00.000}").ToString();
            allTimeBestTextF.text=  ($"Total Time Elapsed: {Mathf.FloorToInt(allTimeBestFloat/ 60)}:{allTimeBestFloat %60:00.000}").ToString();

            AudioSource[] audios =FindObjectsOfType<AudioSource>();

            foreach (AudioSource a in audios)
            {if( a.ToString() != "songObject (UnityEngine.AudioSource)")
            a.Pause();

            }



        }
        if (started && !finished)
        {
            currentLapTime += Time.deltaTime;
            totalLapTime += Time.deltaTime;

            if (bestLap == 0)
            {
                bestLap = 1;
            }
        
        }

        if (started)
        {
            currentLapTimeText.text= ($"Current:           {Mathf.FloorToInt(currentLapTime / 60)}:{currentLapTime %60:00.000} - (Lap{currentLap})").ToString();

            TotalLapTimeText.text=  ($"Total Lap Time: {Mathf.FloorToInt(totalLapTime/ 60)}:{totalLapTime %60:00.000}").ToString();

        
            TotalLapTimeTextF.text=  ($"Total Time Elapsed: {Mathf.FloorToInt(totalLapTime/ 60)}:{totalLapTime %60:00.000}").ToString();
        

            bestLapTimeTextF.text=    ($"All Time Best: {Mathf.FloorToInt(allTimeBestFloat/ 60)}:{allTimeBestFloat %60:00.000}").ToString();


            bestLapTimeText.text=  ($"Best Lap Time:   {Mathf.FloorToInt(bestLapTime / 60)}:{bestLapTime %60:00.000} - (Lap{bestLap})").ToString();



            if (bestLap == currentLap)
            {
                bestLapTime = currentLapTime;


            }

        
        
        }
        

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Checkpoint"))
        {
            GameObject thisCheckpoint = other.gameObject;

            // Started the race
            if (thisCheckpoint == start && !started)
            {
                started = true;


            }
            // Ended the lap or race
            else if(thisCheckpoint == end && started)
            {
                // If all laps are finished, end the race
                if (currentLap == laps)
                {
                    if (currentCheckpoint == checkpoints.Length)
                    {
                        if(currentLapTime<bestLapTime)
                        {
                            bestLap = currentLap;
                        }

                        finished = true;

                        using(StreamReader reader = new StreamReader(filepath))
                        {
                            allTimeBest= reader.ReadLine() ?? "";
                        }

                        allTimeBestFloat = float.Parse(allTimeBest);
                    



                        if (allTimeBestFloat > ((totalLapTime)))
                        {
                            File.Create(filepath).Close();
                            WriteFile();
                            allTimeBestFloat = (totalLapTime);
                        }
                        print($"{Mathf.FloorToInt(allTimeBestFloat / 60)}:{allTimeBestFloat %60:00.000}");
                        print("Finished");
                    }
                    else
                    {
                        print("Did not  go through all checkpoints");
                    }
                }
                // If all laps are not finished, start a new lap
                else if (currentLap < laps)
                {
                    if(currentCheckpoint == checkpoints.Length)
                    {
                        if(currentLapTime<bestLapTime)
                        {
                            bestLap= currentLap;
                            bestLapTime = currentLapTime;
                        }

                        currentLap++;
                        currentCheckpoint = 0;
                        currentLapTime = 0;
                        print($"Started lap {currentLap} - {Mathf.FloorToInt(currentLapTime / 60)}:{currentLapTime %60:00.000}");
                    }
                }
                else
                {
                    print("Did not go through all the checkpoints");
                }
            }

            //Loop throught the checkpoints and compare and check which one the player passed through
            for (int i = 0; i <checkpoints.Length; i++)
            {
                if(finished)
                    return;

                // If the checkpoint is correct 
                if (thisCheckpoint == checkpoints[i] && i == currentCheckpoint)
                {
                    print($"Correct checkpoint - {Mathf.FloorToInt(currentLapTime / 60)}:{currentLapTime %60:00.000}");
                    currentCheckpoint++;

                }
                // If the checkpoint is incorrect
                else if (thisCheckpoint == checkpoints[i] && i != currentCheckpoint)
                {
                    print("Incorrect checkpoint");
                }
            }
            
        }
    }

    
}
