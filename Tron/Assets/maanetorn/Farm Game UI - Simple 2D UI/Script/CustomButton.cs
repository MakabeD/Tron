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
    /// <summary>
    /// Custom button que mueve el texto al presionar y al soltar verifica
    /// si en sus hijos hay un LogData (con la info esMalicioso) y dispara eventos.
    /// </summary>
    public class CustomButton : Button, IPointerDownHandler, IPointerUpHandler
    {
        RectTransform textRect;
        Vector2 originalTextPos;

        public bool isHeld;

        [Header("Events")]
        public static Action onMalicious; // conectar desde inspector
        public static Action onBenign;    // conectar desde inspector

        protected override void Awake()
        {
            base.Awake();

            // Intentamos obtener el TMP dentro de los hijos (puede lanzar si no existe)
            var tmp = GetComponentInChildren<TextMeshProUGUI>();
            if (tmp != null)
            {
                textRect = tmp.rectTransform;
                originalTextPos = textRect.anchoredPosition;
            }
            else
            {
                Debug.LogWarning($"CustomButton '{name}' no encontró TextMeshProUGUI en hijos.");
            }
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);
            isHeld = true;
            ApplyPressedVisual();
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            base.OnPointerUp(eventData);
            isHeld = false;
            ApplyNormalVisual();

            // Al soltar el botón, verificamos el LogData en los hijos
            CheckChildLogData();
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

        /// <summary>
        /// Busca un LogData en los hijos y ejecuta la acción correspondiente.
        /// Usa includeInactive = true por si el objeto hijo está desactivado.
        /// </summary>
        private void CheckChildLogData()
        {
            // Busca componente LogData en hijos (incluyendo inactivos)
            var log = GetComponentInChildren<LogData>(true);
            if (log == null)
            {
                Debug.LogWarning($"CustomButton '{name}': no se encontró LogData en hijos.");
                return;
            }

            if (log.isMalicious)
            {
                HandleMalicious(log);
            }
            else
            {
                HandleBenign(log);
            }
        }

        /// <summary>
        /// Acción por defecto al detectar log malicioso.
        /// Invoca UnityEvent onMalicious y hace un Debug.Log.
        /// Personaliza a tu gusto o conecta listeners en el Inspector.
        /// </summary>
        private void HandleMalicious(LogData log)
        {
            Debug.Log($"[CustomButton] Log marcado COMO MALICIOSO: {log.texto}");
            onMalicious?.Invoke();

            // Ejemplo adicional: cambiar color del texto a rojo si existe
            var tmp = GetComponentInChildren<TextMeshProUGUI>();
            if (tmp != null)
                tmp.color = Color.red;
        }

        /// <summary>
        /// Acción por defecto al detectar log benigno.
        /// Invoca UnityEvent onBenign y hace un Debug.Log.
        /// </summary>
        private void HandleBenign(LogData log)
        {
            Debug.Log($"[CustomButton] Log benigno: {log.texto}");
            onBenign?.Invoke();

            // Ejemplo adicional: restaurar color (o setear a verde)
            var tmp = GetComponentInChildren<TextMeshProUGUI>();
            if (tmp != null)
                tmp.color = Color.white;
        }
    }

}

