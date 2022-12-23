using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InactiveOnClose : MonoBehaviour {
    public void OnClose() {
        gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 1f;  // Time is stopped
    }
}
