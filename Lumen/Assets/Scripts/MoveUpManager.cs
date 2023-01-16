using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

public class MoveUpManager : MonoBehaviour {
    public MoveUp[] floors;
    public GameObject canvas;
    public void MoveAllFloors() {
        StartCoroutine(MoveFloors());
    }

    public IEnumerator MoveFloors() {
        yield return new WaitForSeconds(1.5f);
        canvas.SetActive(true);
        WaitForSeconds delay = new WaitForSeconds(0.3f);
        foreach (MoveUp floor in floors) {
            floor.move = true;
            yield return delay;
        }
    }
}
