using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class MileStone : MonoBehaviour, IPooledObject
{
    private VisualEffect vfx;

    public void onObjectSpawn()
    {
        vfx.Play();
    }

    // Start is called before the first frame update
    void Awake()
    {
        vfx = GetComponent<VisualEffect>();
    }

}
