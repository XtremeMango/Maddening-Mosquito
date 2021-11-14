using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HowToPlayAnimator : MonoBehaviour
{
    public Image bloodFill;
    public Image AnnoyFill;
    public RectTransform targetRect;
    public Image innerBloodFill;

    bool moveRect;
    float rectRadius;
    float deg = 0f;
    int innerFill;
    public void Animate()
    {
        rectRadius = targetRect.anchoredPosition.magnitude;
        moveRect = true;
        
        Sequence blood = DOTween.Sequence();
        Sequence annoy = DOTween.Sequence();
        Sequence inner = DOTween.Sequence();

        blood.Append(bloodFill.DOFillAmount(0.75f, 0.5f).SetEase(Ease.OutQuad))
            .Append(bloodFill.DOFillAmount(0.4f, 2f).SetEase(Ease.OutQuad));
        blood.SetLoops(-1,LoopType.Restart);

        annoy.Append(AnnoyFill.DOFillAmount(1f, 5f).SetEase(Ease.OutQuad))
            .Append(AnnoyFill.DOFillAmount(0f, 2f).SetEase(Ease.OutQuad));
        annoy.SetLoops(-1, LoopType.Restart);
    }

    private void Update()
    {
        if(moveRect)
        {
            targetRect.anchoredPosition = rectRadius * new Vector2(Mathf.Cos(Mathf.Deg2Rad * (deg+90)), Mathf.Sin(Mathf.Deg2Rad * (deg+90)));
            targetRect.rotation = Quaternion.Euler(0, 0, deg);
            deg -= Time.deltaTime * 60;
            if (deg <= -360f)
            {
                deg += 360f;
                if (innerFill < 2)
                {
                    innerBloodFill.DOFillAmount(innerBloodFill.fillAmount - 0.3f, 0.5f).SetEase(Ease.OutQuad);
                }
                else
                {
                    innerBloodFill.DOFillAmount(1f, 0.5f).SetEase(Ease.OutQuad);
                    innerFill = 0;
                }
                innerFill++;
            }
        }
    }
}
