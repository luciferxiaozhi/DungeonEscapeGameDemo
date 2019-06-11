using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriderAnimationEvent : MonoBehaviour
{
    private Spider _spider;

    private void Start()
    {
        _spider = GetComponentInParent<Spider>();
    }

    public void Fire()
    {
        // Tell spider to fire
        _spider.Attack();
    }
}
