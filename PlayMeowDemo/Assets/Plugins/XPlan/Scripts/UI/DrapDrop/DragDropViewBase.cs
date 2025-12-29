using UnityEngine.EventSystems;

namespace XPlan.UI
{
    public class DragDropViewBase<TViewModel> : ItemViewBase<TViewModel>, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler 
        where TViewModel : DragDropViewModelBase
    {
        private static DragDropViewModelBase _currentDragging;

        protected override void OnDataBound()
        {
            // 留給子類別實作，在 ViewModel 綁定和 UI 初始化完成後執行客製化邏輯
            _disposables.Add(_viewModel.DropResult.Subscribe(result =>
            {
                if (result == null) return;

                if (!result.Success && result.ShouldSnapBack)
                    SnapBack();
            }));
        }

        private void OnDisable()
        {
            if (ReferenceEquals(_currentDragging, _viewModel))
                _currentDragging = null;
        }

        private new void OnDestroy()
        {
            base.OnDestroy();

            if (ReferenceEquals(_currentDragging, _viewModel))
                _currentDragging = null;
        }

        // ===============================
        // DragDrop 相關
        // ===============================
        protected virtual void SnapBack()
        {
            // 留給子類別實作
        }

        private void OnDrop(DragDropViewModelBase dragPayload)
        {
            if (_viewModel == null) return;

            // 寫 payload
            _viewModel.RequestDrop(dragPayload);
        }

        // ===============================
        // Event Trigger
        // ===============================
        public void OnBeginDrag(PointerEventData eventData)
        {
            if (_viewModel == null) return;

            _currentDragging = _viewModel;
        }

        public void OnDrag(PointerEventData data)
        {

        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (ReferenceEquals(_currentDragging, _viewModel))
                _currentDragging = null;
        }

        public void OnDrop(PointerEventData eventData)
        {
            if(_currentDragging == null)
                return;

            OnDrop(_currentDragging);
        }
    }
}
