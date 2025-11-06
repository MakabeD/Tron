using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace FGUIStarter
{
    public class CustomButton2 : Button, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
    {
        RectTransform textRect;
        Vector2 originalTextPos;

        public bool isHeld;

       
        
        

        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);
            isHeld = true;
            ApplyPressedVisual();
            
        }
        public override void OnPointerClick(PointerEventData eventData)
        {
            if (!interactable) return;

            
            base.OnPointerClick(eventData);

            
            interactable = false;

            
            DoStateTransition(SelectionState.Disabled, false);
        }
        public override void OnPointerUp(PointerEventData eventData)
        {
            base.OnPointerUp(eventData);
            isHeld = false;
            ApplyNormalVisual();

            // Al soltar el botón, verificamos el LogData en los hijos
            //CheckChildLogData(); <<<<<modificado>>>>
            
        }

        private void ApplyPressedVisual()
        {
            if (textRect != null)
            {
                float height = ((RectTransform)transform).rect.height;
                float offset = height - (height * 0.86718f); // cálculo original
                textRect.anchoredPosition = originalTextPos - new Vector2(0, offset);
            }
        }

        private void ApplyNormalVisual()
        {
            if (textRect != null)
            {
                textRect.anchoredPosition = originalTextPos;
            }
        }

        protected override void DoStateTransition(SelectionState state, bool instant)
        {
            base.DoStateTransition(state, instant);
            if (state == SelectionState.Pressed)
            {
                ApplyPressedVisual();
            }
            else
            {
                ApplyNormalVisual();
            }
        }

        
    }

}

