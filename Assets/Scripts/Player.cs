using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float balance;
    public float currBet;
    public GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        balance = 500;
        gm.totalBal.text = "$" + balance;
    }
}
