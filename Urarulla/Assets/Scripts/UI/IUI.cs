namespace Urarulla
{
    public interface IUI
    {
        public float Depth { get; }
        public string CanvasName { get; }
        public void SetActive();
        public void SetDeactive();
    }
}