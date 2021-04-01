
using UnityEngine;
using System.Collections;
using System;
using DG.Tweening;
using UnityEngine.UI;

public class Flyer : MonoBehaviour
{
    public Image m_Image;
    public delegate void FlyerCallback();
    public void SetSprite(Sprite sp)
    {
        m_Image.sprite = sp;
    }
    public IEnumerator FlyToTarget(Vector3 target, FlyerCallback callback)
    {
        Debug.Log(target);
        Vector3 start = gameObject.transform.position;
        Vector3 end = target;

        float length = (end - start).magnitude;

        Vector3 up = (end - start).normalized * length / 6.0f;
        Vector3 right = Quaternion.AngleAxis(270, Vector3.forward) * up;

        Vector3 v1 = start + up;
        Vector3 v2 = v1 + up * 2;
        Vector3 v3 = v2 + up * 2;

        v1 = v1 + new Vector3(UnityEngine.Random.Range(-length / 5.0f, length / 5.0f), UnityEngine.Random.Range(-length / 8.0f, length / 8.0f), 0);
        v2 = v2 + new Vector3(UnityEngine.Random.Range(-length / 5.0f, length / 5.0f), UnityEngine.Random.Range(-length / 8.0f, length / 8.0f), 0);
        v3 = v3 + new Vector3(UnityEngine.Random.Range(-length / 5.0f, length / 5.0f), UnityEngine.Random.Range(-length / 8.0f, length / 8.0f), 0);

        Vector3[] next = new Vector3[] { start, start, v1, v2, v3, end, end };
        transform.DORotate(new Vector3(0, 0, 360f), 1.0f, RotateMode.FastBeyond360).SetLoops(1);
        transform.DOScale(new Vector3(0.9f, 0.9f, 0.9f), 0.2f);
        transform.DOPath(next, 1.0f, PathType.CatmullRom, PathMode.TopDown2D).SetEase(Ease.InQuad);// move it back to the start without an LTSpline

        yield return Yielders.Get(1.0f);

        if (callback != null)
        {
            callback();
        }

        yield return Yielders.EndOfFrame;
    }

    public void FlyToTargetOneSide(Vector3 target, FlyerCallback callback, bool rotate = true, int side = -1, float scaleTo = 1.0f)
    {
        StartCoroutine(coFlyToTargetOneSide(target, callback, rotate, side, scaleTo));
    }

    IEnumerator coFlyToTargetOneSide(Vector3 target, FlyerCallback callback, bool rotate = true, int side = -1, float scaleTo = 1.0f)
    {
        //Vector3 start = gameObject.transform.position;
        //Vector3 end = target;
        //float length = (end - start).magnitude;
        //Vector3 up = (end - start).normalized * length / 2.0f;
        //Vector3 right = Quaternion.AngleAxis(270, Vector3.forward) * up;
        //Vector3 left = Quaternion.AngleAxis(90, Vector3.forward) * up;
        //Vector3 v1 = Vector3.zero;        
        //v1 = start + up + side * right * 0.2f;        
        //Vector3[] next = new Vector3[] { start, start, v1, end, end };
        //if (rotate) {
        //    LeanTween.rotateAroundLocal(gameObject, Vector3.forward, 360, 1.0f);
        //}
        //LeanTween.scale(gameObject, new Vector3(scaleTo, scaleTo, scaleTo), 1.0f);    
        //LeanTween.moveSpline(gameObject, next, 1.0f).setEase(LeanTweenType.easeInQuad); // move it back to the start without an LTSpline                
        //yield return Yielders.Get(1.0f);        
        float hh = 9.6f;
        float hw = 5.04f;

        //float y = UnityEngine.Random.Range(-hh, hh);
        //float x = UnityEngine.Random.Range(-hw, hw);
        //Vector3 target1 = new Vector3(x, y, 0);

        float time1 = 1.0f;
        float time2 = 1.0f;

        float y = UnityEngine.Random.Range(-3.0f, 3.0f);
        float x = UnityEngine.Random.Range(-3.0f, 3.0f);
        Vector3 target1 = new Vector3(x, y, 0) + transform.position;

        time1 = (float)Math.Sqrt(x * x + y * y) / 3.0f;

        transform.DOMove(target1, time1).SetEase(Ease.InOutQuad);
        yield return Yielders.Get(time1);

        transform.DOMove(target, time2).SetEase(Ease.InOutQuad);
        yield return Yielders.Get(time2);

        SimplePool.Despawn(gameObject);
        if (callback != null)
        {
            callback();
        }
        yield return Yielders.EndOfFrame;
    }

    public IEnumerator FlyToBottle(Vector3 target, float time)
    {
        Vector3 start = gameObject.transform.position;
        Vector3 end = target;

        float length = (end - start).magnitude;

        Vector3 up = (end - start).normalized * length / 4.0f;
        Vector3 right = Quaternion.AngleAxis(270, Vector3.forward) * up;

        Vector3 v1 = start + up + right * UnityEngine.Random.Range(1.0f, 1.5f);
        Vector3 v2 = (v1 + end) / 2;

        Vector3[] next = new Vector3[] { start, start, v1, v2, end, end };

        transform.DOLocalRotate(new Vector3(0, 0, 360), time);

        transform.DOScale(new Vector3(1.0f, 1.0f, 1.0f), 1.0f);
        transform.DOPath(next, time).SetEase(Ease.InOutQuad); // move it back to the start without an LTSpline

        yield return Yielders.Get(time);
        yield return Yielders.EndOfFrame;
    }

    //internal void FlyToTargetOneSide(object position, Action onDiamondReach, bool v) {
    //    throw new NotImplementedException();
    //}

    public void FlyToTargetType1(Vector3 target, FlyerCallback callback)
    {
        StartCoroutine(coFlyToTargetType1(target, callback));
    }

    IEnumerator coFlyToTargetType1(Vector3 target, FlyerCallback callback)
    {

        transform.DOMoveY(target.y, 1.5f).SetEase(Ease.InBack);
        transform.DOMoveX(target.x, 1.5f).SetEase(Ease.InQuad);
        yield return new WaitForSeconds(1.5f);

        SimplePool.Despawn(gameObject);
        if (callback != null)
        {
            callback();
        }
        yield return Yielders.EndOfFrame;
    }

    public void FlyToTargetEXP(Vector3 target, FlyerCallback callback, bool rotate = true, int side = -1, float scaleTo = 1.0f, float range = 1f, float flyTime1 = 2f)
    {
        StartCoroutine(coFlyToTargetEXP(target, callback, rotate, side, scaleTo, range, flyTime1));
    }

    IEnumerator coFlyToTargetEXP(Vector3 target, FlyerCallback callback, bool rotate = true, int side = -1, float scaleTo = 1.0f, float range = 1f, float flyTime1 = 2f)
    {
        float time1 = 1.0f;
        float time2 = 0.5f;

        float y = UnityEngine.Random.Range(-range, range);
        float x = UnityEngine.Random.Range(-range, range);
        Vector3 target1 = new Vector3(x, y, 0) + transform.position;

        time1 = (float)Math.Sqrt(x * x + y * y) / flyTime1;

        transform.DOMove(target1, time1).SetEase(Ease.InOutQuad);
        yield return Yielders.Get(time1);
        yield return Yielders.Get(0.3f);
        if (rotate)
        {
            transform.DOLocalRotate(transform.localEulerAngles + new Vector3(0, 0, 360), 1.0f);
        }
        transform.DOMove(target, time2).SetEase(Ease.InQuad);
        yield return Yielders.Get(time2);

        SimplePool.Despawn(gameObject);
        if (callback != null)
        {
            callback();
        }
        yield return Yielders.EndOfFrame;
    }
}
