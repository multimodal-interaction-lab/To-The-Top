using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateHands : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(ActivateCoroutine());
    }

    IEnumerator ActivateCoroutine()
    {
        yield return new WaitForSeconds(0.5f);
        transform.GetChild(2).gameObject.SetActive(true);
        transform.GetChild(3).gameObject.SetActive(true);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.GetChild(2).gameObject.SetActive(true);
        transform.GetChild(3).gameObject.SetActive(true);
    }
}
