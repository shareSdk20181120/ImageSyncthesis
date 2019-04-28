using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
public class ImageSyncthesis : MonoBehaviour {

    public RawImage[] desTexs;
    public RawImage desImg;
    public string desPath
    {
       get { return Path.Combine(Application.streamingAssetsPath, "shareCapture.png"); }
    }
    //Use this for initialization

   void Start ()
    {
        
        CaptureScreenShot();
        
    }
	

    public void CaptureScreenShot()
    {
        StartCoroutine(CaptureCoroutine(desTexs));
    }

    private IEnumerator CaptureCoroutine( RawImage[] desTexs)
    {
        yield return new WaitForEndOfFrame();
        //截取当前屏幕
        Rect rect = new Rect(0, 0, Screen.width, Screen.height);
        Texture2D sourceTexture = new Texture2D((int)rect.width, (int)rect.height, TextureFormat.RGB24, false);
        sourceTexture.ReadPixels(rect, 0, 0);
        sourceTexture.Apply();

        //将目标图片合成到原图片上
        for (int i = 0; i < desTexs.Length; i++)////Resources.Load<Texture2D>("Texture/qrcode");
        {
            Texture2D textureQrCode = desTexs[i].texture as Texture2D;
            // textureQrCode.alphaIsTransparency = true;
            //textureQrCode.anisoLevel = 1;
            for (int x = 0; x < textureQrCode.width; x++)
            {
                for (int y = 0; y < textureQrCode.height; y++)
                {
                    int iniX = (int)(sourceTexture.width / 2f + x + desTexs[i].transform.localPosition.x - textureQrCode.width / 2f);
                    int iniY = (int)(sourceTexture.height / 2f + y + desTexs[i].transform.localPosition.y - textureQrCode.height / 2f);
                    Color color = textureQrCode.GetPixel(x, y);
                    if (color.a > 0.11f)
                        sourceTexture.SetPixel(iniX, iniY,color);
                }
            }
        }
        sourceTexture.Apply();
        if(!Directory.Exists(Application.streamingAssetsPath))
        {
            Directory.CreateDirectory(Application.streamingAssetsPath);
        }
        //保存图片到目标路径
        File.WriteAllBytes(desPath, sourceTexture.EncodeToPNG());
        desImg.texture = sourceTexture;
        
        //Destroy(sourceTexture);
        //sourceTexture = null;
    }
    
}
