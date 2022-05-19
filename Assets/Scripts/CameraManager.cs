using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{

    Transform target;

    // Start is called before the first frame update
    void Start()
    {
        target = HeroManager.hm.headHero.transform;
    }

    private void LateUpdate()
    {
        transform.position = target.position;
    }
}
