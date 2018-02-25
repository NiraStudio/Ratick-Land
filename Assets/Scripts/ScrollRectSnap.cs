using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
public class ScrollRectSnap : MonoBehaviour, IEndDragHandler, IBeginDragHandler
{
    public enum Direction
    {
        Horizontal,Vertical
    }
     
    public RectTransform Content;
    public ScrollRect.MovementType MovementType;
    public Direction MoveDirection;
    [Tooltip("Start by this script or start manually by other scripts")]
    public bool AutoStart;
    public bool Negetive;
    public bool Scaler;
    public Vector2 ScaleSize;


    RectTransform[] childTransforms;
    float[] distances;

    RectTransform _Rect;
    ScrollRect _RectScroll;

    int MinElementNumber;
    float ElementDistance;
    bool dragging;
    Vector2 baseSacle;
    bool Horizontal,wait;

    void Start()
    {
        if (MoveDirection == Direction.Horizontal)
            Horizontal = true;
        else
            Horizontal = false;

        _RectScroll = gameObject.AddComponent<ScrollRect>();
        _RectScroll.content = Content;
        _RectScroll.movementType = MovementType;
        _RectScroll.horizontal = Horizontal;
        _RectScroll.vertical = !Horizontal;
        _RectScroll.decelerationRate = 0;
        _Rect = GetComponent<RectTransform>();

        if (AutoStart)
            StartArrange(0);
    }


    void Update()
    {
        if (wait)
            return;
        for (int i = 0; i < distances.Length; i++)
        {
            if (Horizontal)
                distances[i] = Mathf.Abs(transform.position.x - childTransforms[i].transform.position.x);
            else
                distances[i] = Mathf.Abs(transform.position.y - childTransforms[i].transform.position.y);

        }


        if (!dragging)
            MoveTo((int)(MinElementNumber * ElementDistance));
        if (Scaler)
            changeScale();
    }




    public void StartArrange(int Focus)
    {
        StartCoroutine(FirstArrange(Focus));

    }


    public void ChangeFocus(int ElementNumber)
    {
        if (ElementNumber > childTransforms.Length || ElementNumber <= 0)
            return;


        MinElementNumber = ElementNumber-1 ;
        ChangeCharacter();

    }

    IEnumerator FirstArrange(int Focus)
    {
        wait = true;
        yield return new WaitForSeconds(0.1f);
        childTransforms = null;
        childTransforms = new RectTransform[_RectScroll.content.transform.childCount];

        for (int i = 0; i < _RectScroll.content.transform.childCount; i++)
        {
            childTransforms[i] = _RectScroll.content.transform.GetChild(i).GetComponent<RectTransform>();
        }
        baseSacle = childTransforms[0].localScale;
        distances = new float[childTransforms.Length];

        yield return new WaitForSeconds(0.01f);
        for (int i = 0; i < distances.Length; i++)
        {
            if (Horizontal)
                distances[i] = Mathf.Abs(transform.position.x - childTransforms[i].transform.position.x);
            else
                distances[i] = Mathf.Abs(transform.position.y - childTransforms[i].transform.position.y);

        }

        if (Horizontal)
            ElementDistance = Mathf.Abs(childTransforms[0].anchoredPosition.x - childTransforms[1].anchoredPosition.x);
        else
            ElementDistance = Mathf.Abs(childTransforms[0].anchoredPosition.y - childTransforms[1].anchoredPosition.y);


        ChangeFocus(Focus);
        wait = false;
    }

    void MoveTo(int pos)
    {
        float t = 0;
        Vector2 a = new Vector2();
        int g;
        if (Horizontal)
        {
            g = Negetive == true ? 1 : -1;
            t = Mathf.Lerp(Content.anchoredPosition.x,g*  pos, Time.deltaTime * 20);
            a = new Vector2(t, Content.anchoredPosition.y);
        }
        else
        {
            g = Negetive == true ? -1 : 1;
            t = Mathf.Lerp(_RectScroll.content.anchoredPosition.y, g * pos, Time.deltaTime * 20);
            a = new Vector2(_RectScroll.content.anchoredPosition.x, t);
        }

        Content.anchoredPosition = a;
    }

    void changeScale()
    {
        float min = Mathf.Min(distances);
        for (int i = 0; i < distances.Length; i++)
        {
            if (distances[i] == min)
            {
                childTransforms[i].localScale = Vector2.Lerp(childTransforms[i].localScale, ScaleSize, Time.deltaTime * 10);
            }
            else
            {
                childTransforms[i].localScale = Vector2.Lerp(childTransforms[i].localScale, baseSacle, Time.deltaTime * 10);

            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        dragging = false;
        float t = Mathf.Min(distances);
        if (Negetive)
        {
            for (int i = 0; i < distances.Length; i++)
            {
                if (distances[i] == t)
                {
                    MinElementNumber = distances.Length - 1 - i;
                    break;
                }
            }
        }
        else
        {
            for (int i = 0; i < distances.Length; i++)
            {
                if (distances[i] == t)
                {
                    MinElementNumber = i;
                    break;
                }
            }
        }
        ChangeCharacter();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        dragging = true;
    }
    public void ChangeCharacter()
    {
        CampaignMenuManager.Instance.ChangeCurrentCharacter(childTransforms[MinElementNumber].GetComponent<CharacterCampaignCard>().data);
    }
}