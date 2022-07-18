using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITweenAnimationHandler : MonoBehaviour
{
    public bool EnableMove = false;
    public bool EnableRotation = false;
    public bool EnableScale = false;
    public bool EnableAlpha = false;

    [System.Serializable]
    public class Positions
    {
        //public Vector3 Start;
        public bool setLoopClamp = false;
        public Vector3 EndPos;
        public float time;
        public float delay;
        public LeanTweenType leanTweenType;
    }

    [System.Serializable]
    public class Rotations
    {
        //public Vector3 Start;
        public Vector3 EndRotation;
        public float time;
        public float delay;
        public LeanTweenType leanTweenType;
    }

    [System.Serializable]
    public class Scaling
    {
        //public Vector3 Start;
        public Vector3 EndScale;
        public float time;
        public float delay;
        public LeanTweenType leanTweenType;
    }
    [System.Serializable]
    public class Alpha
    {
        public float Start;
        public float End;
        public float time;
        public float delay;
        public LeanTweenType leanTweenType;
    }
    public Positions _positions;
    public Rotations _rotations;
    public Scaling _scaling;
    public Alpha _alpha;
    // Start is called before the first frame update
    void OnEnable()
    {
        if (EnableMove)
        {

            Invoke("MoveTween", _positions.delay);
        }

        if (EnableRotation)
        {
            Invoke("RotateTween", _rotations.delay);
        }

        if (EnableScale)
        {
            Invoke("ScaleTween", _scaling.delay);
        }
            
        if (EnableAlpha)
        {
            Invoke("AlphaTween", _alpha.delay);
        }
    }

    void MoveTween()
    {
        if (_positions.setLoopClamp)
            LeanTween.move(gameObject.GetComponent<RectTransform>(), _positions.EndPos, _positions.time).setEase(_positions.leanTweenType).setLoopClamp();
        else
            LeanTween.move(gameObject.GetComponent<RectTransform>(), _positions.EndPos, _positions.time).setEase(_positions.leanTweenType);
    }

    void RotateTween()
    {
        //LeanTween.rotate(gameObject.GetComponent<RectTransform>(), _rotations.EndRotation, _rotations.time).setEase(_rotations.leanTweenType);
        LeanTween.rotateX(gameObject, _rotations.EndRotation.x, _rotations.time);
        LeanTween.rotateY(gameObject, _rotations.EndRotation.y, _rotations.time);
        LeanTween.rotateZ(gameObject, _rotations.EndRotation.z, _rotations.time);
    }

    void ScaleTween()
    {
        LeanTween.scale(gameObject.GetComponent<RectTransform>(), _scaling.EndScale, _scaling.time).setEase(_scaling.leanTweenType);

    }

    void AlphaTween()
    {
        Image r = gameObject.GetComponent<Image>();
        LeanTween.value(gameObject, _alpha.Start, _alpha.End, _alpha.time).setOnUpdate((float val) =>
        {
            Color c = r.color;
            c.a = val;
            r.color = c;
        }).setLoopPingPong();
    }
}
