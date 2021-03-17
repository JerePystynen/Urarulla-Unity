namespace WheelOfJoy
{
    [System.Serializable]
    public class JobAdvert
    {
        public string title;
        public string url;

        public JobAdvert(string title, string url)
        {
            this.title = title;
            this.url = url;
        }
    }
}
