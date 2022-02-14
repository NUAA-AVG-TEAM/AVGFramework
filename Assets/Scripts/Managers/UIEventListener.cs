using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIEventListener : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{

	// �����¼�����
	public delegate void UIEventProxy(GameObject gb);

	// ������¼�
	public event UIEventProxy OnClick;

	// �������¼�
	public event UIEventProxy OnMouseEnter;

	// ��껬���¼�
	public event UIEventProxy OnMouseExit;

	public void OnPointerClick(PointerEventData eventData)
	{
		if (OnClick != null)
			OnClick(this.gameObject);
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		if (OnMouseEnter != null)
			OnMouseEnter(this.gameObject);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		if (OnMouseExit != null)
			OnMouseExit(this.gameObject);
	}

}

