using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageDisplayController : MonoBehaviour
{

    public VideoPlayerController videoPlayer;
    public MainController master;
    public Rect screenRect;
    public Texture2D drawTexture;
    private Dictionary<string, Texture2D> imageBank = new Dictionary<string, Texture2D>();
    public float timer;
    bool show = false;
    // Start is called before the first frame update
    void Start()
    {
        screenRect = new Rect(0, 0, Screen.width, Screen.height);
    }

    // Update is called once per frame
    void Update()
    {
        if (show)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                master.Next();
            }
        }
    }

    public void DisplayImage(string imagePath) 
    {
        imagePath = imagePath.Replace("Assets/Resources/", "");
        imagePath = imagePath.Replace(".jpg", "");
        imagePath = imagePath.Replace(".png", "");
        if (!imageBank.ContainsKey(imagePath))
        {
            imageBank[imagePath]= (Texture2D)Resources.Load(imagePath) ;
        }
        drawTexture = imageBank[imagePath];
    }

    public void Hide()
    {
        show = false;
    }

    public void Show()
    {
        show = true;
    }

    public void SetDelayNext(int waitSeconds)
    {
        timer = waitSeconds;
    }

    public void OnGUI()
    {
        if (show &&drawTexture)
        {
            Graphics.DrawTexture(screenRect, drawTexture);

        }
    }
}
