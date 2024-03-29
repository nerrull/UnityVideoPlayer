﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class MainController : MonoBehaviour
{

    public enum VisualMedia
    {
        Image,
        Video
    }

    public enum VisualContent
    {
        LowBattery,
        Esker
    }


    public VisualContent currentContent;
    public VisualMedia currentMedia;
    public AbstractPlayer videoController;
    public ImageDisplayController imageController;
    public ProjectSettings settings;

    public List<string> LowBatteryImages;
    public List<string> EskerImages;
    public List<string> EskerVideos;
                             
    bool flipflop = true;
    bool started = false;
    // Start is called before the first frame update
    void Start()
    {
        videoController = gameObject.AddComponent<VideoPlayerDynamic>();
        LoadAllImages();
        LoadAllVideo();
        currentMedia = VisualMedia.Image;
        currentContent = VisualContent.Esker;
    }
    public void Update()
    {
        if (!started)
        {
            started = true;
            GetNextElement();

        }
    }


    public void Next()
    {
        currentContent = GetNextContentType();
        GetNextElement();
    }

    string GetEskerVideo()
    {
        int idx = Random.Range(0, EskerVideos.Count);
        return EskerVideos[idx];
    }
    string GetEskerImage()
    {
        int idx = Random.Range(0, EskerImages.Count);
        return EskerImages[idx];
    }
    string GetLowBatteryImage()
    {
        int idx = Random.Range(0, LowBatteryImages.Count);
        return LowBatteryImages[idx];
    }

    void GetNextElement()
    {
        if (currentContent == VisualContent.LowBattery)
        {
            if (currentMedia == VisualMedia.Video)
            {
                //hide video component
                videoController.Hide();
            }
            var image = GetLowBatteryImage();
            imageController.DisplayImage(image);
            imageController.Show();
            imageController.SetDelayNext(settings.TestMode? settings.TetModeImageDisplayTime : settings.LowBatteryImageDisplayTime);
            //get image from charging bank
            // set image on image controller
            //display image
            currentMedia = VisualMedia.Image;
            currentContent = VisualContent.LowBattery;
        }

        else if (currentContent == VisualContent.Esker)
        {
            var nextMedia = GetNextMediaType();

            if (currentMedia == VisualMedia.Video && nextMedia == VisualMedia.Image)
            {
                //hide video
                videoController.Hide();
                //choose image
                var image = GetEskerImage();
                //display image
                imageController.DisplayImage(image);
                //show image component
                imageController.Show();
                imageController.SetDelayNext(settings.TestMode ? settings.TetModeImageDisplayTime : settings.EskerImageDisplayTime);
            }

            if (currentMedia == VisualMedia.Image &&nextMedia == VisualMedia.Video)
            {

                //choose video
                var video = GetEskerVideo();

                //show video component
                videoController.Show();
                videoController.PrepareMedia(video, VideoPreparedCallback);
            }

            if (currentMedia == nextMedia)
            {
                if (currentMedia == VisualMedia.Image)
                {
                    //choose image
                    var image = GetEskerImage();
                    //display image
                    imageController.DisplayImage(image);
                    imageController.SetDelayNext(settings.TestMode ? settings.TetModeImageDisplayTime : settings.EskerImageDisplayTime);

                }
                else if (currentMedia ==VisualMedia.Video)
                {
                    //choose video
                    var video = GetEskerVideo();
                    //play video
                    videoController.Play(video);
                }

            }
            currentMedia = nextMedia;
            currentContent = VisualContent.Esker;
        }
    }

    VisualMedia GetNextMediaType()
    {
        flipflop = !flipflop;
        if (flipflop)
        {
            Debug.Log("Showing image");

            return VisualMedia.Image;
        }
        else
        {
            Debug.Log("Showing video");

            return VisualMedia.Video;
        }
    }

    VisualContent GetNextContentType()
    {
        if (currentContent == VisualContent.Esker)
        {
            Debug.Log("Switching to Low Battery Media");
            return VisualContent.LowBattery;
        }
        else
        {
            Debug.Log("Switching to Esker Media");
            return VisualContent.Esker;
        }
    }

    void LoadAllImages()
    {
        foreach (var image in LowBatteryImages)
        {
            imageController.DisplayImage(image);
        }
        foreach (var image in EskerImages)
        {
            imageController.DisplayImage(image);
        }
    }

    void LoadAllVideo()
    {
        foreach (var video in EskerVideos)
        {
            videoController.PreloadMedia(video);
        }

    }

    void VideoPreparedCallback(VideoPlayer player)
    {
        //play video
        videoController.Play("");
        //hide image
        imageController.Hide();
    }
}
