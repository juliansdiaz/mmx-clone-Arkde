using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuBackground : MonoBehaviour
{
    //Variables
    [SerializeField] float offsetSpeed;

    Vector2 offset;
    Material backgroundMaterial;

    // Start is called before the first frame update
    void Start()
    {
        backgroundMaterial = GetComponent<Renderer>().material;
        offset = new Vector2(offsetSpeed, 0);
    }

    // Update is called once per frame
    void Update()
    {
        backgroundMaterial.mainTextureOffset += offset * Time.deltaTime; //Set background movement
    }
}
