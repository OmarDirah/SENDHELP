using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraRig : MonoBehaviour
{
    public Transform yAxis;
    public Transform xAxis;
    public float moveTime;
    // Start is called before the first frame update
   

    public void AlignTo(Transform target) 
    {

        Sequence seq = DOTween.Sequence();
        seq.Append(yAxis.transform.DOMove(target.position, 0.75f));
        //seq.Join(yAxis.DORotate(new Vector3(0f, target.rotation.eulerAngles.y, 0f), 0.75f));
        //seq.Join(xAxis.DOLocalRotate(new Vector3(0f, target.rotation.eulerAngles.x, 0f), 0.75f));


    }
}
