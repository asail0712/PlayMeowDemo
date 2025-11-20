namespace XPlan.UI
{
    public interface IUIView
    {
        void InitialUI(int idx);

        int SortIdx { get; set; }
    }
}