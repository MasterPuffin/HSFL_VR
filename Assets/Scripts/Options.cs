using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Options : MonoBehaviour {
    public List<TextMeshPro> list;

    [Space] public Color color_active;
    public Color color_deactiv;

    public void SetOption(int option) {
        foreach (TextMeshPro x in list) {
            x.color = color_deactiv;
        }

        list[option].color = color_active;
    }
}