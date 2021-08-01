using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public abstract class AbstractPlayer : MonoBehaviour
{

    public MainController master;
    public ProjectSettings settings;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public abstract void Play(string mediaPath);
    public abstract void Show();
    public abstract void Hide();
    public abstract void PreloadMedia(string mediaPath);

    public virtual void PrepareMedia(string mediaPath, VideoPlayer.EventHandler callback) { }
}
