using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickedOnBlock : MonoBehaviour
{
    private void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == "block") FindObjectOfType<Game>().CheckAnswer(hit.collider.gameObject);
            }
        }
    }
}
