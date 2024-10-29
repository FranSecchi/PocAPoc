using FMOD.Studio;
using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance { get; private set; }
    [field: SerializeField] public EventReference music { get; private set; }
    [field: SerializeField] public EventReference people { get; private set; }
    public float loopStartTime;
    private Coroutine coro;
    private bool isPaused;
    private EventInstance musicEvent;
    private EventInstance peopleEvent;


    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one Audio Manager in the scene");
        }

        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        musicEvent = CreateEventInstance(music);
        peopleEvent = CreateEventInstance(people);
        peopleEvent.start();
        SetStartPeople(false);
        musicEvent.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public EventInstance CreateEventInstance(EventReference eventReference)
    {
        EventInstance eventInstance = RuntimeManager.CreateInstance(eventReference);
        return eventInstance;
    }
    public void Play(bool play)
    {
        if (play)
        {
            musicEvent.start();
            SetStartPeople(true);
        }
        else
        {
            // Stop the event with a fade-out
            musicEvent.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            SetStartPeople(false);
        }
    }
    public void Pause(bool pause)
    {
        if (pause)
        {
            isPaused = true;
            musicEvent.setPaused(true);
            SetStartPeople(false);
        }
        else
        {
            isPaused = false;
            musicEvent.setPaused(false);
            SetStartPeople(true);
        }
    }
    public void SetStartPeople(bool isActive)
    {
        // Set the "StartPeople" parameter on the peopleEvent instance
        float parameterValue = isActive ? 0.0f : 1.0f;
        peopleEvent.setParameterByName("StartPeople", parameterValue);
    }
    public void SetMusicSpeed(float speedValue)
    {
        // Set the "speed" parameter on the musicEvent instance
        musicEvent.setParameterByName("speed", Mathf.Min(speedValue, 3f));
    }
}
