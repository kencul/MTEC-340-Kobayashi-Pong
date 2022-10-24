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
    public TextMeshProUGUI titleGUI;

    private AudioSource m_audioSource;

    public AudioClip gameoverSound;
    public AudioClip startSound;
    public AudioClip serveSound;

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
        State = "home";
        titleGUI.enabled = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(startKey))
        {
            if (State == "home")
            {
                State = "play";
                playSound(startSound, 0.5f);
                messagesGUI.enabled = false;
                titleGUI.enabled = false;
            }
            else if(State == "serve")
            {
                State = "play";
                playSound(serveSound, 0.5f);
            }
            else if (State == "gameover")
            {
                State = "home";
                titleGUI.text = "PONG";
                resetScore();
            }
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
                State = "gameover";
                playSound(gameoverSound, 0.5f);
                titleGUI.text = "Player " + player + " wins!";
                titleGUI.enabled = true;
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
