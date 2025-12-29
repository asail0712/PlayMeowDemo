using UnityEngine;

namespace XPlan
{
    public class DragDropRequest
    {
        public readonly DragDropViewModelBase Source;
        public readonly DragDropViewModelBase Target;

        public DragDropRequest(DragDropViewModelBase source, DragDropViewModelBase target)
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


    public class DragDropViewModelBase : ItemViewModelBase
    {
        // payload（這次 drop 的內容）
        private ObservableProperty<DragDropRequest> DropRequest { get; }
            = new ObservableProperty<DragDropRequest>(null);

        // trigger（保證每次都會通知）
        private ObservableProperty<int> DropSeq { get; }
            = new ObservableProperty<int>(0);

        // output（VM 處理完的結果）
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

        internal void RequestDrop(DragDropViewModelBase source)
        {
            DropRequest.Value = new DragDropRequest(source, this);
            DropSeq.Value++;
        }

        private void HandleDrop(DragDropRequest request)
        {
            if (request.Source.GetType() != request.Target.GetType())
            {
                DropResult.Value = DragDropResult.Fail("Not Same Type !!");
                return;
            }

            OnHandleDrop(request);
        }

        protected virtual void OnHandleDrop(DragDropRequest request)
        {
            // 依照業務邏輯修改 DropResult
        }
    }
}
