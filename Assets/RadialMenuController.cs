using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class RadialMenuController : MonoBehaviour
{
    #region Variables
    List<Button> childButtons = new List<Button>();
    Vector2[] buttonGoalPos;
    bool open = false;
    int buttonDistance = 100;
    float speed = 2f;
    #endregion

    // Use this for initialization
    void Start()
    {
        // get all button components
        childButtons = this.GetComponentsInChildren<Button>(true).Where(x => x.gameObject.transform.parent != transform.parent).ToList();
        // set a new Vector2[childButtons.Count] for buttonGoalPos
        buttonGoalPos = new Vector2[childButtons.Count];
        // assign menu button's onClick function
        this.GetComponent<Button>().onClick.AddListener(() => { OpenMenu(); });
        // centre pivot point
        this.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
        // foreach button in childButtons
        foreach (Button b in childButtons)
        {
            // set each button position to this gameobject position
            b.gameObject.transform.position = this.transform.position;
            // set each button color to white
            b.gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 0);
            // deactivate each button
            b.gameObject.SetActive(false);
        }
    }

    public void OpenMenu()
    {
        // flip open bool
        open = !open;
        // store angle as a float counting a 90 degree / by number of childButtons * Mathf.Deg2Rad
        float angle = 90 / (childButtons.Count - 1) * Mathf.Deg2Rad;
        // for loop by number of childButtons
        for (int i = 0; i < childButtons.Count; i++)
        {
            // if open is true
            if (open)
            {
                // assigning float xPos to Mathf.Cos(angle * i) * buttonDistance
                float xPos = Mathf.Cos(angle * i) * buttonDistance;
                // assigning float yPos to Mathf.Sin(angle * i) * buttonDistance
                float yPos = Mathf.Sin(angle * i) * buttonDistance;
                // set buttonGoalPos for each i in a new position, in this gameobject transform position + xPos, yPos
                buttonGoalPos[i] = new Vector2(this.transform.position.x + xPos, this.transform.position.y + yPos);
            }
            // if open is false
            else
            {
                // set the buttonGoalPos position into this gameobject transform position
                buttonGoalPos[i] = this.transform.position;
            }
        }
        // call MoveButtons()
        StartCoroutine(MoveButtons());
    }

    IEnumerator MoveButtons()
    {
        // foreach button in childButtons
        foreach (Button b in childButtons)
        {
            // activate buttons
            b.gameObject.SetActive(true);
        }
        // assign int loops to zero
        int loops = 0;
        // while loops is equal or less than buttonDistance / speed
        while (loops <= buttonDistance / speed)
        {
            // wait for 0.01 seconds
            yield return new WaitForSeconds(0.01f);
            // looping through childButtons
            for (int i = 0; i < childButtons.Count; i++)
            {
                // shifting color 
                // get color component from all the childButtons gameobject and store in an new variable called c
                Color c = childButtons[i].gameObject.GetComponent<Image>().color;
                // if open is true
                if (open)
                {
                    // change alpha color to 1, change speed increasse gradually using Mathf.Lerp
                    c.a = Mathf.Lerp(c.a, 1, speed * Time.deltaTime);
                }
                else
                {
                    // change alpha color to 0, change speed increasse gradually using Mathf.Lerp
                    c.a = Mathf.Lerp(c.a, 0, speed * Time.deltaTime);
                }
                // set color component of all childButtons to c
                childButtons[i].gameObject.GetComponent<Image>().color = c;
                // Lerp the transform position of all childButtons to buttonGoalPos, speed * Time.deltaTime
                childButtons[i].gameObject.transform.position = Vector2.Lerp(childButtons[i].gameObject.transform.position, buttonGoalPos[i], speed * Time.deltaTime);
            }
            // increment loops
            loops++;
        }
        // if open is false
        if (!open)
        {
            // foreach button in childButtons
            foreach (Button b in childButtons)
            {
                // deactivate every button
                b.gameObject.SetActive(false);
            }
        }
    }
}
