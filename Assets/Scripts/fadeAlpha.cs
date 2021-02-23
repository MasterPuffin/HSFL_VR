using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fadeAlpha : MonoBehaviour {
    [Range(0.1f, 1.0f)]
    public Color spriteColor; // Used to store color reference.

    public SpriteRenderer sprite;

    void Start() {
        // Get material's starting color value.
        sprite = this.GetComponent<SpriteRenderer>();
        spriteColor = sprite.color;
    }


    // Update is called once per frame
    void Update() {
        while (spriteColor.a > 0.0f) {
            spriteColor.a -= 0.08f;
        }

        spriteColor = new Color(spriteColor.r, spriteColor.g, spriteColor.b, spriteColor.a);
    }
}