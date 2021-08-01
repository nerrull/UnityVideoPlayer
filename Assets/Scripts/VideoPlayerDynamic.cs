using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoPlayerDynamic : AbstractPlayer
{

    public VideoPlayer videoPlayer;
    private GameObject gameCamera;
    // Instantiate and set up the videoplayer   
    void Start()
    {
        master = GetComponent<MainController>();
        settings = GetComponent<ProjectSettings>();
        // Will attach a VideoPlayer to the main camera.
        gameCamera = GameObject.Find("Main Camera");

        // VideoPlayer automatically targets the camera backplane when it is added
        // to a camera object, no need to change videoPlayer.targetCamera.
        videoPlayer = gameCamera.GetComponent<VideoPlayer>();

        // Play on awake defaults to true. Set it to false to avoid the url set
        // below to auto-start playback since we're in Start().
        videoPlayer.playOnAwake = false;

        // By default, VideoPlayers added to a camera will use the far plane.
        // Let's target the near plane instead.
        videoPlayer.renderMode = UnityEngine.Video.VideoRenderMode.CameraNearPlane;
        videoPlayer.aspectRatio = VideoAspectRatio.FitOutside;

        videoPlayer.loopPointReached += EndReached;
        videoPlayer.isLooping = true;
        videoPlayer.enabled = false;
    }

    public override void PreloadMedia(string mediaPath)
    {

    }

    void EndReached(UnityEngine.Video.VideoPlayer vp)
    {
        vp.Stop();
        master.Next();
    }

    public override void PrepareMedia(string videoPath, VideoPlayer.EventHandler callback)
    {
        if (!videoPlayer.enabled)
        {
            videoPlayer.enabled = true;
        }
        // Set the video to play. URL supports local absolute or relative paths.
        // Here, using absolute.
        videoPlayer.url = videoPath;

        // Start playback. This means the VideoPlayer may have to prepare (reserve
        // resources, pre-load a few frames, etc.). To better control the delays
        // associated with this preparation one can use videoPlayer.Prepare() along with
        // its prepareCompleted event.
        videoPlayer.Prepare();
        videoPlayer.prepareCompleted += callback;
    }

    public override void Play(string videoPath )
    {
        videoPlayer.Play();
    }
    // Update is called once per frame
    void Update()
    {

        if (videoPlayer && settings.TestMode && videoPlayer.frame > settings.TestModeMaxVideoFrames)
        {
            EndReached(videoPlayer);
        }
        else if (videoPlayer && videoPlayer.frame > 0 && (ulong)videoPlayer.frame > (videoPlayer.frameCount - 30))
        {
            EndReached(videoPlayer);
        }

    }

    public override void Hide()
    {
        if (videoPlayer.isPlaying)
        {
            videoPlayer.Stop();
        }
        videoPlayer.enabled = false;
    }

    public override void Show()
    {
        videoPlayer.enabled = true;
    }
}

