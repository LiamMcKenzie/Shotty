using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] variations;
    private int currentVariationIndex = 0;
    private int desiredVariationIndex = 0;
    private float trackLength;
    private float sectionLength;
    private float timer = 0;
    public int trackSections = 8;
    public static MusicManager instance;

    void Awake ()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            Debug.LogError("MusicManager instance already exists. Destroying duplicate.");
            return;
        }
    }
    private void Start()
    {
       
        if (variations.Length == 0) //There are no songs loaded
        {
            return;   
        }

        audioSource.clip = variations[currentVariationIndex];
        audioSource.Play();
        trackLength = audioSource.clip.length;
        sectionLength = trackLength / trackSections; //Divide track into 16 sections
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if(timer >= sectionLength && sectionLength != 0)
        {
            if(desiredVariationIndex != currentVariationIndex)
            {
                ChangeTrack();
            }
            timer = 0;
        }
    }

    /// <summary>
    /// Changes the track index to the desired index
    /// </summary>
    /// <param name="index"></param>
    public void ChangeTrackIndex(int index) //This should be called from the editor. This queues up a new track
    {
        //make sure the index is within the bounds of the array
        if(index < 0 || index >= variations.Length)
        {
            //round index to value within range.
            index = Mathf.Clamp(index, 0, variations.Length - 1);
            Debug.LogError("Track index out of bounds, rounding to value within range.");
            return;
        }else{
            desiredVariationIndex = index; //changes desired track index to new value.
        }
    }

    private void ChangeTrack()
    {
        currentVariationIndex = desiredVariationIndex;
        float currentPosition = audioSource.time;

        audioSource.clip = variations[currentVariationIndex];
        audioSource.time = currentPosition;
        audioSource.Play();
    }
}
