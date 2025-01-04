using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebLoadingBillboard : MonoBehaviour
{
    private Renderer render;

    public void Operate()
    {
        Managers.Images.GetWebImage(OnWebImage);
    }

    private void OnWebImage(Texture2D texture)
    {
        render.material.mainTexture = texture;
    }

    // Start is called before the first frame update
    void Start()
    {
        render = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
