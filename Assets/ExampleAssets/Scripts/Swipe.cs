using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Swipe : MonoBehaviour
{
    public int CurrentPageID;
    private Vector2 startTouchPosition;
    private Vector2 endTouchPosition;

    private void Update()
    {
        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            startTouchPosition = Input.GetTouch(0).position;
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            endTouchPosition = Input.GetTouch(0).position;

            if(endTouchPosition.x < startTouchPosition.x)
            {
                NextPage();
            }

            if(endTouchPosition.x > startTouchPosition.x)
            {
                PreviousPage();
            }
        }
    }

    private void PreviousPage()
    {
        SceneManager.LoadScene(CurrentPageID--);
    }

    private void NextPage()
    {
        SceneManager.LoadScene(CurrentPageID ++);
    }
}
