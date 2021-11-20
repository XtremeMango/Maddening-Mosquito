
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameOverUIController : MonoBehaviour
{
    [SerializeField]
    GameObject gameOverUI;
    [SerializeField]
    GameObject optionsUI;
    [SerializeField]
    Text gameOverText;
    [SerializeField]
    float delay;
    [SerializeField]
    string[] quotes;

    private void Start()
    {
        Events.events.OnGameStateChangedToGameOver += EnableGameOverUI;
    }

    private void EnableGameOverUI()
    {
        gameOverText.text = quotes[Random.Range(0, quotes.Length - 1)];
        Sequence s = DOTween.Sequence();
        s.AppendInterval(delay)
            .Append(gameOverUI.GetComponent<RectTransform>().DOAnchorPosY(0, 0.5f).SetEase(Ease.OutBack));
    }
}
