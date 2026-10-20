using R3;
using SimpleUi.Abstracts;
using UnityEngine.EventSystems;

namespace Game.Ui.PlayerMovementInput
{
    public class PlayerMovementInputView : UiView,
        IPointerDownHandler,
        IPointerUpHandler,
        IPointerMoveHandler
    {
        public ReactiveCommand<PointerEventData> OnPointerDownCmd { get; } = new();
        public ReactiveCommand<PointerEventData> OnPointerMoveCmd { get; } = new();
        public ReactiveCommand<PointerEventData> OnPointerUpCmd { get; } = new();

        public void OnPointerDown(PointerEventData eventData)
        {
            // Более показать не могу
        }

        public void OnPointerMove(PointerEventData eventData)
        {
            // Более показать не могу
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            // Более показать не могу
        }
    }
}