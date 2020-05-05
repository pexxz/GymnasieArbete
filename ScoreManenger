using UnityEngine;

public class Diamond : MonoBehaviour
{
    //Set scoreValue to how much point you want the diamond to give
    public int scoreValue;


    //plays audio, adds score, and the destroy diamond object.
    void OnTriggerEnter2D(Collider2D collision)
    {
        AudioManenger.Instance.PlaySFX(AudioManenger.Instance.collectDiamondSfx);
        ScoreManenger.myScore = ScoreManenger.myScore + scoreValue;
        Destroy(gameObject);
    }
}
