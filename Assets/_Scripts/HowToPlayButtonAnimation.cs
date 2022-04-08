using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HowToPlayButtonAnimation : MonoBehaviour
{
    public Image image;
    public Button button;

    // blue/white colors for the image
    private Color blue = new Color(0.0f, 214 / 255f, 214 / 255f);          
    private Color white = new Color(208 / 255f, 208 / 255f, 208 / 255f);

    // white/grey colors for the disabled colors
    private Color disabledWhite = new Color(1, 1, 1);
    private Color disabledGrey = new Color(200 / 255f, 200 / 255f, 200 / 255f);

    // change color of button from blue to white or vice versa
    void ColorChange()
    {
        // swap between blue/white
        if (image.color == blue) image.color = white;
        else image.color = blue;
    }

    void ClickAnimation()
    {
        if (button == null) return;

        // define a new color block, change its disabled color, then assign it to the button
        var newColorBlock = button.colors;

        if (button.colors.disabledColor == disabledWhite) newColorBlock.disabledColor = disabledGrey;
        else newColorBlock.disabledColor = disabledWhite;

        button.colors = newColorBlock;
    }

}
