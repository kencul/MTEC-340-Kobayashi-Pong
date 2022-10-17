using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Player P1;
    public Player P2;

    private Player[] Players = new Player[2];

    public float initBallSpeed;
    public float ballSpeedIncrement;
    public int pointToVictory;

    private string _state;
    public string State
    {
        get => _state;
        set
        {
            _state = value;
        }
    }

    public KeyCode pauseKey;
    public KeyCode startKey;

    public TextMeshProUGUI messagesGUI;

    private AudioSource m_audioSource;

    public void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;
        Players[0] = P1;
        Players[1] = P2;

        m_audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        State = "gameover";
    }

    private void Update()
    {
        if ((State == "gameover"||State == "serve")&& Input.GetKeyDown(startKey))
        {
            State = "play";
            messagesGUI.enabled = false;
        }
        else if (Input.GetKeyDown(pauseKey))
            State = State == "play" ? "pause" : "play";
    }

    public void updateScore (int player)
    {
        Players[player - 1].Score++;
        foreach (Player p in Players)
        {
            if (p.Score >= pointToVictory)
            {
                resetScore();
                break;
            }
        }
    }

    private void resetScore()
    {
        foreach(Player p in Players)
        {
            p.Score = 0;
        }
    }

    public void playSound(AudioClip clip, float volume=1.0f)
    {
        m_audioSource.volume = volume;
        m_audioSource.PlayOneShot(clip);
    }

}
