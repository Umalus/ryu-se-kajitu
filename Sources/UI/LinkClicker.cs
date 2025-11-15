using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class LinkClicker : MonoBehaviour,IPointerClickHandler {
    public void OnPointerClick(PointerEventData _event) {

        Vector2 clickPos = Input.mousePosition;

        TextMeshProUGUI text = GetComponent<TextMeshProUGUI>();

        Canvas canvas = text.canvas;

        Camera camera = canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : canvas.worldCamera;

        int textIndex = TMP_TextUtilities.FindIntersectingLink(text, clickPos, camera);

        if (textIndex != -1) {
            TMP_LinkInfo linkInfo = text.textInfo.linkInfo[textIndex];

            string url = linkInfo.GetLinkID();

            Application.OpenURL(url);
        }
    }
}
