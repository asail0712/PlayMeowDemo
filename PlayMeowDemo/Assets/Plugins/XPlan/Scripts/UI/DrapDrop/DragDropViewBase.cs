using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

using XPlan;
using XPlan.Recycle;
using XPlan.Utility;

namespace XPlan.UI
{
    public class DragDropViewBase<TViewModel> : MonoBehaviour where TViewModel : DragDropViewModelBase
    {
        private TViewModel _viewModel;
        private EventTrigger _eventTrigger;
        private readonly Dictionary<string, ObservableBinding> _vmObservableMap     = new(StringComparer.Ordinal);  // 新增：把 VM 內的 ObservableProperty 索引起來（baseName → 綁定資訊）
        private readonly List<IDisposable> _disposables                             = new();                        // Item View 內部的訂閱列表
        private readonly SpriteCache _spriteCache                                   = new();                        // 每個 Item View 使用自己的 SpriteCache 以供 Image 綁定


        private static Dictionary<Type, DragDropViewBase<TViewModel>> DragdropDict  = new Dictionary<Type, DragDropViewBase<TViewModel>>();

        /// <summary>
        /// 由 TableView 呼叫，設定此單元的 ViewModel 並執行自動綁定。
        /// </summary>
        public void SetViewModel(TViewModel vm)
        {
            // 清理舊的訂閱
            CleanupBindings();

            _viewModel = vm;

            if (vm == null) return;

            // 註冊 Event Trigger
            RegisterEventTriggers();

            ViewBindingHelper.IndexVmObservables(vm, _vmObservableMap);

            // VM → UI 綁定：利用 ViewBindingHelper
            // Item View 通常只處理 VM→UI 綁定，不需要 UI→VM 的 AutoRegisterComponents
            ViewBindingHelper.AutoBindObservables(
                this,
                vm,
                _disposables,
                _spriteCache);

            ViewBindingHelper.AutoBindObservableHandlers(this, _vmObservableMap, _disposables);

            OnDataBound();

            _viewModel.DropResult.Subscribe(result =>
            {
                if (result == null) return;

                if (!result.Success && result.ShouldSnapBack)
                    SnapBack();
            });
        }

        protected virtual void OnDataBound()
        {
            // 留給子類別實作，在 ViewModel 綁定和 UI 初始化完成後執行客製化邏輯
        }

        protected virtual void SnapBack()
        {
            // 留給子類別實作
        }

        private void OnDrop(object dragPayload)
        {
            // 寫 payload
            _viewModel.RequestDrop(dragPayload, this);
        }

        // ===============================
        // Event Trigger
        // ===============================
        private void RegisterEventTriggers()
        {
            _eventTrigger = gameObject.AddOrFindComponent<EventTrigger>();

            _eventTrigger.triggers ??= new List<EventTrigger.Entry>();
            _eventTrigger.triggers.Clear();

            AddTrigger(EventTriggerType.BeginDrag, OnBeginDragEvent);
            AddTrigger(EventTriggerType.Drop, OnDropEvent);
            AddTrigger(EventTriggerType.EndDrag, OnEndDragEvent);
        }

        private void AddTrigger(EventTriggerType type, Action<BaseEventData> callback)
        {
            var entry = new EventTrigger.Entry
            {
                eventID = type
            };

            entry.callback.AddListener(data => callback(data));
            _eventTrigger.triggers.Add(entry);
        }

        private void OnBeginDragEvent(BaseEventData data)
        {
            if (_viewModel == null) return;

            if (!TryGetDragDropView(data, out DragDropViewBase<TViewModel> payload))
            {
                return;
            }

            if(!DragdropDict.ContainsKey(payload.GetType()))
                DragdropDict.Add(payload.GetType(), payload);
            else
                DragdropDict[payload.GetType()] = payload;
        }

        private void OnEndDragEvent(BaseEventData data)
        {
            if (_viewModel == null) return;

            if (!TryGetDragDropView(data, out DragDropViewBase<TViewModel> payload))
            {
                return;
            }

            DragdropDict.Remove(payload.GetType());
        }

        private void OnDropEvent(BaseEventData data)
        {
            if(!DragdropDict.ContainsKey(GetType()))
            {
                return;
            }

            OnDrop(DragdropDict[GetType()]);
        }

        private bool TryGetDragDropView(BaseEventData data, out DragDropViewBase<TViewModel> view)
        {
            view = null;

            if (data is not PointerEventData ped)
                return false;

            view = ped.pointerDrag
                ?.GetComponent<DragDropViewBase<TViewModel>>();

            if (view == null || view.GetType() != GetType())
                return false;

            return true;
        }

        // ===============================
        // Internal
        // ===============================

        private void CleanupBindings()
        {
            foreach (var d in _disposables)
                d?.Dispose();
            _disposables.Clear();

            _spriteCache.Dispose();
            _vmObservableMap.Clear();
        }
    }

}
