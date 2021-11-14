using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TitleMenuController : MonoBehaviour
{
    public GameObject title;
    public GameObject HowTo;
    public void Open(GameObject menu)
    {
        menu.SetActive(true);
        menu.GetComponent<RectTransform>().DOAnchorPosY(0, 0.5f).SetEase(Ease.OutBack);
        Button[] buttons = title.GetComponentsInChildren<Button>();
        foreach (Button button in buttons)
        {
            button.interactable = false;
        }
    }

    public void Close(GameObject menu)
    {
        menu.GetComponent<RectTransform>().DOAnchorPosY(-590, 0.5f).SetEase(Ease.InBack).OnComplete(()=>menu.SetActive(false));
        Button[] buttons = title.GetComponentsInChildren<Button>();
        foreach (Button button in buttons)
        {
            button.interactable = true;
        }
    }

    public void OpenHowTo()
    {
        HowTo.SetActive(true);
        HowTo.GetComponent<RectTransform>().DOAnchorPosY(0, 0.5f).SetEase(Ease.OutBack);
        HowTo.GetComponent<HowToPlayAnimator>().Animate();
    }

    public void CloseHowTo()
    {
        HowTo.GetComponent<RectTransform>().DOAnchorPosY(-1000, 0.5f).SetEase(Ease.InBack).OnComplete(()=> HowTo.SetActive(false));
    }


    public void PlayGame()
    {
        DOTween.KillAll();
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
