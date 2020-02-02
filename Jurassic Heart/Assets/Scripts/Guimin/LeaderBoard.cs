﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderBoard : MonoBehaviour
{
    public class Player
    {
        private string name;
        private int score;
        
        public Player()
        {

        }
        public string getName()
        {
            return name;
        }
        public int getScore()
        {
            return score;
        }
        public void setName(string newName)
        {
            name = newName;
        }
        public void setScore(int newScore)
        {
           score = newScore;
        }
    }
    List<Player> PlayerData = new List<Player>();
    Player curplayer = new Player();
    public Text[] nameline;
    public Text[] scoreLine;
    public Text yourName;
    public Text yourScore;
    public Button BtnAdd;
    public InputField userName;
    public float countTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        Button btn = BtnAdd.GetComponent<Button>();
        btn.onClick.AddListener(Click);
        if(PlayerData.Count == 0)
        {
            for (int i = 0; i < 4; i++)
            {
                Player ouc = new Player();
                PlayerData.Add(ouc);
                PlayerPrefs.SetString("Name_" + i, null);
                PlayerPrefs.SetInt("Score_" + i, 0);
            }
        }
        ReadData();
    }

    // Update is called once per frame
    void Update()
    {
        countTime += Time.deltaTime;
    }
    void ReadData()
    {
        for(int i = 0; i < 4; i++)
        {
            PlayerData[i].setName(PlayerPrefs.GetString("Name_" + i));
            PlayerData[i].setScore(PlayerPrefs.GetInt("Score_" + i));
        }
    }
    void WriteData()
    {
        for (int i = 0; i < 4; i++)
        {
            PlayerPrefs.SetString("Name_" + i, PlayerData[i].getName());
            PlayerPrefs.SetInt("Score_" + i, PlayerData[i].getScore());
        }
    }
    void SortData() {
        for (int j = 0; j < PlayerData.Count; j++)
        {
            for (int k = j + 1; k < PlayerData.Count; k++)
            {
                if (PlayerData[k].getScore() > PlayerData[j].getScore())
                {
                    Player temp = PlayerData[j];
                    PlayerData[j] = PlayerData[k];
                    PlayerData[k] = temp;
                }
            }
        }
    }
    void Click()
    {
        //btnAdd.interactable = false;
        curplayer.setName(userName.text);
        curplayer.setScore((int)(100 / (countTime + 1) + 100));
        yourName.text = curplayer.getName();
        yourScore.text = curplayer.getScore().ToString();
        Player temp = new Player();
        temp.setName(curplayer.getName());
        temp.setScore(curplayer.getScore());
        PlayerData.Add(temp);

        SortData();
        for (int i = 0; i < 4; i++)
        {
            nameline[i].text = PlayerData[i].getName();
            scoreLine[i].text = PlayerData[i].getScore().ToString();
            Debug.Log("playerdata" + i + PlayerData[i].getName());
        }
        WriteData();

    }

}
