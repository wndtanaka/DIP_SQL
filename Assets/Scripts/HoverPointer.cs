using UnityEngine;

public class HoverPointer : MonoBehaviour
{
    [SerializeField]
    Animator anim;
    [SerializeField]
    GameObject hoverGameObject;

    void Start()
    {
        anim = GetComponentInParent<Animator>();
    }

    public void HoverIn()
    {
        hoverGameObject.SetActive(true);
        anim.SetBool(this.name, true);
    }

    public void HoverOut()
    {
        hoverGameObject.SetActive(false);
        anim.SetBool(this.name,false);
    }
}
