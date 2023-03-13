using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{

    public float speed = 4f;
    public GameObject block, equationBlock, explositionEffects, LosePanel;
    public Transform blocksPos, equationBlockPos;
    public Text scoreText;
    public Text finalScoreText, bestScoreText;
    public Text motivationalScoreText;
    public Color[] colors;
    public int rightAnswer;
    public List<Color> rightColors = new List<Color>();
    public string[] motivationalStrings;
    public bool isLost = false;
    public AudioClip destructionSound;
    public AudioClip collectSound;


    List<Color> generatedColors = new List<Color>();
    GameObject newBlock;
    GameObject newEqBlock;
    Animator myAnim;
    List<GameObject> blocksList = new List<GameObject>();
    AudioSource myAudio;

    int maxValue = 3;
    int score = 0;
    int indiceOfRightColor = -1;
    List<string> generatedEquations = new List<string>();

    short level = 0;


    List<int> nums = new List<int>();
    bool generateRightAnswerAgain = true;


    const int blockslength = 5;
    const int increaseFactor = 2;
    const int scoreToReachBeforeChangingTheme = 0;
    const float generatePeriod = 5.8f;
    void Start()
    {
        InvokeRepeating("GenerateAll", 0, generatePeriod);
        myAudio = GetComponent<AudioSource>();
        myAnim = GetComponent<Animator>();
    }

    void GenerateAll()
    {
        LevelManager();
        GenerateColor();
        GenerateEquationBlock();
        GenerateBlock();
    }

    void GenerateColor()
    {
        if (score < scoreToReachBeforeChangingTheme)
            rightColors.Add(colors[0]);
        else
        {
            rightColors.Add(colors[Random.Range(0, colors.Length)]);
            for (int i = 0; i < blockslength; i++)
            {
                generatedColors.Add(colors[Random.Range(0, colors.Length)]);
            }
        }
    }

    void GenerateBlock()
    {
        newBlock = Instantiate(block, blocksPos.position, Quaternion.identity, blocksPos);

        GenerateList();

        newBlock.GetComponent<Blocks>().GenerateRandomNumbers(nums);
        newBlock.GetComponent<Blocks>().ChangeBoxColors(generatedColors, indiceOfRightColor, rightColors[0]);
        newBlock.GetComponent<RectTransform>().position = blocksPos.GetComponent<RectTransform>().position;
        nums = new List<int>();
        blocksList.Add(newBlock);

    }
    void GenerateEquationBlock()
    {
        if (generateRightAnswerAgain)
            GenerateEquation();

        newEqBlock = Instantiate(equationBlock, equationBlockPos.position, Quaternion.identity, equationBlockPos);

        newEqBlock.GetComponent<EquationBlock>().ChangeText(generatedEquations.FirstOrDefault());
        newEqBlock.GetComponent<EquationBlock>().ChangeBackGroundColor(rightColors.FirstOrDefault());
        newEqBlock.GetComponent<RectTransform>().position = equationBlockPos.GetComponent<RectTransform>().position;
    }

    void GenerateList()
    {
        int number;
        for (int i = 0; i < blockslength; i++)
        {
            number = rightAnswer + Random.Range(1, 7);
            nums.Add(number);
        }
        indiceOfRightColor = Random.Range(0, nums.Count);
        nums[indiceOfRightColor] = rightAnswer;
    }

    void GenerateEquation()
    {
        generateRightAnswerAgain = false;
        int a, b, c;

        a = Random.Range(0, maxValue);
        b = Random.Range(0, maxValue);
        c = Random.Range(0, maxValue);
        rightAnswer = 0;

        switch (level)
        {
            case 0:
                rightAnswer = a + b;
                generatedEquations.Add(a.ToString() + "+" + b.ToString());
                break;
            case 1:
                rightAnswer = a - b;
                generatedEquations.Add(a.ToString() + "-" + b.ToString());
                break;
            case 2:
                rightAnswer = a + b + c;
                generatedEquations.Add(a.ToString() + "+" + b.ToString() + "+" + c.ToString());
                break;
            case 3:
                rightAnswer = a + b - c;
                generatedEquations.Add(a.ToString() + "+" + b.ToString() + "-" + c.ToString());
                break;
            case 4:
                rightAnswer = a * b;
                generatedEquations.Add(a.ToString() + "x" + b.ToString());
                break;
            case 5:
                rightAnswer = a * b + c;
                generatedEquations.Add(a.ToString() + "x" + b.ToString() + "+" + c.ToString());
                break;
            case 6:
                rightAnswer = a * b - c;
                generatedEquations.Add(a.ToString() + "x" + b.ToString() + "-" + c.ToString());
                break;
            case 7:
                rightAnswer = a * b * c;
                generatedEquations.Add(a.ToString() + "x" + b.ToString() + "x" + c.ToString());
                break;

            default:
                Debug.Log("ERRORRRR!");
                break;
        }
    }


    public void Lose(GameObject player, Vector2 pos)
    {
        Instantiate(explositionEffects, pos, Quaternion.identity);
        Destroy(player);
        StartCoroutine(StopGameAfterTime(1.5f));
        PlayAnimationAndEnablePanel();
        speed = 0;
        isLost = true;
        myAudio.PlayOneShot(destructionSound);
    }

    void PlayAnimationAndEnablePanel()
    {
        SaveScript.isGameOver = true;
        LosePanel.SetActive(true);
        myAnim.Play("GameOver");
        Camera.main.GetComponent<AudioSource>().enabled = false;
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("FadeAble"))
        {
            g.GetComponent<Animator>().SetBool("isOver", true);
        }
        finalScoreText.text = scoreText.text;
        if (score > PlayerPrefs.GetInt("BestScore", 0)) PlayerPrefs.SetInt("BestScore", score);
        bestScoreText.text = "Best : " + (PlayerPrefs.GetInt("BestScore")).ToString();
    }

    IEnumerator StopGameAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        Time.timeScale = 0;
    }

    public void IncrementScore(GameObject obj)
    {
        score += 10;
        maxValue += increaseFactor;
        motivationalScoreText.text = motivationalStrings[Random.Range(0, motivationalStrings.Length)];
        myAnim.Play("ScoreInc");
        myAudio.PlayOneShot(collectSound);
        obj.GetComponent<Collider2D>().enabled = false;
        scoreText.text = score.ToString();
        rightAnswer = 0;
        rightColors.RemoveAt(0);
        generatedEquations.RemoveAt(0);
        blocksList.RemoveAt(0);
        generateRightAnswerAgain = true;
    }

    void LevelManager()
    {
        switch (score)
        {
            case 50:
                level = 1;
                break;
            case 100:
                level = 2;
                break;
            case 150:
                level = 3;
                break;
            case 200:
                level = 4;
                maxValue = 4;
                break;
            case 250:
                level = 5;
                maxValue = 4;
                break;
            case 300:
                level = 6;
                maxValue = 4;
                break;
            case 350:
                level = 7;
                maxValue = 2;
                break;
        }
    }
}
