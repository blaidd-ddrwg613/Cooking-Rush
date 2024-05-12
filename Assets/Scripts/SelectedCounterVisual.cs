using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour {

    [SerializeField] private BaseCounter baseCounter;
    [SerializeField] private GameObject[] visualGameObjectArray;
    
    private void Start() {
        Player.Instance.onSelectedCounterChange += PlayerOnSelectedCounterChange;
    }

    private void PlayerOnSelectedCounterChange(object sender, Player.OnSelectedCounterChangeEventArgs e) {
        if (e.selectedCounter == baseCounter) {
            Show();
        } else {
            Hide();
        }
    }

    private void Show() {
        foreach (GameObject gameObject in visualGameObjectArray) {
            gameObject.SetActive(true);
        }
    }

    private void Hide() {
        foreach (GameObject gameObject in visualGameObjectArray) {
            gameObject.SetActive(false);
        }
    }
}
