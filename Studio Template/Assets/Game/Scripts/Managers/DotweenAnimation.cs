using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public enum DoAnimationType
{
    Move,
    Rotate,
    Scale,
    Jump,
    Text,
    TextFade,   
    SpriteFade,
    SpriteColor,
    RectTransformMove,
    RectTransformRotate,
    RectTransformScale
   
}


[AddComponentMenu("DOTween/DotweenAnimation")]
public class DotweenAnimation : MonoBehaviour
{
    public delegate void OnStepComplete();
    public event OnStepComplete onStepComplete;

    [SerializeField] DoAnimationType AnimationType;
    public Vector3 target;
   
    [SerializeField] float float1 = 0f;
    [SerializeField] Color color1;  
    [SerializeField] float duration = 2f;
    [SerializeField] float jumpPower = 2f;
    [SerializeField] Ease easeType = Ease.Linear;   
    [SerializeField] bool isToFrom;
    [SerializeField] bool isLocal;
    [SerializeField] int Loops = -1;
    [SerializeField] float wait = 0f;
    [SerializeField] LoopType loopType = LoopType.Yoyo;
    [SerializeField] RotateMode rotateMode = RotateMode.LocalAxisAdd;
    [SerializeField] bool isTextMeshPro = false;

    Vector3 toPos;
    public Tween t;
    void Start()
    {
        SetTween();
    }


    void SetTween()
    {
        if (AnimationType == DoAnimationType.Move)
        {
            if (isToFrom)
            {
                toPos = transform.localPosition;
                transform.localPosition = target;
                target = toPos;
            }

            if (isLocal)
            {
                t = transform.DOLocalMove(target, duration).SetEase(easeType).
                    SetAutoKill(true).SetLoops(Loops, loopType).OnStepComplete(() => onStepComplete());
            }
            else
            {
                t = transform.DOMove(target, duration).SetEase(easeType).SetAutoKill(true).SetLoops(Loops, loopType);
            }



        }


        if (AnimationType == DoAnimationType.Rotate)
        {
            if (!isLocal)
            {
                t = transform.DORotate(target, duration, rotateMode).SetEase(easeType).SetLoops(Loops, loopType);
            }
            else
            {
                t = transform.DOLocalRotate(target, duration, rotateMode).SetEase(easeType).SetLoops(Loops, loopType);
            }


            if (wait > 0f)
            {
                t.SetDelay(wait).OnComplete(() => {
                    if (Loops == -1)
                    {
                        RotateLoopWithDelay();
                    }

                });
            }


        }


        if (AnimationType == DoAnimationType.Scale)
        {
            t = transform.DOScale(target, duration).SetEase(easeType).SetLoops(Loops, loopType);
        }


        if (AnimationType == DoAnimationType.Jump)
        {
            if (isLocal)
            {
                t = transform.DOLocalJump(target, jumpPower, 1, duration).SetEase(easeType).SetLoops(Loops, loopType);
            }
            else
            {
                t = transform.DOJump(target, jumpPower, 1, duration).SetEase(easeType).SetLoops(Loops, loopType);
            }
        }

        if (AnimationType == DoAnimationType.Text)
        {

            var txt = transform.GetComponent<Text>().text;
            transform.GetComponent<Text>().text = "";
            t = transform.GetComponent<Text>().DOText(txt, duration).SetEase(easeType);
        }

        if (AnimationType == DoAnimationType.TextFade)
        {
            if(isTextMeshPro)
                t = transform.GetComponent<TextMeshProUGUI>().DOFade(float1, duration).SetEase(easeType).SetLoops(Loops, loopType);
            else
                t = transform.GetComponent<Text>().DOFade(float1, duration).SetEase(easeType).SetLoops(Loops, loopType);
        }      

        if (AnimationType == DoAnimationType.SpriteFade)
        {

            t = transform.GetComponent<SpriteRenderer>().DOFade(float1, duration).SetEase(easeType).SetLoops(Loops, loopType);
        }

        if (AnimationType == DoAnimationType.SpriteColor)
        {

            t = transform.GetComponent<SpriteRenderer>().DOColor(color1, duration).SetEase(easeType).SetLoops(Loops, loopType);
        }

        if (AnimationType == DoAnimationType.RectTransformMove)
        {

            t = transform.GetComponent<RectTransform>().DOLocalMove(target, duration).SetEase(easeType).SetAutoKill(true).SetLoops(Loops, loopType).SetUpdate(true);
        }

        if (AnimationType == DoAnimationType.RectTransformRotate)
        {

            t = transform.GetComponent<RectTransform>().DOLocalRotate(target, duration).SetEase(easeType).SetAutoKill(true).SetLoops(Loops, loopType).SetUpdate(true);
        }



        if (AnimationType == DoAnimationType.RectTransformScale)
        {

            t = transform.GetComponent<RectTransform>().DOScale(target, duration).SetEase(easeType).SetAutoKill(true).SetLoops(Loops, loopType).SetUpdate(true);
        }



        if (AnimationType != DoAnimationType.Rotate)
            t.SetDelay(wait);
    }

    void RotateLoopWithDelay()
    {
        t = transform.DORotate(target, duration, rotateMode).SetEase(easeType).SetLoops(Loops, loopType);

        t.SetDelay(wait).OnComplete(() => {

            RotateLoopWithDelay();
        });

    }

    private void Reset()
    {
        target = transform.localPosition;

        if (AnimationType == DoAnimationType.TextFade)
        {
            transform.GetComponent<Text>().DOFade(1f, 0).SetEase(easeType);
        }
    }

    public void Pause()
    {
        t.Pause();
    }
    public void Resume()
    {
        t.Play();

    }

    public void Restart()
    {
        t.Kill();
        transform.localPosition = Vector3.zero;

        if (AnimationType == DoAnimationType.TextFade)
        {

            transform.GetComponent<Text>().DOFade(1f, 0).SetEase(easeType);
        }

        SetTween();
    
    }
}
