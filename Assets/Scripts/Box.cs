using UnityEngine;
using UnityEngine.UI;
public class Box : MonoBehaviour {

    public int myNumber;
    public Color myColor;
    public Image img;

    void Start()
    {
        myColor = img.color;
    }
}
