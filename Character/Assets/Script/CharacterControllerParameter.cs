using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterControllerParameter
{
    [Range(0.1f, 10f)] public float speed;
    [Range(0.1f, 5f)] public float chargeMulti;
    [Range(0.1f, 20f)]public float jumpStr;
}
