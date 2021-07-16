using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputStr : MonoBehaviour
{
    public InputField input;

    public string GetreturnInput()
    {
        return input.text;
    }
}
