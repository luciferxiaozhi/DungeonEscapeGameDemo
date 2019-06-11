using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidEffect : MonoBehaviour
{
    public bool faceRight;

    void Start()
    {
        Destroy(this.gameObject, 3.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (faceRight)
            transform.Translate(Vector3.right * 3.0f * Time.deltaTime);
        else
            transform.Translate(Vector3.left * 3.0f * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            IDamageable hit = other.GetComponent<IDamageable>();
            if (hit != null)
            {
                hit.Damage();
                Destroy(this.gameObject);
            }
        }
    }


}
