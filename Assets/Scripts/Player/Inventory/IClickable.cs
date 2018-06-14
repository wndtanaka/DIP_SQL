using UnityEngine.UI;

namespace RPG
{
    public interface IClickable
    {
        Image MyIcon
        {
            get;
            set;
        }
        int MyCount
        {

            get;
        }
        Text MyStackText
        { get; }
    }
}