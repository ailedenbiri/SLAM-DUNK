using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("--- BASIC LEVEL OBJECT")]
    [SerializeField] private GameObject Platform;
    [SerializeField] private GameObject Hoop;
    [SerializeField] private GameObject HoopGrow;
    [SerializeField] private GameObject[] ParticularFormationPoints;
    [SerializeField] private AudioSource[] Sounds;
    [SerializeField] private ParticleSystem[] Effects;
    SceneManager scene;




    [Header("---UI OBJECT")]
    [SerializeField] private Image [] MissionImage;
    [SerializeField] private Sprite MissionCompletedSprite;
    [SerializeField] private int MissionTarget;
    [SerializeField] private GameObject[] Panels;
    [SerializeField] private TextMeshProUGUI LevelName;
    int BasketPoint;
    float FingerPosX;

    void Start()
    {
        LevelName.text = "LEVEL : "+ SceneManager.GetActiveScene().name;


        for (int i = 0; i < MissionTarget; i++)
        {
            MissionImage[i].gameObject.SetActive(true);
        }
        //Invoke("FeatureFormation", 3f);
    }

   void FeatureFormation()
    {
        int RandomValue = Random.Range(0, ParticularFormationPoints.Length - 1);
        HoopGrow.transform.position = ParticularFormationPoints[RandomValue].transform.position;
        HoopGrow.SetActive(true);
    }

    void Update()
    {
        if (Time.timeScale != 0)
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                Vector3 TouchPosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y,10));

                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        FingerPosX = TouchPosition.x - Platform.transform.position.x;
                        break;
                    case TouchPhase.Moved:
                        if (TouchPosition.x - FingerPosX > -1.15 && TouchPosition.x - FingerPosX < 1.15)
                        {
                            Platform.transform.position = Vector3.Lerp(Platform.transform.position, new Vector3(TouchPosition.x - FingerPosX, Platform.transform.position.y, Platform.transform.position.z), 5f);
                        }
                        break;
                }
            }
        }         
    }

    public void Basket(Vector3 Pos)
    {
        BasketPoint++;
        MissionImage[BasketPoint - 1].sprite = MissionCompletedSprite;
        Effects[0].transform.position = Pos;
        Effects[0].gameObject.SetActive(true);
        Sounds[4].Play();
        
        if(BasketPoint == MissionTarget)
        {
            Win();
        }

        if(BasketPoint == 1) 
        {
            FeatureFormation();
        }
    }

    public void Lose()
    {
        Sounds[1].Play();
        Panels[2].SetActive(true);
        Time.timeScale = 1;
    }

    void Win()
    {
        Sounds[2].Play();
        Panels[1].SetActive(true);
        PlayerPrefs.SetInt("Level",PlayerPrefs.GetInt("Level") + 1);
        Time.timeScale = 0;
    }

    public void HoopExpansion(Vector3 Pos)
    {
        Effects[1].transform.position = Pos;
        Effects[1].gameObject.SetActive(true);
        Sounds[0].Play();
        Hoop.transform.localScale = new Vector3(55f, 55f, 55f);
    }

    public void Actionsofbuttons(string Value)
    {
        switch (Value) 
        {

            case "Pause":
                Time.timeScale = 0;
                 Panels[0].SetActive(true);
                break;

            case "resume":
                Time.timeScale = 1;
                 Panels[0].SetActive(false);
                break;

            case "again":
               SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
               Time.timeScale = 1;
                break;


            case "next":
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
                Time.timeScale = 1;
                break;

            case "quit":
                Application.Quit();
                break;

        }
    }
}
