using System.Collections;
using GameCode.Tutorial;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace GameCode.CameraRig
{
    public class CameraController
    {
        private readonly CameraView _view;
        private readonly ITutorialModel _tutorialModel;

        public CameraController(CameraView view, ITutorialModel tutorialModel)
        {
            _view = view;
            _tutorialModel = tutorialModel;
            view.UpdateAsObservable()
                .Subscribe(_ => OnUpdate())
                .AddTo(view);
        }

        private void OnUpdate()
        {
            var yInput = Input.GetAxis("Vertical");

            if (Mathf.Abs(yInput) > 0)
            {
                if (_tutorialModel.ShouldShowTooltip.Value)
                {
                    MainThreadDispatcher.StartCoroutine(DisableTooltip());
                }

                var yPosition = _view.Position.y;
                yPosition += yInput * _view.Speed * Time.deltaTime;
                yPosition = Mathf.Clamp(yPosition, _view.VerticalLimit.x, _view.VerticalLimit.y);

                _view.Position = new Vector2(0, yPosition);
            }
        }

        private IEnumerator DisableTooltip()
        {
            yield return new WaitForSeconds(_view.TooltipDelay);
            _tutorialModel.ShouldShowTooltip.Value = false;
        }
    }
}