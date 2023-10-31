using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TagManager : SingletoneBase<TagManager>
{
    public List<Sprite> images;
    public int TagReturn(GameObject obj)
    {
        int index = int.Parse(obj.tag);
        int returnTag = (index * 2);
        obj.tag = returnTag.ToString();
        Sprite spriteByName = images.Find(obj => obj.name == returnTag.ToString());
        obj.GetComponent<Image>().sprite = spriteByName;
        return returnTag;
    }





}
