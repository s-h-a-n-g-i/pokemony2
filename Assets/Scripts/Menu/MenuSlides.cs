using UnityEngine;

public class MenuSlides : MonoBehaviour
{

    [SerializeField] private int xBasic = 570;
    [SerializeField] private int xMiddle = 320;
    [SerializeField] private int xMax = -1130;

    private RectTransform rTransform;

    void Start()
    {
        rTransform = GetComponent<RectTransform>();
        rTransform.localPosition = new Vector3(xBasic, 0, 0);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Z) && rTransform.localPosition.x == xBasic) 
        {
            MiddleRangeMenu();
        }
    }

    public void CloseMenu()
    {
        
        rTransform.localPosition = new Vector3(xBasic, 0, 0); 
    }

    private void MiddleRangeMenu()
    {
        rTransform.localPosition = new Vector3(xMiddle, 0, 0);
    }

    public void MaxRangeMenu()
    {
        rTransform.localPosition = new Vector3(xMax, 0, 0);
    }

}
