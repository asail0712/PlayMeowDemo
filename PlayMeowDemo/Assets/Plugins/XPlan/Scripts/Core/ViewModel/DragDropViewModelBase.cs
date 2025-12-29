using UnityEditor.PackageManager.Requests;
using UnityEngine;

namespace XPlan
{
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
        // output（VM 處理完的結果）
        public ObservableProperty<DragDropResult> DropResult { get; }
            = new ObservableProperty<DragDropResult>(null);


        internal void RequestDrop(DragDropViewModelBase source)
        {
            if (source.GetType() != GetType())
            {
                DropResult.Value = DragDropResult.Fail("Not Same Type !!");
                return;
            }

            HandleDrop(source);
        }

        protected void HandleDrop(DragDropViewModelBase source)
        {
            // 依照業務邏輯修改 DropResult
        }
    }
}
