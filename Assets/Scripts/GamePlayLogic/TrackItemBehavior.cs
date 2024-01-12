using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TrackItemBehavior : BaseView
{
    private void Update()
    {
        gameObject.transform.position -= Vector3.back * -1;
    }

}
