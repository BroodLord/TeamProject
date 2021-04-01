using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class NPCBase : MonoBehaviour
{
    public virtual TextMeshProUGUI GetIntroText()
    {
        return null;
    }
    
    public virtual TextMeshProUGUI GetAboutText() 
    {
        return null;
    }

    public virtual TextMeshProUGUI GetDoingText()
    {
        return null;
    }
}
