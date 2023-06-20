using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HoopGrow : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI Duration;
    [SerializeField] private int StartDuration;
    [SerializeField] private GameManager _GameManager;


    IEnumerator Start()
    {
        Duration.text = StartDuration.ToString();

        while (true)
        {
            yield return new WaitForSeconds(1f);
            StartDuration--;
            Duration.text = StartDuration.ToString();

            if (StartDuration == 0)
            {
                gameObject.SetActive(false);
                break;
            }
        }
    }


  
    private void OnTriggerEnter(Collider other)
    {
        gameObject.SetActive(false);
        _GameManager.HoopExpansion(transform.position);

    }

}
