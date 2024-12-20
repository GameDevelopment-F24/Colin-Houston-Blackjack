using System.Collections;
using System.Collections.Generic;
//using System.Numerics;
using TMPro;
//using Unity.VisualScripting.ReorderableList.Element_Adder_Menu;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Data;
//using System.Collections.Generic;
//using Unity.VisualScripting;
//using UnityEngine.UIElements;
//using UnityEditor.Search;
using UnityEditor;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Player player;
    public Dealer dealer;   
    //public Button betButt;
    public Button dealButt;
    public Button hitButt;
    public Button standButt;
    public TextMeshProUGUI dealerScoreTxt;
    public TextMeshProUGUI playerScoreTxt;
    public int dealerScore;
    public int playerScore;
    public Sprite cardBack;
    public List<Sprite> cards;
    public List<GameObject> activeSprites;
    public List<int> usedCards;
    public List<int> oneCards = new List<int> {9, 22, 35, 48};
    public List<int> twoCards = new List<int> {0, 13, 26, 39};
    public List<int> threeCards = new List<int> {1, 14, 27, 40};
    public List<int> fourCards = new List<int> {2, 15, 28, 41};
    public List<int> fiveCards = new List<int> {3, 16, 29, 42};
    public List<int> sixCards = new List<int> {4, 17, 30, 43};
    public List<int> sevenCards = new List<int> {5, 18, 31, 44};
    public List<int> eightCards = new List<int> {6, 19, 32, 45};
    public List<int> nineCards = new List<int> {7, 20, 33, 46};
    public List<int> tenCards = new List<int> {8, 10, 11, 12, 23, 24, 25, 34, 36, 37, 38, 47, 49, 50, 51};
    public TextMeshProUGUI totalBal; 
    public TextMeshProUGUI totalBet;
    public Button betOne;
    public Button betFive;
    public Button betTwenty;
    public Button betFifty;
    public Button finalBet;
    public TextMeshProUGUI Text21;
    public Button nextButton;
    public float xCardInc;
    public float yCardInc;
    public Vector2 lastPlCardPos;
    public Vector2 lastDlCardPos;

    public TextMeshProUGUI lossText;
    public TextMeshProUGUI tieText;
    public TextMeshProUGUI winText;
    public Button playAgain;
    public Button quitButton;
    public TextMeshProUGUI noBet;
    public TextMeshProUGUI noMoney;


    // Start is called before the first frame update
    void Start()
    {
        oneCards = new List<int> {9, 22, 35, 48};
        twoCards = new List<int> {0, 13, 26, 39};
        threeCards = new List<int> {1, 14, 27, 40};
        fourCards = new List<int> {2, 15, 28, 41};
        fiveCards = new List<int> {3, 16, 29, 42};
        sixCards = new List<int> {4, 17, 30, 43};
        sevenCards = new List<int> {5, 18, 31, 44};
        eightCards = new List<int> {6, 19, 32, 45};
        nineCards = new List<int> {7, 20, 33, 46};
        tenCards = new List<int> {8, 10, 11, 12, 23, 24, 25, 34, 36, 37, 38, 47, 49, 50, 51};
        player = FindObjectOfType<Player>();
        player.balance = 0;
        player.currBet = 0;
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void UpdateElement (GameObject elem, bool active)
    {
        elem.SetActive(active);
    }

    public void AddToBet (float amt)
    {
        player.balance -= amt;
        player.currBet += amt;
        totalBal.text = "$" + player.balance;
        totalBet.text = "$" + player.currBet;

    }
    public void BetOne(){ AddToBet(1); }
    public void BetFive(){ AddToBet(5); }
    public void BetTwenty(){ AddToBet(20); }
    public void BetFifty(){ AddToBet(50); }
    

    public void FinalizeBet()
    {
        if (player.currBet == 0) { 
            StartCoroutine(AlertTimer(noBet.gameObject));
        } else{
            betOne.gameObject.SetActive(false);
            betFive.gameObject.SetActive(false);
            betTwenty.gameObject.SetActive(false);
            betFifty.gameObject.SetActive(false);
            finalBet.gameObject.SetActive(false);
            dealButt.gameObject.SetActive(true);
        }
    }

    public void OpeningDeal()
    {
        dealButt.gameObject.SetActive(false);

        Debug.Log("OpeningDeal function called.");
        System.Random rand = new System.Random();
        int dlRand = rand.Next(cards.Count);
        usedCards.Add(dlRand);
        Debug.Log("dlRand: " + dlRand);

        int plRandOne = rand.Next(cards.Count);
        while(dlRand == plRandOne) { plRandOne = rand.Next(cards.Count); }
        usedCards.Add(plRandOne);
        Debug.Log("plRandOne: " + plRandOne);

        int plRandTwo = rand.Next(cards.Count);
        while (plRandTwo == dlRand || plRandTwo == plRandOne) { plRandTwo = rand.Next(cards.Count); }
        usedCards.Add(plRandTwo);
        Debug.Log("plRandTwo: " + plRandTwo);

        GameObject dlCard = new GameObject("Dealer Card");
        //GameObject cardBackObj = new GameObject("Card Back");
        GameObject plCardOne = new GameObject("Player Card One");
        GameObject plCardTwo = new GameObject("Player Card Two");

        activeSprites.Add(dlCard);
        activeSprites.Add(plCardOne);
        activeSprites.Add(plCardTwo);

        SpriteRenderer spriteRendererOne = dlCard.AddComponent<SpriteRenderer>();
        //SpriteRenderer spriteRendererTwo = cardBackObj.AddComponent<SpriteRenderer>();
        SpriteRenderer spriteRendererThree = plCardOne.AddComponent<SpriteRenderer>();
        SpriteRenderer spriteRendererFour = plCardTwo.AddComponent<SpriteRenderer>();

        Sprite dlCardSp = cards[dlRand];
        //Sprite cardBackSp = cardBack;
        Sprite plCardOneSp = cards[plRandOne];
        Sprite plCardTwoSp = cards[plRandTwo];

        spriteRendererOne.sprite = dlCardSp;
       // spriteRendererTwo.sprite = cardBack;
        spriteRendererThree.sprite = plCardOneSp;
        spriteRendererFour.sprite = plCardTwoSp;

        Vector2 dlPosOne = new Vector2 (-6, 3);
        Vector2 dlPosTwo = new Vector2 (-2, 3);
        lastDlCardPos = dlPosTwo;
        Vector2 plPosOne = new Vector2 (-6, 0);
        Vector2 plPosTwo = new Vector2 (-5.45f, 0);
        lastPlCardPos = plPosTwo;

        dlCard.transform.position = dlPosOne;
        //cardBackObj.transform.position = dlPosTwo;
        plCardOne.transform.position = plPosOne;
        plCardTwo.transform.position = plPosTwo;

        dealerScore = CalculateScore(new List<int> {dlRand});
        playerScore = CalculateScore(new List<int> {plRandOne, plRandTwo});

        
        dealerScoreTxt.text = dealerScore.ToString();
        playerScoreTxt.text = playerScore.ToString();
        Debug.Log("Dealer Score: " + dealerScore);
        Debug.Log("Dealer Score (ToString): " + dealerScore.ToString());


        if (playerScore == 21) {
            Text21.gameObject.SetActive(true);
            nextButton.gameObject.SetActive(true);

        } else {
            hitButt.gameObject.SetActive(true);
            standButt.gameObject.SetActive(true);
        }
    }

    public void SecondDeal()
    {
        Text21.gameObject.SetActive(false);
        nextButton.gameObject.SetActive(false);

        GameObject newCard = new GameObject("New Dealer Card");

        SpriteRenderer spriteRenderer = newCard.AddComponent<SpriteRenderer>();
        System.Random rand = new System.Random();
        int newCardRand = rand.Next(cards.Count);
        usedCards.Add(newCardRand);
        Sprite newCardSp = cards[newCardRand];
        spriteRenderer.sprite = newCardSp;
        activeSprites.Add(newCard);
        float newCardX = lastDlCardPos.x + xCardInc;
        float newCardY = lastDlCardPos.y + yCardInc;
        Vector2 newCardPos = new Vector2(newCardX, newCardY);
        newCard.transform.position = newCardPos;

        dealerScore += CalculateScore(new List<int> {newCardRand});
        dealerScoreTxt.text = "$" + dealerScore;

        if (dealerScore > 21) { Win(); }
        else if (dealerScore == 21) { 
            if (playerScore == 21) { Tie(); }
            else { Bust(); }
        } else { SecondDeal(); }
          //  hitButt.gameObject.SetActive(true);
           // standButt.gameObject.SetActive(true);
        //}
    }

    public int CalculateScore(List<int> cds)
    {
        Debug.Log(tenCards.Contains(51));
        Debug.Log(string.Join(", ", tenCards));
        Debug.Log("Entered CalculateScore function");
        int result = 0;
        foreach(int cd in cds)
        {
            Debug.Log("Card in CalculateScore loop: " + cd);
            if(oneCards.Contains(cd)) { result += 1; }
            if(twoCards.Contains(cd)) { result += 2; }
            if(threeCards.Contains(cd)) { result += 3; }
            if(fourCards.Contains(cd)) { result += 4; }
            if(fiveCards.Contains(cd)) { result += 5; }
            if(sixCards.Contains(cd)) { result += 6; }
            if(sevenCards.Contains(cd)) { result += 7; }
            if(eightCards.Contains(cd)) { result += 8; }
            if(nineCards.Contains(cd)) { result += 9; }
            if(tenCards.Contains(cd)) { result += 10; }
            Debug.Log("Result after one CalculateScore for loop iteration: " + result);
        }
        return result;
    }

    public void Hit()
    {
        GameObject newCard = new GameObject("New Player Card");
        activeSprites.Add(newCard);
        SpriteRenderer spriteRenderer = newCard.AddComponent<SpriteRenderer>();
        System.Random rand = new System.Random();
        int newCardRand = rand.Next(cards.Count);
        usedCards.Add(newCardRand);
        Sprite newCardSp = cards[newCardRand];
        spriteRenderer.sprite = newCardSp;
        float newCardX = lastPlCardPos.x + xCardInc;
        float newCardY = lastPlCardPos.y + yCardInc;
        Vector2 newCardPos = new Vector2(newCardX, newCardY);
        newCard.transform.position = newCardPos;
        playerScore += CalculateScore(new List<int> {newCardRand});
        playerScoreTxt.text = "$" + playerScore;
        if (playerScore > 21) { Bust(); }
        else if (playerScore == 21) { 
            Text21.gameObject.SetActive(true);
            nextButton.gameObject.SetActive(true);
        } //else {
          //  SecondDeal();
       // }
    }

    public void Bust()
    {
        foreach(GameObject sp in activeSprites) { sp.SetActive(false); }

        hitButt.gameObject.SetActive(false);
        standButt.gameObject.SetActive(false);
        playAgain.gameObject.SetActive(true);
        quitButton.gameObject.SetActive(true);

        playerScoreTxt.text = playerScore.ToString();
        dealerScoreTxt.text = dealerScore.ToString();
        player.currBet = 0;
        totalBal.text = "$" + player.balance;
        totalBet.text = "$" + 0;
        lossText.gameObject.SetActive(true);
    }

    public void Tie()
    {
        foreach(GameObject sp in activeSprites) { sp.SetActive(false); }

        hitButt.gameObject.SetActive(false);
        standButt.gameObject.SetActive(false);
        playAgain.gameObject.SetActive(true);
        quitButton.gameObject.SetActive(true);

        player.balance += player.currBet;
        playerScoreTxt.text = "$" + player.balance;
        dealerScoreTxt.text = "$" + dealerScoreTxt;
        player.currBet = 0;
        totalBet.text = "$" + 0;
        tieText.gameObject.SetActive(true);
    }

    public void Win()
    {
        foreach(GameObject sp in activeSprites) { sp.SetActive(false); }

        hitButt.gameObject.SetActive(false);
        standButt.gameObject.SetActive(false);
        playAgain.gameObject.SetActive(true);
        quitButton.gameObject.SetActive(true);

        player.balance += 2 * (player.currBet);
        playerScoreTxt.text = playerScore.ToString();
        dealerScoreTxt.text = dealerScore.ToString();
        player.currBet = 0;
        totalBal.text = "$" + player.balance;
        totalBet.text = "$" + 0;

        winText.gameObject.SetActive(true);
    }

    public void PlayAgain()
    {
        if (player.balance <= 0){
            StartCoroutine(AlertTimer(noMoney.gameObject));
        }
        playAgain.gameObject.SetActive(false);
        quitButton.gameObject.SetActive(false);
        lossText.gameObject.SetActive(false);
        tieText.gameObject.SetActive(false);
        winText.gameObject.SetActive(false);

        betOne.gameObject.SetActive(true);
        betFive.gameObject.SetActive(true);
        betTwenty.gameObject.SetActive(true);
        betFifty.gameObject.SetActive(true);
        finalBet.gameObject.SetActive(true);
    }

    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }
    
    IEnumerator AlertTimer(GameObject alert)
    {
        alert.SetActive(true);
        yield return new WaitForSeconds(2f);
        alert.SetActive(false);
    }

}
