using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleController : MonoBehaviour
{
    private void OnMouseDown()
    {
        transform.parent.gameObject.SetActive(false);
    }
}
