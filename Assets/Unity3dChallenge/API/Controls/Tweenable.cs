using UnityEngine;
using System.Collections;
using DG.Tweening;

public class Tweenable : MonoBehaviour 
{
    public Vector3 Direction = -Vector3.right;
    public float AnimationTime = 0.5f;

    public void PunchScale(float amount)
    {
        transform.DOPunchScale(Direction * amount, AnimationTime, 7);
    }

    public void PunchPosition(float amount)
    {
        transform.DOPunchPosition(Direction * amount, AnimationTime, 7);
    }
	
}
