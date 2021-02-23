using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fadeAlpha : MonoBehaviour
{
    [Range (0.1f, 1.0f)]
     //public float fadeSpeed = 0.1f;    // How fast alpha value decreases.
     public Color spriteColor;            // Used to store color reference.
     public SpriteRenderer sprite;
 
     void Start ()
     {
 
         // Get material's starting color value.
        sprite = this.GetComponent<SpriteRenderer>();    
        spriteColor = sprite.color;
 
       
     }
    
         

    // Update is called once per frame
    void Update()
    {
        //  // Alpha start value.
        //  float alpha = spriteColor.a;
 
        //  // Loop until aplha is below zero (completely invisalbe)
         
        //      // Reduce alpha by fadeSpeed amount.
         
 
        //  while (alpha > 0) 
        //  {
        //      alpha -= fadeSpeed * Time.deltaTime;
        //     // Create a new color using original color RGB values combined
        //      // with new alpha value. We have to do this because we can't 
        //      // change the alpha value of the original color directly.
        //      spriteColor = new Color(spriteColor.r, spriteColor.g, spriteColor.b, alpha);
        //  }

         while(spriteColor.a > 0.0f){
            spriteColor.a -= 0.08f;
            
         }    
         spriteColor = new Color(spriteColor.r,spriteColor.g,spriteColor.b,spriteColor.a);
  }
}
