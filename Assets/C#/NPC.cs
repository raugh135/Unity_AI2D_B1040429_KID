using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NPC : MonoBehaviour
{
    #region
    public enum state
    {
        Start, notComplete, complete
    }

    public state NPCstate;

    [Header("對話")]
    public string sayStart = "請逃離這裡";
    public string sayNotComplete = "出口在最底端呦";
    public string sayComplete = "恭喜你成功了";
    [Header("對話速度")]
    public float speed = 1.5f;
    [Header("任務相關")]
    public bool complete;
    public int countPlayer;
    public int countFinish = 5;
    [Header("介面")]
    public GameObject objCanvas;
    public Text textSay;
    #endregion

    public GameObject win;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "玩家")
        {
            Say();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "玩家")
        {
            SayClose();
        }
    }

    private void Say()
    {
        objCanvas.SetActive(true);
        StopAllCoroutines();

        if (countPlayer >= countFinish) NPCstate = state.complete;

        switch (NPCstate)
        {
            case state.Start:
                StartCoroutine(ShowDialog(sayStart));
                NPCstate = state.notComplete;
                break;
            case state.notComplete:
                StartCoroutine(ShowDialog(sayNotComplete));
                break;
            case state.complete:
                StartCoroutine(ShowDialog(sayComplete));

                if (countFinish == 5) win.SetActive(true);
                break;
        }

    }

    private IEnumerator ShowDialog(string say)
    {
        textSay.text = "";
        for (int i = 0; i < say.Length; i++)
        {
            textSay.text += say[i].ToString();
            yield return new WaitForSeconds(speed);
        }
    }

    private void SayClose()
    {
        StopAllCoroutines();
        objCanvas.SetActive(false);
    }

    public void PlayerGet()
    {
        countPlayer++;
    }
}
