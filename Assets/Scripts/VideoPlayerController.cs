using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
public class VideoPlayerController : MonoBehaviour
{
    public MainController master;
    public ProjectSettings settings;
    public VideoPlayer currentVideo;
    private Dictionary<string, VideoPlayer> videoBank = new Dictionary<string, VideoPlayer>();
    private GameObject gameCamera;
    // Instantiate and set up the videoplayer   
    void Start()
    {
        // Will attach a VideoPlayer to the main camera.
        gameCamera = GameObject.Find("Main Camera");

        
    }

    public void PreloadVideo(string videoPath)
    {
        videoPath = videoPath.Replace("Assets/StreamingAssets", Application.streamingAssetsPath);
        // VideoPlayer automatically targets the camera backplane when it is added
        // to a camera object, no need to change videoPlayer.targetCamera.
        var videoPlayer = gameCamera.AddComponent<VideoPlayer>();

        // Play on awake defaults to true. Set it to false to avoid the url set
        // below to auto-start playback since we're in Start().
        videoPlayer.playOnAwake = false;

        // By default, VideoPlayers added to a camera will use the far plane.
        // Let's target the near plane instead.
        videoPlayer.renderMode = UnityEngine.Video.VideoRenderMode.CameraNearPlane;
        videoPlayer.aspectRatio = VideoAspectRatio.FitOutside;

        // Each time we reach the end, we slow down the playback by a factor of 10.
        videoPlayer.loopPointReached += EndReached;

        // Set the video to play. URL supports local absolute or relative paths.
        // Here, using absolute.
        videoPlayer.url = videoPath;


        // Skip the first 100 frames.
        // videoPlayer.frame = 100;

        // Restart from beginning when done.
        videoPlayer.isLooping = false;
        videoPlayer.Prepare();
        videoBank.Add(videoPath, videoPlayer);
        videoPlayer.enabled = false;
    }

    void EndReached(UnityEngine.Video.VideoPlayer vp)
    {
        vp.Stop();
        vp.enabled = false;
        master.Next();

    }

    public void Play(string videoPath)
    {
        videoPath = videoPath.Replace("Assets/StreamingAssets", Application.streamingAssetsPath);
        currentVideo = videoBank[videoPath];
        currentVideo.enabled = true;
        // Start playback. This means the VideoPlayer may have to prepare (reserve
        // resources, pre-load a few frames, etc.). To better control the delays
        // associated with this preparation one can use videoPlayer.Prepare() along with
        // its prepareCompleted event.
        currentVideo.Play();

    }

    // Update is called once per frame
    void Update()
    {

        if (currentVideo && settings.TestMode && currentVideo.frame > settings.TestModeMaxVideoFrames)
        {
            EndReached(currentVideo);
        }
        else if (currentVideo && currentVideo.frame >0 && (ulong) currentVideo.frame > (currentVideo.frameCount -30))
        {
            EndReached(currentVideo);
        }

    }

    public void Hide()
    {
        //videoPlayer.enabled = false;
    }

    public void Show()
    {
        //videoPlayer.enabled = true;
    }    
}

