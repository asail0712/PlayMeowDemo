using UnityEngine;

namespace XPlan
{
    public class DragDropRequest
    {
        public readonly object Source;
        public readonly object Target;

        public DragDropRequest(object source, object target)
        {
            Source = source;
            Target = target;
        }
    }

    public sealed class DragDropResult
    {
        public bool Success;
        public bool ShouldSnapBack;
        public string Reason;

        public static DragDropResult Ok()
            => new DragDropResult { Success = true };

        public static DragDropResult Fail(string reason, bool snapBack = true)
            => new DragDropResult
            {
                Success         = false,
                Reason          = reason,
                ShouldSnapBack  = snapBack
            };
    }


    public class DragDropViewModelBase : ViewModelBase
    {
        /// payload（這次 drop 的內容）
        public ObservableProperty<DragDropRequest> DropRequest { get; }
            = new ObservableProperty<DragDropRequest>(null);

        /// trigger（保證每次都會通知）
        public ObservableProperty<int> DropSeq { get; }
            = new ObservableProperty<int>(0);

        /// output（VM 處理完的結果）
        public ObservableProperty<DragDropResult> DropResult { get; }
            = new ObservableProperty<DragDropResult>(null);

        protected DragDropViewModelBase()
        {
            // VM 只關心「DropSeq 變了」
            DropSeq.Subscribe(_ =>
            {
                if (DropRequest.Value != null)
                    HandleDrop(DropRequest.Value);
            });
        }

        protected virtual void HandleDrop(DragDropRequest request)
        {

        }
    }

}
