using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class craneHandler : MonoBehaviour
{
    public float craneMin = -12f;
    public float craneMax = 11f;
    public GameObject bot;
    public float speed = 1f;

    private float lastMovePosition = -90f;

    public float accuracy = 0.1f;
    public float pos;
    public bool mv;

    private void Update()
    {
        if (mv)
        {
            ChangeBotPositionTo(pos);
            mv = false;
        }
    }
    private void ChangeBotPositionTo(float position)
    {
        if (lastMovePosition > craneMin && lastMovePosition < craneMax) 
        {
            StopCoroutine(MoveBot(lastMovePosition));
        }
        StartCoroutine(MoveBot(position));
    }

    IEnumerator MoveBot(float position)
    {
        if (position > craneMax)
        {
            position = craneMax;
        }
        if (position < craneMin)
        {
            position = craneMin;
        }
        lastMovePosition = position;
        while (Mathf.Abs(bot.transform.position.z - position) > accuracy)
        {
            if (bot.transform.position.z > position)
            {
                bot.transform.position -= new Vector3(0f, 0f, speed * Time.deltaTime);
            }
            else
            {
                bot.transform.position += new Vector3(0f, 0f, speed * Time.deltaTime);
            }
            yield return null;
        }
        bot.transform.position = new Vector3(bot.transform.position.x, bot.transform.position.y, position);
        lastMovePosition = -90f;
    }
}
