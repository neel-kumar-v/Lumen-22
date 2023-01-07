using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerForCanvas : MonoBehaviour
{
    [SerializeField] private Animator canvasAnim;

    [SerializeField] private string canvasMove = "CanvasPanelZoom";

    public GameObject canvas;
    private void OnTriggerEnter(Collider other)
    {
        canvas.SetActive(true);
        canvasAnim.Play(canvasMove, 0, 0.0f);
    }
}
