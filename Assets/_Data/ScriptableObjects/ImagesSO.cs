using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Images", menuName = "ScriptableObjects/Images", order = 1)]
public class ImagesSO : ScriptableObject
{
    public List<Sprite> images;
}