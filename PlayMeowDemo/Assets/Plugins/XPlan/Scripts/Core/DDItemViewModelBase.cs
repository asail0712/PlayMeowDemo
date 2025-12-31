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
    public class DDItemViewModelBase : ItemViewModelBase
    {
        private int id = -1;

        public DDItemViewModelBase()
        {

        }

        public void Init(int id)
        {
            this.id = id;
        }
    }
}
