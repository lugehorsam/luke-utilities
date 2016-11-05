using UnityEngine;
using System.Collections;

public class RadialGradientTexture : MonoBehaviour {

    //TEXTURE SETTINGS
    const int texWidth = 512;
    const int texHeight = 512;

    //MASK SETTINGS
    const float maskThreshold = 2.0f;

    //REFERENCES
    Texture2D mask;

    [SerializeField]
    Renderer rendererToApply;

    [SerializeField]
    bool applyOnStart;
 
    void Start()
    {
        GenerateTexture();
        if (rendererToApply != null)
            rendererToApply.material.mainTexture = mask;
    }

    void GenerateTexture()
    {
        mask = new Texture2D(texWidth, texHeight, TextureFormat.RGBA32, mipmap: false);
        Vector2 maskCenter = new Vector2(texWidth * 0.5f, texHeight * 0.5f);

        for (int y = 0; y < texHeight; ++y){
            for (int x = 0; x < texWidth; ++x){
                float distFromCenter = Vector2.Distance(maskCenter, new Vector2(x, y));
                float maskPixel = (0.5f - (distFromCenter / texWidth)) * maskThreshold;
                mask.SetPixel(x, y, new Color(maskPixel, maskPixel, maskPixel, 1.0f));
            }
        }
        mask.Apply();
    }
}
